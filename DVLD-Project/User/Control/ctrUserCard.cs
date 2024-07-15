using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD_Project
{
    public partial class ctrUserCard : UserControl
    {
        public ctrUserCard()
        {
            InitializeComponent();
        }

        private int _UserID = -1;
        private User user;

        public int UserID
        {
            get { return _UserID; } 
        }
        
        public void LoadUserInfo(int UserID)
        {
            this._UserID = UserID;
            user = User.FindByUserID(UserID);
            if(user == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FailData();
        }
        private void FailData()
        {
            ctrPersonCard1.LoadPersonInfo(user.PersonID);
            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName.ToString();

            if (user.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }

        private void ResetPersonInfo()
        {

            ctrPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
    }
}
