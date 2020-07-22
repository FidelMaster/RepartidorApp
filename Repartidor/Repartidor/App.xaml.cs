using Repartidor.Data;
using Repartidor.Interfaces;
using Repartidor.Models;
using Repartidor.Services;
using Repartidor.View;
using SQLite;
using Xamarin.Forms;

namespace Repartidor
{
    public partial class App : Application
    {

        #region Attributes
        static DataAccess database;
        #endregion
        public SQLiteConnection conn;

        #region Properties
        public static NavigationPage Navigator { get; internal set; }

        public static MenuPage Master { get; internal set; }
        #endregion


        public App()
        {
            InitializeComponent();

            conn = DependencyService.Get<IConfig>().GetConnection();
            conn.CreateTable<User>();
            MainPage = new LoginPage();
        }

        public static DataAccess Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataAccess();
                }
                return database;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
