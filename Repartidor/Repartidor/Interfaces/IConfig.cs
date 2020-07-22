using SQLite;
 
 
namespace Repartidor.Interfaces
{
    public interface IConfig
    {
        SQLiteConnection GetConnection();
    }

}
