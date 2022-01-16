using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Find
{
    public partial class frmFind : Form
    {
        public dynamic User { get; set; }

        public SearchType SearchType { get; set; }

        private readonly ClientService _clientService;

        private readonly EmployeeService _employeeService;

        public frmFind(dynamic user, SearchType searchType)
        {
            User = user;

            SearchType = searchType;

            _clientService = new ClientService();

            _employeeService = new EmployeeService();

            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var inputData = textBoxSearch.Text;

                if (SearchType == SearchType.Employee)
                {
                    var employee = _employeeService.Get(inputData, User);

                    if (employee != null)
                    {
                        var frmEmployeeDetails = new Employees.frmEmployeeDetails(User, employee, OperationType.Update);

                        frmEmployeeDetails.Show();
                    }
                }
                else
                {
                    if (SearchType == SearchType.Client)
                    {
                        var client = _clientService.Get(inputData, User);

                        if (client != null)
                        {
                            var frmClientDetails = new Clients.frmClientDetails(User, client, OperationType.Update);

                            frmClientDetails.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }
    }
}
