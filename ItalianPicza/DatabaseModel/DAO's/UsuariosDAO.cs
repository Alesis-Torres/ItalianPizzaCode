using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class UsuariosDAO
    {

        public UsuariosDAO() { }

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


            }catch(InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }


            return empleadoVerificado;
        }

    }
}
