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
                FechadateTimePicker.Text = entradaArticulo.Fecha.ToString();
                ArticulocomboBox.Text = entradaArticulo.Articulo.ToString();
                CantidadtextBox.Text = entradaArticulo.Cantidad.ToString();
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
            if (EntradaIdnumericUpDown.Value == 0)
                paso = BLL.EntradaArticuloBLL.Guardar(LlenarClase());
            else
                paso = BLL.EntradaArticuloBLL.Modificar(LlenarClase());

            //Informar el resultado
            if (paso)

                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);


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
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private EntradaArticulo LlenarClase()
        {

            EntradaArticulo enart = new EntradaArticulo();

            enart.EntradaId = Convert.ToInt32(EntradaIdnumericUpDown.Value);
            enart.Fecha = FechadateTimePicker.Value;
            enart.Articulo = ArticulocomboBox.SelectedText;
            enart.Cantidad = Convert.ToInt32(CantidadtextBox.Text);
            return enart;
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
            if (validar == 2 && ArticulocomboBox.Text == string.Empty)
            {

                errorProvider.SetError(ArticulocomboBox, "Ingrese un Articulo");
                paso = true;
            }
            if (validar == 2 && CantidadtextBox.Text == string.Empty)
            {

                errorProvider.SetError(CantidadtextBox, "Ingrese la Cantidad");

            }
            return paso;

        }
    }
}
