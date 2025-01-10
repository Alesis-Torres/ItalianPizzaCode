using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DTO
{
    internal class ProductoDTO
    {
        public string nombre { get; set; }
        public string lote { get; set; }
        public string caducidad { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public string tipo { get; set; }
        public string proveedor { get; set; }
        public string medida { get; set; }
        public int? codigo { get; set; }
    }
}
