using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MSToDoDB.Modules;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Models;
using ToDoTomatoClock.Services.ThemeController;
using ToDoTomatoClock.Services.ToDoDBMonitor;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.Views;

namespace ToDoTomatoClock.ViewModels
{
    public class TodayToDoViewModel : ObservableObject
    {
        public TodayToDoViewModel()
        {
            InitBinding();
        }

        private void InitBinding()
        {
            InitBindingWindow();

            InitBindingCloseBtn();
            InitBindingTopMostBtn();
            InitBindingTodayTaskLB();

            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(TomatoClockViewModel), nameof(TodayToDoViewModel), "ShowTodayToDoWindow"),
                (r, m) =>
                {
                    ApplyTheme(Ioc.Default.GetService<ITomatoClockThemeService>().CurrentTheme);
                    WeakReferenceMessenger.Default.Send(
                        new object(),
                        MsgToken.Create(nameof(TodayToDoViewModel), nameof(TodayToDoView), "ShowWindow"));
                });
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Global("Close"),
                (r, m) => {
                    WeakReferenceMessenger.Default.Send(
                        new object(),
                        MsgToken.Create(nameof(TodayToDoViewModel), nameof(TodayToDoView), "CloseWindow"));
                });
        }

        #region Binding TomatoClockTheme
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

        // 更改主题
        private void ApplyTheme(TomatoClockTheme theme)
        {
            GlobalBackground = new SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(theme.GlobalBackground));
            ButtonStaticForeground = new SolidColorBrush(ColorStrToColor(theme.Button.StaticForeground));
            ButtonMouseOverForeground = new SolidColorBrush(ColorStrToColor(theme.Button.MouseOverForeground));
            ButtonPressedForeground = new SolidColorBrush(ColorStrToColor(theme.Button.PressedForeground));
            ButtonStaticOpacity = ColorStrToColor(theme.Button.StaticForeground).A / 255.0;
            ButtonMouseOverOpacity = ColorStrToColor(theme.Button.MouseOverForeground).A / 255.0;
            ButtonPressedOpacity = ColorStrToColor(theme.Button.PressedForeground).A / 255.0;
            CloseIcon = GetBitmapBaseOnTheme(AppResource.CloseIcon);
            TopMostIcon = GetBitmapBaseOnTheme(AppResource.UnpinIcon);
            LabelFontFamily = new System.Windows.Media.FontFamily(theme.Label.FontFamily);
            LabelFontSize = theme.Label.FontSize;
            LabelForeground = new SolidColorBrush(ColorStrToColor(theme.Label.Foreground));
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

        private void InitBindingWindow() { }
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
                WeakReferenceMessenger.Default.Send<object, string>(
                    new object(),
                    MsgToken.Create(nameof(TodayToDoViewModel), nameof(TodayToDoView), "HideWindow"));
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

        #region Binding TodayTaskLB
        private List<Task> todayTasks;

        public List<Task> TodayTasks
        {
            get => todayTasks;
            set => SetProperty(ref todayTasks, value);
        }

        private Task selectedTask;

        public Task SelectedTask
        {
            get => selectedTask;
            set
            {
                SetProperty(ref selectedTask, value);
            }
        }

        public ICommand TodayTaskLBItemDBClickCmd { get; set; }

        private void InitBindingTodayTaskLB()
        {
            TodayTaskLBItemDBClickCmd = new RelayCommand<object>((_) =>
            {
                WeakReferenceMessenger.Default.Send(
                    SelectedTask,
                    MsgToken.Create(nameof(TodayToDoViewModel), nameof(TomatoClockViewModel), "selectedTask"));
            });
            Ioc.Default.GetService<ITodayTaskMonitorService>().DataChangeEvent += (d) =>
            {
                TodayTasks = d;
            };
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
