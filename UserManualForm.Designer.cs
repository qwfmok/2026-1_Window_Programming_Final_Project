using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    partial class UserManualForm
    {
        private RichTextBox txtManual;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.txtManual = new RichTextBox();
            this.btnClose = new Button();
            this.SuspendLayout();

            this.Text = "사용 설명서";
            this.ClientSize = new Size(820, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Malgun Gothic", 10F);
            this.MinimumSize = new Size(640, 460);

            this.txtManual.ReadOnly = true;
            this.txtManual.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtManual.Dock = DockStyle.Fill;
            this.txtManual.BorderStyle = BorderStyle.FixedSingle;
            this.txtManual.BackColor = Color.FromArgb(250, 252, 255);
            this.txtManual.ForeColor = Color.FromArgb(30, 30, 30);
            this.txtManual.Font = new Font("Malgun Gothic", 10F);
            this.txtManual.DetectUrls = false;
            this.txtManual.Text =
                "나 뭐 했지? - PC Activity Timeline 사용 설명서\r\n\r\n" +
                "1. 프로그램 목적\r\n" +
                "- 사용자가 하루 동안 어떤 Windows 프로그램을 사용했는지 자동으로 기록합니다.\r\n" +
                "- 기록된 활동은 시간 순서대로 확인하고, 분류와 메모를 수정할 수 있습니다.\r\n\r\n" +
                "2. 기본 사용 방법\r\n" +
                "- [기록 시작] 버튼을 누르면 현재 활성 창 기록을 시작합니다.\r\n" +
                "- 다른 프로그램으로 전환하면 프로그램명과 창 제목이 자동으로 기록됩니다.\r\n" +
                "- [기록 중지] 버튼을 누르면 자동 기록을 멈춥니다.\r\n" +
                "- [저장] 버튼을 누르면 activities.json 파일에 기록을 저장합니다.\r\n" +
                "- [불러오기] 버튼을 누르면 저장된 기록을 다시 불러옵니다.\r\n\r\n" +
                "3. CSV 내보내기\r\n" +
                "- [CSV 내보내기] 버튼을 누르면 현재 조회 날짜의 활동 기록을 CSV 파일로 저장합니다.\r\n" +
                "- 저장한 CSV 파일은 Excel에서 열어 확인할 수 있습니다.\r\n\r\n" +
                "4. 기록 수정 방법\r\n" +
                "- 목록에서 기록을 클릭하면 오른쪽 입력 영역에 상세 정보가 표시됩니다.\r\n" +
                "- 분류, 시작 시간, 종료 시간, 메모를 수정한 뒤 [수정] 버튼을 누릅니다.\r\n" +
                "- [추가] 버튼으로 수동 기록을 만들 수 있습니다.\r\n" +
                "- [삭제] 버튼으로 선택한 기록을 삭제할 수 있습니다.\r\n\r\n" +
                "5. 분류별 색상 표시\r\n" +
                "- 과제, 자료검색, 수업/시험, 문서작성, 휴식 등 분류에 따라 목록 색상이 다르게 표시됩니다.\r\n" +
                "- 색상 표시를 통해 하루 활동의 성격을 빠르게 구분할 수 있습니다.\r\n\r\n" +
                "6. 상세 분석\r\n" +
                "- [상세 분석] 버튼을 누르면 현재 조회 날짜의 활동을 더 자세히 확인할 수 있습니다.\r\n" +
                "- 프로그램별 사용 시간, 분류별 사용 시간, 가장 긴 활동, 분석 문장을 제공합니다.\r\n\r\n" +
                "7. 오늘 초기화\r\n" +
                "- [오늘 초기화] 버튼을 누르면 확인 팝업이 표시됩니다.\r\n" +
                "- 예를 선택하면 조회 날짜의 기록이 모두 삭제됩니다.\r\n\r\n" +
                "8. 최소화와 종료\r\n" +
                "- 창을 최소화하면 시스템 트레이 영역으로 숨겨지고 기록은 계속 진행됩니다.\r\n" +
                "- 트레이 아이콘을 더블클릭하면 창을 다시 열 수 있습니다.\r\n" +
                "- X 버튼을 누르면 [최소화], [완전 종료], [취소] 중 하나를 선택할 수 있습니다.\r\n" +
                "- 프로그램을 끝내려면 상단 [종료] 버튼을 누르거나 X 버튼에서 [완전 종료]를 선택합니다.\r\n\r\n" +
                "9. 자동 제외 처리\r\n" +
                "- 프로그램 자기 자신, 제목 없는 창, 시스템 트레이, Windows 쉘 관련 창은 기록하지 않습니다.\r\n" +
                "- 의미 있는 사용자 작업 기록만 남기기 위한 예외 처리입니다.\r\n\r\n" +
                "10. 키워드 분류 규칙\r\n" +
                "- [분류 규칙] 버튼에서 키워드와 분류를 직접 등록할 수 있습니다.\r\n" +
                "- 창 제목이나 프로그램명에 등록한 키워드가 포함되면 해당 분류로 자동 기록됩니다.\r\n" +
                "- 예: youtube는 휴식, github는 과제, lms는 수업/시험처럼 등록할 수 있습니다.\r\n" +
                "- 규칙을 저장하면 기존 미분류 기록에도 가능한 범위에서 다시 적용됩니다.\r\n";

            this.btnClose.Text = "닫기";
            this.btnClose.Dock = DockStyle.Bottom;
            this.btnClose.Height = 46;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            this.Controls.Add(this.txtManual);
            this.Controls.Add(this.btnClose);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
