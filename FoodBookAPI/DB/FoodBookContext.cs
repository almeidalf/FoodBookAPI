using FoodBookAPI.V1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.DB
{
    public class FoodBookContext : IdentityDbContext<ApplicationUser>
    {
        public FoodBookContext(DbContextOptions<FoodBookContext> options) : base(options)
        {

        }

        public DbSet<Receita> Receita { get; set; }
    }
}
