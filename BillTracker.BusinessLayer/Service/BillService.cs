using BillTracker.BusinessLayer.Interface;
using BillTracker.BusinessLayer.Service.Repository;
using BillTracker.DataLayer;
using BillTracker.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Service
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        private IMongoCollection<BillDetails> _mongoCollection;
        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
       
        /// <summary>
        /// Call repository method to retrieve all generated bills
        /// </summary>
        /// <returns></returns>
        public async Task<List<BillDetails>> GetAllBillsAsync()
        {
            //Business Logic goes here
            try
            {                     
                    var lstBills =await _billRepository.GetAllBillsAsync();
                    return lstBills;
            
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        /// <summary>
        /// Call repository method to retrieve all generated bills filtered by bill due date
        /// </summary>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public async Task<List<BillDetails>> GetAllBillsByDueDateAsync(DateTime dueDate)
        {
            //Business logic goes here
            try
            {
                var lstBillsByDueDate = await _billRepository.GetAllBillsByDueDateAsync(dueDate);
                return lstBillsByDueDate;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        /// <summary>
        /// Call repository method to save generated bills into database
        /// </summary>
        /// <param name="billdetails"></param>
        /// <returns></returns>

        public async Task<bool> SaveBillAsync(BillDetails billdetails)
        {
            //Business logic goes here
            try
            {
                                
                if (billdetails != null)
                {

                    var result =await _billRepository.SaveBillAsync(billdetails);
                    return result;
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
