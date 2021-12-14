using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region XMLInputEntity
    [DataContract]
    [DataDesc(CName = "XML导入配置文件", ClassCName = "XMLInputEntity", ShortCode = "XMLInputEntity", Desc = "XML导入配置文件")]
    public class XMLInputEntity
    {
        [DataMember]
        [DataDesc(CName = "对象属性名", ShortCode = "FieldName", Desc = "对象属性名", ContextType = SysDic.All, Length = 200)]
        public virtual string FieldName { get; set; }

        [DataMember]
        [DataDesc(CName = "对象属性对应的属性名", ShortCode = "ExcelFieldName", Desc = "对象属性对应的属性名", ContextType = SysDic.All, Length = 200)]
        public virtual string ExcelFieldName { get; set; }

        [DataMember]
        [DataDesc(CName = "是否主键", ShortCode = "IsPrimaryKey", Desc = "是否主键", ContextType = SysDic.All, Length = 200)]
        public virtual string IsPrimaryKey { get; set; }

        [DataMember]
        [DataDesc(CName = "是否必填字段", ShortCode = "IsRequiredField", Desc = "是否必填字段", ContextType = SysDic.All, Length = 200)]
        public virtual string IsRequiredField { get; set; }

        [DataMember]
        [DataDesc(CName = "默认值", ShortCode = "DefaultValue", Desc = "默认值", ContextType = SysDic.All, Length = 200)]
        public virtual string DefaultValue { get; set; }

    }
    #endregion

    #region XMLOutputEntity
    [DataContract]
    [DataDesc(CName = "XML导出配置文件", ClassCName = "XMLOutputEntity", ShortCode = "XMLOutputEntity", Desc = "XML导出配置文件")]
    public class XMLOutputEntity
    {
        [DataMember]
        [DataDesc(CName = "对象属性名", ShortCode = "FieldName", Desc = "对象属性名", ContextType = SysDic.All, Length = 200)]
        public virtual string FieldName { get; set; }

        [DataMember]
        [DataDesc(CName = "对象属性对应的属性名", ShortCode = "ExcelFieldName", Desc = "对象属性对应的属性名", ContextType = SysDic.All, Length = 200)]
        public virtual string ExcelFieldName { get; set; }

        [DataMember]
        [DataDesc(CName = "日期格式", ShortCode = "DataFormat", Desc = "日期格式", ContextType = SysDic.All, Length = 200)]
        public virtual string DataFormat { get; set; }
    }
    #endregion
}