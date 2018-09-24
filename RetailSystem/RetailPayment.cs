using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem
{
    public class RetailPayment
    {

        private int _pairOfPluA;
        private int _numberOfPluB;
        private double _amountOfPluC;

        private const double PricePluA = 59.90;
        private const int PricePluB = 399;
        private const int PriceThreePackPluB = 999;
        public RetailPayment()
        {
            _pairOfPluA = 0;
            _numberOfPluB = 0;
            _amountOfPluC = 0;
        }

        public void Add(string plu, int i)
        {
            switch (plu)
            {
                case "PLU A":
                    _pairOfPluA += i;
                    break;
                case "PLU B":
                    _numberOfPluB += i;
                    break;
                case "PLU C":
                    _amountOfPluC += i;
                    break;
                default:
                    //Error: Unknown product
                    break;
            }
        }

        public int CalculateCost()
        {
            var numberPluAPayingFor = _pairOfPluA - _pairOfPluA / 3;
            var pricePluA = numberPluAPayingFor * PricePluA;
            var pricePluB = CalculatePluBCost(_numberOfPluB, PricePluB, PriceThreePackPluB);

            var price = pricePluA + pricePluB;

            return (RoundOff(price));
        }

        private static int CalculatePluBCost(int numberOfArticles, int pricePrArticle, int bundlePrice)
        {
            const int amountIsBundle = 3;
            var numberOfBundles = numberOfArticles / amountIsBundle;
            var rest = numberOfArticles % amountIsBundle;
            return numberOfBundles * bundlePrice + rest * pricePrArticle;
        }


        private int RoundOff(double price)
        {
            return (int) Math.Round(price, MidpointRounding.AwayFromZero);
        }
    }

}
