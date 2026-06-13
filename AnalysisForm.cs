using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public partial class AnalysisForm : Form
    {
        private readonly DateTime targetDate;
        private readonly List<ActivityRecord> records;

        public AnalysisForm(DateTime targetDate, List<ActivityRecord> records)
        {
            this.targetDate = targetDate;
            this.records = records ?? new List<ActivityRecord>();
            InitializeComponent();
            LoadAnalysis();
            Shown += (sender, e) =>
            {
                txtAnalysis.SelectionStart = 0;
                txtAnalysis.SelectionLength = 0;
                gridPrograms.ClearSelection();
                gridCategories.ClearSelection();
                btnClose.Focus();
            };
        }

        private void LoadAnalysis()
        {
            Text = targetDate.ToString("yyyy-MM-dd") + " 활동 상세 분석";
            txtAnalysis.Text = BuildAnalysisText();
            StyleAnalysisText();
            LoadProgramGrid();
            LoadCategoryGrid();
        }

        private void StyleAnalysisText()
        {
            txtAnalysis.SelectAll();
            txtAnalysis.SelectionFont = new System.Drawing.Font("Malgun Gothic", 9.5F, System.Drawing.FontStyle.Regular);
            txtAnalysis.SelectionColor = System.Drawing.Color.FromArgb(30, 30, 30);
            txtAnalysis.SelectionBackColor = System.Drawing.Color.FromArgb(250, 252, 255);

            if (txtAnalysis.Text.Length > 0)
            {
                int firstLineLength = txtAnalysis.Text.IndexOf(Environment.NewLine);
                if (firstLineLength < 0) firstLineLength = txtAnalysis.Text.Length;
                txtAnalysis.Select(0, firstLineLength);
                txtAnalysis.SelectionFont = new System.Drawing.Font("Malgun Gothic", 11F, System.Drawing.FontStyle.Bold);
                txtAnalysis.SelectionColor = System.Drawing.Color.FromArgb(28, 81, 135);
            }

            int adviceIndex = txtAnalysis.Text.IndexOf("분석:");
            if (adviceIndex >= 0)
            {
                txtAnalysis.Select(adviceIndex, txtAnalysis.Text.Length - adviceIndex);
                txtAnalysis.SelectionFont = new System.Drawing.Font("Malgun Gothic", 9.5F, System.Drawing.FontStyle.Bold);
                txtAnalysis.SelectionColor = System.Drawing.Color.FromArgb(35, 100, 70);
            }

            txtAnalysis.Select(0, 0);
        }

        private string BuildAnalysisText()
        {
            if (records.Count == 0)
                return "선택한 날짜의 활동 기록이 없습니다.";

            TimeSpan total = TimeSpan.FromTicks(records.Sum(r => r.Duration.Ticks));
            int activityCount = records.Count;
            var longest = records.OrderByDescending(r => r.Duration).FirstOrDefault();
            var topProgram = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.ProgramName) ? "Unknown" : r.ProgramName)
                .Select(g => new { Name = g.Key, Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)) })
                .OrderByDescending(x => x.Duration)
                .FirstOrDefault();
            var topCategory = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .Select(g => new { Name = g.Key, Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)) })
                .OrderByDescending(x => x.Duration)
                .FirstOrDefault();

            var lines = new List<string>();
            lines.Add("오늘의 활동 상세 분석");
            lines.Add("총 기록 수: " + activityCount + "개");
            lines.Add("총 기록 시간: " + FormatDuration(total));

            if (topProgram != null)
                lines.Add("가장 오래 사용한 프로그램: " + topProgram.Name + " (" + FormatDuration(topProgram.Duration) + ")");
            if (topCategory != null)
                lines.Add("가장 많은 분류: " + topCategory.Name + " (" + FormatDuration(topCategory.Duration) + ")");
            if (longest != null)
                lines.Add("가장 긴 단일 활동: " + longest.ProgramName + " / " + FormatDuration(longest.Duration));

            lines.Add("");
            lines.Add(MakeAdvice(topCategory == null ? "" : topCategory.Name));
            return string.Join(Environment.NewLine, lines);
        }

        private string MakeAdvice(string topCategory)
        {
            if (topCategory == "과제")
                return "분석: 과제 관련 활동 비중이 높습니다. 작업 중심으로 PC를 사용한 날입니다.";
            if (topCategory == "자료검색")
                return "분석: 자료검색 시간이 가장 많습니다. 검색 이후 실제 정리/구현 시간이 충분했는지 확인해볼 수 있습니다.";
            if (topCategory == "휴식")
                return "분석: 휴식 분류 시간이 높습니다. 공부/작업 시간과 휴식 시간의 균형을 점검할 수 있습니다.";
            if (topCategory == "미분류")
                return "분석: 미분류 기록이 많습니다. 기록을 수정하여 분류를 지정하면 더 정확한 분석이 가능합니다.";
            return "분석: 분류별 사용 시간을 통해 하루 PC 사용 패턴을 확인할 수 있습니다.";
        }

        private void LoadProgramGrid()
        {
            gridPrograms.Rows.Clear();
            var totalSeconds = Math.Max(1, records.Sum(r => r.Duration.TotalSeconds));

            var groups = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.ProgramName) ? "Unknown" : r.ProgramName)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks))
                })
                .OrderByDescending(x => x.Duration)
                .ToList();

            foreach (var item in groups)
            {
                double percent = item.Duration.TotalSeconds / totalSeconds * 100;
                gridPrograms.Rows.Add(item.Name, item.Count, FormatDuration(item.Duration), percent.ToString("0.0") + "%");
            }
        }

        private void LoadCategoryGrid()
        {
            gridCategories.Rows.Clear();
            var totalSeconds = Math.Max(1, records.Sum(r => r.Duration.TotalSeconds));

            var groups = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks))
                })
                .OrderByDescending(x => x.Duration)
                .ToList();

            foreach (var item in groups)
            {
                double percent = item.Duration.TotalSeconds / totalSeconds * 100;
                gridCategories.Rows.Add(item.Name, item.Count, FormatDuration(item.Duration), percent.ToString("0.0") + "%");
            }
        }

        private string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
                return string.Format("{0}시간 {1}분 {2}초", (int)duration.TotalHours, duration.Minutes, duration.Seconds);
            return string.Format("{0}분 {1}초", duration.Minutes, duration.Seconds);
        }
    }
}
