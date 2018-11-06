using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SportStore.EF;

namespace SportStore.Models
{
    public class ProductRepository : IProductRepository
    {
       private EFProductContext productRepo;
        public ProductRepository(EFProductContext productRepo, ILogger<ProductRepository> logger)
        {
            logger.Log(LogLevel.Critical, "msg2");


            this.productRepo = productRepo;


        }

        public IQueryable<Product> Products =>
            productRepo.Products;

    }

    public class ProductmokcRepository : IProductRepository
    {
        public IQueryable<Product> Products =>
            new List<Product> {
                new Product {
                    Category = "Military",
                    Description = "mockDescr",
                    Name ="FromAutoFac",
                    ProductID = 999,
                    Price  = 120
                },
                new Product {
                    Category = "Transport",
                    Description = "FakeDescr",
                    Name ="FromAutoFac2",
                    ProductID = 888,
                    Price  = 145
                }
            }.AsQueryable();
    }
}
