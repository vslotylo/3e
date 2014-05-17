using System;
using System.Web.Security;
using WebMarket.Common;
using WebMarket.Repository.Entities;

namespace WebMarket.Extensions.Entities
{
    public static class ProductExtensions
    {
        public static Product MarkAsEdited(this Product product)
        {
            product.LastModifiedDate = DateTime.Now.ToUkrainianTimeZone();
            var user = Membership.GetUser();
            if (user != null)
            {
                product.LastModifiedBy = user.UserName;
            }

            return product;
        }
    }
}