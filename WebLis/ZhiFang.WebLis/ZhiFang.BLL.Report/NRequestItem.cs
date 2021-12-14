using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.Model.UiModel;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Report
{
    public partial class NRequestItem : IBNRequestItem
    {
        IDAL.IDNRequestItem dal = DalFactory<IDAL.IDNRequestItem>.GetDalByClassName("NRequestItem");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");

        public NRequestItem()
        {

        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int NRequestItemNo)
        {
            return dal.Exists(NRequestItemNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.NRequestItem model)
        {
            return dal.Add(model);
        }

        public int Add_TaiHe(ZhiFang.Model.NRequestItem model)
        {
            return dal.Add_TaiHe(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.NRequestItem model)
        {
            return dal.Update(model);
        }
        public int Update_TaiHe(ZhiFang.Model.NRequestItem model)
        {
            return dal.Update_TaiHe(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int NRequestItemNo)
        {
            return dal.Delete(NRequestItemNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string NRequestItemNolist)
        {
            return dal.DeleteList(NRequestItemNolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.NRequestItem GetModel(int NRequestItemNo)
        {
            return dal.GetModel(NRequestItemNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.NRequestItem GetModelByCache(int NRequestItemNo)
        {

            string CacheKey = "NRequestItemModel-" + NRequestItemNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(NRequestItemNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.NRequestItem)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.NRequestItem> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.NRequestItem> modelList = new List<ZhiFang.Model.NRequestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.NRequestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.NRequestItem();
                    if (dt.Rows[n]["NRequestItemNo"].ToString() != "")
                    {
                        model.NRequestItemNo = int.Parse(dt.Rows[n]["NRequestItemNo"].ToString());
                    }
                    if (dt.Rows[n]["HISCharge"].ToString() != "")
                    {
                        model.HISCharge = decimal.Parse(dt.Rows[n]["HISCharge"].ToString());
                    }
                    if (dt.Rows[n]["ItemCharge"].ToString() != "")
                    {
                        model.ItemCharge = decimal.Parse(dt.Rows[n]["ItemCharge"].ToString());
                    }
                    if (dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    model.zdy1 = dt.Rows[n]["zdy1"].ToString();
                    model.zdy2 = dt.Rows[n]["zdy2"].ToString();
                    model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
                    model.zdy3 = dt.Rows[n]["zdy3"].ToString();
                    model.zdy4 = dt.Rows[n]["zdy4"].ToString();
                    model.zdy5 = dt.Rows[n]["zdy5"].ToString();
                    if (dt.Rows[n]["DeleteFlag"].ToString() != "")
                    {
                        model.DeleteFlag = int.Parse(dt.Rows[n]["DeleteFlag"].ToString());
                    }
                    if (dt.Rows[n]["ItemSource"].ToString() != "")
                    {
                        model.ItemSource = int.Parse(dt.Rows[n]["ItemSource"].ToString());
                    }
                    model.OldSerialNo = dt.Rows[n]["OldSerialNo"].ToString();
                    model.CountNodesItemSource = dt.Rows[n]["CountNodesItemSource"].ToString();
                    if (dt.Rows[n]["ReportFlag"].ToString() != "")
                    {
                        model.ReportFlag = int.Parse(dt.Rows[n]["ReportFlag"].ToString());
                    }
                    if (dt.Rows[n]["PartFlag"].ToString() != "")
                    {
                        model.PartFlag = int.Parse(dt.Rows[n]["PartFlag"].ToString());
                    }
                    model.WebLisOrgID = dt.Rows[n]["WebLisOrgID"].ToString();
                    model.WebLisSourceOrgID = dt.Rows[n]["WebLisSourceOrgID"].ToString();
                    model.WebLisSourceOrgName = dt.Rows[n]["WebLisSourceOrgName"].ToString();
                    model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    if (dt.Rows[n]["NRequestFormNo"].ToString() != "")
                    {
                        model.NRequestFormNo = long.Parse(dt.Rows[n]["NRequestFormNo"].ToString());
                    }
                    if (dt.Rows[n]["BarCodeFormNo"].ToString() != "")
                    {
                        model.BarCodeFormNo = long.Parse(dt.Rows[n]["BarCodeFormNo"].ToString());
                    }
                    if (dt.Rows[n]["FormNo"].ToString() != "")
                    {
                        model.FormNo = int.Parse(dt.Rows[n]["FormNo"].ToString());
                    }
                    if (dt.Rows[n]["TollItemNo"].ToString() != "")
                    {
                        model.TollItemNo = int.Parse(dt.Rows[n]["TollItemNo"].ToString());
                    }
                    if (dt.Rows[n]["ParItemNo"].ToString() != "")
                    {
                        //model.ParItemNo = int.Parse(dt.Rows[n]["ParItemNo"].ToString());
                        model.ParItemNo = dt.Rows[n]["ParItemNo"].ToString();
                    }
                    if (dt.Rows[n]["IsCheckFee"].ToString() != "")
                    {
                        model.IsCheckFee = int.Parse(dt.Rows[n]["IsCheckFee"].ToString());
                    }
                    if (dt.Rows[n]["ReceiveFlag"].ToString() != "")
                    {
                        model.ReceiveFlag = int.Parse(dt.Rows[n]["ReceiveFlag"].ToString());
                    }
                    if (dt.Rows[n]["CombiItemNo"].ToString() != "" && dt.Rows[n]["CombiItemNo"].ToString() != null)
                        model.CombiItemNo = dt.Rows[n]["CombiItemNo"].ToString();
                    else
                        model.CombiItemNo = null;
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
        public DataSet GetList(ZhiFang.Model.NRequestItem model)
        {
            return dal.GetList(model);
        }

        public DataSet GetList_TaiHe(ZhiFang.Model.NRequestItem model)
        {
            return dal.GetList_TaiHe(model);
        }

        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.NRequestItem model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.NRequestItem model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion

        #region IBNRequestItem 成员


        public bool DeleteList_ByNRequestFormNo(long NRequestItemNolist)
        {
            return dal.DeleteList_ByNRequestFormNo(NRequestItemNolist);
        }

        public DataSet GetList_By_NRequestFormNo(long NRequestFormNo)
        {
            return dal.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = NRequestFormNo });
        }
        public bool CheckNReportItemCenter(DataSet dsItem, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                switch (str)
                {
                    case "SAMPLETYPENO":
                        //if (dsItem.Tables[0].Columns.Contains("SampleTypeNo"))
                        //{
                        //    for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                        //    {
                        //        if (dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                        //        {
                        //            stringList.Add(dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                        //        }
                        //    }
                        //    if (stringList.Count > 0)
                        //    {
                        //        result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                        //        if (!result)
                        //        {
                        //            for (int j = 0; j < stringList.Count; j++)
                        //            {
                        //                SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                        //                SamplleTypeControl.LabCode = DestiOrgID;
                        //                int count = idstc.GetTotalCount(SamplleTypeControl);
                        //                if (count <= 0)
                        //                {
                        //                    ReturnDescription = String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照", SamplleTypeControl.SampleTypeNo);
                        //                }
                        //            }
                        //            return false;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ReturnDescription += String.Format("ItemCenter_SampleTypeNo实验室内样本类型为空，无法进行对照");
                        //        return false;
                        //    }
                        //}
                        break;
                    case "ITEMNO":
                        if (dsItem.Tables[0].Columns.Contains("ParItemNo"))
                        {
                            for (int count = 0; count < dsItem.Tables[0].Rows.Count; count++)
                            {
                                if (dsItem.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsItem.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                                {
                                    l.Add(dsItem.Tables[0].Rows[count]["ParItemNo"].ToString());
                                }
                            }
                            if (l.Count > 0)
                            {
                                result = idtic.CheckIncludeCenterCode(l, DestiOrgID);
                                if (!result)
                                {
                                    ZhiFang.Common.Log.Log.Error("CheckNReportItemCenter.ITEMNO.存在未对照的项目");
                                    for (int n = 0; n < l.Count; n++)
                                    {
                                        TestItemControl.ItemNo = l[n].Trim();
                                        TestItemControl.ControlLabNo = DestiOrgID;
                                        int count = idtic.GetTotalCount(TestItemControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的ParItemNo={0}的编号和实验室的未对照", TestItemControl.ItemNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("ItemCenter_ParItemNo实验室内项目编码为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                    case "GenderNo":
                        if (dsItem.Tables[0].Columns.Contains("GenderNo"))
                        {
                            for (int count = 0; count < dsItem.Tables[0].Rows.Count; count++)
                            {
                                if (dsItem.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsItem.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                                {
                                    ListStr.Add(dsItem.Tables[0].Rows[count]["GenderNo"].ToString());
                                }
                            }
                            if (ListStr.Count > 0)
                            {
                                result = idgtc.CheckIncludeCenterCode(ListStr, DestiOrgID);
                                if (!result)
                                {
                                    for (int n = 0; n < ListStr.Count; n++)
                                    {
                                        GenderType.GenderNo = Convert.ToInt32(ListStr[n].Trim());
                                        GenderType.LabCode = DestiOrgID;
                                        int count = idgtc.GetTotalCount(GenderType);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室的未对照", GenderType.GenderNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("ItemCenter_GenderNo实验室内性别编号为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }

        public bool CheckNReportItemLab(DataSet dsItem, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsItem.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    if (dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsItem.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeLabCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeControlNo = stringList[j].Trim();
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription = String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                    return false;
                }
            }
            if (dsItem.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsItem.Tables[0].Rows.Count; count++)
                {
                    if (dsItem.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsItem.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsItem.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeLabCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ControlItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内ParItemNo={0}的编号和中心的未对照", TestItemControl.ControlItemNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("CheckNReportItemLab实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
            if (dsItem.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsItem.Tables[0].Rows.Count; count++)
                {
                    if (dsItem.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsItem.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsItem.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeLabCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderControlNo = ListStr[n].Trim();
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内GenderNo={0}的编号和中心的未对照", GenderType.GenderControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region IBLLBase<NRequestItem> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public List<Model.NRequestItem> GetModelList(Model.NRequestItem t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion

        #region IBNRequestItem 成员


        public DataSet GetList_By_NRequestFormNo_ClientNo(long NRequestFormNo, string LabCode)
        {
            return dal.GetList_ClientNo(new ZhiFang.Model.NRequestItem() { NRequestFormNo = NRequestFormNo, WebLisSourceOrgID = LabCode });
        }

        #endregion

        public DataSet GetList_PKI(string strWhere)
        {
            return dal.GetList_PKI(strWhere);
        }
        public int UpdateByList(List<string> listStrColumnNi, List<string> listStrDataNi)
        {
            return dal.UpdateByList(listStrColumnNi, listStrDataNi);
        }

        public int AddByList(List<string> listStrColumnNi, List<string> listStrDataNi)
        {
            return dal.AddByList(listStrColumnNi, listStrDataNi);
        }

        public DataSet GetNrequestItemByNrequestNo(long nrequestFormNo)
        {
            return dal.GetNrequestItemByNrequestNo(nrequestFormNo);
        }
        IBbarCodeSeq ibcs = ZhiFang.BLLFactory.BLLFactory<IBbarCodeSeq>.GetBLL();




        /// <summary>
        /// 给NRequestItem和BarCodeForm对象赋值
        /// </summary>
        /// <param name="barcodelist"></param>
        /// <param name="weblisorgid"></param>
        /// <param name="nrequestForm"></param>
        /// <param name="webLisOrgID"></param>
        /// <param name="collecter"></param>
        /// <param name="emplID"></param>
        /// <param name="bcf_List"></param>
        /// <returns></returns>
        List<Model.NRequestItem> IBNRequestItem.SetNrequestItemAndBarCodeForm(List<UiBarCode> barcodelist, Model.NRequestForm nrequestForm, string collecter, int emplID, out List<Model.BarCodeForm> bcf_List)
        {
            IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
            IBTestItem ibti = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            Model.BarCodeForm bcf_m = new Model.BarCodeForm();
            List<Model.BarCodeForm> bcf_listTemp = new List<Model.BarCodeForm>();

            List<Model.NRequestItem> nri_List = new List<Model.NRequestItem>();
            Model.NRequestItem nri_m = new Model.NRequestItem();
            foreach (UiBarCode uibc in barcodelist)
            {
                long barCodeFormNo = 0;
                //BarCode
                string barCodeTemp = string.Empty;
                if (uibc.BarCode == "" || uibc.BarCode == null)
                {
                    barCodeTemp = ibcs.GetBarCode(nrequestForm.ClientNo, DateTime.Now.ToString());
                    barCodeFormNo = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                }
                else
                {
                    barCodeTemp = uibc.BarCode;
                    IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
                    DataSet ds = ibbcf.GetList(new Model.BarCodeForm() { BarCode = barCodeTemp });
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        long.TryParse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString(), out barCodeFormNo);
                    }
                    else
                        barCodeFormNo = 0;
                }

                if (barCodeFormNo == 0)
                {
                    ZhiFang.Common.Log.Log.Error("BarCode在BarCodeForm表中不存在");
                    continue;
                }

                bcf_m = new Model.BarCodeForm();

                foreach (string strItem in uibc.ItemList)
                {

                    #region 拼凑NrequestItem，将组套项目写入NI表
                    nri_m = new Model.NRequestItem();
                    nri_m.BarCodeFormNo = barCodeFormNo;
                    int CenterCombiItemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, strItem));
                    //转码
                    nri_m.CombiItemNo = CenterCombiItemNo.ToString();
                    nri_m.ParItemNo = CenterCombiItemNo.ToString();
                    nri_m.NRequestFormNo = nrequestForm.NRequestFormNo;
                    nri_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                    nri_m.WebLisSourceOrgID = nrequestForm.ClientNo;
                    nri_m.WebLisSourceOrgName = nrequestForm.ClientName;
                    nri_m.ClientNo = nrequestForm.ClientNo;
                    nri_m.ClientName = nrequestForm.ClientName;
                    Model.Lab_TestItem a = ltic.GetModel(nrequestForm.AreaNo, strItem);
                    if (a != null)
                        nri_m.Price = a.Price == null ? 0 : (decimal)a.Price;
                    //nri_List.Add(nri_m);在NI表不插入组套项目
                    #endregion

                    #region 拼凑BarCodeForm中的ItemNo和ItemName
                    Model.TestItem testItemModel = ibti.GetModel(CenterCombiItemNo.ToString());
                    if (testItemModel != null)
                    {
                        if (bcf_m.ItemNo == null)
                        {
                            bcf_m.ItemNo = testItemModel.ItemNo;
                            bcf_m.ItemName = testItemModel.CName;
                        }
                        else
                        {
                            bcf_m.ItemNo += "," + testItemModel.ItemNo;
                            bcf_m.ItemName += "," + testItemModel.CName;
                        }
                    }
                    #endregion
                }

                #region 组套中的子项插入NI表
                foreach (string strItem in uibc.ItemList)
                {
                    List<TestItemDetail> ttdList = new List<TestItemDetail>();
                    GetSubLabItem(strItem, nrequestForm.ClientNo, ref ttdList);

                    foreach (TestItemDetail tid in ttdList)
                    {

                        #region 拼凑NrequestItem
                        nri_m = new Model.NRequestItem();
                        nri_m.BarCodeFormNo = barCodeFormNo;
                        int CenterCombiItemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, strItem));
                        int centerParitemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, tid.ItemNo));
                        //转码
                        nri_m.CombiItemNo = CenterCombiItemNo.ToString();
                        nri_m.ParItemNo = centerParitemNo.ToString();
                        nri_m.NRequestFormNo = nrequestForm.NRequestFormNo;
                        nri_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                        nri_m.WebLisSourceOrgID = nrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = nrequestForm.ClientName;
                        nri_m.ClientNo = nrequestForm.ClientNo;
                        nri_m.ClientName = nrequestForm.ClientName;
                        Model.Lab_TestItem a = ltic.GetModel(nrequestForm.AreaNo, tid.ItemNo);
                        if (a != null)
                            nri_m.Price = a.Price == null ? 0 : (decimal)a.Price;
                        nri_List.Add(nri_m);
                        #endregion
                    }
                }
                #endregion


                #region 拼凑BarCodeForm
                bcf_m.BarCode = barCodeTemp;
                bcf_m.BarCodeFormNo = barCodeFormNo;
                int sampleTypeNo;
                int.TryParse(uibc.SampleType, out sampleTypeNo);
                bcf_m.SampleTypeNo = sampleTypeNo;
                bcf_m.SampleTypeName = uibc.SampleTypeName;
                bcf_m.Color = uibc.ColorName;
                bcf_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                bcf_m.WebLisSourceOrgId = nrequestForm.ClientNo;
                bcf_m.WebLisSourceOrgName = nrequestForm.ClientName;
                bcf_m.ClientNo = nrequestForm.ClientNo;
                bcf_m.ClientName = nrequestForm.ClientName;
                bcf_m.CollectDate = nrequestForm.CollectDate;
                bcf_m.CollectTime = nrequestForm.CollectTime;
                bcf_m.Collecter = collecter;
                bcf_m.CollecterID = emplID;
                bcf_m.PrintCount = 0;
                bcf_m.IsAffirm = 1;
                bcf_listTemp.Add(bcf_m);
                #endregion

            }
            bcf_List = bcf_listTemp;
            return nri_List;
        }

        /// <summary>
        /// 十堰太和定制 新增申请单，组套项目不需要对照，
        /// </summary>
        /// <param name="barcodelist"></param>
        /// <param name="nrequestForm"></param>
        /// <param name="collecter"></param>
        /// <param name="emplID"></param>
        /// <param name="bcf_List"></param>
        /// <returns></returns>
        List<Model.NRequestItem> IBNRequestItem.SetNrequestItemAndBarCodeForm_TaiHe(List<UiBarCode> barcodelist, Model.NRequestForm nrequestForm, string collecter, int emplID, out List<Model.BarCodeForm> bcf_List)
        {
            IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
            IBTestItem ibti = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            Model.BarCodeForm bcf_m = new Model.BarCodeForm();
            List<Model.BarCodeForm> bcf_listTemp = new List<Model.BarCodeForm>();

            List<Model.NRequestItem> nri_List = new List<Model.NRequestItem>();
            Model.NRequestItem nri_m = new Model.NRequestItem();
            foreach (UiBarCode uibc in barcodelist)
            {
                long barCodeFormNo = 0;
                //BarCode
                string barCodeTemp = string.Empty;
                if (uibc.BarCode == "" || uibc.BarCode == null)
                {
                    barCodeTemp = ibcs.GetBarCode(nrequestForm.ClientNo, DateTime.Now.ToString());
                    ZhiFang.Common.Log.Log.Debug("SetNrequestItemAndBarCodeForm_TaiHe.barCodeTemp:" + barCodeTemp);
                    barCodeFormNo = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                }
                else
                {
                    barCodeTemp = uibc.BarCode;
                    IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
                    DataSet ds = ibbcf.GetList(new Model.BarCodeForm() { BarCode = barCodeTemp });
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        long.TryParse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString(), out barCodeFormNo);
                    }
                    else
                        barCodeFormNo = 0;
                }

                if (barCodeFormNo == 0)
                {
                    ZhiFang.Common.Log.Log.Error("BarCode在BarCodeForm表中不存在");
                    continue;
                }

                bcf_m = new Model.BarCodeForm();

                foreach (string strItem in uibc.ItemList)
                {

                    #region 拼凑NrequestItem
                    nri_m = new Model.NRequestItem();
                    nri_m.BarCodeFormNo = barCodeFormNo;
                    //组套项目不需要对照

                    nri_m.LabCombiItemNo = int.Parse(strItem);
                    nri_m.LabParItemNo = strItem;
                    nri_m.NRequestFormNo = nrequestForm.NRequestFormNo;
                    nri_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                    nri_m.WebLisSourceOrgID = nrequestForm.ClientNo;
                    nri_m.WebLisSourceOrgName = nrequestForm.ClientName;
                    nri_m.ClientNo = nrequestForm.ClientNo;
                    nri_m.ClientName = nrequestForm.ClientName;
                    Model.Lab_TestItem Lab_CombiItem = ltic.GetModel(nrequestForm.AreaNo, strItem);
                    if (Lab_CombiItem != null)
                    {
                        nri_m.Price = Lab_CombiItem.Price == null ? 0 : (decimal)Lab_CombiItem.Price;
                        
                    }
                    int centerCombiItemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, strItem));
                    nri_m.CombiItemNo = centerCombiItemNo.ToString();
                    //nri_List.Add(nri_m);//Nrequestitem不保存组套项目记录
                    #endregion

                    #region 拼凑BarCodeForm中的ItemNo和ItemName

                    //拼凑BarCodeForm中的LabItemNo和LabItemName
                    if (bcf_m.LabItemNo == null)
                    {
                        bcf_m.LabItemNo = strItem;
                        bcf_m.LabItemName = Lab_CombiItem.CName;
                    }
                    else
                    {
                        bcf_m.LabItemNo += "," + strItem;
                        bcf_m.LabItemName += "," + Lab_CombiItem.CName;
                    }

                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeFormItemIsCombiItem") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeFormItemIsCombiItem").Trim() == "1")
                    {
                        if (bcf_m.ItemNo == null)
                        {
                            bcf_m.ItemNo = strItem;
                            bcf_m.ItemName = Lab_CombiItem.CName;
                        }
                        else
                        {
                            bcf_m.ItemNo += "," + strItem;
                            bcf_m.ItemName += "," + Lab_CombiItem.CName;
                        }
                    }
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestItemSaveCombiItem") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestItemSaveCombiItem").Trim() == "1")
                    {
                        nri_List.Add(nri_m);
                    }
                    #endregion
                }

                #region 组套中的子项插入NI表
                foreach (string strItem in uibc.ItemList)
                {
                    List<TestItemDetail> ttdList = new List<TestItemDetail>();
                    GetSubLabItem(strItem, nrequestForm.ClientNo, ref ttdList);
                    Model.Lab_TestItem Lab_CombiItem = ltic.GetModel(nrequestForm.AreaNo, strItem);
                    if (Lab_CombiItem != null)
                        nri_m.Price = Lab_CombiItem.Price == null ? 0 : (decimal)Lab_CombiItem.Price;
                    int centerCombiItemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, strItem));
                    foreach (TestItemDetail tid in ttdList)
                    {

                        #region 拼凑NrequestItem
                        nri_m = new Model.NRequestItem();
                        nri_m.BarCodeFormNo = barCodeFormNo;
                        // int CenterCombiItemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, strItem));
                        int centerParitemNo = int.Parse(tic.GetCenterNo(nrequestForm.ClientNo, tid.ItemNo));

                        nri_m.ParItemNo = centerParitemNo.ToString();
                        nri_m.CombiItemNo = centerCombiItemNo.ToString();
                        nri_m.LabCombiItemNo = int.Parse(strItem);
                        nri_m.LabParItemNo = tid.ItemNo;
                        nri_m.NRequestFormNo = nrequestForm.NRequestFormNo;
                        nri_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                        nri_m.WebLisSourceOrgID = nrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = nrequestForm.ClientName;
                        nri_m.ClientNo = nrequestForm.ClientNo;
                        nri_m.ClientName = nrequestForm.ClientName;
                        Model.Lab_TestItem a = ltic.GetModel(nrequestForm.AreaNo, tid.ItemNo);
                        if (a != null)
                            nri_m.Price = a.Price == null ? 0 : (decimal)a.Price;
                        nri_List.Add(nri_m);

                        //凑BarCodeForm中的ItemNo和ItemName
                        Model.TestItem testItemModel = ibti.GetModel(centerParitemNo.ToString());
                        if (testItemModel != null)
                        {
                            //判断barcodeform保存组套还是细项
                            if (!(ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeFormItemIsCombiItem") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeFormItemIsCombiItem").Trim() == "1"))
                            {
                                if (bcf_m.ItemNo == null)
                                {
                                    bcf_m.ItemNo = testItemModel.ItemNo;
                                    bcf_m.ItemName = testItemModel.CName;
                                }
                                else
                                {
                                    bcf_m.ItemNo += "," + testItemModel.ItemNo;
                                    bcf_m.ItemName += "," + testItemModel.CName;
                                }
                            }
                        }

                        //拼凑BarCodeForm中的LabItemNo和LabItemName
                        if (bcf_m.LabItemNo == null)
                        {
                            bcf_m.LabItemNo = tid.ItemNo;
                            bcf_m.LabItemName = tid.CName;
                        }
                        else
                        {
                            bcf_m.LabItemNo += "," + tid.ItemNo;
                            bcf_m.LabItemName += "," + tid.CName;
                        }
                        #endregion
                    }
                }
                #endregion


                #region 拼凑BarCodeForm
                bcf_m.BarCode = barCodeTemp;
                bcf_m.BarCodeFormNo = barCodeFormNo;
                int sampleTypeNo;
                int.TryParse(uibc.SampleType, out sampleTypeNo);
                bcf_m.SampleTypeNo = sampleTypeNo;
                bcf_m.SampleTypeName = uibc.SampleTypeName;
                bcf_m.Color = uibc.ColorName;
                bcf_m.WebLisOrgID = nrequestForm.WebLisOrgID;
                bcf_m.WebLisSourceOrgId = nrequestForm.ClientNo;
                bcf_m.WebLisSourceOrgName = nrequestForm.ClientName;
                bcf_m.ClientNo = nrequestForm.ClientNo;
                bcf_m.ClientName = nrequestForm.ClientName;
                bcf_m.CollectDate = nrequestForm.CollectDate;
                bcf_m.CollectTime = nrequestForm.CollectTime;
                bcf_m.Collecter = collecter;
                bcf_m.CollecterID = emplID;
                bcf_m.PrintCount = 0;
                bcf_m.IsAffirm = 1;
                bcf_listTemp.Add(bcf_m);
                #endregion

            }
            bcf_List = bcf_listTemp;
            return nri_List;
        }

        public void GetSubLabItem(string itemid, string labcode, ref List<TestItemDetail> listtestitem)
        {
            try
            {
                IBLL.Common.BaseDictionary.IBLab_GroupItem LabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
                IBLL.Common.BaseDictionary.IBGroupItem CenterGroupItem = BLLFactory<IBGroupItem>.GetBLL();
                IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
                DataSet dsitem = null;
                if (labcode != null && labcode != "")
                {
                    dsitem = LabGroupItem.GetGroupItemList(itemid, labcode);
                }
                else
                    dsitem = CenterGroupItem.GetGroupItemList(itemid);

                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsitem.Tables[0].Rows.Count; i++)
                    {
                        GetSubLabItem(dsitem.Tables[0].Rows[i]["ItemNo"].ToString(), labcode, ref listtestitem);
                        TestItemDetail ttd = new TestItemDetail();
                        ttd.CName = dsitem.Tables[0].Rows[i]["CName"].ToString();
                        ttd.ItemNo = dsitem.Tables[0].Rows[i]["ItemNo"].ToString();
                        ttd.EName = dsitem.Tables[0].Rows[i]["EName"].ToString();
                        ttd.ColorName = dsitem.Tables[0].Rows[i]["Color"].ToString();
                        ttd.Prices = dsitem.Tables[0].Rows[i]["price"].ToString();
                        if (ttd.ColorName != "")
                        {
                            var aa = icd.GetModelByColorName(ttd.ColorName);
                            if (aa != null)
                                ttd.ColorValue = aa.ColorValue; //ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].ColorValue;
                            //List<SampleType> listsampletype = ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].SampleType;
                            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();
                            foreach (ZhiFang.Model.SampleType sampletype in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(ttd.ColorName)) //ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].SampleType)
                            {
                                SampleTypeDetail sampleDetail = new SampleTypeDetail();
                                sampleDetail.CName = sampletype.CName;
                                sampleDetail.SampleTypeID = sampletype.SampleTypeID.ToString();
                                sampleDetailList.Add(sampleDetail);
                            }
                            ttd.SampleTypeDetail = sampleDetailList;
                        }
                        listtestitem.Add(ttd);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        bool IBNRequestItem.AddNrequestItem(List<Model.NRequestItem> nri_List)
        {
            bool result = false;

            foreach (Model.NRequestItem nri in nri_List)
            {
                int i = this.Add(nri);
                if (i > 0)
                {
                    result = true;
                }
                else
                    result = false;
            }
            return result;
        }

        bool IBNRequestItem.AddNrequestItem_TaiHe(List<Model.NRequestItem> nri_List)
        {
            bool result = false;

            foreach (Model.NRequestItem nri in nri_List)
            {
                int i = this.Add_TaiHe(nri);
                if (i > 0)
                {
                    result = true;
                }
                else
                    result = false;
            }
            return result;
        }

        bool IBNRequestItem.UpdateNrequestItem(List<Model.NRequestItem> nri_List, long nRequestFormNo)
        {
            bool result = false;
            //先删除
            this.DeleteList_ByNRequestFormNo(nRequestFormNo);
            foreach (Model.NRequestItem nri in nri_List)
            {
                int i = this.Add(nri);
                if (i > 0)
                {
                    result = true;
                }
                else
                    result = false;
            }
            return result;
        }

        bool IBNRequestItem.UpdateNrequestItem_TaiHe(List<Model.NRequestItem> nri_List, long nRequestFormNo)
        {
            bool result = false;
            //先删除
            this.DeleteList_ByNRequestFormNo(nRequestFormNo);
            foreach (Model.NRequestItem nri in nri_List)
            {
                int i = this.Add_TaiHe(nri);
                // int i = this.Update_TaiHe(nri);
                if (i > 0)
                {
                    result = true;
                }
                else
                    result = false;
            }
            return result;
        }

        List<UiCombiItem> IBNRequestItem.GetUiCombiItemByNrequestForm(Model.NRequestForm nrf_m)
        {
            List<UiCombiItem> uiCombiItemList = new List<UiCombiItem>();
            UiCombiItem uiCombiItem = null;

            List<Model.NRequestItem> nrequestItemList = this.GetModelList(new Model.NRequestItem() { NRequestFormNo = nrf_m.NRequestFormNo });
            IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
            IBCLIENTELE bclientele = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL("BaseDictionary.CLIENTELE");
            IBClientEleArea bclientelearea = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBClientEleArea>.GetBLL("BaseDictionary.ClientEleArea");
            //var client = bclientele.GetModel(long.Parse(nrf_m.ClientNo));
            //int areaclientno = bclientelearea.GetModel(client.AreaID.Value).ClientNo.Value;
            //nrf_m.AreaNo = areaclientno.ToString();
            nrf_m.AreaNo = nrf_m.ClientNo;
            //DataSet dsitemmap = tic.GetLabItemCodeMapListByNRequestLabCodeAndFormNo(areaclientno.ToString(), nrf_m.NRequestFormNo.ToString());
            DataSet dsitemmap = tic.GetLabItemCodeMapListByNRequestLabCodeAndFormNo(nrf_m.ClientNo, nrf_m.NRequestFormNo.ToString());
            Dictionary<string, List<string>> combiItemNoTempList = new Dictionary<string, List<string>>();
            //NRequestItem中包含多个combiItemNo
            foreach (var requestItem1 in nrequestItemList)
            {
                int combiItemNo = -1;
                string combiItemName = "";
                string itemPrice = "";
                string itemColorName = "";
                string itemColorValue = "";
                if (dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").Count() > 0)
                {
                    combiItemNo = int.Parse(dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["ControlItemNo"].ToString());
                    combiItemName = dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["CName"].ToString();
                    itemPrice = dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["price"].ToString();
                    itemColorName = dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["Color"].ToString();
                    itemColorValue = dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["ColorValue"].ToString();
                }

                //用于记录nrequestItemList的combiItemNo
                if (!combiItemNoTempList.Keys.Contains(combiItemNo.ToString()))
                {
                    combiItemNoTempList.Add(combiItemNo.ToString(),new List<string> { combiItemName, itemPrice, itemColorName,itemColorValue });
                }
            }

            //拼接契约所需格式
            for (int i = 0; i < combiItemNoTempList.Count; i++)
            {
                uiCombiItem = new UiCombiItem();
                uiCombiItem.CombiItemNo = combiItemNoTempList.ElementAt(i).Key;
                var combiinfo= combiItemNoTempList.ElementAt(i).Value;
                if (combiinfo!=null )
                {
                    uiCombiItem.CombiItemName = combiinfo[0];//combiItemNoTempList.ElementAt(i).Value;
                    uiCombiItem.Prices = combiinfo[1];
                    uiCombiItem.ColorName= combiinfo[2];
                    uiCombiItem.ColorValue = combiinfo[3];
                }
                uiCombiItemList.Add(uiCombiItem);
            }



            return uiCombiItemList;
        }

      
        List<UiCombiItem> IBNRequestItem.GetUiCombiItemByNrequestForm_TaiHe(Model.NRequestForm nrf_m)
        {
            List<UiCombiItem> uiCombiItemList = new List<UiCombiItem>();
            UiCombiItem uiCombiItem = null;
            long NRequestFormNo = 0;
            NRequestFormNo = (long)(nrf_m.NRequestFormNo);
            DataSet dsCombiItem = new DataSet();
            dsCombiItem = dal.GetCombiItemByNrequestFormNo(NRequestFormNo);


            //拼接契约所需格式
            if (dsCombiItem != null && dsCombiItem.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsCombiItem.Tables[0].Rows.Count; i++)
                {
                    uiCombiItem = new UiCombiItem();
                    uiCombiItem.CombiItemNo = dsCombiItem.Tables[0].Rows[i]["LabCombiItemNo"].ToString();
                    uiCombiItem.CombiItemName = dsCombiItem.Tables[0].Rows[i]["CName"].ToString();
                    uiCombiItem.Prices = dsCombiItem.Tables[0].Rows[i]["SumPrice"].ToString();

                    uiCombiItemList.Add(uiCombiItem);
                }
            }

            return uiCombiItemList;
        }

        public DataSet GetNrequestItemByBarCodeFormNo(string BarCodeFormNo)
        {
            return dal.GetNrequestItemByBarCodeFormNo(BarCodeFormNo);
        }
        public int Add(string strSql)
        {
            return dal.Add(strSql);
        }
        public DataSet GetRefuseList(string strSql)
        {
            return dal.GetRefuseList(strSql);
        }
    }
}
