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

        
    }
}
