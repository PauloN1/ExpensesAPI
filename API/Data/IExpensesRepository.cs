using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public interface IExpensesRepository : IRepositoryBase<Expenses>
    {
        IEnumerable<Expenses> GetPastMonth();  
        IEnumerable<Expenses> GetUserExpenses(string userName);             
    }
}