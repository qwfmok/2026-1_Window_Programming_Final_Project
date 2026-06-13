using PCActivityTimeline.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PCActivityTimeline.Core
{
    public class KeywordRuleStorage
    {
        public void Save(List<KeywordRule> rules, string path)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<KeywordRule>));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, rules ?? new List<KeywordRule>());
                string json = Encoding.UTF8.GetString(stream.ToArray());
                File.WriteAllText(path, json, Encoding.UTF8);
            }
        }

        public List<KeywordRule> Load(string path)
        {
            if (!File.Exists(path)) return new List<KeywordRule>();

            string json = File.ReadAllText(path, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(json)) return new List<KeywordRule>();

            var serializer = new DataContractJsonSerializer(typeof(List<KeywordRule>));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (List<KeywordRule>)serializer.ReadObject(stream);
            }
        }
    }
}
