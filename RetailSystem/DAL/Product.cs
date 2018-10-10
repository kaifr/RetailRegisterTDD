using System;

namespace RetailSystem.DAL
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Discount Discount { get; set; }
        public int Bundle { get; set; }
        public Tuple<int,int> GetXPayForY { get; set; }
        public double BundlePrice { get; set; }
    }


    public enum Unit
    {
        Pieces = 1,
        Amount = 2
    };

    public enum Discount
    {
        GetXPayForY = 1,
        AmountIsABundle = 2,
        None = 3
    };
}
