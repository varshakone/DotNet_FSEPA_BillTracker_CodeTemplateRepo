using BillTracker.DataLayer;
using BillTracker.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Service.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly IMongoDBContext _mongoContext;

        private IMongoCollection<BillDetails> _mongoCollection;
        public BillRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _mongoCollection = _mongoContext.GetCollection<BillDetails>(typeof(BillDetails).Name);
        }
        public async Task<List<BillDetails>> GetAllBillsAsync()
        {
          try
            {
               var lstBills =await _mongoCollection.FindAsync(FilterDefinition<BillDetails>.Empty).Result.ToListAsync();
               return lstBills;
           }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        public async Task<List<BillDetails>> GetAllBillsByDueDateAsync(DateTime dueDate)
        {
            try
            {
              var dueDateFilter = Builders<BillDetails>.Filter.Gte(d => d.DueDate, new BsonDateTime(dueDate));
              var lstBillsByDueDate =await _mongoCollection.FindAsync(dueDateFilter).Result.ToListAsync();
              return lstBillsByDueDate;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        public async Task<bool> SaveBillAsync(BillDetails billdetails)
        {
            try
            {
                if (billdetails != null)
                {
                  await _mongoCollection.InsertOneAsync(billdetails);
                    return true;
                }
                else
                {
                    throw new ArgumentNullException(typeof(BillDetails).Name + " object is null");
                }
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }
    }
}
