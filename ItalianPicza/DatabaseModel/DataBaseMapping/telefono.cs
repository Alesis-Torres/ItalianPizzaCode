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
    
    public partial class telefono
    {
        public telefono()
        {
            this.cliente = new HashSet<cliente>();
            this.pedido = new HashSet<pedido>();
        }
    
        public int idTelefono { get; set; }
        public string numero { get; set; }
    
        public virtual ICollection<cliente> cliente { get; set; }
        public virtual ICollection<pedido> pedido { get; set; }
    }
}