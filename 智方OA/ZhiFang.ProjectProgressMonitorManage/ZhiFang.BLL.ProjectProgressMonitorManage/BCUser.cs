
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BCUser : BaseBLL<CUser>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBCUser
    {
        IDPClientDao IDPClientDao { get; set; }
        IBBProvince IBBProvince { get; set; }
        //IBBCountry IBBCountry { get; set; }

        /// <summary>
        /// 将CUser某一记录行复制到PClient中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResultBool CopyCUserToPClientByCUserId(long id, int type, long empID, string empName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            CUser cuser = this.Get(id);
            if (cuser != null)
            {
                PClient pclient = new PClient();
                pclient.IsUse = true;
                pclient.IsRepeat = false;
                pclient.Id = pclient.Id + cuser.Id;
                pclient.Name = cuser.UserName;
                pclient.LicenceClientName = cuser.UserName;
                pclient.ProvinceName = cuser.UserArea;
                if (!String.IsNullOrEmpty(pclient.ProvinceName))
                {
                    BProvince bprovince = null;
                    IList<BProvince> list = IBBProvince.SearchListByHQL("Name='" + pclient.ProvinceName.Trim() + "'");
                    if (list != null && list.Count > 0)
                    {
                        bprovince = list[0];
                    }
                    if (bprovince != null)
                    {
                        pclient.ProvinceID = bprovince.Id;
                        if (bprovince.BCountry != null)
                        {
                            pclient.CountryID = bprovince.BCountry.Id;
                            pclient.CountryName = bprovince.BCountry.Name;
                        }
                    }
                }
                pclient.LinkMan = cuser.UserLinkman;
                pclient.PhoneNum = cuser.UserTelephone;
                pclient.Address = cuser.UserAddress;

                pclient.MailNo = cuser.UserPostcode;
                pclient.Emall = "";
                pclient.Fax = cuser.UserFax;
                if (!String.IsNullOrEmpty(cuser.UserFWNo))
                {
                    pclient.LicenceCode = cuser.UserFWNo.Trim();
                }
                pclient.LRNo1 = cuser.LRNo1;
                pclient.LRNo2 = cuser.LRNo2;
                baseResultBool.success = IDPClientDao.Save(pclient);
                if (baseResultBool.success)
                {
                    IDPClientDao.UpdateByHql(" update PClient as p set p.ClientNo=(select max(p1.ClientNo)+1 from PClient as p1) where p.Id=" + pclient.Id);
                    cuser.IsMapping = true;
                    switch (type)
                    {
                        case 1:
                            cuser.ContrastId = empID;
                            cuser.ContrastCName = empName;
                            break;
                        case 2:
                            if (!cuser.ContrastId.HasValue)
                                cuser.ContrastId = empID;
                            if (String.IsNullOrEmpty(cuser.ContrastCName))
                                cuser.ContrastCName = empName;
                            cuser.CheckId = empID;
                            cuser.CheckCName = empName;
                            break;
                        default:
                            break;
                    }
                    this.Entity = cuser;
                    this.Edit();
                }

            }
            return baseResultBool;
        }
    }
}