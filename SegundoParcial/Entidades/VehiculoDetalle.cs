using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{
    public class VehiculoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime ProximoMantenimiento { get; set; }
        public string Vehiculo { get; set; }
        public string Taller { get; set; }
        public string Servicio { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Importe { get; set; }

        [ForeignKey("VehiculoId")]
        public virtual Vehiculo Vehiculos { get; set; }
        
        public VehiculoDetalle()
        {
            this.Id = 0;
            
        }

        public VehiculoDetalle(int id, int mantenimientoId, DateTime Fecha, DateTime proximoMantenimiento,string vehiculo, string taller,string servicio, int precio, int cantidad, int importe)
        {
            Id = id;

            Vehiculo = vehiculo;
            Taller = taller;
            Servicio = servicio;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }

    }
}

