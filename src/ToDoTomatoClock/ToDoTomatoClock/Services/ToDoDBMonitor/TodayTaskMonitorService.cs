using Microsoft.EntityFrameworkCore;
using MSToDoDB;
using MSToDoDB.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ToDoTomatoClock.Services.ToDoDBMonitor
{
    public class TodayTaskMonitorService : ITodayTaskMonitorService
    {
        private List<Task> currentData = new List<Task>();
        public List<Task> CurrentData
        {
            get => currentData;
            set => currentData = value;
        }

        public event IMonitor<Task>.DataChangedDeleget DataChangeEvent;

        private MSToDoContext Context { get; set; }
        private Timer Timer { get; set; }

        protected readonly object checkChangeLocker = new object();

        public TodayTaskMonitorService()
        {
            Context = new MSToDoContext(MSToDoContext.CreateConnectionString(App.UConfig.MSToDoDetail.DBPath));
            Timer = new Timer(App.UConfig.MSToDoDetail.PollingInterval);
            Timer.Elapsed += (s, e) =>
            {
                lock (checkChangeLocker)
                {
                    CheckChangeWithoutLock();
                }
            };

            Timer.AutoReset = true;
            Timer.Start();
        }

        protected void CheckChangeWithoutLock()
        {
            foreach (var entity in Context.ChangeTracker.Entries().ToList())
            {
                entity.Reload();
            }

            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");

            List<Task> completedTask = Context.Tasks
                .Where(x => x.CommittedDate == nowDate)
                .Where(x => x.Status == "Completed")
                .OrderBy(x => x.CommittedOrder)
                .Reverse().ToList();
            List<Task> notStartedTask = Context.Tasks
                .Where(x => x.CommittedDate == nowDate)
                .Where(x => x.Status == "NotStarted")
                .OrderBy(x => x.CommittedOrder)
                .Reverse().ToList();
            List<Task> nowTodayTasks = new List<Task>(notStartedTask);
            nowTodayTasks.AddRange(completedTask);

            if (nowTodayTasks.Count != currentData.Count)
            {
                DataChangeEvent?.Invoke(nowTodayTasks);
                currentData = new List<Task>(nowTodayTasks);
            }

            for (int i = 0; i < nowTodayTasks.Count; i++)
            {
                if (currentData[i].CommittedOrder != nowTodayTasks[i].CommittedOrder ||
                    currentData[i].Subject != nowTodayTasks[i].Subject ||
                    currentData[i].Status != nowTodayTasks[i].Status ||
                    currentData[i].OriginalBodyContent != nowTodayTasks[i].OriginalBodyContent)
                {
                    DataChangeEvent?.Invoke(nowTodayTasks);
                    currentData = new List<Task>(nowTodayTasks);
                    break;
                }
            }
        }

    }
}
