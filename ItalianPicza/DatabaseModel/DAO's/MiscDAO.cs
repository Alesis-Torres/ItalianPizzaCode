using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class MiscDAO
    {
        public static List<mesa> ObtenerMesas()
        {
            List<mesa> listaMesas = new List<mesa>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaMesa = context.mesa.ToList();
                    listaMesas = listaMesa;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaMesas;
        }

        public static mesa ObtenerMesaPedido(int idPedido)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var mesa = (from p in context.pedidolocal
                                join m in context.mesa on p.idMesa equals m.idMesa
                                where p.idPedidoLocal == idPedido
                                select m).FirstOrDefault();

                    return mesa;
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int agregarBaja(int idProducto, string descripcion,string fecha, int idCorteCaja, int Empleado)
        {
            int resultado = 0;
            baja bajaNueva = new baja
            {
                idProducto = idProducto,
                descripcion = descripcion,
                fecha = fecha,
                idCorteCaja = idCorteCaja,
                idEmpleado = Empleado
            };      
            try
                {
                    using (var context = new italianpizzaEntities())
                    {
                        context.baja.Add(bajaNueva);
                        context.SaveChanges();
                       
                    }

                    return resultado;
                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                return -1;
                }
            }
        }
    }

