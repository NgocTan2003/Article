using Article.Common.Seedwork;
using Article.Dtos.ArticleFunctionDto;
using Article.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Interfaces
{
    public interface IArticleFunctionService
    {
        Task<List<ArticleFunctionDto>> GetAll();
    }
}
