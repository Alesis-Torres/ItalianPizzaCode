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
    
    public partial class cortecaja
    {
        public cortecaja()
        {
            this.baja = new HashSet<baja>();
            this.entradaextraordinaria = new HashSet<entradaextraordinaria>();
            this.pedido = new HashSet<pedido>();
            this.salidaextraordinaria = new HashSet<salidaextraordinaria>();
        }
    
        public int idCorteCaja { get; set; }
        public Nullable<decimal> balance { get; set; }
        public string fecha { get; set; }
        public Nullable<int> idEmpleado { get; set; }
    
        public virtual ICollection<baja> baja { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual ICollection<entradaextraordinaria> entradaextraordinaria { get; set; }
        public virtual ICollection<pedido> pedido { get; set; }
        public virtual ICollection<salidaextraordinaria> salidaextraordinaria { get; set; }
    }
}
