using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    partial class AnalysisForm
    {
        private TableLayoutPanel mainPanel;
        private RichTextBox txtAnalysis;
        private DataGridView gridPrograms;
        private DataGridView gridCategories;
        private Label lblPrograms;
        private Label lblCategories;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.mainPanel = new TableLayoutPanel();
            this.txtAnalysis = new RichTextBox();
            this.gridPrograms = new DataGridView();
            this.gridCategories = new DataGridView();
            this.lblPrograms = new Label();
            this.lblCategories = new Label();
            this.btnClose = new Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrograms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCategories)).BeginInit();
            this.SuspendLayout();

            this.ClientSize = new Size(860, 620);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Malgun Gothic", 9F);
            this.MinimumSize = new Size(760, 520);

            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(10);
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.RowCount = 4;
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

            this.txtAnalysis.Dock = DockStyle.Fill;
            this.txtAnalysis.ReadOnly = true;
            this.txtAnalysis.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtAnalysis.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnalysis.BackColor = Color.FromArgb(250, 252, 255);
            this.txtAnalysis.ForeColor = Color.FromArgb(30, 30, 30);
            this.txtAnalysis.Font = new Font("Malgun Gothic", 9.5F);
            this.txtAnalysis.DetectUrls = false;
            this.mainPanel.Controls.Add(this.txtAnalysis, 0, 0);
            this.mainPanel.SetColumnSpan(this.txtAnalysis, 2);

            this.lblPrograms.Text = "프로그램별 사용 시간";
            this.lblPrograms.Dock = DockStyle.Fill;
            this.lblPrograms.TextAlign = ContentAlignment.MiddleLeft;
            this.lblPrograms.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            this.mainPanel.Controls.Add(this.lblPrograms, 0, 1);

            this.lblCategories.Text = "분류별 사용 시간";
            this.lblCategories.Dock = DockStyle.Fill;
            this.lblCategories.TextAlign = ContentAlignment.MiddleLeft;
            this.lblCategories.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            this.mainPanel.Controls.Add(this.lblCategories, 1, 1);

            ConfigureGrid(this.gridPrograms);
            this.gridPrograms.Columns.Add("Program", "프로그램");
            this.gridPrograms.Columns.Add("Count", "기록 수");
            this.gridPrograms.Columns.Add("Duration", "사용 시간");
            this.gridPrograms.Columns.Add("Percent", "비율");
            this.mainPanel.Controls.Add(this.gridPrograms, 0, 2);

            ConfigureGrid(this.gridCategories);
            this.gridCategories.Columns.Add("Category", "분류");
            this.gridCategories.Columns.Add("Count", "기록 수");
            this.gridCategories.Columns.Add("Duration", "사용 시간");
            this.gridCategories.Columns.Add("Percent", "비율");
            this.mainPanel.Controls.Add(this.gridCategories, 1, 2);

            this.btnClose.Text = "닫기";
            this.btnClose.Width = 100;
            this.btnClose.Height = 32;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.mainPanel.Controls.Add(this.btnClose, 1, 3);

            this.Controls.Add(this.mainPanel);

            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPrograms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCategories)).EndInit();
            this.ResumeLayout(false);
        }

        private void ConfigureGrid(DataGridView grid)
        {
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = Color.White;
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
