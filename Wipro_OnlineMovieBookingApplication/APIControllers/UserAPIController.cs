using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Wipro_OnlineMovieBookingApplication.DTO;

namespace Wipro_OnlineMovieBookingApplication.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserRepository iUserRepository;
        public UserAPIController(IUserRepository _iUserRepository)
        {
            iUserRepository = _iUserRepository;
        }

        [HttpGet("SingleUser")]
        public ActionResult GetSingleUser(int userId)
        {
            UserDTO userDTO = new UserDTO();
            User userEntity = iUserRepository.GetUser(userId);
            userDTO.UserId = userEntity.UserId;
            userDTO.UserName = userEntity.UserName;
            userDTO.Password = userEntity.Password;
            userDTO.Email = userEntity.Email;
            userDTO.FirstName = userEntity.FirstName;
            userDTO.LastName = userEntity.LastName;
            userDTO.Name = $"{userEntity.FirstName} {userEntity.LastName}";
            userDTO.Address = userEntity.Address;
            userDTO.ContactNo = userEntity.ContactNo;
            userDTO.ModifiedDate = DateTime.UtcNow;

            return Ok(userDTO);
        }

        [HttpPut("EditUser")]
        public ActionResult EditUser(UserDTO model)
        {
            User userEntity = new User()
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                ContactNo = model.ContactNo
            };
            iUserRepository.UpdateUser(userEntity);
            return Ok(userEntity);
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            return Ok(iUserRepository.DeleteUser(userId));
        }
    }
}
