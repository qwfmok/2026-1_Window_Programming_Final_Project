using PCActivityTimeline.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PCActivityTimeline.Core
{
    public class ActivityStorage
    {
        public void Save(List<ActivityRecord> records, string path)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<ActivityRecord>));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, records ?? new List<ActivityRecord>());
                string json = Encoding.UTF8.GetString(stream.ToArray());
                File.WriteAllText(path, json, Encoding.UTF8);
            }
        }

        public List<ActivityRecord> Load(string path)
        {
            if (!File.Exists(path)) return new List<ActivityRecord>();

            string json = File.ReadAllText(path, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(json)) return new List<ActivityRecord>();

            var serializer = new DataContractJsonSerializer(typeof(List<ActivityRecord>));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (List<ActivityRecord>)serializer.ReadObject(stream);
            }
        }
    }
}
