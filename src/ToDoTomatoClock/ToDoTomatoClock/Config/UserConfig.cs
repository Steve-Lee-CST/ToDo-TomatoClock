using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using ToDoTomatoClock.Tools;

namespace ToDoTomatoClock.Config
{
    public class UserConfig
    {
        [JsonProperty("mstodo_detail")]
        public MSToDoDetail MSToDoDetail { get; set; } = new MSToDoDetail();

        [JsonProperty("tomato_clock_theme_list")]
        public List<TomatoClockTheme> TomatoClockThemes { get; set; } = new List<TomatoClockTheme>();

        [JsonProperty("clock_list")]
        public List<Clock> Clocks { get; set; } = new List<Clock>();

        [JsonProperty("alarm_wave")]
        public string AlarmFile { get; set; } = string.Empty;

        public static UserConfig GetDefault()
        {
            UserConfig config = new UserConfig();
            config.MSToDoDetail = MSToDoDetail.GetDefault();
            config.TomatoClockThemes = TomatoClockTheme.GetDefaultThemeList();
            config.Clocks = Clock.GetDefaultClockList();
            config.AlarmFile = string.Empty;

            return config;
        }

        public static string Serialize(UserConfig config)
        {
            return JsonConvert.SerializeObject(config, Formatting.Indented);
        }

        public static UserConfig Deserialize(string jsonStr)
        {
            return JsonConvert.DeserializeObject<UserConfig>(jsonStr);
        }

        public static (bool, UserConfig) LoadUserConfig()
        {
            string configPath = $"{Utils.GetCurrentPath}config.json";
            UserConfig UConfig = null;
            if (!File.Exists(configPath))
            {
                if (MessageBox.Show(
                        "config.json does not exist! \nCreate config.json as default?",
                        "Warning",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UConfig = UserConfig.GetDefault();
                    File.WriteAllText(configPath, UserConfig.Serialize(UConfig), Encoding.UTF8);
                    return (true, UConfig);
                }

                return (false, UConfig);
            }
            else
            {
                string json = File.ReadAllText(configPath, Encoding.UTF8);
                try
                {
                    UConfig = UserConfig.Deserialize(json);
                    if (null == UConfig.TomatoClockThemes || 0 == UConfig.TomatoClockThemes.Count)
                    {
                        UConfig.TomatoClockThemes = TomatoClockTheme.GetDefaultThemeList();
                    }
                    if(null == UConfig.Clocks || 0 == UConfig.Clocks.Count)
                    {
                        UConfig.Clocks = Clock.GetDefaultClockList();
                    }
                    File.WriteAllText(configPath, UserConfig.Serialize(UConfig), Encoding.UTF8);
                    return (true, UConfig);
                }
                catch (Exception _)
                {
                    if (MessageBox.Show(
                            "config.json is incorrect! \nReset config.json as default?",
                            "Warning",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        UConfig = UserConfig.GetDefault();
                        File.WriteAllText(configPath, UserConfig.Serialize(UConfig), Encoding.UTF8);
                        return (true, UConfig);
                    }
                    else
                    {
                        return (false, UConfig);
                    }
                }
            }
        }
    }
}
