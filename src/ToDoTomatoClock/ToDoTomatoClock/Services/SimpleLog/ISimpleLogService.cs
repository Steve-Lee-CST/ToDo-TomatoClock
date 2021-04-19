using MSToDoDB.Modules;
using SimpleLogDB;
using SimpleLogDB.Modules;
using System;
using System.Collections.Generic;
using System.Text;
namespace ToDoTomatoClock.Services.SimpleLog
{
    public interface ISimpleLogService
    {
        public SimpleLogContext Context { get; }

        public SimpleGroup ConvertFromGroup(Group group);
        public SimpleTaskFolder ConvertFromTaskFolder(TaskFolder taskFolder);
        public SimpleTask ConvertFromTask(Task task);
    }
}
