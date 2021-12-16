using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop
{
    public partial class frmLogin : Form
    {
        private readonly UserService _userService;

        public frmLogin()
        {
            _userService = new UserService();

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

                    if (!string.IsNullOrEmpty(response.Token.Token))
                    {
                        // TODO: create the menu form and a functionality to open the said form.
                    }
                }
                else
                {
                    DeserializeErros(errors);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }

        private static void DeserializeErros(List<string> errors)
        {
            try
            {
                var errorConcat = string.Empty;

                foreach (var error in errors)
                {
                    errorConcat += error;
                }

                throw new Exception(errorConcat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
