using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmEditApplications : Form
    {
        int ApplicationID;
        ApplactionType Applaction;

        public frmEditApplications(int ApplicationID)
        {
            InitializeComponent();
            this.ApplicationID = ApplicationID;
        }

        private void frmEditApplications_Load(object sender, EventArgs e)
        {
            Applaction = ApplactionType.Find(ApplicationID);

            if(Applaction != null )
            {
                lblApplicationTypeID.Text = ApplicationID.ToString();
                txtTitle.Text = Applaction.Title;
                txtFees.Text = Applaction.Fees.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidTitle(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                errorProvider1.SetError(txtTitle, "This Field Is Require");
                e.Cancel = true;
            }
            else errorProvider1.SetError(txtTitle, null);
        }

        private void ValidFees(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFees, "This Field Is Require");
                e.Cancel = true;
            }
            else if (!Validation.IsValidNumber(txtFees.Text))
            {
                errorProvider1.SetError(txtFees, "Enter A Valid Value");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtFees, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Applaction.Title = txtTitle.Text.Trim();
            Applaction.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (Applaction.Save())
            {
                MessageBox.Show("Successfully Process", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                MessageBox.Show("Unsuccessfully Process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       
    }
}
