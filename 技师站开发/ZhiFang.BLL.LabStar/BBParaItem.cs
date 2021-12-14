using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBParaItem : BaseBLL<BParaItem>, ZhiFang.IBLL.LabStar.IBBParaItem
    {
        ZhiFang.IBLL.LabStar.IBBPara IBBPara { get; set; }
        IDBHostTypeUserDao IDBHostTypeUserDao { get; set; }

        public BaseResultDataValue AddAndEditParaItem(string objectInfo, IList<BParaItem> listParaItem, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (objectInfo != null && objectInfo.Trim().Length > 0)
            {
                JArray jsonArray = JArray.Parse(objectInfo);
                if (jsonArray != null && jsonArray.Count > 0)
                {
                    foreach (JObject jsonObject in jsonArray)
                    {
                        if (jsonObject != null)
                        {
                            string objectID = jsonObject["ObjectID"] != null ? jsonObject["ObjectID"].ToString() : "";
                            string objectName = jsonObject["ObjectName"] != null ? jsonObject["ObjectName"].ToString() : "";
                            baseResultDataValue = AddAndEditParaItem(listParaItem, objectID, objectName, operaterID, operater);
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddAndEditParaItem(IList<BParaItem> listParaItem, string objectID, string objectName, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            foreach (BParaItem paraItemEntity in listParaItem)
            {
                BParaItem entity = null;
                if (paraItemEntity.BPara == null)
                    continue;
                IList<BParaItem> list = DBDao.GetListByHQL(" bparaitem.ObjectID=" + objectID + " and bparaitem.BPara.Id=" + paraItemEntity.BPara.Id);
                if (list != null && list.Count > 0)
                    entity = list[0];
                if (entity == null)
                {
                    BParaItem addEntity = ClassMapperHelp.GetMapper<BParaItem, BParaItem>(paraItemEntity);
                    addEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    if (objectID != null && objectID.Trim().Length > 0)
                        addEntity.ObjectID = long.Parse(objectID);
                    addEntity.ObjectName = objectName;
                    if (operaterID != null && operaterID.Trim().Length > 0)
                        addEntity.OperaterID = long.Parse(operaterID);
                    addEntity.Operater = operater;
                    addEntity.DataUpdateTime = DateTime.Now;
                    addEntity.IsUse = true;
                   
                    baseResultDataValue.success = DBDao.Save(addEntity);
                }
                else if (entity.ParaValue != paraItemEntity.ParaValue)
                {
                    if (operaterID != null && operaterID.Trim().Length > 0)
                        entity.OperaterID = long.Parse(operaterID);
                    entity.Operater = operater;
                    entity.DataUpdateTime = DateTime.Now;
                    //if (objectID != null && objectID.Trim().Length > 0)
                    //{
                    //    entity.ObjectID = long.Parse(objectID);
                    //    entity.ObjectName = objectName;
                    //}
                    //else
                    //{
                    //    entity.ObjectID = paraItemEntity.ObjectID;
                    //    entity.ObjectName = paraItemEntity.ObjectName;
                    //}
                    //entity.BPara = paraItemEntity.BPara;
                    entity.ParaValue = paraItemEntity.ParaValue;
                    entity.IsUse = true;
                    baseResultDataValue.success = DBDao.Update(entity);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddCopyParaItemByObjectID(string fromObjectID, string toObjectID, string toObjectName, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (fromObjectID != null && fromObjectID.Trim().Length > 0 && toObjectID != null && toObjectID.Trim().Length > 0)
            {
                IList<BParaItem> listFormParaItem = DBDao.GetListByHQL(" bparaitem.ObjectID=" + fromObjectID);
                if (listFormParaItem != null && listFormParaItem.Count > 0)
                {
                    int delCount = DBDao.DeleteByHql(" from BParaItem bparaitem where bparaitem.ObjectID=" + toObjectID);
                    foreach (BParaItem paraItemEntity in listFormParaItem)
                    {
                        BParaItem addEntity = ClassMapperHelp.GetMapper<BParaItem, BParaItem>(paraItemEntity);
                        addEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        addEntity.ObjectID = long.Parse(toObjectID);
                        addEntity.ObjectName = toObjectName;
                        if (operaterID != null && operaterID.Trim().Length > 0)
                            addEntity.OperaterID = long.Parse(operaterID);
                        addEntity.Operater = operater;
                        addEntity.DataUpdateTime = DateTime.Now;
                        addEntity.IsUse = true;
                        baseResultDataValue.success = DBDao.Save(addEntity);
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditParaItemDefaultValueByObjectID(string objectID, string paraTypeCode, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (objectID != null && objectID.Trim().Length > 0)
            {
                IList<BParaItem> listFormParaItem = DBDao.GetListByHQL(" bparaitem.ObjectID=" + objectID);
                if (listFormParaItem != null && listFormParaItem.Count > 0)
                {
                    IList<BPara> listPara = IBBPara.GetSystemDefaultPara(paraTypeCode, operaterID, operater);
                    foreach (BParaItem paraItemEntity in listFormParaItem)
                    {
                        IList<BPara> list = listPara.Where(p => p.Id == paraItemEntity.BPara.Id).ToList();
                        if (list != null && list.Count > 0)
                        {
                            paraItemEntity.ParaValue = list[0].ParaValue;
                            if (operaterID != null && operaterID.Trim().Length > 0)
                                paraItemEntity.OperaterID = long.Parse(operaterID);
                            paraItemEntity.Operater = operater;
                            paraItemEntity.IsUse = true;
                            baseResultDataValue.success = DBDao.Update(paraItemEntity);
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 删除系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息，格式："[{\"ObjectID\":\"1\",\"ObjectName\":\"测试1\"},{\"ObjectID\":\"2\",\"ObjectName\":\"测试2\"}]"</param>
        /// <returns></returns>
        public BaseResultDataValue DeleteParaItemByObjectID(string objectInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (objectInfo != null && objectInfo.Trim().Length > 0)
            {
                int delCount = 0;
                JArray jsonArray = JArray.Parse(objectInfo);
                if (jsonArray != null && jsonArray.Count > 0)
                {
                    foreach (JObject jsonObject in jsonArray)
                    {
                        if (jsonObject != null)
                        {
                            string objectID = jsonObject["ObjectID"] != null ? jsonObject["ObjectID"].ToString() : "";
                            string objectName = jsonObject["ObjectName"] != null ? jsonObject["ObjectName"].ToString() : "";
                            if (objectID != null && objectID.Trim().Length > 0)
                                delCount = DBDao.DeleteByHql(" from BParaItem bparaitem where bparaitem.ObjectID=" + objectID);
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemTypeCode"></param>
        /// <param name="paraTypeCode"></param>
        /// <returns></returns>
        public IList<object> QueryParaSystemTypeInfo(string systemTypeCode, string paraTypeCode)
        {
            return (DBDao as IDBParaItemDao).QueryParaSystemTypeInfoDao(systemTypeCode, paraTypeCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="systemTypeCode"></param>
        /// <param name="paraTypeCode"></param>
        /// <returns></returns>
        public IList<BParaItem> QuerySystemParaItem(string where, string systemTypeCode, string paraTypeCode)
        {
            string strHqlWhere = " bpara.TypeCode=\'" + paraTypeCode + "\'" + " and bpara.SystemCode=\'" + systemTypeCode + "\'";
            if (where != null && where.Trim().Length > 0)
                strHqlWhere += " and " + where;
            return (DBDao as IDBParaItemDao).QuerySystemParaItemDao(strHqlWhere);
        }

        /// <summary>
        /// 根据参数编码查询查找对应的参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <returns></returns>
        public BPara QueryParaValueByParaNo(string paraNo, string objectID)
        {
            BPara para = (DBDao as IDBParaItemDao).QueryParaValueByParaNo(paraNo, objectID);
            if (para == null)
                para = IBBPara.QuerySystemDefaultParaValueByParaNo(paraNo);
            else
                para.ParaSource = 2;
            return para;
        }

        /// <summary>
        /// 根据参数分类编码和参数相关性对象ID查找参数值
        /// </summary>
        /// <param name="paraTypeCode">参数分类编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="operaterID">操作人ID</param>
        /// <param name="operater">操作人</param>
        /// <returns></returns>
        public IList<BPara> QueryParaValueByParaTypeCode(string paraTypeCode, string objectID, string operaterID, string operater)
        {
            IList<BPara> returnList = new List<BPara>();
            string[] arrayTypeCode = paraTypeCode.Split(',');
            string typeCodeHQL = "";
            foreach (string typeCode in arrayTypeCode)
            {
                if (typeCodeHQL == "")
                    typeCodeHQL = " bpara.TypeCode=\'" + typeCode + "\'";
                else
                    typeCodeHQL += " or bpara.TypeCode=\'" + typeCode + "\'";
            }
            string strHqlWhere = " bparaitem.ObjectID=" + objectID;
            if (typeCodeHQL != null && typeCodeHQL.Trim().Length > 0)
                strHqlWhere += " and (" + typeCodeHQL + ")";

            IList<BParaItem> paraItemList = (DBDao as IDBParaItemDao).QuerySystemParaItemDao(strHqlWhere);
            IList<BPara> paraList = IBBPara.GetSystemDefaultPara(paraTypeCode, operaterID, operater);
            if (paraItemList == null || paraItemList.Count == 0)
                returnList = paraList;
            else if (paraList != null && paraList.Count > 0)
            {
                foreach (BParaItem paraItem in paraItemList)
                {
                    IList<BPara> tempList = paraList.Where(p => p.ParaNo == paraItem.BPara.ParaNo).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        tempList[0].ParaValue = paraItem.ParaValue;
                        tempList[0].ParaSource = 2;
                    }
                }
                returnList = paraList;
            }
            return returnList;
        }


        /// <summary>
        /// 根据参数分类编码和参数相关性对象ID查找参数值
        /// </summary>
        /// <param name="where">HQL查询条件</param>
        /// <param name="paraTypeCode">参数分类编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="operaterID">操作人ID</param>
        /// <param name="operater">操作人</param>
        /// <returns></returns>
        public IList<BPara> QueryParaValueByParaTypeCode(string where, string paraTypeCode, string objectID, string operaterID, string operater)
        {
            IList<BPara> returnList = new List<BPara>();
            string strHqlWhere = " 1=1 ";
            if (where != null && where.Trim().Length > 0)
                strHqlWhere = where;
            string[] arrayTypeCode = paraTypeCode.Split(',');
            string typeCodeHQL = "";
            foreach (string typeCode in arrayTypeCode)
            {
                if (typeCodeHQL == "")
                    typeCodeHQL = " bpara.TypeCode=\'" + typeCode + "\'";
                else
                    typeCodeHQL += " or bpara.TypeCode=\'" + typeCode + "\'";
            }
            strHqlWhere += " and bparaitem.ObjectID=" + objectID;
            if (typeCodeHQL != null && typeCodeHQL.Trim().Length > 0)
                strHqlWhere += " and (" + typeCodeHQL + ")";
            IList<BParaItem> paraItemList = (DBDao as IDBParaItemDao).QuerySystemParaItemDao(strHqlWhere);
            IList<BPara> paraList = IBBPara.GetSystemDefaultPara(paraTypeCode, operaterID, operater);
            if (paraItemList == null || paraItemList.Count == 0)
                returnList = paraList;
            else if (paraList != null && paraList.Count > 0)
            {
                foreach (BParaItem paraItem in paraItemList)
                {
                    IList<BPara> tempList = paraList.Where(p => p.ParaNo == paraItem.BPara.ParaNo).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        tempList[0].ParaValue = paraItem.ParaValue;
                        tempList[0].ParaSource = 2;
                    }
                }
                returnList = paraList;
            }
            return returnList;
        }

        /// <summary>
        /// 获取历史对比参数
        /// </summary>
        /// <param name="sectionID">参数相关性ID---此方法应该填写小组ID</param>
        /// <param name="systemTypeCode">参数相关性编码</param>
        /// <param name="paraTypeCode">参数分类编码</param>
        /// <param name="operaterID">操作人ID</param>
        /// <param name="operater">操作人</param>
        /// <returns></returns>
        public ItemHistoryComparePara GetItemHistoryComparePara(string objectID, string operaterID, string operater)
        {
            string paraTypeCode = "NTestType_ItemResult_HistoryCompare_Para";
            ItemHistoryComparePara itemHistoryComparePara;
            //是否进行历史对比
            itemHistoryComparePara.IsHistoryCompare = true;
            //历史对比小组范围
            itemHistoryComparePara.HistoryCompareSection = "";
            //对比日期范围
            itemHistoryComparePara.HistoryCompareDays = 90;
            //是否区分样本类型
            itemHistoryComparePara.IsCompareSampleType = false;
            //历史对比查询字段列表
            itemHistoryComparePara.HistoryCompareField = "PatNo";
            //需对比的历史结果记录数
            itemHistoryComparePara.CompareRecordCount = 3;
            //历史对比百分比小数位数
            itemHistoryComparePara.DecimalBit = 1;
            //历史对比超高低百分比参考值"
            itemHistoryComparePara.DiffValueHH = 50;
            //历史对比高低百分比参考值
            itemHistoryComparePara.DiffValueH = 30;
            //历史对比超高百分比符号
            itemHistoryComparePara.DiffValueHHFalg = "▲";
            //历史对比超低百分比符号
            itemHistoryComparePara.DiffValueLLFalg = "▼";
            //历史对比高百分比符号
            itemHistoryComparePara.DiffValueHFalg = "↑";
            //历史对比低百分比符号
            itemHistoryComparePara.DiffValueLFalg = "↓";

            IList<BPara> listPara = QueryParaValueByParaTypeCode(paraTypeCode, objectID, operaterID, operater);
            if (listPara != null && listPara.Count > 0)
            {
                IList<BPara> tempList = null;
                //是否进行历史对比
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0001").ToList();
                itemHistoryComparePara.IsHistoryCompare = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue == "1") : true;
                //对比日期范围
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0011").ToList();
                itemHistoryComparePara.HistoryCompareDays = (tempList != null && tempList.Count > 0) ? (int.Parse(tempList[0].ParaValue)) : 90;
                //历史对比小组范围
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0012").ToList();
                itemHistoryComparePara.HistoryCompareSection = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue) : "";
                //是否区分样本类型
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0013").ToList();
                itemHistoryComparePara.IsCompareSampleType = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue == "1") : true;
                //历史对比查询字段列表
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0014").ToList();
                itemHistoryComparePara.HistoryCompareField = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue) : "";

                //历史对比高低百分比参考值
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0021").ToList();
                itemHistoryComparePara.DiffValueH = (tempList != null && tempList.Count > 0) ? (double.Parse(tempList[0].ParaValue)) : 0;
                //历史对比高低百分比符号
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0022").ToList();
                string strDiffValueH = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue) : "";
                if (strDiffValueH != null && strDiffValueH.Trim().Length > 0)
                {
                    string[] tempArray = strDiffValueH.Split('|');
                    itemHistoryComparePara.DiffValueHFalg = tempArray[0];
                    itemHistoryComparePara.DiffValueLFalg = tempArray.Length > 1 ? tempArray[1] : "";
                }

                //历史对比超高低百分比参考值
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0025").ToList();
                itemHistoryComparePara.DiffValueHH = (tempList != null && tempList.Count > 0) ? (double.Parse(tempList[0].ParaValue)) : 0;
                //历史对比超高低百分比符号
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0026").ToList();
                string strDiffValueHH = (tempList != null && tempList.Count > 0) ? (tempList[0].ParaValue) : "";
                if (strDiffValueHH != null && strDiffValueHH.Trim().Length > 0)
                {
                    string[] tempArray = strDiffValueHH.Split('|');
                    itemHistoryComparePara.DiffValueHHFalg = tempArray[0];
                    itemHistoryComparePara.DiffValueLLFalg = tempArray.Length > 1 ? tempArray[1] : "";
                }
                //历史对比百分比小数位数
                tempList = listPara.Where(p => p.ParaNo == "NTestType_ItemResult_HistoryCompare_0029").ToList();
                itemHistoryComparePara.DecimalBit = (tempList != null && tempList.Count > 0) ? (int.Parse(tempList[0].ParaValue)) : 0;
            }
            //对比日期范围
            itemHistoryComparePara.HistoryCompareDays = itemHistoryComparePara.HistoryCompareDays > 1 ? itemHistoryComparePara.HistoryCompareDays : 90;
            //历史对比查询字段列表
            itemHistoryComparePara.HistoryCompareField = (!string.IsNullOrEmpty(itemHistoryComparePara.HistoryCompareField)) ? itemHistoryComparePara.HistoryCompareField : "PatNo";
            //历史对比百分比小数位数
            itemHistoryComparePara.DecimalBit = itemHistoryComparePara.DecimalBit > 0 ? itemHistoryComparePara.DecimalBit : 1;
            //历史对比超高低百分比参考值"
            itemHistoryComparePara.DiffValueHH = itemHistoryComparePara.DiffValueHH > 0 ? itemHistoryComparePara.DiffValueHH : 50;
            //历史对比高低百分比参考值
            itemHistoryComparePara.DiffValueH = itemHistoryComparePara.DiffValueH > 0 ? itemHistoryComparePara.DiffValueH : 25;
            //历史对比超高百分比符号
            itemHistoryComparePara.DiffValueHHFalg = (!string.IsNullOrEmpty(itemHistoryComparePara.DiffValueHHFalg)) ? itemHistoryComparePara.DiffValueHHFalg : "▲";
            //历史对比超低百分比符号
            itemHistoryComparePara.DiffValueLLFalg = (!string.IsNullOrEmpty(itemHistoryComparePara.DiffValueLLFalg)) ? itemHistoryComparePara.DiffValueLLFalg : "▼";
            //历史对比高百分比符号
            itemHistoryComparePara.DiffValueHFalg = (!string.IsNullOrEmpty(itemHistoryComparePara.DiffValueHFalg)) ? itemHistoryComparePara.DiffValueHFalg : "↑";
            //历史对比低百分比符号
            itemHistoryComparePara.DiffValueLFalg = (!string.IsNullOrEmpty(itemHistoryComparePara.DiffValueLFalg)) ? itemHistoryComparePara.DiffValueLFalg : "↓";
            return itemHistoryComparePara;
        }

        public BaseResultDataValue PreEditParSystemParaItem(string ObjectID, List<BParaItem> entityList, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (string.IsNullOrEmpty(ObjectID))
            {
                baseResultDataValue.ErrorInfo = "参数错误：ObjectID不可为空！";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            DBDao.DeleteByHql(" From BParaItem bparaitem where bparaitem.ObjectID=" + long.Parse(ObjectID));
            if (entityList != null && entityList.Count > 0) {
                foreach (var bParaItem in entityList)
                {
                    BParaItem addEntity = ClassMapperHelp.GetMapper<BParaItem, BParaItem>(bParaItem);
                    addEntity.Operater = System.Uri.UnescapeDataString(operater);
                    if (operaterID != null && operaterID.Trim().Length > 0)
                        addEntity.OperaterID = long.Parse(operaterID);
                    addEntity.DataAddTime = DateTime.Now;
                    addEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    addEntity.IsUse = true;
                    baseResultDataValue.success = DBDao.Save(addEntity);
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 参数获取
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="paranos"></param>
        /// <param name="typecode"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<BPara> SelPreBParas(long nodetype, string paranos, string typecode, string userid, string username)
        {
            List<BPara> bParas = new List<BPara>();
            if (string.IsNullOrEmpty(typecode))
            {
                //参数枚举类型不可为空，为空则直接返回空数组
                return bParas;
            }
            //查询默认参数和出厂参数
            bParas = IBBPara.GetSystemDefaultPara(typecode, userid, username).ToList();
            if (nodetype != 0)
            {
                //查询个性化参数

                var bParaItems = this.QuerySystemParaItem("bparaitem.ObjectID=" + nodetype, Para_SystemType.检验站点相关.Key, typecode);
                if (bParaItems.Count > 0)
                {
                    //如果包含个性化参数，则将个性化参数的参数值替换默认参数值
                    foreach (var item in bParas)
                    {
                        var bParaItem = bParaItems.Where(a => a.ParaNo == item.ParaNo);
                        if (bParaItem.Count() > 0)
                            item.ParaValue = bParaItem.First().ParaValue;
                    }
                }
            }
            //判断参数编码是否有值，如果有则按照参数编码对配置参数进行过滤
            if (!string.IsNullOrEmpty(paranos))
            {
                string[] paranoarr = paranos.Split(',');
                List<BPara> bParasByParaNo = new List<BPara>();
                foreach (var item in paranoarr)
                {
                    var bpara = bParas.Where(a => a.ParaNo == item);
                    if (bpara.Count() > 0)
                        bParasByParaNo.Add(bpara.First());
                }
                return bParasByParaNo;
            }
            return bParas;
        }
        /// <summary>
        /// 删除个性参数与站点人员关系
        /// </summary>
        /// <param name="objectInfo"></param>
        /// <returns></returns>
        public BaseResultDataValue DeleteParaItemByObjectIDAndHostTypeUser(string objectInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (objectInfo != null && objectInfo.Trim().Length > 0)
            {
                int delCount = 0;
                JArray jsonArray = JArray.Parse(objectInfo);
                if (jsonArray != null && jsonArray.Count > 0)
                {
                    foreach (JObject jsonObject in jsonArray)
                    {
                        if (jsonObject != null)
                        {
                            string objectID = jsonObject["ObjectID"] != null ? jsonObject["ObjectID"].ToString() : "";
                            string objectName = jsonObject["ObjectName"] != null ? jsonObject["ObjectName"].ToString() : "";
                            if (objectID != null && objectID.Trim().Length > 0)
                            {
                                delCount = DBDao.DeleteByHql(" from BParaItem bparaitem where bparaitem.ObjectID=" + objectID);
                                IDBHostTypeUserDao.DeleteByHql(" from BHostTypeUser bhosttypeuser where bhosttypeuser.HostTypeID=" + objectID);
                            }
                               
                        }
                    }
                }
            }
            return baseResultDataValue;
        }
    }
}