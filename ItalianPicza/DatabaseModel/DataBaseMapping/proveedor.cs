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
    
    public partial class proveedor
    {
        public proveedor()
        {
            this.ingrediente = new HashSet<ingrediente>();
            this.pedidoproveedor = new HashSet<pedidoproveedor>();
            this.producto = new HashSet<producto>();
        }
    
        public int idProveedor { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
    
        public virtual ICollection<ingrediente> ingrediente { get; set; }
        public virtual ICollection<pedidoproveedor> pedidoproveedor { get; set; }
        public virtual ICollection<producto> producto { get; set; }
    }
}
