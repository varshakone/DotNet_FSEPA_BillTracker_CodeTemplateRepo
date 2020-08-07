using BillTracker.DataLayer;
using BillTracker.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Service.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDBContext _mongoContext;

        private IMongoCollection<User> _mongoCollection;
        public UserRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _mongoCollection = _mongoContext.GetCollection<User>(typeof(User).Name);
        }

        public async Task<string> RegisterNewUser(User user)
        {
            try
            {
                if (user != null)
                {
                   await _mongoCollection.InsertOneAsync(user);
                    return "User Register Successfully";
                }
                else
                {
                    throw new ArgumentNullException(typeof(User).Name + " object is null");
                }
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        public async Task<User> VerifyUser(UserLogin user)
        {
            try
            {
                if (user != null)
                {
                    FilterDefinition<User> nameFilter = Builders<User>.Filter.Eq(unm => unm.UserName, user.UserName);
                    FilterDefinition<User> passwordFilter = Builders<User>.Filter.Eq(unm => unm.Password, user.Password);

                    FilterDefinition<User> UnmPwd = Builders<User>.Filter.And(nameFilter, passwordFilter);
                    var result =await _mongoCollection.FindAsync<User>(UnmPwd).Result.FirstOrDefaultAsync();
                    return result;
                }
                else
                {
                    throw new ArgumentNullException(typeof(User).Name + " object is null");
                }
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }
    }
}
