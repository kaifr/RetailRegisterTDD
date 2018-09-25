using System;

namespace RetailSystem
{
    public class RetailPayment
    {
        //My in memory storage
        private int _pairOfPluA;
        private int _numberOfPluB;
        private double _amountOfPluCInGrams;

        //My config "files" 
        private const double PricePluA = 59.90;
        private const int GetPayFor = 3;
        private const int PricePluB = 399;
        private const int PriceThreePackPluB = 999;
        private const double PricePluCprGram = 19.54 / 1000;


        public RetailPayment()
        {
            _pairOfPluA = 0;
            _numberOfPluB = 0;
            _amountOfPluCInGrams = 0;
        }

        public void Add(string plu, double amount)
        {
            if (amount < 0)
            {
                //Error. You can't remove items
                return;
            }
            var units = (int)amount;

            //Refactor when adding more items
            switch (plu)
            {
                case "PLU A":
                    _pairOfPluA += units;
                    break;
                case "PLU B":
                    _numberOfPluB += units;
                    break;
                case "PLU C":
                    _amountOfPluCInGrams += amount;
                    break;
                default:
                    //Error: Unknown product
                    break;
            }
        }

        public int CalculateCost()
        {
            var pricePluA = CalculatePluACost(_pairOfPluA, PricePluA, GetPayFor);
            var pricePluB = CalculatePluBCost(_numberOfPluB, PricePluB, PriceThreePackPluB);
            var pricePubC = CalculatePluCCost(_amountOfPluCInGrams, PricePluCprGram);
            var price = pricePluA + pricePluB + pricePubC;
            return (RoundOff(price));
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
