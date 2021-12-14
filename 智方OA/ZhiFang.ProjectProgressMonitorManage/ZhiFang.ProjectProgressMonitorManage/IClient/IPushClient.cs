using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ZhiFang.ProjectProgressMonitorManage.IClient
{
    public interface IPushClient
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
    public class ClientKey
    {
        //账号名称
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        //部门编码
        private string deptno;
        public string DeptNo
        {
            get { return deptno; }
            set { deptno = value; }
        }
        //角色编码列表
        private List<string> rolelist;
        public List<string> RoleList
        {
            get { return rolelist; }
            set { rolelist = value; }
        }
        //检验小组编码列表
        private List<string> pgrouplist;
        public List<string> PGroupList
        {
            get { return pgrouplist; }
            set { pgrouplist = value; }
        }
    }
}
