//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItalianPicza.DatabaseModel.DataBaseMapping
{
    using System;
    using System.Collections.Generic;
    
    public partial class ingrediente
    {
        public ingrediente()
        {
            this.baja = new HashSet<baja>();
            this.pedidoproveedoringrediente = new HashSet<pedidoproveedoringrediente>();
            this.recetaingrediente = new HashSet<recetaingrediente>();
        }
    
        public int idIngrediente { get; set; }
        public string caducidad { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> codigo { get; set; }
        public string lote { get; set; }
        public string medida { get; set; }
        public string nombre { get; set; }
        public byte[] imagen { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public Nullable<int> idInventario { get; set; }
    
        public virtual ICollection<baja> baja { get; set; }
        public virtual inventario inventario { get; set; }
        public virtual proveedor proveedor { get; set; }
        public virtual ICollection<pedidoproveedoringrediente> pedidoproveedoringrediente { get; set; }
        public virtual ICollection<recetaingrediente> recetaingrediente { get; set; }
    }
}