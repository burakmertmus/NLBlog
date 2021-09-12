﻿using NLBlog.Entities.Concrete;
using NLBLog.Shared.Entities.Abstract;
using NLBLog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Entities.Dtos
{
    public class ArticleDto:DtoGetBase
    {
        public Article Article { get; set; }
        
    }
}
