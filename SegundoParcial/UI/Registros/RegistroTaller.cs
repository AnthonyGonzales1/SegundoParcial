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
            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }
            else
            {

                int id = Convert.ToInt32(TallerIdnumericUpDown.Value);
                Taller taller = BLL.TalleresBLL.Buscar(id);

                if (taller != null)
                {

                    NombretextBox.Text = taller.ToString();

                }
                else
                {
                    MessageBox.Show("No se encontro", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                errorProvider.Clear();
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Taller taller = LlenarClase();
            bool paso = false;
            if (Validar(2))
            {
                MessageBox.Show("Favor de Llenar las Casillas");
            }
            else
            {
                if (TallerIdnumericUpDown.Value == 0)
                {
                    paso = BLL.TalleresBLL.Guardar(taller);
                }
                else
                {
                    var V = BLL.TalleresBLL.Buscar(Convert.ToInt32(TallerIdnumericUpDown.Value));

                    if (V != null)
                    {
                        paso = BLL.TalleresBLL.Modificar(taller);
                    }
                }
                errorProvider.Clear();
                if (paso)
                {
                    MessageBox.Show("Guardado!", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No pudo Guardar!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            TallerIdnumericUpDown.Value = 0;
            NombretextBox.Clear();
            
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
            }
            else
            {
                int id = Convert.ToInt32(TallerIdnumericUpDown.Value);

                if (BLL.EntradaArticuloBLL.Eliminar(id))
                {
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                errorProvider.Clear();
            }
        }
        private Taller LlenarClase()
        {

            Taller taller = new Taller();

            taller.TallerId = Convert.ToInt32(TallerIdnumericUpDown.Value);
            taller.Nombre = NombretextBox.ToString();
            
            return taller;
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
