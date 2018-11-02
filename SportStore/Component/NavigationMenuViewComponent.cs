using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Component
{
    public class NavigationMenuViewComponent :ViewComponent
    {
        IProductRepository productRepository;
        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            var categories = productRepository.Products
               
                .Select(p => p.Category) 
                .Distinct()
                .OrderBy(p => p);

            return View(categories);
        }
    }
}
