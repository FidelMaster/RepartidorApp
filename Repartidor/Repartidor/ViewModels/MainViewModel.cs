using GalaSoft.MvvmLight.Command;
using Repartidor.Clases;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Repartidor.Services;
using Xamarin.Forms;
using Repartidor.Models;

namespace Repartidor.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private User currentUser;
        private DialogService dialogService;


        #endregion


        public LoginViewModel Login { get; set; }

        public SelectOrderViewModel SelectOrder { get; set; }

        public OrderDetailViewModel OrderDetail { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public User CurrentUser
        {
            set
            {
                if (currentUser != value)
                {
                    currentUser = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
                }
            }
            get
            {
                return currentUser;
            }
        }



        #region Constructor
        public MainViewModel()
        {
            instance = this;
            dialogService = new DialogService();

          
            Login = new LoginViewModel();

            LoadMenu();


        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Singleton
        private static MainViewModel instance;
     
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region commands
         #endregion

        #region Methods
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();


            Menu.Add(new MenuItemViewModel
            {
                Icon = "home.png",
                PageName = "HomePage",
                Title = "Inicio",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "order.png",
                PageName = "OrderPage",
                Title = "Ordenes Pendientes",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "history.png",
                PageName = "HistoryPage",
                Title = "Historial De Entregas",
            });



        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        #endregion
    }
}
