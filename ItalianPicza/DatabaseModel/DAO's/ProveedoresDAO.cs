using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Data;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class ProveedoresDAO
    {

        public ProveedoresDAO() { }

        public int DarDeAltaNuevoProveedor(proveedor nuevoProveedor)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.proveedor.Add(nuevoProveedor);
                    resultado = context.SaveChanges();
                    nuevoProveedor.idProveedor = nuevoProveedor.idProveedor;
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }
        /*
        public List<GUI_Proveedores.Proveedor> ObtenerListaProveedores()
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Obtener la lista de proveedores sin hacer la conversión de imagen en la consulta
                    var proveedores = context.proveedor.ToList();

                    // Convertir los datos después de haberlos recuperado
                    return proveedores.Select(p => new GUI_Proveedores.Proveedor
                    {
                        NombreProveedor = p.nombre,
                        DescripcionProveedor = p.descripcion,
                        ImagenProveedor = p.imagen != null ? ConvertirImagenAString(p.imagen) : "/Imagenes/Logos/default.png"
                    }).ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Error al recuperar la lista de proveedores.", ex);
            }
        }
        private string ConvertirImagenAString(byte[] imagenBytes)
        {
            if (imagenBytes != null && imagenBytes.Length > 0)
            {
                return "data:image/png;base64," + Convert.ToBase64String(imagenBytes);
            }
            return "/Imagenes/Logos/default.png"; // Ruta de la imagen por defecto si no hay imagen
        }
        */
    }
}
