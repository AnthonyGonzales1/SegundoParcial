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

            try
            {
                Contexto contexto = new Contexto();
                contexto.Articulos.Add(articulo);
                contexto.SaveChanges();

                paso = true;
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
            try
            {
                Contexto contexto = new Contexto();
                contexto.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();

                paso = true;
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
           
            try
            {
                Contexto contexto = new Contexto();
                articulo = contexto.Articulos.ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return articulo;
        }
        public static decimal Calcularganancia(decimal Costo, decimal Precio)
        {
            decimal Gane = Precio - Costo;

            return (Convert.ToDecimal(Gane) / Convert.ToDecimal(Costo)) * 100;

        }

        public static decimal Calcularprecio(decimal Costo, decimal Ganancia)
        {

            Ganancia /= 100;
            Ganancia *= Costo;
            return Convert.ToDecimal(Costo) + Convert.ToDecimal(Ganancia);
        }

        public static string Return(string nombre)
        {
            string descripcion = string.Empty;
            var lista = GetList(x => x.Descripcion.Equals(nombre));
            foreach (var item in lista)
            {
                descripcion = item.Descripcion;
            }

            return descripcion;
        }
        
    }
}
