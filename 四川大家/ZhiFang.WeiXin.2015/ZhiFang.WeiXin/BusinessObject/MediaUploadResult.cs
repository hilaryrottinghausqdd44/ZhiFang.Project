using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class MediaUploadResult:ErrResult
    {
        public string type { get; set; }
        public string media_id { get; set; }
        public long created_at { get; set; }
    }
}