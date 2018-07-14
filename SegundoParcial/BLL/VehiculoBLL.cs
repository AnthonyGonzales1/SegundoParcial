using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SegundoParcial.Entidades;
using SegundoParcial.DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;


namespace SegundoParcial.BLL
{
    public class VehiculoBLL
    {

        /// <summary>
        /// Permite guardar una entidad en la base de datos
        /// </summary>
        /// <param name="articulos">Una instancia de Articulos</param>
        /// <returns>Retorna True si guardo o Falso si falló </returns>
        public static bool Guardar(Vehiculo vehiculo)
        {
            bool paso = false;
            //Creamos una instancia del contexto para poder conectar con la BD
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Vehiculos.Add(vehiculo) != null)
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
        public static bool Modificar(Vehiculo vehiculo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(vehiculo).State = EntityState.Modified;
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
                Vehiculo vehiculo = contexto.Vehiculos.Find(id);

                if (vehiculo != null)
                {
                    contexto.Entry(vehiculo).State = EntityState.Deleted;
                }

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                    contexto.Dispose();
                }
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
        public static Vehiculo Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Vehiculo vehiculo = new Vehiculo();
            try
            {
                vehiculo = contexto.Vehiculos.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return vehiculo;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        /// </summary> 
        ///<param name="expression">Expression Lambda conteniendo los filtros de busqueda </param>
        /// <returns>Retorna una lista de personas</returns>
        public static List<Vehiculo> GetList(Expression<Func<Vehiculo, bool>> expression)
        {
            List<Vehiculo> vehiculo = new List<Vehiculo>();
            try
            {
                Contexto contexto = new Contexto();
                vehiculo = contexto.Vehiculos.Where(expression).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return vehiculo;
        }

    }
}
