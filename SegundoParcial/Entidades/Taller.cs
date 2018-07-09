using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{
    public class Taller
    {
        [Key]
        public int TallerId { get; set; }
        public string Nombre { get; set; }

        public Taller()
        {
            TallerId = 0;
            Nombre = string.Empty;
        }

        public Taller(int tallerId, string nombre)
        {
            TallerId = tallerId;
            Nombre = nombre;
           
        }
    }
}
