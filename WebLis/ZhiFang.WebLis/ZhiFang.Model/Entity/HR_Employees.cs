using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //HR_Employees
    public class HR_Employees
    {

        /// <summary>
        /// 员工标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 姓
        /// </summary>		
        private string _namel;
        public string NameL
        {
            get { return _namel; }
            set { _namel = value; }
        }
        /// <summary>
        /// 名
        /// </summary>		
        private string _namef;
        public string NameF
        {
            get { return _namef; }
            set { _namef = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>		
        private string _sex;
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>		
        private DateTime? _birth;
        public DateTime? Birth
        {
            get { return _birth; }
            set { _birth = value; }
        }
        /// <summary>
        /// Email
        /// </summary>		
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 电话1
        /// </summary>		
        private string _tel1;
        public string Tel1
        {
            get { return _tel1; }
            set { _tel1 = value; }
        }
        /// <summary>
        /// 电话2
        /// </summary>		
        private string _tel2;
        public string Tel2
        {
            get { return _tel2; }
            set { _tel2 = value; }
        }
        /// <summary>
        /// 手机
        /// </summary>		
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>		
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 城市
        /// </summary>		
        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// 省份
        /// </summary>		
        private string _province;
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }
        /// <summary>
        /// 国家
        /// </summary>		
        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>		
        private string _zip;
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>		
        private string _maritalstatus;
        public string MaritalStatus
        {
            get { return _maritalstatus; }
            set { _maritalstatus = value; }
        }
        /// <summary>
        /// 学历
        /// </summary>		
        private string _educationlevel;
        public string EducationLevel
        {
            get { return _educationlevel; }
            set { _educationlevel = value; }
        }
        /// <summary>
        /// 在职
        /// </summary>		
        private int _enabled;
        public int Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        /// <summary>
        /// 照片
        /// </summary>		
        private byte[] _pic;
        public byte[] Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }
        /// <summary>
        /// 加入本公司时间
        /// </summary>		
        private DateTime? _joindate;
        public DateTime? JoinDate
        {
            get { return _joindate; }
            set { _joindate = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>		
        private string _idcardno;
        public string IDCardNO
        {
            get { return _idcardno; }
            set { _idcardno = value; }
        }
        /// <summary>
        /// DesktopTheme
        /// </summary>		
        private int _desktoptheme;
        public int DesktopTheme
        {
            get { return _desktoptheme; }
            set { _desktoptheme = value; }
        }
        /// <summary>
        /// FirstPamId
        /// </summary>		
        private string _firstpamid;
        public string FirstPamId
        {
            get { return _firstpamid; }
            set { _firstpamid = value; }
        }
        /// <summary>
        /// 学位
        /// </summary>		
        private string _degree;
        public string Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }
        /// <summary>
        /// 毕业院校
        /// </summary>		
        private string _graduateschool;
        public string GraduateSchool
        {
            get { return _graduateschool; }
            set { _graduateschool = value; }
        }
        /// <summary>
        /// 专业能力介绍
        /// </summary>		
        private string _professionalcompetence;
        public string ProfessionalCompetence
        {
            get { return _professionalcompetence; }
            set { _professionalcompetence = value; }
        }
        /// <summary>
        /// 教育背景
        /// </summary>		
        private string _educationbackground;
        public string EducationBackground
        {
            get { return _educationbackground; }
            set { _educationbackground = value; }
        }
        /// <summary>
        /// 健康状况记录
        /// </summary>		
        private string _healthstatusrecord;
        public string HealthStatusRecord
        {
            get { return _healthstatusrecord; }
            set { _healthstatusrecord = value; }
        }
        /// <summary>
        /// 政治面貌
        /// </summary>		
        private string _politicallandscape;
        public string PoliticalLandscape
        {
            get { return _politicallandscape; }
            set { _politicallandscape = value; }
        }
        /// <summary>
        /// 部门
        /// </summary>		
        private string _department;
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// 培训情况
        /// </summary>		
        private string _training;
        public string Training
        {
            get { return _training; }
            set { _training = value; }
        }
        /// <summary>
        /// 个人主页
        /// </summary>		
        private string _personalhomepage;
        public string PersonalHomePage
        {
            get { return _personalhomepage; }
            set { _personalhomepage = value; }
        }
        /// <summary>
        /// 工作简历
        /// </summary>		
        private string _jobresume;
        public string JobResume
        {
            get { return _jobresume; }
            set { _jobresume = value; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>		
        private decimal _officephone;
        public decimal OfficePhone
        {
            get { return _officephone; }
            set { _officephone = value; }
        }
        /// <summary>
        /// 家庭情况
        /// </summary>		
        private string _family;
        public string Family
        {
            get { return _family; }
            set { _family = value; }
        }
        /// <summary>
        /// 入职时间
        /// </summary>		
        private DateTime? _entrytime;
        public DateTime? EntryTime
        {
            get { return _entrytime; }
            set { _entrytime = value; }
        }
        /// <summary>
        /// 继续教育情况
        /// </summary>		
        private string _continuingeducation;
        public string ContinuingEducation
        {
            get { return _continuingeducation; }
            set { _continuingeducation = value; }
        }
        /// <summary>
        /// 职位
        /// </summary>		
        private string _position;
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        /// <summary>
        /// 工资变化
        /// </summary>		
        private string _wagechange;
        public string WageChange
        {
            get { return _wagechange; }
            set { _wagechange = value; }
        }
        /// <summary>
        /// 家庭地址
        /// </summary>		
        private string _homeaddress;
        public string HomeAddress
        {
            get { return _homeaddress; }
            set { _homeaddress = value; }
        }
        /// <summary>
        /// 劳动合同
        /// </summary>		
        private string _laborcontract;
        public string LaborContract
        {
            get { return _laborcontract; }
            set { _laborcontract = value; }
        }
        /// <summary>
        /// 分机
        /// </summary>		
        private decimal _ext;
        public decimal Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }
        /// <summary>
        /// 民族
        /// </summary>		
        private string _nationality;
        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }
        /// <summary>
        /// 专业资格
        /// </summary>		
        private string _professionalqualifications;
        public string ProfessionalQualifications
        {
            get { return _professionalqualifications; }
            set { _professionalqualifications = value; }
        }
        /// <summary>
        /// 获奖简历及证书
        /// </summary>		
        private string _awardandcertificates;
        public string AwardandCertificates
        {
            get { return _awardandcertificates; }
            set { _awardandcertificates = value; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>		
        private decimal _hometel;
        public decimal HomeTel
        {
            get { return _hometel; }
            set { _hometel = value; }
        }
        /// <summary>
        /// 照片
        /// </summary>		
        private string _stuffphoto;
        public string StuffPhoto
        {
            get { return _stuffphoto; }
            set { _stuffphoto = value; }
        }
        /// <summary>
        /// 工作职责
        /// </summary>		
        private string _jobduty;
        public string JobDuty
        {
            get { return _jobduty; }
            set { _jobduty = value; }
        }

        public string DeptCName { get; set; }
        public long DeptId { get; set; }

        public string Account { get; set; }

        public string PWD { get; set; }
    }
}

