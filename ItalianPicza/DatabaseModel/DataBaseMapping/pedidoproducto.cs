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
    
    public partial class pedidoproducto
    {
        public int idPedidoProducto { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> idPedido { get; set; }
        public Nullable<int> idProducto { get; set; }
    
        public virtual pedido pedido { get; set; }
        public virtual producto producto { get; set; }
    }
}
