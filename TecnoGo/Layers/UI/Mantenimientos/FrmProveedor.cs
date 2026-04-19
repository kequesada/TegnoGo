using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnoGo.Enumeraciones;
using TecnoGo.Extensions;
using TecnoGo.Layers.BLL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.UI.Mantenimientos
{
    public partial class FrmProveedor : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public FrmProveedor()
        {
            InitializeComponent();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ErrorProvider erp = new ErrorProvider();

        private void ChangeState(EstadoMantenimiento estado)
        {
            erp.Clear();
            this.txtIdProveedor.Clear();
            this.txtDescripcion.Clear();
            this.cmbEstado.SelectedIndex = -1;

            this.txtIdProveedor.Enabled = false;
            this.txtDescripcion.Enabled = false;
            this.cmbEstado.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtIdProveedor.Enabled = true;
                    this.txtDescripcion.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtIdProveedor.Focus();
                    break;
                case EstadoMantenimiento.Editar:
                    this.txtIdProveedor.Enabled = false;
                    this.txtDescripcion.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtDescripcion.Focus();
                    break;
                case EstadoMantenimiento.Borrar:
                    break;
                case EstadoMantenimiento.Ninguno:
                    break;
            }
        }

        private async void LoadData()
        {
            IProveedorBLL bllProveedor = new ProveedorBLL();
            try
            {
                // Cambiar el estado
                this.ChangeState(EstadoMantenimiento.Ninguno);

                // dgvDatos.RowTemplate.Height = 100;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Delay forzado
                await Task.Delay(500);

                // Cargar el DataGridView
                this.dgvDatos.DataSource = await bllProveedor.GetAll();

                // Cargar el combo
                cmbEstado.DataSource = Enum.GetValues(typeof(EstadoGeneral));
                cmbEstado.SelectedIndex = 0;
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.ChangeState(EstadoMantenimiento.Nuevo);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ChangeState(EstadoMantenimiento.Ninguno);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            IProveedorBLL bllProveedor = new ProveedorBLL();
            erp = new ErrorProvider();
            try
            {
                erp.Clear();
                Proveedor oProveedor = new Proveedor();

                if (string.IsNullOrEmpty(txtIdProveedor.Text))
                {
                    erp.SetError(txtIdProveedor, "El Id de la marca es requerido.");
                    txtIdProveedor.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    erp.SetError(txtDescripcion, "El nombre/descripción de la marca es requerido.");
                    txtDescripcion.Focus();
                    return;
                }

                oProveedor.Id = int.Parse(this.txtIdProveedor.Text);
                oProveedor.Nombre = this.txtDescripcion.Text;
                oProveedor.Estado = (EstadoGeneral)cmbEstado.SelectedItem;

                oProveedor = await bllProveedor.Save(oProveedor);

                if (oProveedor != null)
                    this.LoadData();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            IProveedorBLL bllProveedor = new ProveedorBLL();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    this.ChangeState(EstadoMantenimiento.Borrar);

                    Proveedor oProveedor = this.dgvDatos.SelectedRows[0].DataBoundItem as Proveedor;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oProveedor.Id} {oProveedor.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await bllProveedor.Delete(oProveedor.Id);
                        this.LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Proveedor oProveedor = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    // Cambiar de estado
                    this.ChangeState(EstadoMantenimiento.Editar);
                    oProveedor = this.dgvDatos.SelectedRows[0].DataBoundItem as Proveedor;

                    this.txtIdProveedor.Text = oProveedor.Id.ToString();
                    this.txtDescripcion.Text = oProveedor.Nombre;
                    cmbEstado.SelectedValue = oProveedor.Estado;
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
