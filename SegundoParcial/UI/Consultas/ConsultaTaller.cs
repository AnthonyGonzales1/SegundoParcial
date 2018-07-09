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
    public partial class ConsultaTaller : Form
    {
        public ConsultaTaller()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            //Inicializando el filtro en True
            Expression<Func<Taller, bool>> filtro = taller => true;

            int id;
            switch (FiltrocomboBox.SelectedIndex)
            {
                case 0://ID del Artículo.
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = taller => taller.TallerId == id;
                    break;
                case 1://Descripcion del Artículo.
                    filtro = taller => taller.Nombre.Contains(CriteriotextBox.Text);
                    break;
                
            }
            ConsultadataGridView.DataSource = BLL.TalleresBLL.GetList(filtro);
        }
    }
}
