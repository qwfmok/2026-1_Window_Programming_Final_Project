using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    partial class KeywordRuleForm
    {
        private TableLayoutPanel mainPanel;
        private DataGridView gridRules;
        private Label lblHelp;
        private Label lblKeyword;
        private Label lblCategory;
        private TextBox txtKeyword;
        private ComboBox cmbCategory;
        private FlowLayoutPanel editorButtons;
        private FlowLayoutPanel bottomButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.mainPanel = new TableLayoutPanel();
            this.gridRules = new DataGridView();
            this.lblHelp = new Label();
            this.lblKeyword = new Label();
            this.lblCategory = new Label();
            this.txtKeyword = new TextBox();
            this.cmbCategory = new ComboBox();
            this.editorButtons = new FlowLayoutPanel();
            this.bottomButtons = new FlowLayoutPanel();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).BeginInit();
            this.editorButtons.SuspendLayout();
            this.bottomButtons.SuspendLayout();
            this.SuspendLayout();

            this.Text = "키워드 분류 규칙";
            this.ClientSize = new Size(640, 480);
            this.MinimumSize = new Size(560, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Malgun Gothic", 9F);

            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(10);
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.RowCount = 6;
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82F));
            this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

            this.lblHelp.Dock = DockStyle.Fill;
            this.lblHelp.Text = "창 제목이나 프로그램명에 특정 키워드가 포함되면 지정한 분류로 자동 기록됩니다.\r\n예: youtube -> 휴식, github -> 과제, lms -> 수업/시험";
            this.lblHelp.ForeColor = Color.FromArgb(70, 70, 70);
            this.lblHelp.TextAlign = ContentAlignment.MiddleLeft;
            this.mainPanel.Controls.Add(this.lblHelp, 0, 0);
            this.mainPanel.SetColumnSpan(this.lblHelp, 2);

            this.gridRules.Dock = DockStyle.Fill;
            this.gridRules.ReadOnly = true;
            this.gridRules.AllowUserToAddRows = false;
            this.gridRules.AllowUserToDeleteRows = false;
            this.gridRules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRules.MultiSelect = false;
            this.gridRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRules.RowHeadersVisible = false;
            this.gridRules.BackgroundColor = Color.White;
            this.gridRules.Columns.Add("Keyword", "키워드");
            this.gridRules.Columns.Add("Category", "분류");
            this.gridRules.CellClick += new DataGridViewCellEventHandler(this.GridRules_CellClick);
            this.mainPanel.Controls.Add(this.gridRules, 0, 1);
            this.mainPanel.SetColumnSpan(this.gridRules, 2);

            this.lblKeyword.Text = "키워드";
            this.lblKeyword.Dock = DockStyle.Fill;
            this.lblKeyword.TextAlign = ContentAlignment.MiddleLeft;
            this.txtKeyword.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(this.lblKeyword, 0, 2);
            this.mainPanel.Controls.Add(this.txtKeyword, 1, 2);

            this.lblCategory.Text = "분류";
            this.lblCategory.Dock = DockStyle.Fill;
            this.lblCategory.TextAlign = ContentAlignment.MiddleLeft;
            this.cmbCategory.Dock = DockStyle.Fill;
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbCategory.Items.AddRange(new object[] { "과제", "자료검색", "수업/시험", "문서작성", "휴식", "기타", "미분류" });
            this.cmbCategory.Text = "미분류";
            this.mainPanel.Controls.Add(this.lblCategory, 0, 3);
            this.mainPanel.Controls.Add(this.cmbCategory, 1, 3);

            this.editorButtons.Dock = DockStyle.Fill;
            this.editorButtons.FlowDirection = FlowDirection.LeftToRight;
            this.btnAdd.Text = "추가";
            this.btnAdd.Width = 92;
            this.btnAdd.Height = 32;
            this.btnAdd.Margin = new Padding(4);
            this.btnAdd.Click += new System.EventHandler(this.AddRule);
            this.btnUpdate.Text = "수정";
            this.btnUpdate.Width = 92;
            this.btnUpdate.Height = 32;
            this.btnUpdate.Margin = new Padding(4);
            this.btnUpdate.Click += new System.EventHandler(this.UpdateRule);
            this.btnDelete.Text = "삭제";
            this.btnDelete.Width = 92;
            this.btnDelete.Height = 32;
            this.btnDelete.Margin = new Padding(4);
            this.btnDelete.Click += new System.EventHandler(this.DeleteRule);
            this.editorButtons.Controls.Add(this.btnAdd);
            this.editorButtons.Controls.Add(this.btnUpdate);
            this.editorButtons.Controls.Add(this.btnDelete);
            this.mainPanel.Controls.Add(this.editorButtons, 1, 4);

            this.bottomButtons.Dock = DockStyle.Fill;
            this.bottomButtons.FlowDirection = FlowDirection.RightToLeft;
            this.btnSave.Text = "저장";
            this.btnSave.Width = 100;
            this.btnSave.Height = 32;
            this.btnSave.Margin = new Padding(4);
            this.btnSave.Click += new System.EventHandler(this.SaveAndClose);
            this.btnCancel.Text = "취소";
            this.btnCancel.Width = 100;
            this.btnCancel.Height = 32;
            this.btnCancel.Margin = new Padding(4);
            this.btnCancel.Click += new System.EventHandler(this.CancelAndClose);
            this.bottomButtons.Controls.Add(this.btnCancel);
            this.bottomButtons.Controls.Add(this.btnSave);
            this.mainPanel.Controls.Add(this.bottomButtons, 0, 5);
            this.mainPanel.SetColumnSpan(this.bottomButtons, 2);

            this.Controls.Add(this.mainPanel);
            this.AcceptButton = this.btnAdd;
            this.CancelButton = this.btnCancel;

            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).EndInit();
            this.editorButtons.ResumeLayout(false);
            this.bottomButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
