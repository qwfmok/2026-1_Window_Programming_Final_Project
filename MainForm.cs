using PCActivityTimeline.Core;
using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public partial class MainForm : Form
    {
        private enum CloseChoice
        {
            Minimize,
            Exit,
            Cancel
        }

        private const int ColId = 0;
        private const int ColStart = 1;
        private readonly ActivityManager activityManager = new ActivityManager();
        private readonly ActivityStorage activityStorage = new ActivityStorage();
        private readonly KeywordRuleStorage keywordRuleStorage = new KeywordRuleStorage();
        private readonly ActivitySummaryService summaryService = new ActivitySummaryService();
        private readonly ActivityTracker tracker;
        private readonly Timer trackingTimer = new Timer();

        private DataGridView grid;
        private DateTimePicker dateFilterPicker;
        private TextBox txtProgramName;
        private TextBox txtWindowTitle;
        private ComboBox cmbCategory;
        private DateTimePicker startPicker;
        private DateTimePicker endPicker;
        private TextBox txtMemo;
        private RichTextBox txtSummary;
        private Label lblStatus;
        private Label lblNotice;

        private List<KeywordRule> keywordRules = new List<KeywordRule>();
        private string selectedId;
        private bool allowExit;
        private readonly string defaultDataPath = Path.Combine(Application.StartupPath, "activities.json");
        private readonly string keywordRulesPath = Path.Combine(Application.StartupPath, "keyword_rules.json");

        public MainForm()
        {
            tracker = new ActivityTracker(new WindowInfoProvider(), activityManager);
            InitializeComponent();
            ConfigureActivityGridColumns();
            SetTrackingLight(false);
            ConfigureTimer();
            TryLoadKeywordRules();
            TryLoadDefaultData();
            RefreshGrid();
        }

        private void ConfigureActivityGridColumns()
        {
            if (grid == null || grid.Columns.Count < 8) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Columns[0].Visible = false;
            grid.Columns[1].FillWeight = 55F;
            grid.Columns[1].MinimumWidth = 62;
            grid.Columns[2].FillWeight = 55F;
            grid.Columns[2].MinimumWidth = 62;
            grid.Columns[3].FillWeight = 45F;
            grid.Columns[3].MinimumWidth = 58;
            grid.Columns[4].FillWeight = 85F;
            grid.Columns[4].MinimumWidth = 78;
            grid.Columns[5].FillWeight = 135F;
            grid.Columns[5].MinimumWidth = 115;
            grid.Columns[6].FillWeight = 70F;
            grid.Columns[6].MinimumWidth = 72;
            grid.Columns[7].FillWeight = 240F;
            grid.Columns[7].MinimumWidth = 180;
            grid.Columns[7].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void ConfigureTimer()
        {
            trackingTimer.Interval = 5000;
            trackingTimer.Tick += (s, e) =>
            {
                try
                {
                    var record = tracker.TrackOnce();
                    RefreshGrid();
                    SetNotice(record == null
                        ? "안내: 시스템/자기 프로그램 창은 자동 제외했습니다."
                        : "안내: " + DateTime.Now.ToString("HH:mm:ss") + " 활동 기록 중");
                }
                catch (Exception ex)
                {
                    trackingTimer.Stop();
                    SetTrackingLight(false);
                    MessageBox.Show("활동 기록 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetNotice("오류: 기록을 중지했습니다. " + ex.Message);
                }
            };
        }

        private void StartTracking(object sender, EventArgs e)
        {
            trackingTimer.Start();
            try
            {
                tracker.TrackOnce();
                RefreshGrid();
                SetTrackingLight(true);
                SetNotice("안내: 활동 기록을 시작했습니다.");
            }
            catch (Exception ex)
            {
                trackingTimer.Stop();
                SetTrackingLight(false);
                MessageBox.Show("기록 시작 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateFilterPicker_ValueChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void BtnToday_Click(object sender, EventArgs e)
        {
            dateFilterPicker.Value = DateTime.Today;
            RefreshGrid();
        }

        private void BtnManual_Click(object sender, EventArgs e)
        {
            using (var manual = new UserManualForm())
            {
                manual.ShowDialog(this);
            }
        }

        private void ShowKeywordRules(object sender, EventArgs e)
        {
            using (var form = new KeywordRuleForm(keywordRules))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                keywordRules = form.Rules;
                tracker.SetKeywordRules(keywordRules);
                keywordRuleStorage.Save(keywordRules, keywordRulesPath);

                int updated = ApplyKeywordRulesToUnclassifiedRecords();
                RefreshGrid();
                SetNotice("분류 규칙 저장 완료: " + updated + "개 미분류 기록 재분류");
            }
        }

        private void StopTracking(object sender, EventArgs e)
        {
            trackingTimer.Stop();
            tracker.StopCurrent();
            RefreshGrid();
            SetTrackingLight(false);
            SetNotice("안내: 활동 기록을 중지했습니다.");
        }

        private void AddRecord(object sender, EventArgs e)
        {
            try
            {
                var record = BuildRecordFromEditor(null);
                activityManager.Add(record);
                RefreshGrid();
                SelectRecord(record.Id);
                SetNotice("안내: 기록 추가 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRecord(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("수정할 기록을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var record = BuildRecordFromEditor(selectedId);
                activityManager.Update(record);
                RefreshGrid();
                SelectRecord(record.Id);
                SetNotice("안내: 기록 수정 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRecord(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("삭제할 기록을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("선택한 기록을 삭제할까요?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            activityManager.Delete(selectedId);
            ClearEditor(sender, e);
            RefreshGrid();
            SetNotice("안내: 기록 삭제 완료");
        }

        private void ClearTodayRecords(object sender, EventArgs e)
        {
            DateTime targetDate = dateFilterPicker.Value.Date;
            string message = targetDate == DateTime.Today
                ? "오늘 기록을 모두 초기화하시겠습니까?"
                : targetDate.ToString("yyyy-MM-dd") + " 기록을 모두 초기화하시겠습니까?";

            var result = MessageBox.Show(
                message + "\n삭제된 기록은 저장 전이라도 목록에서 제거됩니다.",
                "기록 초기화 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            int deletedCount = activityManager.DeleteByDate(targetDate);
            selectedId = null;
            ResetEditorFields();
            RefreshGrid();
            SetNotice("안내: " + deletedCount + "개 기록 초기화 완료");
        }

        private ActivityRecord BuildRecordFromEditor(string id)
        {
            if (string.IsNullOrWhiteSpace(txtProgramName.Text))
                throw new InvalidOperationException("프로그램명을 입력하세요.");
            if (string.IsNullOrWhiteSpace(txtWindowTitle.Text))
                throw new InvalidOperationException("창 제목을 입력하세요.");
            if (endPicker.Value < startPicker.Value)
                throw new InvalidOperationException("종료 시간은 시작 시간보다 빠를 수 없습니다.");

            return new ActivityRecord
            {
                Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString("N") : id,
                ProgramName = txtProgramName.Text.Trim(),
                WindowTitle = txtWindowTitle.Text.Trim(),
                Category = string.IsNullOrWhiteSpace(cmbCategory.Text) ? "미분류" : cmbCategory.Text.Trim(),
                StartTime = startPicker.Value,
                EndTime = endPicker.Value,
                Memo = txtMemo.Text.Trim()
            };
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = GetRowId(grid.Rows[e.RowIndex]);
            LoadRecordToEditor(id);
        }

        private void LoadRecordToEditor(string id)
        {
            var record = activityManager.FindById(id);
            if (record == null) return;

            selectedId = record.Id;
            txtProgramName.Text = record.ProgramName;
            txtWindowTitle.Text = record.WindowTitle;
            cmbCategory.Text = record.Category;
            startPicker.Value = record.StartTime;
            endPicker.Value = record.EndTime;
            txtMemo.Text = record.Memo;
        }

        private void ClearEditor(object sender, EventArgs e)
        {
            ResetEditorFields();
        }

        private void ResetEditorFields()
        {
            selectedId = null;
            txtProgramName.Text = "";
            txtWindowTitle.Text = "";
            cmbCategory.Text = "미분류";
            startPicker.Value = DateTime.Now;
            endPicker.Value = DateTime.Now;
            txtMemo.Text = "";
            grid.ClearSelection();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            allowExit = true;
            trackingTimer.Stop();
            tracker.StopCurrent();
            SetTrackingLight(false);

            try
            {
                activityStorage.Save(activityManager.GetAll(), defaultDataPath);
                keywordRuleStorage.Save(keywordRules, keywordRulesPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("종료 전 자동 저장 중 오류가 발생했습니다.\n" + ex.Message, "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void SetTrackingLight(bool isTracking)
        {
            if (lblTrackingLight == null) return;
            lblTrackingLight.ForeColor = isTracking ? Color.LimeGreen : Color.Red;
            lblTrackingLight.Invalidate();
            if (lblStatus != null)
                lblStatus.Text = isTracking ? "기록 중" : "기록 중지";
        }

        private void DrawTrackingLight(object sender, PaintEventArgs e)
        {
            var label = sender as Label;
            if (label == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(label.ForeColor))
            {
                Rectangle bounds = new Rectangle(2, 2, label.Width - 5, label.Height - 5);
                e.Graphics.FillEllipse(brush, bounds);
            }
        }

        private void SetNotice(string text)
        {
            if (lblNotice != null)
                lblNotice.Text = text;
        }

        private int ApplyKeywordRulesToUnclassifiedRecords()
        {
            int updated = 0;
            foreach (var record in activityManager.GetAll())
            {
                if (record == null) continue;
                if (!string.IsNullOrWhiteSpace(record.Category) && record.Category != "미분류") continue;

                string guessed = tracker.GuessCategoryFor(record.ProgramName, record.WindowTitle);
                if (string.IsNullOrWhiteSpace(guessed) || guessed == "미분류") continue;

                record.Category = guessed;
                updated++;
            }

            return updated;
        }

        private void RefreshGrid()
        {
            string keepSelectedId = selectedId;
            int firstDisplayedRowIndex = -1;

            try
            {
                if (grid.Rows.Count > 0)
                    firstDisplayedRowIndex = grid.FirstDisplayedScrollingRowIndex;
            }
            catch
            {
                firstDisplayedRowIndex = -1;
            }

            List<ActivityRecord> records = activityManager.GetByDate(dateFilterPicker.Value);
            grid.Rows.Clear();

            foreach (var record in records)
            {
                int rowIndex = grid.Rows.Add(
                    record.Id,
                    record.StartTime.ToString("HH:mm:ss"),
                    record.EndTime.ToString("HH:mm:ss"),
                    FormatDuration(record.Duration),
                    record.ProgramName,
                    record.WindowTitle,
                    record.Category,
                    record.Memo);

                ApplyCategoryColor(grid.Rows[rowIndex], record.Category);
            }

            grid.ClearSelection();
            RestoreGridState(keepSelectedId, firstDisplayedRowIndex);
            txtSummary.Text = summaryService.BuildSummary(records);
            StyleSummaryText();
        }

        private void StyleSummaryText()
        {
            if (txtSummary == null || string.IsNullOrEmpty(txtSummary.Text)) return;

            txtSummary.SelectAll();
            txtSummary.SelectionFont = new Font("Malgun Gothic", 9.5F, FontStyle.Regular);
            txtSummary.SelectionColor = Color.FromArgb(30, 30, 30);
            txtSummary.SelectionBackColor = Color.FromArgb(250, 252, 255);

            int firstLineLength = txtSummary.Text.IndexOf(Environment.NewLine);
            if (firstLineLength < 0) firstLineLength = txtSummary.Text.Length;
            txtSummary.Select(0, firstLineLength);
            txtSummary.SelectionFont = new Font("Malgun Gothic", 10F, FontStyle.Bold);
            txtSummary.SelectionColor = Color.FromArgb(28, 81, 135);

            txtSummary.Select(0, 0);
        }

        private void ApplyCategoryColor(DataGridViewRow row, string category)
        {
            if (row == null) return;

            string value = category ?? "";
            if (value == "과제")
                row.DefaultCellStyle.BackColor = Color.FromArgb(225, 238, 255);
            else if (value == "자료검색")
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
            else if (value == "수업/시험")
                row.DefaultCellStyle.BackColor = Color.FromArgb(235, 225, 255);
            else if (value == "문서작성")
                row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 240);
            else if (value == "휴식")
                row.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            else if (value == "기타")
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 255);
            else
                row.DefaultCellStyle.BackColor = Color.White;
        }

        private void RestoreGridState(string keepSelectedId, int firstDisplayedRowIndex)
        {
            bool selectedRestored = false;

            if (!string.IsNullOrWhiteSpace(keepSelectedId))
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (GetRowId(row) == keepSelectedId)
                    {
                        row.Selected = true;
                        selectedRestored = true;
                        break;
                    }
                }
            }

            if (!selectedRestored)
                selectedId = null;

            try
            {
                if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < grid.Rows.Count)
                    grid.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            }
            catch
            {
                // 화면 갱신 중 스크롤 위치를 복구할 수 없는 경우에도 기록 기능은 계속 동작한다.
            }
        }

        private void SelectRecord(string id)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (GetRowId(row) == id)
                {
                    row.Selected = true;
                    if (row.Cells.Count > ColStart)
                        grid.CurrentCell = row.Cells[ColStart];
                    LoadRecordToEditor(id);
                    return;
                }
            }
        }

        private string GetRowId(DataGridViewRow row)
        {
            if (row == null || row.Cells.Count <= ColId) return "";
            return Convert.ToString(row.Cells[ColId].Value);
        }

        private string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
                return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)duration.TotalHours, duration.Minutes, duration.Seconds);
            return string.Format("{0:D2}:{1:D2}", duration.Minutes, duration.Seconds);
        }

        private void SaveData(object sender, EventArgs e)
        {
            try
            {
                activityStorage.Save(activityManager.GetAll(), defaultDataPath);
                keywordRuleStorage.Save(keywordRules, keywordRulesPath);
                SetNotice("저장 완료: " + defaultDataPath);
                MessageBox.Show("활동 기록을 저장했습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("저장 중 오류가 발생했습니다.\n" + ex.Message, "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData(object sender, EventArgs e)
        {
            try
            {
                var records = activityStorage.Load(defaultDataPath);
                activityManager.ReplaceAll(records);
                RefreshGrid();
                SetNotice("불러오기 완료: " + defaultDataPath);
                MessageBox.Show("활동 기록을 불러왔습니다.", "불러오기", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("불러오기 중 오류가 발생했습니다.\n" + ex.Message, "불러오기 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportCsv(object sender, EventArgs e)
        {
            try
            {
                var records = activityManager.GetByDate(dateFilterPicker.Value);
                if (records.Count == 0)
                {
                    MessageBox.Show("내보낼 활동 기록이 없습니다.", "CSV 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = "CSV 내보내기";
                    dialog.Filter = "CSV 파일 (*.csv)|*.csv";
                    dialog.FileName = "activity_" + dateFilterPicker.Value.ToString("yyyyMMdd") + ".csv";

                    if (dialog.ShowDialog(this) != DialogResult.OK) return;

                    File.WriteAllText(dialog.FileName, BuildCsv(records), new System.Text.UTF8Encoding(true));
                    SetNotice("CSV 저장 완료: " + dialog.FileName);
                    MessageBox.Show("CSV 파일을 저장했습니다.", "CSV 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 내보내기 중 오류가 발생했습니다.\n" + ex.Message, "CSV 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetailedAnalysis(object sender, EventArgs e)
        {
            var records = activityManager.GetByDate(dateFilterPicker.Value);
            using (var form = new AnalysisForm(dateFilterPicker.Value.Date, records))
            {
                form.ShowDialog(this);
            }
        }

        private string BuildCsv(List<ActivityRecord> records)
        {
            var lines = new List<string>();
            lines.Add("시작,종료,사용시간,프로그램,창 제목,분류,메모");

            foreach (var record in records)
            {
                lines.Add(string.Join(",",
                    EscapeCsv(record.StartTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    EscapeCsv(record.EndTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    EscapeCsv(FormatDuration(record.Duration)),
                    EscapeCsv(record.ProgramName),
                    EscapeCsv(record.WindowTitle),
                    EscapeCsv(record.Category),
                    EscapeCsv(record.Memo)));
            }

            return string.Join(Environment.NewLine, lines);
        }

        private string EscapeCsv(string value)
        {
            string safe = value ?? "";
            if (safe.Contains("\""))
                safe = safe.Replace("\"", "\"\"");

            if (safe.Contains(",") || safe.Contains("\"") || safe.Contains("\r") || safe.Contains("\n"))
                safe = "\"" + safe + "\"";

            return safe;
        }

        private void TryLoadDefaultData()
        {
            try
            {
                if (File.Exists(defaultDataPath))
                    activityManager.ReplaceAll(activityStorage.Load(defaultDataPath));
            }
            catch
            {
                SetNotice("안내: 기존 데이터 불러오기 실패");
            }
        }

        private void TryLoadKeywordRules()
        {
            try
            {
                keywordRules = keywordRuleStorage.Load(keywordRulesPath);
                tracker.SetKeywordRules(keywordRules);
            }
            catch
            {
                keywordRules = new List<KeywordRule>();
                tracker.SetKeywordRules(keywordRules);
                SetNotice("안내: 분류 규칙 불러오기 실패");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowExit)
            {
                var result = ShowCloseChoiceDialog();

                if (result == CloseChoice.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (result == CloseChoice.Minimize)
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                    SetNotice(trackingTimer.Enabled ? "안내: 최소화 상태로 기록 중입니다." : "안내: 창을 최소화했습니다.");
                    return;
                }

                allowExit = true;
                trackingTimer.Stop();
                tracker.StopCurrent();
                SetTrackingLight(false);
            }

            try
            {
                activityStorage.Save(activityManager.GetAll(), defaultDataPath);
                keywordRuleStorage.Save(keywordRules, keywordRulesPath);
            }
            catch
            {
                // 종료 시 자동 저장 실패는 사용자가 직접 저장할 수 있으므로 종료를 막지 않는다.
            }

            base.OnFormClosing(e);
        }

        private CloseChoice ShowCloseChoiceDialog()
        {
            using (var form = new Form())
            using (var lblMessage = new Label())
            using (var btnMinimize = new Button())
            using (var btnExit = new Button())
            using (var btnCancel = new Button())
            {
                form.Text = "종료 방식 선택";
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new Size(360, 150);
                form.Font = new Font("Malgun Gothic", 9F);

                lblMessage.Text = "프로그램을 어떻게 처리할까요?\n최소화해도 기록은 계속 진행됩니다.";
                lblMessage.AutoSize = false;
                lblMessage.TextAlign = ContentAlignment.MiddleLeft;
                lblMessage.SetBounds(20, 18, 320, 52);

                btnMinimize.Text = "최소화";
                btnMinimize.SetBounds(30, 92, 90, 34);
                btnMinimize.Click += (s, e) =>
                {
                    form.Tag = CloseChoice.Minimize;
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                };

                btnExit.Text = "완전 종료";
                btnExit.SetBounds(130, 92, 100, 34);
                btnExit.Click += (s, e) =>
                {
                    form.Tag = CloseChoice.Exit;
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                };

                btnCancel.Text = "취소";
                btnCancel.SetBounds(240, 92, 90, 34);
                btnCancel.Click += (s, e) =>
                {
                    form.Tag = CloseChoice.Cancel;
                    form.DialogResult = DialogResult.Cancel;
                    form.Close();
                };

                form.Controls.Add(lblMessage);
                form.Controls.Add(btnMinimize);
                form.Controls.Add(btnExit);
                form.Controls.Add(btnCancel);
                form.AcceptButton = btnMinimize;
                form.CancelButton = btnCancel;
                form.Tag = CloseChoice.Cancel;

                form.ShowDialog(this);
                return (CloseChoice)form.Tag;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (trackingTimer != null) trackingTimer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
