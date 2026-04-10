using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnoGo.Extensions;
using TecnoGo.Layers.UI.Mantenimientos;
using TecnoGo.Layers.UI.Procesos;
using TecnoGo.Properties;

namespace TecnoGo.Layers.UI
{
    public partial class FrmPrincipal : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
       
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                //Commit generados por terminal
                Utils.CultureInfo();
                this.Text = ConfigurationManager.AppSettings["NombreEmpresa"] + " " + Application.ProductName + " Versión:  " + Application.ProductVersion;

                toolStripStatusLblMensaje.Text = "Usuario Conectado: " + Settings.Default.Login + "/" + Settings.Default.Nombre;

                if (!Directory.Exists(@"C:\temp"))
                    Directory.CreateDirectory(@"C:\temp");

                _myLogControlEventos.InfoFormat("Conectado a Form Principal");



                // Activar Seguridad
                //Seguridad();

            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes oForm;
            try
            {
                oForm = new FrmClientes();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void marcasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmMarca oForm;
            try
            {
                oForm = new FrmMarca();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tipoDeDispositivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoDispositivo oForm;
            try
            {
                oForm = new FrmTipoDispositivo();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProveedor oForm;
            try
            {
                oForm = new FrmProveedor();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProducto oForm;
            try
            {
                oForm = new FrmProducto();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInventario oForm;
            try
            {
                oForm = new FrmInventario();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFacturacion oForm;
            try
            {
                oForm = new FrmFacturacion();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemAcercaDe_Click(object sender, EventArgs e)
        {
            AbxAcercade oForm;
            try
            {
                oForm = new AbxAcercade();
                oForm.MdiParent = this;
                oForm.Show();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        /*private void Seguridad()
        {
            List<string> menus = new List<string>();

            // Se deshabilita TODO primero
            foreach (ToolStripItem opcionMenu in this.mspMenuPrincipal.Items) //para cada opción de la barra de menú
            {
                // deshabita todos !
                ((ToolStripItem)(opcionMenu)).Enabled = false;
            }

            // Tabla Rol
            // IdRol DescripcionRol
            // 1   Administrador
            // 2   Vendedor
            // 3   Reportes
            // Siempre permitir el MENU Acercade  
            menus.Add("toolStripMenuItemAcercaDe");

            // Admin
            if (Settings.Default.RolId.Equals("1"))
            {
                menus.Add("toolStripMenuItemMantenimientos");
                menus.Add("toolStripMenuItemProcesos");
                menus.Add("reportesToolStripMenuItemReportes");
                menus.Add("administracionToolStripMenuItem");
            }

            // Vendedor
            if (Settings.Default.RolId.Equals("2"))
            {
                menus.Add("toolStripMenuItemMantenimientos");
                menus.Add("toolStripMenuItemProcesos");
            }

            // Reportes
            if (Settings.Default.RolId.Equals("3"))
            {
                menus.Add("reportesToolStripMenuItemReportes");
            }


            foreach (ToolStripItem opcionMenu in this.mspMenuPrincipal.Items) //para cada opción de la barra de menú
            {
                if (opcionMenu is ToolStripDropDownButton)
                {
                    foreach (ToolStripMenuItem oToolStripMenuItem in ((ToolStripDropDownButton)opcionMenu).DropDownItems)
                    {
                        oToolStripMenuItem.Enabled = menus.Exists(p => p.Equals(oToolStripMenuItem.Name, StringComparison.InvariantCultureIgnoreCase));
                    }
                }
                // Habilita solo las opciones que se encuentrna en la lista "menu"
                opcionMenu.Enabled = menus.Exists(p => p.Equals(opcionMenu.Name, StringComparison.InvariantCultureIgnoreCase));
            }

        }*/
    }
}
