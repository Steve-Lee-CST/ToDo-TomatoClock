using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Models;
using System.Linq;

namespace ToDoTomatoClock.Services.Countdown
{
    public class CountdownService : DispatcherTimer, ICountdownService
    {
        public event ICountdownService.TickDelegate TickEvent;
        public event ICountdownService.ReachEndDelegate ReachEndEvent;

        void ICountdownService.Start()
        {
            if (-1 == infoPtr) return;
            if(0 == currentInfo.TotalSecond)
            {
                ((ICountdownService)this).Next();
            }

            if(DateTime.MinValue == currentInfo.StartTime)
            {
                currentInfo.StartTime = DateTime.Now;
            }

            base.Start();
        }

        void ICountdownService.Pause()
        {
            if (-1 == infoPtr) return;
            base.Stop();
        }

        void ICountdownService.Reset()
        {
            if (-1 == infoPtr) return;
            base.Stop();
            CurrentInfo = countdownInfos[infoPtr];
            TickEvent?.Invoke(CurrentInfo);
        }

        void ICountdownService.Next()
        {
            if (-1 == infoPtr) return;

            base.Stop();
            if (DateTime.MinValue == currentInfo.FinishTime)
            {
                currentInfo.FinishTime = DateTime.Now;
            }
            currentInfo.Minute = 0;
            currentInfo.Second = 0;
            TickEvent?.Invoke(CurrentInfo);
            ReachEndEvent?.Invoke(CurrentInfo);

            infoPtr = (infoPtr + 1) % countdownInfos.Count;
            CurrentInfo = countdownInfos[infoPtr];
            TickEvent?.Invoke(CurrentInfo);
        }

        private List<CountdownInfo> countdownInfos;
        public List<CountdownInfo> CountdownInfos
        {
            get { return countdownInfos; }
            set { countdownInfos = value ?? new List<CountdownInfo>(); }
        }
        
        private CountdownInfo currentInfo;
        public CountdownInfo CurrentInfo
        {
            get => currentInfo?.Copy() ?? new CountdownInfo();
            set => currentInfo = value?.Copy() ?? new CountdownInfo();
        }

        private int infoPtr;
        public int InfoPtr
        {
            get { return infoPtr; }
            set { infoPtr = value; }
        }

        public CountdownService()
        {
            ResetCountdownInfo();

            Interval = new TimeSpan(0, 0, 1);
            Tick += (s, e) =>
            {
                currentInfo.Tick();
                if (currentInfo.ReachEnd)
                {
                    TickEvent?.Invoke(currentInfo.Copy());
                }
                else
                {
                    Stop();
                    TickEvent?.Invoke(currentInfo.Copy());
                    ReachEndEvent?.Invoke(CurrentInfo);
                }
            };
        }

        public CountdownInfo ClockToCountdownInfo(Clock clock) => new CountdownInfo()
        {
            Minute = clock.Minute,
            Second = clock.Second
        };

        public void ResetCountdownInfo()
        {
            countdownInfos = (from Clock item in App.UConfig.Clocks
                              select ClockToCountdownInfo(item)).ToList();
            if (0 == countdownInfos.Count)
            {
                CurrentInfo = new CountdownInfo();
                infoPtr = -1;
            }
            else
            {
                CurrentInfo = countdownInfos[0];
                infoPtr = 0;
            }
        }
    }
}
