using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Inventories
{
    public partial class frmItemDetails : Form
    {
        public List<ItemEntity> Items { get; set; }

        private List<string> ItemsList { get; set; }

        public frmItemDetails()
        {
            InitializeComponent();
        }

        public void SetItem(string item)
        {
            try
            {
                if (ItemsList == null) ItemsList = new List<string>();

                ItemsList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void frmItemDetails_Load(object sender, EventArgs e)
        {
            try
            {
                if (ItemsList != null)
                {
                    if (ItemsList.Count > 0)
                    {
                        foreach (var item in ItemsList) listViewItems.Items.Add(item);

                        listViewItems.View = View.List;
                    }
                    else
                    {
                        MessageBox.Show("The list of items cannot be empty.");
                    }
                }
                else
                {
                    if (Items.Count > 0)
                    {
                        foreach (var item in Items) listViewItems.Items.Add($"{item.ItemID}");

                        listViewItems.View = View.List;
                    }
                    else
                    {
                        MessageBox.Show("The list of items cannot be empty.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var validate = Validate(true);

                if (string.IsNullOrEmpty(validate))
                {
                    if (Items == null) Items = new List<ItemEntity>();

                    Items.Add(new ItemEntity
                    {
                        ItemID = int.Parse(textBoxEnterItemID.Text),
                        Quantity = int.Parse(textBoxQuantity.Text)
                    });

                    MessageBox.Show($"#{textBoxQuantity.Text} of {textBoxEnterItemID.Text} added successfully.");
                }
                else
                {
                    MessageBox.Show(validate);
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
                var validate = Validate(false);

                if (string.IsNullOrEmpty(validate))
                {
                    var isNumeric = int.TryParse(textBoxEnterItemID.Text, out int itemID);

                    if (isNumeric)
                    {
                        foreach (var item in Items)
                        {
                            if (item.ItemID == itemID)
                            {
                                Items.Remove(item);

                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(validate);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private new string Validate(bool isAdd)
        {
            try
            {
                if (isAdd)
                {
                    if (string.IsNullOrEmpty(textBoxEnterItemID.Text)) return "Please, enter an item id.";
                    if (string.IsNullOrEmpty(textBoxQuantity.Text)) return "Please, enter a quantity referred to the item.";

                    if (!int.TryParse(textBoxEnterItemID.Text, out int _)) return "The item id must be a numeric value.";
                    if (!int.TryParse(textBoxQuantity.Text, out int _)) return "The quantity must be a numeric value.";
                }
                else
                {
                    if (string.IsNullOrEmpty(textBoxEnterItemID.Text)) return "Please, enter an item id.";
                    if (!string.IsNullOrEmpty(textBoxQuantity.Text)) return "A quantity cannot be informed when removing an item.";

                    if (!int.TryParse(textBoxEnterItemID.Text, out int _)) return "The item id must be a numeric value.";
                }

                return string.Empty;
            }
            catch (Exception) { throw; }
        }
    }
}
