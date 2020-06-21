using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamTutorial.Models;

namespace XamTutorial.Services.Statements
{
    public interface IStatementsService
    {
        Task<List<PayStatement>> GetStatementHistoryAsync();
    }
}
