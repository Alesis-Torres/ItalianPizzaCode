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
    
    public partial class pedidoproveedor
    {
        public pedidoproveedor()
        {
            this.pedidoproveedoringrediente = new HashSet<pedidoproveedoringrediente>();
            this.pedidoproveedorproducto = new HashSet<pedidoproveedorproducto>();
        }
    
        public int idPedidoProveedor { get; set; }
        public string fecha { get; set; }
        public byte[] monto { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public Nullable<int> idEmpleado { get; set; }
    
        public virtual proveedor proveedor { get; set; }
        public virtual ICollection<pedidoproveedoringrediente> pedidoproveedoringrediente { get; set; }
        public virtual ICollection<pedidoproveedorproducto> pedidoproveedorproducto { get; set; }
    }
}