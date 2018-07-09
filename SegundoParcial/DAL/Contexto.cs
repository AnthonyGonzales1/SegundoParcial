using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SegundoParcial.BLL;
using SegundoParcial.Entidades;

namespace SegundoParcial.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Taller> Tallers { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<EntradaArticulo> EntradaArticulos { get; set; }

        public DbSet<MantenimientoDetalle> MantenimientoDetalles { get; set; }
        public Contexto() : base("ConStr") { }
            
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
