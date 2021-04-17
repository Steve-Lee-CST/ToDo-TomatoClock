using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoTomatoClock.Tools;

namespace ToDoTomatoClock.Config
{
    public class MSToDoDetail
    {
        [JsonProperty("db_path")]
        public string DBPath { get; set; } = string.Empty;

        [JsonProperty("polling_interval")]
        public int PollingInterval { get; set; } = 4000;

        public static MSToDoDetail GetDefault()
        {
            MSToDoDetail detail = new MSToDoDetail();

            List<string> dbPaths = Utils.SearchFileInPath(HardCode.MicrosoftToDoPath, HardCode.MicrosoftToDoDBName);
            if (0 < dbPaths.Count) detail.DBPath = dbPaths[0];

            return detail;
        }
    }
}
