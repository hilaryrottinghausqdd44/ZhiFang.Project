using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    [DataContract]
    public class CV_AddDoctorOrNurseVO
    {
        /// <summary>
        /// 帐号（工号）
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string PWD { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Sex { get; set; }

        /// <summary>
        /// 部门名称（所属部门默认第一个，其它的为危急值数据权限部门）
        /// </summary>
        [DataMember]
        public string DeptName { get; set; }

        /// <summary>
        /// 部门HIS编码（所属逻辑同部门名称一致，个数同部门名称一致）
        /// </summary>
        [DataMember]
        public string DeptHISCode { get; set; }
        
        /// <summary>
        /// 部门LIS编码（所属逻辑同部门名称一致，个数同部门名称一致）
        /// </summary>
        [DataMember]
        public string DeptLISCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// 人员类型，医生/护士/检验技师
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// 人员类型，医生/护士/检验技师
        /// </summary>
        [DataMember]
        public string HISCode { get; set; }
    }

    [DataContract]
    public class CV_AddTechVO : CV_AddDoctorOrNurseVO
    {
        
    }
}

