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
    
    public partial class usuario
    {
        public usuario()
        {
            this.empleado = new HashSet<empleado>();
        }
    
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
    
        public virtual ICollection<empleado> empleado { get; set; }
    }
}
