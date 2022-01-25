using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Orders
{
    public partial class frmOrderDetails : Form
    {
        private dynamic User { get; set; }

        private List<Item> Items { get; set; }

        private OperationType OperationType { get; set; }

        private readonly ClientService _clientService;

        private readonly InventoryService _inventoryService;

        private Inventories.frmItemDetails FrmItemDetails { get; set; }

        public frmOrderDetails(dynamic user, OperationType operationType)
        {
            try
            {
                User = user;

                OperationType = operationType;

                _clientService = new ClientService();

                _inventoryService = new InventoryService();

                InitializeComponent();

                #region InitializingObjects

                foreach (var payment in GetPayments()) comboBoxPayments.Items.Add(payment);

                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show($"The following error occurred: {e.Message}");
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void textBoxFindItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var itemInput = textBoxFindItem.Text.ToLower().Trim();

                    var itemDetails = new Inventories.frmItemDetails();

                    FrmItemDetails = itemDetails;
                    
                    foreach (var item in _inventoryService.Get(User, CategoryType.Items))
                    {
                        if (Convert.ToString(item.name).ToLower().Contains(itemInput))
                            itemDetails.SetItem($"{Convert.ToString(item.itemID)} - {Convert.ToString(item.name)}");
                    }

                    itemDetails.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Items == null) Items = new List<Item>();

                var newItems = FrmItemDetails.Items;

                foreach (var item in newItems) Items.Add(item);

                listViewItems.View = View.List;
                listViewItems.Clear();

                foreach (var item in Items) listViewItems.Items.Add($"#{item.Quantity} - {item.ItemID}");

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private static List<string> GetPayments()
        {
            try
            {
                return new List<string>
                {
                    "Cash",
                    "Check",
                    "Debit card",
                    "Credit card",
                    "Mobile transfer",
                    "Eletronic bank transfer"
                };
            }
            catch (Exception) { throw; }
        }

        private string ValidateInput()
        {
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(textBoxClientIdentification.Text)) return "The client's identification cannot be null or empty.";
                
                if (listViewItems.Items.Count == 0) return "To finish an order, there must be, at least, one item.";

                if (!long.TryParse(textBoxClientIdentification.Text, out long _)) return "The client's identification must be a numeric value.";

                if (comboBoxPayments.SelectedIndex == -1) return "Please, select a payment type.";

                if (comboBoxPayments.SelectedIndex == 2 || comboBoxPayments.SelectedIndex == 3)
                {
                    if (string.IsNullOrEmpty(textBoxNameOnCard.Text)) return "The name on card cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxCardNumber.Text)) return "The card number cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxMM.Text)) return "The expiration month of the card cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxYY.Text)) return "The expiration year of the card cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxCCV.Text)) return "The security code of the card cannot be null or empty.";

                    if (!long.TryParse(textBoxCardNumber.Text, out long _)) return "The card number must be numeric.";
                    if (!int.TryParse(textBoxMM.Text, out int _)) return "The expiration month must be numeric.";
                    if (!int.TryParse(textBoxYY.Text, out int _)) return "The expiration year must be numeric.";
                    if (!int.TryParse(textBoxCCV.Text, out int _)) return "The security code of the card must be numeric.";
                }

                if (comboBoxPayments.SelectedIndex == 4 || comboBoxPayments.SelectedIndex == 5)
                {
                    if (string.IsNullOrEmpty(textBoxBankName.Text)) return "The bank name cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxNumber.Text)) return "The bank number cannot be null or empty.";
                    if (string.IsNullOrEmpty(textBoxAgency.Text)) return "The bank agency cannot be null or empty.";

                    if (!int.TryParse(textBoxNumber.Text, out int _)) return "The bank number must be numeric.";
                    if (!int.TryParse(textBoxAgency.Text, out int _)) return "The bank agency must be numeric.";
                }

                #endregion

                return string.Empty;
            }
            catch (Exception) { throw; }
        }
    }
}
