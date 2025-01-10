//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItalianPicza.DatabaseModel.DataBaseMapping
{
    using System;
    using System.Collections.Generic;
    
    public partial class pedido
    {
        public pedido()
        {
            this.pedidocliente = new HashSet<pedidocliente>();
            this.pedidoproducto = new HashSet<pedidoproducto>();
        }
    
        public int idPedido { get; set; }
        public string fecha { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public Nullable<int> idCorteCaja { get; set; }
        public Nullable<int> idEstadoPedido { get; set; }
        public Nullable<int> idDireccion { get; set; }
        public Nullable<int> idTelefono { get; set; }
    
        public virtual cortecaja cortecaja { get; set; }
        public virtual direccion direccion { get; set; }
        public virtual estadopedido estadopedido { get; set; }
        public virtual pedido pedido1 { get; set; }
        public virtual pedido pedido2 { get; set; }
        public virtual telefono telefono { get; set; }
        public virtual ICollection<pedidocliente> pedidocliente { get; set; }
        public virtual pedidolocal pedidolocal { get; set; }
        public virtual ICollection<pedidoproducto> pedidoproducto { get; set; }
    }
}
