using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    public static class AppDialog
    {
        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (Form form = new Form())
            using (TableLayoutPanel panel = new TableLayoutPanel())
            using (Label iconLabel = new Label())
            using (Label titleLabel = new Label())
            using (Label messageLabel = new Label())
            using (FlowLayoutPanel buttonPanel = new FlowLayoutPanel())
            {
                form.Text = title;
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = false;
                form.ClientSize = new Size(460, 230);

                panel.Dock = DockStyle.Fill;
                panel.Padding = new Padding(18);
                panel.ColumnCount = 2;
                panel.RowCount = 3;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));

                iconLabel.Text = GetIconText(icon);
                iconLabel.Dock = DockStyle.Fill;
                iconLabel.TextAlign = ContentAlignment.MiddleCenter;
                iconLabel.Font = new Font("Malgun Gothic", 22F, FontStyle.Bold);
                iconLabel.ForeColor = GetIconColor(icon);

                titleLabel.Text = title;
                titleLabel.Dock = DockStyle.Fill;
                titleLabel.TextAlign = ContentAlignment.MiddleLeft;
                titleLabel.Font = new Font("Malgun Gothic", 12F, FontStyle.Bold);
                titleLabel.ForeColor = UiTheme.AccentDark;

                messageLabel.Text = message;
                messageLabel.Dock = DockStyle.Fill;
                messageLabel.TextAlign = ContentAlignment.TopLeft;
                messageLabel.Font = new Font("Malgun Gothic", 9.5F);
                messageLabel.ForeColor = UiTheme.Text;

                buttonPanel.Dock = DockStyle.Fill;
                buttonPanel.FlowDirection = FlowDirection.RightToLeft;
                buttonPanel.WrapContents = false;
                buttonPanel.Padding = new Padding(0, 12, 0, 0);

                AddButtons(form, buttonPanel, buttons);

                panel.Controls.Add(iconLabel, 0, 0);
                panel.SetRowSpan(iconLabel, 2);
                panel.Controls.Add(titleLabel, 1, 0);
                panel.Controls.Add(messageLabel, 1, 1);
                panel.Controls.Add(buttonPanel, 0, 2);
                panel.SetColumnSpan(buttonPanel, 2);
                form.Controls.Add(panel);

                UiTheme.ApplyForm(form);
                panel.BackColor = UiTheme.CardBlue;
                buttonPanel.BackColor = UiTheme.CardBlue;
                iconLabel.ForeColor = GetIconColor(icon);
                titleLabel.ForeColor = UiTheme.AccentDark;
                messageLabel.ForeColor = UiTheme.Text;

                return form.ShowDialog(owner);
            }
        }

        public static DialogResult Show(IWin32Window owner, string message, string title)
        {
            return Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult ShowCloseChoice(IWin32Window owner)
        {
            using (Form form = new Form())
            using (TableLayoutPanel panel = new TableLayoutPanel())
            using (Label titleLabel = new Label())
            using (Label messageLabel = new Label())
            using (FlowLayoutPanel buttonPanel = new FlowLayoutPanel())
            {
                form.Text = "종료 방식 선택";
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = false;
                form.ClientSize = new Size(470, 220);

                panel.Dock = DockStyle.Fill;
                panel.Padding = new Padding(20);
                panel.RowCount = 3;
                panel.ColumnCount = 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));

                titleLabel.Text = "프로그램을 어떻게 처리할까요?";
                titleLabel.Dock = DockStyle.Fill;
                titleLabel.TextAlign = ContentAlignment.MiddleLeft;
                titleLabel.Font = new Font("Malgun Gothic", 12F, FontStyle.Bold);
                titleLabel.ForeColor = UiTheme.AccentDark;

                messageLabel.Text = "백그라운드 최소화를 선택하면 창은 숨겨지고 기록은 계속됩니다.";
                messageLabel.Dock = DockStyle.Fill;
                messageLabel.TextAlign = ContentAlignment.TopLeft;
                messageLabel.Font = new Font("Malgun Gothic", 9.5F);
                messageLabel.ForeColor = UiTheme.Text;

                buttonPanel.Dock = DockStyle.Fill;
                buttonPanel.FlowDirection = FlowDirection.RightToLeft;
                buttonPanel.WrapContents = false;
                buttonPanel.Padding = new Padding(0, 12, 0, 0);

                AddDialogButton(form, buttonPanel, "취소", DialogResult.Cancel);
                AddDialogButton(form, buttonPanel, "완전 종료", DialogResult.No);
                AddDialogButton(form, buttonPanel, "백그라운드 최소화", DialogResult.Yes, 142);

                panel.Controls.Add(titleLabel, 0, 0);
                panel.Controls.Add(messageLabel, 0, 1);
                panel.Controls.Add(buttonPanel, 0, 2);
                form.Controls.Add(panel);

                UiTheme.ApplyForm(form);
                panel.BackColor = UiTheme.CardBlue;
                buttonPanel.BackColor = UiTheme.CardBlue;
                titleLabel.ForeColor = UiTheme.AccentDark;
                messageLabel.ForeColor = UiTheme.Text;

                form.AcceptButton = FindButtonByResult(buttonPanel, DialogResult.Yes);
                form.CancelButton = FindButtonByResult(buttonPanel, DialogResult.Cancel);

                return form.ShowDialog(owner);
            }
        }

        private static void AddButtons(Form form, FlowLayoutPanel buttonPanel, MessageBoxButtons buttons)
        {
            if (buttons == MessageBoxButtons.YesNo)
            {
                AddDialogButton(form, buttonPanel, "아니요", DialogResult.No);
                AddDialogButton(form, buttonPanel, "예", DialogResult.Yes);
                form.AcceptButton = FindButtonByResult(buttonPanel, DialogResult.Yes);
                form.CancelButton = FindButtonByResult(buttonPanel, DialogResult.No);
                return;
            }

            AddDialogButton(form, buttonPanel, "확인", DialogResult.OK);
            form.AcceptButton = FindButtonByResult(buttonPanel, DialogResult.OK);
            form.CancelButton = FindButtonByResult(buttonPanel, DialogResult.OK);
        }

        private static void AddDialogButton(Form form, FlowLayoutPanel buttonPanel, string text, DialogResult result)
        {
            AddDialogButton(form, buttonPanel, text, result, 92);
        }

        private static void AddDialogButton(Form form, FlowLayoutPanel buttonPanel, string text, DialogResult result, int width)
        {
            Button button = new Button();
            button.Text = text;
            button.Width = width;
            button.Height = 34;
            button.Margin = new Padding(8, 0, 0, 0);
            button.DialogResult = result;
            button.Click += (sender, e) =>
            {
                form.DialogResult = result;
                form.Close();
            };
            buttonPanel.Controls.Add(button);
        }

        private static IButtonControl FindButtonByResult(FlowLayoutPanel panel, DialogResult result)
        {
            foreach (Control control in panel.Controls)
            {
                Button button = control as Button;
                if (button != null && button.DialogResult == result)
                    return button;
            }

            return null;
        }

        private static string GetIconText(MessageBoxIcon icon)
        {
            if (icon == MessageBoxIcon.Error) return "!";
            if (icon == MessageBoxIcon.Warning) return "!";
            if (icon == MessageBoxIcon.Question) return "?";
            if (icon == MessageBoxIcon.Information) return "i";
            return "i";
        }

        private static Color GetIconColor(MessageBoxIcon icon)
        {
            if (icon == MessageBoxIcon.Error) return Color.FromArgb(192, 57, 43);
            if (icon == MessageBoxIcon.Warning) return Color.FromArgb(230, 126, 34);
            if (icon == MessageBoxIcon.Question) return UiTheme.AccentDark;
            if (icon == MessageBoxIcon.Information) return UiTheme.AccentDark;
            return UiTheme.AccentDark;
        }
    }
}
