using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace HealthCenterDataAccess
{

    public class PeopleDTO
    {

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

        public PeopleDTO(int PersonID,string NationalNo,string FirstName,string LastName,string Phone,string Email,string Address,DateTime DateOfBirth,string Gender,string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
        }
    }

    public class clsPeopleDataAccess
    {
        public static int AddPerson(PeopleDTO PersonDTO)
        {     

            using (var connection = new SqlConnection(clsHealthCenterDataAccessSettings._connectionString))
            using (var command = new SqlCommand("SP_AddPerson", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NationalNo", PersonDTO.NationalNo);//not null
                command.Parameters.AddWithValue("@FirstName", PersonDTO.FirstName);//not null
                if (string.IsNullOrEmpty(PersonDTO.LastName))
                {
                    command.Parameters.AddWithValue("@LastName", DBNull.Value);

                }
                else 
                {
                command.Parameters.AddWithValue("@LastName", PersonDTO.LastName);
                }
                command.Parameters.AddWithValue("@Phone", PersonDTO.Phone);//not null
                if(string.IsNullOrEmpty(PersonDTO.Email))
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", PersonDTO.Email);
                }
                if(string.IsNullOrEmpty(PersonDTO.Address))
                {
                    command.Parameters.AddWithValue("@Address", DBNull.Value);

                }
                else
                {
                    command.Parameters.AddWithValue("@Address", PersonDTO.Address);
                }
                command.Parameters.AddWithValue("@DateOfBirth", PersonDTO.DateOfBirth);//not null
                command.Parameters.AddWithValue("@Gender", PersonDTO.Gender);//not null
                if(string.IsNullOrEmpty(PersonDTO.ImagePath))
                {
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@ImagePath", PersonDTO.ImagePath);
                }



                var outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                connection.Open();
                command.ExecuteNonQuery();

                return outputIdParam.Value == DBNull.Value ? -1 : (int)outputIdParam.Value;

            }
        }



    }
}
