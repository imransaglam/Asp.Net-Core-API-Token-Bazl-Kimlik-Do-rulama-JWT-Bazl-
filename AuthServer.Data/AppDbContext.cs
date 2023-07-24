using AuthServer.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data
{
    //Üyelik sistemi ile igili tabloları tutacağından IdentityDbContext miras al.
    //Kullanıcı ile ilgili tüm dbsetler ıdentitydbcontextten geliyor
    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        //Üyelik sistemi ile ilgili olmayan
      public DbSet<Product> Products { get; set; }
      public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        //metodu şişirmemesi için herbir entity için configurasyon oluşturulucak.örn:product name 20 karakter olsun gibi
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }

    }
}
