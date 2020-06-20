using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamTutorial.PageModels.Base;

namespace XamTutorial.Services.Navigation
{
    public interface INavigationService
    {
        /// <summary>
        /// Navigation Method to push onto the Navigation Stack
        /// Changed from TPageModelBase to TPageModel, this makes it so that only
        /// pages which are of a type PageModelBase will be added to the navigation stack.
        /// </summary>
        /// <typeparam name="TPageModel"></typeparam>
        /// <param name="navigationData"></param>
        /// <param name="setRoot"></param>
        /// <returns></returns>
        Task NavigateToAsync<TPageModel>(object navigationData = null, bool setRoot = false)
            where TPageModel : PageModelBase;

        /// <summary>
        /// Navigation method to pop off of the Navigation Stack
        /// </summary>
        /// <returns></returns>
        Task GoBackAsync();
    }
}
