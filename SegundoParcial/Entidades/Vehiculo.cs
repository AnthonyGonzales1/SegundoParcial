using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalMantenimiento { get; set; }

        public Vehiculo()
        {
            VehiculoId = 0;
            Descripcion = string.Empty;
            TotalMantenimiento = 0;
        }
        
    }
}
