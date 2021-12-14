using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    #region ServiceInfoList
    public class ServiceContractnfo
    {
        public string CName { get; set; }//中文显示名
        public string EName { get; set; }///英文显示名
        public string ServerUrl { get; set; }///服务地址
        public string Description { get; set; }///描述
        public static Dictionary<string, string> GetDescription(string description)
        {
            Dictionary<string, string> a = new Dictionary<string, string>();
            string[] tmpinfo = description.Split(',');
            foreach (string tmp in tmpinfo)
            {
                string[] tmpa = tmp.Split(':');
                if (tmpa.Length >= 2)
                {
                    a.Add(tmpa[0], tmpa[1]);
                }
            }
            return a;
        }
    }
    #endregion

    #region ServiceContractDescription
    public class ServiceContractDescription
    {
        public string Name { get; set; }///中文名
        public string Desc { get; set; }///描述
        public string Url { get; set; }///服务地址
        public string Get { get; set; }///GET参数形式
        public string Post { get; set; }///POST参数类型名
        public string Return { get; set; }///返回对象名类型名
        public string ReturnType { get; set; }///返回数据类型名
    }
    #endregion

    #region ServiceContractDescriptionAttribute
    public class ServiceContractDescriptionAttribute : System.ComponentModel.DescriptionAttribute
    {
        public string Name { get; set; }///中文名
        public string Desc { get; set; }///描述
        public string Url { get; set; }///服务地址
        public string Get { get; set; }///GET参数形式
        public string Post { get; set; }///POST参数类型名
        public string Return { get; set; }///返回对象名类型名
        public string ReturnType { get; set; }///返回数据类型名
        public override string Description {
            get { return "{Name:'"+this.Name+"',Desc:'"+this.Desc+"',Url:'"+this.Url+"',Get:'"+this.Get+"',Post:'"+this.Post+"',Return:'"+this.Return+"',ReturnType:'"+this.ReturnType+"'}"; }
        }

    }
    #endregion
}