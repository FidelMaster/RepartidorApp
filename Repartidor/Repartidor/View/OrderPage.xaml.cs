
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Repartidor.Services;
namespace Repartidor.View

{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
  
        public OrderPage()
        {
             InitializeComponent();
          //  CambioCommand = new Command<string>(hello);

        }
        // async void go_test(object sender, EventArgs e) => await dialogService.ShowMessage("Error", "You must enter the user email.");

      
    }
}