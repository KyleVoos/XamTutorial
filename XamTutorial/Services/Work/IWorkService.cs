using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamTutorial.Models;

namespace XamTutorial.Services.Work
{
    public interface IWorkService
    {
        Task<bool> LogWorkAsynv(WorkItem item);
        Task<ObservableCollection<WorkItem>> GetTodaysWorkAsync();
    }
}
