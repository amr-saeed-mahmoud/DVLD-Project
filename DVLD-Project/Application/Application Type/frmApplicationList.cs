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
    public partial class frmApplicationList : Form
    {

        DataTable Applactions;

        public frmApplicationList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmApplicationList_Load(object sender, EventArgs e)
        {
            Applactions = ApplactionType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = Applactions;
            lbRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 110;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 103;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int) dgvApplicationTypes.CurrentRow.Cells[0].Value;
            Form frm = new frmEditApplications(ApplicationID);
            frm.ShowDialog();
            this.frmApplicationList_Load(null, null);
        }

        private void dgvApplicationTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
