using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamTutorial.Models;

namespace XamTutorial.Services.Work
{
    public class MockWorkService : IWorkService
    {
        public List<WorkItem> Items { get; set; }

        public MockWorkService()
        {
            Items = new List<WorkItem>();
        }
        public Task<bool> LogWorkAsync(WorkItem item)
        {
            Items.Add(item);

            return Task.FromResult(true);
        }

        Task<ObservableCollection<WorkItem>> IWorkService.GetTodaysWorkAsync()
        {
            return Task.FromResult(new ObservableCollection<WorkItem>(Items));
        }
    }
}
