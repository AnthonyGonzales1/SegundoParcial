using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{

    public class EntradaArticulo
    {
        [Key]
        public int EntradaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }

        public EntradaArticulo(int entradaId, DateTime fecha, string articulo, int cantidad)
        {
            EntradaId = entradaId;
            Fecha = fecha;
            Articulo = articulo;
            Cantidad = cantidad;
          }

        public EntradaArticulo()
        {
            this.EntradaId = 0;
            this.Fecha = DateTime.Now;
            Articulo = string.Empty;
            Cantidad = 0;
            
        }
    }
}
