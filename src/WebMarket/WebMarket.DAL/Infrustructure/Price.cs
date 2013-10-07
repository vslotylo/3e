using System;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Entities.Enums;

namespace WebMarket.DAL.Infrustructure
{
    public class Price
    {
        private readonly Product product;

        public Price(Product product)
        {
            this.product = product;
        }

        public double PriceUah
        {
            get
            {
                double uahPrice = product.Price;
                if (product.Producer.BuyCurrency == (int)Currency.Usd)
                {
                    uahPrice = Math.Round(uahPrice * product.Producer.UsdRate);
                }

                return uahPrice;
            }
        }

        public double PriceUsd
        {
            get
            {
                double usdPrice = product.Price;
                if (product.Producer.BuyCurrency == Currency.Uah)
                {
                    usdPrice = Math.Round(usdPrice / product.Producer.UsdRate);
                }

                return usdPrice;
            }
        }

        public double PriceFinalUah
        {
            get
            {
                double priceFinalUah = PriceUah;
                if (product.Discount > 0 && product.Discount < 100)
                {
                    priceFinalUah = Math.Round((100 - product.Discount) * priceFinalUah / 100);
                }

                return priceFinalUah;
            }
        }

        public double PriceFinalUsd
        {
            get
            {
                double priceFinalUsd = PriceUsd;
                if (product.Discount > 0 && product.Discount < 100)
                {
                    priceFinalUsd = Math.Round((100 - product.Discount) * priceFinalUsd / 100, 1);
                }

                return priceFinalUsd;
            }
        }
    }
}
