using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Repartidor.Clases;
using Repartidor.Data;
using Repartidor.Interfaces;
using Repartidor.Models;
using Repartidor.Services;
using SQLite;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Repartidor.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private DataAccess dataAccess;

        private NavigationService navigationService;
        public SQLiteConnection conn;
        private string email;

        private string password;

        private bool isRunning;

        private bool isEnabled;

        private bool isRemembered;
        #endregion

        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            conn = DependencyService.Get<IConfig>().GetConnection();
            conn.CreateTable<User>();
            IsRemembered = true;
            IsEnabled = true;
            Email = null;
            Password = null;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter the user email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a password.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }


            var token = await apiService.GetToken(Email, Password);

            if (token == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "The user name or password in incorrect.");
                Password = null;
                return;
            }

            if (token.Id == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Error");
                Password = null;
                return;
            }
            var response = await apiService.GetUserByEmail(string.Format("https://api-test-momba.herokuapp.com/api/user/information/{0}",token.Id));

            var user = (User)response.Result;
            conn.Delete(user);
            conn.Insert(user);
     
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentUser(user);

            Email = null;
            Password = null;

            IsRunning = false;
            IsEnabled = true;
            navigationService.SetMainPage("MenuPage");


        }
        #endregion
    }
}



