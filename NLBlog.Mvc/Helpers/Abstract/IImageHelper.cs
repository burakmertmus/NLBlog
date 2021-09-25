using Microsoft.AspNetCore.Http;
using NLBlog.Entities.Dtos;
using NLBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile,string folderName="userImages");
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
