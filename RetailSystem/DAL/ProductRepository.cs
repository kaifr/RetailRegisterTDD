using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.DAL
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductContext _productContext;

        public ProductRepository(ProductContext context)
        {
            _productContext = context;
        }


        public List<Product> GetAllProducts()
        {
            return _productContext.ToList();
        }


    }

    internal class ProductContext
    {
        public Product Products { get; set; }

        public IEnumerable<Product> ToList()
        {

            var productList = new List<Product>
            {
                new Product
                {
                    Name = "PluA",
                    Price = 10,
                    Unit = Unit.Pieces,
                    Discount = Discount.GetXPayForY,
                    GetXPayForY = new Tuple<int, int>(3,2)
                },
                new Product
                {
                    Name = "PluB",
                    Price = 17,
                    Unit = Unit.Pieces,
                    Discount = Discount.AmountIsABundle,
                    Bundle = 3
                },
                new Product
                {
                    Name = "PluC",
                    Price = 188,
                    Unit = Unit.Amount,
                    Discount = Discount.None
                }
            };
            return productList;
        }
    }
}
