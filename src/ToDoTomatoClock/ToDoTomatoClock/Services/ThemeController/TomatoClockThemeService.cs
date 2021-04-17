using System;
using System.Collections.Generic;
using System.Text;
using ToDoTomatoClock.Config;

namespace ToDoTomatoClock.Services.ThemeController
{
    public class TomatoClockThemeService : ITomatoClockThemeService
    {
        public int ThemePtr { get; set; }

        public TomatoClockTheme CurrentTheme => App.UConfig.TomatoClockThemes[ThemePtr];

        public TomatoClockThemeService()
        {
            ThemePtr = 0;
        }
        public TomatoClockTheme Next()
        {
            ThemePtr = (ThemePtr + 1) % App.UConfig.TomatoClockThemes.Count;
            return CurrentTheme;
        }
    }
}
