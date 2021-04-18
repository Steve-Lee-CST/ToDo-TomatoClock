using SimpleLogDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Services.SimpleLog
{
    public interface ISimpleLogService
    {
        public SimpleLogContext Context { get; }
    }
}
