using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Config
{
    public static class HardCode
    {
        public static string UserPath => Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

        public static string MicrosoftToDoDBName => "todosqlite.db";
        public static string MicrosoftToDoPath => string.Format("{0}\\{1}", UserPath, @"AppData\Local\Packages\Microsoft.Todos_8wekyb3d8bbwe");
        
        public static string SimpleLogDBName => "simplelog.db";

        public static string DefaultAlarmPath => "pack://application:,,,/Resources/Alarm.wav";
    }
}
