//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItalianPicza.DatabaseModel.DataBaseMapping
{
    using System;
    using System.Collections.Generic;
    
    public partial class pedidoproveedorproducto
    {
        public int idPedidoProveedorProducto { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> idPedidoProveedor { get; set; }
        public Nullable<int> idProducto { get; set; }
    
        public virtual pedidoproveedor pedidoproveedor { get; set; }
        public virtual producto producto { get; set; }
    }
}
