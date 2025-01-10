using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public List<proveedor> ObtenerProveedores()
        {
            List<proveedor> proveedores = new List<proveedor>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    proveedores = context.proveedor.ToList();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return proveedores;
        }
        
        public proveedor ObtenerInformacionProveedor(int idProveedor)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var proveedorEncontrado = context.proveedor
                                           .Where(e => e.idProveedor == idProveedor)
                                           .FirstOrDefault();

                    if (proveedorEncontrado == null)
                    {
                        throw new Exception("El Proveedor con el ID de Proveedor especificado no existe.");
                    }

                    return proveedorEncontrado;
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener la información del proveedor.", ex);
            }
        }

        public int ModificarProveedor(int idProveedor, proveedor proveedorModificado)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {

                    var proveedorExistente = context.proveedor.FirstOrDefault(u => u.idProveedor == idProveedor);
                    if (proveedorExistente == null)
                    {
                        throw new Exception("El Proveedor con el ID especificado no existe.");
                    }

                    proveedorExistente.nombre = proveedorModificado.nombre;
                    proveedorExistente.telefono = proveedorModificado.telefono;
                    proveedorExistente.descripcion = proveedorModificado.descripcion;
                    proveedorExistente.imagen = proveedorModificado.imagen;
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
                throw new Exception("Ocurrió un error al modificar el proveedor.", ex);
            }
        }

    }
}
