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
                contexto.Mantenimientos.Add(mantenimiento);
                foreach (MantenimientoDetalle detalle in mantenimiento.Detalle)
                {
                    contexto.MantenimientoDetalles.Add(detalle);
                    Articulo articulo = ArticulosBLL.Buscar(detalle.ArticuloId);
                    articulo.Inventario += detalle.Cantidad;
                    ArticulosBLL.Modificar(articulo);
                }

                contexto.SaveChanges();
                contexto.Dispose();
                paso = true;
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
                var Mantenimiento = BLL.MantenimientoBLL.Buscar(mantenimiento.MantenimientoId);
                if (Mantenimiento != null)
                {
                    foreach (var item in Mantenimiento.Detalle)
                    {
                        contexto.Articulos.Find(item.ArticuloId).Inventario += item.Cantidad;
                        if (!mantenimiento.Detalle.ToList().Exists(v => v.Id == item.Id))
                        {
                            //  contexto.registrodeArticulos.Find(item.ArticulosId).Inventario -= item.Cantidad;

                            item.Articulos = null;
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    foreach (var item in mantenimiento.Detalle)
                    {
                        contexto.Articulos.Find(item.ArticuloId).Inventario -= item.Cantidad;
                        var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                        contexto.Entry(item).State = estado;
                    }

                    Mantenimiento EntradaAnterior = BLL.MantenimientoBLL.Buscar(Mantenimiento.MantenimientoId);
                    
                    decimal diferencia;

                    diferencia = mantenimiento.Total - EntradaAnterior.Total;
                    
                    Vehiculo vehiculo = BLL.VehiculoBLL.Buscar(mantenimiento.VehiculoId);
                    vehiculo.TotalMantenimiento += diferencia;
                    BLL.VehiculoBLL.Editar(vehiculo);

                    contexto.Entry(Mantenimiento).State = EntityState.Modified;
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
                    contexto.Vehiculos.Find(mantenimiento.VehiculoId).TotalMantenimiento -= mantenimiento.Total;

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
                if (mantenimiento != null)
                {
                    mantenimiento.Detalle.Count();

                    foreach (var item in mantenimiento.Detalle)
                    {

                        string art = item.Articulos.Descripcion;
                    }

                }
                contexto.Dispose();
            catch (Exception)
            {
                throw;
            }
            return mantenimiento;
        }

        public static List<Mantenimiento> GetList(Expression<Func<Mantenimiento, bool>> expression)
        {
            List<Mantenimiento> Mantenimientos = new List<Mantenimiento>();
            try
            {
                Contexto contexto = new Contexto();
                Mantenimientos = contexto.Mantenimientos.Where(expression).ToList();
                
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
