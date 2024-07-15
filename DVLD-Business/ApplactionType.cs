using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcess;

namespace DVLD_Business
{
    public class ApplactionType
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;
        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public ApplactionType()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        public ApplactionType(int ID, string ApplicationTypeTitel, float ApplicationTypeFees)
        {
            this.ID = ID;
            this.Title = ApplicationTypeTitel;
            this.Fees = ApplicationTypeFees;
            Mode = enMode.Update;
        }
        private bool _AddNewApplicationType()
        {
            this.ID = ApplactionTypeData.AddNewApplicationType(this.Title, this.Fees);
            return (this.ID != -1);
        }

        private bool _UpdateApplicationType()
        {
            return ApplactionTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public static ApplactionType Find(int ID)
        {
            string Title = ""; float Fees = 0;
            if (ApplactionTypeData.GetApplicationTypeInfoByID((int)ID, ref Title, ref Fees))
                return new ApplactionType(ID, Title, Fees);
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return ApplactionTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplicationType();

            }
            return false;
        }
    }
}
