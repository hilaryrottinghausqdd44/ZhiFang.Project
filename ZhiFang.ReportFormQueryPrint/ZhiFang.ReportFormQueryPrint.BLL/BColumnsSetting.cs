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
    public class BBColumnsSetting
    {
        private readonly IDBColumnsSetting dal = DalFactory<IDBColumnsSetting>.GetDal("BColumnsSetting");
        private readonly IDBColumnsUnit dalUnit = DalFactory<IDBColumnsUnit>.GetDal("BColumnsUnit");


        public DataSet GetList(string strWhere, string order)
        {
            return dal.GetList(strWhere, order);
        }
        public int deleteByAppType(string appType) {
            return dal.deleteByAppType(appType);
        }
        public bool SetDefaultSetting(string appType)
        {
            bool flag = true;
            DataSet ds =  dalUnit.GetList("colid in (2,3,4)");
            if (ds != null && ds.Tables !=null && ds.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("BBColumnsSetting.SetDefaultSetting:查询到列，开始设置默认值");
                BColumnsSetting bcs = new BColumnsSetting();
                bcs.AppType = appType;
                bcs.IsShow = true;
                int count = 0;
                int convInt = 0;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bcs.ColID = (long) item["ColId"];
                    bcs.DataAddTime = new DateTime();
                    bcs.ColumnName = item["ColumnName"].ToString();
                    bcs.OrderNo = int.Parse(item["ColId"].ToString());
                    //bcs.Render = item["Render"].ToString();//.Replace("\t"," ");
                    bcs.ShowName = item["CName"].ToString();
                    bcs.CName = item["CName"].ToString();
                    bcs.Width = int.TryParse(item["Width"].ToString(), out convInt) ? convInt : 0;
                    count=dal.Add(bcs);
                    if (count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("BBColumnsSetting.SetDefaultSetting:ColID="+ bcs.ColID.ToString()+" ColumnName="+ bcs.ColumnName+" 插入失败");
                        flag = false;
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("BBColumnsSetting.SetDefaultSetting:默认设置添加成功");
            return flag;
        }

        public int deleteById(long id) {
            return dal.deleteById(id);
        }
        public int Add(Model.BColumnsSetting t)
        {
            return dal.Add(t);
        }

        public DataSet GetList(Model.BColumnsSetting t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BColumnsSetting t)
        {
            return dal.Update(t);
        }
    }
}

