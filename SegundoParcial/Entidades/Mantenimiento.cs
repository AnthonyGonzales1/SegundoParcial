using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{
    public class Mantenimiento
    {
        [Key]
        public int MantenimientoId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime ProxFecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ITBIS { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<MantenimientoDetalle> Detalle { get; set; }

        public Mantenimiento()
        {
            MantenimientoId = 0;
            Fecha = DateTime.Now;
            ProxFecha = DateTime.Now;
            Subtotal = 0;
            ITBIS = 0;
            Total = 0;
            Detalle = new List<MantenimientoDetalle>();

        }
        public void AgregarDetalle(int id,int mantenimientoId, int tallerId, int articuloId, string articulo, int cantidad,decimal precio, decimal importe)
        {
            this.Detalle.Add(new MantenimientoDetalle(id, mantenimientoId, tallerId, articuloId, articulo, cantidad, precio, importe));
        }
    }
}
