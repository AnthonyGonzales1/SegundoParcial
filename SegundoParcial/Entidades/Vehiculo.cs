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
        public int Manteniento { get; set; }

        public Vehiculo() { }

        public override string ToString()
        {
            return this.Descripcion;
        }

    }
}
