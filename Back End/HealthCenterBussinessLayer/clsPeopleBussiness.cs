using HealthCenterDataAccess;

namespace HealthCenterBussinessLayer
{
    public class clsPeopleBussiness
    {
        public enum enMode { Addnew=0,Update=1};
        public enMode Mode = enMode.Addnew;

        //PersonID, NationalNo, FirstName, LastName, Phone, Email, Address, DateOfBirth, Gendor, ImagePath

        public int PersonID { set; get; }

        public string NationalNo { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Phone { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string Gender { set; get; }

        public string ImagePath { set; get; }

        public PeopleDTO PDTO
        {
            get { return (new PeopleDTO(this.PersonID, this.NationalNo, this.FirstName, this.LastName, this.Phone, this.Email, this.Address, this.DateOfBirth, this.Gender, this.ImagePath)); }
        }

        public clsPeopleBussiness(PeopleDTO PDTO,enMode cMode=enMode.Addnew)
        {
            this.PersonID = PDTO.PersonID;
            this.NationalNo = PDTO.NationalNo;
            this.FirstName = PDTO.FirstName;
            this.LastName = PDTO.LastName;
            this.Phone = PDTO.Phone;
            this.Email = PDTO.Email;
            this.Address = PDTO.Address;
            this.DateOfBirth = PDTO.DateOfBirth;
            this.Gender = PDTO.Gender;
            this.ImagePath = PDTO.ImagePath;
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleDataAccess.AddPerson(PDTO);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Addnew:
                    {
                        if (_AddNewPerson())
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
                        return _UpdatePerson();
                    }
            }
            return false;
        }
    }
}
