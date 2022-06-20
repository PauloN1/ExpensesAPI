using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ExpensesDTO
    {
        public DateTime Date{get;set;}
        public string Description{get;set;}
        public decimal Amount{get;set;}
    }
}