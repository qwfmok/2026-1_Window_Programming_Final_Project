using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public partial class KeywordRuleForm : Form
    {
        private readonly List<KeywordRule> rules;

        public List<KeywordRule> Rules
        {
            get { return rules.Select(r => new KeywordRule { Keyword = r.Keyword, Category = r.Category }).ToList(); }
        }

        public KeywordRuleForm(IEnumerable<KeywordRule> currentRules)
        {
            rules = currentRules == null
                ? new List<KeywordRule>()
                : currentRules.Select(r => new KeywordRule { Keyword = r.Keyword, Category = r.Category }).ToList();

            InitializeComponent();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            gridRules.Rows.Clear();
            foreach (var rule in rules)
                gridRules.Rows.Add(rule.Keyword, rule.Category);
        }

        private void GridRules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= rules.Count) return;

            txtKeyword.Text = rules[e.RowIndex].Keyword;
            cmbCategory.Text = rules[e.RowIndex].Category;
        }

        private void AddRule(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            string category = cmbCategory.Text.Trim();

            if (keyword.Length == 0)
            {
                MessageBox.Show("키워드를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (category.Length == 0)
            {
                MessageBox.Show("분류를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rules.Add(new KeywordRule { Keyword = keyword, Category = category });
            txtKeyword.Text = "";
            cmbCategory.Text = "미분류";
            RefreshGrid();
        }

        private void UpdateRule(object sender, EventArgs e)
        {
            if (gridRules.CurrentRow == null || gridRules.CurrentRow.Index < 0 || gridRules.CurrentRow.Index >= rules.Count)
            {
                MessageBox.Show("수정할 규칙을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string keyword = txtKeyword.Text.Trim();
            string category = cmbCategory.Text.Trim();
            if (keyword.Length == 0 || category.Length == 0)
            {
                MessageBox.Show("키워드와 분류를 모두 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = gridRules.CurrentRow.Index;
            rules[index].Keyword = keyword;
            rules[index].Category = category;
            RefreshGrid();
            if (index < gridRules.Rows.Count)
                gridRules.Rows[index].Selected = true;
        }

        private void DeleteRule(object sender, EventArgs e)
        {
            if (gridRules.CurrentRow == null || gridRules.CurrentRow.Index < 0 || gridRules.CurrentRow.Index >= rules.Count)
            {
                MessageBox.Show("삭제할 규칙을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            rules.RemoveAt(gridRules.CurrentRow.Index);
            txtKeyword.Text = "";
            cmbCategory.Text = "미분류";
            RefreshGrid();
        }

        private void SaveAndClose(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelAndClose(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
