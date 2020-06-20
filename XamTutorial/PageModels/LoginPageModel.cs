using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamTutorial.PageModels.Base;
using XamTutorial.Services.Account;
using XamTutorial.Services.Navigation;

namespace XamTutorial.PageModels
{
    public class LoginPageModel : PageModelBase
    {
        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get => _loginCommand;
            set => SetProperty(ref _loginCommand, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private INavigationService _navigationService;
        private IAccountService _accountService;
        public LoginPageModel(INavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;

            // Initializes the login command
            LoginCommand = new Command(DoLoginAction);
        }

        /// <summary>
        /// Performs the login and navigation if everything is correct.
        /// there is no actual accounts being verified right now, its all just placeholder stuff.
        /// </summary>
        /// <param name="obj"></param>
        private async void DoLoginAction(object obj)
        {
            var loginAttempt = await _accountService.LoginAsync(Username, Password);

            if (loginAttempt)
            {
                // navigates to the Dashboard
                await _navigationService.NavigateToAsync<DashboardPageModel>();
            }
            else
            {
                // TODO: display an alert for failure 
            }
            
        }
    }
}
