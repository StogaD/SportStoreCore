using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;

namespace SportStore.EF
{
    public class EFProductContext : DbContext
    {
        public EFProductContext(DbContextOptions<EFProductContext> options) : base(options)
        { }
            public DbSet<Product> Products { get; set; }
    }
    
    
}
