using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Log;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    /// 参数业务处理 
    /// </summary>
    public class BBPara : BaseBLL<BPara>, ZhiFang.IBLL.LabStar.IBBPara
    {
        /// <summary>
        /// 保存参数信息
        /// </summary>
        /// <param name="listPara">参数列表</param>
        /// <param name="operaterID">操作者ID</param>
        /// <param name="operater">操作者名称</param>
        /// <returns></returns>
        public BaseResultDataValue AddAndEditPara(IList<BPara> listPara, string operaterID, string operater)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            foreach (BPara paraEntity in listPara)
            {
                BPara entity = this.Get(paraEntity.Id);
                if (entity == null)
                {
                    if (operaterID != null && operaterID.Trim().Length > 0)
                        paraEntity.OperaterID = long.Parse(operaterID);
                    paraEntity.Operater = operater;
                    paraEntity.BVisible = true;
                    paraEntity.IsUse = true;
                    paraEntity.DataUpdateTime = DateTime.Now;
                    this.Entity = paraEntity;
                    baseResultDataValue.success = this.Add();
                }
                else if ((entity.ParaValue != paraEntity.ParaValue) || (entity.ParaEditInfo != paraEntity.ParaEditInfo) || (entity.SystemCode != paraEntity.SystemCode) ||
                         (entity.CName != paraEntity.CName) || (entity.BVisible != paraEntity.BVisible) || (entity.IsUse != paraEntity.IsUse))
                {
                    if (operaterID != null && operaterID.Trim().Length > 0)
                        entity.OperaterID = long.Parse(operaterID);
                    entity.Operater = operater;
                    entity.CName = paraEntity.CName;
                    entity.ParaValue = paraEntity.ParaValue;
                    entity.BVisible = paraEntity.BVisible;
                    entity.IsUse = paraEntity.IsUse;
                    entity.ParaEditInfo = paraEntity.ParaEditInfo;
                    entity.SystemCode = paraEntity.SystemCode;
                    entity.DataUpdateTime = DateTime.Now;
                    this.Entity = entity;
                    baseResultDataValue.success = this.Edit();
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据参数分类编码获取系统默认参数列表
        /// 如果系统默认参数不在数据库参数表中，则自动添加到数据库参数表中
        /// </summary>
        /// <param name="paraTypeCode">参数分类编码或逗号分隔的编码字符串</param>
        /// <param name="operaterID">操作者ID</param>
        /// <param name="operater">操作者名称</param>
        /// <returns></returns>
        public IList<BPara> GetSystemDefaultPara(string paraTypeCode, string operaterID, string operater)
        {
            IList<BPara> paraList = new List<BPara>();
            if (paraTypeCode != null && paraTypeCode.Trim().Length > 0)
            {
                string[] arrayTypeCode = paraTypeCode.Split(',');
                string typeCodeHQL = "";
                foreach (string typeCode in arrayTypeCode)
                {
                    if (typeCodeHQL == "")
                        typeCodeHQL = " bpara.TypeCode=\'" + typeCode + "\'";
                    else
                        typeCodeHQL += " or bpara.TypeCode=\'" + typeCode + "\'";
                }
                //获取设置的默认参数列表
                IList<BPara> paraDBList = DBDao.GetListByHQL("(" + typeCodeHQL + ")");
                //获取出厂设置参数列表
                IList<BPara> paraDictList = QueryFactoryParaListByParaClassName(paraTypeCode);
                if (paraDictList != null && paraDictList.Count > 0)
                {
                    IList<BPara> paraAddList = null;
                    if (paraDBList.Count == 0)
                        paraAddList = paraDictList;
                    else
                    {
                        BParaComparer comparer = new BParaComparer();
                        //查找出厂设置参数中存在而默认参数中不存在的参数列表
                        paraAddList = paraDictList.Except(paraDBList, comparer).ToList();
                    }
                    if (paraAddList != null && paraAddList.Count > 0)
                    {
                        foreach (BPara paraEntity in paraAddList)
                        {
                            if (operaterID != null && operaterID.Trim().Length > 0)
                                paraEntity.OperaterID = long.Parse(operaterID);
                            paraEntity.Operater = operater;
                            paraEntity.BVisible = true;
                            paraEntity.IsUse = true;
                            paraEntity.DataUpdateTime = DateTime.Now;
                            if (DBDao.Save(paraEntity))
                                paraDBList.Add(paraEntity);
                        }
                    }
                }
                if (paraDBList.Count > 0)
                    paraDBList.ToList().ForEach(p => { p.ParaSource = 1; });
                paraList = paraDBList;
            }
            return paraList;
        }

        /// <summary>
        /// 根据参数字典类类名反射对应的参数列表
        /// </summary>
        /// <param name="classname">参数字典类名</param>
        /// <returns></returns>
        public IList<BPara> QueryFactoryParaListByParaClassName(string classname)
        {
            IList<BPara> paraList = new List<BPara>();
            if (!string.IsNullOrWhiteSpace(classname))
            {
                paraList = QueryFactoryParaListByParaClassName(classname.Split(','));
            }
            return paraList;
        }

        /// <summary>
        /// 根据参数字典类类名反射对应的参数列表
        /// </summary>
        /// <param name="classname">参数字典类名</param>
        /// <returns></returns>
        public IList<BPara> QueryFactoryParaListByParaClassName(string[] listClassName)
        {
            IList<BPara> paraList = new List<BPara>();
            string entitynamespace = "ZhiFang.Entity.LabStar";
            foreach (string classname in listClassName)
            {
                Type type = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
                if (type != null)
                {
                    FieldInfo field = type.GetField("ParaList");
                    if (field != null)
                    {
                        var list = field.GetValue(null);
                        if (list != null && (typeof(List<BPara>)) == list.GetType())
                            paraList = paraList.Union((IList<BPara>)list).ToList();
                    }
                }
                else
                    Log.Error("错误信息：未找到类字典：" + classname + ",命名空间：" + entitynamespace + "！");
            }
            return paraList;
        }

        /// <summary>
        /// 根据参数名称查询相关出厂参数信息
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns></returns>
        public IList<BaseClassDicEntity> QueryFactoryParaInfoByParaName(string paraName)
        {
            IList<BaseClassDicEntity> paraTypeList = new List<BaseClassDicEntity>();
            IList<BaseClassDicEntity> firstClassTypeList = QueryDictObjectListByClassName("Para_MoudleType");
            foreach (BaseClassDicEntity entity in firstClassTypeList)//一级分类循环
            {
                IList<string> listTypeCode = new List<string>();
                IList<BaseClassDicEntity> secondClassTypeList = QueryDictObjectListByClassName(entity.Code);
                foreach (BaseClassDicEntity dicEntity in secondClassTypeList)//二级分类循环
                {
                    if (dicEntity.Name != null && dicEntity.Name.Contains(paraName))
                        paraTypeList.Add(dicEntity);
                    if (dicEntity.Code != null)
                    {
                        IList<BPara> paraList = QueryFactoryParaListByParaClassName(dicEntity.Code);
                        foreach (BPara para in paraList)//参数列表循环
                        {
                            if (para.CName != null && para.CName.Contains(paraName) && (!listTypeCode.Contains(para.TypeCode)))
                            {
                                listTypeCode.Add(para.TypeCode);
                                break;
                            }
                        }
                    }
                }
                foreach (string typeCode in listTypeCode)
                {
                    IList<BaseClassDicEntity> tempList1 = paraTypeList.Where(p => p.Code == typeCode).ToList();
                    if (tempList1 == null || tempList1.Count == 0)
                    {
                        IList<BaseClassDicEntity> tempList2 = secondClassTypeList.Where(p => p.Code == typeCode).ToList();
                        if (tempList2 != null && tempList2.Count > 0)
                            paraTypeList.Add(tempList2[0]);
                    }
                }
            }
            return paraTypeList;
        }

        /// <summary>
        /// 根据参数编码查询相关默认参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <returns></returns>
        public BPara QuerySystemDefaultParaValueByParaNo(string paraNo)
        {
            BPara para = null;
            IList<BPara> list = this.SearchListByHQL(" bpara.ParaNo=\'" + paraNo + "\'");
            if (list != null && list.Count > 0)
                para = list[0];
            else
                para = QueryFactoryParaInfoByParaNo(paraNo);
            if (para != null)
                para.ParaSource = 1;
            return para;
        }

        /// <summary>
        /// 根据参数编码查询出厂参数信息---反射参数字典类
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <returns></returns>
        public BPara QueryFactoryParaInfoByParaNo(string paraNo)
        {
            BPara para = null;
            IList<BaseClassDicEntity> firstClassTypeList = QueryDictObjectListByClassName("Para_MoudleType");
            foreach (BaseClassDicEntity entity in firstClassTypeList)//一级分类循环
            {
                IList<BaseClassDicEntity> secondClassTypeList = QueryDictObjectListByClassName(entity.Code);
                foreach (BaseClassDicEntity dicEntity in secondClassTypeList)//二级分类循环
                {
                    IList<BPara> paraList = QueryFactoryParaListByParaClassName(dicEntity.Code);
                    foreach (BPara paraEntity in paraList)//参数列表循环
                    {
                        if (paraEntity.ParaNo == paraNo)
                            return paraEntity;
                    }
                }
            }
            return para;
        }

        /// <summary>
        /// 根据字典类类名反射字典列表
        /// </summary>
        /// <param name="classname">字典类类名</param>
        /// <returns></returns>
        public IList<BaseClassDicEntity> QueryDictObjectListByClassName(string classname)
        {
            IList<BaseClassDicEntity> paraTypeList = new List<BaseClassDicEntity>();
            string entitynamespace = "ZhiFang.Entity.LabStar";
            Type type = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
            if (type != null)
            {
                foreach (FieldInfo field in type.GetFields())
                {
                    object obj = field.GetValue(null);
                    if (obj != null && (obj is KeyValuePair<string, BaseClassDicEntity>))
                    {
                        KeyValuePair<string, BaseClassDicEntity> kvParaType = (KeyValuePair<string, BaseClassDicEntity>)field.GetValue(null);
                        if (kvParaType.Value != null && kvParaType.Value.Name != null)
                        {
                            paraTypeList.Add(kvParaType.Value);
                        }
                    }
                }
            }
            else
                Log.Error("错误信息：未找到类字典：" + classname + ",命名空间：" + entitynamespace + "！");
            return paraTypeList;
        }

        public bool JudgeParaBoolValue(IList<BPara> listPara, string paraNo, string paraValue="1")
        {
            bool resultBool = false;
            if (listPara != null && listPara.Count > 0)
            {
                IList<BPara> tempList = listPara.Where(p => p.ParaNo == paraNo).ToList();
                if (tempList != null && tempList.Count > 0)
                    resultBool = (tempList[0].ParaValue == paraValue);
            }
            return resultBool;             
        }

        public IList<PreParaEnumTypeEntity> QueryOrderBarCodeSelectFieldsByClassName(string classname)
        {
            IList<PreParaEnumTypeEntity> FieldList = new List<PreParaEnumTypeEntity>();
            string entitynamespace = "ZhiFang.Entity.LabStar";
            Type type = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
            if (type != null)
            {
                FieldInfo field = type.GetField("FieldList");
                if (field != null)
                {
                    var list = field.GetValue(null);
                    if (list != null && (typeof(List<PreParaEnumTypeEntity>)) == list.GetType())
                        FieldList = FieldList.Union((IList<PreParaEnumTypeEntity>)list).ToList();
                }
            }
            else
                Log.Error("错误信息：未找到类字典：" + classname + ",命名空间：" + entitynamespace + "！");
            
            return FieldList;
        }
    }

    /// <summary>
    /// 参数过滤器
    /// </summary>
    public class BParaComparer : IEqualityComparer<BPara>
    {
        public bool Equals(BPara x, BPara y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            return x != null && y != null && x.ParaNo == y.ParaNo;
        }
        public int GetHashCode(BPara obj)
        {
            int hashStudentId = obj.ParaNo.GetHashCode();
            return hashStudentId;
        }
    }

}