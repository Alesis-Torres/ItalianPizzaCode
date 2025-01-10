using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class DomicilioDAO
    {
        public static int registrarDomicilio(direccion nuevaDireccion)
        {
            int resultado = 0;
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.direccion.Add(nuevaDireccion);
                    resultado = context.SaveChanges();
                    resultado = nuevaDireccion.idDireccion;
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }
    }
}
