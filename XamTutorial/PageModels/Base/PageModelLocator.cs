﻿using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;
using Xamarin.Forms;
using XamTutorial.Pages;
using XamTutorial.Services.Account;
using XamTutorial.Services.Navigation;

namespace XamTutorial.PageModels.Base
{
    public class PageModelLocator
    {
        static TinyIoCContainer _container;
        static Dictionary<Type, Type> _viewLookup;

        static PageModelLocator()
        {
            _container = new TinyIoCContainer();
            _viewLookup = new Dictionary<Type, Type>();

            // Register pages and page models
            Register<DashboardPageModel, DashboardPage>();
            Register<LoginPageModel, LoginPage>();
            Register<ProfilePageModel, ProfilePage>();
            Register<SettingsPageModel, SettingsPage>();
            Register<SummaryPageModel, SummaryPage>();
            Register<TimeClockPageModel, TimeClockPage>();

            // Register services (services are registered as Singletons by default)
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IAccountService, AccountService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static Page CreatePageFor(Type pageModelType)
        {
            var pageType = _viewLookup[pageModelType];
            var page = (Page)Activator.CreateInstance(pageType);
            var pageModel = _container.Resolve(pageModelType);
            page.BindingContext = pageModel;

            return page;
        }

        static void Register<TPageModel, TPage>() where TPageModel : PageModelBase where TPage : Page
        {
            _viewLookup.Add(typeof(TPageModel), typeof(TPage));
            _container.Register<TPageModel>();
        }
    }
}
