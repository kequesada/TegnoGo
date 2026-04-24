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
using TecnoGo.Util;

namespace TecnoGo.Layers.UI.Mantenimientos
{
    public partial class FrmProducto : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public FrmProducto()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
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

            this.txtIdProducto.Clear();
            this.txtNombre.Clear();
            this.txtModelo.Clear();
            this.txtCaracteristicas.Clear();
            this.txtExtras.Clear();
            this.txtPrecio.Clear();
            

            if (pbImagen.Image != TecnoGo.Properties.Resources.imagenG)
                pbImagen.Image.Dispose();

            pbImagen.Image = TecnoGo.Properties.Resources.imagenG;
            pbImagen.Tag = TecnoGo.Properties.Resources.imagenG;

            if (pbDocumento.Image != TecnoGo.Properties.Resources.documentoG)
                pbDocumento.Image.Dispose();

            pbDocumento.Image = TecnoGo.Properties.Resources.documentoG;
            pbDocumento.Tag = TecnoGo.Properties.Resources.documentoG;


            this.txtIdProducto.Enabled = false;
            this.txtNombre.Enabled = false;
            this.txtModelo.Enabled = false;
            this.txtCaracteristicas.Enabled = false;
            this.txtExtras.Enabled = false;
            this.txtPrecio.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;
            this.numCantidad.Enabled = false;

            this.cmbMarca.Enabled = false;
            this.cmbDispositivo.Enabled = false;
            this.cmbProveedor.Enabled = false;
            this.cmbColor.Enabled = false;
            this.cmbEstado.Enabled = false;
           
            this.pbImagen.Enabled = false;
            this.pbDocumento.Enabled = false;

            // Coloca el primer item por defecto
            if (cmbMarca.Items.Count > 0)
                this.cmbMarca.SelectedIndex = 0;
            if (cmbDispositivo.Items.Count > 0)
                this.cmbDispositivo.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0)
                this.cmbProveedor.SelectedIndex = 0;
            if (cmbColor.Items.Count > 0)
                this.cmbColor.SelectedIndex = 0;
            if (cmbEstado.Items.Count > 0)
                this.cmbEstado.SelectedIndex = 0;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.txtIdProducto.Enabled = true;
                    this.txtNombre.Enabled = true;
                    this.txtModelo.Enabled = true;
                    this.txtCaracteristicas.Enabled = true;
                    this.txtExtras.Enabled = true;
                    this.txtPrecio.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.numCantidad.Enabled = true;

                    this.cmbMarca.Enabled = true;
                    this.cmbDispositivo.Enabled = true;
                    this.cmbProveedor.Enabled = true;
                    this.cmbColor.Enabled = true;
                    this.cmbEstado.Enabled = true;

                    this.pbImagen.Enabled = true;
                    this.pbDocumento.Enabled = true;

                    txtIdProducto.Focus();

                    break;
                case EstadoMantenimiento.Editar:
                    this.txtIdProducto.Enabled = false;
                    this.txtNombre.Enabled = true;
                    this.txtModelo.Enabled = true;
                    this.txtCaracteristicas.Enabled = true;
                    this.txtExtras.Enabled = true;
                    this.txtPrecio.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.numCantidad.Enabled = true;

                    this.cmbMarca.Enabled = true;
                    this.cmbDispositivo.Enabled = true;
                    this.cmbProveedor.Enabled = true;
                    this.cmbColor.Enabled = true;
                    this.cmbEstado.Enabled = true;

                    this.pbImagen.Enabled = true;
                    this.pbDocumento.Enabled = true;

                    txtNombre.Focus();

                    break;
                case EstadoMantenimiento.Borrar:
                    break;
                case EstadoMantenimiento.Ninguno:
                    break;
            }
        }

        private async void LoadData()
        {
            IProductoBLL bllProducto = new ProductoBLL();

            IMarcaBLL bllMarca = new MarcaBLL();
            ITipoDispositivoBLL bllDispositivo = new TipoDispositivoBLL();
            IProveedorBLL bllProveedor = new ProveedorBLL();
            try
            {
                // Cambiar el estado
                this.ChangeState(EstadoMantenimiento.Ninguno);

                // dgvDatos.RowTemplate.Height = 100;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Delay forzado
                await Task.Delay(500);

                /* Cargar el DataGridView
                this.dgvDatos.DataSource = await bllProducto.GetAll();

                dgvDatos.Columns["Fotografia"].Visible = false;
                dgvDatos.Columns["Documento"].Visible = false;
                dgvDatos.Columns["Caracteristicas"].Visible = false;
                dgvDatos.Columns["Extras"].Visible = false;*/

                // Cargar los combos
                List<Marca> listaMarcas = (List<Marca>)await bllMarca.GetAll();
                cmbMarca.DataSource = listaMarcas;
                cmbMarca.DisplayMember = "Nombre";      // Lo que se muestra
                cmbMarca.ValueMember = "IdMarca";       // El ID real
                cmbMarca.SelectedIndex = 0;            // Seleccionado

                List<TipoDispositivo> listaDispositivos = (List<TipoDispositivo>)await bllDispositivo.GetAll();
                cmbDispositivo.DataSource = listaDispositivos;
                cmbDispositivo.DisplayMember = "Nombre";
                cmbDispositivo.ValueMember = "IdTipoDispositivo";
                cmbDispositivo.SelectedIndex = 0;

                List<Proveedor> listaProveedores = (List<Proveedor>)await bllProveedor.GetAll();
                cmbProveedor.DataSource = listaDispositivos;
                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.SelectedIndex = 0;

                cmbEstado.DataSource = Enum.GetValues(typeof(EstadoGeneral));
                cmbEstado.SelectedIndex = 0;

                cmbColor.DataSource = Enum.GetValues(typeof(Colores));
                cmbColor.SelectedIndex = 0;
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
            IProductoBLL bllProducto = new ProductoBLL();
            erp = new ErrorProvider();
            try
            {
                erp.Clear();
                Producto oProducto = new Producto();

                if (string.IsNullOrEmpty(txtIdProducto.Text))
                {
                    erp.SetError(txtIdProducto, "El Id del producto es requerido.");
                    txtIdProducto.Focus();
                    return;
                }

                oProducto.Id = int.Parse(this.txtIdProducto.Text);
                oProducto.Nombre = this.txtNombre.Text;
                oProducto.IdMarca = (Marca)cmbMarca.SelectedItem;
                
                oCliente.Apellido1 = this.txtApellido1.Text;
                oCliente.Apellido2 = this.txtApellido2.Text;

                if (rdbMasculino.Checked)
                {
                    oCliente.Sexo = "Masculino";
                }
                else if (rdbFemenino.Checked)
                {
                    oCliente.Sexo = "Femenino";
                }

                oCliente.Correo = this.txtCorreo.Text;
                oCliente.Telefono = this.txtTelefono.Text;
                oCliente.Provincia = cmbProvincia.Text;
                oCliente.Direccion = this.txtDireccion.Text;
                oCliente.Estado = (EstadoGeneral)cmbEstado.SelectedItem;

                if (pbImagen.Tag != TecnoGo.Properties.Resources.imagenG)
                {
                    oCliente.Fotografia = (byte[])pbImagen.Tag;
                }
                else
                {
                    oCliente.Fotografia = null;
                }

                oCliente = await bllCliente.Save(oCliente);


                if (oCliente != null)
                    this.LoadData();

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
