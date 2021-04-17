using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Services.ToDoDBMonitor
{
    public interface IMonitor<T>
    {
        public delegate void DataChangedDeleget(List<T> data);
        public event DataChangedDeleget DataChangeEvent;
        public List<T> CurrentData { get; }
    }
}
