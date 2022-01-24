using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERP.Store.Desktop.Forms.Inventories
{
    public partial class frmItemDetails : Form
    {
        private List<string> ItemsList { get; set; }

        public frmItemDetails()
        {
            ItemsList = new List<string>();

            InitializeComponent();
        }

        public void SetItem(string item)
        {
            try
            {
                ItemsList.Add(item);
            }
            catch (Exception) { throw; }
        }
    }
}
