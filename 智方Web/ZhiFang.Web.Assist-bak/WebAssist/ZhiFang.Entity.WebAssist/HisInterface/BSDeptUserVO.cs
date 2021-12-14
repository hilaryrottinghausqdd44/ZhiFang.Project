
using System.Runtime.Serialization;

namespace ZhiFang.Entity.WebAssist.HisInterface
{
    /// <summary>
    /// 百色市人民医院科室人员关系VO
    /// </summary>
    [DataContract]
    public class BSDeptUserVO
    {
        #region Constructors
        public BSDeptUserVO() { }
        #endregion

        /// <summary>
        /// 账号
        /// </summary>
        [DataMember]
        public virtual string PERSONAL_CODE { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DataMember]
        public virtual string PERSONAL_NAME { get; set; }

        /// <summary>
        /// 人员身份
        /// </summary>
        [DataMember]
        public virtual string PERSONAL_CLASS { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [DataMember]
        public virtual string PERSONAL_DEPT { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [DataMember]
        public virtual string PERSONAL_DEPT_NAME { get; set; }


    }
}