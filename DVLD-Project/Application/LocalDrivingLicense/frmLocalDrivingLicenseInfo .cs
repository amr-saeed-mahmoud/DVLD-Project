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
    public partial class frmLocalDrivingLicenseInfo : Form
    {
        int ApplictionID = -1;
        public frmLocalDrivingLicenseInfo(int ApplictionID)
        {
            InitializeComponent();
            this.ApplictionID = ApplictionID;
        }

        private void frmLocalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(ApplictionID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
