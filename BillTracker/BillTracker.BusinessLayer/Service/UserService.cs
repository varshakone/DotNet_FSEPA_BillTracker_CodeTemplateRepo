using BillTracker.BusinessLayer.Interface;
using BillTracker.BusinessLayer.Service.Repository;
using BillTracker.DataLayer;
using BillTracker.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// call repository method to create new user into database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>New User Registered</returns>
        
        public async Task<string> RegisterNewUser(User user)
        {
            //Business logic goes here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Call repository method to validate user exist into database or not
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User details</returns>
        public async Task<User> VerifyUser(UserLogin user)
        {
            //Business logic goes here
            throw new NotImplementedException();
        }
    }
}
