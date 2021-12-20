﻿using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Employees
{
    public partial class frmEmployeeDetails : Form
    {
        private UserResponse User { get; set; }

        public EmployeeResponse Employee { get; set; }

        public OperationType OperationType { get; set; }

        private readonly ImageService _imageService;

        private readonly EmployeeService _employeeService;

        public frmEmployeeDetails(UserResponse user, OperationType operationType)
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
        }

        public frmEmployeeDetails(UserResponse user, EmployeeResponse employee, OperationType operationType)
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

            textBoxFirstName.Text = employee.FirstName;
            textBoxMiddleName.Text = employee.MiddleName;
            textBoxLastName.Text = employee.LastName;
            textBoxIdentification.Text = employee.Identification;

            textBoxUsername.Text = employee.UserInfo.Username;
            // TODO: Implement conversion byte[] to string (password).
            // textBoxPassword.Text = employee.UserInfo.Password;

            textBoxZip.Text = employee.Address.Zip;
            textBoxStreet.Text = employee.Address.Street;
            textBoxNumber.Text = employee.Address.Number;
            textBoxComment.Text = employee.Address.Comment;
            textBoxNeighborhood.Text = employee.Address.Neighborhood;
            textBoxCity.Text = employee.Address.City;
            textBoxState.Text = employee.Address.State;
            textBoxCountry.Text = employee.Address.Country;

            textBoxEmail.Text = employee.Contact.Email;
            textBoxCellphone.Text = employee.Contact.Cellphone;
            textBoxPhone.Text = employee.Contact.Phone;

            comboBoxAccessLevel.SelectedIndex = employee.ExtraInfo.AccessLevelID - 1;
            textBoxSalary.Text = Convert.ToString(employee.ExtraInfo.Salary);
            comboBoxJob.SelectedIndex = employee.ExtraInfo.JobID - 1;

            // TODO: Implement conversion of base 64 to image.

            #endregion
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
                        case 403:
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }
    }
}
