using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Services.Countdown;
using ToDoTomatoClock.Services.SimpleLog;
using ToDoTomatoClock.Services.ThemeController;
using ToDoTomatoClock.Services.ToDoDBMonitor;
using ToDoTomatoClock.Views;

namespace ToDoTomatoClock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserConfig UConfig { get; private set; }

        static App()
        {
            var (flg, config) = UserConfig.LoadUserConfig();
            if (!flg) App.Current.Shutdown();
            UConfig = config;
            InitService();
        }

        static void InitService()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ICountdownService, CountdownService>()
                .AddSingleton<ITomatoClockThemeService, TomatoClockThemeService>()
                .AddSingleton<ITodayTaskMonitorService, TodayTaskMonitorService>()
                .AddSingleton<ISimpleLogService, SimpleLogService>()
                .AddSingleton<TodayToDoView>()
                .BuildServiceProvider());

            Ioc.Default.GetService<TodayToDoView>();
        }

       
    }
}
