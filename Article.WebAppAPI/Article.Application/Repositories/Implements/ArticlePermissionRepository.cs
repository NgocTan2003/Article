using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Common.ReponseBase;
using Article.Data.EF;
using Article.Data.Entity;
using Article.Dtos.ArticlePermissionDto;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.Implements
{
    public class ArticlePermissionRepository : RepositoryBase<ArticlePermission>, IArticlePermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticlePermissionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ArticlePermission>> GetPermissionByRole(string roleName)
        {
            var listPermission = await _context.ArticlePermissions.Where(x => x.RoleName == roleName).ToListAsync();
            return listPermission;
        }
    }
}
