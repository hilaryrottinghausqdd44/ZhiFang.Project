using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //B_GroupItem
    public partial class GroupItem : IBSynchData, IBGroupItem, IBBatchCopy, IBDataDownload
    {
        IDAL.IDGroupItem dal;
        IDAL.IDBatchCopy dalCopy;

        public GroupItem()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDGroupItem>.GetDal("GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDGroupItem>.GetDal("B_GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.GroupItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.GroupItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string PItemNo, string ItemNo)
        {

            return dal.Delete(PItemNo, ItemNo);
        }
        public int Delete(ZhiFang.Model.GroupItem model, string flag)
        {
            return dal.Delete(model, flag);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.GroupItem GetModel(string PItemNo, string ItemNo)
        {

            return dal.GetModel(PItemNo, ItemNo);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.GroupItem> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.GroupItem> modelList = new List<ZhiFang.Model.GroupItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.GroupItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.GroupItem();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("PItemNo") && dt.Rows[n]["PItemNo"].ToString() != "")
                    {
                        model.PItemNo = dt.Rows[n]["PItemNo"].ToString();
                    }
                    if (dt.Columns.Contains("ItemNo") && dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    }
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetGroupItemList(string p)
        {
            return dal.GetList(new Model.GroupItem() { PItemNo = p });
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList(new ZhiFang.Model.GroupItem());
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.GroupItem model)
        {
            return dal.GetList(model);
        }
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        #endregion

        #region IBBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }

        #endregion

        #region IBBase<GroupItem> 成员


        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.GroupItem model)
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml结构文件
                strXML = dsAll.GetXml();//数据内容xml文件
                strMsg = "通过服务获取XML成功";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.GroupItem.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，GroupItem获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBGroupItem 成员

        public bool Exists(string PItemNo, string ItemNo)
        {
            return dal.Exists(PItemNo, ItemNo);
        }

        #endregion

        public string GetSubItemList_No_CName(string p)
        {
            string itemlist = "";
            DataSet ds = this.GetGroupItemList(p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        itemlist += GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                    }
                    else
                    {
                        itemlist += ds.Tables[0].Rows[i]["ItemNo"] + "," + ds.Tables[0].Rows[i]["CName"] + "|";
                    }
                }
            }
            return itemlist;
        }

        public string GetSubItemList_No(string p)
        {
            string itemlist = "";
            DataSet ds = this.GetGroupItemList(p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        itemlist += GetSubItemList_No(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                    }
                    else
                    {
                        itemlist += ds.Tables[0].Rows[i]["ItemNo"] + "|";
                    }
                }
            }
            return itemlist;
        }

        #region IBGroupItem 成员


        public int DeleteAll()
        {
            return dal.DeleteAll();
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion

        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
    }
}
