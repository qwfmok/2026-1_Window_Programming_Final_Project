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
                txtInsight.SelectionStart = 0;
                txtInsight.SelectionLength = 0;
                gridPrograms.ClearSelection();
                gridCategories.ClearSelection();
                btnClose.Focus();
            };
        }

        private void LoadAnalysis()
        {
            Text = targetDate.ToString("yyyy-MM-dd") + " 활동 상세 분석";
            txtAnalysis.Text = BuildAnalysisText();
            txtInsight.Text = BuildInsightText();
            StyleAnalysisText();
            StyleInsightText();
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

            txtAnalysis.Select(0, 0);
        }

        private void StyleInsightText()
        {
            txtInsight.SelectAll();
            txtInsight.SelectionFont = new System.Drawing.Font("Malgun Gothic", 9.5F, System.Drawing.FontStyle.Regular);
            txtInsight.SelectionColor = System.Drawing.Color.FromArgb(35, 75, 55);
            txtInsight.SelectionBackColor = System.Drawing.Color.FromArgb(252, 255, 250);

            int firstLineLength = txtInsight.Text.IndexOf(Environment.NewLine);
            if (firstLineLength < 0) firstLineLength = txtInsight.Text.Length;
            if (firstLineLength > 0)
            {
                txtInsight.Select(0, firstLineLength);
                txtInsight.SelectionFont = new System.Drawing.Font("Malgun Gothic", 13F, System.Drawing.FontStyle.Bold);
                txtInsight.SelectionColor = System.Drawing.Color.FromArgb(35, 100, 70);
            }

            int quoteIndex = txtInsight.Text.IndexOf("오늘의 명언:");
            if (quoteIndex >= 0)
            {
                txtInsight.Select(quoteIndex, txtInsight.Text.Length - quoteIndex);
                txtInsight.SelectionFont = new System.Drawing.Font("Malgun Gothic", 9.3F, System.Drawing.FontStyle.Italic);
                txtInsight.SelectionColor = System.Drawing.Color.FromArgb(95, 95, 95);
            }

            txtInsight.Select(0, 0);
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
            lines.Add("핵심 요약");
            lines.Add("총 기록 수: " + activityCount + "개");
            lines.Add("총 기록 시간: " + FormatDuration(total));

            if (topProgram != null)
                lines.Add("가장 오래 사용한 프로그램: " + topProgram.Name + " (" + FormatDuration(topProgram.Duration) + ")");
            if (topCategory != null)
                lines.Add("가장 많은 분류: " + topCategory.Name + " (" + FormatDuration(topCategory.Duration) + ")");
            if (longest != null)
                lines.Add("가장 긴 단일 활동: " + longest.ProgramName + " / " + FormatDuration(longest.Duration));

            return string.Join(Environment.NewLine, lines);
        }

        private string BuildInsightText()
        {
            if (records.Count == 0)
                return "오늘의 피드백\r\n기록이 쌓이면 여기에 맞춤형 피드백과 명언이 표시됩니다.";

            TimeSpan total = TimeSpan.FromTicks(records.Sum(r => r.Duration.Ticks));
            int activityCount = records.Count;
            var topCategory = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .Select(g => new { Name = g.Key, Duration = TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)) })
                .OrderByDescending(x => x.Duration)
                .FirstOrDefault();
            var categoryDurations = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .ToDictionary(g => g.Key, g => TimeSpan.FromTicks(g.Sum(r => r.Duration.Ticks)));

            var lines = new List<string>();
            lines.Add("오늘의 피드백");
            lines.Add(MakeAdvice(topCategory == null ? "" : topCategory.Name));
            lines.Add(PickMotivationMessage(total, activityCount, categoryDurations));
            lines.Add(PickPraiseMessage(total, categoryDurations));
            lines.Add("");
            lines.Add("오늘의 명언: " + PickQuote());
            return string.Join(Environment.NewLine, lines);
        }

        private List<string> MakeInsightMessages(
            string topCategory,
            TimeSpan total,
            int activityCount,
            Dictionary<string, TimeSpan> categoryDurations)
        {
            var messages = new List<string>();
            messages.Add(MakeAdvice(topCategory));

            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double unclassifiedPercent = GetCategoryPercent(categoryDurations, "미분류", totalSeconds);
            double workPercent = GetCategoryPercent(categoryDurations, "과제", totalSeconds)
                + GetCategoryPercent(categoryDurations, "문서작성", totalSeconds)
                + GetCategoryPercent(categoryDurations, "수업/시험", totalSeconds);
            double restPercent = GetCategoryPercent(categoryDurations, "휴식", totalSeconds);

            if (workPercent >= 55)
                messages.Add("칭찬: 작업과 학습 관련 시간이 높습니다. 오늘은 꽤 집중해서 PC를 사용한 편입니다.");
            else if (restPercent >= 45)
                messages.Add("동기부여: 휴식 시간이 눈에 띕니다. 다음 기록에서는 과제나 정리 시간을 조금만 더 늘려보면 좋습니다.");
            else
                messages.Add("칭찬: 활동이 한쪽으로만 치우치지 않았습니다. 기록을 보면서 사용 패턴을 조절하기 좋은 상태입니다.");

            if (unclassifiedPercent >= 25)
                messages.Add("개선 힌트: 미분류 비율이 높습니다. 키워드 분류 규칙을 추가하면 다음 분석이 더 정확해집니다.");
            else
                messages.Add("칭찬: 분류가 비교적 잘 되어 있습니다. 하루 활동을 한눈에 파악하기 좋은 기록입니다.");

            if (activityCount >= 20)
                messages.Add("동기부여: 기록 수가 많다는 건 여러 활동을 세밀하게 추적했다는 뜻입니다. 이제 중요한 활동만 메모로 보강해보세요.");
            else if (total.TotalMinutes >= 30)
                messages.Add("동기부여: 기록 시간이 충분히 쌓이고 있습니다. 이 흐름을 유지하면 하루 사용 습관을 더 선명하게 볼 수 있습니다.");
            else
                messages.Add("동기부여: 아직 기록 시간이 짧습니다. 조금 더 사용한 뒤 다시 분석하면 더 의미 있는 결과가 나옵니다.");

            return messages;
        }

        private string PickMotivationMessage(TimeSpan total, int activityCount, Dictionary<string, TimeSpan> categoryDurations)
        {
            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double unclassifiedPercent = GetCategoryPercent(categoryDurations, "미분류", totalSeconds);
            double restPercent = GetCategoryPercent(categoryDurations, "휴식", totalSeconds);

            var messages = new List<string>();
            if (unclassifiedPercent >= 25)
                messages.Add("개선 힌트: 키워드 규칙을 추가하면 미분류를 줄일 수 있어요.");
            if (restPercent >= 45)
                messages.Add("동기부여: 다음엔 과제 시간을 조금만 더 확보해봐요.");
            if (activityCount >= 20)
                messages.Add("동기부여: 기록이 촘촘해요. 중요한 활동엔 메모를 붙여봐요.");
            if (total.TotalMinutes < 10)
                messages.Add("동기부여: 조금 더 기록하면 흐름이 더 선명해질 거예요.");

            if (messages.Count == 0)
                messages.Add("동기부여: 기록이 쌓이면 사용 습관이 더 잘 보입니다.");

            return PickByDate(messages, 1);
        }

        private string PickPraiseMessage(TimeSpan total, Dictionary<string, TimeSpan> categoryDurations)
        {
            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double workPercent = GetCategoryPercent(categoryDurations, "과제", totalSeconds)
                + GetCategoryPercent(categoryDurations, "문서작성", totalSeconds)
                + GetCategoryPercent(categoryDurations, "수업/시험", totalSeconds);
            double searchPercent = GetCategoryPercent(categoryDurations, "자료검색", totalSeconds);

            var messages = new List<string>();
            if (workPercent >= 50)
                messages.Add("칭찬: 작업과 학습 비중이 좋아요.");
            if (searchPercent >= 25)
                messages.Add("칭찬: 자료를 찾은 흐름이 잘 남아 있어요.");

            messages.Add("칭찬: 기록을 남긴 것 자체가 좋은 습관입니다.");
            messages.Add("칭찬: 오늘 기록은 나중에 돌아보기 좋은 데이터예요.");
            messages.Add("칭찬: 작은 기록이 생활 패턴을 바꾸는 근거가 됩니다.");

            return PickByDate(messages, 2);
        }

        private string PickQuote()
        {
            var quotes = new List<string>
            {
                "측정되는 것은 개선될 수 있습니다.",
                "작은 기록이 내일의 선택을 바꿉니다.",
                "집중은 환경과 습관에서 만들어집니다.",
                "시간을 보는 사람은 시간을 바꿀 수 있습니다.",
                "완벽한 하루보다 기록된 하루가 더 많이 가르쳐줍니다.",
                "작은 개선이 큰 변화를 만듭니다.",
                "시간을 아는 것이 변화의 시작입니다."
            };

            return PickByDate(quotes, 3);
        }

        private string PickByDate(List<string> values, int salt)
        {
            if (values == null || values.Count == 0) return "";
            int index = Math.Abs(targetDate.DayOfYear + records.Count + salt) % values.Count;
            return values[index];
        }

        private double GetCategoryPercent(Dictionary<string, TimeSpan> categoryDurations, string category, double totalSeconds)
        {
            if (categoryDurations == null || !categoryDurations.ContainsKey(category)) return 0;
            return categoryDurations[category].TotalSeconds / totalSeconds * 100;
        }

        private string MakeAdvice(string topCategory)
        {
            if (topCategory == "과제")
                return "분석: 과제 비중이 높아요. 작업 중심의 하루입니다.";
            if (topCategory == "자료검색")
                return "분석: 자료검색이 많아요. 이제 정리로 이어가면 좋습니다.";
            if (topCategory == "휴식")
                return "분석: 휴식 시간이 높아요. 균형을 한번 점검해봐요.";
            if (topCategory == "미분류")
                return "분석: 미분류가 많아요. 규칙을 추가하면 더 정확해집니다.";
            return "분석: 분류별 시간으로 오늘의 패턴을 확인할 수 있습니다.";
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
