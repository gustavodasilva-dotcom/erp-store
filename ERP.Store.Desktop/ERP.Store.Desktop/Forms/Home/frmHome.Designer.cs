
namespace ERP.Store.Desktop.Forms.Home
{
    partial class frmHome
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerNewOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerNewItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerNewClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suppliersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerNewSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerNewEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordersToolStripMenuItem,
            this.inventoriesToolStripMenuItem,
            this.clientsToolStripMenuItem,
            this.suppliersToolStripMenuItem,
            this.employeesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerNewOrderToolStripMenuItem,
            this.getOrderToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.ordersToolStripMenuItem.Text = "Orders";
            // 
            // registerNewOrderToolStripMenuItem
            // 
            this.registerNewOrderToolStripMenuItem.Name = "registerNewOrderToolStripMenuItem";
            this.registerNewOrderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.registerNewOrderToolStripMenuItem.Text = "Register new order";
            this.registerNewOrderToolStripMenuItem.Click += new System.EventHandler(this.registerNewOrderToolStripMenuItem_Click);
            // 
            // getOrderToolStripMenuItem
            // 
            this.getOrderToolStripMenuItem.Name = "getOrderToolStripMenuItem";
            this.getOrderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.getOrderToolStripMenuItem.Text = "Get order";
            this.getOrderToolStripMenuItem.Click += new System.EventHandler(this.getOrderToolStripMenuItem_Click);
            // 
            // inventoriesToolStripMenuItem
            // 
            this.inventoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getItemToolStripMenuItem,
            this.registerNewItemToolStripMenuItem});
            this.inventoriesToolStripMenuItem.Name = "inventoriesToolStripMenuItem";
            this.inventoriesToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.inventoriesToolStripMenuItem.Text = "Inventories";
            // 
            // getItemToolStripMenuItem
            // 
            this.getItemToolStripMenuItem.Name = "getItemToolStripMenuItem";
            this.getItemToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.getItemToolStripMenuItem.Text = "Get item";
            this.getItemToolStripMenuItem.Click += new System.EventHandler(this.getItemToolStripMenuItem_Click);
            // 
            // registerNewItemToolStripMenuItem
            // 
            this.registerNewItemToolStripMenuItem.Name = "registerNewItemToolStripMenuItem";
            this.registerNewItemToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.registerNewItemToolStripMenuItem.Text = "Register new item";
            this.registerNewItemToolStripMenuItem.Click += new System.EventHandler(this.registerNewItemToolStripMenuItem_Click);
            // 
            // clientsToolStripMenuItem
            // 
            this.clientsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerNewClientToolStripMenuItem,
            this.findClientToolStripMenuItem});
            this.clientsToolStripMenuItem.Name = "clientsToolStripMenuItem";
            this.clientsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.clientsToolStripMenuItem.Text = "Clients";
            // 
            // registerNewClientToolStripMenuItem
            // 
            this.registerNewClientToolStripMenuItem.Name = "registerNewClientToolStripMenuItem";
            this.registerNewClientToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.registerNewClientToolStripMenuItem.Text = "Register new client";
            this.registerNewClientToolStripMenuItem.Click += new System.EventHandler(this.registerNewClientToolStripMenuItem_Click);
            // 
            // findClientToolStripMenuItem
            // 
            this.findClientToolStripMenuItem.Name = "findClientToolStripMenuItem";
            this.findClientToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.findClientToolStripMenuItem.Text = "Find client";
            this.findClientToolStripMenuItem.Click += new System.EventHandler(this.findClientToolStripMenuItem_Click);
            // 
            // suppliersToolStripMenuItem
            // 
            this.suppliersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getSupplierToolStripMenuItem,
            this.registerNewSupplierToolStripMenuItem});
            this.suppliersToolStripMenuItem.Name = "suppliersToolStripMenuItem";
            this.suppliersToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.suppliersToolStripMenuItem.Text = "Suppliers";
            // 
            // getSupplierToolStripMenuItem
            // 
            this.getSupplierToolStripMenuItem.Name = "getSupplierToolStripMenuItem";
            this.getSupplierToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.getSupplierToolStripMenuItem.Text = "Get supplier";
            this.getSupplierToolStripMenuItem.Click += new System.EventHandler(this.getSupplierToolStripMenuItem_Click);
            // 
            // registerNewSupplierToolStripMenuItem
            // 
            this.registerNewSupplierToolStripMenuItem.Name = "registerNewSupplierToolStripMenuItem";
            this.registerNewSupplierToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.registerNewSupplierToolStripMenuItem.Text = "Register new supplier";
            this.registerNewSupplierToolStripMenuItem.Click += new System.EventHandler(this.registerNewSupplierToolStripMenuItem_Click);
            // 
            // employeesToolStripMenuItem
            // 
            this.employeesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerNewEmployeeToolStripMenuItem,
            this.findEmployeeToolStripMenuItem});
            this.employeesToolStripMenuItem.Name = "employeesToolStripMenuItem";
            this.employeesToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.employeesToolStripMenuItem.Text = "Employees";
            // 
            // registerNewEmployeeToolStripMenuItem
            // 
            this.registerNewEmployeeToolStripMenuItem.Name = "registerNewEmployeeToolStripMenuItem";
            this.registerNewEmployeeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.registerNewEmployeeToolStripMenuItem.Text = "Register new employee";
            this.registerNewEmployeeToolStripMenuItem.Click += new System.EventHandler(this.registerNewEmployeeToolStripMenuItem_Click);
            // 
            // findEmployeeToolStripMenuItem
            // 
            this.findEmployeeToolStripMenuItem.Name = "findEmployeeToolStripMenuItem";
            this.findEmployeeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.findEmployeeToolStripMenuItem.Text = "Find employee";
            this.findEmployeeToolStripMenuItem.Click += new System.EventHandler(this.findEmployeeToolStripMenuItem_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmHome";
            this.Text = "Home";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suppliersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerNewEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerNewClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerNewItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerNewOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerNewSupplierToolStripMenuItem;
    }
}