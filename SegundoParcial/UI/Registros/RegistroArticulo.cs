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
    public partial class RegistroArticulo : Form
    {
        public RegistroArticulo()
        {
            InitializeComponent();
            InventariotextBox.Text = "0";
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(ArticuloIdnumericUpDown.Value);
            Articulo articulo = BLL.ArticulosBLL.Buscar(id);

            if (articulo != null)
            {

                DescripciontextBox.Text = articulo.Descripcion;
                CostonumericUpDown.Text = articulo.Costo.ToString();
                GananciatextBox.Text = articulo.Ganancia.ToString();
                PreciotextBox.Text = articulo.Precio.ToString();
                InventariotextBox.Text = articulo.Inventario.ToString();
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
            if (ArticuloIdnumericUpDown.Value == 0)
                paso = BLL.ArticulosBLL.Guardar(LlenarClase());
            else
                paso = BLL.ArticulosBLL.Modificar(LlenarClase());

            //Informar el resultado
            if (paso)

                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            ArticuloIdnumericUpDown.Value = 0;
            DescripciontextBox.Clear();
            PreciotextBox.Clear();
            CostonumericUpDown.Value = 0;
            GananciatextBox.Clear();
            InventariotextBox.Clear();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(ArticuloIdnumericUpDown.Value);

            if (BLL.ArticulosBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private decimal Ganancia(decimal c, decimal p)
        {
            decimal r = p - c;
            r = r / c;
            r = r * 100;
            return r;
        }

        private Articulo LlenarClase()
        {
            Articulo art = new Articulo();

            art.ArticuloId = Convert.ToInt32(ArticuloIdnumericUpDown.Value);
            art.Descripcion = DescripciontextBox.Text;
            art.Precio = Convert.ToInt32(PreciotextBox.Text);
            art.Costo = Convert.ToInt32(CostonumericUpDown.Text);
            art.Inventario = Convert.ToInt32(InventariotextBox.Text);
            art.Ganancia = Convert.ToDecimal(GananciatextBox.Text);
            return art;
        }

        private bool Validar(int validar)
        {

            bool paso = false;
            if (validar == 1 && ArticuloIdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(ArticuloIdnumericUpDown, "Ingrese un ID");
                paso = true;

            }
            if (validar == 2 && DescripciontextBox.Text == string.Empty)
            {
                errorProvider.SetError(DescripciontextBox, "Ingrese una descripcion");
                paso = true;
            }
            if (validar == 2 && PreciotextBox.Text == string.Empty)
            {

                errorProvider.SetError(PreciotextBox, "Ingrese un precio");
                paso = true;
            }
            if (validar == 2 && CostonumericUpDown.Text == string.Empty)
            {

                errorProvider.SetError(CostonumericUpDown, "Ingrese el Costo");

            }
            if (validar == 2 && InventariotextBox.Text == string.Empty)
            {

                errorProvider.SetError(InventariotextBox, "Ingrese el Inventario");

            }
            if (validar == 2 && GananciatextBox.Text == string.Empty)
            {

                errorProvider.SetError(GananciatextBox, "Ingrese el Inventario");

            }
            return paso;
        }

        private void PrecionumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void GananciatextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            string precio = PreciotextBox.Text;
            decimal DPrecio = Convert.ToDecimal(precio);
            GananciatextBox.Text = Ganancia(CostonumericUpDown.Value, DPrecio).ToString();
        }
    }
}
