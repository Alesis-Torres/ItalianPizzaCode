using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

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
            usuario usuarioVerificado = new usuario();
            empleado empleadoEncontrado =  new empleado();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Verificar si existe el usuario con el nombreUsuario y password especificados
                    usuarioVerificado = context.usuario
                        .FirstOrDefault(u => u.nombreUsuario == empleadoCuenta.nombreUsuario
                                          && u.password == empleadoCuenta.password);

                    if (usuarioVerificado != null)
                    {
                        // Si el usuario existe, buscar el empleado relacionado
                        empleadoEncontrado = context.empleado
                            .FirstOrDefault(e => e.idUsuario == usuarioVerificado.idUsuario);
                        VentanaPrincipal.empleado = empleadoEncontrado.idEmpleado;
                        Console.WriteLine(VentanaPrincipal.empleado);  
                    }
                }

            }
            catch (TargetException te) 
            {
                throw new TargetException("Error al colocar un target", te);
            }

            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return usuarioVerificado;
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
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al modificar el usuario y el empleado.", ex);
            }
        }

        public List<empleado> ObtenerEmpleados()
        {
            List<empleado> empleados = new List<empleado>();

            try
            {
                using (var context =new italianpizzaEntities())
                {
                    empleados = context.empleado.Include("usuario").ToList();
                }

            }catch(EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return empleados;
        }

        public List<empleado> ObtenerEmpleadosPorRol(int idRol)
        {
            using (var context = new italianpizzaEntities())
            {
                return context.empleado.Where(e => e.idRol == idRol).ToList();
            }
        }

        public int ModificarUsuario(int idUsuario, empleado empleadoModificado, usuario usuarioModificado)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    
                    var usuarioExistente = context.usuario.FirstOrDefault(u => u.idUsuario == idUsuario);
                    if (usuarioExistente == null)
                    {
                        throw new Exception("El usuario con el ID especificado no existe.");
                    }

                    var empleadoExistente = context.empleado.FirstOrDefault(e => e.idUsuario == idUsuario);

                    usuarioExistente.nombreUsuario = usuarioModificado.nombreUsuario;
                    
                    empleadoExistente.nombre = empleadoModificado.nombre;
                    empleadoExistente.apellidoPaterno = empleadoModificado.apellidoPaterno;
                    empleadoExistente.apellidoMaterno = empleadoModificado.apellidoMaterno;
                    empleadoExistente.telefono = empleadoModificado.telefono;
                    empleadoExistente.idRol = empleadoModificado.idRol;
                    empleadoExistente.imagen = empleadoModificado.imagen;
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al modificar el usuario y el empleado.", ex);
            }
        }

        public (empleado Empleado, string NombreUsuario) ObtenerInformacionEmpleado(int idUsuario)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var empleadoEncontrado = context.empleado
                                           .Include("usuario")
                                           .Where(e => e.idUsuario == idUsuario)
                                           .Select(e => new {
                                               Empleado = e,
                                               NombreUsuario = e.usuario.nombreUsuario
                                           })
                                           .FirstOrDefault();

                    if (empleadoEncontrado == null)
                    {
                        throw new Exception("El empleado con el ID de usuario especificado no existe.");
                    }

                    return (empleadoEncontrado.Empleado, empleadoEncontrado.NombreUsuario);
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener la información del empleado.", ex);
            }
        }

    }
}
