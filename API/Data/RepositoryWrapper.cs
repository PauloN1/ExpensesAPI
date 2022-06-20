using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _appDbContext;
        private IExpensesRepository _expenses;

        public RepositoryWrapper(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IExpensesRepository Expenses
        {
            get{
                if(_expenses == null)
                  _expenses = new EFExpensesRepository(_appDbContext);
                return _expenses;    
            }  
        }

    }
}