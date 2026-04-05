using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecnoGo.Layers.UI.Procesos
{
    public partial class FrmFirma : Form
    {
        private Bitmap firmaBitmap;
        private Point lastPoint;
        private bool isDrawing = false;

        public byte[] nFirma { get; set; }

        public FrmFirma()
        {
            InitializeComponent();
        }

        private void FrmFirma_Load(object sender, EventArgs e)
        {
            if (nFirma != null)
            {
                using (var ms = new MemoryStream(nFirma))
                    firmaBitmap = new Bitmap(ms);
            }
            else
            {
                firmaBitmap = new Bitmap(pcbFirma.Width, pcbFirma.Height);
            }

            pcbFirma.Image = firmaBitmap;

            pcbFirma.MouseDown += pcbFirma_MouseDown;
            pcbFirma.MouseMove += pcbFirma_MouseMove;
            pcbFirma.MouseUp += pcbFirma_MouseUp;
        }

        private void pcbFirma_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;
            }
        }

        private void pcbFirma_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(firmaBitmap))
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawLine(pen, lastPoint, e.Location);
                }

                lastPoint = e.Location;
                pcbFirma.Invalidate();
            }
        }

        private void pcbFirma_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isDrawing = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            firmaBitmap = new Bitmap(pcbFirma.Width, pcbFirma.Height);
            pcbFirma.Image = firmaBitmap;
            nFirma = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var ms = new MemoryStream())
            {
                firmaBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                nFirma = ms.ToArray();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
