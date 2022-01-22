
namespace ERP.Store.Desktop.Forms.Inventories
{
    partial class frmInventoryDetails
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
            this.textBoxItemID = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.comboBoxCategories = new System.Windows.Forms.ComboBox();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.textBoxSupplierName = new System.Windows.Forms.TextBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonUploadImage = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxIdentification = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxItemID
            // 
            this.textBoxItemID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxItemID.Location = new System.Drawing.Point(38, 54);
            this.textBoxItemID.Name = "textBoxItemID";
            this.textBoxItemID.PlaceholderText = "Item ID";
            this.textBoxItemID.Size = new System.Drawing.Size(195, 23);
            this.textBoxItemID.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName.Location = new System.Drawing.Point(38, 114);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.PlaceholderText = "Name";
            this.textBoxName.Size = new System.Drawing.Size(195, 23);
            this.textBoxName.TabIndex = 1;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPrice.Location = new System.Drawing.Point(38, 179);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.PlaceholderText = "Price";
            this.textBoxPrice.Size = new System.Drawing.Size(195, 23);
            this.textBoxPrice.TabIndex = 2;
            // 
            // comboBoxCategories
            // 
            this.comboBoxCategories.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxCategories.FormattingEnabled = true;
            this.comboBoxCategories.Location = new System.Drawing.Point(38, 246);
            this.comboBoxCategories.Name = "comboBoxCategories";
            this.comboBoxCategories.Size = new System.Drawing.Size(195, 23);
            this.comboBoxCategories.TabIndex = 3;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxQuantity.Location = new System.Drawing.Point(305, 54);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.PlaceholderText = "Quantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(195, 23);
            this.textBoxQuantity.TabIndex = 4;
            // 
            // textBoxSupplierName
            // 
            this.textBoxSupplierName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxSupplierName.Location = new System.Drawing.Point(305, 114);
            this.textBoxSupplierName.Name = "textBoxSupplierName";
            this.textBoxSupplierName.PlaceholderText = "Supplier";
            this.textBoxSupplierName.Size = new System.Drawing.Size(195, 23);
            this.textBoxSupplierName.TabIndex = 5;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImage.Location = new System.Drawing.Point(410, 246);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(90, 77);
            this.pictureBoxImage.TabIndex = 23;
            this.pictureBoxImage.TabStop = false;
            // 
            // buttonUploadImage
            // 
            this.buttonUploadImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonUploadImage.Location = new System.Drawing.Point(284, 248);
            this.buttonUploadImage.Name = "buttonUploadImage";
            this.buttonUploadImage.Size = new System.Drawing.Size(117, 23);
            this.buttonUploadImage.TabIndex = 7;
            this.buttonUploadImage.Text = "Upload Image";
            this.buttonUploadImage.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSend.Location = new System.Drawing.Point(557, 54);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(216, 36);
            this.buttonSend.TabIndex = 8;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxIdentification
            // 
            this.textBoxIdentification.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxIdentification.Location = new System.Drawing.Point(305, 179);
            this.textBoxIdentification.Name = "textBoxIdentification";
            this.textBoxIdentification.PlaceholderText = "Identification";
            this.textBoxIdentification.Size = new System.Drawing.Size(195, 23);
            this.textBoxIdentification.TabIndex = 6;
            // 
            // frmInventoryDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxIdentification);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.buttonUploadImage);
            this.Controls.Add(this.textBoxSupplierName);
            this.Controls.Add(this.textBoxQuantity);
            this.Controls.Add(this.comboBoxCategories);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxItemID);
            this.Name = "frmInventoryDetails";
            this.Text = "Inventory Details";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxItemID;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.ComboBox comboBoxCategories;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.TextBox textBoxSupplierName;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonUploadImage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxIdentification;
    }
}