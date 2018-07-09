using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SegundoParcial
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void registroArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Registros.RegistroArticulo registroArticulo = new UI.Registros.RegistroArticulo();
            registroArticulo.Show();

        }

        private void registroEntradaArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Registros.RegistroEntrada registroEntrada = new UI.Registros.RegistroEntrada();
            registroEntrada.Show();
        }

        private void registroMantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Registros.RegistroDetalle registroDetalle = new UI.Registros.RegistroDetalle();
            registroDetalle.Show();
        }

        private void registroTallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Registros.RegistroTaller registroTaller = new UI.Registros.RegistroTaller();
            registroTaller.Show();
        }

        private void registroVehiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Registros.RegistroVehiculo registroVehiculo = new UI.Registros.RegistroVehiculo();
            registroVehiculo.Show();
        }

        private void consultaArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Consultas.ConsultaArticulo consultaArticulo = new UI.Consultas.ConsultaArticulo();
            consultaArticulo.Show();
        }

        private void consultaMantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Consultas.ConsultaMantenimiento consultaMantenimiento = new UI.Consultas.ConsultaMantenimiento();
            consultaMantenimiento.Show();
        }

        private void consultaTallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Consultas.ConsultaTaller consultaTaller = new UI.Consultas.ConsultaTaller();
            consultaTaller.Show();

        }

        private void consultaVehiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SegundoParcial.UI.Consultas.ConsultaVehiculo consultaVehiculo = new UI.Consultas.ConsultaVehiculo();
            consultaVehiculo.Show();

        }
    }
}
