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
    
    public partial class mesa
    {
        public mesa()
        {
            this.pedidolocal = new HashSet<pedidolocal>();
        }
    
        public int idMesa { get; set; }
        public Nullable<int> numero { get; set; }
    
        public virtual ICollection<pedidolocal> pedidolocal { get; set; }
    }
}
