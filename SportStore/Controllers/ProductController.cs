using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository;
        private const int PageSize = 4;
        public ProductController(IProductRepository productRepo)
        {
            productRepository = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(string category, int productPage = 1)
        {
            var productsInCategory = productRepository.Products
                .Where(p => category == null || p.Category == category);

            var productOnPage = productsInCategory
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);

            var pageInfo = new PageInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = productsInCategory.Count()
            };

            var productList = new ProductsListViewModel
            {
                Products = productOnPage,
                PageInfo = pageInfo,
                CurrentCategory = category
            };

            return View(productList);
        }
    }
}