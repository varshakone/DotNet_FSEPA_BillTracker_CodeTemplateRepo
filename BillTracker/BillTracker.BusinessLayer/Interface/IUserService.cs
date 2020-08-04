using BillTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Interface
{
  public  interface IUserService
    {
        Task<String> RegisterNewUser(User user);
        Task<User> VerifyUser(UserLogin user);
    }
}
