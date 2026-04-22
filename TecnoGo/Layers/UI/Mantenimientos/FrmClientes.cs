using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnoGo.Enumeraciones;
using TecnoGo.Extensions;
using TecnoGo.Layers.BLL;
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;
using TecnoGo.Util;

namespace TecnoGo.Layers.UI.Mantenimientos
{
    public partial class FrmClientes : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
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

        //Llenar los campos del nombre 
        private void LlenarCamposDesdeNombreCompleto(string nombreCompleto)
        {
            string[] partes = nombreCompleto.Trim().Split(' ');

            if (partes.Length < 2)
            {
                txtNombre.Text = partes[0];
                txtApellido1.Text = "";
                txtApellido2.Text = "";
                return;
            }

            if (partes.Length == 2)
            {
                txtNombre.Text = partes[0];
                txtApellido1.Text = partes[1];
                txtApellido2.Text = "";
                return;
            }

            // Para 3 o más partes
            txtApellido2.Text = partes[partes.Length - 1];
            txtApellido1.Text = partes[partes.Length - 2];

            // Todo lo anterior son nombres
            txtNombre.Text = string.Join(" ", partes.Take(partes.Length - 2));
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string Identificacion = txtIdentificacion.Text.Trim();

            if (string.IsNullOrWhiteSpace(Identificacion))
            {
                MessageBox.Show("Debe ingresar un número de cédula.");
                return;
            }

            // Consulta al API de Hacienda
            HaciendaBLL api = new HaciendaBLL();
            string nombreCompleto = await api.ObtenerNombrePorCedula(Identificacion);

            if (string.IsNullOrEmpty(nombreCompleto))
            {
                MessageBox.Show("No se encontraron datos en Hacienda.");
                return;
            }

            LlenarCamposDesdeNombreCompleto(nombreCompleto);
        }

        ErrorProvider erp = new ErrorProvider();

        private void ChangeState(EstadoMantenimiento estado)
        {
            erp.Clear();
            
            this.txtIdentificacion.Clear();
            this.txtNombre.Clear();
            this.txtApellido1.Clear();
            this.txtApellido2.Clear();
            this.txtCorreo.Clear();
            this.txtTelefono.Clear();
            this.txtDireccion.Clear();

            if (pbImagen.Image != TecnoGo.Properties.Resources.imagenG)
                pbImagen.Image.Dispose();

            pbImagen.Image = TecnoGo.Properties.Resources.imagenG;
            pbImagen.Tag = TecnoGo.Properties.Resources.imagenG;

            this.txtIdentificacion.Enabled = false;
            this.txtNombre.Enabled = false;
            this.txtApellido1.Enabled = false;
            this.txtApellido2.Enabled = false;
            this.txtCorreo.Enabled = false;
            this.txtTelefono.Enabled = false;
            this.txtDireccion.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;
            this.btnBuscar.Enabled = false;
            this.cmbTipoId.Enabled = false;
            this.cmbProvincia.Enabled = false;
            this.cmbEstado.Enabled = false;
            this.rdbFemenino.Enabled = false;
            this.rdbMasculino.Enabled = false;
            this.pbImagen.Enabled = false;

            // Coloca el primer item por defecto
            if (cmbProvincia.Items.Count > 0)
                this.cmbProvincia.SelectedIndex = 0;
            if (cmbEstado.Items.Count > 0)
                this.cmbEstado.SelectedIndex = 0;

            switch (estado)
            {
                case EstadoMantenimiento.Nuevo:
                    this.cmbTipoId.Enabled = true;

                    cmbTipoId_SelectedIndexChanged(null, null);

                    this.txtIdentificacion.Enabled = true;
                    this.txtCorreo.Enabled = true;
                    this.txtTelefono.Enabled = true;
                    this.txtDireccion.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.cmbProvincia.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.rdbFemenino.Enabled = true;
                    this.rdbMasculino.Enabled = true;
                    this.pbImagen.Enabled = true;

                    cmbTipoId.Focus();

                    break;
                case EstadoMantenimiento.Editar:
                    this.cmbTipoId.Enabled = false;

                    cmbTipoId_SelectedIndexChanged(null, null);

                    this.txtIdentificacion.Enabled = false;
                    this.txtCorreo.Enabled = true;
                    this.txtTelefono.Enabled = true;
                    this.txtDireccion.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                    this.cmbProvincia.Enabled = true;
                    this.cmbEstado.Enabled = true;
                    this.rdbFemenino.Enabled = true;
                    this.rdbMasculino.Enabled = true;
                    this.pbImagen.Enabled = true;

                    txtCorreo.Focus();

                    break;
                case EstadoMantenimiento.Borrar:
                    break;
                case EstadoMantenimiento.Ninguno:
                    break;
            }
        }

