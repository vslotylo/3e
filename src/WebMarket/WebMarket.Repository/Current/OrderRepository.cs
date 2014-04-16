using System;
using System.Data.Entity;
using System.Linq;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Entities.Enums;
using WebMarket.Repository.Extensions;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Current
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Order Find(object id)
        {
            return DbContext.Orders.Include(obj => obj.Items).FirstOrDefault(obj => obj.Id == (int)id);
        }

        public override void Update(Order order)
        {
            var dbOrder = DbContext.Orders.Find(order.Id);
            DbContext.Entry(dbOrder).State = EntityState.Modified;
            dbOrder.Status = order.Status;
            if (order.Status == Status.Completed || order.Status == Status.Refunded)
            {
                dbOrder.CloseDate = DateTime.UtcNow.ToUkrainianTimeZone();
            }

            DbContext.SaveChanges();
        }
    }
}
