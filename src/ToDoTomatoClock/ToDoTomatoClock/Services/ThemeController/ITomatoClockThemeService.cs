using System;
using System.Collections.Generic;
using System.Text;
using ToDoTomatoClock.Config;

namespace ToDoTomatoClock.Services.ThemeController
{
    public interface ITomatoClockThemeService
    {
        public TomatoClockTheme Next();
        public TomatoClockTheme CurrentTheme { get; }
    }
}
