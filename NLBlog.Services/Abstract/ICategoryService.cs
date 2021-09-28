using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using NLBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geri döner
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID değeri</param>
        /// <returns> Asekron bir operasyon ile Task olarak işlem sonucu Data Result tipinde geri döner.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndIsActiveAsync();


        /// <summary>
        /// Verilen CategoryAddDto ve CreateByName parametresine ait  bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">CategoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// /// <param name="createdByName">string tipinde kullanıcının kullanıcı adı</param>
        /// <returns> Asenkron bir operasyon ile Task Olarak bizlere ekleme işleminin sonucu DataResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto,string createdByName);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int categoryId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
    