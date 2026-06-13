using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public partial class UserManualForm : Form
    {
        public UserManualForm()
        {
            InitializeComponent();
            UiTheme.ApplyForm(this);
            txtManual.Text = BuildManualText();
            StyleManualText();
            Shown += (sender, e) =>
            {
                txtManual.SelectionStart = 0;
                txtManual.SelectionLength = 0;
                btnClose.Focus();
            };
        }

        private void StyleManualText()
        {
            txtManual.SelectAll();
            txtManual.SelectionFont = new Font("Malgun Gothic", 10F, FontStyle.Regular);
            txtManual.SelectionColor = Color.FromArgb(30, 30, 30);
            txtManual.SelectionBackColor = Color.FromArgb(250, 252, 255);

            string[] headings =
            {
                "나 뭐 했지? - PC Activity Timeline 사용 설명서",
                "1. 프로그램 목적",
                "2. 기록 시작과 중지",
                "3. 저장과 불러오기",
                "4. 기록 수정과 삭제",
                "5. 조회 날짜와 전체 삭제",
                "6. 분류와 색상 표시",
                "7. 키워드 분류 규칙",
                "8. CSV 내보내기",
                "9. 상세 분석",
                "10. 최소화와 시스템 트레이",
                "11. 자동 제외 처리"
            };

            foreach (string heading in headings)
            {
                int index = txtManual.Text.IndexOf(heading);
                if (index < 0) continue;

                txtManual.Select(index, heading.Length);
                txtManual.SelectionFont = new Font("Malgun Gothic", heading.StartsWith("나 ") ? 13F : 10.5F, FontStyle.Bold);
                txtManual.SelectionColor = Color.FromArgb(28, 81, 135);
            }

            txtManual.Select(0, 0);
        }

        private string BuildManualText()
        {
            return string.Join("\r\n", new[]
            {
                "나 뭐 했지? - PC Activity Timeline 사용 설명서",
                "",
                "1. 프로그램 목적",
                "- 사용자가 하루 동안 어떤 Windows 프로그램과 창을 사용했는지 자동으로 기록합니다.",
                "- 기록된 활동을 시간순으로 확인하고, 분류와 메모를 수정하여 하루 PC 사용 패턴을 정리할 수 있습니다.",
                "",
                "2. 기록 시작과 중지",
                "- [기록 시작] 버튼을 누르면 현재 활성 창 기록을 시작합니다.",
                "- 다른 프로그램이나 웹사이트로 이동하면 프로그램명과 창 제목이 자동으로 기록됩니다.",
                "- [기록 중지] 버튼을 누르면 자동 기록을 멈춥니다.",
                "- 상태등은 기록 중일 때 초록색, 기록 중지가 되었을 때 빨간색으로 표시됩니다.",
                "",
                "3. 저장과 불러오기",
                "- [저장] 버튼을 누르면 활동 기록은 activities.json 파일로 저장됩니다.",
                "- 키워드 분류 규칙은 keyword_rules.json 파일로 함께 저장됩니다.",
                "- [불러오기] 버튼을 누르면 저장된 활동 기록을 다시 불러옵니다.",
                "- 저장 위치와 처리 결과는 조회 날짜 줄 오른쪽 안내 문구에 표시됩니다.",
                "",
                "4. 기록 수정과 삭제",
                "- 왼쪽 목록에서 기록을 클릭하면 오른쪽 입력 영역에 상세 정보가 표시됩니다.",
                "- 분류, 시작 시간, 종료 시간, 메모를 수정한 뒤 [수정] 버튼을 누르면 기록이 변경됩니다.",
                "- [추가] 버튼으로 수동 기록을 만들 수 있습니다.",
                "- [삭제] 버튼으로 선택한 기록만 삭제할 수 있습니다.",
                "- 추가/수정 시 기존 기록과 시간이 겹치면 저장되지 않으며 안내 창이 표시됩니다.",
                "",
                "5. 조회 날짜와 전체 삭제",
                "- 조회 날짜를 변경하면 해당 날짜의 활동 기록만 목록과 요약에 표시됩니다.",
                "- 조회 날짜를 바꾸면 수동 추가 입력칸의 기본 날짜도 해당 조회 날짜로 맞춰집니다.",
                "- [오늘] 버튼을 누르면 조회 날짜가 오늘로 돌아옵니다.",
                "- [기록 전체 삭제] 버튼을 누르면 현재 조회 날짜의 기록을 모두 삭제할 수 있습니다.",
                "- 삭제 전 확인 창이 표시되므로 실수로 삭제하는 것을 줄일 수 있습니다.",
                "",
                "6. 분류와 색상 표시",
                "- 과제, 자료검색, 수업/시험, 문서작성, 휴식, 기타, 미분류 같은 분류를 사용할 수 있습니다.",
                "- 기록 목록은 분류에 따라 다른 배경색으로 표시되어 하루 활동 성격을 빠르게 구분할 수 있습니다.",
                "",
                "7. 키워드 분류 규칙",
                "- [분류 규칙] 버튼에서 키워드와 분류를 직접 등록할 수 있습니다.",
                "- 프로그램명 또는 창 제목에 등록한 키워드가 포함되면 해당 분류로 자동 기록됩니다.",
                "- 예: youtube -> 휴식, github -> 과제, lms -> 수업/시험처럼 등록할 수 있습니다.",
                "- 규칙 저장 시 기존 미분류 기록에도 가능한 범위에서 다시 적용되어 미분류를 줄일 수 있습니다.",
                "",
                "8. CSV 내보내기",
                "- [CSV 내보내기] 버튼을 누르면 현재 조회 날짜의 활동 기록을 CSV 파일로 저장합니다.",
                "- 저장한 CSV 파일은 Excel에서 열어 확인할 수 있습니다.",
                "",
                "9. 상세 분석",
                "- [상세 분석] 버튼을 누르면 현재 조회 날짜의 활동을 자세히 확인할 수 있습니다.",
                "- 총 기록 수, 총 기록 시간, 가장 오래 사용한 프로그램, 가장 많은 분류, 프로그램별/분류별 사용 시간이 표시됩니다.",
                "- 분류 비율 파이차트와 오늘의 피드백 문구를 통해 하루 활동 패턴을 한눈에 확인할 수 있습니다.",
                "",
                "10. 최소화와 시스템 트레이",
                "- 창을 최소화하면 작업표시줄에서 사라지고 시스템 트레이로 들어갑니다.",
                "- 시스템 트레이 아이콘을 더블클릭하면 창을 다시 열 수 있습니다.",
                "- 트레이 아이콘 우클릭 메뉴에서 열기, 기록 시작, 기록 중지, 완전 종료를 사용할 수 있습니다.",
                "- X 버튼을 누르면 [최소화], [완전 종료], [취소] 중 하나를 선택할 수 있습니다.",
                "",
                "11. 자동 제외 처리",
                "- 프로그램 자기 자신, 제목 없는 창, 시스템 트레이, Windows 쉘 관련 창은 기록하지 않습니다.",
                "- 의미 있는 사용자 작업 기록만 남기기 위한 예외 처리입니다."
            });
        }
    }
}
