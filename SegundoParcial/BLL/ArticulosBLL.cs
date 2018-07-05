using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SegundoParcial.Entidades;
using SegundoParcial.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace SegundoParcial.BLL
{
    public class ArticulosBLL
    {
        /// <summary>
        /// Permite guardar una entidad en la base de datos
        /// </summary>
        /// <param name="articulos">Una instancia de Articulos</param>
        /// <returns>Retorna True si guardo o Falso si falló </returns>
        public static bool Guardar(Articulo articulo)
        {
            bool paso = false;
            //Creamos una instancia del contexto para poder conectar con la BD
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Articulos.Add(articulo) != null)
                {
                    contexto.SaveChanges();//Guardar los cambios
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
        public static bool Modificar(Articulo articulo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(articulo).State = EntityState.Modified;
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
                Articulo articulo = contexto.Articulos.Find(id);

                contexto.Articulos.Remove(articulo);

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
        public static Articulo Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Articulo articulo = new Articulo();
            try
            {
                articulo = contexto.Articulos.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return articulo;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        /// </summary> 
        ///<param name="expression">Expression Lambda conteniendo los filtros de busqueda </param>
        /// <returns>Retorna una lista de personas</returns>
        public static List<Articulo> GetList(Expression<Func<Articulo, bool>> expression)
        {
            List<Articulo> articulo = new List<Articulo>();
            Contexto contexto = new Contexto();
            try
            {
                articulo = contexto.Articulos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return articulo;
        }

    }
}
