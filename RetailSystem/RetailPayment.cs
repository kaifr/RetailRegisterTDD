using System;
using System.Collections.Generic;
using System.Linq;
using RetailSystem.DAL;

namespace RetailSystem
{
    public class RetailPayment
    {
        private readonly IEnumerable<Product> _allProducts;
        private readonly List<Item> _accumulatedProducts;

        public RetailPayment(ProductRepository repository)
        {
            _allProducts = repository.GetAllProducts();
            _accumulatedProducts = new List<Item>();

            foreach (var product in _allProducts)
            {
                _accumulatedProducts.Add(new Item
                {
                    ProductType = product,
                    Amount = 0
                });
            }
        }

        public List<Item> GetItems()
        {
            return _accumulatedProducts;
        }

        public void Add(string plu, double amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("You can't remove items"); 
                return;
            }

            if (_allProducts.Any(p => p.Name.Equals(plu)))
            {
                _accumulatedProducts.Find(p => p.ProductType.Name.Equals(plu)).Amount += amount;
            }
            else
            {
                Console.WriteLine("Product does not exist");
            }

        }

        public int CalculateCost(List<Item> accumulatedProducts)
        {
            double cost = 0;

            foreach (var item in accumulatedProducts)
            {
                if ((int)item.Amount == 0) continue;
                switch (item.ProductType.Discount)
                {
                    case Discount.GetXPayForY:
                    {
                        cost += CalculateCostOfGetXPayForY(item);
                        break;
                    }
                    case Discount.AmountIsABundle:
                    {
                        cost += CalculateCostOfBundle(item);
                            break;
                    }
                    case Discount.None:
                    {
                        cost += CalculateCostOfItem(item);
                            break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return RoundOff(cost);
        }

        private static double CalculateCostOfBundle(Item item)
        {
            var bundleSize = item.ProductType.Bundle;
            var pricePrItem = item.ProductType.Price;
            var bundlePrice = item.ProductType.BundlePrice;
            var rest = item.Amount % bundleSize;
            var numberOfBundles = (item.Amount - rest) / bundleSize;
            return numberOfBundles * bundlePrice + rest * pricePrItem;
        }

        private static double CalculateCostOfGetXPayForY(Item item)
        {
            var price = item.ProductType.Price;
            var getY = item.ProductType.GetXPayForY.Item1;
            var payX = item.ProductType.GetXPayForY.Item2;
            var rest = item.Amount % getY;
            var payFor = ((item.Amount-rest) / getY) * payX;
            return (payFor + rest) * price;
        }

        private static double CalculateCostOfItem(Item item)
        {
            var amount = item.Amount;
            var price = item.ProductType.Price;
            var costKr = amount * price;
            //Smallest amount is one oere.
            if (costKr < 0.01)
            {
                costKr = 0;
            }
            return Math.Round(costKr, 2);
        }

        private static int RoundOff(double price)
        {
            return (int)Math.Round(price, MidpointRounding.AwayFromZero);
        }
    }

}
