using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SegundoParcial.DAL;
using SegundoParcial.Entidades;

namespace SegundoParcial.BLL
{
    public class EntradaArticuloBLL {
        /// <summary>
        /// Permite guardar una entidad en la base de datos
        /// </summary>
        /// <param name="articulos">Una instancia de Articulos</param>
        /// <returns>Retorna True si guardo o Falso si falló </returns>
        public static bool Guardar(EntradaArticulo entradaArticulo)
        {
            bool paso = false;
            
            //Creamos una instancia del contexto para poder conectar con la BD
            Contexto contexto = new Contexto();
            Repositorio<Articulo> articulo = new Repositorio<Articulo>(new Contexto());

            try
            {
                if (contexto.EntradaArticulos.Add(entradaArticulo) != null)
                {
                    foreach (var item in articulo.GetList(x => x.Descripcion == entradaArticulo.Articulo))
                    {
                        contexto.Articulos.Find(item.ArticuloId).Inventario += entradaArticulo.Cantidad;
                    }

                    contexto.SaveChanges();
                    paso = true;
                }
                contexto.Dispose();//siempre hay que cerrar la conexion
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        /// <summary>
        /// Permite Modificar una entidad en la base de datos 
        /// </summary>
        /// <param name="articulos">Una instancia de Articulos</param>
        /// <returns>Retorna True si Modifico o Falso si falló </returns>
        public static bool Modificar(EntradaArticulo entradaArticulo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                EntradaArticulo EntradaAnterior = BLL.EntradaArticuloBLL.Buscar(entradaArticulo.EntradaId);

                int diferencia;

                diferencia = entradaArticulo.Cantidad - EntradaAnterior.Cantidad;

                Articulo articulo = BLL.ArticulosBLL.Buscar(EntradaArticulos.ArticuloId);
                articulo.Inventario += diferencia;
                BLL.ArticulosBLL.Modificar(articulo);
                contexto.Entry(entradaArticulo).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Eliminar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la persona que se desea eliminar </param>
        /// <returns>Retorna True si Eliminó o Falso si falló </returns>
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                EntradaArticulo entradaArticulo = contexto.EntradaArticulos.Find(id);

                contexto.EntradaArticulos.Remove(entradaArticulo);

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Buscar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la persona que se desea encontrar </param>
        /// <returns>Retorna la persona encontrada </returns>
        public static EntradaArticulo Buscar(int id)
        {
            Contexto contexto = new Contexto();
            EntradaArticulo entradaArticulo = new EntradaArticulo();
            try
            {
                entradaArticulo = contexto.EntradaArticulos.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return entradaArticulo;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        /// </summary> 
        ///<param name="expression">Expression Lambda conteniendo los filtros de busqueda </param>
        /// <returns>Retorna una lista de personas</returns>
        public static List<EntradaArticulo> GetList(Expression<Func<EntradaArticulo, bool>> expression)
        {
            List<EntradaArticulo> entradaArticulo = new List<EntradaArticulo>();
            Contexto contexto = new Contexto();
            try
            {
                entradaArticulo = contexto.EntradaArticulos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return entradaArticulo;
        }
    }
}

