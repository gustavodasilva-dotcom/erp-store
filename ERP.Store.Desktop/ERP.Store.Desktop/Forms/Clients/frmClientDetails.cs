using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Clients
{
    public partial class frmClientDetails : Form
    {
        private dynamic User { get; set; }
        
        public OperationType OperationType { get; set; }

        private readonly ImageService _imageService;

        private readonly ClientService _clientService;

        public frmClientDetails(dynamic user, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            if (OperationType == OperationType.Create)
                buttonDelete.Visible = false;
        }

        public frmClientDetails(dynamic user, dynamic client, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            #region InitializingComponents

            textBoxFirstName.Text = client.firstName.ToString();
            textBoxMiddleName.Text = client.middleName.ToString();
            textBoxLastName.Text = client.lastName.ToString();
            textBoxIdentification.Text = client.identification.ToString();

            textBoxZip.Text = client.address.zip.ToString();
            textBoxStreet.Text = client.address.street.ToString();
            textBoxNumber.Text = client.address.number.ToString();
            textBoxComment.Text = client.address.comment.ToString();
            textBoxNeighborhood.Text = client.address.neighborhood.ToString();
            textBoxCity.Text = client.address.city.ToString();
            textBoxState.Text = client.address.state.ToString();
            textBoxCountry.Text = client.address.country.ToString();

            textBoxEmail.Text = client.contact.email.ToString();
            textBoxCellphone.Text = client.contact.cellphone.ToString();
            textBoxPhone.Text = client.contact.phone.ToString();

            if (!string.IsNullOrEmpty(client.image.base64.ToString()) && (bool)client.image.isImage)
            {
                var bytes = Convert.FromBase64String(client.image.base64.ToString());

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

                    var clientID = _clientService.Post(newClient, User);

                    if (clientID != null)
                        MessageBox.Show($"Client registered successfully! Client ID: {clientID}");
                    else
                        MessageBox.Show("An error occurred while registering the client.");

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

                        var clientID = _clientService.Put(newClient, User);

                        if (clientID != null)
                            MessageBox.Show($"Client updated successfully! Client ID: {clientID}");
                        else
                            MessageBox.Show("An error occurred while registering the client.");

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
                var response = _clientService.Delete(textBoxIdentification.Text, User);

                MessageBox.Show(response.ToString());

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }
    }
}
