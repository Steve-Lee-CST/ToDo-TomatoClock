using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using ToDoTomatoClock.Config;
using ToDoTomatoClock.Models;
using ToDoTomatoClock.Services.ThemeController;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.Views;

namespace ToDoTomatoClock.ViewModels
{
    public class InputRemarkViewModel : ObservableObject
    {
        public InputRemarkViewModel()
        {
            InitBinding();
        }

        private void InitBinding()
        {
            InitBindingWindow();
            InitBindingCloseBtn();
            InitBindingTitle();
            InitBindingRemark();
            InitBindingCancelBtn();
            InitBindingOKBtn();
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
            ButtonStaticForeground = new SolidColorBrush(ColorStrToColor(theme.Label.Foreground));
            ButtonMouseOverForeground = new SolidColorBrush(ColorStrToColor(theme.Button.MouseOverForeground));
            ButtonPressedForeground = new SolidColorBrush(ColorStrToColor(theme.Button.PressedForeground));
            ButtonStaticOpacity = ColorStrToColor(theme.Button.StaticForeground).A / 255.0;
            ButtonMouseOverOpacity = ColorStrToColor(theme.Button.MouseOverForeground).A / 255.0;
            ButtonPressedOpacity = ColorStrToColor(theme.Button.PressedForeground).A / 255.0;
            CloseIcon = GetBitmapBaseOnTheme(AppResource.CloseIcon);
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

        private void InitBindingWindow() 
        {
            WeakReferenceMessenger.Default.Register<string, string>(
                this,
                MsgToken.Create(nameof(TomatoClockViewModel), nameof(InputRemarkViewModel), "ShowInputRemarkWindow"),
                (r, m) =>
                {
                    ApplyTheme(Ioc.Default.GetService<ITomatoClockThemeService>().CurrentTheme);
                    Title = m;
                    WeakReferenceMessenger.Default.Send(
                        new object(),
                        MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "ShowWindow"));
                });
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Global("Close"),
                (r, m) => {
                    WeakReferenceMessenger.Default.Send(
                        new object(),
                        MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "CloseWindow"));
                });
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
                WeakReferenceMessenger.Default.Send<object, string>(
                    new object(),
                    MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "HideWindow"));
            });
        }
        #endregion

        #region Binding Title
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private void InitBindingTitle() { }
        #endregion

        #region Binding Remark
        private string remark;
        public string Remark
        {
            get => remark;
            set => SetProperty(ref remark, value);
        }

        private void InitBindingRemark()
        {

        }
        #endregion

        #region Binding CancelBtn
        public ICommand CancelCmd { get; set; }
        
        private void InitBindingCancelBtn()
        {
            CancelCmd = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(
                    string.Empty,
                    MsgToken.Create(nameof(InputRemarkViewModel), nameof(TomatoClockViewModel), string.Format("{0}\\{1}", Title, "Remark")));
                Title = string.Empty;
                Remark = string.Empty;
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "HideWindow"));
            });
        }
        #endregion

        #region Binding OKBtn
        public ICommand OKCmd { get; set; }
        private void InitBindingOKBtn()
        {
            OKCmd = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(
                    Remark,
                    MsgToken.Create(nameof(InputRemarkViewModel), nameof(TomatoClockViewModel), string.Format("{0}\\{1}", Title, "Remark")));
                Title = string.Empty;
                Remark = string.Empty;
                WeakReferenceMessenger.Default.Send(
                    new object(),
                    MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "HideWindow"));
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

        #endregion
    }
}
