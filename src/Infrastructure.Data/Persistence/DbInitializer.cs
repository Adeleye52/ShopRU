using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Data.Persistence
{
    public static class DbInitializer
    {
        /// <summary>
        /// This role seeds a basic platform roles on start of the application
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task SeedDiscount(this IHost host)
        {
            var serviceProvider = host.Services.CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureCreatedAsync();

            if (!context.Discounts.Any())
            {
                List<Discount> discounts = new()
                {
                    new Discount()
                    {
                        Type = EDiscountType.Employee.ToString(),
                        Description= "This discount is  for an employee of the store",
                        Percentage = 30
                    },
                     new Discount()
                    {
                        Type = EDiscountType.Affilate.ToString(),
                        Description= "This discount is  for an affilate of the store",
                        Percentage = 10
                    },
                     new Discount()
                    {
                        Type = EDiscountType.LongTermUser.ToString(),
                        Description= "This discount is  for a long term customer of the store",
                        Percentage = 5
                    },
                     new Discount()
                    {
                        Type = EDiscountType.ProductPrice.ToString(),
                        Description= "This discount is for applied on every $100 on the bill",
                        Percentage = 5
                    },

                };
                await context.AddRangeAsync(discounts);
                await context.SaveChangesAsync();


            }
        }

        /// <summary>
        /// This method create seed data : super user who administrates the platform
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task SeedCustomer(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var context = serviceProvider.GetRequiredService<AppDbContext>();


            await context.Database.EnsureCreatedAsync();

            if (!context.Customers.Any())
            {
                List<Customer> customers = new()
                {
                    new Customer()
                    {
                        FirstName = "Ade",
                        LastName = "Bayo",
                        UserName = "adebayo",
                        EmailAddress = "adebayo@mailinator.com",
                        Type = ECustomerType.Affilate.ToString(),
                        AddressLine = "Lagos",
                        Country = "Nigeria",
                        State = "Lagos",
                        ZipCode = "10001",
                        CreatedAt = DateTime.UtcNow,

                    },
                    new Customer()
                    {
                        FirstName = "Kola",
                        LastName = "Olawale",
                        UserName = "kola43",
                        EmailAddress = "kola43@mailinator.com",
                        Type = ECustomerType.Employee.ToString(),
                        AddressLine = "Lagos",
                        Country = "Nigeria",
                        State = "Lagos",
                        ZipCode = "10001",
                        CreatedAt = DateTime.UtcNow,

                    },
                    new Customer()
                    {
                        FirstName = "Ola",
                        LastName = "Ade",
                        UserName = "olaade42",
                        EmailAddress = "olaade42@mailinator.com",
                        Type = ECustomerType.Other.ToString(),
                        AddressLine = "Lagos",
                        Country = "Nigeria",
                        State = "Lagos",
                        ZipCode = "10001",
                        CreatedAt = DateTime.UtcNow,

                    },
                    new Customer()
                    {
                        FirstName = "Sikemi",
                        LastName = "Olowo",
                        UserName = "solowo45",
                        EmailAddress = "solowo45@mailinator.com",
                        Type = ECustomerType.Other.ToString(),
                        AddressLine = "Lagos",
                        Country = "Nigeria",
                        State = "Lagos",
                        ZipCode = "10001",
                        CreatedAt = new DateTime(2016,12,3).ToUniversalTime()

                    },

                };
                await context.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }

        }
    }
}
