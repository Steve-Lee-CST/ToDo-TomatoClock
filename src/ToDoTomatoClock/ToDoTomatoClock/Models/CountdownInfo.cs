using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Models
{
    public class CountdownInfo
    {
        public int TotalSecond { get; set; } = 0;
        
        public int Minute
        {
            get => TotalSecond / 60;
            set => TotalSecond = value * 60 + Second;
        }
        public int Second
        {
            get => TotalSecond % 60;
            set => TotalSecond = Minute * 60 + value;
        }

        public CountdownInfoStatus Status { get; set; } = CountdownInfoStatus.NotStart;
        public void Tick()
        {
            TotalSecond--;
            if (TotalSecond <= 0)
            {
                TotalSecond = 0;
            }
        }

        public CountdownInfo Copy() => new CountdownInfo() 
        {
            TotalSecond = this.TotalSecond,
            Status = this.Status,

            TotalSecondRecord = this.TotalSecondRecord,
            StartTime = this.StartTime,
            FinishTime = this.FinishTime,
            InterruptionInfos = new List<InterruptionInfo>(this.InterruptionInfos),
            Remark = this.Remark
        };

        public int TotalSecondRecord { get; set; } = 0;
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; set; } = DateTime.MinValue;
        public List<InterruptionInfo> InterruptionInfos { get; set; } = new List<InterruptionInfo>();
        public string Remark { get; set; } = string.Empty;
    }

    public enum CountdownInfoStatus
    {
        NotStart = 0,
        Running = 1,
        Paused = 2,
        Completed = 3,
        Default = 0,
    }
}
