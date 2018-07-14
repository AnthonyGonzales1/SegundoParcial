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
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }

        public EntradaArticulo(int entradaId, DateTime fecha, int articuloId, int cantidad)
        {
            EntradaId = entradaId;
            Fecha = fecha;
            ArticuloId = articuloId;
            Cantidad = cantidad;
          }

        public EntradaArticulo()
        {
            this.EntradaId = 0;
            this.Fecha = DateTime.Now;
            ArticuloId = 0;
            Cantidad = 0;
            
        }
    }
}
