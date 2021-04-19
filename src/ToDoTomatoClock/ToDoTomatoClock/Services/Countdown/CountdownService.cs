using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Models;
using System.Linq;
using System.Threading;

namespace ToDoTomatoClock.Services.Countdown
{
    // 有空改成 有限状态机
    // 倒计时服务
    public class CountdownService : DispatcherTimer, ICountdownService
    {
        public event ICountdownService.TickDelegate TickEvent;
        public event ICountdownService.ReachEndDelegate ReachEndEvent;
        public event ICountdownService.InterruptDelegate RecoverFromInterruptEvent;

        void ICountdownService.Start()
        {
            if (-1 == infoPtr) return;

            switch (currentInfo.Status)
            {
                case CountdownInfoStatus.NotStart:
                    currentInfo.Status = CountdownInfoStatus.Running;
                    currentInfo.StartTime = DateTime.Now;
                    base.Start();
                    return;
                case CountdownInfoStatus.Running:
                    return;
                case CountdownInfoStatus.Paused:
                    RecoverFromInterruptEvent.Invoke();
                    return;
                case CountdownInfoStatus.Completed:
                    return;
                default:
                    return;
            }
        }

        void ICountdownService.Pause()
        {
            if (-1 == infoPtr) return;

            switch (currentInfo.Status)
            {
                case CountdownInfoStatus.NotStart:
                    return;
                case CountdownInfoStatus.Running:
                    base.Stop();
                    currentInfo.InterruptionInfos.Add(new InterruptionInfo()
                    {
                        StartTime = DateTime.Now
                    });
                    currentInfo.Status = CountdownInfoStatus.Paused;
                    return;
                case CountdownInfoStatus.Paused:
                    return;
                case CountdownInfoStatus.Completed:
                    return;
                default:
                    return;
            }
            
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

            switch (currentInfo.Status)
            {
                case CountdownInfoStatus.NotStart:
                    infoPtr = (infoPtr + countdownInfos.Count + 1) % countdownInfos.Count;
                    CurrentInfo = countdownInfos[infoPtr];

                    TickEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Running:
                    base.Stop();
                    currentInfo.Minute = 0;
                    currentInfo.Second = 0;
                    currentInfo.FinishTime = DateTime.Now;
                    currentInfo.Status = CountdownInfoStatus.Completed;

                    TickEvent?.Invoke(CurrentInfo);
                    ReachEndEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Paused:
                    base.Stop();
                    currentInfo.Minute = 0;
                    currentInfo.Second = 0;
                    currentInfo.FinishTime = DateTime.Now;
                    currentInfo.InterruptionInfos.Last().FinishTime = DateTime.Now;
                    currentInfo.Status = CountdownInfoStatus.Completed;

                    TickEvent?.Invoke(CurrentInfo);
                    ReachEndEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Completed:
                    infoPtr = (infoPtr + countdownInfos.Count + 1) % countdownInfos.Count;
                    CurrentInfo = countdownInfos[infoPtr];

                    TickEvent?.Invoke(CurrentInfo);
                    return;
                default:
                    return;
            }
        }

        void ICountdownService.Pre()
        {
            if (-1 == infoPtr) return;

            switch (currentInfo.Status)
            {
                case CountdownInfoStatus.NotStart:
                    infoPtr = (infoPtr + countdownInfos.Count - 1) % countdownInfos.Count;
                    CurrentInfo = countdownInfos[infoPtr];

                    TickEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Running:
                    base.Stop();
                    currentInfo.Minute = 0;
                    currentInfo.Second = 0;
                    currentInfo.FinishTime = DateTime.Now;
                    currentInfo.Status = CountdownInfoStatus.Completed;

                    TickEvent?.Invoke(CurrentInfo);
                    ReachEndEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Paused:
                    base.Stop();
                    currentInfo.Minute = 0;
                    currentInfo.Second = 0;
                    currentInfo.FinishTime = DateTime.Now;
                    currentInfo.InterruptionInfos.Last().FinishTime = DateTime.Now;
                    currentInfo.Status = CountdownInfoStatus.Completed;

                    TickEvent?.Invoke(CurrentInfo);
                    ReachEndEvent?.Invoke(CurrentInfo);
                    return;
                case CountdownInfoStatus.Completed:
                    infoPtr = (infoPtr + countdownInfos.Count - 1) % countdownInfos.Count;
                    CurrentInfo = countdownInfos[infoPtr];

                    TickEvent?.Invoke(CurrentInfo);
                    return;
                default:
                    return;
            }
        }

        CountdownInfo ICountdownService.SetCountdownRemark(string remark)
        {
            currentInfo.Remark = remark;
            return CurrentInfo;
        }

        CountdownInfo ICountdownService.SetInterruptionRemark(string remark)
        {
            currentInfo.InterruptionInfos.Last().Remark = remark;
            currentInfo.InterruptionInfos.Last().FinishTime = DateTime.Now;

            base.Start();
            currentInfo.Status = CountdownInfoStatus.Running;

            return CurrentInfo;
        }

        private CountdownInfo currentInfo;
        public CountdownInfo CurrentInfo
        {
            get => currentInfo?.Copy() ?? new CountdownInfo();
            set => currentInfo = value?.Copy() ?? new CountdownInfo();
        }

        private List<CountdownInfo> countdownInfos;
        public List<CountdownInfo> CountdownInfos
        {
            get { return countdownInfos; }
            set { countdownInfos = value ?? new List<CountdownInfo>(); }
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
                if (currentInfo.TotalSecond <= 0)
                {
                    base.Stop();
                    currentInfo.FinishTime = DateTime.Now;
                    currentInfo.Status = CountdownInfoStatus.Completed;
                    TickEvent?.Invoke(CurrentInfo);
                    ReachEndEvent?.Invoke(CurrentInfo);
                }
                else
                {
                    TickEvent?.Invoke(CurrentInfo);
                }
            };
        }

        public CountdownInfo ClockToCountdownInfo(Clock clock)
        {
            int totalSecond = clock.Minute * 60 + clock.Second;
            return new CountdownInfo() 
            {
                TotalSecond = totalSecond,
                TotalSecondRecord = totalSecond
            };
        }

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
