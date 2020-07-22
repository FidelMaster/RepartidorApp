using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Repartidor.Models;
using Repartidor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace Repartidor.ViewModels
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        private int cod_factura;
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        #region Properties
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Payment> Payments { get; set; }
        #endregion

        public OrderDetailViewModel(int cod_factura)
        {
            this.cod_factura = cod_factura;
            apiService =new  ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            Clients = new ObservableCollection<Client>();
            Products = new ObservableCollection<Product>();
            Payments = new ObservableCollection<Payment>();
            LoadMatches();
        }
        #region Singleton
        private static OrderDetailViewModel instance;

        public static OrderDetailViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private async void LoadMatches()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                
                return;
            }

            // Client
            var url = string.Format("https://api-test-momba.herokuapp.com/api/v1/pedidos/repartidor/detalle/{0}", cod_factura);
            var response = await apiService.Get<Client>(url);
            ReloadOrderDetails((List<Client>)response.Result);
            var url1 = string.Format("https://api-test-momba.herokuapp.com/api/v1/pedidos/repartidor/products/{0}", cod_factura);
            var response1 = await apiService.Get<Product>(url1);
            ReloadOrderProduct((List<Product>)response1.Result);
            var url2 = string.Format("https://api-test-momba.herokuapp.com/api/v1/pedidos/repartidor/payments/{0}", cod_factura);
            var response2 = await apiService.Get<Payment>(url2);
            ReloadOrderPayment((List<Payment>)response2.Result);
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                 return;
            }

       
        }

        private void ReloadOrderPayment(List<Payment> result)
        {
            Payments.Clear();
            foreach (var payment in result)
            {
                Payments.Add(new Payment
                {
                    cod_factura=payment.cod_factura,
                    subtotal=payment.subtotal,
                    tax=payment.tax,
                    total=payment.total,

                });
            }

        }

        private void ReloadOrderProduct(List<Product> result)
        {
            Products.Clear();

            foreach (var product in result)
            {
                Products.Add(new Product
                {
                    codproducto=product.codproducto,
                    cantidad=product.cantidad,
                    Talla=product.Talla,
                    Nombre = product.Nombre, 

                });
            }
        }

        private void ReloadOrderDetails(List<Client> result)
        {
            Clients.Clear();

            foreach (var client in result)
            {
                Clients.Add(new Client
                {
                    Nombre=client.Nombre,
                    Direccion=client.Direccion,
                    Celular= client.Celular,
                    Ciudad=client.Ciudad,
                    Telefono=client.Telefono,

                });
            }
        }



        #endregion


    }
}
