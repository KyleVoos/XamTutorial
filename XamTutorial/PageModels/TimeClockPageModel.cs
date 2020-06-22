using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using XamTutorial.Models;
using XamTutorial.PageModels.Base;
using XamTutorial.Services.Account;
using XamTutorial.Services.Work;
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

        private Timer _timer;
        ObservableCollection<WorkItem> _workItems;
        private IAccountService _accountService;
        private IWorkService _workService;
        private double _hourlyRate;

        public TimeClockPageModel(IAccountService accountService, IWorkService workService)
        {
            _accountService = accountService;
            _workService = workService;
            ClockInOutButtonModel = new ButtonModel("Clock In", OnClockInOutAction);
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunningTotal += TimeSpan.FromSeconds(1);
        }

        public async override Task InitializeAsync(object navigationDate)
        {
            RunningTotal = new TimeSpan();
            _hourlyRate = await _accountService.GetCurrentPayRateAsync();
            WorkItems = await _workService.GetTodaysWorkAsync();
            await base.InitializeAsync(navigationDate);
        }

        private async void OnClockInOutAction()
        {
            if (IsClockedIn)
            {
                _timer.Enabled = false; // stops the timer
                TodaysEarnings += _hourlyRate * RunningTotal.TotalHours; // adds the earning from the current period to the total sum
                RunningTotal = TimeSpan.Zero;   // resets the RunningTotal back to zero
                ClockInOutButtonModel.Text = "Clock In";
                var item = new WorkItem    // adds a new work item to the beginning of the collection that is displayed
                {
                    Start = CurrentStartTime,
                    End = DateTime.Now
                };
                WorkItems.Insert(0, item);
                await _workService.LogWorkAsync(item);
            }
            else
            {
                CurrentStartTime = DateTime.Now;
                _timer.Enabled = true;
                ClockInOutButtonModel.Text = "Clock Out";
            }

            IsClockedIn = !IsClockedIn;
        }
    }
}
