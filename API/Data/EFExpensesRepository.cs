using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFExpensesRepository : RepositoryBase<Expenses>, IExpensesRepository
    {
        public EFExpensesRepository(ApplicationDbContext appDbContext)
            : base(appDbContext)
        { }

        public IEnumerable<Expenses> GetPastMonth(){
             var lastDate = DateTime.Now.AddDays(30);
             return _appDbContext.Expenses.Where(s => s.Date <= lastDate);
        }
        public IEnumerable<Expenses> GetUserExpenses(string userName){
              var lastDate = DateTime.Now.AddDays(30);
             return _appDbContext.Expenses.Where(s => s.UserName == userName);
        }
        
    }
}