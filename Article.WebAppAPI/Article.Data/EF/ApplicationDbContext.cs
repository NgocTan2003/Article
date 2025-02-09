using Article.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Article.Data.EF
{
    public class ApplicationDbContext : IdentityDbContext<ArticleAppUser, ArticleAppRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // DeleteBehavior.Restrict: muốn giữ dl cha và con đồng bộ và không muốn tự động xóa dữ liệu con khi xóa dữ liệu cha.
            builder.Entity<ArticleData>()
               .HasOne(ad => ad.ArticleSubCategory)
               .WithMany(asc => asc.ArticleDatas)
               .HasForeignKey(ad => ad.ArticleSubCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArticleSubCategory>()
                .HasOne(asc => asc.ArticleCategory)
                .WithMany(ac => ac.ArticleSubCategories)
                .HasForeignKey(asc => asc.ArticleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }

        public DbSet<ArticleAppUser> ArticleAppUsers { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleSubCategory> ArticleSubCategories { get; set; }
        public DbSet<ArticleAppRole> ArticleAppRoles { get; set; }
        public DbSet<ArticleData> ArticleDatas { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<ArticlePermission> ArticlePermissions { get; set; }
        public DbSet<ArticleFunction> ArticleFunctions { get; set; }

    }
}
