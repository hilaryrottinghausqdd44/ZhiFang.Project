using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Text;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_GroupItem		
    public partial class Lab_GroupItem : IBSynchData, IBLab_GroupItem
    {
        IDAL.IDLab_GroupItem dal = DALFactory.DalFactory<IDAL.IDLab_GroupItem>.GetDal("B_Lab_GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_GroupItem()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_GroupItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_GroupItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string PItemNo, string ItemNo, string LabCode)
        {

            return dal.Delete(PItemNo, ItemNo, LabCode);
        }
        public int Delete(ZhiFang.Model.Lab_GroupItem model, string flag)
        {
            return dal.Delete(model, flag);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_GroupItem GetModel(string PItemNo, string ItemNo, string LabCode)
        {

            return dal.GetModel(PItemNo, ItemNo, LabCode);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_GroupItem GetModelByCache(string PItemNo, string ItemNo, string LabCode)
        {

            string CacheKey = "B_Lab_GroupItemModel-" + PItemNo + ItemNo + LabCode;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PItemNo, ItemNo, LabCode);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_GroupItem)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_GroupItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.Lab_GroupItem());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_GroupItem> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_GroupItem> modelList = new List<ZhiFang.Model.Lab_GroupItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_GroupItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_GroupItem();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.PItemNo = dt.Rows[n]["PItemNo"].ToString();
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_GroupItem model)
        {
            return dal.GetList(model);
        }
        #endregion

        #region IBBase<Lab_GroupItem> 成员


        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.Lab_GroupItem model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<Lab_GroupItem> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBLab_GroupItem 成员

        public bool Exists(string PItemNo, string ItemNo, string LabCode)
        {
            return dal.Exists(PItemNo, ItemNo, LabCode);
        }

        #endregion

        #region IBLab_GroupItem 成员


        public int DeleteAll(string LabCode)
        {
            return dal.DeleteAll(LabCode);
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

        #region IBLab_GroupItem 成员


        public DataSet GetGroupItemList(string pitemno, string labcode)
        {
            return dal.GetList(new Model.Lab_GroupItem() { PItemNo = pitemno, LabCode = labcode });
        }
        public DataSet GetGroupItemListBySubItemNo(string subitemno, string labcode)
        {
            return dal.GetPitemList(new Model.Lab_GroupItem() { ItemNo = subitemno, LabCode = labcode });
        }
        ItemColorDict icd = new ItemColorDict();
        public string GetSubItemList_No_CName(string p, string labcode)
        {
            string itemlist = "";
            DataSet ds = this.GetGroupItemList(p, labcode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        itemlist += GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                    }
                    else
                    {
                        string bcolor = "#ffffff";
                        if (ds.Tables[0].Rows[i]["Color"] != DBNull.Value && ds.Tables[0].Rows[i]["Color"].ToString().Trim() != "")
                        {
                            bcolor = icd.GetModelByColorName(ds.Tables[0].Rows[i]["Color"].ToString().Trim()).ColorValue;//ZhiFang.BLL.Common.Lib.ItemColor()[ds.Tables[0].Rows[i]["Color"].ToString().Trim()].ColorValue;
                        }
                        itemlist += ds.Tables[0].Rows[i]["ItemNo"] + "," + ds.Tables[0].Rows[i]["CName"] + "," + bcolor + "|";
                    }
                }
            }
            return itemlist;
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder) {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        #endregion
    }
}