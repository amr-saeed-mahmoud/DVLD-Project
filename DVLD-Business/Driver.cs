﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcess;

namespace DVLD_Business
{
    public class Driver
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public Person PersonInfo;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }

        public Driver()

        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }

        public Driver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = Person.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            //call DataAccess Layer 

            this.DriverID = DriverData.AddNewDriver(PersonID, CreatedByUserID);


            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return DriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static Driver FindByDriverID(int DriverID)
        {

            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (DriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))

                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static Driver FindByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (DriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))

                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static DataTable GetAllDrivers()
        {
            return DriverData.GetAllDrivers();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return DVLD_Business.License.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return InternationalLicense.GetDriverInternationalLicenses(DriverID);
        }



    }
}
