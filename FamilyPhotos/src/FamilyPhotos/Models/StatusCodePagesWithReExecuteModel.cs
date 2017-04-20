using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class StatusCodePagesWithReExecuteModel
    {
        public string OriginalPath { get; set; }
        public string OriginalPathBase { get; set; }
        public string OriginalQueryString { get; set; }
        public int StatusCode { get; set; }
    }
}
