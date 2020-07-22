using Plugin.Connectivity;
using Repartidor.Data;
using Repartidor.Interfaces;
using Repartidor.Models;
using Repartidor.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Repartidor.ViewModels
{



    public class SelectOrderViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        
        private DataAccess dataAccess;
        private DialogService dialogService;

      
        private NavigationService navigationService;
        //   private DataService dataService;
        public SQLiteConnection conn;

        #endregion

        #region propiedades
        public ObservableCollection<OrderItemViewModel> Orders { get; set; }
        #endregion
        #region Constructor
        public SelectOrderViewModel() {
             apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            //dataAccess = new DataAccess();
            conn = DependencyService.Get<IConfig>().GetConnection();

            Orders = new ObservableCollection<OrderItemViewModel>();
            LoadOrders();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private async void LoadOrders()
        {
         
         
           
            if (!CrossConnectivity.Current.IsConnected)
            {

                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }
            var details = (from x in conn.Table<User>() select x).ToList();
            var id = details[1].Id;

            var response = await apiService.Get<Order>(string.Format("https://api-test-momba.herokuapp.com/api/v1/pedidos/repartidor/{0}", id));
         
            ReloadOrders((List<Order>)response.Result);
         

        }

        private void ReloadOrders(List<Order> orders)
        {
           
            foreach (var order in orders)
            {
                Orders.Add(new OrderItemViewModel
                {
                    cod_pedido = order.cod_pedido,
                    cod_factura=order.cod_factura,
                    Nombre = order.Nombre,
                    CodigoPostal=order.CodigoPostal,
                    Direccion = order.Direccion,
                    Fecha=order.Fecha,
                    Total = order.Total,
                    
                });
            }
        }

        #endregion

    }


}