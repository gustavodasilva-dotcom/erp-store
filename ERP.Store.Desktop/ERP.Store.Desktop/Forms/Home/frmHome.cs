using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Home
{
    public partial class frmHome : Form
    {
        private dynamic User { get; set; }

        public frmHome(dynamic user)
        {
            User = user;

            InitializeComponent();
        }

        private void registerNewEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var employeeDetails = new Employees.frmEmployeeDetails(User, OperationType.Create);

                employeeDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void findEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var findEmployee = new Find.frmFind(User, SearchType.Employee);

                findEmployee.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void registerNewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var clientDetails = new Clients.frmClientDetails(User, OperationType.Create);

                clientDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void findClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var findClient = new Find.frmFind(User, SearchType.Client);

                findClient.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void getItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var getItem = new Find.frmFind(User, SearchType.Inventory);

                getItem.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void registerNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inventoryDetails = new Inventories.frmInventoryDetails(User, OperationType.Create);

                inventoryDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void registerNewOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var orderDetails = new Orders.frmOrderDetails(User, OperationType.Create);

                orderDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void getOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var getOrder = new Find.frmFind(User, SearchType.Order);

                getOrder.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void registerNewSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var supplierDetails = new Suppliers.frmSupplierDetails(User, OperationType.Create);

                supplierDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }

        private void getSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var getSupplier = new Find.frmFind(User, SearchType.Supplier);

                getSupplier.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occured: {ex.Message} Please, contact the system administrator.");
            }
        }
    }
}
