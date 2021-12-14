using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region PanicValueMsgVO 危机值消息相关实体，兼容6.6技师站的XML消息格式

    //[DataContract]
    //[DataDesc(CName = "危急值消息", ClassCName = "PanicValueMsgVO", ShortCode = "PanicValueMsgVO", Desc = "")]
    public class PanicValueMsgVO
    {
        public string MSGBIGTYPE
        {
            get { return "EMGENCY"; }
        }
        public string MSGSMALLTYPE
        {
            get { return "1001"; }
        }
        public MSGCONTENT MSGCONTENT { get; set; }
    }
    #endregion

    public class MSGCONTENT
    {
        public string MSGID { get; set; }
        public string PARENTMSGID { get; set; }
        public string MSGTITLE
        {
            get { return "危机值结果报警"; }
        }
        public MSGBODY MSGBODY { get; set; }
        public MSGKEY MSGKEY { get; set; }
    }

    public class MSGBODY
    {
        //public MSG MSG { get; set; }
        public IList<MSG> MSG { get; set; }
    }

    public class MSGKEY
    {
        public string AGEUNITNAME { get; set; }
        public string GENDERNAME { get; set; }
        public string DEPTNAME { get; set; }
        public string DISTRICTNAME { get; set; }
        public string WARDNAME { get; set; }
        public string SICKTYPENAME { get; set; }
        public string SECTIONNO { get; set; }
        public string PATNO { get; set; }
        public string CNAME { get; set; }
        public string AGE { get; set; }
        public string BED { get; set; }
        public string DOCTOR { get; set; }
        public string CHECKER { get; set; }
        public string CHECKDATE { get; set; }
        public string CHECKTIME { get; set; }
        public string SERIALNO { get; set; }
        public string SICKTYPENO { get; set; }
        public string REPORTFORMID { get; set; }
        public string LISDOCTORNO { get; set; }
        public string HISDOCTORID { get; set; }
        public string HISDOCTORPHONECODE { get; set; }
        public string RECEIVEDATE { get; set; }
        public string TESTTYPENO { get; set; }
        public string SAMPLENO { get; set; }
        public string TECHNICIAN { get; set; }
        public string SECTIONNAME { get; set; }
        public string COLLECTDATE { get; set; }
        public string INCEPTTIME { get; set; }
    }

    public class MSG
    {
        public string TESTITEMNAME { get; set; }
        public string TESTITEMSNAME { get; set; }
        public string RECEIVEDATE { get; set; }
        public string SECTIONNO { get; set; }
        public string TESTTYPENO { get; set; }
        public string SAMPLENO { get; set; }
        public string PARITEMNO { get; set; }
        public string ITEMNO { get; set; }
        public string REFRANGE { get; set; }
        public string TESTITEMDATETIME { get; set; }
        public string REPORTVALUEALL { get; set; }
        public string PARITEMNAME { get; set; }
        public string PARITEMSNAME { get; set; }
        public string UNIT { get; set; }
        public string REPORTFORMID { get; set; }
        public string RESULTSTATUS { get; set; }
        public string MASTERDESC { get; set; }
        public string DETAILDESC { get; set; }
        public string ITEMKEY { get; set; }
    }
}