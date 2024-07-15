using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        enMode Mode = enMode.AddNew;
        Person person;
        int PersonID;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            person = new Person();
            Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            Mode = enMode.Update;
        }

        private void LoadData()
        {

            person = Person.Find(PersonID);

            if (person == null)
            {
                MessageBox.Show("No Person with ID = " + PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = PersonID.ToString();
            txtFirstName.Text = person.FirstName;
            txtSecondName.Text = person.SecondName;
            txtThirdName.Text = person.ThirdName;
            txtLastName.Text = person.LastName;
            txtNationalNo.Text = person.NationalNo;
            dtpDateOfBirth.Value = person.DateOfBirth;

            if (person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = person.Address;
            txtPhone.Text = person.Phone;
            txtEmail.Text = person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(person.Country.CountryName);


            if (person.ImagePath != "")
            {
                pbPerson.ImageLocation = person.ImagePath;

            }

            llbRemove.Visible = (person.ImagePath != "");

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
            if (Mode == enMode.Update)
                LoadData();
        }

        private void FailCountries()
        {
            DataTable Countries = CountryInfo.GetAllCountries();
            foreach (DataRow Country in Countries.Rows)
                cbCountry.Items.Add(Country["CountryName"]);
        }
        private void SetDefaultValues()
        {

            if (Mode == enMode.AddNew)
                lbTitle.Text = "Add New Person";
            else lbTitle.Text = "Update Person";

            FailCountries();

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

            llbRemove.Visible = (pbPerson.ImageLocation != "");
        }




       private void rdCheck(object sender, EventArgs e)
        {
            if (rbMale.Checked)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;
       }





        private void ValidName(object sender, CancelEventArgs e)
        {

            TextBox Temp = ((TextBox)sender);
            if (!Validation.IsValidName(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Enter a Vliad Name");
            }
            else
                errorProvider1.SetError(Temp, null);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "")
                return;

            if (!Validation.IsValidEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtThirdName.Text))
            {
                errorProvider1.SetError(txtThirdName, null);
                return;
            }
            
            if(!Validation.IsValidName(txtThirdName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtThirdName, "Enter a Vliad Name");
            }
            else
                errorProvider1.SetError(txtThirdName, null);
        }

        private void txtValidNumber(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (!Validation.IsValidNumber(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Invalid Number!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Enter a Valid Address");
            }
            else
                errorProvider1.SetError(txtAddress, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool HandlePersonImage()
        {
            if(person.ImagePath != pbPerson.ImageLocation)
            {
                if(person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(person.ImagePath);
                    }catch (Exception) { }
                }

                if(pbPerson.ImageLocation != null)
                {
                    string SourceImageFile = pbPerson.ImageLocation.ToString();
                    if (Util.CopyFileToImageFolder(ref SourceImageFile))
                    {
                        pbPerson.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!HandlePersonImage())
                return;

            

            person.FirstName = txtFirstName.Text.Trim();
            person.SecondName = txtSecondName.Text.Trim();
            person.ThirdName = txtThirdName.Text.Trim();
            person.LastName = txtLastName.Text.Trim();
            person.NationalNo = txtNationalNo.Text.Trim();
            person.Email = txtEmail.Text.Trim();
            person.Phone = txtPhone.Text.Trim();
            person.Address = txtAddress.Text.Trim();
            person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                person.Gendor = (short)enGendor.Male;
            else
                person.Gendor = (short)enGendor.Female;

            person.NationalityCountryID = CountryInfo.GetCountryID(cbCountry.SelectedItem.ToString());

            if (pbPerson.ImageLocation != null)
                person.ImagePath = pbPerson.ImageLocation;
            else
                person.ImagePath = "";

            if (person.Save())
            {
                lblPersonID.Text = person.PersonID.ToString();
                //change form mode to update.
                Mode = enMode.Update;
                lbTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void llbAddImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.FileName = person.ImagePath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPerson.Load(selectedFilePath);
                llbRemove.Visible = true;
            }
        }

        private void llbRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPerson.ImageLocation = null;
            llbRemove.Visible = false;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != person.NationalNo && Person.IsExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }
    }
}
