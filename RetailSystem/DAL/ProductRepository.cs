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
            return _productContext.Products;
        }
    }
}
