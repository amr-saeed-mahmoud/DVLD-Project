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
    public partial class frmAddUpdateUsers : Form
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;
        private int UserID = -1;
        private User user;
        
        public frmAddUpdateUsers()
        {
            InitializeComponent();
            Mode = enMode.AddNew; 
        }
        public frmAddUpdateUsers(int UserID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            this.UserID = UserID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetDefaultValues()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    lbTitle.Text = "Add New User";
                    this.Text = "Add New User";
                    user = new User();
                    tgLoginInfo.Enabled = false;
                    ctrPersonCardWithFilter1.FilterFocus();
                    break;
                case enMode.Update:
                    lbTitle.Text = "Update User";
                    this.Text = "Update User";
                    btnSave.Enabled = true;
                    tgLoginInfo.Enabled = true;
                    break;
            }
            lblUserID.Text = "???";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void LoadData()
        {
            user = User.FindByUserID(UserID);
            ctrPersonCardWithFilter1.FilterEnabled = false;
            if(user == null)
            {
                MessageBox.Show("No User with ID = " + UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lblUserID.Text = user.UserID.ToString();
            txtUserName.Text = user.UserName;
            txtPassword.Text = user.Password;
            txtConfirmPassword.Text = user.Password;
            chkIsActive.Checked = user.IsActive;
            ctrPersonCardWithFilter1.LoadPersonInfo(user.PersonID);
        }

        private void frmAddUpdateUsers_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
            if (Mode == enMode.Update)
                LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Mode == enMode.Update)
            {
                tgLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                tabUserInfoSecreen.SelectedIndex = 1;
                return;
            }
            int x = ctrPersonCardWithFilter1.PersonID;
            if (ctrPersonCardWithFilter1.PersonID != -1)
            {

                if (User.isUserExistForPersonID(ctrPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tgLoginInfo.Enabled = true;
                    tabUserInfoSecreen.SelectedIndex = 1;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrPersonCardWithFilter1.FilterFocus();

            }
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

            user.PersonID = ctrPersonCardWithFilter1.PersonID;
            user.UserName = txtUserName.Text.Trim();
            user.Password = txtPassword.Text.Trim();
            user.IsActive = chkIsActive.Checked;

            if(user.Save())
            {
                lblUserID.Text = user.UserID.ToString();
                lbTitle.Text = "Update User";
                this.Text = "Update User";
                Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                errorProvider1.SetError(txtUserName, "This Field Is Reqired");
                e.Cancel = true;
                return;
            }
            else errorProvider1.SetError(txtUserName, null);

            if(Mode == enMode.AddNew)
            {
                if (User.isUserExist(txtUserName.Text.Trim()))
                {
                    errorProvider1.SetError(txtUserName, "User Name Is Used By Another Person");
                    e.Cancel = true;
                }
                else errorProvider1.SetError(txtUserName, null);
            }
            else
            {
                if (user.UserName != txtUserName.Text.Trim())
                {
                    if(User.isUserExist(txtUserName.Text.Trim()))
                    {
                        errorProvider1.SetError(txtUserName, "User Name Is Used By Another Person");
                        e.Cancel = true;
                    }
                    else errorProvider1.SetError (txtUserName, null);
                }
            }


        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtPassword, "This Field Is Reqired");
                e.Cancel = true;
            }
            else errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password Is Not Correct");
                e.Cancel = true;
            }
            else errorProvider1.SetError(txtConfirmPassword, null);
        }

        
    }
}
