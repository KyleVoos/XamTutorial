using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using XamTutorial.Models;
using XamTutorial.PageModels.Base;
using XamTutorial.Services.Statements;

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
        private List<PayStatement> _statements;
        public List<PayStatement> Statements
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
            Statements = await _statementsService.GetStatementHistoryAsync();

            await base.InitializeAsync(navigationData);
        }
    }
}
