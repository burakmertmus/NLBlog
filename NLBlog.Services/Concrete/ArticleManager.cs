using AutoMapper;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using NLBlog.Services.Abstract;
using NLBlog.Services.Utilities;
using NLBlog.Shared.Utilities.Results.Abstract;
using NLBlog.Shared.Utilities.Results.ComplexTypes;
using NLBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {

            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, Messages.Article.Add(article.Title));
        }

        public async Task<IDataResult<int>> Count()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync();
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, data: -1, message: "Beklenmedik bir hata ile karşılaşıldı.");
            }
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                    

                return new Result(ResultStatus.Success, Messages.Article.Delete(article.Title));
            }
            return new Result(ResultStatus.Error, message: "Makale bulunamadı");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a=>a.Id==articleId,a => a.User, a=>a.Category);
            if (article!=null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success,data:new ArticleDto 
                {
                Article=article,
                ResultStatus=ResultStatus.Success
                });

            }

            return new DataResult<ArticleDto>(ResultStatus.Error,message: Messages.Article.NotFound(false),data:null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(predicate:null, a => a.User, a => a.Category);
            if (articles.Count >-1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, data: new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(true), data: null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id== categoryId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.Id == categoryId && !a.IsDeleted && a.IsActive, a => a.User, ar => ar.Category);

                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, data: new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });

                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(true), data: null);
            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(false), data: null);



        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, data: new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(true), data: null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted&&a.IsActive, a => a.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, data: new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(true), data: null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);

                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, message: Messages.Article.HardDelete(article.Title));
            }
            return new Result(ResultStatus.Error, message: Messages.Article.NotFound(false));
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, message: Messages.Article.Update(articleUpdateDto.Tiltle));
        }
    }
}
