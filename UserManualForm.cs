using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public partial class UserManualForm : Form
    {
        public UserManualForm()
        {
            InitializeComponent();
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
                "2. 기본 사용 방법",
                "3. CSV 내보내기",
                "4. 기록 수정 방법",
                "5. 분류별 색상 표시",
                "6. 상세 분석",
                "7. 오늘 초기화",
                "8. 최소화와 종료",
                "9. 자동 제외 처리",
                "10. 키워드 분류 규칙"
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
    }
}
