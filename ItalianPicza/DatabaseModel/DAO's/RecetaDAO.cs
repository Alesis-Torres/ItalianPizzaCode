using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;


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
                                    Nombre = ing.nombre,   
                                    Cantidad = ri.cantidad 
                                };

                    foreach (var item in query)
                    {
                        ingredientes.Add(new ingrediente
                        {
                            nombre = item.Nombre,
                            cantidad = item.Cantidad
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
                            cantidad = ingrediente.cantidad 
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

    }
}
