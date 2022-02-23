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

        private dynamic OrderJson { get; set; }

        private List<ItemEntity> Items { get; set; }

        private OperationType OperationType { get; set; }

        private readonly PdfService _pdfService;

        private readonly OrderService _orderService;

        private readonly InventoryService _inventoryService;

        private Inventories.frmItemDetails FrmItemDetails { get; set; }

        public frmOrderDetails(dynamic user, OperationType operationType)
        {
            try
            {
                InitializeComponent();

                if (operationType == OperationType.Update || operationType == OperationType.Read)
                {
                    labelOrderID.Visible = true;
                    labelOrderIDText.Visible = true;
                }

                User = user;

                OperationType = operationType;

                _pdfService = new PdfService();

                _orderService = new OrderService();

                _inventoryService = new InventoryService();

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
                InitializeComponent();

                if (operationType == OperationType.Update || operationType == OperationType.Read)
                {
                    labelOrderID.Visible = true;
                    labelOrderIDText.Visible = true;
                }

                User = user;

                OperationType = operationType;

                _pdfService = new PdfService();

                _orderService = new OrderService();

                _inventoryService = new InventoryService();

                if (operationType == OperationType.Update || operationType == OperationType.Read)
                {
                    #region InitializingObjects

                    labelOrderID.Text = $"#{order.orderID.ToString()}";

                    textBoxClientIdentification.Text = order.client.identification.ToString();

                    labelClientsName.Text = order.client.firstName.ToString() + " " + order.client.middleName.ToString() + " " + order.client.lastName.ToString();

                    labelIsCanceled.Text = (bool)order.isCanceled ? "Yes" : "No";
                    labelIsCompleted.Text = (bool)order.isCompleted ? "Yes" : "No";

                    listViewItems.View = View.List;

                    foreach (var item in order.items)
                    {
                        listViewItems.Items.Add($"#{item.quantity.ToString()} - {item.itemID.ToString()}");

                        if (Items == null)
                            Items = new List<ItemEntity>();

                        Items.Add(new ItemEntity
                        {
                            ItemID = int.Parse(item.itemID.ToString()),
                            Quantity = int.Parse(item.quantity.ToString())
                        });
                    }

                    foreach (var payment in GetPayments()) comboBoxPayments.Items.Add(payment);

                    foreach (var payment in GetPayments()) if (payment.Equals(order.payment.description.ToString())) comboBoxPayments.Text = payment;

                    if (order.payment.paymentID == 3 || order.payment.paymentID == 4)
                    {
                        textBoxNameOnCard.Text = order.payment.paymentInfo.nameOnCard.ToString();
                        textBoxCardNumber.Text = order.payment.paymentInfo.cardNumber.ToString();
                        textBoxYY.Text = order.payment.paymentInfo.yearExpiryDate.ToString();
                        textBoxMM.Text = order.payment.paymentInfo.monthExpiryDate.ToString();
                    }

                    if (order.payment.paymentID == 2 || order.payment.paymentID == 5 || order.payment.paymentID == 6)
                    {
                        textBoxBankName.Text = order.payment.paymentInfo.bankName.ToString();
                        textBoxNumber.Text = order.payment.paymentInfo.number.ToString();
                        textBoxAgency.Text = order.payment.paymentInfo.agency.ToString();
                    }

                    if ((bool)order.isCompleted) checkBoxFinishOrder.Checked = true;
                    if ((bool)order.isCanceled) checkBoxCancelOrder.Checked = true;

                    OrderJson = order;

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

                if (checkBoxCancelOrder.Checked && checkBoxFinishOrder.Checked) throw new Exception("It's not possible to finish and cancel an order at the same time.");

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

                    if (OperationType == OperationType.Create)
                    {
                        if (checkBoxCancelOrder.Checked) throw new Exception("It's not possible to created and cancel an order that is being created.");
                        if (checkBoxFinishOrder.Checked) throw new Exception("It's not possible to created and finish an order at the same time.");

                        var orderID = _orderService.Post(orderRequest, User);

                        if (orderID != 0)
                        {
                            MessageBox.Show($"Order created successfully. Order id: {orderID}.");

                            this.Close();
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("It was not possible to complete de request.");
                        }
                    }
                    else
                    {
                        if (OperationType == OperationType.Read || OperationType == OperationType.Update)
                        {
                            if (int.TryParse(labelOrderID.Text.Substring(1, labelOrderID.Text.Length - 1), out int orderIdFromLabel))
                            {
                                var orderID = _orderService.Put(orderRequest, orderIdFromLabel, User);

                                if (orderID != 0)
                                    MessageBox.Show($"Order {orderID} updated successfully.");
                                else
                                    MessageBox.Show("It was not possible to complete de request.");

                                if (!(bool)OrderJson.isCanceled && checkBoxCancelOrder.Checked)
                                {
                                    orderID = _orderService.Post(true, orderIdFromLabel, User);

                                    if (orderID != 0)
                                        MessageBox.Show($"Order {orderID} canceled successfully.");
                                    else
                                        MessageBox.Show("It was not possible to complete de request.");
                                }

                                if (!(bool)OrderJson.isCompleted && checkBoxFinishOrder.Checked)
                                {
                                    orderID = _orderService.Post(false, orderIdFromLabel, User);

                                    if (orderID != 0)
                                        MessageBox.Show($"Order {orderID} finished successfully.");
                                    else
                                        MessageBox.Show("It was not possible to complete de request.");
                                }
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
                    itemDetails.DesableUpdateButton();
                    itemDetails.DesableDeleteButton();

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

                if (FrmItemDetails != null)
                {
                    var alreadyItems = new List<ItemEntity>();

                    var itemsDisplay = new List<ItemEntity>();

                    var newItems = FrmItemDetails.Items;

                    listViewItems.View = View.List;
                    listViewItems.Clear();

                    if (Items != null && !Items.Count.Equals(newItems.Count))
                    {
                        if (Items.Count > 0)
                        {
                            foreach (var item in Items)
                            {
                                alreadyItems.Add(new ItemEntity
                                {
                                    ItemID = item.ItemID,
                                    Quantity = item.Quantity
                                });
                            }

                            Items.Clear();
                        }

                        foreach (var newItem in newItems)
                        {
                            itemsDisplay.Add(new ItemEntity
                            {
                                ItemID = newItem.ItemID,
                                Quantity = newItem.Quantity
                            });
                        }

                        foreach (var item in alreadyItems)
                        {
                            if (itemsDisplay.Contains(item))
                            {
                                continue;
                            }
                            else
                            {
                                itemsDisplay.Add(new ItemEntity
                                {
                                    ItemID = item.ItemID,
                                    Quantity = item.Quantity
                                });
                            }
                        }

                        foreach (var item in itemsDisplay)
                        {
                            Items.Add(new ItemEntity
                            {
                                ItemID = item.ItemID,
                                Quantity = item.Quantity
                            });

                            listViewItems.Items.Add($"#{item.Quantity} - {item.ItemID}");
                        }
                    }
                    else
                    {
                        Items.Clear();

                        foreach (var item in newItems)
                        {
                            Items.Add(new ItemEntity
                            {
                                ItemID = item.ItemID,
                                Quantity = item.Quantity
                            });

                            listViewItems.Items.Add($"#{item.Quantity} - {item.ItemID}");
                        }
                    }

                    Refresh();
                }
                else
                {
                    MessageBox.Show("Before refreshing, please, add, at least, an item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error occurred: {ex.Message}");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FrmItemDetails != null || Items != null)
                {
                    var itemDetails = new Inventories.frmItemDetails
                    {
                        Items = new List<ItemEntity>()
                    };

                    FrmItemDetails = itemDetails;

                    foreach (var item in Items) itemDetails.Items.Add(item);

                    itemDetails.DesableAddButton();
                    itemDetails.Show();
                }
                else
                {
                    MessageBox.Show("Before refreshing, please, add, at least, an item.");
                }
            }
            catch (Exception) { throw; }
        }

        private void buttonGetOrderPdf_Click(object sender, EventArgs e)
        {
            try
            {
                _pdfService.CreateOrderPdf(OrderJson);
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
                    "Checks",
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

                    if (!int.TryParse(textBoxAgency.Text, out int _)) return "The bank agency must be numeric.";
                }

                #endregion

                return string.Empty;
            }
            catch (Exception) { throw; }
        }
    }
}
