﻿
namespace ERP.Store.Desktop.Forms.Inventories
{
    partial class frmItemDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewItems = new System.Windows.Forms.ListView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxEnterItemID = new System.Windows.Forms.TextBox();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewItems
            // 
            this.listViewItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listViewItems.FullRowSelect = true;
            this.listViewItems.HideSelection = false;
            this.listViewItems.LabelEdit = true;
            this.listViewItems.Location = new System.Drawing.Point(67, 68);
            this.listViewItems.MultiSelect = false;
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(444, 142);
            this.listViewItems.TabIndex = 0;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.Location = new System.Drawing.Point(554, 152);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxEnterItemID
            // 
            this.textBoxEnterItemID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxEnterItemID.Location = new System.Drawing.Point(554, 68);
            this.textBoxEnterItemID.Name = "textBoxEnterItemID";
            this.textBoxEnterItemID.PlaceholderText = "Enter the ItemID";
            this.textBoxEnterItemID.Size = new System.Drawing.Size(100, 23);
            this.textBoxEnterItemID.TabIndex = 2;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxQuantity.Location = new System.Drawing.Point(554, 110);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.PlaceholderText = "Quantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(100, 23);
            this.textBoxQuantity.TabIndex = 3;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.Location = new System.Drawing.Point(554, 215);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonUpdate.Location = new System.Drawing.Point(554, 184);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(100, 23);
            this.buttonUpdate.TabIndex = 5;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // frmItemDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 288);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.textBoxQuantity);
            this.Controls.Add(this.textBoxEnterItemID);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listViewItems);
            this.Name = "frmItemDetails";
            this.Text = "Item Details";
            this.Load += new System.EventHandler(this.frmItemDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxEnterItemID;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.Button buttonDelete;

        public void DesableDeleteButton()
        {
            buttonDelete.Enabled = false;
        }

        public void DesableUpdateButton()
        {
            buttonUpdate.Enabled = false;
        }

        public void DesableAddButton()
        {
            buttonAdd.Enabled = false;
        }

        public void DesableTextBoxQuantity()
        {
            textBoxQuantity.Enabled = false;
        }

        private System.Windows.Forms.Button buttonUpdate;
    }
}