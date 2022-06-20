using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class SeedData
    {
        public static void EnsurePopulate(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.CreateScope()
                 .ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManager = app.ApplicationServices.CreateScope()
                 .ServiceProvider.GetService<UserManager<AppUser>>();
                var userLists = new List<AppUser>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!userManager.Users.Any())
            {
                //logic to generate add new users
                var appusers = new string[] {
                   "Jimme", "Betty", "Julia", "Jonia", "Zola"
                };

                for (int i=0; i < 5; i++)
                {
                    AppUser appUser = new AppUser
                    {
                        UserName = appusers[i],
                        Email = appusers[i] + "@gmail.com"
                    };
                     
                    if(!userLists.Contains(appUser)){
                        userLists.Add(appUser);
                   
                    }

                }
                
                    foreach (var item in userLists)
                    {
                        userManager.CreateAsync(item, "P@ssword123");
                    }
            }
            if (!context.Expenses.Any())
            {
                
                 //logic to generate add new users 
                 var expenses = new Faker("en");
                 
                 
                 /*ForRule(s => s.Date, f => f.Date.Recent())
                .ForRule(s => s.Amount, f => f.Amount.Price())
                .ForRule(s => s.Description, f=> f.Lorem.Sentence());*/

               // var expenseList = expenses.Generate(35);

                for(int i=0; i < 35; i++){
                    var rand = new Random();
                    int index = rand.Next(0, 5);

                    int amount = rand.Next(100, 10000);

                    var username = userLists[index].UserName;

                    context.Expenses.Add(new Expenses{
                        Date = expenses.Date.Recent(),
                        Description = expenses.Lorem.Sentence(),
                        Amount = amount,
                        UserName = username
                    });
                }
            }
            context.SaveChanges();
    }
   }   
}