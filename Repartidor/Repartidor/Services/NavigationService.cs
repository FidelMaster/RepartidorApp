using Repartidor.View;
using Repartidor.ViewModels;
using System.Threading.Tasks;
namespace Repartidor.Services
{
    public class NavigationService
    {
        #region Methods
        public NavigationService()
        {
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            switch (pageName)
            {
                case "OrderDetailPage":
                       await App.Navigator.PushAsync(new OrderDetailPage());

                    break;
                case "OrderPage":
                    mainViewModel.SelectOrder = new SelectOrderViewModel();
                    await App.Navigator.PushAsync(new OrderPage());
                    break;
                case "HistoryPage":
                  
                    await App.Navigator.PushAsync(new HistoryPage());
                    break;
                default:
                    break;
            }
        }




        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "MenuPage":
                    App.Current.MainPage = new MenuPage();
                    break;
                case "LoginPage":

                    App.Current.MainPage = new LoginPage();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
