
namespace ERP.Store.Desktop.Forms.Orders
{
    partial class frmOrderDetails
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
            this.textBoxClientIdentification = new System.Windows.Forms.TextBox();
            this.comboBoxPayments = new System.Windows.Forms.ComboBox();
            this.textBoxNameOnCard = new System.Windows.Forms.TextBox();
            this.textBoxCardNumber = new System.Windows.Forms.TextBox();
            this.textBoxMM = new System.Windows.Forms.TextBox();
            this.textBoxYY = new System.Windows.Forms.TextBox();
            this.textBoxCCV = new System.Windows.Forms.TextBox();
            this.textBoxBankName = new System.Windows.Forms.TextBox();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.textBoxAgency = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxFindItem = new System.Windows.Forms.TextBox();
            this.buttonReload = new System.Windows.Forms.Button();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // textBoxClientIdentification
            // 
            this.textBoxClientIdentification.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxClientIdentification.Location = new System.Drawing.Point(69, 57);
            this.textBoxClientIdentification.Name = "textBoxClientIdentification";
            this.textBoxClientIdentification.PlaceholderText = "Client identification";
            this.textBoxClientIdentification.Size = new System.Drawing.Size(233, 23);
            this.textBoxClientIdentification.TabIndex = 0;
            // 
            // comboBoxPayments
            // 
            this.comboBoxPayments.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxPayments.FormattingEnabled = true;
            this.comboBoxPayments.Location = new System.Drawing.Point(488, 57);
            this.comboBoxPayments.Name = "comboBoxPayments";
            this.comboBoxPayments.Size = new System.Drawing.Size(233, 23);
            this.comboBoxPayments.TabIndex = 3;
            // 
            // textBoxNameOnCard
            // 
            this.textBoxNameOnCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxNameOnCard.Location = new System.Drawing.Point(488, 119);
            this.textBoxNameOnCard.Name = "textBoxNameOnCard";
            this.textBoxNameOnCard.PlaceholderText = "Name on card";
            this.textBoxNameOnCard.Size = new System.Drawing.Size(233, 23);
            this.textBoxNameOnCard.TabIndex = 4;
            // 
            // textBoxCardNumber
            // 
            this.textBoxCardNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCardNumber.Location = new System.Drawing.Point(488, 165);
            this.textBoxCardNumber.Name = "textBoxCardNumber";
            this.textBoxCardNumber.PlaceholderText = "Card number";
            this.textBoxCardNumber.Size = new System.Drawing.Size(233, 23);
            this.textBoxCardNumber.TabIndex = 5;
            // 
            // textBoxMM
            // 
            this.textBoxMM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMM.Location = new System.Drawing.Point(488, 209);
            this.textBoxMM.Name = "textBoxMM";
            this.textBoxMM.PlaceholderText = "MM";
            this.textBoxMM.Size = new System.Drawing.Size(62, 23);
            this.textBoxMM.TabIndex = 6;
            // 
            // textBoxYY
            // 
            this.textBoxYY.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxYY.Location = new System.Drawing.Point(556, 209);
            this.textBoxYY.Name = "textBoxYY";
            this.textBoxYY.PlaceholderText = "YY";
            this.textBoxYY.Size = new System.Drawing.Size(62, 23);
            this.textBoxYY.TabIndex = 7;
            // 
            // textBoxCCV
            // 
            this.textBoxCCV.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCCV.Location = new System.Drawing.Point(659, 209);
            this.textBoxCCV.Name = "textBoxCCV";
            this.textBoxCCV.PlaceholderText = "CCV";
            this.textBoxCCV.Size = new System.Drawing.Size(62, 23);
            this.textBoxCCV.TabIndex = 8;
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBankName.Location = new System.Drawing.Point(488, 272);
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.PlaceholderText = "Bank name";
            this.textBoxBankName.Size = new System.Drawing.Size(233, 23);
            this.textBoxBankName.TabIndex = 9;
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxNumber.Location = new System.Drawing.Point(488, 318);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.PlaceholderText = "Number";
            this.textBoxNumber.Size = new System.Drawing.Size(103, 23);
            this.textBoxNumber.TabIndex = 10;
            // 
            // textBoxAgency
            // 
            this.textBoxAgency.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxAgency.Location = new System.Drawing.Point(618, 318);
            this.textBoxAgency.Name = "textBoxAgency";
            this.textBoxAgency.PlaceholderText = "Agency";
            this.textBoxAgency.Size = new System.Drawing.Size(103, 23);
            this.textBoxAgency.TabIndex = 11;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSend.Location = new System.Drawing.Point(488, 373);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(233, 39);
            this.buttonSend.TabIndex = 12;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxFindItem
            // 
            this.textBoxFindItem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxFindItem.Location = new System.Drawing.Point(69, 119);
            this.textBoxFindItem.Name = "textBoxFindItem";
            this.textBoxFindItem.PlaceholderText = "Enter an item name and press enter";
            this.textBoxFindItem.Size = new System.Drawing.Size(233, 23);
            this.textBoxFindItem.TabIndex = 13;
            this.textBoxFindItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFindItem_KeyDown);
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReload.Location = new System.Drawing.Point(308, 165);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(58, 23);
            this.buttonReload.TabIndex = 14;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // listViewItems
            // 
            this.listViewItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listViewItems.HideSelection = false;
            this.listViewItems.Location = new System.Drawing.Point(69, 165);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(233, 247);
            this.listViewItems.TabIndex = 15;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            // 
            // frmOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.textBoxFindItem);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxAgency);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.textBoxBankName);
            this.Controls.Add(this.textBoxCCV);
            this.Controls.Add(this.textBoxYY);
            this.Controls.Add(this.textBoxMM);
            this.Controls.Add(this.textBoxCardNumber);
            this.Controls.Add(this.textBoxNameOnCard);
            this.Controls.Add(this.comboBoxPayments);
            this.Controls.Add(this.textBoxClientIdentification);
            this.Name = "frmOrderDetails";
            this.Text = "Order Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxClientIdentification;
        private System.Windows.Forms.ComboBox comboBoxPayments;
        private System.Windows.Forms.TextBox textBoxNameOnCard;
        private System.Windows.Forms.TextBox textBoxCardNumber;
        private System.Windows.Forms.TextBox textBoxMM;
        private System.Windows.Forms.TextBox textBoxYY;
        private System.Windows.Forms.TextBox textBoxCCV;
        private System.Windows.Forms.TextBox textBoxBankName;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.TextBox textBoxAgency;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxFindItem;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.ListView listViewItems;
    }
}