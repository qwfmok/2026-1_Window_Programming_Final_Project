using PCActivityTimeline.Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PCActivityTimeline.Core
{
    public class WindowInfoProvider
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public WindowInfo GetCurrentWindowInfo()
        {
            IntPtr handle = GetForegroundWindow();
            if (handle == IntPtr.Zero)
            {
                return new WindowInfo { ProgramName = "Unknown", WindowTitle = "활성 창 없음" };
            }

            var titleBuilder = new StringBuilder(512);
            GetWindowText(handle, titleBuilder, titleBuilder.Capacity);

            uint processId;
            GetWindowThreadProcessId(handle, out processId);

            string programName = "Unknown";
            try
            {
                var process = Process.GetProcessById((int)processId);
                programName = process.ProcessName;
            }
            catch
            {
                programName = "Unknown";
            }

            string title = titleBuilder.ToString();
            if (string.IsNullOrWhiteSpace(title)) title = "(제목 없는 창)";

            return new WindowInfo
            {
                ProgramName = programName,
                WindowTitle = title
            };
        }
    }
}
