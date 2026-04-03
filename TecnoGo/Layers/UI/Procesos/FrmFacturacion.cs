using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnoGo.Layers.BLL;
using TecnoGo.Layers.DAL;

namespace TecnoGo.Layers.UI.Procesos
{
    public partial class FrmFacturacion : Form
    {
        private double dolares;

        public FrmFacturacion()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            DolarBLL dolarBLL = new DolarBLL();
            // Configurar el webService para consumir el WebService del Banco Central
            dolares = double.Parse(this.txtCambio.Text) / dolarBLL.GetVentaDolar();

        }

        private void FrmFacturacion_Load(object sender, EventArgs e)
        {
            DolarBLL bllDolar = new DolarBLL();
            txtCambio.Text = "Venta Dolar " + bllDolar.GetVentaDolar();
        }
    }
}

