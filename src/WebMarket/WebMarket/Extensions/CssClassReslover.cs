using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Entities.Enums;
using System.Web.Mvc.Html;
namespace WebMarket.Extensions
{
    public static class CssClassReslover
    {
        public static string CssClass(this OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Pending:
                    return "warning";
                case OrderStatus.Cancelled:
                    return "error";
                case OrderStatus.Refunded:
                    return "error";
                case OrderStatus.Completed:
                    return "success";
                case OrderStatus.Processing:
                    return "info";
                case OrderStatus.OnHold:
                    return "info";
            }

            return string.Empty;
        }

        public static string CssRibbonClass(this Product product)
        {
            if (product.Discount > 0)
            {
                return "ribbon-sale";
            }

            switch (product.DisplayClass)
            {
                case DisplayClass.New:
                    return "ribbon-new";
                case DisplayClass.TopBuyed:
                    return "ribbon-top";
                case DisplayClass.BestPrice:
                    return "ribbon-bestprice";
                case DisplayClass.OurChoise:
                    return "ribbon-ourchoise";
                case DisplayClass.None:
                    return string.Empty;
                default: return string.Empty;
            }
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType)
        {
            Dictionary<string, string> enumItems = enumType.GetDescription();
            var list = enumItems.Select(pair => new SelectListItem { Value = pair.Key, Text = pair.Value }).ToList();
            return htmlHelper.DropDownListFor(expression, list);
        }

        public static Dictionary<string, string> GetDescription(this Type enumeration)
        {
            if (!enumeration.IsEnum)
            {
                throw new ArgumentException("passed type must be of Enum type", "enumerationValue");
            }

            var descriptions = new Dictionary<string, string>();
            var members = enumeration.GetMembers().Where(m => m.MemberType == MemberTypes.Field);

            foreach (MemberInfo member in members)
            {
                var attrs = member.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Count() != 0)
                    descriptions.Add(member.Name, ((DescriptionAttribute)attrs[0]).Description);
            }
            return descriptions;
        }
    }
}