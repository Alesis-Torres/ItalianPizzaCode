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
    
    public partial class recetaingrediente
    {
        public int idRecetaIngrediente { get; set; }
        public string cantidad { get; set; }
        public Nullable<int> idReceta { get; set; }
        public Nullable<int> idIngrediente { get; set; }
    
        public virtual ingrediente ingrediente { get; set; }
        public virtual receta receta { get; set; }
    }
}
