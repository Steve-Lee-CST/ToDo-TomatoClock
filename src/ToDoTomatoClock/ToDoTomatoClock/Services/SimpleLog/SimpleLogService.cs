using MSToDoDB.Modules;
using SimpleLogDB;
using SimpleLogDB.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Tools;

namespace ToDoTomatoClock.Services.SimpleLog
{
    public class SimpleLogService : ISimpleLogService
    {
        private SimpleLogContext context;
        public SimpleLogContext Context
        {
            get => context;
        }

        public SimpleLogService()
        {
            string dbPath = string.Format("{0}\\{1}", Utils.GetCurrentPath, HardCode.SimpleLogDBName);
            context = new SimpleLogContext(SimpleLogContext.CreateConnectionString(dbPath));
            if (!File.Exists(dbPath))
                context.Database.EnsureCreated();
        }

        public SimpleGroup ConvertFromGroup(Group group)
        {
            return new SimpleGroup()
            {
                OnlineId = group.OnlineId,
                LocalId = group.LocalId,
                Name = group.Name
            };
        }

        public SimpleTaskFolder ConvertFromTaskFolder(TaskFolder taskFolder)
        {
            return new SimpleTaskFolder()
            {
                OnlineId = taskFolder.OnlineId,
                LocalId = taskFolder.LocalId,
                Name =taskFolder.Name,
                GroupLocalId = taskFolder.GroupLocalId,
            };
        }

        public SimpleTask ConvertFromTask(Task task)
        {
            return new SimpleTask()
            {
                OnlineId = task.OnlineId,
                LocalId = task.LocalId,
                Subject = task.Subject,
                OriginalBodyContent = task.OriginalBodyContent,
                TaskFolderLocalId = task.TaskFolderLocalId,
            };
        }
    }
}
