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
using TecnoGo.Extensions;
using TecnoGo.Layers.BLL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;
using TecnoGo.Layers.UI.Mantenimientos;
using TecnoGo.Properties;

namespace TecnoGo.Layers.UI
{
    public partial class FrmLogin : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private int contador = 0;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            /* Debe validar los datos requeridos
            IUsuarioBLL bllUsuario = new UsuarioBLL();
            epError.Clear();
            Usuario oUsuario = null;

            try
            {

                if (string.IsNullOrEmpty(this.txtLogin.Text))
                {
                    epError.SetError(txtLogin, "Usuario requerido");
                    this.txtLogin.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    epError.SetError(txtPassword, "Contraseña requerida");
                    this.txtPassword.Focus();
                    return;
                }



                oUsuario = bllUsuario.Login(this.txtLogin.Text,
                                           this.txtPassword.Text);

                if (oUsuario == null)
                {
                    ++contador;
                    MessageBox.Show("Error en el acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Si el contador es 3 cierre la aplicación
                    if (contador == 3)
                    {
                        // se devuelve Cancel
                        MessageBox.Show("Se equivocó en 3 ocasiones, el Sistema se Cerrará por seguridad", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _myLogControlEventos.WarnFormat("Se equivocó + de 3 ocasiones Login: {0}", this.txtLogin.Text);
                        this.DialogResult = DialogResult.Cancel;
                        Application.Exit();
                    }
                }
                else
                {

                    Settings.Default.Login = this.txtLogin.Text.Trim();
                    Settings.Default.Nombre = oUsuario.Nombre;
                    Settings.Default.RolId = oUsuario.Id.ToString();

                    //EfectoConexionNoAsync();
                    bool respuesta = await EfectoConexion();

                    // Log de errores
                    _myLogControlEventos.InfoFormat("Accedió a la aplicación :{0}", Settings.Default.Nombre);
                    this.DialogResult = DialogResult.OK;

                }
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }*/
            FrmPrincipal oForm;
            oForm = new FrmPrincipal();
            oForm.Show();
        }

        private async Task<bool> EfectoConexion()
        {
            //Efecto en la barra de entrada.
            toolStripPbBarra.Visible = true;
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(150);
                //Thread.Sleep(100);
                this.toolStripPbBarra.Value += 10;
                this.sttBarraInferior.Refresh();
            }
            return true;
        }
    }
}
