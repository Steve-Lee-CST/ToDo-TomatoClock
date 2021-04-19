using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MSToDoDB.Modules;
using SimpleLogDB.Modules;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Models;
using ToDoTomatoClock.Services.Countdown;
using ToDoTomatoClock.Services.SimpleLog;
using ToDoTomatoClock.Services.ThemeController;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.Views;

namespace ToDoTomatoClock.ViewModels
{
    public class TomatoClockViewModel : ObservableObject
    {
        public TomatoClockViewModel()
        {
            InitBinding();
        }

        private void InitBinding()
        {
            InitBindingWindow();

            InitBindingChangeThemeBtn();

            InitBindingCloseBtn();
            InitBindingMinimizeBtn();
            InitBindingTopMostBtn();
            
            InitBindingStartBtn();
            InitBindingPauseBtn();
            InitBindingResetBtn();
            InitBindingNextBtn();
            InitBindingPreBtn();
            InitBindingFCListBtn();
            InitBindingTaskListBtn();

            InitBindingCountdownStr();

            
        }

        #region Binding TomatoClockTheme

        // 更改主题
        private void ApplyTheme(TomatoClockTheme theme)
        {
            ThemeTag = theme.Tag;
            GlobalBackground = new SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(theme.GlobalBackground));
            ButtonStaticForeground = new SolidColorBrush(ColorStrToColor(theme.Button.StaticForeground));
            ButtonMouseOverForeground = new SolidColorBrush(ColorStrToColor(theme.Button.MouseOverForeground));
            ButtonPressedForeground = new SolidColorBrush(ColorStrToColor(theme.Button.PressedForeground));
            ButtonStaticOpacity = ColorStrToColor(theme.Button.StaticForeground).A / 255.0;
            ButtonMouseOverOpacity = ColorStrToColor(theme.Button.MouseOverForeground).A / 255.0;
            ButtonPressedOpacity = ColorStrToColor(theme.Button.PressedForeground).A / 255.0;

            CloseIcon = GetBitmapBaseOnTheme(AppResource.CloseIcon);
            MinimizeIcon = GetBitmapBaseOnTheme(AppResource.MinimizeIcon);
            TopMostIcon = GetBitmapBaseOnTheme(AppResource.UnpinIcon);

            StartIcon = GetBitmapBaseOnTheme(AppResource.StartIcon);
            PauseIcon = GetBitmapBaseOnTheme(AppResource.PauseIcon);
            ResetIcon = GetBitmapBaseOnTheme(AppResource.ResetIcon);
            NextIcon = GetBitmapBaseOnTheme(AppResource.NextIcon);
            PreIcon = GetBitmapBaseOnTheme(AppResource.PreIcon);
            FCListIcon = GetBitmapBaseOnTheme(AppResource.FCListIcon);
            TaskListIcon = GetBitmapBaseOnTheme(AppResource.TaskListIcon);

            LabelFontFamily = new System.Windows.Media.FontFamily(theme.Label.FontFamily);
            LabelFontSize = theme.Label.FontSize;
            LabelForeground = new SolidColorBrush(ColorStrToColor(theme.Label.Foreground));
        }

        private SolidColorBrush buttonStaticForeground;

        public SolidColorBrush ButtonStaticForeground
        {
            get => buttonStaticForeground;
            set => SetProperty(ref buttonStaticForeground, value);
        }

        private double buttonStaticOpacity;
        public double ButtonStaticOpacity
        {
            get => buttonStaticOpacity;
            set => SetProperty(ref buttonStaticOpacity, value);
        }


        private SolidColorBrush buttonMouseOverForeground;

        public SolidColorBrush ButtonMouseOverForeground
        {
            get => buttonMouseOverForeground;
            set => SetProperty(ref buttonMouseOverForeground, value);
        }

        private double buttonMouseOverOpacity;

        public double ButtonMouseOverOpacity
        {
            get => buttonMouseOverOpacity;
            set => SetProperty(ref buttonMouseOverOpacity, value);
        }

        private SolidColorBrush buttonPressedForeground;

        public SolidColorBrush ButtonPressedForeground
        {
            get => buttonPressedForeground;
            set => SetProperty(ref buttonPressedForeground, value);
        }

