using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common
{
    public interface IBDataDownload
    {
        /// <summary>
        /// 提供数据字典下载(中心端、客户端、对照关系共同拼接的xml)
        /// <param name="LabCode">实验室编码</param>
        /// <param name="time">客户端提交的时间</param>
        /// <param name="strXML">返回字典数据xml字符串</param>
        /// <param name="strXMLSchema">返回字典表结构文件</param>
        /// <param name="strMsg">返回描述消息：成功、失败(失败描述)</param>
        /// </summary>
        /// <returns>0代表失败；1代表成功</returns>
        int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg);

        /// <summary>
        /// 根据客户端提交的时间获取一个字典列表返回给客户端
        /// </summary>
        /// <param name="time">客户端提交的时间</param>
        /// <param name="LabCode">实验室编码</param>
        /// <param name="strXML">返回字典名称列表的xml字符串</param>
        /// <param name="strMsg">返回描述消息：成功、失败(失败描述)</param>
        /// <returns>0代表失败；1代表成功</returns>
        int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg);
    }
}
