using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class InsumosDAO
    {
        public InsumosDAO() { }

        public List<ingrediente> ObtenerIngredientes()
        {
            List<ingrediente> ingredientes = new List<ingrediente>();

            try
            {

                using (var context = new italianpizzaEntities())
                {
                    ingredientes = context.ingrediente.ToList();
                }

            }
            catch(EntityException ex) 
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return ingredientes;
        }
    }
}
