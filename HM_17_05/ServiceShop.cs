using HM_17_05.Models;
using System.Linq;
using System.Data.Entity;

namespace HM_17_05
{
    internal class ServiceShop
    {
        private readonly Shop1 _shop1;

        public ServiceShop(Shop1 context)
        {
            _shop1 = context;
        }

        // Создание нового продукта
        public void AddProduct(Product product)
        {
            _shop1.Products.Add(product);
            _shop1.SaveChanges();
        }

        // Получение всех продуктов
        public IQueryable<Product> GetAllProducts()
        {
            return _shop1.Products;
        }

        // Создание нового заказа
        public void AddOrder(Order order)
        {
            _shop1.Orders.Add(order);
            _shop1.SaveChanges();
        }

        // Получение всех заказов
        public IQueryable<Order> GetAllOrders()
        {
            return _shop1.Orders;
        }

        // Создание нового клиента
        public void AddClient(Client client)
        {
            _shop1.Clients.Add(client);
            _shop1.SaveChanges();
        }

        // Получение всех клиентов
        public IQueryable<Client> GetAllClients()
        {
            return _shop1.Clients;
        }

        // Получение всех продуктов для определенного заказа
        public IQueryable<Product> GetProductsForOrder(int orderId)
        {
            return _shop1.Orders
                   .Where(o => o.Id == orderId)
                   .SelectMany(o => o.Products);
        }

        // Получение всех заказов с информацией о продуктах и клиентах
        public IQueryable<Order> GetAllOrdersWithDetails()
        {
            return _shop1.Orders
                   .Include(o => o.Products)
                   .Include(o => o.Client);
        }
    }
}
