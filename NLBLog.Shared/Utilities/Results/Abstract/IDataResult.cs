using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T>:IResult
    {
        public T Data { get;} //new DataResult<Category>(ResultSatatus.Success,category)
                              //new DataResult<IList<Category>>(ResultSatatus.Success,category
    }
}
