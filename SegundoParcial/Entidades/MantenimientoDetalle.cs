using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{
    public class MantenimientoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public int MantenimientoId { get; set; }
        public string Vehiculo { get; set; }
        public string Taller { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Importe { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal ITBIS { get; set; }
        public Decimal Total { get; set; }

        [ForeignKey("VehiculoId")]
        public virtual Vehiculo Vehiculos { get; set; }

        public MantenimientoDetalle()
        {
            this.Id = 0;
            this.MantenimientoId = 0;

        }
    }
}
