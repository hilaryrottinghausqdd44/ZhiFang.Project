using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPWorkLogInteraction : BaseBLL<PWorkLogInteraction>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkLogInteraction
    {
        public IDAO.ProjectProgressMonitorManage.IDPWorkDayLogDao IDPWorkDayLogDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPWorkWeekLogDao IDPWorkWeekLogDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPWorkMonthLogDao IDPWorkMonthLogDao { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public bool Add(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            if (DBDao.Save(this.Entity))
            {
                long tmpempid = 0;
                PWorkLogBase pwdl = null;
                string logtypename = "";
                if (Entity.WorkDayLogID.HasValue)
                {
                    pwdl = IDPWorkDayLogDao.Get(Entity.WorkDayLogID.Value);
                    logtypename = "日报";
                }
                if (Entity.WorkWeekLogID.HasValue)
                {
                    pwdl = IDPWorkWeekLogDao.Get(Entity.WorkWeekLogID.Value);
                    logtypename = "周报";
                }
                if (Entity.WorkMonthLogID.HasValue)
                {
                    pwdl = IDPWorkMonthLogDao.Get(Entity.WorkMonthLogID.Value);
                    logtypename = "月报";
                }
                if (pwdl != null)
                {
                    if (pwdl.EmpID != Entity.SenderID)
                    {
                        Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                        string urgencycolor = "#11cd6e";
                        data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你" + pwdl.DateCode + "的" + logtypename + "有了新评论" });
                        string Contents = (Entity.Contents.Length > 15) ? Entity.Contents.Substring(0, 15) + "..." : Entity.Contents;
                        data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = Contents });
                        data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统" });
                        data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = Entity.SenderName });
                        string tmpdatetime = (Entity.DataAddTime.HasValue) ? Entity.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                        data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                        data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                        IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { pwdl.EmpID.Value }, data, "", "");
                    }
                }
                
                return true;
            }
            return false;
        }
    }
}
