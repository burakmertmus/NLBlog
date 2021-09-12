using NLBLog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBLog.Shared.Entities.Abstract
{
    public class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
    }
}
