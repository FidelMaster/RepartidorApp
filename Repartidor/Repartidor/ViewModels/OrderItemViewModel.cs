using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Repartidor.Clases;
using Repartidor.Models;
using Repartidor.Services;

namespace Repartidor.ViewModels
{
    public class OrderItemViewModel : Order
    {
        private NavigationService navigationService;
        public ICommand SelectOrderCommand { get { return new RelayCommand(SelectOrder); } }
       
        public OrderItemViewModel()
        {
            navigationService = new NavigationService();
         }



        private async void SelectOrder()
        {
                      
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.OrderDetail = new OrderDetailViewModel(cod_factura);
            
            await navigationService.Navigate("OrderDetailPage");
        }


    }
}
