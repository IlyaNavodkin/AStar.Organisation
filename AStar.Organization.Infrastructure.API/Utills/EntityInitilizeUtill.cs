using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Contexts;

namespace AStar.Organisation.Infrastructure.API.Utills
{
    public class EntityInitilizeUtill
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Иванов Иван",
                    Email = "ivanov@example.com",
                    Phone = "1234567890"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Петров Петр",
                    Email = "petrov@example.com",
                    Phone = "9876543210"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Сидорова Елена",
                    Email = "sidorova@example.com",
                    Phone = "5555555555"
                },
                new Customer
                {
                    Id = 4,
                    Name = "Смирнова Ольга",
                    Email = "smirnova@example.com",
                    Phone = "1111111111"
                },
                new Customer
                {
                    Id = 5,
                    Name = "Кузнецов Алексей",
                    Email = "kuznetsov@example.com",
                    Phone = "2222222222"
                },
                new Customer
                {
                    Id = 6,
                    Name = "Васильева Мария",
                    Email = "vasilieva@example.com",
                    Phone = "3333333333"
                },
                new Customer
                {
                    Id = 7,
                    Name = "Николаева Анна",
                    Email = "nikolaeva@example.com",
                    Phone = "4444444444"
                },
                new Customer
                {
                    Id = 8,
                    Name = "Морозов Денис",
                    Email = "morozov@example.com",
                    Phone = "6666666666"
                },
                new Customer
                {
                    Id = 9,
                    Name = "Егоров Дмитрий",
                    Email = "egorov@example.com",
                    Phone = "7777777777"
                },
                new Customer
                {
                    Id = 10,
                    Name = "Лебедева Алина",
                    Email = "lebedeva@example.com",
                    Phone = "8888888888"
                }
            };
        }

        public static Customer GetCustomer() => 
            new() {Id = 11, Name = "Владимир Владимирович", Email = "VVP@gmail.com", Phone = "880055535"};
        
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Телевизор",
                    Description = "Большой ЖК-телевизор с высоким разрешением",
                    Price = 9999.99m
                },
                new Product
                {
                    Id = 2,
                    Name = "Микроволновая печь",
                    Description = "Мощная микроволновка с различными режимами приготовления",
                    Price = 1999.99m
                },
                new Product
                {
                    Id = 3,
                    Name = "Утюг",
                    Description = "Паровой утюг с функцией автоматического отключения",
                    Price = 499.99m
                },
                new Product
                {
                    Id = 4,
                    Name = "Пылесос",
                    Description = "Робот-пылесос для автоматической уборки дома",
                    Price = 2999.99m
                },
                new Product
                {
                    Id = 5,
                    Name = "Кофемашина",
                    Description = "Автоматическая кофемашина для приготовления вкусного кофе",
                    Price = 3999.99m
                },
                new Product
                {
                    Id = 6,
                    Name = "Фен",
                    Description = "Мощный фен для быстрой сушки волос",
                    Price = 799.99m
                },
                new Product
                {
                    Id = 7,
                    Name = "Планшет",
                    Description = "Современный планшет с большим экраном и высокой производительностью",
                    Price = 6999.99m
                },
                new Product
                {
                    Id = 8,
                    Name = "Монитор",
                    Description = "Качественный монитор для компьютера с широкими углами обзора",
                    Price = 2999.99m
                },
                new Product
                {
                    Id = 9,
                    Name = "Принтер",
                    Description = "Лазерный принтер с возможностью двусторонней печати",
                    Price = 1999.99m
                },
                new Product
                {
                    Id = 10,
                    Name = "Наушники",
                    Description = "Беспроводные наушники с шумоподавлением",
                    Price = 1499.99m
                }
            };
        }
        
        public static Product GetProduct() => 
            new() {Id = 11, Name = "Робот пылесос с китая", Description = "Моет и сосет", Price = 200};

        public static void SeedTestsData(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var products = GetProducts();
            var customers = GetCustomers();

            context.Product.AddRange(products);
            context.Customer.AddRange(customers);

            context.SaveChanges();
        }
    }
}