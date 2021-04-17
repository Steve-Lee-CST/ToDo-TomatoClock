using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.ViewModels;

namespace ToDoTomatoClock.Views
{
    /// <summary>
    /// TodayToDoView.xaml 的交互逻辑
    /// </summary>
    public partial class TodayToDoView : Window
    {
        public TodayToDoView()
        {
            InitializeComponent();
            RegisteMsg();
            this.Loaded += MiniWindow_Loaded;
        }

        private void MiniWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DisappearFromAltTab();
        }

        private void RegisteMsg()
        {
            WeakReferenceMessenger.Default.Register<object, MsgToken>(
                this,
                MsgToken.Create(nameof(TodayToDoViewModel), nameof(TodayToDoView), "HideWindow"),
                (r, m) => {
                    this.Hide();
                });
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
            {
                this.DragMove();
            }
        }
    }
}
