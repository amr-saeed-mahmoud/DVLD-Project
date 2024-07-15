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
    public partial class frmTestTypeList : Form
    {
        DataTable AllTestType;

        public frmTestTypeList()
        {
            InitializeComponent();
        }

        private void frmTestTypeList_Load(object sender, EventArgs e)
        {
            AllTestType = TestType.GetAllTestTypes();
            dgvTestTypes.DataSource = AllTestType;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 92;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 640;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dgvTestTypes.CurrentRow.Cells[0].Value;
            Form frm = new frmEditTestType((TestType.enTestType)TestTypeID);
            frm.ShowDialog();
            this.frmTestTypeList_Load(null, null);
        }

        private void dgvTestTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
