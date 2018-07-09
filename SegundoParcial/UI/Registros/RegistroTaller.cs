using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SegundoParcial.BLL;
using SegundoParcial.Entidades;

namespace SegundoParcial.UI.Registros
{
    public partial class RegistroTaller : Form
    {
        public RegistroTaller()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(TallerIdnumericUpDown.Value);
            Taller taller = BLL.TalleresBLL.Buscar(id);

            if (taller != null)
            {

                NombretextBox.Text = taller.ToString();
                
            }
            else
                MessageBox.Show("No se encontro", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            if (Validar(2))
            {

                MessageBox.Show("Llenar todos los campos marcados");
                return;
            }

            errorProvider.Clear();

            //Determinar si es Guardar o Modificar
            if (TallerIdnumericUpDown.Value == 0)
                paso = BLL.TalleresBLL.Guardar(LlenarClase());
            else
                paso = BLL.TalleresBLL.Modificar(LlenarClase());

            //Informar el resultado
            if (paso)

                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            TallerIdnumericUpDown.Value = 0;
            NombretextBox.ToString();
            
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(TallerIdnumericUpDown.Value);

            if (BLL.EntradaArticuloBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private Taller LlenarClase()
        {

            Taller tal = new Taller();

            tal.TallerId = Convert.ToInt32(TallerIdnumericUpDown.Value);
            tal.Nombre = NombretextBox.ToString();
            
            return tal;
        }
        private bool Validar(int validar)
        {

            bool paso = false;
            if (validar == 1 && TallerIdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(TallerIdnumericUpDown, "Ingrese un ID");
                paso = true;

            }
            if (validar == 2 && NombretextBox.Text == string.Empty)
            {

                errorProvider.SetError(NombretextBox, "Debe ingresar el Nombre");
                paso = true;
            }
            
            return paso;
        }
    }
}
