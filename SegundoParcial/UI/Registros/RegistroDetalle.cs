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
    public partial class RegistroDetalle : Form
    {
        decimal itbis = 0;
        decimal total = 0;
        
        public RegistroDetalle()
        {
            InitializeComponent();
            LlenarComboBox();
        }

        public void Columnas()
        {
            DetalledataGridView.Columns["MantenimientoId"].Visible = false;
            DetalledataGridView.Columns["Id"].Visible = false;
            DetalledataGridView.Columns["MantenimientoId"].Visible = false;
            DetalledataGridView.Columns["TallerId"].Visible = false;
            DetalledataGridView.Columns["ArticulosId"].Visible = false;
            DetalledataGridView.Columns["RegistrodeArticulos"].Visible = false;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(MantenimientoIdnumericUpDown.Value);
            Mantenimiento mantenimiento = BLL.MantenimientoBLL.Buscar(id);

            if (mantenimiento != null)
            {
                LlenarCampos(mantenimiento);
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            MantenimientoIdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            ProxFechadateTimePicker.Value = DateTime.Now;
            CantidadnumericUpDown.Value = 0;
            PreciotextBox.Text = string.Empty;
            ImportetextBox.Text = string.Empty;
            SubTotaltextBox.Text = string.Empty;
            ITBIStextBox.Text = string.Empty;
            TotaltextBox.Text = string.Empty;

            DetalledataGridView.DataSource = null;
            errorProvider.Clear();
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(2))
            {
                MessageBox.Show("Debe Agregar Algun Producto al Grid", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                Mantenimiento mantenimiento = LlenaClase();
                bool Paso = false;

                if (MantenimientoIdnumericUpDown.Value == 0)
                {
                    Paso = BLL.MantenimientoBLL.Guardar(mantenimiento);
                    Error.Clear();
                }
                else
                {
                    var mantenimientos = BLL.MantenimientoBLL.Buscar(Convert.ToInt32(MantenimientoIdnumericUpDown.Value));

                    if (mantenimientos != null)
                    {
                        Paso = BLL.MantenimientoBLL.Modificar(mantenimiento);
                    }
                    Error.Clear();
                }
                //Informar el resultado
                if (Paso)
                {
                    Limpiar();
                    MessageBox.Show("Guardado!!", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se pudo guardar!!", "Fallo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Error(1))
            {
                MessageBox.Show("Favor Llenar Casilla!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                int id = Convert.ToInt32(MantenimientoIdnumericUpDown.Value);
                if (BLL.MantenimientoBLL.Eliminar(id))
                {
                    MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                    MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<MantenimientoDetalle> mantenimientoDetalle = new List<MantenimientoDetalle>();
            
            if (DetalledataGridView.DataSource != null)
            {
                mantenimientoDetalle = (List<MantenimientoDetalle>)DetalledataGridView.DataSource;
            }
            foreach (var item in BLL.ArticulosBLL.GetList(x => x.Inventario < CantidadnumericUpDown.Value))
            {
                MessageBox.Show("No hay esa Existencia para Vender ", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(ImportetextBox.Text))
            {
                MessageBox.Show("Importe esta vacio , Llene cantidad", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mantenimientoDetalle.Add(
                    new MantenimientoDetalle(id: 0,
                        mantenimientoId: (int)Convert.ToInt32(MantenimientoIdnumericUpDown.Value),
                        tallerId: (int)TallercomboBox.SelectedValue,
                        articuloId: (int)ArticulocomboBox.SelectedValue,
                        articulo: (string)BLL.ArticulosBLL.Return(ArticulocomboBox.Text),
                        cantidad: (int)Convert.ToInt32(CantidadnumericUpDown.Value),
                        precio: (decimal)Convert.ToDecimal(PreciotextBox.Text),
                        importe: (decimal)Convert.ToDecimal(ImportetextBox.Text)

                    ));

                //Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = mantenimientoDetalle;
                Columnas();
            }
            decimal subtotal = 0;
            foreach (var item in mantenimientoDetalle)
            {
                subtotal += item.Importe;
            }
            SubTotaltextBox.Text = subtotal.ToString();
            itbis = BLL.MantenimientoBLL.CalcularItbis(Convert.ToDecimal(SubTotaltextBox.Text));
            ITBIStextBox.Text = itbis.ToString();
            total = BLL.MantenimientoBLL.Total(Convert.ToDecimal(SubTotaltextBox.Text), Convert.ToDecimal(ITBIStextBox.Text));
            TotaltextBox.Text = total.ToString();
        }

        private void LlenarComboBox()
        {
            Repositorio<Vehiculo> vehiculo = new Repositorio<Vehiculo>(new Contexto());
            Repositorio<Taller> taller = new Repositorio<Taller>(new Contexto());
            Repositorio<Articulo> Entrada = new Repositorio<Articulo>(new Contexto());
            VehiculocomboBox.DataSource = vehiculo.GetList(c => true);
            VehiculocomboBox.ValueMember = "VehiculoId";
            VehiculocomboBox.DisplayMember = "Descripcion";
            TallercomboBox.DataSource = taller.GetList(c => true);
            TallercomboBox.ValueMember = "TallerId";
            TallercomboBox.DisplayMember = "Nombre";
            ArticulocomboBox.DataSource = Entrada.GetList(c => true);
            ArticulocomboBox.ValueMember = "ArticuloId";
            ArticulocomboBox.DisplayMember = "Descripcion";
        }

        private Mantenimiento LlenaClase()
        {
            Mantenimiento mantenimiento = new Mantenimiento();

            mantenimiento.MantenimientoId = Convert.ToInt32(MantenimientoIdnumericUpDown.Value);
            mantenimiento.VehiculoId = Convert.ToInt32(VehiculocomboBox.SelectedValue);
            mantenimiento.Fecha = FechadateTimePicker.Value;
            mantenimiento.Subtotal = Convert.ToDecimal(SubTotaltextBox.Text);
            mantenimiento.ITBIS = Convert.ToDecimal(ITBIStextBox.Text);
            mantenimiento.Total = Convert.ToDecimal(TotaltextBox.Text);

            //Agregar cada linea del Grid al detalle
            foreach (DataGridViewRow item in DetalledataGridView.Rows)
            {
                mantenimiento.AgregarDetalle
                    (ToInt(item.Cells["id"].Value),
                     mantenimiento.MantenimientoId,
                     ToInt(item.Cells["tallerId"].Value),
                     ToInt(item.Cells["articulosId"].Value),
                     Convert.ToString(item.Cells["articulo"].Value),
                     ToInt(item.Cells["cantidad"].Value),
                     ToDecimal(item.Cells["precio"].Value),
                     ToDecimal(item.Cells["importe"].Value)



                  );
            }
            return mantenimiento;
        }
        private void cantidadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

            ImportetextBox.Text = BLL.MantenimientoBLL.CalcularImporte(Convert.ToDecimal(PreciotextBox.Text), Convert.ToInt32(CantidadnumericUpDown.Value)).ToString(); ;

        }
        private bool Validar(int error)
        {
            bool Error = false;

            if (MantenimientoIdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(MantenimientoIdnumericUpDown,
                   "No debes dejar la Mantenimiento Id Vacio");
                Error = true;
            }
            if (CantidadnumericUpDown.Value == 0)
            {
                errorProvider.SetError(CantidadnumericUpDown,
                    "Debes introducir una Cantidad");
                Error = true;
            }
            if (string.IsNullOrWhiteSpace(TotaltextBox.Text))
            {
                errorProvider.SetError(TotaltextBox,
                   "No debes dejar la total vacio");
                Error = true;
            }
            if (string.IsNullOrWhiteSpace(SubTotaltextBox.Text))
            {
                errorProvider.SetError(SubTotaltextBox,
                   "No debes dejar la Subtotal vacio");
                Error = true;
            }
            if (string.IsNullOrWhiteSpace(ITBIStextBox.Text))
            {
                errorProvider.SetError(ITBIStextBox,
                   "No debes dejar el ITBIS vacio");
                Error = true;
            }
            if (string.IsNullOrWhiteSpace(DetalledataGridView.Text))
            {
                errorProvider.SetError(DetalledataGridView,
                   "No debes dejar sin seleccionar");
                Error = true;
            }

            if (String.IsNullOrWhiteSpace(PreciotextBox.Text))
            {
                errorProvider.SetError(PreciotextBox,
                    "Debes introducir un Precio");
                Error = true;
            }
            
            if (String.IsNullOrWhiteSpace(ImportetextBox.Text))
            {
                errorProvider.SetError(ImportetextBox,
                    "Debes introducir un Importe");
                Error = true;
            }
            
            return Error;
        }

        private void LlenarCampos(Mantenimiento mantenimiento)
        {
            Limpiar();
            MantenimientoIdnumericUpDown.Value = mantenimiento.MantenimientoId;
            FechadateTimePicker.Value = mantenimiento.Fecha;
            SubTotaltextBox.Text = mantenimiento.Subtotal.ToString();
            ITBIStextBox.Text = mantenimiento.ITBIS.ToString();
            TotaltextBox.Text = mantenimiento.Total.ToString();


            //Cargar el detalle al Grid
            DetalledataGridView.DataSource = mantenimiento.Detalle;

            Columnas();
        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            MantenimientoDetalle mantenimientoDetalle = new MantenimientoDetalle();
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                List<MantenimientoDetalle> detalle = (List<MantenimientoDetalle>)DetalledataGridView.DataSource;            
                detalle.RemoveAt(DetalledataGridView.CurrentRow.Index);

                decimal subtotal = 0;
                foreach (var item in detalle)
                {
                    subtotal -= item.Importe;
                }
                subtotal *= (-1);
                SubTotaltextBox.Text = subtotal.ToString();
                itbis = BLL.MantenimientoBLL.CalcularItbis(Convert.ToDecimal(SubTotaltextBox.Text));
                ITBIStextBox.Text = itbis.ToString();
                total = BLL.MantenimientoBLL.Total(Convert.ToDecimal(SubTotaltextBox.Text), Convert.ToDecimal(ITBIStextBox.Text));
                TotaltextBox.Text = total.ToString();

                // Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = detalle;
                Columnas();
            }

        }

        private void CantidadnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ImportetextBox.Text = BLL.MantenimientoBLL.CalcularImporte(Convert.ToInt32(PreciotextBox.Text), Convert.ToInt32(CantidadnumericUpDown.Value)).ToString(); 
        }

        private void fechaDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ProxFechadateTimePicker.Value = FechadateTimePicker.Value;
            DateTime fecha = Convert.ToDateTime(ProxFechadateTimePicker.Text);
            fecha = fecha.AddDays(90);

            ProxFechadateTimePicker.Text = fecha.ToString();
        }

        private int ToInt(object valor)
        {
            int retorno = 0;
            int.TryParse(valor.ToString(), out retorno);
            return retorno;

        }
        private void FechadateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ProxFechadateTimePicker.Value = FechadateTimePicker.Value;
            DateTime fecha = Convert.ToDateTime(ProxFechadateTimePicker.Text);
            fecha = fecha.AddDays(90);

            ProxFechadateTimePicker.Text = fecha.ToString();
        }

        private void MantenimientoIdnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
        }
        
        private decimal ToDecimal(object valor)
        {
            decimal retorno = 0;
            decimal.TryParse(valor.ToString(), out retorno);
            return retorno;

        }

        private void RegistroDetalle_Load(object sender, EventArgs e)
        {
                DateTime fecha = Convert.ToDateTime(ProxFechadateTimePicker.Text);
                fecha = fecha.AddMonths(3);
                ProxFechadateTimePicker.Text = fecha.ToString();
            
        }

        private void ArticulocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in BLL.ArticulosBLL.GetList(x => x.Descripcion == ArticulocomboBox.Text))
            {
                PreciotextBox.Text = item.Precio.ToString();

            }
        }

        private void FechadateTimePicker_ValueChanged_1(object sender, EventArgs e)
        {
            ProxFechadateTimePicker.Value = FechadateTimePicker.Value;
            DateTime fecha = Convert.ToDateTime(ProxFechadateTimePicker.Text);
            fecha = fecha.AddMonths(3);

            ProxFechadateTimePicker.Text = fecha.ToString();

        }
    }
}
