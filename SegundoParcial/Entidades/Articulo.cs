using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SegundoParcial.Entidades
{
    public class Articulo
    {
        [Key]
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public Decimal Costo { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Ganancia { get; set; }
        public int Inventario { get; set; }

        public Articulo(int articulosId, string descripcion, Decimal costo, Decimal precio, Decimal ganancia, int inventario)
        {
            ArticuloId = articulosId;
            Descripcion = descripcion;
            Precio = precio;
            Costo = costo;
            Precio = precio;
            Ganancia = ganancia;
            Inventario = inventario;
        }

        public Articulo()
        {
            this.ArticuloId = 0;
            this.Descripcion = string.Empty;
            this.Costo = 0;
            this.Precio = 0;
            this.Ganancia = 0;
            Inventario = 0;
        }
    }
}
