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
    public class TalleresBLL
    {
        public static bool Guardar(Taller taller)
        {
            bool paso = false;
            //Creamos una instancia del contexto para poder conectar con la BD
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Tallers.Add(taller) != null)
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
        public static bool Modificar(Taller taller)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(taller).State = EntityState.Modified;
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
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Taller taller = contexto.Tallers.Find(id);
                if (taller != null)
                {
                    contexto.Entry(taller).State = EntityState.Deleted;
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
        public static Taller Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Taller taller = new Taller();
            try
            {
                taller = contexto.Tallers.Find();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return taller;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        public static List<Taller> GetList(Expression<Func<Taller, bool>> expression)
        {
            List<Taller> taller = new List<Taller>();
            try
            {
                Contexto contexto = new Contexto();
                taller = contexto.Tallers.Where(expression).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return taller ;
        }

    }
}

