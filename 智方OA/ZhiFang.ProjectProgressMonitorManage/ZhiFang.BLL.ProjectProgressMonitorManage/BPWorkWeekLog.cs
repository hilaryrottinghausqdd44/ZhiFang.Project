using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPWorkWeekLog : BaseBLL<PWorkWeekLog>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkWeekLog
    {
        IDPWorkLogCopyForDao IDPWorkLogCopyForDao { get; set; }
        IDPProjectAttachmentDao IDPProjectAttachmentDao { get; set; }
        public bool AddPWorkWeekLogByWeiXin(List<string> attachmentUrlList)
        {
            if (this.Add())
            {
                if (this.Entity.CopyForEmpIdList.Count > 0 && this.Entity.CopyForEmpNameList.Count > 0 && this.Entity.CopyForEmpIdList.Count == this.Entity.CopyForEmpNameList.Count)
                {
                    for (int i = 0; i < this.Entity.CopyForEmpIdList.Count; i++)
                    {
                        PWorkLogCopyFor pwlcf = new PWorkLogCopyFor();
                        pwlcf.LogID = this.Entity.Id;
                        pwlcf.PublishEmpID = this.Entity.EmpID;
                        pwlcf.PublishEmpName = this.Entity.EmpName;
                        pwlcf.ReceiveEmpID = this.Entity.CopyForEmpIdList[i];
                        pwlcf.ReceiveEmpName = this.Entity.CopyForEmpNameList[i];
                        pwlcf.LogType = WorkLogType.WorkLogWeek.ToString(); //WorkLogType.WorkLogDay.ToString();
                        IDPWorkLogCopyForDao.Save(pwlcf);
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool UpdatePWorkWeekLogByField(PWorkWeekLog entity, string[] tempArray)
        {
            bool result = true;
            if (this.Update(tempArray))
            {
                if (entity.CopyForEmpIdList.Count > 0 && entity.CopyForEmpNameList.Count > 0 && entity.CopyForEmpIdList.Count == entity.CopyForEmpNameList.Count)
                {
                    //先删除原来的抄送人信息
                    result = IDPWorkLogCopyForDao.DeleteByLogId(entity.Id);
                    for (int i = 0; i < entity.CopyForEmpIdList.Count; i++)
                    {
                        PWorkLogCopyFor pwlcf = new PWorkLogCopyFor();
                        pwlcf.LogID = entity.Id;
                        pwlcf.PublishEmpID = entity.EmpID;
                        pwlcf.PublishEmpName = entity.EmpName;
                        pwlcf.ReceiveEmpID = entity.CopyForEmpIdList[i];
                        pwlcf.ReceiveEmpName = entity.CopyForEmpNameList[i];
                        pwlcf.LogType = WorkLogType.WorkLogWeek.ToString();
                        IDPWorkLogCopyForDao.Save(pwlcf);
                    }
                }
            }
            else
            {
                result = false;
                return result;
            }
            return result;
        }

    }
}