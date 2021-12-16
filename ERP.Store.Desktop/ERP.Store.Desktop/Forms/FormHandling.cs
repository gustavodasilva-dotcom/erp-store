using System;
using System.Windows.Forms;

namespace ERP.Store.Desktop.Forms
{
    public class FormHandling
    {
        public Form ActiveForm { get; set; }

        public Panel Background { get; set; }

        public void LoadMdiChildForm(Form form)
        {
            try
            {
                ActiveForm = form;

                ActiveForm.TopLevel = false;
                ActiveForm.FormBorderStyle = FormBorderStyle.None;
                ActiveForm.Dock = DockStyle.Fill;
                
                Background.Controls.Add(ActiveForm);
                Background.Tag = ActiveForm;
                
                ActiveForm.BringToFront();
                ActiveForm.Show();
            }
            catch (Exception) { throw; }
        }

        public void CloseMdiChildForm()
        {
            try
            {
                ActiveForm.Close();

                ActiveForm = null;
            }
            catch (Exception) { throw; }
        }
    }
}
