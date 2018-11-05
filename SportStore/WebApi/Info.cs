using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.WebApi
{
    //[ApiController]
    [Route("api/[controller]")]
    public class Info :Controller
    {
        IProductRepository productRepository;
        public Info(IProductRepository repo)
        {
            productRepository = repo;
        }


        [HttpPost]
        [ProducesResponseType(200,Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]Product prod)
        {
            if (prod != null && prod.Name != null)
                return Ok(prod.Description);
            else
                return BadRequest();
        }


        [HttpGet]
        [Route("{prodId}")]
        //[ProducesResponseType(200,Type = typeof(string))]
        public IActionResult Get( int prodId)
        {
            var prod = productRepository.Products.Where(p => p.ProductID == prodId).SingleOrDefault();

            return Ok(prod);
            //return new NotFoundResult();
           

        }
    }
}
