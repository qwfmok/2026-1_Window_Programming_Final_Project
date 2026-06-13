namespace PCActivityTimeline.Models
{
    public class WindowInfo
    {
        public string ProgramName { get; set; }
        public string WindowTitle { get; set; }

        public bool IsSameActivity(WindowInfo other)
        {
            if (other == null) return false;
            return ProgramName == other.ProgramName && WindowTitle == other.WindowTitle;
        }
    }
}
