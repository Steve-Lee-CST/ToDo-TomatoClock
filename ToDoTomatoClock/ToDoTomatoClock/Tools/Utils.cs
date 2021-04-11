using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoTomatoClock.Tools
{
    public static class Utils
    {
        public static List<string> SearchFileInPath(string path, string fileName)
        {
            return new List<string>(Directory.GetFiles(path, fileName, SearchOption.AllDirectories));
        }
    }
}
