using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class TransaccionesDAO
    {
        public TransaccionesDAO() { }

        public int RegistrarTransaccionEntrada(entradaextraordinaria entrada)
        {
            int resultado;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.entradaextraordinaria.Add(entrada);
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public int RegistrarTransaccionSalida(salidaextraordinaria salida)
        {
            int resultado;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.salidaextraordinaria.Add(salida);
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public cortecaja ObtenerCorteCajaActual()
        {
            cortecaja corteCajaActual = null;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    corteCajaActual = context.cortecaja
                        .OrderByDescending(c => c.idCorteCaja)  
                        .FirstOrDefault();  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el corte de caja actual: " + ex.Message);
            }

            return corteCajaActual;
        }

        public bool ActualizarBalanceCorteCajaEntrada(int idCorteCaja, decimal cantidadEntrada)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Buscar el corte de caja
                    var corteCaja = context.cortecaja
                        .FirstOrDefault(c => c.idCorteCaja == idCorteCaja);

                    if (corteCaja != null)
                    {
                        // Actualizar el balance (sumar la cantidad de la entrada)
                        corteCaja.balance += cantidadEntrada;

                        context.SaveChanges();  // Guardar los cambios en la base de datos
                        return true;  // Indicar que la actualización fue exitosa
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el corte de caja.");
                        return false;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public bool ActualizarBalanceCorteCajaSalida(int idCorteCaja, decimal cantidadSalida)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Buscar el corte de caja
                    var corteCaja = context.cortecaja
                        .FirstOrDefault(c => c.idCorteCaja == idCorteCaja);

                    if (corteCaja != null)
                    {
                        // Actualizar el balance (sumar la cantidad de la entrada)
                        corteCaja.balance -= cantidadSalida;

                        context.SaveChanges();  // Guardar los cambios en la base de datos
                        return true;  // Indicar que la actualización fue exitosa
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el corte de caja.");
                        return false;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public List<entradaextraordinaria> ObtenerEntradasExtraordinarias()
        {
            List<entradaextraordinaria> entradas = new List<entradaextraordinaria>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    entradas = context.entradaextraordinaria.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return entradas;
        }

        public List<salidaextraordinaria> ObtenerSalidasExtraordinarias()
        {
            List<salidaextraordinaria> salidas = new List<salidaextraordinaria>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    salidas = context.salidaextraordinaria.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return salidas;
        }

    }
}