        private async void LoadData()
        {
            IClienteBLL bllCliente = new ClienteBLL();
            try
            {
                // Cambiar el estado
                this.ChangeState(EstadoMantenimiento.Ninguno);

                // dgvDatos.RowTemplate.Height = 100;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Delay forzado
                await Task.Delay(500);

                // Cargar el DataGridView
                this.dgvDatos.DataSource = await bllCliente.GetAll();

                dgvDatos.Columns["Fotografia"].Visible = false;
                dgvDatos.Columns["Direccion"].Visible = false;
                dgvDatos.Columns["Sexo"].Visible = false;
                dgvDatos.Columns["Provincia"].Visible = false;

                // Cargar los combos
                var provincias = ProvinciasHelper.ObtenerProvincias();

                cmbProvincia.DataSource = provincias;
                cmbProvincia.DisplayMember = "Descripcion"; // lista que muestra
                cmbProvincia.ValueMember = "IdProvincia"; // valor interno
                cmbProvincia.SelectedIndex = 0; 

                cmbEstado.DataSource = Enum.GetValues(typeof(EstadoGeneral));
                cmbEstado.SelectedIndex = 0;

                cmbTipoId.DataSource = Enum.GetValues(typeof(TipoIdentificacion));
                cmbTipoId.SelectedIndex = 0;
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
            IClienteBLL bllCliente = new ClienteBLL();
            erp = new ErrorProvider();
            try
            {
                erp.Clear();
                Cliente oCliente = new Cliente();

                if (string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    erp.SetError(txtIdentificacion, "El Id del cliente es requerido.");
                    txtIdentificacion.Focus();
                    return;
                }

                if (!LeerDatos.Es_Email(this.txtCorreo))
                {
                    erp.SetError(this.txtCorreo, "Campo correo no es correcto");
                    return;
                }

                oCliente.Id = this.txtIdentificacion.Text;
                oCliente.TipoIdentificacion = (TipoIdentificacion)cmbTipoId.SelectedItem;
                oCliente.Nombre = this.txtNombre.Text;
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

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            IClienteBLL bllCliente = new ClienteBLL();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    this.ChangeState(EstadoMantenimiento.Borrar);

                    Cliente oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Cliente;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oCliente.Id.Trim()} {oCliente.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await bllCliente.Delete(oCliente.Id);
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
            Cliente oCliente = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    // Cambiar de estado
                    this.ChangeState(EstadoMantenimiento.Editar);
                    oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Cliente;

                    this.txtIdentificacion.Text = oCliente.Id;
                    cmbTipoId.SelectedItem = oCliente.TipoIdentificacion;
                    this.txtNombre.Text = oCliente.Nombre;
                    this.txtApellido1.Text = oCliente.Apellido1;
                    this.txtApellido2.Text = oCliente.Apellido2;

                    if (oCliente.Sexo == "Masculino")
                    {
                        rdbMasculino.Checked = true;
                    }
                    else if (oCliente.Sexo == "Femenino")
                    {
                        rdbFemenino.Checked = true;
                    }

                    this.txtCorreo.Text = oCliente.Correo;
                    this.txtTelefono.Text = oCliente.Telefono;
                    cmbProvincia.Text = oCliente.Provincia;
                    this.txtDireccion.Text = oCliente.Direccion;
                    cmbEstado.SelectedItem = oCliente.Estado;

                    this.pbImagen.Image = new Bitmap(new MemoryStream(oCliente.Fotografia));
                    this.pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbImagen.Tag = oCliente.Fotografia;
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

        private void pctImagen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opt = new OpenFileDialog();
                opt.Title = "Seleccione la Imagen ";
                opt.SupportMultiDottedExtensions = true;
                opt.DefaultExt = "*.jpg";
                opt.Filter = "Archivos de Imagenes (*.jpg)|*.jpg| All files (*.*)|*.*";
                opt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                opt.FileName = "";

                if (opt.ShowDialog(this) == DialogResult.OK)
                {
                    //ruta = opt.FileName.Trim();
                    this.pbImagen.ImageLocation = opt.FileName;
                    pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;

                    byte[] cadenaBytes = File.ReadAllBytes(opt.FileName);

                    // Guarla la imagenen Bytes en el Tag de la imagen.
                    pbImagen.Tag = (byte[])cadenaBytes;
                }
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoId.Text == "Extranjero")
            {
                this.txtNombre.Enabled = true;
                this.txtApellido1.Enabled = true;
                this.txtApellido2.Enabled = true;
                this.btnBuscar.Enabled = false;
            }
            else if (cmbTipoId.Text == "Nacional")
            {
                this.txtNombre.Enabled = false;
                this.txtApellido1.Enabled = false;
                this.txtApellido2.Enabled = false;
                this.btnBuscar.Enabled = true;
            }
        }
    }
}

