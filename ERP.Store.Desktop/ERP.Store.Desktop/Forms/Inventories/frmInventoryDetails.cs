using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Inventories
{
    public partial class frmInventoryDetails : Form
    {
        private dynamic User { get; set; }

        public OperationType OperationType { get; set; }

        private readonly ImageService _imageService;

        private readonly ClientService _clientService;

        private readonly InventoryService _inventoryService;

        public frmInventoryDetails(dynamic user, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            _inventoryService = new InventoryService();

            #region InitializingObjects

            var categories = _inventoryService.Get(User, CategoryType.Categories);

            foreach (var category in categories)
                comboBoxCategories.Items.Add(category.description);

            #endregion

            textBoxItemID.Enabled = false;
        }

        public frmInventoryDetails(dynamic user, dynamic inventory, OperationType operationType)
        {
            User = user;

            OperationType = operationType;

            InitializeComponent();

            _imageService = new ImageService();

            _clientService = new ClientService();

            _inventoryService = new InventoryService();

            #region InitializingObjects

            if (operationType == OperationType.Update)
            {
                textBoxItemID.Text = inventory.itemID.ToString();
                textBoxName.Text = inventory.name;
                textBoxPrice.Text = inventory.price.ToString();
                textBoxQuantity.Text = inventory.inventory.quantity.ToString();
                textBoxSupplierName.Text = inventory.inventory.supplier.name;
                textBoxIdentification.Text = inventory.inventory.supplier.identification;

                var categories = _inventoryService.Get(User, CategoryType.Categories);

                foreach (var category in categories)
                {
                    comboBoxCategories.Items.Add(category.description);

                    if (category.description.Equals(inventory.category.description)) comboBoxCategories.Text = category.description;
                }
            }

            #endregion

            textBoxItemID.Enabled = false;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                var validation = ValidateInputs();

                if (string.IsNullOrEmpty(validation))
                {
                    int itemID;

                    #region InitializingObjects

                    var categoryID = 0;

                    var categories = _inventoryService.Get(User, CategoryType.Categories);

                    foreach (var category in categories) if (category.description.Equals(comboBoxCategories.SelectedItem)) categoryID = category.categoryID;

                    if (textBoxPrice.Text.Contains(".")) textBoxPrice.Text = textBoxPrice.Text.Replace(".", ",");

                    var item = new ItemRequest
                    {
                        Name = textBoxName.Text,
                        Price = double.Parse(textBoxPrice.Text),
                        Quantity = int.Parse(textBoxQuantity.Text),
                        Category = new CategoryRequest
                        {
                            CategoryID = categoryID
                        },
                        Supplier = new SupplierRequest
                        {
                            Name = textBoxName.Text,
                            Identification = textBoxIdentification.Text
                        },
                        Image = new ImageRequest
                        {
                            IsImage = pictureBoxImage.Image == null ? false : true,
                            Base64 = pictureBoxImage.Image == null ? string.Empty : _imageService.ConvertImageToBase64(pictureBoxImage.Image)
                        }
                    };

                    #endregion

                    if (OperationType == OperationType.Create)
                    {
                        item.ItemID = 0;

                        itemID = _inventoryService.Post(item, User);

                        if (itemID != 0)
                            MessageBox.Show($"Item registered successfully. New id: {itemID}.");
                        else
                            throw new Exception("It wasn't possible to finish the request.");
                    }
                    else
                    {
                        if (OperationType == OperationType.Update)
                        {
                            if (int.TryParse(textBoxItemID.Text, out itemID))
                            {
                                item.ItemID = itemID;

                                item.ItemID = _inventoryService.Put(item, User);

                                if (item.ItemID != 0)
                                    MessageBox.Show($"Item {item.ItemID} updated successfully.");
                                else
                                    throw new Exception("It wasn't possible to finish the request.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(validation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }

        private string ValidateInputs()
        {
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(textBoxName.Text)) return "The name of the item cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxPrice.Text)) return "The price of the item cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxQuantity.Text)) return "The quantity of the item cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxSupplierName.Text)) return "The supplier's name of the item cannot be null or empty.";
                if (string.IsNullOrEmpty(textBoxIdentification.Text)) return "The supplier's identification of the item cannot be null or empty.";

                if (comboBoxCategories.SelectedIndex == -1) return "Please, select a category for the item.";

                if (!double.TryParse(textBoxPrice.Text.Trim(), out double _)) return "The price informed is not a numeric value.";
                if (!int.TryParse(textBoxQuantity.Text.Trim(), out int _)) return "The quantity informed is not a numeric value.";
                if (!long.TryParse(textBoxIdentification.Text.Trim(), out long _)) return "The identification of the supplier informed is not a numeric value.";

                #endregion

                return string.Empty;
            }
            catch (Exception) { throw; }
        }
    }
}
