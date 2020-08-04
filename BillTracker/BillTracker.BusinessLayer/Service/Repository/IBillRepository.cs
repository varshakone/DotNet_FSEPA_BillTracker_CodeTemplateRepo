using BillTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillTracker.BusinessLayer.Service.Repository
{
   public interface IBillRepository
    {
        Task<bool> SaveBillAsync(BillDetails billdetails);
        Task<List<BillDetails>> GetAllBillsAsync();
        Task<List<BillDetails>> GetAllBillsByDueDateAsync(DateTime dueDate);
    }
}
