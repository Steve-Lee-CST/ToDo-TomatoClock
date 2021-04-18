using SimpleLogDB;
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
    }
}
