using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Employees
{
    public partial class frmEmployeeDetails : Form
    {
        private dynamic User { get; set; }

        public dynamic Employee { get; set; }

        public OperationType OperationType { get; set; }

        private readonly ImageService _imageService;

        private readonly EmployeeService _employeeService;

        public frmEmployeeDetails(dynamic user, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _employeeService = new EmployeeService();

            #region InitializingComponents

            comboBoxAccessLevel.Items.Add("Administrator");
            comboBoxAccessLevel.Items.Add("Salesperson");

            comboBoxJob.Items.Add("Manager");
            comboBoxJob.Items.Add("Assistant manager");
            comboBoxJob.Items.Add("Office administrator");
            comboBoxJob.Items.Add("Administrator");
            comboBoxJob.Items.Add("Salesperson");

            #endregion

            if (OperationType == OperationType.Create)
                buttonDelete.Visible = false;
        }

        public frmEmployeeDetails(dynamic user, dynamic employee, OperationType operationType)
        {
            User = user;

            Employee = employee;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _employeeService = new EmployeeService();

            #region InitializingComponents

            comboBoxAccessLevel.Items.Add("Administrator");
            comboBoxAccessLevel.Items.Add("Salesperson");

            comboBoxJob.Items.Add("Manager");
            comboBoxJob.Items.Add("Assistant manager");
            comboBoxJob.Items.Add("Office administrator");
            comboBoxJob.Items.Add("Administrator");
            comboBoxJob.Items.Add("Salesperson");

            textBoxFirstName.Text = employee.firstName.ToString();
            textBoxMiddleName.Text = employee.middleName.ToString();
            textBoxLastName.Text = employee.lastName.ToString();
            textBoxIdentification.Text = employee.identification.ToString();

            textBoxUsername.Text = employee.userInfo.username.ToString();
            textBoxPassword.Text = Convert.ToString(employee.userInfo.password.ToString());

            textBoxZip.Text = employee.address.zip.ToString();
            textBoxStreet.Text = employee.address.street.ToString();
            textBoxNumber.Text = employee.address.number.ToString();
            textBoxComment.Text = employee.address.comment.ToString();
            textBoxNeighborhood.Text = employee.address.neighborhood.ToString();
            textBoxCity.Text = employee.address.city.ToString();
            textBoxState.Text = employee.address.state.ToString();
            textBoxCountry.Text = employee.address.country.ToString();

            textBoxEmail.Text = employee.contact.email.ToString();
            textBoxCellphone.Text = employee.contact.cellphone.ToString();
            textBoxPhone.Text = employee.contact.phone.ToString();

            comboBoxAccessLevel.SelectedIndex = int.Parse(employee.extraInfo.accessLevelID.ToString()) - 1;
            textBoxSalary.Text = Convert.ToString(employee.extraInfo.salary);
            comboBoxJob.SelectedIndex = int.Parse(employee.extraInfo.jobID.ToString()) - 1;

            if (!string.IsNullOrEmpty(employee.image.base64.ToString()) && (bool)employee.image.isImage)
            {
                var bytes = Convert.FromBase64String(employee.image.base64.ToString());

                using var ms = new MemoryStream(bytes);
                pictureBoxImage.Image = Image.FromStream(ms);
            }

            #endregion

            if (OperationType == OperationType.Create)
                buttonDelete.Visible = false;
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
                var message = string.Empty;

                if (OperationType == OperationType.Create)
                {
                    #region RegisterNewEmployee

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

                    var statusCode = _employeeService.Post(newEmployee, User);

                    switch (statusCode)
                    {
                        case 201:
                            message = "Employee registered succefully.";
                            break;
                        case 401:
                            message = $"This user doens't have authorization to complete this request.";
                            break;
                        case 409:
                            message = $"There's alredy an employee registered with the identification number {textBoxIdentification.Text}.";
                            break;
                        default:
                            message = "An error occurred while processing the request.";
                            break;
                    }

                    MessageBox.Show(message);

                    #endregion
                }
                else
                {
                    if (OperationType == OperationType.Update)
                    {
                        #region UpdateEmployee

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

                        var employeeID = _employeeService.Put(newEmployee, User);

                        if (employeeID != 0)
                            MessageBox.Show($"Employee updated sucessfully. ID: {employeeID}");
                        else
                            MessageBox.Show("An error occurred while updating the employee.");

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var message = _employeeService.Delete(textBoxIdentification.Text, User);
                
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }
    }
}
