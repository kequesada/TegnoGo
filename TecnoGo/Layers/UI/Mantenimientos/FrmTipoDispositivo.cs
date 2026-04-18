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
    public partial class FrmTipoDispositivo : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public FrmTipoDispositivo()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmTipoDispositivo_Load(object sender, EventArgs e)
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
            this.txtIdDispositivo.Clear();
            this.txtDescripcion.Clear();
            this.cmbEstado.SelectedIndex = 0;

            this.txtIdDispositivo.Enabled = false;
            this.txtDescripcion.Enabled = false;
            this.cmbEstado.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtIdDispositivo.Enabled = true;
                    this.txtDescripcion.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtIdDispositivo.Focus();
                    break;
                case EstadoMantenimiento.Editar:
                    this.txtIdDispositivo.Enabled = false;
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
            ITipoDispositivoBLL bllDispositivo = new TipoDispositivoBLL();
            try
            {
                // Cambiar el estado
                this.ChangeState(EstadoMantenimiento.Ninguno);

                // dgvDatos.RowTemplate.Height = 100;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Delay forzado
                await Task.Delay(500);

                // Cargar el DataGridView
                this.dgvDatos.DataSource = await bllDispositivo.GetAll();

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
            ITipoDispositivoBLL bllDispositivo = new TipoDispositivoBLL();
            erp = new ErrorProvider();
            try
            {
                erp.Clear();
                TipoDispositivo oDispositivo = new TipoDispositivo();

                if (string.IsNullOrEmpty(txtIdDispositivo.Text))
                {
                    erp.SetError(txtIdDispositivo, "El Id de la marca es requerido.");
                    txtIdDispositivo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    erp.SetError(txtDescripcion, "El nombre/descripción de la marca es requerido.");
                    txtDescripcion.Focus();
                    return;
                }

                oDispositivo.Id = int.Parse(this.txtIdDispositivo.Text);
                oDispositivo.Nombre = this.txtDescripcion.Text;
                oDispositivo.Estado = (EstadoGeneral)cmbEstado.SelectedItem;

                oDispositivo = await bllDispositivo.Save(oDispositivo);

                if (oDispositivo != null)
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
            ITipoDispositivoBLL bllDispositivo = new TipoDispositivoBLL();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    this.ChangeState(EstadoMantenimiento.Borrar);

                    TipoDispositivo oDispositivo = this.dgvDatos.SelectedRows[0].DataBoundItem as TipoDispositivo;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oDispositivo.Id} {oDispositivo.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await bllDispositivo.Delete(oDispositivo.Id);
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
            TipoDispositivo oDispositivo = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    // Cambiar de estado
                    this.ChangeState(EstadoMantenimiento.Editar);
                    oDispositivo = this.dgvDatos.SelectedRows[0].DataBoundItem as TipoDispositivo;

                    this.txtIdDispositivo.Text = oDispositivo.Id.ToString();
                    this.txtDescripcion.Text = oDispositivo.Nombre;
                    cmbEstado.SelectedValue = oDispositivo.Estado;
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
