using System;
using System.Runtime.Serialization;

namespace PCActivityTimeline.Models
{
    [DataContract]
    public class ActivityRecord
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string ProgramName { get; set; }

        [DataMember]
        public string WindowTitle { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public string Memo { get; set; }

        public TimeSpan Duration
        {
            get
            {
                if (EndTime < StartTime) return TimeSpan.Zero;
                return EndTime - StartTime;
            }
        }

        public ActivityRecord()
        {
            Id = Guid.NewGuid().ToString("N");
            Category = "미분류";
            Memo = "";
        }
    }
}
