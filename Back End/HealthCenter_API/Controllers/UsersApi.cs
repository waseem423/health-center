using HealthCenterBussinessLayer;
using HealthCenterDataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCenter_API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersApi : ControllerBase
    {


        [HttpPost(Name = "AddUser")]
        public bool AddnewUserFromRegister(UserDTOForRegister newUserDTOForRegister)
        {
            if (newUserDTOForRegister == null || string.IsNullOrEmpty(newUserDTOForRegister.FirstName) 
                || string.IsNullOrEmpty(newUserDTOForRegister.NationalNo) || string.IsNullOrEmpty(newUserDTOForRegister.Username)
                || string.IsNullOrEmpty(newUserDTOForRegister.Password)
                || string.IsNullOrEmpty(newUserDTOForRegister.Phone)|| string.IsNullOrEmpty(newUserDTOForRegister.Gender))
            {
                return false;
            }

            clsPeopleBussiness Person = new clsPeopleBussiness(new PeopleDTO(-1,newUserDTOForRegister.NationalNo,
                newUserDTOForRegister.FirstName,newUserDTOForRegister.LastName,newUserDTOForRegister.Phone, newUserDTOForRegister.Email,
                newUserDTOForRegister.Address, newUserDTOForRegister.DateOfBirth, newUserDTOForRegister.Gender, newUserDTOForRegister.ImagePath));

            if(Person.Save())
            {
                clsUsersBussiness User = new clsUsersBussiness(new UserDTO(-1, Person.PersonID, newUserDTOForRegister.Username, newUserDTOForRegister.Password,
                    newUserDTOForRegister.Role));

                if(User.Save())
                {
                    return true;
                }
            }

            return false;

        }


        // this for Login
        [HttpGet("{Username}/{Password}", Name = "GetUserByUsernameandpassword")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUsernameandPassword(string Username,string Password)
        {
            if (string.IsNullOrEmpty(Username)|| string.IsNullOrEmpty(Password))
            {
                return BadRequest($"Not accepted empty Username,Password");

            }

            var User = clsUsersBussiness.Find(Username,Password);

            if (User == null)
                return NotFound($"User with Username {Username} and Password {Password} Is Not Found.");

            UserDTO UDTO = User.UDTO;

            return Ok(UDTO);
        }

        

    }
}
