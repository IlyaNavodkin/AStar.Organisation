using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Factories
{
    public class EntityFactory
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Name = "Иванов Иван",
                    Email = "ivanov@example.com",
                    Phone = "1234567890"
                },
                new Customer
                {
                    Name = "Петров Петр",
                    Email = "petrov@example.com",
                    Phone = "9876543210"
                },
                new Customer
                {
                    Name = "Сидорова Елена",
                    Email = "sidorova@example.com",
                    Phone = "5555555555"
                },
                new Customer
                {
                    Name = "Смирнова Ольга",
                    Email = "smirnova@example.com",
                    Phone = "1111111111"
                },
                new Customer
                {
                    Name = "Кузнецов Алексей",
                    Email = "kuznetsov@example.com",
                    Phone = "2222222222"
                },
                new Customer
                {
                    Name = "Васильева Мария",
                    Email = "vasilieva@example.com",
                    Phone = "3333333333"
                },
                new Customer
                {
                    Name = "Николаева Анна",
                    Email = "nikolaeva@example.com",
                    Phone = "4444444444"
                },
                new Customer
                {
                    Name = "Морозов Денис",
                    Email = "morozov@example.com",
                    Phone = "6666666666"
                },
                new Customer
                {
                    Name = "Егоров Дмитрий",
                    Email = "egorov@example.com",
                    Phone = "7777777777"
                },
                new Customer
                {
                    Name = "Лебедева Алина",
                    Email = "lebedeva@example.com",
                    Phone = "8888888888"
                }
            };
        }

        public static Customer GetCustomer() => 
            new() {Name = "Владимир Владимирович", Email = "VVP@gmail.com", Phone = "880055535"};
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Телевизор",
                    Description = "Большой ЖК-телевизор с высоким разрешением",
                    Price = 9999.99m
                },
                new Product
                {
                    Name = "Микроволновая печь",
                    Description = "Мощная микроволновка с различными режимами приготовления",
                    Price = 1999.99m
                },
                new Product
                {
                    Name = "Утюг",
                    Description = "Паровой утюг с функцией автоматического отключения",
                    Price = 499.99m
                },
                new Product
                {
                    Name = "Пылесос",
                    Description = "Робот-пылесос для автоматической уборки дома",
                    Price = 2999.99m
                },
                new Product
                {
                    Name = "Кофемашина",
                    Description = "Автоматическая кофемашина для приготовления вкусного кофе",
                    Price = 3999.99m
                },
                new Product
                {
                    Name = "Фен",
                    Description = "Мощный фен для быстрой сушки волос",
                    Price = 799.99m
                },
                new Product
                {
                    Name = "Планшет",
                    Description = "Современный планшет с большим экраном и высокой производительностью",
                    Price = 6999.99m
                },
                new Product
                {
                    Name = "Монитор",
                    Description = "Качественный монитор для компьютера с широкими углами обзора",
                    Price = 2999.99m
                },
                new Product
                {
                    Name = "Принтер",
                    Description = "Лазерный принтер с возможностью двусторонней печати",
                    Price = 1999.99m
                },
                new Product
                {
                    Name = "Наушники",
                    Description = "Беспроводные наушники с шумоподавлением",
                    Price = 1499.99m
                }
            };
        }
        public static Product GetProduct() => 
            new() {Name = "Робот пылесос с китая", Description = "Моет и сосет", Price = 200};

    }
}