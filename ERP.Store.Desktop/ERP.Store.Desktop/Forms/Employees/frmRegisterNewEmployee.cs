using System.Windows.Forms;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Forms.Employees
{
    public partial class frmRegisterNewEmployee : Form
    {
        private UserResponse User { get; set; }

        public frmRegisterNewEmployee(UserResponse user)
        {
            User = user;

            InitializeComponent();
        }
    }
}
