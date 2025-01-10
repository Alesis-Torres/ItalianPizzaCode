using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;


namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class RecetaDAO
    {
        public RecetaDAO() { }

        public receta ObtenerRecetaProducto(int idReceta)
        {
            receta receta = new receta();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                   
                    receta = context.receta.FirstOrDefault(r => r.idReceta == idReceta);

                    if (receta == null)
                    {
                        return null;
                    }
                    else
                    {
                        return receta;
                    }
                }
            }catch(InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

        }

        public List<ingrediente> ObtenerIngredientesReceta(int idReceta)
        {
            List<ingrediente> ingredientes = new List<ingrediente>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var query = from ri in context.recetaingrediente
                                join ing in context.ingrediente on ri.idIngrediente equals ing.idIngrediente
                                where ri.idReceta == idReceta
                                select new
                                {
                                    IDIngrediente = ing.idIngrediente,
                                    Nombre = ing.nombre,
                                    Cantidad = ri.cantidad,
                                    Imagen = ing.imagen 
                                };

                    foreach (var item in query)
                    {
                        ingredientes.Add(new ingrediente
                        {
                            idIngrediente = item.IDIngrediente,
                            nombre = item.Nombre,
                            cantidadRegistrada = Int32.Parse(item.Cantidad)
                        });
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return ingredientes;
        }

        public int DarDeAltaReceta(string instrucciones, List<ingrediente> ingredientesReceta, producto producto)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    receta nuevaReceta = new receta
                    {
                        instrucciones = instrucciones
                    };

                    context.receta.Add(nuevaReceta);

                    resultado = context.SaveChanges();

                    int idRecetaCreada = nuevaReceta.idReceta;

                    producto.idReceta = idRecetaCreada;

                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;

                    foreach (var ingrediente in ingredientesReceta)
                    {
                        recetaingrediente recetaIngrediente = new recetaingrediente
                        {
                            idReceta = idRecetaCreada,   
                            idIngrediente = ingrediente.idIngrediente,  
                            cantidad = ingrediente.cantidadRegistrada.ToString() 
                        };

                        context.recetaingrediente.Add(recetaIngrediente);
                    }

                    resultado = context.SaveChanges();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la receta en la base de datos.", ex);
            }
        }

        public int ModificarReceta(int idReceta, string instrucciones, List<ingrediente> ingredientesReceta)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    receta recetaExistente = context.receta.FirstOrDefault(r => r.idReceta == idReceta);
                    if (recetaExistente == null)
                    {
                        throw new Exception($"La receta con id {idReceta} no existe en la base de datos.");
                    }

                    recetaExistente.instrucciones = instrucciones;

                    var ingredientesExistentes = context.recetaingrediente.Where(ri => ri.idReceta == idReceta).ToList();
                    context.recetaingrediente.RemoveRange(ingredientesExistentes);

                    foreach (var ingrediente in ingredientesReceta)
                    {

                        recetaingrediente nuevoIngrediente = new recetaingrediente
                        {
                            idReceta = idReceta,
                            idIngrediente = ingrediente.idIngrediente,
                            cantidad = ingrediente.cantidad.ToString()
                        };
                        context.recetaingrediente.Add(nuevoIngrediente);
                    }

                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la receta y sus ingredientes.", ex);
            }
        }

    }
}