        private double buttonPressedOpacity;

        public double ButtonPressedOpacity
        {
            get => buttonPressedOpacity;
            set => SetProperty(ref buttonPressedOpacity, value);
        }

        private int labelFontSize;

        public int LabelFontSize
        {
            get => labelFontSize;
            set => SetProperty(ref labelFontSize, value);
        }

        private System.Windows.Media.FontFamily labelFontFamily;
        public System.Windows.Media.FontFamily LabelFontFamily
        {
            get => labelFontFamily;
            set => SetProperty(ref labelFontFamily, value);
        }

        private SolidColorBrush labelForeground;

        public SolidColorBrush LabelForeground
        {
            get => labelForeground;
            set => SetProperty(ref labelForeground, value);
        }

        private System.Windows.Media.Color ColorStrToColor(string colorStr) =>
            (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorStr);
        #endregion

        #region Binding Window
        // 窗体背景刷
        private SolidColorBrush globalBackGround;
        public SolidColorBrush GlobalBackground
        {
            get => globalBackGround;
            set => SetProperty(ref globalBackGround, value);
        }

        // 是否显示在最前
        private bool topMost;
        public bool TopMost
        {
            get => topMost;
            set => SetProperty(ref topMost, value);
        }

        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private void InitBindingWindow() 
        {
        }
        #endregion

        #region Binding CloseBtn
        // 关闭图标
        private ImageBrush closeIcon;
        public ImageBrush CloseIcon
        {
            get => closeIcon;
            set => SetProperty(ref closeIcon, value);
        }

        public ICommand CloseCmd { get; set; }

