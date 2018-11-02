using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.EF;

namespace SportStore.Models
{
    public class ProductRepository : IProductRepository
    {
       private EFProductContext productRepo;
        public ProductRepository(EFProductContext productRepo)
        {
            this.productRepo = productRepo;
        }

        public IQueryable<Product> Products =>
            productRepo.Products;



    }
}
