using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCActivityTimeline
{
    public partial class AnalysisForm : Form
    {
        private readonly DateTime targetDate;
        private readonly List<ActivityRecord> records;
        private static readonly Random random = new Random();

        public AnalysisForm(DateTime targetDate, List<ActivityRecord> records)
        {
            this.targetDate = targetDate;
            this.records = records ?? new List<ActivityRecord>();
            InitializeComponent();
            UiTheme.ApplyForm(this);
            ApplyModernLayout();
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

        private void ApplyModernLayout()
        {
            UiTheme.ApplyBackgroundCanvas(mainPanel);

            txtAnalysis.BackColor = UiTheme.CardBlue;
            txtInsight.BackColor = UiTheme.CardGreen;
            grpChart.BackColor = UiTheme.CardBlue;
            grpCategories.BackColor = UiTheme.CardBlue;
            grpPrograms.BackColor = UiTheme.CardBlue;
        }

        private void LoadAnalysis()
        {
            Text = targetDate.ToString("yyyy-MM-dd") + " 활동 상세 분석";
            txtAnalysis.Text = BuildAnalysisText();
            txtInsight.Text = BuildInsightText();
            StyleAnalysisText();
            StyleInsightText();
            LoadCategoryChart();
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
            string topName = topCategory == null ? "" : topCategory.Name;
            double topPercent = topCategory == null ? 0 : topCategory.Duration.TotalSeconds / Math.Max(1, total.TotalSeconds) * 100;
            lines.Add(MakeAdvice(topName, topPercent));
            lines.Add(PickMotivationMessage(topName, topPercent, total, activityCount, categoryDurations));
            lines.Add(PickPraiseMessage(topName, topPercent, total, categoryDurations));
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
            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double topPercent = GetCategoryPercent(categoryDurations, topCategory, totalSeconds);
            messages.Add(MakeAdvice(topCategory, topPercent));

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

        private string PickMotivationMessage(string topCategory, double topPercent, TimeSpan total, int activityCount, Dictionary<string, TimeSpan> categoryDurations)
        {
            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double unclassifiedPercent = GetCategoryPercent(categoryDurations, "미분류", totalSeconds);
            double restPercent = GetCategoryPercent(categoryDurations, "휴식", totalSeconds);

            var messages = new List<string>();
            if (topCategory == "과제")
            {
                messages.Add("동기부여: 과제 흐름이 잡혀 있어요. 마무리 전 핵심 작업을 메모로 남겨두면 보고서 작성이 쉬워집니다.");
                messages.Add("동기부여: 작업 시간이 충분합니다. 다음에는 짧은 휴식을 끼워 넣으면 집중을 더 오래 유지할 수 있어요.");
            }
            else if (topCategory == "수업/시험")
            {
                messages.Add("동기부여: 시험/수업 시간이 가장 큽니다. 복습할 부분을 메모에 적어두면 학습 기록의 가치가 커집니다.");
                messages.Add("동기부여: 학습 중심의 흐름입니다. 시험 뒤에는 틀린 개념이나 헷갈린 키워드를 따로 정리해보세요.");
            }
            else if (topCategory == "휴식")
            {
                messages.Add("동기부여: 휴식 비중이 높습니다. 쉬는 시간을 인정하되, 다음 기록에는 과제나 정리 시간을 한 블록 추가해보세요.");
                messages.Add("동기부여: 휴식도 필요한 활동입니다. 다만 목표 시간이 있다면 짧은 작업 타이머를 하나 더 만들어보면 좋아요.");
            }
            else if (topCategory == "자료검색")
            {
                messages.Add("동기부여: 자료를 찾는 시간이 많습니다. 검색한 내용을 메모로 요약하면 탐색이 실제 산출물로 이어집니다.");
                messages.Add("동기부여: 정보 수집 흐름이 보입니다. 다음 단계는 찾은 자료를 과제나 문서작성으로 연결하는 것입니다.");
            }
            else if (topCategory == "문서작성")
            {
                messages.Add("동기부여: 문서작성 시간이 잘 기록됐습니다. 작성한 내용의 제목이나 진행률을 메모로 남기면 추적하기 좋아요.");
                messages.Add("동기부여: 정리와 작성에 시간을 썼습니다. 다음에는 자료검색/과제 기록과 연결해 흐름을 더 선명하게 만들어보세요.");
            }
            else if (topCategory == "기타")
            {
                messages.Add("동기부여: 기타 활동이 많습니다. 자주 반복되는 프로그램은 키워드 규칙으로 분류를 세분화해보세요.");
                messages.Add("동기부여: 아직 성격이 애매한 활동이 있습니다. 기록을 보며 나만의 분류 이름을 추가하면 분석이 더 좋아집니다.");
            }
            else if (topCategory == "미분류")
            {
                messages.Add("개선 힌트: 미분류가 가장 많습니다. 분류 규칙에 프로그램명이나 사이트 키워드를 추가해보세요.");
                messages.Add("개선 힌트: 미분류 기록을 몇 개만 수정해도 다음 분석부터 훨씬 정확한 피드백이 나옵니다.");
            }

            if (topPercent >= 70 && topCategory != "휴식" && topCategory != "미분류")
                messages.Add("동기부여: 한 분류에 집중된 날입니다. 이 흐름을 유지하되 짧은 정리 시간을 마지막에 붙이면 더 좋습니다.");
            if (restPercent >= 45 && topCategory != "휴식")
                messages.Add("동기부여: 휴식도 꽤 포함되어 있습니다. 작업과 쉼의 균형을 유지하면 지속하기 쉬워요.");
            if (unclassifiedPercent >= 25 && topCategory != "미분류")
                messages.Add("개선 힌트: 미분류 비율이 조금 높습니다. 자주 보이는 창 제목을 규칙에 추가해보세요.");
            if (activityCount >= 20)
                messages.Add("동기부여: 기록이 촘촘합니다. 중요한 활동에는 메모를 붙여 나중에 바로 이해되게 해보세요.");
            if (total.TotalMinutes < 10)
                messages.Add("동기부여: 아직 표본이 작습니다. 조금 더 기록하면 오늘의 패턴이 더 선명해집니다.");

            return PickByDate(messages, 1);
        }

        private string PickPraiseMessage(string topCategory, double topPercent, TimeSpan total, Dictionary<string, TimeSpan> categoryDurations)
        {
            double totalSeconds = Math.Max(1, total.TotalSeconds);
            double workPercent = GetCategoryPercent(categoryDurations, "과제", totalSeconds)
                + GetCategoryPercent(categoryDurations, "문서작성", totalSeconds)
                + GetCategoryPercent(categoryDurations, "수업/시험", totalSeconds);
            double searchPercent = GetCategoryPercent(categoryDurations, "자료검색", totalSeconds);

            var messages = new List<string>();
            if (topCategory == "과제")
            {
                messages.Add("칭찬: 과제 시간이 가장 크게 기록됐습니다. 목표를 향해 실제로 움직인 흔적이 분명합니다.");
                messages.Add("칭찬: 과제에 시간을 쓴 건 오늘 해야 할 일을 외면하지 않았다는 뜻입니다.");
                messages.Add("칭찬: 작업 중심으로 시간을 보냈습니다. 결과물을 만들기 위한 좋은 흐름입니다.");
            }
            else if (topCategory == "수업/시험")
            {
                messages.Add("칭찬: 수업/시험 준비 시간이 잘 확보됐습니다. 학습 우선순위가 분명한 하루입니다.");
                messages.Add("칭찬: 시험과 수업에 시간을 투자했습니다. 점수로 이어질 수 있는 의미 있는 기록입니다.");
                messages.Add("칭찬: 학습 시간이 가장 크게 남았습니다. 바쁜 일정 속에서도 공부 흐름을 지킨 점이 좋습니다.");
            }
            else if (topCategory == "휴식")
            {
                messages.Add("칭찬: 휴식도 기록했다는 점이 좋습니다. 쉬는 시간까지 보이면 하루 균형을 조절하기 쉬워집니다.");
                messages.Add("칭찬: 쉬는 시간을 숨기지 않고 기록한 점이 좋습니다. 패턴을 알아야 조절도 가능합니다.");
                messages.Add("칭찬: 휴식을 기록으로 남겼습니다. 회복 시간까지 관리하려는 태도가 보입니다.");
            }
            else if (topCategory == "자료검색")
            {
                messages.Add("칭찬: 자료검색 흐름이 잘 남아 있습니다. 무엇을 찾아봤는지 돌아볼 수 있는 좋은 기록입니다.");
                messages.Add("칭찬: 필요한 정보를 찾기 위해 시간을 썼습니다. 준비 과정이 잘 드러납니다.");
                messages.Add("칭찬: 탐색 과정이 기록됐습니다. 이후 과제나 문서작성으로 이어가기 좋은 기반입니다.");
            }
            else if (topCategory == "문서작성")
            {
                messages.Add("칭찬: 문서작성 시간이 확인됩니다. 생각을 결과물로 옮긴 시간이 잘 기록됐습니다.");
                messages.Add("칭찬: 문서로 정리하는 시간이 남았습니다. 단순 사용이 아니라 산출물을 만든 기록입니다.");
                messages.Add("칭찬: 작성 활동이 뚜렷합니다. 보고서나 정리 자료를 완성해가는 흐름이 보입니다.");
            }
            else if (topCategory == "기타")
            {
                messages.Add("칭찬: 기타 활동까지 놓치지 않고 기록했습니다. 이제 분류만 다듬으면 분석 품질이 더 올라갑니다.");
                messages.Add("칭찬: 애매한 활동도 데이터로 남겼습니다. 기록이 있어야 나중에 분류를 개선할 수 있습니다.");
                messages.Add("칭찬: 다양한 활동이 빠지지 않고 기록됐습니다. 사용 패턴을 넓게 볼 수 있습니다.");
            }
            else if (topCategory == "미분류")
            {
                messages.Add("칭찬: 아직 미분류가 많아도 기록 자체는 남아 있습니다. 분류를 붙이면 바로 의미 있는 데이터가 됩니다.");
                messages.Add("칭찬: 분류가 완벽하지 않아도 괜찮습니다. 기록을 남긴 것만으로도 개선의 출발점입니다.");
                messages.Add("칭찬: 미분류 기록도 중요한 단서입니다. 나중에 규칙을 추가하면 더 똑똑한 분석이 됩니다.");
            }

            if (workPercent >= 50)
                messages.Add("칭찬: 작업과 학습 비중이 높습니다. 생산적인 PC 사용 흐름이 잘 보입니다.");
            if (searchPercent >= 25 && topCategory != "자료검색")
                messages.Add("칭찬: 자료를 찾은 흐름도 함께 남아 있어 이후 작업을 되짚기 좋습니다.");
            if (topPercent >= 70)
                messages.Add("칭찬: 한 가지 목표에 집중한 날입니다. 집중도가 높은 사용 패턴이 보입니다.");

            messages.Add("칭찬: 기록을 남긴 것 자체가 좋은 습관입니다.");
            messages.Add("칭찬: 오늘 기록은 나중에 돌아보기 좋은 데이터예요.");
            messages.Add("칭찬: 작은 기록이 생활 패턴을 바꾸는 근거가 됩니다.");

            return PickRandom(messages);
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
                "시간을 아는 것이 변화의 시작입니다.",
                "오늘의 고통은 내일의 힘이 된다.",
                "재능은 출발선일 뿐, 꾸준함이 결승선을 만든다.",
                "남들보다 똑똑할 필요는 없다. 남들보다 오래 버티면 된다.",
                "미래의 나는 지금의 나를 기억하지 못하지만, 지금의 선택은 반드시 결과로 남는다.",
                "성공은 특별한 사람이 하는 것이 아니라, 포기하지 않은 사람이 하는 것이다.",
                "공부는 단기간의 고통으로 평생의 선택지를 늘리는 일이다.",
                "동기부여는 시작하게 만들고, 습관은 끝까지 가게 만든다.",
                "어제의 나보다 1%만 나아져도 충분하다.",
                "지금 놀면 즐거운 하루를 얻고, 지금 공부하면 원하는 미래를 얻는다.",
                "결과를 바꾸고 싶다면 행동을 바꿔야 한다.",
                "할 수 있을 때 하는 사람이 아니라, 하기 싫을 때도 하는 사람이 성장한다.",
                "남들과 비교하지 말고 어제의 나와 비교하라.",
                "포기하고 싶은 순간이 가장 성장하는 순간이다.",
                "노력은 배신할 수 있다. 하지만 노력하지 않으면 성공할 가능성조차 없다.",
                "언젠가 해야 할 일이라면, 오늘 하는 것이 가장 빠르다."
            };

            return PickRandom(quotes);
        }

        private string PickByDate(List<string> values, int salt)
        {
            if (values == null || values.Count == 0) return "";
            int index = Math.Abs(targetDate.DayOfYear + records.Count + salt) % values.Count;
            return values[index];
        }

        private string PickRandom(List<string> values)
        {
            if (values == null || values.Count == 0) return "";
            lock (random)
            {
                return values[random.Next(values.Count)];
            }
        }

        private double GetCategoryPercent(Dictionary<string, TimeSpan> categoryDurations, string category, double totalSeconds)
        {
            if (categoryDurations == null || !categoryDurations.ContainsKey(category)) return 0;
            return categoryDurations[category].TotalSeconds / totalSeconds * 100;
        }

        private string MakeAdvice(string topCategory, double topPercent)
        {
            if (topCategory == "과제")
                return "분석: 과제 비중이 " + topPercent.ToString("0") + "%입니다. 작업 중심의 하루입니다.";
            if (topCategory == "자료검색")
                return "분석: 자료검색 비중이 " + topPercent.ToString("0") + "%입니다. 탐색한 내용을 정리로 이어가면 좋습니다.";
            if (topCategory == "수업/시험")
                return "분석: 수업/시험 비중이 " + topPercent.ToString("0") + "%입니다. 학습과 시험 준비에 집중한 날입니다.";
            if (topCategory == "문서작성")
                return "분석: 문서작성 비중이 " + topPercent.ToString("0") + "%입니다. 결과물을 만드는 시간이 잘 드러납니다.";
            if (topCategory == "휴식")
                return "분석: 휴식 비중이 " + topPercent.ToString("0") + "%입니다. 쉬는 시간과 작업 시간의 균형을 점검해보세요.";
            if (topCategory == "기타")
                return "분석: 기타 비중이 " + topPercent.ToString("0") + "%입니다. 반복되는 활동은 별도 분류로 나누면 더 정확해집니다.";
            if (topCategory == "미분류")
                return "분석: 미분류 비중이 " + topPercent.ToString("0") + "%입니다. 규칙을 추가하면 더 정확해집니다.";
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

        private void LoadCategoryChart()
        {
            chartCategories.Series.Clear();
            chartCategories.ChartAreas.Clear();
            chartCategories.Legends.Clear();
            chartCategories.Titles.Clear();

            var area = new ChartArea("CategoryArea");
            area.BackColor = System.Drawing.Color.White;
            chartCategories.ChartAreas.Add(area);

            var legend = new Legend("CategoryLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = System.Drawing.StringAlignment.Center;
            chartCategories.Legends.Add(legend);

            var series = new Series("분류 비율");
            series.ChartType = SeriesChartType.Pie;
            series.ChartArea = "CategoryArea";
            series.Legend = "CategoryLegend";
            series.Font = new System.Drawing.Font("Malgun Gothic", 8F);
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Gray";
            series.Label = "#PERCENT{P0}";
            series.LegendText = "#VALX";
            chartCategories.Series.Add(series);

            var totalSeconds = Math.Max(1, records.Sum(r => r.Duration.TotalSeconds));
            var groups = records
                .GroupBy(r => string.IsNullOrWhiteSpace(r.Category) ? "미분류" : r.Category)
                .Select(g => new
                {
                    Name = g.Key,
                    Seconds = g.Sum(r => r.Duration.TotalSeconds)
                })
                .Where(x => x.Seconds > 0)
                .OrderByDescending(x => x.Seconds)
                .ToList();

            if (groups.Count == 0)
            {
                chartCategories.Titles.Add("표시할 기록이 없습니다.");
                return;
            }

            foreach (var item in groups)
            {
                int pointIndex = series.Points.AddY(item.Seconds);
                var point = series.Points[pointIndex];
                point.AxisLabel = item.Name;
                point.LegendText = item.Name;
                point.ToolTip = item.Name + " " + (item.Seconds / totalSeconds * 100).ToString("0.0") + "%";
                point.Color = UiTheme.GetCategoryColor(item.Name);
            }

            UiTheme.StyleChart(chartCategories);
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
