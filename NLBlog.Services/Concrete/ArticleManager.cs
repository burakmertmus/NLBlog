using AutoMapper;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using NLBlog.Services.Abstract;
using NLBLog.Shared.Utilities.Results.Abstract;
using NLBLog.Shared.Utilities.Results.ComplexTypes;
using NLBLog.Shared.Utilities.Results.Concrete;
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
            await _unitOfWork.Articles.AddAsync(article).ContinueWith(t=>_unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success,message:$"{article.Title} başlıklı makale başarıyla eklendi");
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
                await _unitOfWork.Articles.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, message: $"{article.Title} başlıklı makale başarıyla silinmiştir");
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

            return new DataResult<ArticleDto>(ResultStatus.Error,message:"Böyle bir makale bulunamadı.",data:null);
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
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: "Makaleler bulunamadı.", data: null);
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
                return new DataResult<ArticleListDto>(ResultStatus.Error, message: "Makaleler bulunamadı.", data: null);
            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, message: "Bötle bir kategori bulunamadı.", data: null);



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
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: "Makaleler bulunamadı.", data: null);
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
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: "Makaleler bulunamadı.", data: null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
                
                await _unitOfWork.Articles.DeleteAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, message: $"{article.Title} başlıklı makale başarıyla veritabanından silinmiştir");
            }
            return new Result(ResultStatus.Error, message: "Makale bulunamadı");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article).ContinueWith(t=>_unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success, message: $"{articleUpdateDto.Tiltle} başlıklı makale başarıyla güncellenmiştir");
        }
    }
}
