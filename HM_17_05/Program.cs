using HM_17_05.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_17_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Shop1())
            {
                //// Создаем сервис магазина
                var serviceShop = new ServiceShop(context);

                //// Добавляем клиентов
                //serviceShop.AddClient(new Client { Name = "Иван", Email = "ivan@example.com" });
                //serviceShop.AddClient(new Client { Name = "Мария", Email = "maria@example.com" });

                //// Добавляем продукты
                //serviceShop.AddProduct(new Product { Name = "Ноутбук", Price = 1000 });
                //serviceShop.AddProduct(new Product { Name = "Смартфон", Price = 500 });


                //Найдите всех клиентов, у которых в адресе электронной почты содержится домен "example.com".
                var task1 = serviceShop.GetAllClients().Where(e => e.Email.Contains("example.com"));

                //Получите все продукты, цена которых меньше средней цены всех продуктов.
                var avgPrice_task2 = serviceShop.GetAllProducts().Average(e => e.Price);
                var task2 = serviceShop.GetAllProducts().Where(p => p.Price < avgPrice_task2);

                //Найдите клиента с самым длинным именем.
                var task3 = serviceShop.GetAllClients().OrderByDescending(n => n.Name.Length).FirstOrDefault();

                //Получите все заказы, сделанные клиентом с самым коротким именем.
                var clientShortName = serviceShop.GetAllClients().OrderBy(n => n.Name.Length).FirstOrDefault();
                var task4 = serviceShop.GetAllOrdersWithDetails().Where(c => c.ClientId == clientShortName.Id);

                //Найдите продукт с наименьшей ценой.
                var task5 = serviceShop.GetAllProducts().OrderBy(p => p.Price).FirstOrDefault();

                //Получите всех клиентов, у которых количество заказов превышает среднее количество заказов всех клиентов.
                var averagetask6 = serviceShop.GetAllOrdersWithDetails()
                    .GroupBy(o => o.ClientId)
                    .Select(g => new { ClientId = g.Key, OrderCount = g.Count() })
                    .Average(g => g.OrderCount);

                var task6Ids = serviceShop.GetAllOrdersWithDetails().GroupBy(o => o.ClientId)
                    .Select(g => new { ClientId = g.Key, OrderCount = g.Count() })
                    .Where(g => g.OrderCount > averagetask6).Select(g => g.ClientId).Distinct();

                var task6 = serviceShop.GetAllClients().Where(c => task6Ids.Contains(c.Id));

                //Найдите клиента, который сделал заказ с самым дорогим продуктом.
                var expenciveProd = serviceShop.GetAllProducts().OrderByDescending(p => p.Price).FirstOrDefault();

                var task7 = serviceShop.GetAllOrdersWithDetails().Where(p => p.Products.Contains(expenciveProd)).Select(c => c.Client);

                //Получите среднюю цену заказа.
                var averageOrderPrice = serviceShop.GetAllOrdersWithDetails()
                    .Select(o => o.Products.Sum(p => p.Price)).Average();

                //Найдите клиента, который сделал последний заказ в базе данных.
                var task9 = serviceShop.GetAllOrdersWithDetails().Last().Client;

                //Получите все заказы, содержащие продукты с ценой больше 800.
                var task10 = serviceShop.GetAllOrdersWithDetails().Where(o => o.Products.Any(p => p.Price > 800));

            }

        }
    }
}
