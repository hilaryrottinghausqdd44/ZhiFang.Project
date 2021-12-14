using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model.UiModel;
using ZhiFang.BLLFactory;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //B_Lab_TestItem
    public partial class Lab_TestItem : IBSynchData, IBLab_TestItem, IBBatchCopy
    {
        IDAL.IDLab_TestItem dal = DALFactory.DalFactory<IDAL.IDLab_TestItem>.GetDal("B_Lab_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDBatchCopy dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_Lab_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
        public Lab_TestItem()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_TestItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_TestItem model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新颜色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateColor(ZhiFang.Model.Lab_TestItem model)
        {
            return dal.UpdateColor(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, string LabItemNo)
        {

            return dal.Delete(LabCode, LabItemNo);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_TestItem GetModel(string LabCode, string LabItemNo)
        {

            return dal.GetModel(LabCode, LabItemNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_TestItem GetModelByCache(string LabCode, string LabItemNo)
        {

            string CacheKey = "B_Lab_TestItemModel-" + LabCode + "-" + LabItemNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabItemNo, LabItemNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_TestItem)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_TestItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_TestItem> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_TestItem> modelList = new List<ZhiFang.Model.Lab_TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_TestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_TestItem();
                    if (dt.Columns.Contains("ItemID") && dt.Rows[n]["ItemID"].ToString() != "")
                    {
                        model.ItemID = long.Parse(dt.Rows[n]["ItemID"].ToString());
                    }
                    if (dt.Columns.Contains("LabCode") && dt.Rows[n]["LabCode"].ToString() != "")
                    {
                        model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    }
                    if (dt.Columns.Contains("ItemNo") && dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.LabItemNo = dt.Rows[n]["ItemNo"].ToString();
                    }
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

                    if (dt.Columns.Contains("ControlStatus") && dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
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
                    if (dt.Columns.Contains("IsNurseItem") && dt.Rows[n]["IsNurseItem"].ToString() != "")
                    {
                        model.IsNurseItem = dt.Rows[n]["IsNurseItem"].ToString();
                    }
                    //if (t.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    if (dt.Columns.Contains("LabSuperGroupNo") && dt.Rows[n]["LabSuperGroupNo"].ToString() != "")
                    {
                        model.LabSuperGroupNo = int.Parse(dt.Rows[n]["LabSuperGroupNo"].ToString());
                    }
                    if (dt.Columns.Contains("Color") && dt.Rows[n]["Color"].ToString() != "")
                        model.Color = dt.Rows[n]["Color"].ToString();
                    if (dt.Columns.Contains("price") && dt.Rows[n]["price"].ToString() != "")
                    {
                       model.Price = decimal.Parse(dt.Rows[n]["price"].ToString());
                    }
                    if (dt.Columns.Contains("GreatMasterPrice") && dt.Rows[n]["GreatMasterPrice"].ToString() != "")
                    {
                        model.GreatMasterPrice = decimal.Parse(dt.Rows[n]["GreatMasterPrice"].ToString());
                    }
                    if (dt.Columns.Contains("MarketPrice") && dt.Rows[n]["MarketPrice"].ToString() != "")
                    {
                        model.MarketPrice = decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
                    }
                    if (dt.Columns.Contains("BonusPercent") && dt.Rows[n]["BonusPercent"].ToString() != "")
                    {
                        model.BonusPercent = decimal.Parse(dt.Rows[n]["BonusPercent"].ToString());
                    }
                    if (dt.Columns.Contains("PhysicalFlag") && dt.Rows[n]["PhysicalFlag"].ToString() != "")
                    {
                        model.PhysicalFlag = int.Parse(dt.Rows[n]["PhysicalFlag"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.UiModel.ApplyInputItemEntity> modelList = new List<ZhiFang.Model.UiModel.ApplyInputItemEntity>();
            IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
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

                    model.ColorName = dt.Rows[n]["Color"].ToString();
                    model.isCombiItem = dt.Rows[n]["isCombiItem"].ToString();
                    if (dt.Rows[n]["Color"].ToString() != "")
                    {
                        //model.ColorValue = ZhiFang.BLL.Common.Lib.ItemColor()[dt.Rows[n]["Color"].ToString()].ColorValue;

                        if (icd.GetModelByColorName(dt.Rows[n]["Color"].ToString()) == null)
                            model.ColorValue = "";
                        else
                            model.ColorValue = icd.GetModelByColorName(dt.Rows[n]["Color"].ToString()).ColorValue;
                        List<Model.SampleType> sampleTypeList = ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(dt.Rows[n]["Color"].ToString());// ZhiFang.BLL.Common.Lib.ItemColor()[dt.Rows[n]["Color"].ToString()].SampleType;
                        List<SampleTypeDetail> UisampleTypeDetailList = new List<SampleTypeDetail>();
                        SampleTypeDetail UisampleTypeDetail = new SampleTypeDetail();
                        foreach (var sampleType in sampleTypeList)
                        {
                            UisampleTypeDetail = new SampleTypeDetail();
                            UisampleTypeDetail.CName = sampleType.CName;
                            UisampleTypeDetail.SampleTypeID = sampleType.SampleTypeID.ToString();
                            UisampleTypeDetailList.Add(UisampleTypeDetail);
                        }
                        model.SampleTypeDetail = UisampleTypeDetailList;
                    }
                    model.Prices = dt.Rows[n]["price"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        //ganwh add 2015-8-21 统计组套项目总价
        public List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt,string labcode)
        {
            try
            {
                List<ZhiFang.Model.UiModel.ApplyInputItemEntity> modelList = new List<ZhiFang.Model.UiModel.ApplyInputItemEntity>();
                IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
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

                        model.ColorName = dt.Rows[n]["Color"].ToString();
                        if (dt.Rows[n]["Color"].ToString() != "")
                        {
                            //model.ColorValue = ZhiFang.BLL.Common.Lib.ItemColor()[dt.Rows[n]["Color"].ToString()].ColorValue;

                            if (icd.GetModelByColorName(dt.Rows[n]["Color"].ToString()) == null)
                                model.ColorValue = "";
                            else
                                model.ColorValue = icd.GetModelByColorName(dt.Rows[n]["Color"].ToString()).ColorValue;
                            List<Model.SampleType> sampleTypeList = ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(dt.Rows[n]["Color"].ToString());// ZhiFang.BLL.Common.Lib.ItemColor()[dt.Rows[n]["Color"].ToString()].SampleType;
                            List<SampleTypeDetail> UisampleTypeDetailList = new List<SampleTypeDetail>();
                            SampleTypeDetail UisampleTypeDetail = new SampleTypeDetail();
                            foreach (var sampleType in sampleTypeList)
                            {
                                UisampleTypeDetail = new SampleTypeDetail();
                                UisampleTypeDetail.CName = sampleType.CName;
                                UisampleTypeDetail.SampleTypeID = sampleType.SampleTypeID.ToString();
                                UisampleTypeDetailList.Add(UisampleTypeDetail);
                            }
                            model.SampleTypeDetail = UisampleTypeDetailList;
                        }
                        string ItemID = dt.Rows[n]["ItemNo"].ToString();
                        IBLL.Common.BaseDictionary.IBLab_GroupItem LabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
                        
                        double price = 0;
                        if (dt.Rows[n]["price"] != null && dt.Rows[n]["price"].ToString() != "" && dt.Rows[n]["price"].ToString() != "0")
                        {
                            double result = 0;
                            if (double.TryParse(dt.Rows[n]["price"].ToString(), out result))
                            {
                                price = double.Parse(dt.Rows[n]["price"].ToString());
                            }
                        }
                        else
                        {
                            DataSet dsitem = LabGroupItem.GetGroupItemList(ItemID, labcode);
                            if (dsitem != null && dsitem.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsitem.Tables[0].Rows.Count; i++)
                                {
                                    if (dsitem.Tables[0].Rows[i]["price"] != null && dsitem.Tables[0].Rows[i]["price"].ToString() != "" && dsitem.Tables[0].Rows[i]["price"].ToString() != "0")
                                    {
                                        double result = 0;
                                        if (double.TryParse(dsitem.Tables[0].Rows[i]["price"].ToString(), out result))
                                        {
                                            price += double.Parse(dsitem.Tables[0].Rows[i]["price"].ToString());
                                        }

                                    }
                                }
                            }
                        }

                        model.Prices = price.ToString();

                        modelList.Add(model);
                    }
                }
                return modelList;
            }
            catch (Exception ex) { 
                ZhiFang.Common.Log.Log.Info("Lab_TestItem.sc_ItemEntityDataTableToList "+ex.Message.ToString());}
            return null;
            
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
        public DataSet GetList(ZhiFang.Model.Lab_TestItem model)
        {
            return dal.GetList(model);
        }
        public DataSet GetList(ZhiFang.Model.Lab_TestItem model, string flag)
        {
            return dal.GetList(model, flag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="nowPageNum"></param>
        /// <param name="nowPageSize"></param>
        /// <returns></returns>
        public DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return dal.GetList(model);
        }

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <param name="OrderBy">排序字段</param>
        /// <returns>DataSet</returns> 
        public DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model, int nowPageNum, int nowPageSize, string sort, string order)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize, sort, order);
            }
            else
                return dal.GetList(model);
        }

        public int GetTotalCount(ZhiFang.Model.Lab_TestItem model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion

        #region IBBase<Lab_TestItem> 成员


        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBLab_TestItem 成员

        public bool Exists(string LabCode, string LabItemNo)
        {
            return dal.Exists(LabCode, LabItemNo);
        }

        #endregion

        #region IBBase<Lab_TestItem> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
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

        public string GetGroupItemColor(string itemno, string labcode)
        {
            IBLab_GroupItem blgi = BLLFactory.BLLFactory<IBLab_GroupItem>.GetBLL();
            string itemlist = "";
            DataSet ds = blgi.GetGroupItemList(itemno, labcode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        itemlist += GetGroupItemColor(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                    }
                    else
                    {
                        itemlist += ds.Tables[0].Rows[i]["ItemNo"] + "," + ds.Tables[0].Rows[i]["CName"] + "," + ds.Tables[0].Rows[i]["Color"] + "|";
                    }
                }
            }
            return itemlist;
        }

        #region IBLab_TestItem 成员


        public void GetColorTotal(string itemno, string labcode, ref Dictionary<string, int> dic)
        {
            Dictionary<string, ColorSampleType> d = ZhiFang.BLL.Common.Lib.ItemColor();
            IBLab_GroupItem blgi = BLLFactory.BLLFactory<IBLab_GroupItem>.GetBLL();
            DataSet ds = blgi.GetGroupItemList(itemno, labcode);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                        {
                            GetColorTotal(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode, ref dic);
                        }
                        else
                        {
                            foreach (var l in dic)
                            {
                                if (l.Key.Split(',')[1] == ds.Tables[0].Rows[i]["Color"].ToString())
                                {
                                    dic[l.Key]++;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ds = this.GetList(new Model.Lab_TestItem() { LabCode = labcode, LabItemNo = itemno });
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (var l in dic)
                        {
                            if (l.Key.Split(',')[1] == ds.Tables[0].Rows[0]["Color"].ToString())
                            {
                                dic[l.Key]++;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public Dictionary<string, int> GetColorTotal(string itemno, string labcode)
        {
            Dictionary<string, ColorSampleType> d = ZhiFang.BLL.Common.Lib.ItemColor();
            Dictionary<string, int> d1 = new Dictionary<string, int>();
            foreach (var l in d)
            {
                d1.Add(l.Key + "," + l.Value.ColorValue, 0);
            }
            GetColorTotal(itemno, labcode, ref d1);
            return d1;
        }
        Dictionary<string, string> dic = new Dictionary<string, string>();
        public Dictionary<string, string> GetColor(string itemno, string labcode)
        {
            Dictionary<string, ColorSampleType> d = ZhiFang.BLL.Common.Lib.ItemColor();
            Dictionary<string, int> d1 = new Dictionary<string, int>();

            IBLab_GroupItem blgi = BLLFactory.BLLFactory<IBLab_GroupItem>.GetBLL();
            string[] itemnos = itemno.Split(',');
            foreach (var l in d)
            {
                d1.Add(l.Key + "," + l.Value.ColorValue, 0);
            }
            //循环项目多个
            for (int it = 0; it < itemnos.Length; it++)
            {
                DataSet ds = blgi.GetGroupItemList(itemnos[it], labcode);

                string key = "";
                string value = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1" || ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                        {
                            GetColorTotal(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                        }
                        else
                        {
                            int f = 0;//首次用
                            int m = 0;//d1用末次
                            foreach (var l in d1)
                            {
                                if (l.Key.Trim() == ds.Tables[0].Rows[i]["Color"].ToString())
                                {
                                    //判断key是否相等
                                    //相等直接赋值value累加
                                    //第一次不相等执行else
                                    //第二次不相等执行else
                                    //第二次相等执行if
                                    if (key == l.Key)
                                    {
                                        key = l.Key;
                                        value += ds.Tables[0].Rows[i]["Cname"].ToString() + "&";

                                        //相等情况 如果是最后一条数据
                                        //并且key出现新的
                                        //在出现一次
                                        if (i + 1 == ds.Tables[0].Rows.Count)
                                        {
                                            try
                                            {
                                                dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            }
                                            catch (Exception)
                                            {

                                            }

                                        }
                                        //dic = GetColors(key, itemno, value);
                                    }
                                    else
                                    {

                                        //首次Add
                                        if (f == 0)
                                        {
                                            key = l.Key;
                                            value = ds.Tables[0].Rows[i]["Cname"].ToString() + "&";
                                            try
                                            {
                                                dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        else
                                        {
                                            dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            key = l.Key;
                                            value = ds.Tables[0].Rows[i]["Cname"].ToString() + "&";
                                            //不相等情况 如果是最后一条数据
                                            //并且key出现新的
                                            //在出现一次
                                            if (i + 1 == ds.Tables[0].Rows.Count)
                                            {
                                                try
                                                {
                                                    dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                                }
                                                catch { }
                                            }
                                        }

                                    }
                                    f++;
                                    break;
                                }
                                else
                                {
                                    int a = m;
                                    if (i + 1 == ds.Tables[0].Rows.Count && m == d1.Count - 1)
                                    {
                                        try
                                        {
                                            dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                                m++;
                            }
                        }

                    }
                    //dic.Add(l.Key, itemno + "," + ds.Tables[0].Rows[i]["Cname"].ToString());
                }
                else
                {
                    ds = this.GetList(new Model.Lab_TestItem() { LabCode = labcode, LabItemNo = itemnos[it] });
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int f = 0;//首次用
                            int m = 0;//d1用末次
                            foreach (var l in d1)
                            {
                                if (l.Key == ds.Tables[0].Rows[i]["Color"].ToString())
                                {
                                    //判断key是否相等
                                    //相等直接赋值value累加
                                    //第一次不相等执行else
                                    //第二次不相等执行else
                                    //第二次相等执行if
                                    if (key == l.Key)
                                    {
                                        key = l.Key;
                                        value += ds.Tables[0].Rows[i]["Cname"].ToString() + "&";

                                        //相等情况 如果是最后一条数据
                                        //并且key出现新的
                                        //在出现一次
                                        if (i + 1 == ds.Tables[0].Rows.Count)
                                        {
                                            try
                                            {
                                                dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            }
                                            catch (Exception)
                                            {
                                            }

                                        }
                                        //dic = GetColors(key, itemno, value);
                                    }
                                    else
                                    {

                                        //首次Add
                                        if (f == 0)
                                        {
                                            key = l.Key;
                                            value = ds.Tables[0].Rows[i]["Cname"].ToString() + "&";
                                            try
                                            {
                                                dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        else
                                        {
                                            dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                            key = l.Key;
                                            value = ds.Tables[0].Rows[i]["Cname"].ToString() + "&";
                                            //不相等情况 如果是最后一条数据
                                            //并且key出现新的
                                            //在出现一次
                                            if (i + 1 == ds.Tables[0].Rows.Count)
                                            {
                                                try
                                                {
                                                    dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                                }
                                                catch (Exception)
                                                {
                                                }

                                            }
                                        }

                                    }
                                    f++;
                                    //break;
                                }
                                else
                                {
                                    int a = m;
                                    if (i + 1 == ds.Tables[0].Rows.Count && m == d1.Count - 1)
                                    {
                                        try
                                        {
                                            dic = GetColors(key, itemnos[it], value.TrimEnd('&'));
                                        }
                                        catch (Exception)
                                        {
                                        }

                                    }
                                }
                                m++;
                            }
                        }

                    }
                }
            }
            return dic;
        }

        private Dictionary<string, string> GetColors(string key, string itemNo, string Cname)
        {
            dic.Add(key, itemNo + "," + Cname);
            return dic;
        }


        #endregion

        #region IBBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }

        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        public DataSet GetLabTestItemByItemNo(string labCode, string ItemNo)
        {
            return dal.GetLabTestItemByItemNo(labCode, ItemNo);
        }


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}