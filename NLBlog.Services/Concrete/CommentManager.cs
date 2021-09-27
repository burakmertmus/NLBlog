using NLBlog.Data.Abstract;
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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> Count()
        {
    
            var commentsCount = await _unitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, data: -1, message: "Beklenmedik bir hata ile karşılaşıldı.");
            }
        }

        public async Task<IDataResult<int>> CountByIsDeleted()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(c=>!c.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, data: -1, message: "Beklenmedik bir hata ile karşılaşıldı.");
            }
        }
    }
}
