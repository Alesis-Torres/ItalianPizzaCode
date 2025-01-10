using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class UsuariosDAO
    {

        public UsuariosDAO() { }

        public static int registrarCliente(cliente nuevoCliente)
        {
                int resultado = 0;
                try
                {
                    using (var context = new italianpizzaEntities())
                    {
                        context.cliente.Add(nuevoCliente);
                        resultado = context.SaveChanges();
                        resultado = nuevoCliente.idCliente;
                    }

                    return resultado;
                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
        }

        public usuario verificarExistenciaDeUsuario(usuario empleadoCuenta)
        {
            usuario empleadoVerificado = null;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Verificar si existe el usuario con el nombreUsuario y password especificados
                    empleadoVerificado = context.usuario
                        .FirstOrDefault(u => u.nombreUsuario == empleadoCuenta.nombreUsuario
                                          && u.password == empleadoCuenta.password);

                    /*if (usuarioEncontrado != null)
                    {
                        // Si el usuario existe, buscar el empleado relacionado
                        empleadoVerificado = context.empleado
                            .FirstOrDefault(e => e.idUsuario == usuarioEncontrado.idUsuario);
                    }*/
                }

            }catch(InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return empleadoVerificado;
        }

        public int DarDeAltaNuevoUsuario(empleado nuevoEmpleado, usuario nuevoUsuario)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.usuario.Add(nuevoUsuario);
                    resultado = context.SaveChanges(); 
                    nuevoEmpleado.idUsuario = nuevoUsuario.idUsuario;
                    context.empleado.Add(nuevoEmpleado);
                    resultado = context.SaveChanges(); 
                }

                return resultado; 
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        


        public static List<telefono> ObtenerNumerosTelefonicos()
        {
            List<telefono> listaTelefono = new List<telefono>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listatelefono = context.telefono.ToList();
                    listaTelefono = listatelefono;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaTelefono;
        }
        public static List<cliente> ObtenerClientes()
        {
            List<cliente> listaCliente = new List<cliente>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaNombres = context.cliente.ToList();
                    listaCliente = listaNombres;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaCliente;
        }
        public static List<direccion> obtenerDirecciones()
        {
            List<direccion> listaDireccion = new List<direccion>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaNombres = context.direccion.ToList();
                    listaDireccion = listaNombres;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaDireccion;
        }

        public static List<direccioncliente> obtenerRelacionDirecciones()
        {
            List<direccioncliente> listaDireccionCliente = new List<direccioncliente>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaNombres = context.direccioncliente.ToList();
                    listaDireccionCliente = listaNombres;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaDireccionCliente;
        }

        public static int registrarTelefono(string text)
        {
                int resultado = 0;
                telefono nuevoTelefono = new telefono
                {
                    numero = text
                };
                try
                {
                    using (var context = new italianpizzaEntities())
                    {
                        context.telefono.Add(nuevoTelefono);
                        context.SaveChanges();
                        resultado = nuevoTelefono.idTelefono;
                        
                    }

                    return resultado;
                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
            }       

            public static int registrarDomicilio(string text)
            {
                int resultado = 0;
                direccion nuevaDireccion = new direccion
                {
                    nombre = text
                };
                try
                {
                    using (var context = new italianpizzaEntities())
                    {
                        context.direccion.Add(nuevaDireccion);
                        context.SaveChanges();
                        resultado = nuevaDireccion.idDireccion;

                    }

                    return resultado;
                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
            }


        public static List<direccioncliente> ObtenerClienteDirecciones()
        {
            List<direccioncliente> listaDireccionCliente = new List<direccioncliente>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaDCliente = context.direccioncliente.ToList();
                    listaDireccionCliente = listaDCliente;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaDireccionCliente;
        }

        public static direccion obtenerDomicilioPedido(int idPedidoEdicion)
        {
            direccion direccionPedido = null;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // LINQ query to get a single cliente related to the given idPedido
                    direccionPedido = (from p in context.pedido // Assuming this is your junction table
                               where p.idPedido == idPedidoEdicion
                               join d in context.direccion on p.idDireccion equals d.idDireccion
                               select d).FirstOrDefault(); // Get the first matching cliente or null
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }

            return direccionPedido; // Return the cliente or null if not found
        }

        public static telefono obtenerNumeroPedido(int idPedidoEdicion)
        {
            telefono telefonoPedido = null;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // LINQ query to get a single cliente related to the given idPedido
                    telefonoPedido = (from p in context.pedido // Assuming this is your junction table
                                       where p.idPedido == idPedidoEdicion
                                       join t in context.telefono on p.idTelefono equals t.idTelefono
                                       select t).FirstOrDefault(); // Get the first matching cliente or null
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return telefonoPedido; // Return the cliente or null if not found
        }
        public static cliente ObtenerClientePorPedido(int idPedido)
        {
            cliente cliente = null;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // LINQ query to get a single cliente related to the given idPedido
                    cliente = (from pc in context.pedidocliente // Assuming this is your junction table
                               where pc.idPedido == idPedido
                               join c in context.cliente on pc.idCliente equals c.idCliente
                               select c).FirstOrDefault(); // Get the first matching cliente or null
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }

            return cliente; // Return the cliente or null if not found
        }
    }
}
