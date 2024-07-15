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
    public partial class frmListPeople : Form
    {
        private static DataTable AllPeople;
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            FailList();
        }

        private void FailList()
        {
            AllPeople = Person.GetAllPeople();

            dgvPeople.Rows.Clear();
            foreach (DataRow row in AllPeople.Rows)
            {
                dgvPeople.Rows.Add(row["PersonID"], row["FirstName"], row["SecondName"], row["ThirdName"], row["LastName"],
                    row["GendorCaption"], row["NationalNo"], row["DateOfBirth"], row["CountryName"], row["Email"], row["Phone"]);
            }

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            FailList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            FailList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (Person.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FailList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonID = (int)dgvPeople.CurrentRow.Cells[0].Value; 
            Form frm = new frmAddUpdatePerson(SelectedPersonID);
            frm.ShowDialog();
            FailList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowDetails(SelectedPersonID);
            frm.ShowDialog();
            FailList();
        }
    }
}
