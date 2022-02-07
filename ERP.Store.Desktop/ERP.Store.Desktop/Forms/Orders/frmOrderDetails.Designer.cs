
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
            this.labelOrderIDText = new System.Windows.Forms.Label();
            this.labelOrderID = new System.Windows.Forms.Label();
            this.labelCName = new System.Windows.Forms.Label();
            this.labelClientsName = new System.Windows.Forms.Label();
            this.labelCanceled = new System.Windows.Forms.Label();
            this.labelIsCanceled = new System.Windows.Forms.Label();
            this.labelIsCompleted = new System.Windows.Forms.Label();
            this.labelCompleted = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxClientIdentification
            // 
            this.textBoxClientIdentification.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxClientIdentification.Location = new System.Drawing.Point(69, 72);
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
            this.textBoxFindItem.Location = new System.Drawing.Point(69, 210);
            this.textBoxFindItem.Name = "textBoxFindItem";
            this.textBoxFindItem.PlaceholderText = "Enter an item name and press enter";
            this.textBoxFindItem.Size = new System.Drawing.Size(233, 23);
            this.textBoxFindItem.TabIndex = 13;
            this.textBoxFindItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFindItem_KeyDown);
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReload.Location = new System.Drawing.Point(308, 258);
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
            this.listViewItems.Location = new System.Drawing.Point(69, 258);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(233, 154);
            this.listViewItems.TabIndex = 15;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            // 
            // labelOrderIDText
            // 
            this.labelOrderIDText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOrderIDText.AutoSize = true;
            this.labelOrderIDText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelOrderIDText.Location = new System.Drawing.Point(69, 33);
            this.labelOrderIDText.Name = "labelOrderIDText";
            this.labelOrderIDText.Size = new System.Drawing.Size(69, 20);
            this.labelOrderIDText.TabIndex = 16;
            this.labelOrderIDText.Text = "Order ID:";
            this.labelOrderIDText.Visible = false;
            // 
            // labelOrderID
            // 
            this.labelOrderID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOrderID.AutoSize = true;
            this.labelOrderID.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelOrderID.Location = new System.Drawing.Point(144, 33);
            this.labelOrderID.Name = "labelOrderID";
            this.labelOrderID.Size = new System.Drawing.Size(17, 20);
            this.labelOrderID.TabIndex = 17;
            this.labelOrderID.Text = "0";
            this.labelOrderID.Visible = false;
            // 
            // labelCName
            // 
            this.labelCName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCName.AutoSize = true;
            this.labelCName.Location = new System.Drawing.Point(69, 119);
            this.labelCName.Name = "labelCName";
            this.labelCName.Size = new System.Drawing.Size(82, 15);
            this.labelCName.TabIndex = 18;
            this.labelCName.Text = "Client\'s name:";
            // 
            // labelClientsName
            // 
            this.labelClientsName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelClientsName.AutoSize = true;
            this.labelClientsName.Location = new System.Drawing.Point(157, 119);
            this.labelClientsName.Name = "labelClientsName";
            this.labelClientsName.Size = new System.Drawing.Size(75, 15);
            this.labelClientsName.TabIndex = 19;
            this.labelClientsName.Text = "Not data yet.";
            // 
            // labelCanceled
            // 
            this.labelCanceled.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCanceled.AutoSize = true;
            this.labelCanceled.Location = new System.Drawing.Point(69, 138);
            this.labelCanceled.Name = "labelCanceled";
            this.labelCanceled.Size = new System.Drawing.Size(68, 15);
            this.labelCanceled.TabIndex = 20;
            this.labelCanceled.Text = "Is canceled:";
            // 
            // labelIsCanceled
            // 
            this.labelIsCanceled.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIsCanceled.AutoSize = true;
            this.labelIsCanceled.Location = new System.Drawing.Point(143, 138);
            this.labelIsCanceled.Name = "labelIsCanceled";
            this.labelIsCanceled.Size = new System.Drawing.Size(75, 15);
            this.labelIsCanceled.TabIndex = 21;
            this.labelIsCanceled.Text = "Not data yet.";
            // 
            // labelIsCompleted
            // 
            this.labelIsCompleted.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIsCompleted.AutoSize = true;
            this.labelIsCompleted.Location = new System.Drawing.Point(153, 157);
            this.labelIsCompleted.Name = "labelIsCompleted";
            this.labelIsCompleted.Size = new System.Drawing.Size(75, 15);
            this.labelIsCompleted.TabIndex = 23;
            this.labelIsCompleted.Text = "Not data yet.";
            // 
            // labelCompleted
            // 
            this.labelCompleted.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCompleted.AutoSize = true;
            this.labelCompleted.Location = new System.Drawing.Point(69, 157);
            this.labelCompleted.Name = "labelCompleted";
            this.labelCompleted.Size = new System.Drawing.Size(78, 15);
            this.labelCompleted.TabIndex = 22;
            this.labelCompleted.Text = "Is completed:";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonEdit.Location = new System.Drawing.Point(308, 287);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(58, 23);
            this.buttonEdit.TabIndex = 24;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // frmOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelIsCompleted);
            this.Controls.Add(this.labelCompleted);
            this.Controls.Add(this.labelIsCanceled);
            this.Controls.Add(this.labelCanceled);
            this.Controls.Add(this.labelClientsName);
            this.Controls.Add(this.labelCName);
            this.Controls.Add(this.labelOrderID);
            this.Controls.Add(this.labelOrderIDText);
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
        private System.Windows.Forms.Label labelOrderIDText;
        private System.Windows.Forms.Label labelOrderID;
        private System.Windows.Forms.Label labelCName;
        private System.Windows.Forms.Label labelClientsName;
        private System.Windows.Forms.Label labelCanceled;
        private System.Windows.Forms.Label labelIsCanceled;
        private System.Windows.Forms.Label labelIsCompleted;
        private System.Windows.Forms.Label labelCompleted;
        private System.Windows.Forms.Button buttonEdit;
    }
}