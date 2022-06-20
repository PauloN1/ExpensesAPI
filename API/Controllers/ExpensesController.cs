using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Entities;
using AutoMapper;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class ExpensesController : BaseController
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public ExpensesController(ILogger<ExpensesController> logger, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _logger = logger;
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<ExpensesDTO> Get()
        {
            IEnumerable<Expenses> expenses = _wrapper.Expenses.GetUserExpenses(User.Identity.Name);
            return _mapper.Map<IEnumerable<ExpensesDTO>>(expenses);
        }
        [HttpGet("pastmount")]
        public IEnumerable<ExpensesDTO> PastMonth()
        {
            var expenses = _wrapper.Expenses.GetPastMonth();
            return  _mapper.Map<IEnumerable<ExpensesDTO>>(expenses);
        }
        [HttpGet("{id}")]
        public ActionResult<ExpensesDTO> GetById(int id){
            int statusCode = 0;
            string message = "";
             if(!User.Identity.IsAuthenticated){
                  statusCode = 403;
                  message = "Access Forbidden! Please login first!";
                  return StatusCode(statusCode, message);
            }
            var expense = _wrapper.Expenses.GetById(id);
            return _mapper.Map<ExpensesDTO>(expense);
        }
        [HttpPost]
        public ActionResult Create(ExpenseModel expense){
            int statusCode = 0;
            string message = "";

            if(!User.Identity.IsAuthenticated){
                  statusCode = 403;
                  message = "Access Forbidden! Please login first!";
                  return StatusCode(statusCode, message);
            }

            try{
                var newExpense = new Expenses{
                    Date = DateTime.Now,
                    Description = expense.Description,
                    Amount = expense.Amount,
                    UserName = User.Identity.Name
                };
                _wrapper.Expenses.Create(newExpense);
                if(_wrapper.Expenses.SaveChanges() > 0){
                      statusCode = 200;
                      message = "New Expense Created Successful!";
                } else{
                    statusCode = 400;
                      message = "Unknown Error: Failed to create new Expense";
                }
            }catch(Exception ex){
                statusCode = 500;
                message = ex.Message;
            }
            return StatusCode(statusCode, message);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, ExpenseModel expense){
            int statusCode = 0;
            string message = "";

            if(!User.Identity.IsAuthenticated){
                  statusCode = 403;
                  message = "Access Forbidden! Please login first!";
                  return StatusCode(statusCode, message);
            }

            try{
                var newExpense = _wrapper.Expenses.GetById(id);
                newExpense.Description = expense.Description;
                newExpense.Amount = expense.Amount;

                if(newExpense == null)
                  return NotFound();

                _wrapper.Expenses.Update(newExpense);
                if(_wrapper.Expenses.SaveChanges() > 0){
                      statusCode = 200;
                      message = "Expense Updated Successful!";
                } else{
                    statusCode = 400;
                      message = "Unknown Error: Failed to update Expense";
                }


            }catch(Exception ex){
                statusCode = 500;
                message = ex.Message;
            }
            return StatusCode(statusCode, message);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            int statusCode = 0;
            string message = "";
           if(!User.Identity.IsAuthenticated){
                  statusCode = 403;
                  message = "Access Forbidden! Please login first!";
                  return StatusCode(statusCode, message);
            }
            try{
                  var newExpense = _wrapper.Expenses.GetById(id);
                  if(newExpense == null)
                    return NotFound();
                _wrapper.Expenses.Delete(newExpense);
                if(_wrapper.Expenses.SaveChanges() > 0){
                      statusCode = 200;
                      message = "Expense Deleted Successful!";
                } else{
                    statusCode = 400;
                      message = "Unknown Error: Failed to delete Expense";
                }

            }
            catch(Exception ex){
                statusCode = 500;
                message = ex.Message;
            }
            return StatusCode(statusCode, message);
        }

    }
}