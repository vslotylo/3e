﻿using System;
using System.Linq;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using PagedList;
using System.Linq.Expressions;
using WebMarket.DAL.Entities.Enums;

namespace WebMarket.Helpers
{
    public static class Sorting
    {
        public static IPagedList<T> ToSortedPagedList<T>(IQueryable<T> collection, Sort sortingMode, int pageSize, int currentPage) where T : Product 
        {
            int page = currentPage;
            if ((currentPage - 1) * pageSize > collection.Count())
            {
                page = 1;
            }

            switch (sortingMode)
            {
                case Sort.PriceAsc:
                    {
                        collection = collection.OrderBy(SortByUsd<T>()).ThenBy(o => o.IsAvailable).ThenByDescending(o => o.Rate);
                        break;
                    }
                case Sort.PriceDesc:
                    {
                        collection = collection.OrderByDescending(SortByUsd<T>()).ThenBy(o => o.IsAvailable).ThenByDescending(o => o.Rate);
                        break;
                    }
                case Sort.RateDesc:
                    {
                        collection = collection.OrderBy(o => o.IsAvailable).OrderByDescending(o => o.Rate);
                        break;
                    }
            }

            return collection.ToPagedList(page, pageSize);
        }

        private static Expression<Func<T, double>> SortByUsd<T>() where T : Product
        {
            return p => p.Producer.BuyCurrency == Currency.Usd ? p.Price / p.Producer.UsdRate  : p.Price;
        }
    }
}