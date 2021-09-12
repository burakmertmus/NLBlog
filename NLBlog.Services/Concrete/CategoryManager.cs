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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            //await _unitOfWork.Categories.AddAsync(
            //    new Category 
            //{
            //Name=categoryAddDto.Name,
            //Description=categoryAddDto.Description,
            //Note=categoryAddDto.Note,
            //IsActive=categoryAddDto.IsActive,
            //CreatedByName=createdByName,
            //CreatedDate=DateTime.Now,
            //ModifiedByName=createdByName,
            //ModifiedDate=DateTime.Now,
            //IsDeleted=false
            //}).ContinueWith(t=>_unitOfWork.SaveAsync());

            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            await _unitOfWork.Categories.AddAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());


            //await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,message:$"{categoryAddDto.Name} adlı kategori başarı ile eklenmiştir.");
        }

        public async Task<IResult> Delete(int categoryId,string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category!=null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync()) ;
                return new Result(ResultStatus.Success,message: $"{category.Name} adlı kategori başarı ile silinmiştir.");
            }
            return new DataResult<Category>(ResultStatus.Error, message: "Kategori bulunamadı.", data: null);
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category= await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c=>c.Articles);
            if(category!=null)
            {
                
                return new DataResult<CategoryDto>(ResultStatus.Success,new CategoryDto {
                    Category = category,
                    ResultStatus=ResultStatus.Success
                    
            });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, message: "Kategori bulunamadı.",data:null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(predicate: null, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto {
                    Categories=categories,
                    ResultStatus=ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error,message:"Hiç Kategori bulunamadı",data:null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c=>!c.IsDeleted, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto {
                    Categories=categories,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, message: "Hiç Kategori bulunamadı", data: null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndIsActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted&&c.IsActive, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, message: "Hiç Kategori bulunamadı", data: null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t=>_unitOfWork.SaveAsync()) ;
                
                return new Result(ResultStatus.Success, message: $"{category.Name} adlı kategori başarı ile veritabanınan silinmiştir.");
            }
            return new DataResult<Category>(ResultStatus.Error, message: "Kategori bulunamadı.", data: null);
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, message: $"{categoryUpdateDto.Name} adlı kategori başarı ile güncellenmiştir.");
        }


    }
}
