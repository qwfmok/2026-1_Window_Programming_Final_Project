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
            this.txtManual.Text = "사용 설명서는 실행 시 최신 기능 기준으로 자동 생성됩니다.";

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
