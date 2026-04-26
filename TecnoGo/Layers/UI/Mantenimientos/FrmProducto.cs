using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            this.txtIdProducto.Enabled = false;
            this.txtNombre.Enabled = false;
            this.txtModelo.Enabled = false;
            this.txtCaracteristicas.Enabled = false;
            this.txtExtras.Enabled = false;
            this.txtPrecio.Enabled = false;
            this.txtCantidad.Enabled = false;

            this.btnAceptar.Enabled = false;
            this.btnCancelar.Enabled = false;

            this.cmbMarca.Enabled = false;
            this.cmbDispositivo.Enabled = false;
            this.cmbProveedor.Enabled = false;
            this.cmbColor.Enabled = false;
            this.cmbEstado.Enabled = false;
           
            this.pbImagen.Enabled = false;
            this.btnDocumento.Enabled = false;
            this.btnVerDoc.Enabled = false;

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
                    this.txtCantidad.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;

                    this.cmbMarca.Enabled = true;
                    this.cmbDispositivo.Enabled = true;
                    this.cmbProveedor.Enabled = true;
                    this.cmbColor.Enabled = true;
                    this.cmbEstado.Enabled = true;

                    this.pbImagen.Enabled = true;
                    this.btnDocumento.Enabled = true;
                    this.btnVerDoc.Enabled = true;

                    txtIdProducto.Focus();

                    break;
                case EstadoMantenimiento.Editar:
                    this.txtIdProducto.Enabled = false;
                    this.txtNombre.Enabled = true;
                    this.txtModelo.Enabled = true;
                    this.txtCaracteristicas.Enabled = true;
                    this.txtExtras.Enabled = true;
                    this.txtPrecio.Enabled = true;
                    this.txtCantidad.Enabled = true;

                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.Enabled = true;

                    this.cmbMarca.Enabled = true;
                    this.cmbDispositivo.Enabled = true;
                    this.cmbProveedor.Enabled = true;
                    this.cmbColor.Enabled = true;
                    this.cmbEstado.Enabled = true;

                    this.pbImagen.Enabled = true;
                    this.btnDocumento.Enabled = true;
                    this.btnVerDoc.Enabled = true;

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

                //Cargar el DataGridView
                this.dgvDatos.DataSource = await bllProducto.GetAll();

                dgvDatos.Columns["Fotografia"].Visible = false;
                dgvDatos.Columns["Documento"].Visible = false;
                dgvDatos.Columns["Caracteristicas"].Visible = false;
                dgvDatos.Columns["Extras"].Visible = false;

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
                cmbProveedor.DataSource = listaProveedores;
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
                oProducto.IdMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                oProducto.Modelo = this.txtModelo.Text;
                oProducto.IdTipoDispositivo = Convert.ToInt32(cmbDispositivo.SelectedValue);
                oProducto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                oProducto.Color = (Colores)cmbColor.SelectedItem;
                oProducto.Caracteristicas = this.txtCaracteristicas.Text;
                oProducto.Extras = this.txtExtras.Text;
                oProducto.CantidadStock = int.Parse(this.txtCantidad.Text);
                oProducto.Precio = double.Parse(this.txtPrecio.Text);
                oProducto.Estado = (EstadoGeneral)cmbEstado.SelectedItem;

                if (pbImagen.Tag != TecnoGo.Properties.Resources.imagenG)
                {
                    oProducto.Fotografia = (byte[])pbImagen.Tag;
                }
                else
                {
                    oProducto.Fotografia = null;
                }

                if (btnDocumento.Tag is byte[] archivo && archivo.Length > 0)
                {
                    oProducto.Documento = archivo;
                }
                else
                {
                    oProducto.Documento = null;
                }

                oProducto = await bllProducto.Save(oProducto);


                if (oProducto != null)
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
            IProductoBLL bllProducto = new ProductoBLL();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    this.ChangeState(EstadoMantenimiento.Borrar);

                    Producto oProducto = this.dgvDatos.SelectedRows[0].DataBoundItem as Producto;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oProducto.Id} {oProducto.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await bllProducto.Delete(oProducto.Id);
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
            Producto oProducto = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    // Cambiar de estado
                    this.ChangeState(EstadoMantenimiento.Editar);
                    oProducto = this.dgvDatos.SelectedRows[0].DataBoundItem as Producto;

                    this.txtIdProducto.Text = oProducto.Id.ToString();
                    this.txtNombre.Text = oProducto.Nombre;
                    cmbMarca.SelectedItem = oProducto.IdMarca;
                    this.txtModelo.Text = oProducto.Modelo;
                    cmbDispositivo.SelectedItem = oProducto.IdTipoDispositivo;
                    cmbProveedor.SelectedItem = oProducto.IdProveedor;
                    cmbColor.SelectedItem = oProducto.Color;
                    this.txtCaracteristicas.Text = oProducto.Caracteristicas;
                    this.txtExtras.Text = oProducto.Extras;
                    this.txtCantidad.Text = oProducto.CantidadStock.ToString();
                    this.txtPrecio.Text = oProducto.Precio.ToString();
                    cmbEstado.SelectedItem = oProducto.Estado;

                    this.pbImagen.Image = new Bitmap(new MemoryStream(oProducto.Fotografia));
                    this.pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbImagen.Tag = oProducto.Fotografia;

                    if (oProducto.Documento != null)
                    {
                        btnDocumento.Tag = oProducto.Documento;
                        lblDocumento.Text = "Documento cargado";
                    }
                    else
                    {
                        btnDocumento.Tag = null;
                        lblDocumento.Text = "Sin documento";
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

        private void pbImagen_Click(object sender, EventArgs e)
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

        private void btnDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Seleccione el documento Word";
                ofd.Filter = "Documentos Word (*.doc;*.docx)|*.doc;*.docx|Todos los archivos (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.FileName = "";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    // Leer el archivo como byte[]
                    byte[] archivoBytes = File.ReadAllBytes(ofd.FileName);

                    // Guardarlo en el Tag
                    btnDocumento.Tag = archivoBytes;

                    // Mostrar el nombre del archivo
                    lblDocumento.Text = Path.GetFileName(ofd.FileName);
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
