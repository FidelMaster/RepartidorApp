using System;
 
using SQLite;

namespace Repartidor.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Nombre { get; set; }


        public DateTime FechaNacimiento { get; set; }


        public string Vehiculo { get; set; }


        public string Placa { get; set; }

        public string Color { get; set; }

        public string Message { get; set; }

        
        

    }
}
