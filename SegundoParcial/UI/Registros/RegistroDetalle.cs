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
        Decimal Importe = 0;
        Decimal Subtotal = 0;
        Decimal ITBIS = 0;
        Decimal Total = 0;
        
        public RegistroDetalle()
        {
            InitializeComponent();
            LlenarComboBox();
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

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            MantenimientoIdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            ProxFechadateTimePicker.Value = DateTime.Now;
            CantidadnumericUpDown.Value=0;
            PreciotextBox.Clear();
            ImportetextBox.Clear();
            SubTotaltextBox.Clear();
            ITBIStextBox.Clear();
            TotaltextBox.Clear();

            DetalledataGridView.DataSource = null;
            errorProvider.Clear();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Mantenimiento mantenimiento;
            bool Paso = false;

            if (Error())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mantenimiento = LlenaClase();

            //Determinar si es Guardar o Modificar
            if (MantenimientoIdnumericUpDown.Value == 0)
                Paso = BLL.MantenimientoBLL.Guardar(mantenimiento);
            else
                //todo: validar que exista.
                Paso = BLL.MantenimientoBLL.Modificar(mantenimiento);

            //Informar el resultado
            if (Paso)
            {
                Nuevobutton.PerformClick();
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(MantenimientoIdnumericUpDown.Value);

            //todo: validar que exista
            if (BLL.MantenimientoBLL.Eliminar(id))
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<MantenimientoDetalle> detalle = new List<MantenimientoDetalle>();
            Mantenimiento mantenimiento = new Mantenimiento();


            if (DetalledataGridView.DataSource != null)
            {
                mantenimiento.Detalle = (List<MantenimientoDetalle>)DetalledataGridView.DataSource;
            }

            //Agregar un nuevo detalle con los datos introducidos.

            foreach (var item in BLL.ArticulosBLL.GetList(x => x.Inventario < CantidadnumericUpDown.Value))
            {
                MessageBox.Show("No Existe ", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(ImportetextBox.Text))
            {
                MessageBox.Show("Importe esta vacio , Llene cantidad", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mantenimiento.Detalle.Add(
                    new MantenimientoDetalle(id: 0,
                    mantenimientoId: (int)Convert.ToInt32(MantenimientoIdnumericUpDown.Value),
                    vehiculoId: (int)VehiculocomboBox.SelectedValue,
                    tallerId: (int)TallercomboBox.SelectedValue,
                    articuloId: (int)ArticulocomboBox.SelectedValue,
                    articulo: (string)BLL.ArticulosBLL.Return(ArticulocomboBox.Text),
                    cantidad: (int)Convert.ToInt32(CantidadnumericUpDown.Value),
                    precio: (int)Convert.ToInt32(PreciotextBox.Text),
                    importe: (int)Convert.ToInt32(ImportetextBox.Text)

                    ));

                //Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = mantenimiento.Detalle;

                Columnas();
            }
            
            Importe += BLL.MantenimientoBLL.CalcularImporte(Convert.ToDecimal(PreciotextBox.Text), Convert.ToInt32(CantidadnumericUpDown.Value));

            if (MantenimientoIdnumericUpDown.Value != 0)
            {
                Subtotal += Importe;
                SubTotaltextBox.Text = Subtotal.ToString();
            }
            else
            {
                Subtotal = Importe;
                SubTotaltextBox.Text = Subtotal.ToString();
            }
                ITBIS = BLL.MantenimientoBLL.CalcularItbis(Convert.ToDecimal(SubTotaltextBox.Text));
                ITBIStextBox.Text = ITBIS.ToString();
                Total = BLL.MantenimientoBLL.Total(Convert.ToDecimal(SubTotaltextBox.Text), Convert.ToDecimal(ITBIStextBox.Text));

                TotaltextBox.Text = Total.ToString();
            
        }

        private void LlenarComboBox()
        {
            Repositorio<Vehiculo> vehiculo = new Repositorio<Vehiculo>(new Contexto());
            VehiculocomboBox.DataSource = vehiculo.GetList(c => true);
            VehiculocomboBox.ValueMember = "VehiculoId";
            VehiculocomboBox.DisplayMember = "Descripcion";

            Repositorio<Taller> taller = new Repositorio<Taller>(new Contexto());
            TallercomboBox.DataSource = taller.GetList(c => true);
            TallercomboBox.ValueMember = "TallerId";
            TallercomboBox.DisplayMember = "Nombre";

            Repositorio<Articulo> Entrada = new Repositorio<Articulo>(new Contexto());
            ArticulocomboBox.DataSource = Entrada.GetList(c => true);
            ArticulocomboBox.ValueMember = "ArticuloId";
            ArticulocomboBox.DisplayMember = "Descripcion";
        }

        private Mantenimiento LlenaClase()
        {
            Mantenimiento mantenimiento = new Mantenimiento();

            mantenimiento.MantenimientoId = Convert.ToInt32(MantenimientoIdnumericUpDown.Value);
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
                     ToInt(item.Cells["vehiculoId"].Value),
                     ToInt(item.Cells["tallerId"].Value),
                     ToInt(item.Cells["articulosId"].Value),
                     Convert.ToString(item.Cells["articulo"].Value),
                     ToInt(item.Cells["cantidad"].Value),
                     ToInt(item.Cells["precio"].Value),
                     ToInt(item.Cells["importe"].Value)
                  );
            }
            return mantenimiento;

        }
        private bool Error()
        {
            bool Error = false;

            if (CantidadnumericUpDown.Value == 0)
            {
                errorProvider.SetError(CantidadnumericUpDown,
                    "Debes introducir una Cantidad");
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
            
            if (DetalledataGridView.RowCount == 0)
            {
                errorProvider.SetError(DetalledataGridView,
                    "Es obligatorio seleccionar ");
                Error = true;
            }

            return Error;
        }

        private void LlenarCampos(Mantenimiento mantenimiento)
        {
            MantenimientoIdnumericUpDown.Value = mantenimiento.MantenimientoId;
            FechadateTimePicker.Value = mantenimiento.Fecha;
            ProxFechadateTimePicker.Value = mantenimiento.ProxFecha;
            SubTotaltextBox.Text = mantenimiento.Subtotal.ToString();
            ITBIStextBox.Text = mantenimiento.ITBIS.ToString();
            TotaltextBox.Text = mantenimiento.Total.ToString();

            //Cargar el detalle al Grid
            DetalledataGridView.DataSource = mantenimiento.Detalle;

            //Ocultar columnas
            DetalledataGridView.Columns["Id"].Visible = false;
            DetalledataGridView.Columns["MantenimientoId"].Visible = false;
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

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                //convertir el grid en la lista
                List<MantenimientoDetalle> mantenimientodetalle = (List<MantenimientoDetalle>)DetalledataGridView.DataSource;
                
                Subtotal -= mantenimientodetalle.ElementAt(DetalledataGridView.CurrentRow.Index).Importe;

                mantenimientodetalle.RemoveAt(DetalledataGridView.CurrentRow.Index);
                
                SubTotaltextBox.Text = Subtotal.ToString();

                ITBIS = BLL.MantenimientoBLL.CalcularItbis(Convert.ToDecimal(SubTotaltextBox.Text));
                ITBIStextBox.Text = ITBIS.ToString();

                Total = BLL.MantenimientoBLL.Total(Convert.ToDecimal(SubTotaltextBox.Text), Convert.ToDecimal(ITBIStextBox.Text));

                TotaltextBox.Text = Total.ToString();
                
                // Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = mantenimientodetalle;
                
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
    }
}
