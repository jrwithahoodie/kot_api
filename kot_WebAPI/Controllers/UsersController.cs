using BusinessLogic.User;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kot_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        private readonly IUserBll _usersBll;
        #endregion

        #region Builder
        public UsersController(IUserBll usersbll)
        {
            _usersBll = usersbll;
        }
        #endregion

        /// <summary>
        /// Get all users information.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var usersList = _usersBll.Get();
                return Ok(usersList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get user information by email.
        /// </summary>
        [HttpGet("{mail}")]
        public IActionResult GetUserByEmail(string mail)
        {
            try
            {
                var user = _usersBll.Get(mail);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get all users information by role.
        /// </summary>
        [HttpGet("role/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            try
            {
                var user = _usersBll.GetByRole(role);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// New user register.
        /// </summary>
        [HttpPost]
        public IActionResult Register([FromBody] User value)
        {
            try
            {
                if (null == value)
                    return BadRequest();

                var result = _usersBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete user by mail.
        /// </summary>
        [HttpDelete("{mail}")]
        public IActionResult DeleteUser(string mail)
        {
            try
            {
                _usersBll.Delete(mail);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
