using System;
using System.Collections.Generic;

namespace RetailSystem.DAL
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductContext _productContext;

        public ProductRepository(ProductContext context)
        {
            _productContext = context;
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _productContext.GetProducts();
        }
    }

    public class ProductContext
    {

        public List<Product> GetProducts()
        {

            var productList = new List<Product>
            {
                new Product
                {
                    Name = "PLU A",
                    Price = 59.90,
                    Unit = Unit.Pieces,
                    Discount = Discount.GetXPayForY,
                    GetXPayForY = new Tuple<int, int>(3,2)
                },
                new Product
                {
                    Name = "PLU B",
                    Price = 399,
                    Unit = Unit.Pieces,
                    Discount = Discount.AmountIsABundle,
                    Bundle = 3,
                    BundlePrice = 999
                },
                new Product
                {
                    Name = "PLU C",
                    Price = 19.54/1000,
                    Unit = Unit.Amount,
                    Discount = Discount.None
                }
            };
            return productList;
        }
    }
}
