using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class User
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public Person PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public User()
        {
            UserID = 0;
            PersonID = 0;
            PersonInfo = new Person();
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNew;
        }

        private User(int UserID, int PersonID, string Username, string Password,
            bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = Username;
            this.Password = Password;
            this.IsActive = IsActive;
            PersonInfo = Person.Find(PersonID);
            Mode = enMode.Update;
        }

        private bool AddNewUser()
        {
            this.UserID = UserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return this.UserID > 0;
        }
        private bool UpdateUser()
        {
            return UserData.UpdateUser(this.UserID, this.PersonID, this.UserName,
                this.Password, this.IsActive);
        }
        public static bool DeleteUser(int UserID)
        {
            return UserData.DeleteUser(UserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewUser()) { return true; }
                    break;
                case enMode.Update:
                    if (UpdateUser()) { return true; }
                    break;
                default:
                    return false;
            }
            return false;
        }

        public static User FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByUserID
                                (UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new User(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
        public static User FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new User(UserID, UserID, UserName, Password, IsActive);
            else
                return null;
        }
        public static User FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByUsernameAndPassword
                                (UserName, Password, ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                return new User(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static bool isUserExist(int UserID)
        {
            return UserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return UserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return UserData.IsUserExistForPersonID(PersonID);
        }

        public static DataTable GetAllUsers()
        {
            return UserData.GetAllUsers();
        }
    }
}
