using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class UseStatusCodePagesWithReExecuteModel
    {
        public int StatusCode { get; set; }
        public string OriginalPath { get;  set; }
        public string OriginalPathBase { get;  set; }
        public string OriginalQueryString { get;  set; }
    }
}
