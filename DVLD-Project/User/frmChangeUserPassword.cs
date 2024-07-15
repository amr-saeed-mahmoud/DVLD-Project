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
    public partial class frmChangeUserPassword : Form
    {
        int UserID = -1;
        User user;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }
        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            SetDefaultValues();

            user = User.FindByUserID(UserID);
            if (user == null)
            {
                MessageBox.Show("Could not Find User With ID = " + UserID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrUserCard1.LoadUserInfo(UserID);
        }

        private void SetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            user.Password = txtPassword.Text.Trim();
            
            if(user.Save())
            {
                MessageBox.Show("Password Changed successfully", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDefaultValues();
            }
            else
            {
                MessageBox.Show("Unsuccessfull Process","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPassword, "This Feild Is Require");
                e.Cancel = true;
            }
            else errorProvider1.SetError(txtCurrentPassword, null);

            if(txtCurrentPassword.Text.Trim() != user.Password)
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Wrong");
            else errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtPassword, "This Feild Is Require");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password Not Equel New Password");
                e.Cancel = true;
            }
            else errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrUserCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
