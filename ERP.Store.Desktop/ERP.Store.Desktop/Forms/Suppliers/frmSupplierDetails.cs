using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Suppliers
{
    public partial class frmSupplierDetails : Form
    {
        public dynamic User { get; set; }

        public OperationType OperationType { get; set; }

        private readonly SupplierService _supplierService;

        public frmSupplierDetails(dynamic user, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _supplierService = new SupplierService();
        }

        public frmSupplierDetails(dynamic user, dynamic supplier, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _supplierService = new SupplierService();

            #region InitializingComponents

            textBoxName.Text = supplier.name.ToString();
            textBoxIdentification.Text = supplier.identification.ToString();

            textBoxZip.Text = supplier.address.zip.ToString();
            textBoxStreet.Text = supplier.address.street.ToString();
            textBoxNumber.Text = supplier.address.number.ToString();
            textBoxComment.Text = supplier.address.comment.ToString();
            textBoxNeighborhood.Text = supplier.address.neighborhood.ToString();
            textBoxCity.Text = supplier.address.city.ToString();
            textBoxState.Text = supplier.address.state.ToString();
            textBoxCountry.Text = supplier.address.country.ToString();

            textBoxEmail.Text = supplier.contact.email.ToString();
            textBoxCellphone.Text = supplier.contact.cellphone.ToString();
            textBoxPhone.Text = supplier.contact.phone.ToString();

            #endregion
        }

        private void buttonSend_Click(object sender, System.EventArgs e)
        {
            try
            {
                var validation = ValidateInput();

                if (string.IsNullOrEmpty(validation))
                {
                    if (OperationType == OperationType.Create)
                    {
                        #region InstantializeObject

                        var request = new SupplierRequest
                        {
                            Name = textBoxName.Text,
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
                            }
                        };

                        #endregion

                        var supplierID = _supplierService.Post(request, User);

                        if (supplierID != null)
                            MessageBox.Show($"Supplier registered successfully! Client ID: {supplierID}");
                        else
                            MessageBox.Show("An error occurred while registering the supplier.");
                    }
                }
                else
                {
                    MessageBox.Show(validation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private string ValidateInput()
        {
            try
            {
                #region ValidateInput

                if (string.IsNullOrEmpty(textBoxName.Text)) return "The name of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxIdentification.Text)) return "The identification of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxEmail.Text)) return "The email of the supplier cannot be null or empty.";

                if (!string.IsNullOrEmpty(textBoxCellphone.Text))
                {
                    if (!long.TryParse(textBoxCellphone.Text, out long _)) return "The cellphone number, if supplied, needs to be numeric.";
                }

                if (string.IsNullOrEmpty(textBoxPhone.Text)) return "The phone of the supplier cannot be null or empty.";
                if (!long.TryParse(textBoxPhone.Text, out long _)) return "The phone number needs to be numeric.";
                
                if (string.IsNullOrEmpty(textBoxZip.Text)) return "The zip of the supplier cannot be null or empty.";
                if (!int.TryParse(textBoxZip.Text, out int _)) return "The zip number needs to be numeric.";

                if (string.IsNullOrEmpty(textBoxStreet.Text)) return "The street of the supplier cannot be null or empty.";

                if (string.IsNullOrEmpty(textBoxNumber.Text)) return "The address' number of the supplier cannot be null or empty.";
                if (!int.TryParse(textBoxNumber.Text, out int _)) return "The address' number needs to be numeric.";

                if (string.IsNullOrEmpty(textBoxComment.Text)) return "The address' comment of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxNeighborhood.Text)) return "The address' neighborhood of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxCity.Text)) return "The address' city of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxState.Text)) return "The address' state of the supplier cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxCountry.Text)) return "The address' country of the supplier cannot be null or empty.";

                return string.Empty;

                #endregion
            }
            catch (Exception) { throw; }
        }
    }
}
