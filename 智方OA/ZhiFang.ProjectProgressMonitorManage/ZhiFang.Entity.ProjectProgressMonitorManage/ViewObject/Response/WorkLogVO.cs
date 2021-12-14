using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response
{
    public class WorkLogVO:ZhiFang.Entity.RBAC.ViewObject.Response.EmpInfo
    {      
        public string Id { get; set; }
        public string LabID { get; set; }
        public virtual string NextDayContent { get; set; }
        public virtual string ToDayContent { get; set; }
        //public virtual bool IsUse { get; set; }
                
        public virtual bool Status { get; set; }

        public virtual string DataUpdateTime { get; set; }

        public virtual string DateCode { get; set; }

        public virtual string Image1 { get; set; }

        public virtual string Image2 { get; set; }

        public virtual string Image3 { get; set; }

        public virtual string Image4 { get; set; }

        public virtual string Image5 { get; set; }

        public virtual string DataAddTime { get; set; }

        public virtual WorkLogType WorkLogType { get; set; }

        public virtual WorkLogExportLevel WorkLogExportLevel { get; set; }

        public virtual long? PTaskID { get; set; }

        public virtual IList<PWorkDayLog> PWorkDayLogList { get; set; }

        public virtual IList<PProjectAttachment> PProjectAttachmentList { get; set; }

        public virtual IList<long> CopyForEmpIdList { get; set; }

        public virtual IList<string> CopyForEmpNameList { get; set; }

        public virtual int InteractionCount { get; set; }
        public virtual long LikeCount { get; set; }

        public string WeekInfo { get; set; }
        public bool IsWorkDay { get; set; }
    }
}
