using System;
using System.Collections.Generic;
using System.Text;
using ToDoTomatoClock.Models;

namespace ToDoTomatoClock.Services.Countdown
{
    public interface ICountdownService
    {
        public delegate void TickDelegate(CountdownInfo info);
        public event TickDelegate TickEvent;

        public delegate void ReachEndDelegate(CountdownInfo info);
        public event ReachEndDelegate ReachEndEvent;

        public delegate void InterruptDelegate();
        public event InterruptDelegate RecoverFromInterruptEvent;

        public void Start();
        public void Pause();
        public void Reset();
        public void Next();
        public void Pre();

        public CountdownInfo SetCountdownRemark(string remark);
        public CountdownInfo SetInterruptionRemark(string remark);

        public CountdownInfo CurrentInfo { get; }
    }
}
