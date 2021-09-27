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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {

            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();



            //await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, message: Messages.Category.Add(categoryAddDto.Name) ,
                data: new CategoryDto
                {
                    Category = addedCategory,
                    ResultStatus=ResultStatus.Success,
                    Message= Messages.Category.Add(categoryAddDto.Name)
                });
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural: false), data:null);
            }

        }
        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c=>c.Id==categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto,oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, message: Messages.Category.Update(updatedCategory.Name),
                data: new CategoryDto
                {
                    Category = updatedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.Update(categoryUpdateDto.Name)
                });
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId,string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category!=null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();

                return new DataResult<CategoryDto>(ResultStatus.Success, message: Messages.Category.Delete(deletedCategory.Name),
                    data: new CategoryDto
                    {
                        Category = deletedCategory,
                        ResultStatus = ResultStatus.Success,
                        Message = Messages.Category.Delete(deletedCategory.Name)
                    });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural: false),
                    data: new CategoryDto
                    {
                        Category = null,
                        ResultStatus = ResultStatus.Error,
                        Message = Messages.Category.NotFound(isPlural:false)
                    });
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category= await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c=>c.Articles);
            if(category!=null)
            {
                
                return new DataResult<CategoryDto>(ResultStatus.Success,new CategoryDto {
                    Category = category,
                    ResultStatus=ResultStatus.Success
                    
            });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural:false),data:
                new CategoryDto {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message= Messages.Category.NotFound(isPlural: false)
                });
        }


        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(predicate: null, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto {
                    Categories=categories,
                    ResultStatus=ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error,message: Messages.Category.NotFound(isPlural: true), data:new CategoryListDto { 
            Categories=null,
                ResultStatus = ResultStatus.Error,
                Message= Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c=>!c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural: true), data: new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndIsActiveAsync()
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
            return new DataResult<CategoryListDto>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural:true), data: null);
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();


                return new Result(ResultStatus.Success, message: Messages.Category.HardDelete(category.Name));
            }
            return new DataResult<Category>(ResultStatus.Error, message: Messages.Category.NotFound(isPlural: false), data: null);
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();
            if (categoriesCount>-1)
            {
                return new DataResult<int>(ResultStatus.Success,categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error,data:-1,message: "Beklenmedik bir hata ile karşılaşıldı.");
            }
            
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, data: -1, message: "Beklenmedik bir hata ile karşılaşıldı.");
            }
        }
    }
}
