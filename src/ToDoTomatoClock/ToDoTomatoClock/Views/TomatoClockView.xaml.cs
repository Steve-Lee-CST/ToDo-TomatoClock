using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.ViewModels;

namespace ToDoTomatoClock.Views
{
    /// <summary>
    /// TomatoClockView.xaml 的交互逻辑
    /// </summary>
    public partial class TomatoClockView : Window
    {
        public TomatoClockView()
        {
            InitializeComponent();

            InitNotifyIcon();
            RegisteMsg();
            this.Loaded += TomatoClockView_Loaded;
            this.Closed += TomatoClockView_Closed;
        }

        private void TomatoClockView_Closed(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        private void TomatoClockView_Loaded(object sender, RoutedEventArgs e)
        {
            DisappearFromAltTab();
        }

        private void RegisteMsg()
        {
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(TomatoClockViewModel), nameof(TomatoClockView), "MinimizeWindow"),
                (r, m) => {
                    this.Hide();
                });

            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(TomatoClockViewModel), nameof(TomatoClockView), "CloseWindow"),
                (r, m) => {
                    this.Close();
                });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
            {
                this.DragMove();
            }
        }

        #region disappear from Alt + Tab
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_EX_STYLE = -20;
        private const int WS_EX_APPWINDOW = 0x00040000, WS_EX_TOOLWINDOW = 0x00000080;
        private void DisappearFromAltTab()
        {
            //Variable to hold the handle for the form
            var helper = new WindowInteropHelper(this).Handle;
            //Performing some magic to hide the form from Alt+Tab
            SetWindowLong(helper, GWL_EX_STYLE, (GetWindowLong(helper, GWL_EX_STYLE) | WS_EX_TOOLWINDOW) & ~WS_EX_APPWINDOW);
        }
        #endregion

        #region Notify
        private NotifyIcon notifyIcon;
        private void InitNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = AppResource.TomatoClockIcon,
                Text = "ToDoTomatoClock",
                Visible = true
            };
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //鼠标左键，实现窗体最小化隐藏或显示窗体
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                    //解决最小化到任务栏可以强行关闭程序的问题。
                    this.ShowInTaskbar = false;//使Form不在任务栏上显示
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    //解决最小化到任务栏可以强行关闭程序的问题。
                    this.ShowInTaskbar = false;//使Form不在任务栏上显示
                    this.Activate();
                }
            }
        }
        #endregion
    }
}
