using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //B_TestItem
    public partial class TestItem : IBSynchData, IBTestItem, IBBatchCopy, IBDataDownload
    {
        IDAL.IDTestItem dal;
        IDAL.IDBatchCopy dalCopy;

        public TestItem()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDTestItem>.GetDal("TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDTestItem>.GetDal("B_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ItemNo)
        {

            return dal.Delete(ItemNo);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TestItem GetModel(string ItemNo)
        {

            return dal.GetModel(ItemNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.TestItem GetModelByCache(string ItemNo)
        {

            string CacheKey = "B_TestItemModel-" + ItemNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ItemNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.TestItem)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.TestItem> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.TestItem> modelList = new List<ZhiFang.Model.TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.TestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.TestItem();
                    if (dt.Columns.Contains("ItemNo") && dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("EName") && dt.Rows[n]["EName"].ToString() != "")
                    {
                        model.EName = dt.Rows[n]["EName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortName") && dt.Rows[n]["ShortName"].ToString() != "")
                    {
                        model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("DiagMethod") && dt.Rows[n]["DiagMethod"].ToString() != "")
                    {
                        model.DiagMethod = dt.Rows[n]["DiagMethod"].ToString();
                    }
                    if (dt.Columns.Contains("Unit") && dt.Rows[n]["Unit"].ToString() != "")
                    {
                        model.Unit = dt.Rows[n]["Unit"].ToString();
                    }
                    if (dt.Columns.Contains("IsCalc") && dt.Rows[n]["IsCalc"].ToString() != "")
                    {
                        model.IsCalc = int.Parse(dt.Rows[n]["IsCalc"].ToString());
                    }
                    if (dt.Columns.Contains("Visible") && dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("Prec") && dt.Rows[n]["Prec"].ToString() != "")
                    {
                        model.Prec = int.Parse(dt.Rows[n]["Prec"].ToString());
                    }
                    if (dt.Columns.Contains("IsProfile") && dt.Rows[n]["IsProfile"].ToString() != "")
                    {
                        model.IsProfile = int.Parse(dt.Rows[n]["IsProfile"].ToString());
                    }
                    if (dt.Columns.Contains("OrderNo") && dt.Rows[n]["OrderNo"].ToString() != "")
                    {
                        model.OrderNo = dt.Rows[n]["OrderNo"].ToString();
                    }
                    if (dt.Columns.Contains("StandardCode") && dt.Rows[n]["StandardCode"].ToString() != "")
                    {
                        model.StandardCode = dt.Rows[n]["StandardCode"].ToString();
                    }
                    if (dt.Columns.Contains("ItemDesc") && dt.Rows[n]["ItemDesc"].ToString() != "")
                    {
                        model.ItemDesc = dt.Rows[n]["ItemDesc"].ToString();
                    }
                    if (dt.Columns.Contains("FWorkLoad") && dt.Rows[n]["FWorkLoad"].ToString() != "")
                    {
                        model.FWorkLoad = decimal.Parse(dt.Rows[n]["FWorkLoad"].ToString());
                    }
                    if (dt.Columns.Contains("Secretgrade") && dt.Rows[n]["Secretgrade"].ToString() != "")
                    {
                        model.Secretgrade = int.Parse(dt.Rows[n]["Secretgrade"].ToString());
                    }
                    if (dt.Columns.Contains("Cuegrade") && dt.Rows[n]["Cuegrade"].ToString() != "")
                    {
                        model.Cuegrade = int.Parse(dt.Rows[n]["Cuegrade"].ToString());
                    }
                    if (dt.Columns.Contains("IsDoctorItem") && dt.Rows[n]["IsDoctorItem"].ToString() != "")
                    {
                        model.IsDoctorItem = int.Parse(dt.Rows[n]["IsDoctorItem"].ToString());
                    }
                    if (dt.Columns.Contains("IschargeItem") && dt.Rows[n]["IschargeItem"].ToString() != "")
                    {
                        model.IschargeItem = int.Parse(dt.Rows[n]["IschargeItem"].ToString());
                    }
                    if (dt.Columns.Contains("IsCombiItem") && dt.Rows[n]["IsCombiItem"].ToString() != "")
                    {
                        model.IsCombiItem = int.Parse(dt.Rows[n]["IsCombiItem"].ToString());
                    }
                    if (dt.Columns.Contains("HisDispOrder") && dt.Rows[n]["HisDispOrder"].ToString() != "")
                    {
                        model.HisDispOrder = int.Parse(dt.Rows[n]["HisDispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("Color") && dt.Rows[n]["Color"].ToString() != "")
                    {
                        model.Color = dt.Rows[n]["Color"].ToString();
                    }
                    if (dt.Columns.Contains("IsNurseItem") && dt.Rows[n]["IsNurseItem"].ToString() != "")
                    {
                        model.IsNurseItem = dt.Rows[n]["IsNurseItem"].ToString();
                    }
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("StandCode") && dt.Rows[n]["StandCode"].ToString() != "")
                    {
                        model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    }
                    if (dt.Columns.Contains("ZFStandCode") && dt.Rows[n]["ZFStandCode"].ToString() != "")
                    {
                        model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    if (dt.Columns.Contains("SuperGroupNo") && dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    {
                        model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
                    }
                    if (dt.Columns.Contains("price") && dt.Rows[n]["price"].ToString() != "")
                    {
                        model.Price = decimal.Parse(dt.Rows[n]["price"].ToString()); 
                    }
                    if (dt.Columns.Contains("ItemNoName") && dt.Rows[n]["ItemNoName"].ToString() != "")
                    {
                        model.ItemNoName = dt.Rows[n]["ItemNoName"].ToString();
                    }
                    //model.TestItemControl = new Model.TestItemControl() { Id = 33 };

                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.UiModel.ApplyInputItemEntity> modelList = new List<ZhiFang.Model.UiModel.ApplyInputItemEntity>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.UiModel.ApplyInputItemEntity model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.UiModel.ApplyInputItemEntity();
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();

                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.EName = dt.Rows[n]["EName"].ToString();

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
            return GetList(new ZhiFang.Model.TestItem());
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.TestItem model)
        {
            return dal.GetList(model);
        }
        public DataSet GetList(ZhiFang.Model.TestItem model, string flag)
        {
            return dal.GetList(model, flag);
        }

        public DataSet GetListByPage(ZhiFang.Model.TestItem model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        #endregion

        #region IBBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            //////////////////////////////////////

            //return dalCopy.CopyToLab(lst);
            IDAL.IDBatchCopy dalCopyGI = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").IndexOf("ZhiFang.DAL.MsSql.Digitlab8") < 0)
            {
                dalCopyGI = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("GroupItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            //IDAL.IDBatchCopy dalCopyTIC = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_TestItemControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
            IDAL.IDTestItemControl dalTIC = DALFactory.DalFactory<IDAL.IDTestItemControl>.GetDal("B_TestItemControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                if (lst[0].Trim() == "CopyToLab_LabFirstSelect")
                {
                    if (dalCopy.CopyToLab(lst) && dalCopyGI.CopyToLab(lst))
                        return true;
                    else
                        return false;
                }
                else
                {

                    if (dalCopy.CopyToLab(lst) && dalCopyGI.CopyToLab(lst))
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region IBBase<TestItem> 成员


        public int GetTotalCount(Model.TestItem model)
        {
            return dal.GetTotalCount(model);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBTestItem 成员

        public DataSet GetListLike(Model.TestItem ti_m)
        {
            return dal.GetListLike(ti_m);
        }

        public DataSet GetList(int p, int PageIndex, Model.TestItem testItem)
        {
            return dal.GetListByPage(testItem, PageIndex, p);
        }

        public int GetListCount(ZhiFang.Common.Dictionary.TestItemSuperGroupClass globalZhiFangCommonDictionaryTestItemSuperGroupClass)
        {
            return this.GetListCount(new Model.TestItem() { TestItemSuperGroupClass = globalZhiFangCommonDictionaryTestItemSuperGroupClass });
        }

        public int GetListCount(Model.TestItem testItem)
        {
            return dal.GetTotalCount(testItem);
        }

        #endregion

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.TestItem.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，TestItem获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region IBTestItem 成员

        public bool Exists(string ItemNo)
        {
            return dal.Exists(ItemNo);
        }

        #endregion

        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }
        public int DeleteByDataRow(DataRow dr)
        {
            return dalCopy.DeleteByDataRow(dr);
        }



        public DataSet getItemCName(string ItemNo)
        {
            return dal.getItemCName(ItemNo);
        }

        //public DataSet getTestItemByCombiItem(string superGroup)
        //{
        //    return dal.getTestItemByCombiItem(superGroup);
        //}



        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }


        public int Add(List<Model.TestItem> modelList)
        {
            return dal.Add(modelList);
        }

        public int Update(List<Model.TestItem> modelList)
        {
            return dal.Update(modelList);
        }
    }
}
