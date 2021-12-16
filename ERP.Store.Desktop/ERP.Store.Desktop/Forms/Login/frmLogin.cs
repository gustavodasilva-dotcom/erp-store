using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Login
{
    public partial class frmLogin : Form
    {
        private readonly UserService _userService;

        private readonly ErrorService _errorService;

        public frmLogin()
        {
            _userService = new UserService();

            _errorService = new ErrorService();

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new UserRequest
                {
                    Username = textBoxUsername.Text,
                    Password = textBoxPassword.Text
                };

                var errors = _userService.CheckInput(user);

                if (errors.Count == 0)
                {
                    var response = _userService.Login(user);

                    if (response != null)
                    {
                        var home = new Home.frmHome(response);

                        Hide();
                        home.Show();
                    }
                    else
                    {
                        throw new Exception("Could not verify the user data. Please, contact the administrator.");
                    }
                }
                else
                {
                    _errorService.DeserializeErros(errors);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }
    }
}
