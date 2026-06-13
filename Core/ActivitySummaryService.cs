using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCActivityTimeline.Core
{
    public class ActivitySummaryService
    {
        public string BuildSummary(IEnumerable<ActivityRecord> records)
        {
            var list = (records ?? new List<ActivityRecord>()).ToList();
            if (list.Count == 0) return "선택한 날짜의 활동 기록이 없습니다.";

            TimeSpan total = TimeSpan.FromTicks(list.Sum(r => r.Duration.Ticks));
            var topProgram = list
                .GroupBy(r => string.IsNullOrWhiteSpace(r.ProgramName) ? "Unknown" : r.ProgramName)
                .Select(g => new { Program = g.Key, Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)) })
                .OrderByDescending(x => x.Duration)
                .FirstOrDefault();

            var topCategory = list
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .Select(g => new { Category = g.Key, Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)) })
                .OrderByDescending(x => x.Duration)
                .FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine("오늘의 활동 요약");
            sb.AppendLine("총 기록 시간: " + Format(total));
            if (topProgram != null)
                sb.AppendLine("가장 오래 사용한 프로그램: " + topProgram.Program + " (" + Format(topProgram.Duration) + ")");
            if (topCategory != null)
                sb.AppendLine("가장 많은 분류: " + topCategory.Category + " (" + Format(topCategory.Duration) + ")");

            if (topProgram != null && total.TotalSeconds > 0)
            {
                double percent = topProgram.Duration.TotalSeconds / total.TotalSeconds * 100;
                sb.AppendLine(topProgram.Program + " 사용 시간이 전체의 " + percent.ToString("0.0") + "%입니다.");
            }

            return sb.ToString();
        }

        private string Format(TimeSpan value)
        {
            if (value.TotalHours >= 1)
                return string.Format("{0}시간 {1}분", (int)value.TotalHours, value.Minutes);
            return string.Format("{0}분 {1}초", value.Minutes, value.Seconds);
        }
    }
}
