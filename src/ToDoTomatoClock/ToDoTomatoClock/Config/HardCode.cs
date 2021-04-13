using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Config
{
    public static class HardCode
    {
        public static string UserPath { get { return Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"); } }
        public static string MicrosoftToDoPath { get { return string.Format("{0}\\{1}", UserPath, @"AppData\Local\Packages\Microsoft.Todos_8wekyb3d8bbwe"); } }
    }
}
