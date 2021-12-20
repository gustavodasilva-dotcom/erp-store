using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Forms.Home
{
    public partial class frmHome : Form
    {
        private UserResponse User { get; set; }

        public frmHome(UserResponse user)
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
    }
}
