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
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;

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
            /*try
            {
                LoadData();
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
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
                MessageBox.Show("Debe ingresar una cédula.");
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
    }
}

