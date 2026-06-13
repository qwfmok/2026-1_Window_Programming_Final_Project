using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCActivityTimeline
{
    public static class UiTheme
    {
        public static readonly Color Background = Color.FromArgb(248, 250, 251);
        public static readonly Color Card = Color.White;
        public static readonly Color Text = Color.FromArgb(44, 62, 80);
        public static readonly Color Accent = Color.FromArgb(52, 152, 219);
        public static readonly Color AccentDark = Color.FromArgb(41, 128, 185);
        public static readonly Color Border = Color.FromArgb(218, 226, 232);
        public static readonly Color Header = Color.FromArgb(230, 241, 250);
        public static readonly Color SoftBlue = Color.FromArgb(236, 246, 252);
        public static readonly Color CardBlue = Color.FromArgb(244, 250, 254);
        public static readonly Color CardGreen = Color.FromArgb(248, 253, 249);

        public static void ApplyForm(Form form)
        {
            if (form == null) return;
            form.BackColor = Background;
            form.ForeColor = Text;
            form.Font = new Font("Malgun Gothic", 9F);
            ApplyApplicationIcon(form);
            ApplyControl(form);
        }

        public static Icon GetApplicationIcon()
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                return icon ?? SystemIcons.Application;
            }
            catch
            {
                return SystemIcons.Application;
            }
        }

        private static void ApplyApplicationIcon(Form form)
        {
            try
            {
                form.Icon = GetApplicationIcon();
            }
            catch
            {
                form.Icon = SystemIcons.Application;
            }
        }

        private static void ApplyControl(Control control)
        {
            if (control == null) return;

            if (control is TableLayoutPanel || control is FlowLayoutPanel || control is Panel)
                control.BackColor = Background;
            else if (control is GroupBox)
            {
                control.BackColor = Card;
                control.ForeColor = Text;
                control.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            }
            else if (control is Button)
                StyleButton((Button)control);
            else if (control is DataGridView)
                StyleGrid((DataGridView)control);
            else if (control is RichTextBox)
                StyleRichTextBox((RichTextBox)control);
            else if (control is TextBox || control is ComboBox || control is DateTimePicker)
                control.BackColor = Card;
            else if (control is Label)
                control.ForeColor = Text;

            foreach (Control child in control.Controls)
                ApplyControl(child);
        }

        public static void ApplyBackgroundCanvas(Control control)
        {
            if (control == null) return;

            control.BackColor = Background;
            control.Paint += (sender, e) =>
            {
                if (control.Width <= 0 || control.Height <= 0) return;

                Rectangle area = control.ClientRectangle;
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    area,
                    Color.FromArgb(248, 250, 251),
                    Color.FromArgb(235, 244, 250),
                    90F))
                {
                    e.Graphics.FillRectangle(brush, area);
                }

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (SolidBrush accentGlow = new SolidBrush(Color.FromArgb(32, Accent)))
                {
                    e.Graphics.FillEllipse(accentGlow, control.Width - 210, -95, 280, 190);
                    e.Graphics.FillEllipse(accentGlow, -90, control.Height - 130, 190, 170);
                }

                using (Pen accentLine = new Pen(Color.FromArgb(130, Accent), 3F))
                    e.Graphics.DrawLine(accentLine, 0, 0, control.Width, 0);
            };
        }

        private static void StyleButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = AccentDark;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(218, 239, 251);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(196, 225, 244);
            button.BackColor = Color.FromArgb(238, 247, 253);
            button.ForeColor = AccentDark;
            button.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Card;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Header;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Header;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Text;
            grid.DefaultCellStyle.BackColor = Card;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 235, 248);
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 254, 255);
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 235, 248);
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Text;
        }

        private static void StyleRichTextBox(RichTextBox textBox)
        {
            textBox.BackColor = Card;
            textBox.ForeColor = Text;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void StyleChart(Chart chart)
        {
            if (chart == null) return;
            chart.BackColor = CardBlue;
            foreach (ChartArea area in chart.ChartAreas)
                area.BackColor = CardBlue;
            foreach (Legend legend in chart.Legends)
            {
                legend.BackColor = CardBlue;
                legend.ForeColor = Text;
                legend.Font = new Font("Malgun Gothic", 8F);
            }
        }

        public static Color GetCategoryColor(string category)
        {
            if (category == "과제") return Color.FromArgb(52, 152, 219);
            if (category == "문서작성") return Color.FromArgb(230, 126, 34);
            if (category == "휴식") return Color.FromArgb(39, 174, 96);
            if (category == "자료검색") return Color.FromArgb(231, 76, 60);
            if (category == "수업/시험") return Color.FromArgb(155, 89, 182);
            if (category == "기타") return Color.FromArgb(127, 140, 141);
            return Color.FromArgb(149, 165, 166);
        }
    }
}
