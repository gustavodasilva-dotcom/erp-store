using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Forms.Orders
{
    public partial class frmOrderDetails : Form
    {
        private dynamic User { get; set; }

        private List<ItemEntity> Items { get; set; }

        private OperationType OperationType { get; set; }

        private readonly OrderService _orderService;

        private readonly InventoryService _inventoryService;

        private Inventories.frmItemDetails FrmItemDetails { get; set; }

        public frmOrderDetails(dynamic user, OperationType operationType)
        {
            try
            {
                User = user;

                OperationType = operationType;

                _orderService = new OrderService();

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

        public frmOrderDetails(dynamic user, dynamic order, OperationType operationType)
        {
            try
            {
                User = user;

                OperationType = operationType;

                _orderService = new OrderService();

                _inventoryService = new InventoryService();

                InitializeComponent();

                if (operationType == OperationType.Update)
                {
                    #region InitializingObjects

                    textBoxClientIdentification.Text = order.client.identification.ToString();

                    listViewItems.View = View.List;

                    foreach (var item in order.items)
                        listViewItems.Items.Add($"#{item.inventory.quantity.ToString()} - {item.itemID.ToString()}");

                    foreach (var payment in GetPayments())
                        if (payment.Equals(order.payment.description.ToString())) comboBoxPayments.Text = payment;
                    
                    if (order.payment.paymentID == 3 || order.payment.paymentID == 4)
                    {
                        textBoxNameOnCard.Text = order.payment.paymentInfo.nameOnCard.ToString();
                        textBoxNameOnCard.Text = order.payment.paymentInfo.cardNumber.ToString();
                        textBoxYY.Text = order.payment.paymentInfo.yearExpiryDate.ToString();
                        textBoxMM.Text = order.payment.paymentInfo.monthExpiryDate.ToString();
                    }

                    if (order.payment.paymentID == 5 || order.payment.paymentID == 6)
                    {
                        textBoxBankName.Text = order.payment.paymentInfo.bankName.ToString();
                        textBoxNumber.Text = order.payment.paymentInfo.number.ToString();
                        textBoxAgency.Text = order.payment.paymentInfo.agency.ToString();
                    }

                    #endregion
                }
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
                var validation = ValidateInput();

                if (string.IsNullOrEmpty(validation))
                {
                    #region InitializingObjets

                    var orderRequest = new OrderRequest
                    {
                        ClientIdentification = textBoxClientIdentification.Text,
                    };

                    var items = SetItems();

                    orderRequest.Items = new List<ItemOrderRequest>();
                    orderRequest.Items = items;

                    var payment = SetPayment();

                    orderRequest.Payment = new PaymentRequest();
                    orderRequest.Payment = payment;

                    #endregion

                    var orderID = _orderService.Post(orderRequest, User);

                    if (orderID != 0)
                        MessageBox.Show($"Order created successfully. Order id: {orderID}.");
                    else
                        MessageBox.Show("It was not possible to complete de request.");
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
                if (Items == null) Items = new List<ItemEntity>();

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

        private List<ItemOrderRequest> SetItems()
        {
            try
            {
                #region SetItems

                var itemsRequest = new List<ItemOrderRequest>();

                foreach (var item in Items)
                {
                    itemsRequest.Add(new ItemOrderRequest
                    {
                        ItemID = item.ItemID,
                        Quantity = item.Quantity
                    });
                }

                #endregion

                return itemsRequest;
            }
            catch (Exception) { throw; }
        }

        private PaymentRequest SetPayment()
        {
            try
            {
                #region SetPayment

                PaymentRequest payment = null;

                if (comboBoxPayments.SelectedIndex == 0)
                {
                    payment = new PaymentRequest
                    {
                        IsCheck = false,
                        IsCard = false,
                        IsBankTransfer = false,
                        Card = new CardRequest(),
                        BankInfo = new BankInfoRequest()
                    };
                }

                if (comboBoxPayments.SelectedIndex == 1)
                {
                    payment = new PaymentRequest
                    {
                        IsCheck = true,
                        IsCard = false,
                        IsBankTransfer = false,
                        Card = new CardRequest(),
                        BankInfo = new BankInfoRequest
                        {
                            IsMobileTransfer = comboBoxPayments.SelectedIndex == 4 ? true : false,
                            IsEletronicBankTransfer = comboBoxPayments.SelectedIndex == 5 ? true : false,
                            BankName = textBoxBankName.Text,
                            Number = textBoxNumber.Text,
                            Agency = textBoxAgency.Text
                        }
                    };
                }

                if (comboBoxPayments.SelectedIndex == 2 || comboBoxPayments.SelectedIndex == 3)
                {
                    payment = new PaymentRequest
                    {
                        IsCheck = false,
                        IsCard = true,
                        IsBankTransfer = false,
                        Card = new CardRequest
                        {
                            IsCredit = comboBoxPayments.SelectedIndex == 3 ? true : false,
                            NameOnCard = textBoxNameOnCard.Text,
                            CardNumber = textBoxCardNumber.Text,
                            YearExpiryDate = int.Parse(textBoxYY.Text),
                            MonthExpiryDate = int.Parse(textBoxMM.Text),
                            SecurityCode = int.Parse(textBoxCCV.Text)
                        },
                        BankInfo = new BankInfoRequest()
                    };
                }

                if (comboBoxPayments.SelectedIndex == 4 || comboBoxPayments.SelectedIndex == 5)
                {
                    payment = new PaymentRequest
                    {
                        IsCheck = false,
                        IsCard = false,
                        IsBankTransfer = true,
                        Card = new CardRequest(),
                        BankInfo = new BankInfoRequest
                        {
                            IsMobileTransfer = comboBoxPayments.SelectedIndex == 4 ? true : false,
                            IsEletronicBankTransfer = comboBoxPayments.SelectedIndex == 5 ? true : false,
                            BankName = textBoxBankName.Text,
                            Number = textBoxNumber.Text,
                            Agency = textBoxAgency.Text
                        }
                    };
                }

                #endregion

                return payment;
            }
            catch (Exception) { throw; }
        }

        private static List<string> GetPayments()
        {
            try
            {
                #region GetPayments

                return new List<string>
                {
                    "Cash",
                    "Check",
                    "Debit card",
                    "Credit card",
                    "Mobile transfer",
                    "Eletronic bank transfer"
                };

                #endregion
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
