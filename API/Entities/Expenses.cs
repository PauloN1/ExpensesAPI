using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Expenses
    {
        public int Id{get;set;}
        public DateTime Date{get;set;}
        public string Description{get;set;}
        public decimal Amount{get;set;}
        public string UserName{get;set;}
    }
}