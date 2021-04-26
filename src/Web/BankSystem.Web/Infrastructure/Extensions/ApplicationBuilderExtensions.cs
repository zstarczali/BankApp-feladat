using BankSystem.Services.BankAccount;

namespace BankSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BankSystem.Models;
    using Common;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            InitializeDatabaseAsync(app).GetAwaiter().GetResult();

            return app;
        }

        private static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<BankSystemDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<BankUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                await dbContext.Database.MigrateAsync();
                await SeedUser(userManager, dbContext);

                if (!await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
                }
            }
        }

        private static async Task SeedUser(UserManager<BankUser> userManager, BankSystemDbContext dbContext)
        {


            if (!dbContext.Users.Any())
            {
                var user = new BankUser
                {
                    Email = "test@hvg.com",
                    FullName = "Nagy Zoltán",
                    UserName = "test@hvg.com",
                    EmailConfirmed = true,
                    BankAccounts = new List<BankAccount>
                    {
                        new BankAccount
                        {
                            Balance = 1000000,
                            Credit = 50000,
                            InterestRate = 10,

                            Currency = BankAccount.CurrencyType.HUF,
                            CreatedOn = DateTime.UtcNow,
                            Name = "Main account",
                            UniqueId = "ABCL73206545",
                            TypeId = BankAccount.AccountType.NormalAccount
                        },
                        new BankAccount
                        {
                            Balance = 5000, // one time bonus
                            Credit = 0,
                            InterestRate = 0,

                            Currency = BankAccount.CurrencyType.HUF,
                            CreatedOn = DateTime.UtcNow,
                            Name = "Savings account",
                            UniqueId = "ABCU18458713",
                            TypeId = BankAccount.AccountType.SavingAccount
                        }
                    }
                };

                await userManager.CreateAsync(user, "Admin1!");
            }
        }

        public static IApplicationBuilder AddDefaultSecurityHeaders(
            this IApplicationBuilder app,
            SecurityHeadersBuilder builder)
            => app.UseMiddleware<SecurityHeadersMiddleware>(builder.Policy());
    }
}