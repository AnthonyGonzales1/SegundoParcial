using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
using SegundoParcial.DAL;
using SegundoParcial.Entidades;

namespace SegundoParcial.BLL
{
    public class MantenimientoBLL
    {

        public static bool Guardar(Mantenimiento mantenimiento)
        {
            bool paso = false;
            Vehiculo vehiculo = new Vehiculo();
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Mantenimientos.Add(mantenimiento) != null)
                {

                    foreach (var item in mantenimiento.Detalle)
                    {
                        contexto.Articulos.Find(item.ArticuloId).Inventario -= item.Cantidad;
                    }


                    contexto.Vehiculos.Find(mantenimiento.Detalle).TotalMantenimiento += Convert.ToInt32(mantenimiento.Total);

                    contexto.SaveChanges();
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

        public static bool Modificar(Mantenimiento mantenimiento)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                foreach (var item in mantenimiento.Detalle)
                {
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    contexto.Entry(item).State = estado;
                }
                contexto.Entry(mantenimiento).State = EntityState.Modified;

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

        public static bool Eliminar(int id)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                Mantenimiento mantenimiento = contexto.Mantenimientos.Find(id);


                if (mantenimiento != null)
                {
                    foreach (var item in mantenimiento.Detalle)
                    {
                        contexto.Articulos.Find(item.ArticuloId).Inventario += item.Cantidad;

                    }

                    contexto.Vehiculos.Find(mantenimiento.Detalle).TotalMantenimiento -= Convert.ToInt32(mantenimiento.Total);

                    mantenimiento.Detalle.Count();
                    contexto.Mantenimientos.Remove(mantenimiento);
                    
                }
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

        public static Mantenimiento Buscar(int id)
        {

            Mantenimiento mantenimiento = new Mantenimiento();
            Contexto contexto = new Contexto();

            try
            {
                mantenimiento = contexto.Mantenimientos.Find(id);
                mantenimiento.Detalle.Count();

                foreach (var item in mantenimiento.Detalle)
                {
                    //string s = item.;
                }
                contexto.Dispose();
            }

            catch (Exception)
            {
                throw;
            }

            return mantenimiento;
        }

        public static List<Mantenimiento> GetList(Expression<Func<Mantenimiento, bool>> expression)
        {

            List<Mantenimiento> Mantenimientos = new List<Mantenimiento>();
            Contexto contexto = new Contexto();

            try
            {
                Mantenimientos = contexto.Mantenimientos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return Mantenimientos;
        }
        public static decimal CalcularImporte(decimal precio, int cantidad)
        {
            return Convert.ToDecimal(precio) * Convert.ToInt32(cantidad);
        }

        public static decimal CalcularItbis(decimal subtotal)
        {
            return Convert.ToDecimal(subtotal) * Convert.ToDecimal(0.18);
        }

        public static decimal Total(decimal subtotal, decimal itbis)
        {
            return Convert.ToDecimal(subtotal) + Convert.ToDecimal(itbis);
        }
    }
}
