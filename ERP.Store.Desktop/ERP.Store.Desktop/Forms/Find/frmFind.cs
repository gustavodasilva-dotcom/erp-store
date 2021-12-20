using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Forms.Find
{
    public partial class frmFind : Form
    {
        public UserResponse User { get; set; }

        public SearchType SearchType { get; set; }

        private readonly EmployeeService _employeeService;

        public frmFind(UserResponse user, SearchType searchType)
        {
            User = user;

            SearchType = searchType;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }
    }
}
