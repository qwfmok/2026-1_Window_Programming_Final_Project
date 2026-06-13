using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCActivityTimeline
{
    partial class AnalysisForm
    {
        private TableLayoutPanel mainPanel;
        private RichTextBox txtAnalysis;
        private RichTextBox txtInsight;
        private Chart chartCategories;
        private DataGridView gridPrograms;
        private DataGridView gridCategories;
        private GroupBox grpChart;
        private GroupBox grpCategories;
        private GroupBox grpPrograms;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.mainPanel = new TableLayoutPanel();
            this.txtAnalysis = new RichTextBox();
            this.txtInsight = new RichTextBox();
            this.chartCategories = new Chart();
            this.gridPrograms = new DataGridView();
            this.gridCategories = new DataGridView();
            this.grpChart = new GroupBox();
            this.grpCategories = new GroupBox();
            this.grpPrograms = new GroupBox();
            this.btnClose = new Button();
            this.mainPanel.SuspendLayout();
            this.grpChart.SuspendLayout();
            this.grpCategories.SuspendLayout();
            this.grpPrograms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrograms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCategories)).BeginInit();
            this.SuspendLayout();

            this.ClientSize = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Malgun Gothic", 9F);
            this.MinimumSize = new Size(1040, 680);

            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(14);
            this.mainPanel.ColumnCount = 3;
            this.mainPanel.RowCount = 3;
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 230F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

            this.txtAnalysis.Dock = DockStyle.Fill;
            this.txtAnalysis.Margin = new Padding(4, 4, 6, 10);
            this.txtAnalysis.ReadOnly = true;
            this.txtAnalysis.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtAnalysis.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnalysis.BackColor = Color.FromArgb(250, 252, 255);
            this.txtAnalysis.ForeColor = Color.FromArgb(30, 30, 30);
            this.txtAnalysis.Font = new Font("Malgun Gothic", 9.5F);
            this.txtAnalysis.DetectUrls = false;
            this.mainPanel.Controls.Add(this.txtAnalysis, 0, 0);

            this.txtInsight.Dock = DockStyle.Fill;
            this.txtInsight.Margin = new Padding(6, 4, 4, 10);
            this.txtInsight.ReadOnly = true;
            this.txtInsight.ScrollBars = RichTextBoxScrollBars.None;
            this.txtInsight.BorderStyle = BorderStyle.FixedSingle;
            this.txtInsight.BackColor = Color.FromArgb(252, 255, 250);
            this.txtInsight.ForeColor = Color.FromArgb(30, 30, 30);
            this.txtInsight.Font = new Font("Malgun Gothic", 9.5F);
            this.txtInsight.DetectUrls = false;
            this.mainPanel.Controls.Add(this.txtInsight, 1, 0);
            this.mainPanel.SetColumnSpan(this.txtInsight, 2);

            this.grpChart.Text = "분류 비율 차트";
            this.grpChart.Dock = DockStyle.Fill;
            this.grpChart.Margin = new Padding(4, 0, 6, 8);
            this.grpChart.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            this.grpChart.Padding = new Padding(8);
            this.chartCategories.Dock = DockStyle.Fill;
            this.chartCategories.BackColor = Color.White;
            this.grpChart.Controls.Add(this.chartCategories);
            this.mainPanel.Controls.Add(this.grpChart, 0, 1);

            this.gridPrograms.Dock = DockStyle.Fill;
            this.gridPrograms.ReadOnly = true;
            this.gridPrograms.AllowUserToAddRows = false;
            this.gridPrograms.AllowUserToDeleteRows = false;
            this.gridPrograms.AllowUserToResizeRows = false;
            this.gridPrograms.AllowUserToResizeColumns = false;
            this.gridPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridPrograms.MultiSelect = false;
            this.gridPrograms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPrograms.RowHeadersVisible = false;
            this.gridPrograms.RowTemplate.Height = 24;
            this.gridPrograms.BackgroundColor = Color.White;
            this.gridPrograms.Columns.Add("Program", "프로그램");
            this.gridPrograms.Columns.Add("Count", "기록 수");
            this.gridPrograms.Columns.Add("Duration", "사용 시간");
            this.gridPrograms.Columns.Add("Percent", "비율");
            this.grpPrograms.Text = "프로그램별 사용 시간";
            this.grpPrograms.Dock = DockStyle.Fill;
            this.grpPrograms.Margin = new Padding(6, 0, 4, 8);
            this.grpPrograms.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            this.grpPrograms.Padding = new Padding(8);
            this.grpPrograms.Controls.Add(this.gridPrograms);
            this.mainPanel.Controls.Add(this.grpPrograms, 2, 1);

            this.gridCategories.Dock = DockStyle.Fill;
            this.gridCategories.ReadOnly = true;
            this.gridCategories.AllowUserToAddRows = false;
            this.gridCategories.AllowUserToDeleteRows = false;
            this.gridCategories.AllowUserToResizeRows = false;
            this.gridCategories.AllowUserToResizeColumns = false;
            this.gridCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridCategories.MultiSelect = false;
            this.gridCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCategories.RowHeadersVisible = false;
            this.gridCategories.RowTemplate.Height = 24;
            this.gridCategories.BackgroundColor = Color.White;
            this.gridCategories.Columns.Add("Category", "분류");
            this.gridCategories.Columns.Add("Count", "기록 수");
            this.gridCategories.Columns.Add("Duration", "사용 시간");
            this.gridCategories.Columns.Add("Percent", "비율");
            this.grpCategories.Text = "분류별 사용 시간";
            this.grpCategories.Dock = DockStyle.Fill;
            this.grpCategories.Margin = new Padding(6, 0, 6, 8);
            this.grpCategories.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            this.grpCategories.Padding = new Padding(8);
            this.grpCategories.Controls.Add(this.gridCategories);
            this.mainPanel.Controls.Add(this.grpCategories, 1, 1);

            this.btnClose.Text = "닫기";
            this.btnClose.Width = 100;
            this.btnClose.Height = 32;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.mainPanel.Controls.Add(this.btnClose, 2, 2);

            this.Controls.Add(this.mainPanel);

            this.mainPanel.ResumeLayout(false);
            this.grpChart.ResumeLayout(false);
            this.grpCategories.ResumeLayout(false);
            this.grpPrograms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrograms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCategories)).EndInit();
            this.ResumeLayout(false);
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
