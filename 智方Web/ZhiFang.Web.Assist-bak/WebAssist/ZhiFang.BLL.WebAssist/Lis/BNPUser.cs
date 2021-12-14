
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BNPUser : BaseBLL<NPUser, int>, ZhiFang.IBLL.WebAssist.IBNPUser
    {
        private readonly string Strcoust = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
        IDDepartmentDao IDDepartmentDao { get; set; }

        #region 将NPUser封装为RBACUser
        public EntityList<RBACUser> SearchRBACUserOfNPUserByHQL(string where, string sort, int page, int limit)
        {
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            tempEntityList.list = new List<RBACUser>();
            EntityList<NPUser> userList = this.SearchListByHQL(where, sort, page, limit);
            tempEntityList.count = userList.count;

            foreach (var entity in userList.list)
            {
                tempEntityList.list.Add(_getRBACUser(entity));
            }

            return tempEntityList;
        }
        private RBACUser _getRBACUser(NPUser entity)
        {
            RBACUser user = new RBACUser();

            user.Id = entity.Id;
            user.Account = entity.ShortCode;
            user.Shortcode = entity.ShortCode;
            user.CName = entity.CName;
            user.DispOrder = entity.DispOrder;
            user.DataAddTime = entity.DataAddTime;
            user.IsUse = entity.Visible == 1 ? true : false;

            HREmployee employee = new HREmployee();
            employee.Id = entity.Id;
            employee.CName = entity.CName;
            employee.DispOrder = entity.DispOrder;
            employee.DataAddTime = entity.DataAddTime;
            employee.IsUse = entity.Visible == 1 ? true : false;

            HRDept dept = null;
            if (entity.DeptNo >= 0)
            {
                Department department = IDDepartmentDao.Get(entity.DeptNo);
                if (department != null) dept = _getHRDept(department);
            }
            if (dept != null) employee.HRDept = dept;
            user.HREmployee = employee;

            return user;
        }
        private HRDept _getHRDept(Department department)
        {
            HRDept dept = new HRDept();
            dept.Id = department.Id;
            dept.CName = department.CName;
            dept.Shortcode = department.ShortCode;
            dept.IsUse = department.Visible == 1 ? true : false;
            dept.DispOrder = department.DispOrder;

            return dept;
        }
        #endregion

        #region 加密/解密
        //加密
        public string CovertPassWord(string astr)
        {
            int iLength, imod3, idv3;
            iLength = astr.Length;
            string result = "";
            if (iLength == 0)
            {
                return "=";
            }
            idv3 = iLength / 3;
            imod3 = iLength % 3;
            while (idv3 > 0)
            {
                Get64Str(3, astr, ref iLength, ref result);
                iLength = iLength - 3;
                idv3--;
            }
            switch (imod3)
            {
                case 1:
                    Get64Str(1, astr, ref iLength, ref result);
                    break;
                case 2:
                    Get64Str(2, astr, ref iLength, ref result);
                    break;
            }
            //ZhiFang.Common.Log.Log.Debug(astr + "加密后为:" + result);
            return result;
        }
        //解密
        public string UnCovertPassWord(string pwd)
        {
            int iLength, imod4, idv4;
            iLength = pwd.Length;
            string result = "";
            if (iLength == 0)
            {
                return "";
            }

            idv4 = iLength / 4;
            imod4 = iLength % 4;
            while (idv4 > 0)
            {
                Get256Str(4, ref pwd, ref iLength, ref result);
                iLength = iLength - 4;
                idv4--;
            }
            switch (imod4)
            {
                case 1:
                    Get256Str(1, ref pwd, ref iLength, ref result);
                    break;
                case 2:
                    Get256Str(2, ref pwd, ref iLength, ref result);
                    break;
                case 3:
                    Get256Str(3, ref pwd, ref iLength, ref result);
                    break;
            }

            for (var i = 0; i < result.Length; i++)
            {
                if ((int)result[i] < 32 || (int)result[i] > 127)
                {
                    result = pwd;
                }
            }
            ZhiFang.Common.Log.Log.Debug(pwd + "解密后为:" + result);
            return result;
        }
        public void Get64Str(int il, string pwd, ref int iLength, ref string Result)
        {
            char achar;
            int iCount64, iChar, imod64, i;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                achar = pwd[iLength - il + i];
                iChar = (int)achar;
                iCount64 = iCount64 * 256 + iChar;
            }
            for (i = 0; i < 4; i++)
            {
                if (iCount64 == 0)
                {
                    Result = '=' + Result;
                }
                else
                {
                    imod64 = (iCount64 % 64) + 1;
                    iCount64 = iCount64 / 64;
                    achar = Strcoust[imod64 - 1];
                    Result = achar + Result;
                }
            }
        }

        public void Get256Str(int il, ref string pwd, ref int iLength, ref string Result)
        {
            int i, ichar, iCount64, imod256;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                ichar = Strcoust.IndexOf(pwd[iLength + i - il]);
                iCount64 = iCount64 * 64 + ichar;
            }
            for (i = 0; i < 3; i++)
            {
                if (iCount64 != 0)
                {
                    imod256 = iCount64 % 256;
                    Result = ((char)imod256) + Result;
                    iCount64 = iCount64 / 256;
                }
            }
        }
        #endregion

    }
}