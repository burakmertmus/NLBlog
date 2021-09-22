using AutoMapper;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using NLBlog.Services.Abstract;
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

        
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {

            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();



            //await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, message: $"{categoryAddDto.Name} adlı kategori başarı ile eklenmiştir.",
                data: new CategoryDto
                {
                    Category = addedCategory,
                    ResultStatus=ResultStatus.Success,
                    Message=$"{categoryAddDto.Name} adlı kategori başarı ile eklenmiştir."
                });
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, message: "Böyle bir kategori bulunamadı",data:null);
            }

        }
        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c=>c.Id==categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto,oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, message: $"{categoryUpdateDto.Name} adlı kategori başarı ile güncellenmiştir.",
                data: new CategoryDto
                {
                    Category = updatedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{categoryUpdateDto.Name} adlı kategori başarı ile güncellenmiştir."
                });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId,string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category!=null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();

                return new DataResult<CategoryDto>(ResultStatus.Success, message: $"{deletedCategory.Name} adlı kategori başarı ile silinmiştir.",
                    data: new CategoryDto
                    {
                        Category = deletedCategory,
                        ResultStatus = ResultStatus.Success,
                        Message = $"{deletedCategory.Name} adlı kategori başarı ile silinmiştir."
                    });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, message:" Böyle bir kategori bulunamadı.",
                    data: new CategoryDto
                    {
                        Category = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Böyle bir kategori bulunamadı."
                    });
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
            return new DataResult<CategoryDto>(ResultStatus.Error, message: "Kategori bulunamadı.",data:
                new CategoryDto {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message= "Kategori bulunamadı."
            });
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

            return new DataResult<CategoryListDto>(ResultStatus.Error,message:"Hiç Kategori bulunamadı",data:new CategoryListDto { 
            Categories=null,
                ResultStatus = ResultStatus.Error,
                Message="Hiç bir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
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

            return new DataResult<CategoryListDto>(ResultStatus.Error, message: "Hiç Kategori bulunamadı", data: new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir kategori bulunamadı."
            });
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
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();


                return new Result(ResultStatus.Success, message: $"{category.Name} adlı kategori başarı ile veritabanınan silinmiştir.");
            }
            return new DataResult<Category>(ResultStatus.Error, message: "Kategori bulunamadı.", data: null);
        }

        
    }
}
