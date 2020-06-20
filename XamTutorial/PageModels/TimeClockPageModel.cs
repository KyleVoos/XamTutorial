using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamTutorial.Models;
using XamTutorial.PageModels.Base;
using XamTutorial.ViewModels.Buttons;

namespace XamTutorial.PageModels
{
    public class TimeClockPageModel : PageModelBase
    {
        bool _isClockedIn;
        public bool IsClockedIn
        {
            get => _isClockedIn;
            set => SetProperty(ref _isClockedIn, value);
        }

        TimeSpan _runningTotal;
        public TimeSpan RunningTotal
        {
            get => _runningTotal;
            set => SetProperty(ref _runningTotal, value);
        }

        DateTime _currentStartTime;
        public DateTime CurrentStartTime
        {
            get => _currentStartTime;
            set => SetProperty(ref _currentStartTime, value);
        }

        ObservableCollection<WorkItem> _workItems;
        public ObservableCollection<WorkItem> WorkItems
        {
            get => _workItems;
            set => SetProperty(ref _workItems, value);
        }

        double _todayEarnings;
        public double TodaysEarnings
        {
            get => _todayEarnings;
            set => SetProperty(ref _todayEarnings, value);
        }

        ButtonModel _clockInOutButtonModel;
        public ButtonModel ClockInOutButtonModel
        {
            get => _clockInOutButtonModel;
            set => SetProperty(ref _clockInOutButtonModel, value);
        }

        public TimeClockPageModel()
        {
            WorkItems = new ObservableCollection<WorkItem>();
            ClockInOutButtonModel = new ButtonModel("Clock In", OnClockInOutAction);
        }

        public override Task InitializeAsync(object navigationDate)
        {
            RunningTotal = new TimeSpan();
            return base.InitializeAsync(navigationDate);
        }

        private void OnClockInOutAction()
        {
            if (IsClockedIn)
            {
                ClockInOutButtonModel.Text = "Clock In";
                WorkItems.Insert(0, new WorkItem
                {
                    Start = CurrentStartTime,
                    End = DateTime.Now
                });
            }
            else
            {
                CurrentStartTime = DateTime.Now;
                ClockInOutButtonModel.Text = "Clock Out";
            }

            IsClockedIn = !IsClockedIn;
        }
    }
}
