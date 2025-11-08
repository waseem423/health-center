using HealthCenterDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterBussinessLayer
{

    public class clsUsersBussiness
    {
        public enum enMode { AddNew=0,Update=1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }

        public int PersonID { set; get; }

        public string Username { set; get; }

        public string Password { set; get; }

        public string Role { set; get; }

        public bool IsActive { set; get; }

        public UserDTO UDTO
        {
            get { return (new UserDTO(this.UserID, this.PersonID, this.Username, this.Password, this.Role, this.IsActive)); }
        }

        public clsUsersBussiness(UserDTO UDTO,enMode cMode=enMode.AddNew)
        {
            this.UserID = UDTO.UserID;
            this.PersonID = UDTO.PersonID;
            this.Username = UDTO.Username;
            this.Password = UDTO.Password;
            this.Role = UDTO.Role;
            this.IsActive = UDTO.IsActive;
        }

        public static clsUsersBussiness Find(string Username,string Password)
        {
            UserDTO UDTO = clsUsersDataAccess.GetUserByUsernameAndPassword(Username, Password);

            if(UDTO!=null)
            {
                return new clsUsersBussiness(UDTO, enMode.Update);
            }

            return null;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(UDTO);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            this.Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
            }
            return false;
        }

    }
}
