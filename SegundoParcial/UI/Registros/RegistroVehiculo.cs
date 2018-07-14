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
    public partial class RegistroVehiculo : Form
    {
        public RegistroVehiculo()
        {
            InitializeComponent();
            TotalMantenimientotextBox.Text = "0";
        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Debe ingresar un ID");
                return;
            }

            int id = Convert.ToInt32(VehiculoIdnumericUpDown.Value);
            Vehiculo vehiculo = BLL.VehiculoBLL.Buscar(id);

            if (vehiculo != null)
            {

                DescripciontextBox.Text = vehiculo.ToString();
                TotalMantenimientotextBox.Text = vehiculo.ToString();

            }
            else
            {
                MessageBox.Show("No se encontro", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            errorProvider.Clear();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Vehiculo vehiculo = LlenarClase();
            if (Validar(2))
            {
                MessageBox.Show("Favor de Llenar las Casillas");
            }
            else
            {
                int id = Convert.ToInt32(VehiculoIdnumericUpDown.Value);
                if (id == 0)
                {
                    paso = BLL.VehiculoBLL.Guardar(vehiculo);
                }
                else
                {
                    var C = BLL.VehiculoBLL.Buscar(id);

                    if (C != null)
                    {
                        paso = BLL.VehiculoBLL.Modificar(vehiculo);
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
            VehiculoIdnumericUpDown.Value = 0;
            DescripciontextBox.Clear();
            TotalMantenimientotextBox.Clear();

            errorProvider.Clear();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
            }
            else
            {

                int id = Convert.ToInt32(VehiculoIdnumericUpDown.Value);

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
        private Vehiculo LlenarClase()
        {
            Vehiculo vehiculo = new Vehiculo();
            TotalMantenimientotextBox.Text = 0.ToString();
            vehiculo.VehiculoId = Convert.ToInt32(VehiculoIdnumericUpDown.Value);
            vehiculo.Descripcion = DescripciontextBox.Text;
            vehiculo.TotalMantenimiento = Convert.ToDecimal(TotalMantenimientotextBox.Text);

            return vehiculo;
        }
        private bool Validar(int validar)
        {

            bool paso = false;
            if (validar == 1 && VehiculoIdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(VehiculoIdnumericUpDown, "Debe ingresar un ID");
                paso = true;

            }
            if (validar == 2 && DescripciontextBox.Text == string.Empty)
            {
                errorProvider.SetError(DescripciontextBox, "Debe ingresar una Descripcion");
                paso = true;
            }
            
            return paso;
        }
    }
}
