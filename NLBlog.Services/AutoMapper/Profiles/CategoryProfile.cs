﻿using AutoMapper;
using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Article>().ForMember(dest => dest.CreatedDate, opt =>
             opt.MapFrom(
                 x => DateTime.Now
                 ));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest => dest.ModifiedDate, opt =>
                opt.MapFrom(
                    x => DateTime.Now
                    )); ;
        }
    }
    
}