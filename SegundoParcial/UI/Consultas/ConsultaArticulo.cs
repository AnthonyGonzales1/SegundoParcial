using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace SegundoParcial.UI.Consultas
{
    public partial class ConsultaArticulo : Form
    {
        public ConsultaArticulo()
        {
            InitializeComponent();
        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            //Inicializando el filtro en True
            Expression<Func<Articulo, bool>> filtro = articulo => true;

            int id;
            switch (FiltrocomboBox.SelectedIndex)
            {
                case 0://ID del Artículo.
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = articulo => articulo.ArticuloId == id;
                    break;
                case 1://Descripcion del Artículo.
                    filtro = articulo => articulo.Descripcion.Contains(CriteriotextBox.Text);
                    break;
                case 2://Costo del Artículo.
                    filtro = articulo => articulo.Costo.Equals(CriteriotextBox.Text);
                    break;
                case 3://Precio del Artículo.
                    filtro = articulo => articulo.Precio.Equals(CriteriotextBox.Text);
                    break;
                case 4://Ganancia del Artículo.
                    filtro = articulo => articulo.Ganancia.Equals(CriteriotextBox.Text);
                    break;
                case 5://Inventario del Artículo.
                    filtro = articulo => articulo.Inventario.Equals(CriteriotextBox.Text);
                    break;
            }
            ConsultadataGridView.DataSource = BLL.ArticulosBLL.GetList(filtro);
        }

    }
}
