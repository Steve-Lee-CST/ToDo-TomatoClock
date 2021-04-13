using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Config
{
    public class UserConfig
    {
        [JsonProperty("mstodo_db_path")]
        public string MSToDoDBPath { get; set; } = string.Empty;

        [JsonProperty("polling_interval")]
        public int PollingInterval { get; set; } = 4000;

        public static UserConfig GetDefault()
        {
            UserConfig config = new UserConfig();
            config.MSToDoDBPath = @"C:\Users\Admin\AppData\Local\Packages\Microsoft.Todos_8wekyb3d8bbwe\LocalState\AccountsRoot\e254b3639b444548ac82e4b0268628b4\todosqlite.db";
            config.PollingInterval = 4000;

            return config;
        }

        public static string ConfigToJsonStr(UserConfig config)
        {
            return JsonConvert.SerializeObject(config, Formatting.Indented);
        }

        public static UserConfig JsonStrToConfig(string jsonStr)
        {
            return JsonConvert.DeserializeObject<UserConfig>(jsonStr);
        }
    }
}
