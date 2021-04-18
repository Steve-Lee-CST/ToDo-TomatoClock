using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Models
{
    public class CountdownInfo
    {
        public int TotalSecond { get; protected set; } = 0;
        public int TotalSecondRecord { get; protected set; } = 0;

        public bool ReachEnd => TotalSecond <= 0;
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

        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; set; } = DateTime.MinValue;

        public void Tick() => TotalSecond--;

        public CountdownInfo(int totalSecond, int totalSecondRecord)
        {
            TotalSecond = totalSecond;
            TotalSecondRecord = totalSecondRecord;
        }

        public CountdownInfo Copy() => new CountdownInfo(TotalSecond, TotalSecondRecord);
    }
}
