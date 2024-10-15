using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
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

    }
}
