using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using SegundoParcial.Entidades;


namespace SegundoParcial.UI.Consultas
{
    public partial class ConsultaVehiculo : Form
    {
        public ConsultaVehiculo()
        {
            InitializeComponent();
        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            //Inicializando el filtro en True
            Expression<Func<Vehiculo, bool>> filtro = vehiculo => true;

            int id;
            switch (FiltrocomboBox.SelectedIndex)
            {
                case 0://ID del Artículo.
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = vehiculo => vehiculo.VehiculoId == id;
                    break;
                case 1://Descripcion del Artículo.
                    filtro = vehiculo => vehiculo.Descripcion.Contains(CriteriotextBox.Text);
                    break;
                case 2://Costo del Artículo.
                    filtro = vehiculo => vehiculo.TotalMantenimiento.Equals(CriteriotextBox.Text);
                    break;
            }
            ConsultadataGridView.DataSource = BLL.VehiculoBLL.GetList(filtro);
        }
    }
}

