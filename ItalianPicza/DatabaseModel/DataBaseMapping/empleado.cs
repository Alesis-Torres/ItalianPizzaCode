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
    
    public partial class empleado
    {
        public empleado()
        {
            this.baja = new HashSet<baja>();
            this.cortecaja = new HashSet<cortecaja>();
            this.entradaextraordinaria = new HashSet<salidaextraordinaria>();
            this.salidaextraordinaria = new HashSet<salidaextraordinaria>();
        }
    
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string email { get; set; }
        public Nullable<bool> estado { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
        public Nullable<int> idRol { get; set; }
        public Nullable<int> idUsuario { get; set; }
    
        public virtual ICollection<baja> baja { get; set; }
        public virtual ICollection<cortecaja> cortecaja { get; set; }
        public virtual rol rol { get; set; }
        public virtual ICollection<salidaextraordinaria> entradaextraordinaria { get; set; }
        public virtual ICollection<salidaextraordinaria> salidaextraordinaria { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
