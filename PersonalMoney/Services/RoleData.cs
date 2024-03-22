using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using PersonalMoney.Models.enums;

namespace PersonalMoney.Services
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[] { "ADMIN", "USER" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<PersonalMoneyContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new Role(role));
                    }
                }
                if(dbContext.Users.FirstOrDefault(u => u.UserName.Equals("admin")) == null){
                    var user = new User().AsBuilder().WithId(Guid.NewGuid().ToString())
                    .WithUserName("admin").WithEmailConfirmed(true).WithSecurityStamp(Guid.NewGuid().ToString())
                    .WithEmail("nguyntrng234@gmail.com").WithFirstName("ADMIN").WithConcurrencyStamp(Guid.NewGuid().ToString())
                    .WithPhoneNumberConfirmed(false).WithLockoutEnabled(true).WithAccessFailedCount(0).Build();
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    await userManager.AddToRoleAsync(user,UserRoles.ADMIN.ToString());
                    await userManager.AddPasswordAsync(user,"123456aA@");
                }
            }
        }
    }
}