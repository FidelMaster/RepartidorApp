using GalaSoft.MvvmLight.Command;
using Repartidor.Services;
using System.Windows.Input;

namespace Repartidor.ViewModels
{
    public class MenuItemViewModel
    {
        #region Attributes
        private NavigationService navigationService;

        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Constructors
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private async void Navigate()
         {
            if (PageName == "LoginPage")
            {
                navigationService.SetMainPage(PageName);
            }
            else
            {
                await navigationService.Navigate(PageName);
            }

        }

        #endregion

    }
}
