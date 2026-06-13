using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PCActivityTimeline.Core
{
    public class ActivityTracker
    {
        private readonly WindowInfoProvider windowInfoProvider;
        private readonly ActivityManager activityManager;
        private List<KeywordRule> keywordRules = new List<KeywordRule>();
        private ActivityRecord currentRecord;

        public ActivityTracker(WindowInfoProvider windowInfoProvider, ActivityManager activityManager)
        {
            this.windowInfoProvider = windowInfoProvider;
            this.activityManager = activityManager;
        }

        public ActivityRecord TrackOnce()
        {
            WindowInfo info = windowInfoProvider.GetCurrentWindowInfo();
            DateTime now = DateTime.Now;

            if (ShouldIgnore(info))
            {
                if (currentRecord != null)
                    currentRecord.EndTime = now;

                currentRecord = null;
                return null;
            }

            if (currentRecord == null)
            {
                currentRecord = CreateRecord(info, now);
                activityManager.Add(currentRecord);
                return currentRecord;
            }

            bool sameProgram = currentRecord.ProgramName == info.ProgramName;
            bool sameTitle = currentRecord.WindowTitle == info.WindowTitle;

            if (sameProgram && sameTitle)
            {
                currentRecord.EndTime = now;
                return currentRecord;
            }

            currentRecord.EndTime = now;
            currentRecord = CreateRecord(info, now);
            activityManager.Add(currentRecord);
            return currentRecord;
        }

        public void StopCurrent()
        {
            if (currentRecord != null)
            {
                currentRecord.EndTime = DateTime.Now;
                currentRecord = null;
            }
        }

        public void SetKeywordRules(IEnumerable<KeywordRule> rules)
        {
            keywordRules = rules == null
                ? new List<KeywordRule>()
                : rules
                    .Where(r => r != null && !string.IsNullOrWhiteSpace(r.Keyword) && !string.IsNullOrWhiteSpace(r.Category))
                    .ToList();
        }

        public string GuessCategoryFor(string programName, string windowTitle)
        {
            return GuessCategory(new WindowInfo
            {
                ProgramName = programName ?? "",
                WindowTitle = windowTitle ?? ""
            });
        }

        private ActivityRecord CreateRecord(WindowInfo info, DateTime now)
        {
            return new ActivityRecord
            {
                Id = Guid.NewGuid().ToString("N"),
                ProgramName = info.ProgramName,
                WindowTitle = info.WindowTitle,
                Category = GuessCategory(info),
                StartTime = now,
                EndTime = now,
                Memo = ""
            };
        }

        private bool ShouldIgnore(WindowInfo info)
        {
            if (info == null) return true;

            string program = (info.ProgramName ?? "").Trim();
            string title = (info.WindowTitle ?? "").Trim();
            string lowerProgram = program.ToLowerInvariant();
            string lowerTitle = title.ToLowerInvariant();
            string currentProcess = "";

            try
            {
                currentProcess = Process.GetCurrentProcess().ProcessName.ToLowerInvariant();
            }
            catch
            {
                currentProcess = "pcactivitytimeline";
            }

            if (string.IsNullOrWhiteSpace(program)) return true;
            if (string.IsNullOrWhiteSpace(title)) return true;
            if (title == "(제목 없는 창)") return true;
            if (lowerProgram == currentProcess) return true;
            if (lowerProgram.Contains("pcactivitytimeline")) return true;

            if (lowerProgram == "explorer")
            {
                if (lowerTitle.Contains("시스템 트레이")) return true;
                if (lowerTitle.Contains("오버플로")) return true;
                if (lowerTitle.Contains("작업 전환")) return true;
                if (lowerTitle.Contains("알림 센터")) return true;
                if (lowerTitle.Contains("시작")) return true;
            }

            if (lowerProgram == "searchhost" || lowerProgram == "shellexperiencehost")
                return true;

            return false;
        }

        private string GuessCategory(WindowInfo info)
        {
            string name = (info.ProgramName ?? "").ToLowerInvariant();
            string title = (info.WindowTitle ?? "").ToLowerInvariant();
            string target = name + " " + title;

            foreach (var rule in keywordRules)
            {
                string keyword = (rule.Keyword ?? "").Trim().ToLowerInvariant();
                if (keyword.Length == 0) continue;
                if (target.Contains(keyword)) return rule.Category;
            }

            if (name.Contains("devenv") || name.Contains("code")) return "과제";
            if (name.Contains("chrome") || name.Contains("edge")) return "자료검색";
            if (name.Contains("zoom")) return "수업/시험";
            if (name.Contains("notepad") || name.Contains("hwp") || name.Contains("winword")) return "문서작성";
            return "미분류";
        }
    }
}
