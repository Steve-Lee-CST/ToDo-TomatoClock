using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<TomatoClockViewModel>();
            SimpleIoc.Default.Register<TodayToDoViewModel>();
        }

        public TomatoClockViewModel TomatoClockVM => SimpleIoc.Default.GetInstance<TomatoClockViewModel>();
        public TodayToDoViewModel TodayToDoVM => SimpleIoc.Default.GetInstance<TodayToDoViewModel>();
    }
}
