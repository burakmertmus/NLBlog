using NLBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByIsDeleted();
    }
}
