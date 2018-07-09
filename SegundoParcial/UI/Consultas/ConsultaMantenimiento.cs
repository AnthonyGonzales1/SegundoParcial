using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using SegundoParcial.BLL;
using SegundoParcial.Entidades;

namespace SegundoParcial.UI.Consultas
{
    public partial class ConsultaMantenimiento : Form
    {
        public ConsultaMantenimiento()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            //Inicializando el filtro en True
            Expression<Func<Mantenimiento, bool>> filtro = mantenimiento => true;

            int id;
            switch (FiltrocomboBox.SelectedIndex)
            {
                case 0://ID del Mantenimiento.
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = mantenimiento => mantenimiento.MantenimientoId == id;
                    break;
                case 1://Fecha de Mantenimiento.
                    filtro = mantenimiento => mantenimiento.Fecha >= DesdedateTimePicker.Value && mantenimiento.Fecha <= HastadateTimePicker.Value;
                    break;
                case 2://Próximo Mantenimiento.
                    filtro = mantenimiento => mantenimiento.ProxFecha >= DesdedateTimePicker.Value && mantenimiento.ProxFecha <= HastadateTimePicker.Value;
                    break;
                case 3://ITBIS.
                    filtro = mantenimiento => mantenimiento.ITBIS.Equals(CriteriotextBox.Text);
                    break;
                case 4://Total del Mantenimiento.
                    filtro = mantenimiento => mantenimiento.Total.Equals(CriteriotextBox.Text);
                    break;
            }
            ConsultadataGridView.DataSource = BLL.MantenimientoBLL.GetList(filtro);
        }
        private void comboBoxFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FiltrocomboBox.SelectedIndex == 1)
            {
                CriteriotextBox.Visible = false;
                Criteriolabel.Visible = false;
            }
            else
            {
                CriteriotextBox.Visible = true;
                Criteriolabel.Visible = true;
            }
        }
    }
}
