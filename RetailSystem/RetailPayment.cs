using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using RetailSystem.DAL;

namespace RetailSystem
{
    public class RetailPayment
    {
        private readonly IEnumerable<Product> _allProducts;
        private readonly List<Item> _accumulatedProducts;

        ////My in memory storage
        //private int _pairOfPluA;
        //private int _numberOfPluB;
        //private double _amountOfPluCInGrams;

        ////My config "files" 
        //private const double PricePluA = 59.90;
        //private const int GetPayFor = 3;
        //private const int PricePluB = 399;
        //private const int PriceThreePackPluB = 999;
        //private const double PricePluCprGram = 19.54 / 1000;


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
            //var price = CalculatePrice(_accumulatedProducts);

            //var pricePluA = CalculatePluACost(_pairOfPluA, PricePluA, GetPayFor);
            //var pricePluB = CalculatePluBCost(_numberOfPluB, PricePluB, PriceThreePackPluB);
            //var pricePubC = CalculatePluCCost(_amountOfPluCInGrams, PricePluCprGram);
            //var price2 = pricePluA + pricePluB + pricePubC;
            //return (RoundOff(price));
            return RoundOff(cost);
        }

        private double CalculateCostOfBundle(Item item)
        {
            throw new NotImplementedException();
        }

        private double CalculateCostOfGetXPayForY(Item item)
        {
            throw new NotImplementedException();
        }

        private double CalculateCostOfItem(Item item)
        {
            return 0;
        }

        private double CalculatePluACost(int numberPluA, double pricePluA, int getXPayFor)
        {
            var numberPluAPayingFor = numberPluA - numberPluA / getXPayFor;
            return numberPluAPayingFor * pricePluA;
        }

        private static int CalculatePluBCost(int numberOfArticles, int pricePrArticle, int bundlePrice)
        {
            const int amountIsBundle = 3;
            var numberOfBundles = numberOfArticles / amountIsBundle;
            var rest = numberOfArticles % amountIsBundle;
            return numberOfBundles * bundlePrice + rest * pricePrArticle;
        }

        private double CalculatePluCCost(double amountOfPluCInGrams, double pricePluCprGram)
        {
            var costKr = amountOfPluCInGrams * pricePluCprGram;
            //Smallest amount is one oere.
            //This doesn't make sense since there is only one product that can have this small amount
            //and anything below 0.50 oere will be 0 kr
            if (costKr < 0.01)
            {
                costKr = 0;
            }
            return Math.Round(costKr, 2);
        }

        private int RoundOff(double price)
        {
            return (int)Math.Round(price, MidpointRounding.AwayFromZero);
        }
    }

}
