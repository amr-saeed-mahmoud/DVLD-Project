using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAcess;


namespace DVLD_Business
{
    public class Person
    {

        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string FirstName {  get; set; }
        public string SecondName {  get; set; }
        public string ThirdName {  get; set; }
        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; } }
        public string NationalNo {  get; set; }
        public string Address { get; set; }
        public string ImagePath {  get; set; }
        public int NationalityCountryID {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor {  get; set; }
        public string Phone { set; get; }
        public string Email { set; get; }

        public CountryInfo Country = new CountryInfo();

        public Person()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private Person(int PersonID, string FirstName, string SecondName, string ThirdName,
                       string LastName, string NationalNo, DateTime DateOfBirth, short Gendor,
                       string Address, string Phone, string Email,
                       int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.Country.CountryID = NationalityCountryID;
            this.Country.CountryName = CountryInfo.GetCountryName(NationalityCountryID);
            Mode = enMode.Update;
        }

        private bool AddNewPerson()
        {
            PersonID = PersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                         this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return PersonID > 0;
        }
        private bool UpdatePerson()
        {
            return PersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                         this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }
        public static bool DeletePerson(int PersonID)
        {
            return PersonData.DeletePerson(PersonID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:

                    if (UpdatePerson())
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        public static Person Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = PersonData.GetPersonInfoByID
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                return new Person(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public static Person Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = PersonData.GetPersonInfoByNationalNo
                                (
                                    NationalNo, ref PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)

                return new Person(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public static DataTable GetAllPeople()
        {
            return PersonData.GetAllPeople();
        }
        public static bool IsExist(int PersonID)
        {
            return PersonData.IsPersonExist(PersonID);
        }
        public static bool IsExist(string NationalNo)
        {
            return PersonData.IsPersonExist(NationalNo);
        }
    }
}
