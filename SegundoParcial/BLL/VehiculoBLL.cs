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

        public static List<Vehiculo> GetList(Expression<Func<Vehiculo, bool>> expression)
        {

            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Contexto contexto = new Contexto();

            try
            {

                vehiculos = contexto.Vehiculos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return vehiculos;
        }
    }
}
