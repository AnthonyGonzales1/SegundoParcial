using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SegundoParcial.BLL;
using SegundoParcial.DAL;
using SegundoParcial.Entidades;

namespace SegundoParcial.UI.Registros
{
    public partial class RegistroEntrada : Form
    {
        public RegistroEntrada()
        {
            InitializeComponent();
            LlenaComboBox();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(EntradaIdnumericUpDown.Value);
            EntradaArticulo entradaArticulo = BLL.EntradaArticuloBLL.Buscar(id);

            if (entradaArticulo != null)
            {
                EntradaIdnumericUpDown.Value = entradaArticulo.EntradaId;
                ArticulocomboBox.SelectedValue = entradaArticulo.ArticuloId.ToString();
                CantidadtextBox.Text = entradaArticulo.Cantidad.ToString();
            }
            else
            {
                MessageBox.Show("No se encontro", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            EntradaArticulo EntradaArticulos = LlenarClase();


            if (Validar(2))
            {
                MessageBox.Show("Favor de Llenar las Casillas");
            }
            else
            {
                if (EntradaIdnumericUpDown.Value == 0)
                {
                    paso = BLL.EntradaArticuloBLL.Guardar(EntradaArticulos);
                }
                else
                {
                    var V = BLL.EntradaArticuloBLL.Buscar(Convert.ToInt32(EntradaIdnumericUpDown.Value));

                    if (V != null)
                    {
                        paso = BLL.EntradaArticuloBLL.Modificar(EntradaArticulos);
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
            EntradaIdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            CantidadtextBox.Clear();
        }

        private void LlenaComboBox()
        {
            Repositorio<Articulo> repositorio = new Repositorio<Articulo>(new Contexto());
            ArticulocomboBox.DataSource = repositorio.GetList(c => true);
            ArticulocomboBox.ValueMember = "ArticuloId";
            ArticulocomboBox.DisplayMember = "Descripcion";
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(EntradaIdnumericUpDown.Value);

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
        private EntradaArticulo LlenarClase()
        {
            EntradaArticulo entradaArticulo = new EntradaArticulo();

            entradaArticulo.EntradaId = Convert.ToInt32(EntradaIdnumericUpDown.Value);
            entradaArticulo.ArticuloId = (int)ArticulocomboBox.SelectedValue;
            entradaArticulo.Cantidad = Convert.ToInt32(CantidadtextBox.Text);

            return entradaArticulo;
        }
        private bool Validar(int validar)
        {
            bool paso = false;
            if (validar == 1 && EntradaIdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(EntradaIdnumericUpDown, "Ingrese un ID");
                paso = true;
            }
            /*if (validar == 2 && FechadateTimePicker.Text == string.Empty)
            {
                errorProvider.SetError(FechadateTimePicker, "Ingrese una descripcion");
                paso = true;
            }*/
            if (validar == 2 && CantidadtextBox.Text == string.Empty)
            {
                errorProvider.SetError(CantidadtextBox, "Ingrese la Cantidad");
                paso = true;
            }
            return paso;
        }
    }
}
