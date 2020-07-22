
using System;

namespace Repartidor.Models
{
    public class Order
    {
        public int cod_pedido { get; set; }

        public int cod_factura { get; set; }
        public string Nombre { get; set; }
     
        public string CodigoPostal { get; set; }

        public string Direccion { get; set; }

        public string Fecha { get; set; }

        public int Total { get; set; }
    }
}
