using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoTomatoClock.Tools
{
    public static class MsgToken
    {
        public static string Create(string sender, string receiver, string action) =>
            string.Format("MsgToken_{0}_To_{1}_On_{2}", sender, receiver, action);
    }
}
