using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BillTracker.BusinessLayer.Interface;
using BillTracker.Entities;

namespace BillTracker.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// create new user by submitting user details and send success message on response
        /// Post api/User/NewUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("NewUser")]
        [HttpPost]
        public async Task<ActionResult<String>> NewUser(User user)
        {
            //Business logic goes here
            throw new NotImplementedException();


        }

        /// <summary>
        /// validate user present in db and send user details as response
        /// POST api/User/ValidateUser
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User details</returns>
        [Route("ValidateUser")]
        [HttpPost]
        public async Task<ActionResult<User>> ValidateUser(UserLogin userCredentials)
        {
            //Business logic goes here
            throw new NotImplementedException();

        }


    }
}
