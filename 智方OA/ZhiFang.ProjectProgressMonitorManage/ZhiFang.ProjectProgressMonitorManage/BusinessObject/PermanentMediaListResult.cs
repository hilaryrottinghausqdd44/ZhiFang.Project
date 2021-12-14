using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ProjectProgressMonitorManage.BusinessObject
{    
    public class PermanentMediaList : ErrResult
    {
        public int total_count { get; set; }
        public int item_count { get; set; }
        public PermanentMedia[] item { get; set; }

    }
    public class PermanentMedia
    {
        public string media_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public PermanentMediaNewsContentItem content { get; set; }
        public string update_time { get; set; }

    }
    public class PermanentMediaNewsContentItem
    {
        public PermanentMediaNewsContent[] news_item { get; set; }
    }
    public class PermanentMediaNewsContent
    {
        public string title { get; set; }
        public string thumb_media_id { get; set; }
        public string thumb_media_Url { get; set; }
        public string show_cover_pic { get; set; }
        public string author { get; set; }
        public string digest { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string content_source_url { get; set; }
    }
    
}
