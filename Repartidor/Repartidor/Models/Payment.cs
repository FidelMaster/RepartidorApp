using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidor.Models
{
    public class Payment
    {
        public int cod_factura { get; set; }
        public int subtotal {get; set;}
        public int  tax {get; set;}
        public int total {get; set;}
    }
}
