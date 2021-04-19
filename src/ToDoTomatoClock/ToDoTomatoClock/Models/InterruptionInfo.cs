using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Models
{
    public class InterruptionInfo
    {
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; set; } = DateTime.MinValue;
        public string Remark { get; set; } = string.Empty;
    }
}
