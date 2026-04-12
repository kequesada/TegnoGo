namespace TecnoGo.Layers.UI
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.sttBarraInferior = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.mspMenuPrincipal = new System.Windows.Forms.MenuStrip();
            this.mantenimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marcasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoDeDispositivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.inventarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.facturacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItemReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.productoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItemUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.sttBarraInferior.SuspendLayout();
            this.mspMenuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // sttBarraInferior
            // 
            this.sttBarraInferior.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sttBarraInferior.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblMensaje});
            this.sttBarraInferior.Location = new System.Drawing.Point(0, 418);
            this.sttBarraInferior.Name = "sttBarraInferior";
            this.sttBarraInferior.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.sttBarraInferior.Size = new System.Drawing.Size(800, 32);
            this.sttBarraInferior.TabIndex = 1;
            // 
            // toolStripStatusLblMensaje
            // 
            this.toolStripStatusLblMensaje.Name = "toolStripStatusLblMensaje";
            this.toolStripStatusLblMensaje.Size = new System.Drawing.Size(19, 25);
            this.toolStripStatusLblMensaje.Text = "-";
            // 
            // mspMenuPrincipal
            // 
            this.mspMenuPrincipal.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mspMenuPrincipal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mspMenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mantenimientosToolStripMenuItem,
            this.toolStripMenuItemProcesos,
            this.reportesToolStripMenuItemReportes,
            this.administracionToolStripMenuItem,
            this.toolStripMenuItemAcercaDe});
            this.mspMenuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mspMenuPrincipal.Name = "mspMenuPrincipal";
            this.mspMenuPrincipal.Size = new System.Drawing.Size(800, 40);
            this.mspMenuPrincipal.TabIndex = 2;
            this.mspMenuPrincipal.Text = "menuStrip1";
            // 
            // mantenimientosToolStripMenuItem
            // 
            this.mantenimientosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.marcasToolStripMenuItem1,
            this.tipoDeDispositivosToolStripMenuItem,
            this.proveedoresToolStripMenuItem,
            this.productosToolStripMenuItem1,
            this.inventarioToolStripMenuItem});
            this.mantenimientosToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.mantenimiento;
            this.mantenimientosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mantenimientosToolStripMenuItem.Name = "mantenimientosToolStripMenuItem";
            this.mantenimientosToolStripMenuItem.Size = new System.Drawing.Size(188, 36);
            this.mantenimientosToolStripMenuItem.Text = "Mantenimientos";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.cliente;
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // marcasToolStripMenuItem1
            // 
            this.marcasToolStripMenuItem1.Image = global::TecnoGo.Properties.Resources.marca;
            this.marcasToolStripMenuItem1.Name = "marcasToolStripMenuItem1";
            this.marcasToolStripMenuItem1.Size = new System.Drawing.Size(274, 34);
            this.marcasToolStripMenuItem1.Text = "Marcas";
            this.marcasToolStripMenuItem1.Click += new System.EventHandler(this.marcasToolStripMenuItem1_Click);
            // 
            // tipoDeDispositivosToolStripMenuItem
            // 
            this.tipoDeDispositivosToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.tipodispositivo;
            this.tipoDeDispositivosToolStripMenuItem.Name = "tipoDeDispositivosToolStripMenuItem";
            this.tipoDeDispositivosToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.tipoDeDispositivosToolStripMenuItem.Text = "Tipo de dispositivos";
            this.tipoDeDispositivosToolStripMenuItem.Click += new System.EventHandler(this.tipoDeDispositivosToolStripMenuItem_Click);
            // 
            // proveedoresToolStripMenuItem
            // 
            this.proveedoresToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.proveedor;
            this.proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            this.proveedoresToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.proveedoresToolStripMenuItem.Text = "Proveedores";
            this.proveedoresToolStripMenuItem.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // productosToolStripMenuItem1
            // 
            this.productosToolStripMenuItem1.Image = global::TecnoGo.Properties.Resources.producto;
            this.productosToolStripMenuItem1.Name = "productosToolStripMenuItem1";
            this.productosToolStripMenuItem1.Size = new System.Drawing.Size(274, 34);
            this.productosToolStripMenuItem1.Text = "Productos";
            this.productosToolStripMenuItem1.Click += new System.EventHandler(this.productosToolStripMenuItem1_Click);
            // 
            // inventarioToolStripMenuItem
            // 
            this.inventarioToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.inventario;
            this.inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            this.inventarioToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.inventarioToolStripMenuItem.Text = "Inventario";
            this.inventarioToolStripMenuItem.Click += new System.EventHandler(this.inventarioToolStripMenuItem_Click);
            // 
            // toolStripMenuItemProcesos
            // 
            this.toolStripMenuItemProcesos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facturacionToolStripMenuItem});
            this.toolStripMenuItemProcesos.Image = global::TecnoGo.Properties.Resources.proceso;
            this.toolStripMenuItemProcesos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemProcesos.Name = "toolStripMenuItemProcesos";
            this.toolStripMenuItemProcesos.Size = new System.Drawing.Size(131, 36);
            this.toolStripMenuItemProcesos.Text = "Procesos";
            // 
            // facturacionToolStripMenuItem
            // 
            this.facturacionToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.factura;
            this.facturacionToolStripMenuItem.Name = "facturacionToolStripMenuItem";
            this.facturacionToolStripMenuItem.Size = new System.Drawing.Size(203, 34);
            this.facturacionToolStripMenuItem.Text = "Facturación";
            this.facturacionToolStripMenuItem.Click += new System.EventHandler(this.facturacionToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItemReportes
            // 
            this.reportesToolStripMenuItemReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productoToolStripMenuItem,
            this.facturaToolStripMenuItem,
            this.clientesToolStripMenuItem1,
            this.grToolStripMenuItem});
            this.reportesToolStripMenuItemReportes.Image = global::TecnoGo.Properties.Resources.reporte;
            this.reportesToolStripMenuItemReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reportesToolStripMenuItemReportes.Name = "reportesToolStripMenuItemReportes";
            this.reportesToolStripMenuItemReportes.Size = new System.Drawing.Size(130, 36);
            this.reportesToolStripMenuItemReportes.Text = "Reportes";
            // 
            // productoToolStripMenuItem
            // 
            this.productoToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.productosR;
            this.productoToolStripMenuItem.Name = "productoToolStripMenuItem";
            this.productoToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.productoToolStripMenuItem.Text = "Productos";
            // 
            // facturaToolStripMenuItem
            // 
            this.facturaToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.factura;
            this.facturaToolStripMenuItem.Name = "facturaToolStripMenuItem";
            this.facturaToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.facturaToolStripMenuItem.Text = "Facturas";
            // 
            // clientesToolStripMenuItem1
            // 
            this.clientesToolStripMenuItem1.Image = global::TecnoGo.Properties.Resources.clientesR;
            this.clientesToolStripMenuItem1.Name = "clientesToolStripMenuItem1";
            this.clientesToolStripMenuItem1.Size = new System.Drawing.Size(195, 34);
            this.clientesToolStripMenuItem1.Text = "Clientes";
            // 
            // grToolStripMenuItem
            // 
            this.grToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.graficos;
            this.grToolStripMenuItem.Name = "grToolStripMenuItem";
            this.grToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.grToolStripMenuItem.Text = "Gráfica";
            // 
            // administracionToolStripMenuItem
            // 
            this.administracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosToolStripMenuItemUsuarios});
            this.administracionToolStripMenuItem.Image = global::TecnoGo.Properties.Resources.seguridad;
            this.administracionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.administracionToolStripMenuItem.Name = "administracionToolStripMenuItem";
            this.administracionToolStripMenuItem.Size = new System.Drawing.Size(141, 36);
            this.administracionToolStripMenuItem.Text = "Seguridad";
            // 
            // usuariosToolStripMenuItemUsuarios
            // 
            this.usuariosToolStripMenuItemUsuarios.Image = global::TecnoGo.Properties.Resources.usuario;
            this.usuariosToolStripMenuItemUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usuariosToolStripMenuItemUsuarios.Name = "usuariosToolStripMenuItemUsuarios";
            this.usuariosToolStripMenuItemUsuarios.Size = new System.Drawing.Size(278, 42);
            this.usuariosToolStripMenuItemUsuarios.Text = "Usuarios";
            this.usuariosToolStripMenuItemUsuarios.Click += new System.EventHandler(this.usuariosToolStripMenuItemUsuarios_Click);
            // 
            // toolStripMenuItemAcercaDe
            // 
            this.toolStripMenuItemAcercaDe.Image = global::TecnoGo.Properties.Resources.acercade;
            this.toolStripMenuItemAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemAcercaDe.Name = "toolStripMenuItemAcercaDe";
            this.toolStripMenuItemAcercaDe.Size = new System.Drawing.Size(48, 36);
            this.toolStripMenuItemAcercaDe.Click += new System.EventHandler(this.toolStripMenuItemAcercaDe_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mspMenuPrincipal);
            this.Controls.Add(this.sttBarraInferior);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPrincipal";
            this.Text = "TecnoGo";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.sttBarraInferior.ResumeLayout(false);
            this.sttBarraInferior.PerformLayout();
            this.mspMenuPrincipal.ResumeLayout(false);
            this.mspMenuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sttBarraInferior;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblMensaje;
        private System.Windows.Forms.MenuStrip mspMenuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProcesos;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItemReportes;
        private System.Windows.Forms.ToolStripMenuItem administracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItemUsuarios;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAcercaDe;
        private System.Windows.Forms.ToolStripMenuItem mantenimientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem inventarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tipoDeDispositivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem grToolStripMenuItem;
    }
}