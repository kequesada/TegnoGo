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
    public partial class FrmMarca : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public FrmMarca()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMarca_Load(object sender, EventArgs e)
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
            this.txtIdMarca.Clear();
            this.txtDescripcion.Clear();
            this.cmbEstado.SelectedIndex = -1;

            this.txtIdMarca.Enabled = false;
            this.txtDescripcion.Enabled = false;
            this.cmbEstado.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtIdMarca.Enabled = true;
                    this.txtDescripcion.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    txtIdMarca.Focus();
                    break;
                case EstadoMantenimiento.Editar:
                    this.txtIdMarca.Enabled = false;
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
            IMarcaBLL bllMarca = new MarcaBLL();
            try
            {
                // Cambiar el estado
                this.ChangeState(EstadoMantenimiento.Ninguno);

                // dgvDatos.RowTemplate.Height = 100;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Delay forzado
                await Task.Delay(500);

                // Cargar el DataGridView
                this.dgvDatos.DataSource = await bllMarca.GetAll();

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
            IMarcaBLL bllMarca = new MarcaBLL();
            erp = new ErrorProvider();
            try
            {
                erp.Clear();
                Marca oMarca = new Marca();

                if (string.IsNullOrEmpty(txtIdMarca.Text))
                {
                    erp.SetError(txtIdMarca, "El Id de la marca es requerido.");
                    txtIdMarca.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    erp.SetError(txtDescripcion, "El nombre/descripción de la marca es requerido.");
                    txtDescripcion.Focus();
                    return;
                }

                oMarca.Id = int.Parse(this.txtIdMarca.Text);
                oMarca.Nombre = this.txtDescripcion.Text;
                oMarca.Estado = (EstadoGeneral)cmbEstado.SelectedItem;

                oMarca = await bllMarca.Save(oMarca);

                if (oMarca != null)
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
            IMarcaBLL bllMarca = new MarcaBLL();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    this.ChangeState(EstadoMantenimiento.Borrar);

                    Marca oMarca = this.dgvDatos.SelectedRows[0].DataBoundItem as Marca;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oMarca.Id} {oMarca.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await bllMarca.Delete(oMarca.Id);
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
            Marca oMarca = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    // Cambiar de estado
                    this.ChangeState(EstadoMantenimiento.Editar);
                    oMarca = this.dgvDatos.SelectedRows[0].DataBoundItem as Marca;

                    this.txtIdMarca.Text = oMarca.Id.ToString();
                    this.txtDescripcion.Text = oMarca.Nombre;
                    cmbEstado.SelectedItem = oMarca.Estado;
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
