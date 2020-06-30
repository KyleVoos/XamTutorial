using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using XamTutorial.Models;
using XamTutorial.PageModels.Base;
using XamTutorial.Services.Statements;
using XamTutorial.ViewModels;
using System.Linq;

namespace XamTutorial.PageModels
{
    public class SummaryPageModel : PageModelBase
    {
        private string _currentPayDateRange;
        public string CurrentPayDateRange 
        { 
            get => _currentPayDateRange; 
            set => SetProperty(ref _currentPayDateRange, value); 
        }
        private double _currentPeriodEarnings;
        public double CurrentPeriodEarnings
        {
            get => _currentPeriodEarnings;
            set => SetProperty(ref _currentPeriodEarnings, value);
        }
        private DateTime _currentPeriodPayDate;
        public DateTime CurrentPeriodPayDate
        {
            get => _currentPeriodPayDate;
            set => SetProperty(ref _currentPeriodPayDate, value);
        }
        private List<PayStatementViewModel> _statements;
        public List<PayStatementViewModel> Statements
        {
            get => _statements;
            set => SetProperty(ref _statements, value);
        }

        private IStatementsService _statementsService;
        public SummaryPageModel(IStatementsService statementsService)
        {
            _statementsService = statementsService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var statements = await _statementsService.GetStatementHistoryAsync();
            if (statements != null)
            {
                Statements = statements.Select(s => new PayStatementViewModel(s)).ToList();
                var lastStatement = statements.LastOrDefault();
                if (lastStatement != null)
                {
                    var today = DateTime.Now;
                    var max = 100;
                    var currentCount = 0;
                    var currentEnd = lastStatement.End;
                    while (currentEnd < today && currentCount < max)
                    {
                        currentEnd = currentEnd.AddDays(14);
                        ++currentCount;
                    }
                    if (currentEnd > today)
                    {
                        if (currentEnd.AddDays(-13) < today)
                        {
                            SetDateRange(currentEnd.AddDays(-13), currentEnd);
                        }
                    }
                }
            }
            await base.InitializeAsync(navigationData);
        }

        private void SetDateRange(DateTime start, DateTime end)
        {
            CurrentPayDateRange = start.ToString("MMMM d") + " - " + end.ToString("MMMM d, yyyy");
            CurrentPeriodPayDate = end.AddDays(6);
        }
    }
}