        private void InitBindingCloseBtn()
        {
            // CloseIcon = GetBitmapBaseOnTheme(AppResource.CloseIcon);
            CloseCmd = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Global("Close"));
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Create(nameof(TomatoClockViewModel), nameof(TomatoClockView), "CloseWindow"));
            });
        }
        #endregion

        #region Binding MinimizeBtn
        // 最小化图标
        private ImageBrush minimizeIcon;
        public ImageBrush MinimizeIcon
        {
            get => minimizeIcon;
            set => SetProperty(ref minimizeIcon, value);
        }

        public ICommand MinimizeCmd { get; set; }

        private void InitBindingMinimizeBtn()
        {
            // MinimizeIcon = GetBitmapBaseOnTheme(AppResource.MinimizeIcon);
            MinimizeCmd = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Create(nameof(TomatoClockViewModel), nameof(TomatoClockView), "MinimizeWindow"));
            });
        }
        #endregion

        #region Binding TopMostBtn
        // 是否置顶的图标
        private ImageBrush topMostIcon;
        public ImageBrush TopMostIcon
        {
            get => topMostIcon;
            set => SetProperty(ref topMostIcon, value);
        }

        // 置顶按钮单击的绑定事件
        public ICommand TopMostCmd { get; set; }

        private void InitBindingTopMostBtn()
        {
            // TopMostIcon = GetBitmapBaseOnTheme(AppResource.UnpinIcon);
            TopMostCmd = new RelayCommand(() =>
            {
                TopMost = !TopMost;
                TopMostIcon = TopMost ? GetBitmapBaseOnTheme(AppResource.PinIcon) : GetBitmapBaseOnTheme(AppResource.UnpinIcon);
            });
        }
        #endregion

        #region Binding ChangeThemeBtn
        // 主题简称的标记
        private string themeTag;
        public string ThemeTag
        {
            get => themeTag;
            set => SetProperty(ref themeTag, value);
        }

        public ICommand ChangeThemeCmd { get; set; }

        private void InitBindingChangeThemeBtn()
        {
            ApplyTheme(Ioc.Default.GetService<ITomatoClockThemeService>().CurrentTheme);
            ChangeThemeCmd = new RelayCommand(
                () =>
                {
                    ApplyTheme(Ioc.Default.GetService<ITomatoClockThemeService>().Next());
                });
        }
        #endregion

        #region Binding StartBtn
        // 开始图标
        private ImageBrush startIcon;

        public ImageBrush StartIcon
        {
            get => startIcon;
            set => SetProperty(ref startIcon, value);
        }

        public ICommand StartCmd { get; set; }

        private void InitBindingStartBtn()
        {
            // StartIcon = GetBitmapBaseOnTheme(AppResource.StartIcon);
            StartCmd = new RelayCommand(
                () =>
                {
                    Ioc.Default.GetService<ICountdownService>().Start();
                });
        }
        #endregion

        #region Binding PauseBtn
        // 暂停图标
        private ImageBrush pauseIcon;
        public ImageBrush PauseIcon
        {
            get => pauseIcon;
            set => SetProperty(ref pauseIcon, value);
        }

        public ICommand PauseCmd { get; set; }

        private void InitBindingPauseBtn()
        {
            // PauseIcon = GetBitmapBaseOnTheme(AppResource.PauseIcon);
            PauseCmd = new RelayCommand(
                () =>
                {
                    Ioc.Default.GetService<ICountdownService>().Pause();
                });
        }
        #endregion

        #region Binding ResetBtn
        // 重置图标
        private ImageBrush resetIcon;
        public ImageBrush ResetIcon
        {
            get => resetIcon;
            set => SetProperty(ref resetIcon, value);
        }

        public ICommand ResetCmd { get; set; }

        private void InitBindingResetBtn()
        {
            // ResetIcon = GetBitmapBaseOnTheme(AppResource.ResetIcon);
            ResetCmd = new RelayCommand(() =>
            {
                Ioc.Default.GetService<ICountdownService>().Reset();
            });
        }
        #endregion

        #region Binding PreBtn
        public ImageBrush preIcon;
        public ImageBrush PreIcon
        {
            get => preIcon;
            set => SetProperty(ref preIcon, value);
        }

        public ICommand PreCmd { get; set; }

        private void InitBindingPreBtn()
        {
            // PreIcon = GetBitmapBaseOnTheme(AppResource.PreIcon);
            PreCmd = new RelayCommand(() =>
            {
                Ioc.Default.GetService<ICountdownService>().Pre();
            });
        }
        #endregion

        #region Binding NextBtn
        // 下一个图标
        public ImageBrush nextIcon;
        public ImageBrush NextIcon
        {
            get => nextIcon;
            set => SetProperty(ref nextIcon, value);
        }

        public ICommand NextCmd { get; set; }

        private void InitBindingNextBtn()
        {
            // NextIcon = GetBitmapBaseOnTheme(AppResource.NextIcon);
            NextCmd = new RelayCommand(() =>
            {
                Ioc.Default.GetService<ICountdownService>().Next();
            });
        }
        #endregion

        #region Binding FCListBtn
        private ImageBrush fcListIcon;

        public ImageBrush FCListIcon
        {
            get => fcListIcon;
            set => SetProperty(ref fcListIcon, value);
        }

        public ICommand FCListCmd { get; set; }

        private void InitBindingFCListBtn()
        {
            FCListIcon = GetBitmapBaseOnTheme(AppResource.FCListIcon);

            FCListCmd = new RelayCommand(() =>
            {
                // show finished-clock list view
            });
        }

        #endregion

        #region Binding TaskListBtn
        private ImageBrush taskListIcon;
        public ImageBrush TaskListIcon
        {
            get => taskListIcon;
            set => SetProperty(ref taskListIcon, value);
        }

        public ICommand TaskListCmd { get; set; }

        private Task selectedTask;

        public Task SelectedTask
        {
            get => selectedTask;
            set => SetProperty(ref selectedTask, value);
        }

        private void InitBindingTaskListBtn()
        {
            // TaskListIcon = GetBitmapBaseOnTheme(AppResource.TaskListIcon);
            TaskListCmd = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Create(nameof(TomatoClockViewModel), nameof(TodayToDoViewModel), "ShowTodayToDoWindow"));
            });

            // 今日任务选择的结果
            WeakReferenceMessenger.Default.Register<Task, string>(
                this,
                MsgToken.Create(nameof(TodayToDoViewModel), nameof(TomatoClockViewModel), "selectedTask"),
                (r, m) =>
                {
                    SelectedTask = m;
                    Title = m.Subject;
                });
        }
        #endregion

        #region Binding CountdownStr
        private string countdonwStr;

        public string CountdownStr
        {
            get => countdonwStr;
            set => SetProperty(ref countdonwStr, value);
        }

        private void InitBindingCountdownStr()
        {
            string countdownRemark = "Countdown Remark";
            string interruptionRemark = "Interruption Remark";

            CountdownStr = CountdownInfoToStr(Ioc.Default.GetService<ICountdownService>().CurrentInfo);

            // 倒计时，每一秒触发一次
            Ioc.Default.GetService<ICountdownService>().TickEvent += (t) =>
            {
                CountdownStr = CountdownInfoToStr(t);
            };

            // 倒计时结束触发
            Ioc.Default.GetService<ICountdownService>().ReachEndEvent += (t) =>
            {
                // 播放音乐，显示remark输入窗体，输入此次 倒计时 的remark
                Utils.PlaySound(App.UConfig.AlarmFile);
                WeakReferenceMessenger.Default.Send(
                    countdownRemark,
                    MsgToken.Create(nameof(TomatoClockViewModel), nameof(InputRemarkViewModel), "ShowInputRemarkWindow"));
            };

            // 倒计时被中断后恢复时触发
            Ioc.Default.GetService<ICountdownService>().RecoverFromInterruptEvent += () =>
            {
                // 显示remark输入窗体，输入此次 中断 的remark
                WeakReferenceMessenger.Default.Send(
                    interruptionRemark,
                    MsgToken.Create(nameof(TomatoClockViewModel), nameof(InputRemarkViewModel), "ShowInputRemarkWindow"));
            };

            // 注册消息响应函数接收 remark输入窗体 输入的倒计时remark信息
            WeakReferenceMessenger.Default.Register<string, string>(
                this,
                MsgToken.Create(nameof(InputRemarkViewModel), nameof(TomatoClockViewModel), string.Format("{0}\\{1}", countdownRemark, "Remark")),
                (r, m) =>
                {
                    CountdownInfo info = Ioc.Default.GetService<ICountdownService>().SetCountdownRemark(m);
                    // 写入数据库
                    ISimpleLogService service = Ioc.Default.GetService<ISimpleLogService>();
                    service.Context.TomatoClockLogs.Add(new TomatoClockLog()
                    {
                        StartTime = info.StartTime,
                        EndTime = info.FinishTime,
                        TotalSecond = info.TotalSecondRecord,
                        Remark = info.Remark,
                        SimpleTask = service.ConvertFromTask(SelectedTask ?? new Task())
                    });

                    service.Context.SaveChanges();
                    TomatoClockLog tcLog = service.Context.TomatoClockLogs.ToList().Last();
                    foreach(InterruptionInfo item in info.InterruptionInfos)
                    {
                        service.Context.InterruptionLogs.Add(new InterruptionLog()
                        {
                            StartTime = item.StartTime,
                            EndTime = item.FinishTime,
                            Remark = item.Remark,
                            TomatoClockLog = tcLog
                        });
                    }

                    service.Context.SaveChanges();
                });

            // 注册消息响应函数接收 remark输入窗体 输入的中断remark信息
            WeakReferenceMessenger.Default.Register<string, string>(
                this,
                MsgToken.Create(nameof(InputRemarkViewModel), nameof(TomatoClockViewModel), string.Format("{0}\\{1}", interruptionRemark, "Remark")),
                (r, m) =>
                {
                    var a = Ioc.Default.GetService<ICountdownService>().SetInterruptionRemark(m);
                });
        }
        #endregion

        #region Function
        private ImageBrush GetBitmapBaseOnTheme(Bitmap bitmap) => 
            new ImageBrush(
                Utils.BitmapToImageSource(
                    Utils.ReColorIcon(
                        bitmap,
                        ButtonStaticForeground.Color.R,
                        ButtonStaticForeground.Color.G,
                        ButtonStaticForeground.Color.B)));

        private string CountdownInfoToStr(CountdownInfo info)
        {
            return $"" +
                    $"{info.Minute.ToString().PadLeft(2, '0')}:" +
                    $"{info.Second.ToString().PadLeft(2, '0')}";
        }

        #endregion
    }
}
