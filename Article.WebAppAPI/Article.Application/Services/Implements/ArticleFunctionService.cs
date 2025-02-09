using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Data.Entity;
using Article.Dtos.ArticleFunctionDto;
using Article.Dtos.SubCategories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Implements
{
    public class ArticleFunctionService : IArticleFunctionService
    {
        private readonly IArticleFunctionRepository _articleFunctionRepository;
        private readonly IMapper _mapper;

        public ArticleFunctionService(IArticleFunctionRepository articleFunctionRepository, IMapper mapper)
        {
            _articleFunctionRepository = articleFunctionRepository;
            _mapper = mapper;
        }

        public async Task<List<ArticleFunctionDto>> GetAll()
        {
            var functions = _articleFunctionRepository.GetAllAsNoTracking();
            var functionsDto = _mapper.Map<List<ArticleFunction>, List<ArticleFunctionDto>>(functions.ToList());
            return functionsDto;
        }
    }
}
