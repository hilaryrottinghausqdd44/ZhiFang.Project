
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  class BCenOrg : BaseBLL<CenOrg>, ZhiFang.Digitlab.IBLL.ReagentSys.IBCenOrg
	{
        ZhiFang.Digitlab.IDAO.ReagentSys.IDCenOrgTypeDao IDCenOrgTypeDao { get; set; }

        public int GetMaxOrgNo(long orgTypeID, int minOrgNo)
        {
            return ((IDCenOrgDao)(this.DBDao)).GetMaxOrgNoDao(orgTypeID, minOrgNo);
        }

        public int GetMaxOrgNo()
        {
            IList<int> list = this.DBDao.Find<int>("select max(cenorg.OrgNo) as OrgNo  from CenOrg cenorg where 1=1 ");
            if (list != null && list.Count > 0)
            {
                int maxOrgNo = list[0];
                maxOrgNo = maxOrgNo < 100001 ? 100001 : ++maxOrgNo;
                //if (maxOrgNo < 100001)
                //    maxOrgNo = 100001;
                //else
                //    maxOrgNo++;
                return maxOrgNo;
            }
            else
                return 0;
        }


        public bool ExcelSave(System.Data.DataTable dt, out string errorinfo)
        {
            try
            {
                errorinfo="";
                var cenorgtypelist=IDCenOrgTypeDao.GetListByHQL(" 1=1 ");
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["公司ID"].ToString().Trim() != "" && dt.Rows[i]["中文名"].ToString().Trim() != "")
                    {
                        CenOrg co = new CenOrg();
                        co.DataUpdateTime = DateTime.Now;
                        co.OrgNo = int.Parse(dt.Rows[i]["公司ID"].ToString());
                        co.CName = dt.Rows[i]["中文名"].ToString();                        
                        co.Address = dt.Rows[i]["公司地址"].ToString();
                        co.Tel = dt.Rows[i]["电话"].ToString();                        
                        co.Fox = dt.Rows[i]["传真"].ToString();
                        if(dt.Columns.Contains("邮箱"))
                        {
                            co.Email = dt.Rows[i]["邮箱"].ToString();
                        }
                        if (dt.Columns.Contains("网址"))
                        {
                            co.WebAddress = dt.Rows[i]["网址"].ToString();
                        }
                        if (dt.Columns.Contains("备注"))
                        {
                            co.Memo = dt.Rows[i]["备注"].ToString();
                        }
                        if (dt.Columns.Contains("联系人"))
                        {
                            co.Contact = dt.Rows[i]["联系人"].ToString();
                        }
                        var cenorgtype = cenorgtypelist.Where(a => a.CName == dt.Rows[i]["类型"].ToString());
                        if (cenorgtype.Count() > 0)
                        {
                            co.CenOrgType = cenorgtype.ElementAt(0);
                            co.EName = dt.Rows[i]["英文名"].ToString();
                            if (dt.Columns.Contains("次序"))
                            {
                                co.DispOrder = int.Parse(dt.Rows[i]["次序"].ToString());
                            }
                            else
                            {
                                co.DispOrder = i;
                            }

                            if (dt.Columns.Contains("是否使用"))
                            {
                                co.Visible = int.Parse(dt.Rows[i]["是否使用"].ToString());
                            }

                            var tmp = DBDao.GetListByHQL(" cenorg.OrgNo='" + co.OrgNo + "' ");
                            if (tmp.Count <= 0)
                            {
                                DBDao.Save(co);
                            }
                            else
                            {
                                //tmp[0].CName = co.CName;
                                //tmp[0].Address = co.Address;
                                //tmp[0].Tel = co.Tel;
                                //tmp[0].Fox = co.Fox;
                                //tmp[0].Email = co.Email;
                                //tmp[0].WebAddress = co.WebAddress;
                                //tmp[0].Memo = co.Memo;
                                //tmp[0].Contact = co.Contact;
                                //tmp[0].EName = co.EName;
                                //tmp[0].DispOrder = co.DispOrder;
                                //tmp[0].Visible = co.Visible;
                                //tmp[0].OrgNo = co.OrgNo;
                                //tmp[0].CenOrgType = co.CenOrgType;

                                DBDao.UpdateByHql(" update CenOrg as cenorg set cenorg.CName='" + co.CName + "' , cenorg.Address='" + co.Address + "' ,cenorg.Tel='" + co.Tel + "' ,cenorg.Fox='" + co.Fox + "' ,cenorg.Email='" + co.Email + "' ,cenorg.WebAddress='" + co.WebAddress + "' ,cenorg.Memo='" + co.Memo + "' ,cenorg.Contact='" + co.Contact + "' ,cenorg.EName='" + co.EName + "',cenorg.DispOrder=" + co.DispOrder + " ,cenorg.Visible=" + co.Visible + "  ,cenorg.CenOrgType.Id=" + co.CenOrgType.Id + "  where cenorg.OrgNo='" + co.OrgNo + "' ");
                            }
                        }
                        else
                        {
                            errorinfo += "公司类型：" + dt.Rows[i]["类型"].ToString() + "，未找到对应字典。";
                        }
                    }
                }
                if (errorinfo.Trim() == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ExcelSave:" + e.ToString());
                errorinfo = e.ToString();
                return false;
            }
        }

        public BaseResultDataValue JudgeOrgIsExist(string orgNo, ref CenOrg cenorg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<CenOrg> listCenOrg = this.SearchListByHQL(" cenorg.OrgNo=\'" + orgNo + "\'");
            if (listCenOrg != null && listCenOrg.Count > 0)
            {
                if (listCenOrg.Count == 1)
                {
                    cenorg = listCenOrg[0];
                }
                else if (listCenOrg.Count > 1)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "根据机构编码【" + orgNo + "】找到多个对应的机构信息，请联系管理员解决！";
                    //throw new Exception("根据机构编码【" + orgNo + "】找到多个对应的机构信息，请联系管理员解决！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据机构编码【" + orgNo + "】找不到对应的机构信息，请联系管理员维护！";
                //throw new Exception("根据机构编码【" + orgNo + "】找不到对应的机构信息，请联系管理员维护！");
            }
            return baseResultDataValue;

        }
    }
}