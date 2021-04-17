using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Config
{
    public class TomatoClockTheme
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("global_background")]
        public string GlobalBackground { get; set; }

        [JsonProperty("button")]
        public ButtonTheme Button { get; set; }

        [JsonProperty("label")]
        public LabelTheme Label { get; set; }

        public static List<TomatoClockTheme> GetDefaultThemeList()
        {
            List<TomatoClockTheme> themeList = new List<TomatoClockTheme>();
            themeList.Add(new TomatoClockTheme()
            {
                Tag = "D",
                Name = "Dark",
                GlobalBackground = "#49000000",
                Button = new ButtonTheme()
                {
                    StaticForeground = "#49FFFFFF",
                    MouseOverForeground = "#64FFFFFF",
                    PressedForeground = "#49FFFFFF"
                },
                Label = new LabelTheme()
                {
                    FontSize = 80,
                    FontFamily = "Consals",
                    Foreground = "#49FFFFFF"
                }
            });

            themeList.Add(new TomatoClockTheme()
            {
                Tag = "L",
                Name = "Light",
                GlobalBackground = "#36E5F0F0",
                Button = new ButtonTheme()
                {
                    StaticForeground = "#81000000",
                    MouseOverForeground = "#95000000",
                    PressedForeground = "#81000000"
                },
                Label = new LabelTheme()
                {
                    FontSize = 80,
                    FontFamily = "Consals",
                    Foreground = "#95000000"
                }
            });

            return themeList;
        }
    }

    public class ButtonTheme
    {
        [JsonProperty("static_foreground")]
        public string StaticForeground { get; set; }

        [JsonProperty("mouse_over_foreground")]
        public string MouseOverForeground { get; set; }

        [JsonProperty("pressed_foreground")]
        public string PressedForeground { get; set; }
    }

    public class LabelTheme
    {
        [JsonProperty("font_size")]
        public int FontSize { get; set; }

        [JsonProperty("font_family")]
        public string FontFamily { get; set; }

        [JsonProperty("foreground")]
        public string Foreground { get; set; }
    }
}
