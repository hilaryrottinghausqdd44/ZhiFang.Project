using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml;

namespace ZhiFang.WeiXin.Common
{
    public class TransXMLToHtml
    {
        /// <summary>
        /// 将xml转换成html
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="strXSLTName">xsl路径</param>
        /// <returns></returns>
        public static string TransformXMLIntoHtml(XmlDocument xml, string strXSLTName)
        {
            try
            {
                System.Xml.Xsl.XslCompiledTransform xct = new System.Xml.Xsl.XslCompiledTransform();
                System.IO.MemoryStream t = new System.IO.MemoryStream();
                xct.Load(strXSLTName);
                xct.Transform(xml, null, t);
                //Encoding utf = Encoding.GetEncoding("GB2312");
                string resultString = System.Text.UTF8Encoding.UTF8.GetString(t.ToArray());
                //string resultString = utf.GetString(t.ToArray());
                return resultString;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 将xml转换成html
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="strXSLTName">xsl路径</param>
        /// <returns></returns>
        public static string TransformXMLIntoHtmlURL(string xmlurl, string strXSLTName)
        {
            try
            {
                System.Xml.Xsl.XslCompiledTransform xct = new System.Xml.Xsl.XslCompiledTransform();
                System.IO.MemoryStream t = new System.IO.MemoryStream();
                xct.Load(strXSLTName);
                xct.Transform(xmlurl,null,t);
                Encoding utf = Encoding.GetEncoding("GB2312");
                string resultString = System.Text.UTF8Encoding.UTF8.GetString(t.ToArray());
                //string resultString = utf.GetString(t.ToArray());
                return resultString;
            }
            catch (Exception e)
            {                
                return "";
            }
        }
    }
}
