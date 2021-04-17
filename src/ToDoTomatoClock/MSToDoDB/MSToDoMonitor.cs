using MSToDoDB.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MSToDoDB
{
    public class MSToDoMonitor : Timer
    {
        public delegate void DataUpdateDelegate<T>(List<T> data);
        public event DataUpdateDelegate<Task> OnTodayTaskUpdated;

        protected object contextLocker = new object();
        protected MSToDoContext context = null;
        public MSToDoContext Context
        {
            get { lock (contextLocker) { return context; } }
            set { lock (contextLocker) { context = value; } }
        }

        public MSToDoMonitor(MSToDoContext context, int interval) : base(interval)
        {
            Context = context;
            Interval = interval;

            AutoReset = true;
            Elapsed += OnElapsed;
        }

        public MSToDoDataCollection GetDataCollection()
        {
            return new MSToDoDataCollection(Context);
        }

        protected object checkChangeLocker = new object();
        protected List<Task> preTodayTasks = new List<Task>();
        public void CheckTodayTasksChange()
        {
            lock (checkChangeLocker)
            {
                CheckTodayTasksChangeWithoutLock();
            }
        }

        protected void CheckTodayTasksChangeWithoutLock()
        {
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            List<Task> tempTask = context.Tasks.ToList();
            List<Task> completedTask = tempTask.Where(x => x.Status == "Completed").OrderBy(x => x.CommittedOrder).Reverse().ToList();
            List<Task> notStartedTask = tempTask.Where(x => x.Status == "NotStarted").OrderBy(x => x.CommittedOrder).Reverse().ToList();
            List<Task> nowTodayTasks = new List<Task>(notStartedTask);
            nowTodayTasks.AddRange(completedTask);
            if (nowTodayTasks.Count != preTodayTasks.Count)
            {
                OnTodayTaskUpdated?.Invoke(nowTodayTasks);
                preTodayTasks = nowTodayTasks;
            }

            for (int i = 0; i < nowTodayTasks.Count; i++)
            {
                if (preTodayTasks[i].CommittedOrder != nowTodayTasks[i].CommittedOrder ||
                    preTodayTasks[i].Subject != nowTodayTasks[i].Subject ||
                    preTodayTasks[i].Status != nowTodayTasks[i].Status ||
                    preTodayTasks[i].OriginalBodyContent != nowTodayTasks[i].OriginalBodyContent)
                {
                    OnTodayTaskUpdated?.Invoke(nowTodayTasks);
                    preTodayTasks = nowTodayTasks;
                    break;
                }
            }
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            lock (checkChangeLocker)
            {
                CheckTodayTasksChangeWithoutLock();
            }
        }
    }
}
