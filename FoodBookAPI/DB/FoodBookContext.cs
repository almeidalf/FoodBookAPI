using FoodBookAPI.V1.Models;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));


            builder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            //Fazendo que o array vire somente um
            builder.Entity<Receita>()
            .Property(e => e.Ingredientes)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            builder.Entity<Receita>()
            .Property(e => e.ModoPreparo)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
