
namespace ERP.Store.Desktop.Forms.Clients
{
    partial class frmClientDetails
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonUploadImage = new System.Windows.Forms.Button();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.textBoxCellphone = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxCountry = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.textBoxNeighborhood = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.textBoxZip = new System.Windows.Forms.TextBox();
            this.textBoxIdentification = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxMiddleName = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.Location = new System.Drawing.Point(567, 198);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(184, 28);
            this.buttonDelete.TabIndex = 17;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSend.Location = new System.Drawing.Point(550, 142);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(216, 36);
            this.buttonSend.TabIndex = 16;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImage.Location = new System.Drawing.Point(676, 27);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(90, 77);
            this.pictureBoxImage.TabIndex = 45;
            this.pictureBoxImage.TabStop = false;
            // 
            // buttonUploadImage
            // 
            this.buttonUploadImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonUploadImage.Location = new System.Drawing.Point(550, 29);
            this.buttonUploadImage.Name = "buttonUploadImage";
            this.buttonUploadImage.Size = new System.Drawing.Size(117, 23);
            this.buttonUploadImage.TabIndex = 15;
            this.buttonUploadImage.Text = "Upload Image";
            this.buttonUploadImage.UseVisualStyleBackColor = true;
            this.buttonUploadImage.Click += new System.EventHandler(this.buttonUploadImage_Click);
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPhone.Location = new System.Drawing.Point(34, 390);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.PlaceholderText = "Phone";
            this.textBoxPhone.Size = new System.Drawing.Size(216, 23);
            this.textBoxPhone.TabIndex = 6;
            // 
            // textBoxCellphone
            // 
            this.textBoxCellphone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCellphone.Location = new System.Drawing.Point(34, 328);
            this.textBoxCellphone.Name = "textBoxCellphone";
            this.textBoxCellphone.PlaceholderText = "Cellphone";
            this.textBoxCellphone.Size = new System.Drawing.Size(216, 23);
            this.textBoxCellphone.TabIndex = 5;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxEmail.Location = new System.Drawing.Point(34, 264);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.PlaceholderText = "Email";
            this.textBoxEmail.Size = new System.Drawing.Size(216, 23);
            this.textBoxEmail.TabIndex = 4;
            // 
            // textBoxCountry
            // 
            this.textBoxCountry.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCountry.Location = new System.Drawing.Point(293, 452);
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.PlaceholderText = "Country";
            this.textBoxCountry.Size = new System.Drawing.Size(216, 23);
            this.textBoxCountry.TabIndex = 14;
            // 
            // textBoxState
            // 
            this.textBoxState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxState.Location = new System.Drawing.Point(293, 390);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.PlaceholderText = "State";
            this.textBoxState.Size = new System.Drawing.Size(216, 23);
            this.textBoxState.TabIndex = 13;
            // 
            // textBoxCity
            // 
            this.textBoxCity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCity.Location = new System.Drawing.Point(293, 328);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.PlaceholderText = "City";
            this.textBoxCity.Size = new System.Drawing.Size(216, 23);
            this.textBoxCity.TabIndex = 12;
            // 
            // textBoxNeighborhood
            // 
            this.textBoxNeighborhood.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxNeighborhood.Location = new System.Drawing.Point(293, 264);
            this.textBoxNeighborhood.Name = "textBoxNeighborhood";
            this.textBoxNeighborhood.PlaceholderText = "Neighborhood";
            this.textBoxNeighborhood.Size = new System.Drawing.Size(216, 23);
            this.textBoxNeighborhood.TabIndex = 11;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxComment.Location = new System.Drawing.Point(293, 202);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.PlaceholderText = "Comment";
            this.textBoxComment.Size = new System.Drawing.Size(216, 23);
            this.textBoxComment.TabIndex = 10;
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxNumber.Location = new System.Drawing.Point(293, 142);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.PlaceholderText = "Number";
            this.textBoxNumber.Size = new System.Drawing.Size(216, 23);
            this.textBoxNumber.TabIndex = 9;
            // 
            // textBoxStreet
            // 
            this.textBoxStreet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxStreet.Location = new System.Drawing.Point(293, 84);
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.PlaceholderText = "Street";
            this.textBoxStreet.Size = new System.Drawing.Size(216, 23);
            this.textBoxStreet.TabIndex = 8;
            // 
            // textBoxZip
            // 
            this.textBoxZip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxZip.Location = new System.Drawing.Point(293, 27);
            this.textBoxZip.Name = "textBoxZip";
            this.textBoxZip.PlaceholderText = "Zip";
            this.textBoxZip.Size = new System.Drawing.Size(216, 23);
            this.textBoxZip.TabIndex = 7;
            // 
            // textBoxIdentification
            // 
            this.textBoxIdentification.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxIdentification.Location = new System.Drawing.Point(34, 202);
            this.textBoxIdentification.Name = "textBoxIdentification";
            this.textBoxIdentification.PlaceholderText = "Identification";
            this.textBoxIdentification.Size = new System.Drawing.Size(216, 23);
            this.textBoxIdentification.TabIndex = 3;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxLastName.Location = new System.Drawing.Point(34, 142);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.PlaceholderText = "Last name";
            this.textBoxLastName.Size = new System.Drawing.Size(216, 23);
            this.textBoxLastName.TabIndex = 2;
            // 
            // textBoxMiddleName
            // 
            this.textBoxMiddleName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMiddleName.Location = new System.Drawing.Point(34, 84);
            this.textBoxMiddleName.Name = "textBoxMiddleName";
            this.textBoxMiddleName.PlaceholderText = "Middle name";
            this.textBoxMiddleName.Size = new System.Drawing.Size(216, 23);
            this.textBoxMiddleName.TabIndex = 1;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxFirstName.Location = new System.Drawing.Point(34, 27);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.PlaceholderText = "First name";
            this.textBoxFirstName.Size = new System.Drawing.Size(216, 23);
            this.textBoxFirstName.TabIndex = 0;
            // 
            // frmClientDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.buttonUploadImage);
            this.Controls.Add(this.textBoxPhone);
            this.Controls.Add(this.textBoxCellphone);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxCountry);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.textBoxCity);
            this.Controls.Add(this.textBoxNeighborhood);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.textBoxStreet);
            this.Controls.Add(this.textBoxZip);
            this.Controls.Add(this.textBoxIdentification);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxMiddleName);
            this.Controls.Add(this.textBoxFirstName);
            this.Name = "frmClientDetails";
            this.Text = "frmClientDetails";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonUploadImage;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.TextBox textBoxCellphone;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxCountry;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.TextBox textBoxNeighborhood;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.TextBox textBoxZip;
        private System.Windows.Forms.TextBox textBoxIdentification;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxMiddleName;
        private System.Windows.Forms.TextBox textBoxFirstName;
    }
}