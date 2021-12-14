using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BBSearchSetting
    {
        private readonly IDBSearchSetting dal = DalFactory<IDBSearchSetting>.GetDal("BSearchSetting");
        private readonly IDBSearchUnit dalUtil = DalFactory<IDBSearchUnit>.GetDal("BSearchUnit");

        public bool SetDefaultSetting(string appType) {
            bool flag = true;
            DataSet ds = dalUtil.GetList("sid in (9,1,3)");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("BBSearchSetting.SetDefaultSetting:查询到列，开始设置默认值");
                BSearchSetting bcs = new BSearchSetting();
                bcs.AppType = appType;
                bcs.IsShow = true;
                int count = 0;
                int convInt = 0;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bcs.SID = (long)item["SID"];
                    bcs.DataAddTime = new DateTime();
                    bcs.SelectName = item["SelectName"].ToString();
                    bcs.JsCode = item["JsCode"].ToString();//.Replace("\t"," ");
                    bcs.ShowName = item["CName"].ToString();
                    bcs.CName = item["CName"].ToString();
                    bcs.ShowOrderNo = int.Parse(item["SID"].ToString());
                    bcs.Width = int.TryParse(item["Width"].ToString(),out convInt) ? convInt:0;
                    bcs.TextWidth = int.TryParse(item["TextWidth"].ToString(), out convInt) ? convInt : 0;
                    bcs.Xtype= item["Xtype"].ToString();
                    bcs.Type = item["Type"].ToString();
                    bcs.Listeners= item["Listeners"].ToString();
                    bcs.Mark = item["Mark"].ToString();
                    count = dal.Add(bcs);
                    if (count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("BBSearchSetting.SetDefaultSetting:ColID=" + bcs.SID.ToString() + " ColumnName=" + bcs.SelectName + " 插入失败");
                        flag = false;
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("BBSearchSetting.SetDefaultSetting:默认设置添加成功");
            return flag;
        }

        public int deleteByAppType(string appType) {
            return dal.deleteByAppType(appType);
        }

        public int deleteById(int id) {
            return dal.deleteById(id);
        }
        public int Add(Model.BSearchSetting t)
        {
            return dal.Add(t);
        }

        public DataSet GetList(Model.BSearchSetting t)
        {
            throw new NotImplementedException();
        }
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public DataSet GetList(string strWhere,string order)
        {
            return dal.GetList(strWhere,order);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BSearchSetting t)
        {
            return dal.Update(t);
        }
        //查询要添加的高级查询项是否存在
        public int GetIsSenior(long STID) {
           int isok = dal.GetIsSenior(STID);
            return isok;
        }
    }
}

