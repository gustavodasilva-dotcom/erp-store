using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Forms.Employees
{
    public partial class frmRegisterNewEmployee : Form
    {
        private UserResponse User { get; set; }

        private readonly ImageService _imageService;

        private readonly EmployeeService _employeeService;

        public frmRegisterNewEmployee(UserResponse user)
        {
            User = user;

            InitializeComponent();

            _imageService = new ImageService();

            _employeeService = new EmployeeService();

            comboBoxAccessLevel.Items.Add("Administrator");
            comboBoxAccessLevel.Items.Add("Salesperson");

            comboBoxJob.Items.Add("Manager");
            comboBoxJob.Items.Add("Assistant manager");
            comboBoxJob.Items.Add("Office administrator");
            comboBoxJob.Items.Add("Administrator");
            comboBoxJob.Items.Add("Salesperson");
        }

        // For testing reasons.
        public frmRegisterNewEmployee()
        {
            InitializeComponent();

            _imageService = new ImageService();

            _employeeService = new EmployeeService();

            comboBoxAccessLevel.Items.Add("Administrator");
            comboBoxAccessLevel.Items.Add("Salesperson");

            comboBoxJob.Items.Add("Manager");
            comboBoxJob.Items.Add("Assistant manager");
            comboBoxJob.Items.Add("Office administrator");
            comboBoxJob.Items.Add("Administrator");
            comboBoxJob.Items.Add("Salesperson");
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "JPG files(*.jpg)|*.jpg|PNG files(*.png)|*.png|All Files(*.*)|*.*"
                };

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imageLocation = fileDialog.FileName;
                    
                    pictureBoxImage.ImageLocation = imageLocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var newEmployee = new EmployeeRequest
                {
                    FirstName = textBoxFirstName.Text,
                    MiddleName = textBoxMiddleName.Text,
                    LastName = textBoxLastName.Text,
                    Identification = textBoxIdentification.Text,
                    Address = new AddressRequest
                    {
                        Zip = textBoxZip.Text,
                        Street = textBoxStreet.Text,
                        Number = textBoxNumber.Text,
                        Comment = textBoxComment.Text,
                        Neighborhood = textBoxNeighborhood.Text,
                        City = textBoxCity.Text,
                        State = textBoxState.Text,
                        Country = textBoxCountry.Text
                    },
                    UserInfo = new UserInfoRequest
                    {
                        Username = textBoxUsername.Text,
                        Password = textBoxPassword.Text
                    },
                    Contact = new ContactRequest
                    {
                        Email = textBoxEmail.Text,
                        Cellphone = textBoxCellphone.Text,
                        Phone = textBoxPhone.Text
                    },
                    ExtraInfo = new ExtraInfoRequest
                    {
                        AccessLevelID = comboBoxAccessLevel.SelectedIndex + 1,
                        Salary = double.Parse(textBoxSalary.Text),
                        JobID = comboBoxJob.SelectedIndex + 1
                    },
                    Image = new ImageRequest
                    {
                        IsImage = pictureBoxImage.Image == null ? false : true,
                        Base64 = pictureBoxImage.Image == null ? "" : _imageService.ConvertImageToBase64(pictureBoxImage.Image)
                    }
                };

                if (_employeeService.Post(newEmployee, User) == 201)
                    MessageBox.Show("Employee registered succefully.");
                else
                    throw new Exception("Could not complete the functionality.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }
    }
}
