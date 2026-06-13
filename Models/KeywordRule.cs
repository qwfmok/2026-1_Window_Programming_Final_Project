using System.Runtime.Serialization;

namespace PCActivityTimeline.Models
{
    [DataContract]
    public class KeywordRule
    {
        [DataMember]
        public string Keyword { get; set; }

        [DataMember]
        public string Category { get; set; }

        public KeywordRule()
        {
            Keyword = "";
            Category = "미분류";
        }
    }
}
