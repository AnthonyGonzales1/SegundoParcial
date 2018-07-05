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
        public DateTime Fecha { get; set; }
        public DateTime ProximoMantenimiento { get; set; }
        public string Vehiculo { get; set; }
        public string Taller { get; set; }

        public virtual ICollection<MantenimientoDetalle> Detalle { get; set; }

        public Mantenimiento()
        {
            MantenimientoId = 0;
            Fecha = DateTime.Now;
            ProximoMantenimiento = DateTime.Now;
            Vehiculo = string.Empty;
            Taller = string.Empty;
            Detalle = new List<MantenimientoDetalle>();

        }
    }
}
