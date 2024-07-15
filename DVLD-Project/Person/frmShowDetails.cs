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
    public partial class frmShowDetails : Form
    {
        int PersonID = -1;
        public frmShowDetails(int personID)
        {
            InitializeComponent();
            this.PersonID = personID;
        }

        private void ShowDetails_Load(object sender, EventArgs e)
        {
            ctrPersonCard1.LoadPersonInfo(PersonID);
        }

        private void ctrPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
