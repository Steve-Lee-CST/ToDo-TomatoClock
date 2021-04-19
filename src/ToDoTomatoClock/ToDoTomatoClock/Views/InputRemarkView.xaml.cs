using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoTomatoClock.Tools;
using ToDoTomatoClock.ViewModels;

namespace ToDoTomatoClock.Views
{
    /// <summary>
    /// InputRemarkView.xaml 的交互逻辑
    /// </summary>
    public partial class InputRemarkView : Window
    {
        public InputRemarkView()
        {
            InitializeComponent();
            RegisteMsg();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
            {
                this.DragMove();
            }
        }

        private void RegisteMsg()
        {
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "HideWindow"),
                (r, m) => {
                    this.Hide();
                });
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "ShowWindow"),
                (r, m) => {
                    if (!this.IsActive)
                        this.Hide();
                    this.ShowDialog();
                });
            WeakReferenceMessenger.Default.Register<object, string>(
                this,
                MsgToken.Create(nameof(InputRemarkViewModel), nameof(InputRemarkView), "CloseWindow"),
                (r, m) => {
                    this.Close();
                });
        }
    }
}
