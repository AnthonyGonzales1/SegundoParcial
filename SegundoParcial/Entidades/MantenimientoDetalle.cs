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
        public int MantenimientoId { get; set; }
        public int VehiculoId { get; set; }
        public int TallerId { get; set; }
        public int ArticuloId { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Importe { get; set; }
      
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulos { get; set; }
        [ForeignKey("VehiculoId")]
        public virtual Vehiculo Vehiculos{ get; set; }
        [ForeignKey("TallerId")]
        public virtual Taller Taller { get; set; }


        public MantenimientoDetalle()
        {
            this.Id = 0;
            this.MantenimientoId = 0;
            this.VehiculoId = 0;
            this.ArticuloId = 0;
            this.TallerId = 0;
            this.Articulo = string.Empty;
            this.Cantidad = 0;
            this.Precio = 0;
            this.Importe = 0;
        }
        public MantenimientoDetalle(int id, int mantenimientoId, int vehiculoId, int tallerId, int articuloId, string articulo, int cantidad, Decimal precio, Decimal importe)
        {
            Id = id;
            MantenimientoId = mantenimientoId;
            TallerId = tallerId;
            VehiculoId = vehiculoId;
            ArticuloId = articuloId;
            Articulo = articulo;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
            
        }
    }
}
