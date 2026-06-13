using PCActivityTimeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCActivityTimeline.Core
{
    public class ActivityManager
    {
        private readonly List<ActivityRecord> records = new List<ActivityRecord>();

        public void Add(ActivityRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            if (string.IsNullOrWhiteSpace(record.Id)) record.Id = Guid.NewGuid().ToString("N");
            records.Add(record);
        }

        public List<ActivityRecord> GetAll()
        {
            return records.OrderBy(r => r.StartTime).ToList();
        }

        public List<ActivityRecord> GetByDate(DateTime date)
        {
            return records
                .Where(r => r.StartTime.Date == date.Date)
                .OrderBy(r => r.StartTime)
                .ToList();
        }

        public ActivityRecord FindById(string id)
        {
            return records.FirstOrDefault(r => r.Id == id);
        }

        public void Update(ActivityRecord updated)
        {
            if (updated == null) throw new ArgumentNullException("updated");
            var target = FindById(updated.Id);
            if (target == null) throw new InvalidOperationException("수정할 기록을 찾을 수 없습니다.");

            target.ProgramName = updated.ProgramName;
            target.WindowTitle = updated.WindowTitle;
            target.Category = updated.Category;
            target.StartTime = updated.StartTime;
            target.EndTime = updated.EndTime;
            target.Memo = updated.Memo;
        }

        public void Delete(string id)
        {
            var target = FindById(id);
            if (target != null) records.Remove(target);
        }

        public int DeleteByDate(DateTime date)
        {
            var targets = records.Where(r => r.StartTime.Date == date.Date).ToList();
            foreach (var target in targets)
                records.Remove(target);
            return targets.Count;
        }

        public void ReplaceAll(IEnumerable<ActivityRecord> newRecords)
        {
            records.Clear();
            if (newRecords == null) return;
            records.AddRange(newRecords);
        }
    }
}
