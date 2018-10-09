using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.DAL
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Unit Unit { get; set; }
        public Discount Discount { get; set; }
        public int Bundle { get; set; }
        public Tuple<int,int> GetXPayForY { get; set; }
        
    }


    internal enum Unit
    {
        Pieces = 1,
        Amount = 2
    };

    internal enum Discount
    {
        GetXPayForY = 1,
        AmountIsABundle = 2,
        None = 3
    };
}
