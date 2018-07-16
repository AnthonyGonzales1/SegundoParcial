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
                CostotextBox.Text = articulo.Costo.ToString();
                GananciatextBox.Text = articulo.Ganancia.ToString();
                PreciotextBox.Text = articulo.Precio.ToString();
                InventariotextBox.Text = articulo.Inventario.ToString();
            }
            else
                MessageBox.Show("No se encontro", "Fallo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Articulo articulo = LlenarClase();


            if (Validar(2))
            {
                MessageBox.Show("Favor de Llenar las Casillas");
            }
            else
            {
                if (ArticuloIdnumericUpDown.Value == 0)
                {
                    paso = BLL.ArticulosBLL.Guardar(articulo);
                }
                else
                {
                    var articulos = BLL.ArticulosBLL.Buscar(Convert.ToInt32(ArticuloIdnumericUpDown.Value));

                    if (articulo != null)
                    {
                        paso = BLL.ArticulosBLL.Modificar(articulo);
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
            ArticuloIdnumericUpDown.Value = 0;
            DescripciontextBox.Clear();
            PreciotextBox.Clear();
            CostotextBox.Clear();
            GananciatextBox.Clear();
            InventariotextBox.Clear();

            errorProvider.Clear();
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
        
        private Articulo LlenarClase()
        {
            Articulo art = new Articulo();
            InventariotextBox.Text = 0.ToString();
            art.ArticuloId = Convert.ToInt32(ArticuloIdnumericUpDown.Value);
            art.Descripcion = DescripciontextBox.Text;
            art.Precio = Convert.ToInt32(PreciotextBox.Text);
            art.Costo = Convert.ToInt32(CostotextBox.Text);
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
            if (validar == 2 && CostotextBox.Text == string.Empty)
            {
                errorProvider.SetError(CostotextBox, "Ingrese el Costo");
                paso = true;
            }
            if (validar == 2 && InventariotextBox.Text == string.Empty)
            {
                errorProvider.SetError(InventariotextBox, "Ingrese el Inventario");
                paso = true;
            }
            if (validar == 2 && GananciatextBox.Text == string.Empty)
            {
                errorProvider.SetError(GananciatextBox, "Ingrese el Inventario");
                paso = true;
            }
            return paso;
        }
        private void CostotextBox_TextChanged(object sender, EventArgs e)
        {


            if (PreciotextBox.Text != string.Empty && CostotextBox.Text != string.Empty && Convert.ToDecimal(CostotextBox.Text) < Convert.ToDecimal(PreciotextBox.Text))
            {
                GananciatextBox.Text = BLL.ArticulosBLL.Calcularganancia(Convert.ToDecimal(CostotextBox.Text), Convert.ToDecimal(PreciotextBox.Text)).ToString();
                return;
            }



            if (GananciatextBox.Text != string.Empty && CostotextBox.Text != string.Empty)
            {
                PreciotextBox.Text = BLL.ArticulosBLL.Calcularprecio(Convert.ToDecimal(CostotextBox.Text), Convert.ToDecimal(GananciatextBox.Text)).ToString();
                return;
            }


        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            if (PreciotextBox.Text != string.Empty && CostotextBox.Text != string.Empty && Convert.ToDecimal(CostotextBox.Text) < Convert.ToDecimal(PreciotextBox.Text))
            {
                GananciatextBox.Text = BLL.ArticulosBLL.Calcularganancia(Convert.ToDecimal(CostotextBox.Text), Convert.ToDecimal(PreciotextBox.Text)).ToString();
                return;
            }


        }

        private void GananciatextBox_TextChanged(object sender, EventArgs e)
        {

            if (GananciatextBox.Text != string.Empty && CostotextBox.Text != string.Empty)
            {
                PreciotextBox.Text = BLL.ArticulosBLL.Calcularprecio(Convert.ToDecimal(CostotextBox.Text), Convert.ToDecimal(GananciatextBox.Text)).ToString();
                return;
            }
            
        }

    }
}
