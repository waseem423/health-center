using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterDataAccess
{

    public class UserDTO
    {
        //public enum enRole { Admin=1,NormalUser=2,Patient=3};
        //enRole _Role = enRole.Patient;

        public int UserID { set; get; }

        public int PersonID { set; get; }

        public string Username { set; get; }

        public string Password { set; get; }

        public string Role { set; get; }

        public bool IsActive { set; get; }

        public UserDTO(int UserID, int PersonID, string Username, string Password, string Role = "Patient" , bool IsActive = true)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.Username = Username;
            this.Password = Password;
            this.Role = Role;
            this.IsActive = IsActive;
        }

    }

    public class UserDTOForRegister
    {

        // public int PersonID { set; get; }

        public string NationalNo { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Phone { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string Gender { set; get; }

        public string ImagePath { set; get; }


        public string Username { set; get; }

        public string Password { set; get; }

        public string Role { set; get; }

        public bool IsActive { set; get; }

        public UserDTOForRegister(string NationalNo, string FirstName, string LastName, string Phone, string Email, string Address, DateTime DateOfBirth, string Gender, string ImagePath,
             string Username, string Password, string Role)
        {
            //this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.ImagePath = ImagePath;


            this.Username = Username;
            this.Password = Password;
            this.Role = Role;

        }
    }


    public class UserDTOForLogin
    {
        public string Username { set; get; }

        public string Password { set; get; }

        public UserDTOForLogin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }

    public class clsUsersDataAccess
    {
        public static int AddNewUser(UserDTO userDTO)
        {

            using (var connection = new SqlConnection(clsHealthCenterDataAccessSettings._connectionString))
            using (var command = new SqlCommand("SP_AddNewUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


                // @PersonID, @Username, @Password, @Role, @IsActive

                command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                command.Parameters.AddWithValue("@Username", userDTO.Username);
                command.Parameters.AddWithValue("@Password", userDTO.Password);
                command.Parameters.AddWithValue("@Role", userDTO.Role);
                command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);




                var outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                connection.Open();
                command.ExecuteNonQuery();

                return outputIdParam.Value == DBNull.Value ? -1 : (int)outputIdParam.Value;

            }

        }


        public static UserDTO GetUserByUsernameAndPassword(string Username,string Password)
        {
            using (var connection = new SqlConnection(clsHealthCenterDataAccessSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_GetGetUserByUserNameAndPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);


                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //int UserID, int PersonID, string Username, string Password, string Role = "Patient", bool IsActive = true
                            return new UserDTO
                                (
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("Username")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetString(reader.GetOrdinal("Role")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }


}
