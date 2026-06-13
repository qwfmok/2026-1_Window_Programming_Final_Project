using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    partial class MainForm
    {
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel topBarPanel;
        private FlowLayoutPanel toolbarPanel;
        private FlowLayoutPanel datePanel;
        private Panel exitPanel;
        private TableLayoutPanel editorPanel;
        private FlowLayoutPanel editorButtonPanel;
        private Button btnStart;
        private Button btnStop;
        private Button btnSave;
        private Button btnLoad;
        private Button btnExportCsv;
        private Button btnAnalysis;
        private Button btnRules;
        private Button btnToday;
        private Button btnManual;
        private Button btnExit;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClearToday;
        private Label lblDateFilter;
        private Label lblTrackingLight;
        private Label lblProgramName;
        private Label lblWindowTitle;
        private Label lblCategory;
        private Label lblStartTime;
        private Label lblEndTime;
        private Label lblMemo;

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topBarPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolbarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTrackingLight = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.exitPanel = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.datePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDateFilter = new System.Windows.Forms.Label();
            this.dateFilterPicker = new System.Windows.Forms.DateTimePicker();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnClearToday = new System.Windows.Forms.Button();
            this.lblNotice = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.editorPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.txtProgramName = new System.Windows.Forms.TextBox();
            this.lblWindowTitle = new System.Windows.Forms.Label();
            this.txtWindowTitle = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.lblMemo = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.editorButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSummary = new System.Windows.Forms.RichTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainPanel.SuspendLayout();
            this.topBarPanel.SuspendLayout();
            this.toolbarPanel.SuspendLayout();
            this.exitPanel.SuspendLayout();
            this.datePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.editorPanel.SuspendLayout();
            this.editorButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.mainPanel.Controls.Add(this.topBarPanel, 0, 0);
            this.mainPanel.Controls.Add(this.grid, 0, 1);
            this.mainPanel.Controls.Add(this.editorPanel, 1, 1);
            this.mainPanel.Controls.Add(this.txtSummary, 0, 2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 3;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.mainPanel.Size = new System.Drawing.Size(1220, 760);
            this.mainPanel.TabIndex = 0;
            // 
            // topBarPanel
            // 
            this.topBarPanel.ColumnCount = 2;
            this.mainPanel.SetColumnSpan(this.topBarPanel, 2);
            this.topBarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.topBarPanel.Controls.Add(this.toolbarPanel, 0, 0);
            this.topBarPanel.Controls.Add(this.exitPanel, 1, 0);
            this.topBarPanel.Controls.Add(this.datePanel, 0, 1);
            this.topBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topBarPanel.Location = new System.Drawing.Point(3, 3);
            this.topBarPanel.Name = "topBarPanel";
            this.topBarPanel.RowCount = 2;
            this.topBarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.topBarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBarPanel.Size = new System.Drawing.Size(1214, 106);
            this.topBarPanel.TabIndex = 0;
            // 
            // toolbarPanel
            // 
            this.toolbarPanel.Controls.Add(this.lblTrackingLight);
            this.toolbarPanel.Controls.Add(this.lblStatus);
            this.toolbarPanel.Controls.Add(this.btnStart);
            this.toolbarPanel.Controls.Add(this.btnStop);
            this.toolbarPanel.Controls.Add(this.btnSave);
            this.toolbarPanel.Controls.Add(this.btnLoad);
            this.toolbarPanel.Controls.Add(this.btnExportCsv);
            this.toolbarPanel.Controls.Add(this.btnAnalysis);
            this.toolbarPanel.Controls.Add(this.btnRules);
            this.toolbarPanel.Controls.Add(this.btnManual);
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbarPanel.Location = new System.Drawing.Point(3, 3);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);
            this.toolbarPanel.Size = new System.Drawing.Size(1096, 52);
            this.toolbarPanel.TabIndex = 0;
            // 
            // lblTrackingLight
            // 
            this.lblTrackingLight.ForeColor = System.Drawing.Color.Red;
            this.lblTrackingLight.Location = new System.Drawing.Point(20, 14);
            this.lblTrackingLight.Margin = new System.Windows.Forms.Padding(12, 4, 8, 0);
            this.lblTrackingLight.Name = "lblTrackingLight";
            this.lblTrackingLight.Size = new System.Drawing.Size(36, 36);
            this.lblTrackingLight.TabIndex = 0;
            this.lblTrackingLight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawTrackingLight);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(67, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(8, 10, 12, 0);
            this.lblStatus.Size = new System.Drawing.Size(86, 35);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "준비됨";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(160, 14);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(92, 32);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "기록 시작";
            this.btnStart.Click += new System.EventHandler(this.StartTracking);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(260, 14);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(92, 32);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "기록 중지";
            this.btnStop.Click += new System.EventHandler(this.StopTracking);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(360, 14);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.SaveData);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(460, 14);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(92, 32);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "불러오기";
            this.btnLoad.Click += new System.EventHandler(this.LoadData);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(560, 14);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(112, 32);
            this.btnExportCsv.TabIndex = 6;
            this.btnExportCsv.Text = "CSV 내보내기";
            this.btnExportCsv.Click += new System.EventHandler(this.ExportCsv);
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Location = new System.Drawing.Point(680, 14);
            this.btnAnalysis.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(92, 32);
            this.btnAnalysis.TabIndex = 7;
            this.btnAnalysis.Text = "상세 분석";
            this.btnAnalysis.Click += new System.EventHandler(this.ShowDetailedAnalysis);
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(780, 14);
            this.btnRules.Margin = new System.Windows.Forms.Padding(4);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(92, 32);
            this.btnRules.TabIndex = 8;
            this.btnRules.Text = "분류 규칙";
            this.btnRules.Click += new System.EventHandler(this.ShowKeywordRules);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(880, 14);
            this.btnManual.Margin = new System.Windows.Forms.Padding(4);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(92, 32);
            this.btnManual.TabIndex = 9;
            this.btnManual.Text = "설명서";
            this.btnManual.Click += new System.EventHandler(this.BtnManual_Click);
            // 
            // exitPanel
            // 
            this.exitPanel.Controls.Add(this.btnExit);
            this.exitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitPanel.Location = new System.Drawing.Point(1105, 3);
            this.exitPanel.Name = "exitPanel";
            this.exitPanel.Padding = new System.Windows.Forms.Padding(4, 8, 8, 0);
            this.exitPanel.Size = new System.Drawing.Size(106, 52);
            this.exitPanel.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExit.Location = new System.Drawing.Point(4, 8);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 32);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "종료";
            this.btnExit.Click += new System.EventHandler(this.ExitApplication);
            // 
            // datePanel
            // 
            this.topBarPanel.SetColumnSpan(this.datePanel, 2);
            this.datePanel.Controls.Add(this.lblDateFilter);
            this.datePanel.Controls.Add(this.dateFilterPicker);
            this.datePanel.Controls.Add(this.btnToday);
            this.datePanel.Controls.Add(this.btnClearToday);
            this.datePanel.Controls.Add(this.lblNotice);
            this.datePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datePanel.Location = new System.Drawing.Point(3, 61);
            this.datePanel.Name = "datePanel";
            this.datePanel.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.datePanel.Size = new System.Drawing.Size(1208, 42);
            this.datePanel.TabIndex = 2;
            this.datePanel.WrapContents = false;
            // 
            // lblDateFilter
            // 
            this.lblDateFilter.AutoSize = true;
            this.lblDateFilter.Location = new System.Drawing.Point(11, 0);
            this.lblDateFilter.Name = "lblDateFilter";
            this.lblDateFilter.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblDateFilter.Size = new System.Drawing.Size(90, 33);
            this.lblDateFilter.TabIndex = 0;
            this.lblDateFilter.Text = "조회 날짜";
            // 
            // dateFilterPicker
            // 
            this.dateFilterPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFilterPicker.Location = new System.Drawing.Point(107, 3);
            this.dateFilterPicker.Name = "dateFilterPicker";
            this.dateFilterPicker.Size = new System.Drawing.Size(150, 31);
            this.dateFilterPicker.TabIndex = 1;
            this.dateFilterPicker.ValueChanged += new System.EventHandler(this.DateFilterPicker_ValueChanged);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(264, 4);
            this.btnToday.Margin = new System.Windows.Forms.Padding(4);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(92, 32);
            this.btnToday.TabIndex = 2;
            this.btnToday.Text = "오늘";
            this.btnToday.Click += new System.EventHandler(this.BtnToday_Click);
            // 
            // btnClearToday
            // 
            this.btnClearToday.Location = new System.Drawing.Point(364, 4);
            this.btnClearToday.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearToday.Name = "btnClearToday";
            this.btnClearToday.Size = new System.Drawing.Size(120, 32);
            this.btnClearToday.TabIndex = 3;
            this.btnClearToday.Text = "기록 전체 삭제";
            this.btnClearToday.Click += new System.EventHandler(this.ClearTodayRecords);
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = true;
            this.lblNotice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblNotice.Location = new System.Drawing.Point(491, 0);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Padding = new System.Windows.Forms.Padding(18, 8, 0, 0);
            this.lblNotice.Size = new System.Drawing.Size(232, 33);
            this.lblNotice.TabIndex = 4;
            this.lblNotice.Text = "안내: 프로그램 준비 완료";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeight = 34;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 115);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 62;
            this.grid.RowTemplate.Height = 24;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(811, 487);
            this.grid.TabIndex = 1;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            // 
            // editorPanel
            // 
            this.editorPanel.ColumnCount = 2;
            this.editorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.editorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editorPanel.Controls.Add(this.lblProgramName, 0, 0);
            this.editorPanel.Controls.Add(this.txtProgramName, 1, 0);
            this.editorPanel.Controls.Add(this.lblWindowTitle, 0, 1);
            this.editorPanel.Controls.Add(this.txtWindowTitle, 1, 1);
            this.editorPanel.Controls.Add(this.lblCategory, 0, 2);
            this.editorPanel.Controls.Add(this.cmbCategory, 1, 2);
            this.editorPanel.Controls.Add(this.lblStartTime, 0, 3);
            this.editorPanel.Controls.Add(this.startPicker, 1, 3);
            this.editorPanel.Controls.Add(this.lblEndTime, 0, 4);
            this.editorPanel.Controls.Add(this.endPicker, 1, 4);
            this.editorPanel.Controls.Add(this.lblMemo, 0, 5);
            this.editorPanel.Controls.Add(this.txtMemo, 1, 5);
            this.editorPanel.Controls.Add(this.editorButtonPanel, 0, 8);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(820, 115);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Padding = new System.Windows.Forms.Padding(8);
            this.editorPanel.RowCount = 9;
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.editorPanel.Size = new System.Drawing.Size(397, 487);
            this.editorPanel.TabIndex = 2;
            // 
            // lblProgramName
            // 
            this.lblProgramName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProgramName.Location = new System.Drawing.Point(11, 8);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(80, 36);
            this.lblProgramName.TabIndex = 0;
            this.lblProgramName.Text = "프로그램";
            this.lblProgramName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProgramName
            // 
            this.txtProgramName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProgramName.Location = new System.Drawing.Point(97, 11);
            this.txtProgramName.Name = "txtProgramName";
            this.txtProgramName.Size = new System.Drawing.Size(289, 31);
            this.txtProgramName.TabIndex = 1;
            // 
            // lblWindowTitle
            // 
            this.lblWindowTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWindowTitle.Location = new System.Drawing.Point(11, 44);
            this.lblWindowTitle.Name = "lblWindowTitle";
            this.lblWindowTitle.Size = new System.Drawing.Size(80, 36);
            this.lblWindowTitle.TabIndex = 2;
            this.lblWindowTitle.Text = "창 제목";
            this.lblWindowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWindowTitle
            // 
            this.txtWindowTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWindowTitle.Location = new System.Drawing.Point(97, 47);
            this.txtWindowTitle.Name = "txtWindowTitle";
            this.txtWindowTitle.Size = new System.Drawing.Size(289, 31);
            this.txtWindowTitle.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Location = new System.Drawing.Point(11, 80);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(80, 36);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "분류";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.Items.AddRange(new object[] {
            "과제",
            "자료검색",
            "수업/시험",
            "문서작성",
            "휴식",
            "기타",
            "미분류"});
            this.cmbCategory.Location = new System.Drawing.Point(97, 83);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(289, 33);
            this.cmbCategory.TabIndex = 5;
            this.cmbCategory.Text = "미분류";
            // 
            // lblStartTime
            // 
            this.lblStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStartTime.Location = new System.Drawing.Point(11, 116);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(80, 36);
            this.lblStartTime.TabIndex = 6;
            this.lblStartTime.Text = "시작";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startPicker
            // 
            this.startPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startPicker.Location = new System.Drawing.Point(97, 119);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(289, 31);
            this.startPicker.TabIndex = 7;
            // 
            // lblEndTime
            // 
            this.lblEndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndTime.Location = new System.Drawing.Point(11, 152);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(80, 36);
            this.lblEndTime.TabIndex = 8;
            this.lblEndTime.Text = "종료";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // endPicker
            // 
            this.endPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endPicker.Location = new System.Drawing.Point(97, 155);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(289, 31);
            this.endPicker.TabIndex = 9;
            // 
            // lblMemo
            // 
            this.lblMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemo.Location = new System.Drawing.Point(11, 188);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(80, 243);
            this.lblMemo.TabIndex = 10;
            this.lblMemo.Text = "메모";
            this.lblMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Location = new System.Drawing.Point(97, 191);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.editorPanel.SetRowSpan(this.txtMemo, 3);
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(289, 237);
            this.txtMemo.TabIndex = 11;
            // 
            // editorButtonPanel
            // 
            this.editorPanel.SetColumnSpan(this.editorButtonPanel, 2);
            this.editorButtonPanel.Controls.Add(this.btnAdd);
            this.editorButtonPanel.Controls.Add(this.btnUpdate);
            this.editorButtonPanel.Controls.Add(this.btnDelete);
            this.editorButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorButtonPanel.Location = new System.Drawing.Point(11, 434);
            this.editorButtonPanel.Name = "editorButtonPanel";
            this.editorButtonPanel.Size = new System.Drawing.Size(375, 42);
            this.editorButtonPanel.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 32);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "추가";
            this.btnAdd.Click += new System.EventHandler(this.AddRecord);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(104, 4);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(92, 32);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.Click += new System.EventHandler(this.UpdateRecord);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(204, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 32);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Click += new System.EventHandler(this.DeleteRecord);
            // 
            // txtSummary
            // 
            this.txtSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.txtSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.SetColumnSpan(this.txtSummary, 2);
            this.txtSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSummary.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.txtSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtSummary.Location = new System.Drawing.Point(3, 608);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(1214, 149);
            this.txtSummary.TabIndex = 3;
            this.txtSummary.Text = "";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "시작";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "종료";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "시간";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "프로그램";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "창 제목";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "분류";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "메모";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1220, 760);
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.MinimumSize = new System.Drawing.Size(1040, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "나 뭐 했지? - PC Activity Timeline";
            this.mainPanel.ResumeLayout(false);
            this.topBarPanel.ResumeLayout(false);
            this.toolbarPanel.ResumeLayout(false);
            this.toolbarPanel.PerformLayout();
            this.exitPanel.ResumeLayout(false);
            this.datePanel.ResumeLayout(false);
            this.datePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.editorPanel.ResumeLayout(false);
            this.editorPanel.PerformLayout();
            this.editorButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}
