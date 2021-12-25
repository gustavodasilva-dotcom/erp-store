using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Forms.Clients
{
    public partial class frmClientDetails : Form
    {
        private UserResponse User { get; set; }
        
        public OperationType OperationType { get; set; }

        private readonly ImageService _imageService;

        private readonly ClientService _clientService;

        public frmClientDetails(UserResponse user, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            if (OperationType == OperationType.Create)
                buttonDelete.Visible = false;
        }

        public frmClientDetails(UserResponse user, ClientResponse client, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            #region InitializingComponents

            textBoxFirstName.Text = client.FirstName;
            textBoxMiddleName.Text = client.MiddleName;
            textBoxLastName.Text = client.LastName;
            textBoxIdentification.Text = client.Identification;

            textBoxZip.Text = client.Address.Zip;
            textBoxStreet.Text = client.Address.Street;
            textBoxNumber.Text = client.Address.Number;
            textBoxComment.Text = client.Address.Comment;
            textBoxNeighborhood.Text = client.Address.Neighborhood;
            textBoxCity.Text = client.Address.City;
            textBoxState.Text = client.Address.State;
            textBoxCountry.Text = client.Address.Country;

            textBoxEmail.Text = client.Contact.Email;
            textBoxCellphone.Text = client.Contact.Cellphone;
            textBoxPhone.Text = client.Contact.Phone;

            if (!string.IsNullOrEmpty(client.Image.Base64) && client.Image.IsImage)
            {
                var bytes = Convert.FromBase64String(client.Image.Base64);

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

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                var message = string.Empty;

                if (OperationType == OperationType.Create)
                {
                    #region RegisterNewClient

                    var newClient = new ClientRequest
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
                        Contact = new ContactRequest
                        {
                            Email = textBoxEmail.Text,
                            Cellphone = textBoxCellphone.Text,
                            Phone = textBoxPhone.Text
                        },
                        Image = new ImageRequest
                        {
                            IsImage = pictureBoxImage.Image == null ? false : true,
                            Base64 = pictureBoxImage.Image == null ? "" : _imageService.ConvertImageToBase64(pictureBoxImage.Image)
                        }
                    };

                    var statusCode = _clientService.Post(newClient, User);

                    switch (statusCode)
                    {
                        case 201:
                            message = "Client registered succefully.";
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
                        #region UpdateClient

                        var newClient = new ClientRequest
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
                            Contact = new ContactRequest
                            {
                                Email = textBoxEmail.Text,
                                Cellphone = textBoxCellphone.Text,
                                Phone = textBoxPhone.Text
                            },
                            Image = new ImageRequest
                            {
                                IsImage = pictureBoxImage.Image == null ? false : true,
                                Base64 = pictureBoxImage.Image == null ? "" : _imageService.ConvertImageToBase64(pictureBoxImage.Image)
                            }
                        };

                        var statusCode = _clientService.Put(newClient, User);

                        switch (statusCode)
                        {
                            case 200:
                                message = "Client updated succefully.";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }
    }
}
