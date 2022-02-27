﻿using System;
using System.Windows.Forms;
using ERP.Store.Desktop.Services;
using ERP.Store.Desktop.Entities.Entities;

namespace ERP.Store.Desktop.Forms.Find
{
    public partial class frmFind : Form
    {
        public dynamic User { get; set; }

        public SearchType SearchType { get; set; }

        private readonly OrderService _orderService;

        private readonly ClientService _clientService;

        private readonly SupplierService _supplierService;

        private readonly EmployeeService _employeeService;

        private readonly InventoryService _inventoryService;

        public frmFind(dynamic user, SearchType searchType)
        {
            User = user;

            SearchType = searchType;

            _orderService = new OrderService();

            _clientService = new ClientService();

            _supplierService = new SupplierService();

            _employeeService = new EmployeeService();

            _inventoryService = new InventoryService();

            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var inputData = textBoxSearch.Text;

                if (SearchType == SearchType.Employee)
                {
                    var employee = _employeeService.Get(inputData, User);

                    if (employee != null)
                    {
                        var frmEmployeeDetails = new Employees.frmEmployeeDetails(User, employee, OperationType.Update);

                        frmEmployeeDetails.Show();
                    }
                }
                else if (SearchType == SearchType.Client)
                {
                    var client = _clientService.Get(inputData, User);

                    if (client != null)
                    {
                        var frmClientDetails = new Clients.frmClientDetails(User, client, OperationType.Update);

                        frmClientDetails.Show();
                    }
                }
                else if (SearchType == SearchType.Inventory)
                {
                    var inventory = _inventoryService.Get(inputData, User);

                    if (inventory != null)
                    {
                        var frmInventoryDetails = new Inventories.frmInventoryDetails(User, inventory, OperationType.Update);

                        frmInventoryDetails.Show();
                    }
                }
                else if (SearchType == SearchType.Order)
                {
                    var isNumeric = int.TryParse(inputData, out int orderID);

                    if (isNumeric)
                    {
                        var order = _orderService.Get(orderID, User);

                        if (order != null)
                        {
                            var frmOrderDetails = new Orders.frmOrderDetails(User, order, OperationType.Update);

                            frmOrderDetails.Show();
                        }
                    }
                }
                else if (SearchType == SearchType.Supplier)
                {
                    var supplier = _supplierService.Get(inputData, User);

                    if (supplier != null)
                    {
                        var frmSupplierDetails = new Suppliers.frmSupplierDetails(User, supplier, OperationType.Update);

                        frmSupplierDetails.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The following error(s) ocurred: {ex.Message}");
            }
        }
    }
}
