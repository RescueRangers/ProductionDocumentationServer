﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> Get();

        Task<Order> GetbyId(int id);

        Task Post(Order order);

        Task<int> GetOrderId(string orderNumber);
        Task<string> GetOrderNumber(int orderId);
    }
}