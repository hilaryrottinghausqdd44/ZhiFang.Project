using System;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.Runtime.InteropServices;//调用DLL用



namespace ZhiFang.WebLisService.clsCommon
{
	/// <summary>
	/// Tools 的摘要说明。
	/// 
	/// 修改历史
	/// </summary> 
	
	public class Tools
	{

		public Tools()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}



        /// <summary>
        /// 转换哈希表的键值为大写
        /// </summary>
        /// <param name="hashData"></param>
        /// <returns></returns>
        public static Hashtable convertHashKeyToUpper(Hashtable hashData)
        {
            Hashtable returnValue = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashData.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string key = myEnumerator.Key.ToString().ToUpper();
                if (returnValue[key] == null)
                    returnValue.Add(key, myEnumerator.Value);
            }
            return returnValue;
        }

        


        /// <summary>
        /// 将某个字符串写到指定的文件中，文件的编码为UTF8
        /// </summary>
        /// <param name="file">指定的文件名称</param>
        /// <param name="text">写到文件的指定文本</param>
        public static void writeStringToLocalFile1(string file, string text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //创建目录
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //删除旧文件
                FileAttributes oldAttr = FileAttributes.Normal;
                if (System.IO.File.Exists(file))
                {
                    //取原来文件的属性
                    oldAttr = System.IO.File.GetAttributes(file);
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, false, Encoding.UTF8);//重新创建
                fileWriter.Write(text);
                fileWriter.Close();
                //保留旧文件的属性
                System.IO.File.SetAttributes(file, oldAttr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
        }




        /// <summary>
        /// 将某个字符串写到指定的文件中，文件的编码为UTF8
        /// </summary>
        /// <param name="file">指定的文件名称</param>
        /// <param name="text">写到文件的byte数组</param>
        public static void writeBytesToLocalFile(string file, byte[] text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //创建目录
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //删除旧文件
                FileAttributes oldAttr = FileAttributes.Normal;
                if (System.IO.File.Exists(file))
                {
                    //取原来文件的属性
                    oldAttr = System.IO.File.GetAttributes(file);
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.FileStream fileStream = new System.IO.FileStream(file, FileMode.OpenOrCreate);
                fileStream.Write(text, 0, text.Length);
                fileStream.Close();
                //保留旧文件的属性
                System.IO.File.SetAttributes(file, oldAttr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
        }



        /// <summary>
        /// 对我们在框架和框架之间传递的页面URL进行解码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string urlEscape(string url)
        {
            //将LIKE进行解码
            string urlNew = url;
            urlNew = urlNew.Replace("+LIKE+", " LIKE ");
            //将分号进行解码
            urlNew = urlNew.Replace("%3b", ";");
            //将单引号进行解码
            urlNew = urlNew.Replace("&apos;", "'");
            //将空格进行解码
            urlNew = urlNew.Replace("%20", " ");
            //将百分号进行解码
            urlNew = urlNew.Replace("%25", "%");
            return urlNew;
        }



        public static Hashtable getParaFromURL(string url)
        {
            Hashtable returnValue = new Hashtable();
            char[] split = new char[] { '?' };
            string[] paraList = url.Split(split);
            if (paraList.Length >= 2)
            {
                //string para = paraList[1];
                string para = url.Substring(paraList[0].Length + 1);
                string[] paraNameValueList = para.Split(new char[] { '&' });
                for (int i = 0; i < paraNameValueList.Length; i++)
                {
                    string paramString = paraNameValueList[i];
                    string[] paraNameValue = paramString.Split(new char[] { '=' });
                    string paraName = paraNameValue[0];
                    if (paraName == "")
                        continue;
                    string paraValue = "";
                    if (paraNameValue.Length > 1)
                    {
                        paraValue = paramString.Replace(paraName + "=", "");
                    }
                    if (returnValue[paraName] == null)
                        returnValue.Add(paraName, paraValue);
                }
            }
            return returnValue;
        }




        public static Hashtable splitParaFromURL(string para)
        {
            char[] split = new char[]{','};
            return Tools.splitParaFromURL(para, split);
        }


        /// <summary>
        /// 拆分字符串到哈希表:将空串删除
        /// </summary>
        /// <param name="para"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static Hashtable splitParaToHashtable(string para, char[] split)
        {
            Hashtable returnValue = new Hashtable();
            string[] paraList = para.Split(split);
            for (int i = 0; i < paraList.Length; i++)
            {
                string paraName = paraList[i];
                if (paraName == "")
                    continue;
                if (returnValue[paraName] == null)
                    returnValue.Add(paraName, paraName);
            }
            return returnValue;
        }


        public static Hashtable splitParaFromURL(string para, char[] split)
        {
            Hashtable returnValue = new Hashtable();
            string[] paraList = para.Split(split);
            for (int i = 0; i < paraList.Length; i++)
            {
                string paramString = paraList[i];
                string[] paraNameValue = paramString.Split(new char[]{'='});
                string paraName = paraNameValue[0];
                if(paraName == "")
                    continue;
                string paraValue = "";
                if (paraNameValue.Length > 1)
                {
                    paraValue = paramString.Replace(paraName + "=", "");
                }
                if (returnValue[paraName] == null)
                    returnValue.Add(paraName, paraValue);
            }
            return returnValue;
        }



        /// <summary>
        /// 将一段时间转换成字符串
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static string getTowTimeString(DateTime dtBegin, DateTime dtEnd)
		{
			string useTimes = "";
			System.TimeSpan ts = dtEnd - dtBegin;
			if(ts.Days > 0)
			{
				useTimes += ts.Days.ToString() + "天";
			}
			if(ts.Hours > 0)
			{
				useTimes += ts.Hours.ToString() + "小时";
			}
			if(ts.Minutes > 0)
			{
				useTimes += ts.Minutes.ToString() + "分钟";
			}
			if(ts.Seconds > 0)
			{
				useTimes += ts.Seconds.ToString() + "秒";
			}
			if(ts.Milliseconds > 0)
			{
				//毫秒
				if(useTimes == "")
					useTimes += ts.Milliseconds.ToString() + "毫秒";
			}
			return useTimes;
		}



		/// <summary>
		/// 从本地文件读取所有的内容,如果出现异常，返回null
		/// </summary>
		/// <param name="file"></param>
		/// <returns>文件的内容</returns>
		public static string readFromLocalFile(string file)
		{
			string text = "";
			try
			{
				if(!System.IO.File.Exists(file))
					return null;
				System.IO.StreamReader fileReader = new System.IO.StreamReader(file,Encoding.Default);
				//取文件内容
				text = fileReader.ReadToEnd();
				fileReader.Close();
			}
			catch(System.Exception ex)
			{
				throw new Exception("读文件：" + file + "出错！\n" + ex.Message);
			}
			return text;
		}





        /// <summary>
        /// 从本地文件读取所有的内容,如果出现异常，返回null
        /// </summary>
        /// <param name="path"></param>
        /// <returns>文件的内容</returns>
        public static byte[] readBytesFromLocalFile(string path)
        {
            byte[] pdfContent = null;
            if (System.IO.File.Exists(path))
            {
                System.IO.FileStream fileStream = new System.IO.FileStream(path, FileMode.Open);
                long fileSize = fileStream.Length;
                pdfContent = new byte[fileSize];
                fileStream.Read(pdfContent, 0, (int)fileSize);
                fileStream.Close();
            }
            return pdfContent;
        }




		/// <summary>
		/// 将某个字符串写到指定的文件中，文件的编码为UTF8
		/// </summary>
		/// <param name="file">指定的文件名称</param>
		/// <param name="text">写到文件的指定文本</param>
		public static void writeStringToLocalFile(string file, string text)
		{
			try
			{
				SplitFileName splitFileName = Tools.getSplitFileName(file, new char[]{'\\'});
				string path = splitFileName.Path;
				//创建目录
				if(path.Length > 0)
				{
					if(!System.IO.Directory.Exists(path))
						System.IO.Directory.CreateDirectory(path);
				}
				//删除旧文件
				if(System.IO.File.Exists(file))
				{
					System.IO.File.SetAttributes(file, FileAttributes.Normal);
					System.IO.File.Delete(file);
				}
				System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, false,Encoding.UTF8);//重新创建
				fileWriter.Write(text);
				fileWriter.Close();
				//System.IO.File.SetAttributes(file, FileAttributes.ReadOnly);
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
			return;
		}



        /// <summary>
        /// 获取应用程序启动目录
        /// </summary>
        /// <returns></returns>
		public static string getSystemBasePath()
		{
			return getPathParent(System.AppDomain.CurrentDomain.BaseDirectory);
		}




	

		/// <summary>
		/// 从配置文件取系统的标题
		/// </summary>
		/// <returns></returns>
		public static string getSystemTitle()
		{
			string title = System.Configuration.ConfigurationSettings.AppSettings["System.Title.Name"].ToString();
			return title;
		}

		
		/// <summary>
		/// 取某个目录的父目录
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string getPathParent(string path)
		{
			System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(path);
			string pathParent = info.Parent.FullName;
			if(!(pathParent.EndsWith("\\")))
			{
				pathParent +="\\";
			}
			return pathParent;
		}		

	

		/// <summary>
		/// 下载某个文件 
		/// </summary>
		/// <param name="url">要下载的文件的服务器链接地址</param>
		/// <param name="destFile">要保存的本地文件名称（包括完整的路径）</param>
		public static bool downLoadFile(string url, string destFile)
		{
			try
			{
				WebClient myWebClient = new WebClient();
				SplitFileName  spfFile = Tools.getSplitFileName(destFile, new char[]{'\\'});
				//先创建目录
				if(!System.IO.Directory.Exists(spfFile.Path))
					System.IO.Directory.CreateDirectory(spfFile.Path);
				//如果文件已经存在，更改其属性让系统能对该文件进行更新
				if(System.IO.File.Exists(destFile))
				{
					System.IO.File.SetAttributes(destFile, System.IO.FileAttributes.Normal);
					//先删除目标文件
					System.IO.File.SetAttributes(destFile, System.IO.FileAttributes.Archive);
					System.IO.File.Delete(destFile);
				}
				myWebClient.DownloadFile(url, destFile);
			}
			catch(System.Net.WebException ex)
			{
				//上抛异常
				throw ex;
			}
			return true;
		}


		/// <summary>
		/// 从指定的XML找出所有的Item，并返回
		/// </summary>
		/// <param name="xmlFileName">存放字典的本地文件名称</param>
		/// <returns>三维数值：第一维为名称从name中取，第二维为代码从value中取，第三维为默认值的标志</returns>
		public static string[,] getItemFromXML(string xmlString)
		{
			string[,] retString = new string[3,1];;
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xmlString);
				XmlNodeList nodelist = doc.SelectNodes("//item");
				int itemNum = nodelist.Count;
				retString = new string [3,itemNum];
				itemNum = 0; 
				foreach(XmlNode node in nodelist)
				{
					retString[2, itemNum] = node.InnerXml;//默认值的标志
					if( node.Attributes.Count > 0)
					{
						retString[1, itemNum] = node.Attributes.GetNamedItem("name").Value;
						retString[0, itemNum] = node.Attributes.GetNamedItem("value").Value;
					}
					itemNum ++;
				}
				//释放变量
				nodelist = null;
				doc = null;
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
			return retString;
		}




		/// <summary>
		/// 从某个字符串中截取某子串
		/// </summary>
		/// <param name="oldString"></param>
		/// <param name="tagStart">子串的前面</param>
		/// <param name="tagEnd">子串的后面</param>
		/// <returns></returns>
		public static string getContentFromString(string oldString, string tagStart, string tagEnd)
		{
			string retString = "";
			int start = oldString.IndexOf(tagStart);
			if(start == -1)
				return retString;
			start += tagStart.Length;
			int end = oldString.IndexOf(tagEnd, start);
			if(end == -1)
				return retString;
			retString = oldString.Substring(start,end - start);
			return retString;
		}
		
		
		/// <summary>
		/// 将指定HTML文本中含有<OBJECT>的部分返回(<OBJECT>的组成由参数决定)
		/// 包括<OBJECT>数量，每个<OBJECT>的在原文中起始位置和截止位置
		/// 每个<OBJECT>的格式为：起始位置、截止位置、<OBJECT>文本
		/// </summary>
		/// <param name="objectString">指定的HTML</param>
		/// <param name="tagStart">OBJECT的开始标记</param>
		/// <param name="tagEnd">OBJECT的结束标记</param>
		/// <returns></returns>
		public static string[,] getObjectFromHTML(string objectString, string tagStart, string tagEnd)
		{
			string tmpStringRight = "";
			string[,] retHtmlString = new string[0,3] ;//返回的数组
			int objectLocationBegin;//搜索的OBJECT的起始位置
			int objectLocationEnd;//搜索的OBJECT的结束位置
			int objectNum;
			try
			{
				//先遍历OBJECT，查看共有几个OBJECT
				string tmpFindString = objectString;
				objectNum = 0;
				objectLocationBegin = 0;//搜索的OBJECT的起始位置
				objectLocationEnd = 0;//搜索的OBJECT的结束位置
				while((objectLocationBegin = tmpFindString.IndexOf(tagStart)) != -1)
				{
					tmpStringRight = tmpFindString.Substring(objectLocationBegin);
					objectLocationEnd = tmpStringRight.IndexOf(tagEnd);
					if(objectLocationEnd != -1) //找到结束标记
					{
						objectNum ++;
						tmpFindString = tmpStringRight.Substring(objectLocationEnd + tagEnd.Length); //继续匹配右边
					}
					else
						tmpFindString = tmpFindString.Substring(objectLocationBegin + tagStart.Length); //继续匹配右边
				}
				if(objectNum == 0)
					return retHtmlString;
				//将结果存放到数组
				retHtmlString = new string[objectNum,3];
				tmpFindString = objectString;
				objectLocationBegin = 0;//搜索的OBJECT的起始位置
				objectLocationEnd = 0;//搜索的OBJECT的结束位置
				objectNum = 0;
				while((objectLocationBegin = tmpFindString.IndexOf(tagStart, objectLocationEnd)) != -1)
				{
					objectLocationEnd = tmpFindString.IndexOf(tagEnd, objectLocationBegin + tagStart.Length);
					if(objectLocationEnd != -1) //找到结束标记
					{
						//Object内容
						retHtmlString[objectNum, 2] = tmpFindString.Substring(objectLocationBegin, objectLocationEnd - objectLocationBegin + tagEnd.Length);
						retHtmlString[objectNum, 0] = objectLocationBegin.ToString(); //开始位置
						retHtmlString[objectNum, 1] = (objectLocationEnd + tagEnd.Length).ToString();//结束位置
						objectNum ++;
					}
				}
			}
			catch//(System.Exception ex)
			{
				//DrmsMessageBox.Show("解析HTML出错！",ex);
				throw;
			}
			return retHtmlString;
		}

		


		/// <summary>
		/// 从tagListItemData中搜索指定字段对应的标题
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="tagListItemData"></param>
		/// <returns></returns>
		public static string getTitlefromName(string fieldName , ListItemData tagListItemData)
		{
			for(int i = 0 ; i < tagListItemData.formFieldNum ; i++)
			{
				if (tagListItemData.InputFieldValue[i,1] == fieldName)
					return tagListItemData.InputFieldValue[i,0] ;  //字段标题
			}
			return "";
		}


		/// <summary>
		/// 从插入元数据类型模版（字段）取各个字段的名称和输入的内容
		/// </summary>
		/// <param name="xmlString"></param>
		/// <param name="fieldNameTag">字段名称的标记，在本系统中用<fieldname></fieldname>表示</param>
		/// <param name="fieldValueTag">字段内容的标记，在本系统中用<PARAM name="textValue" value="输入的内容"></PARAM></param>
		/// <returns></returns>
		public static FieldInfo getFieldNameAndValueFromObject(string xmlString, string fieldNameTag , string fieldValueTag)
		{
			FieldInfo fieldInfo = new FieldInfo();
			
			try
			{
				XmlDocument doc = new XmlDocument();
				//string xmlNew = Tools.addDoubleQuotationMarks(xmlString);
				//xmlNew = Tools.addEndMarks(xmlNew,"<PARAM ", ">");

				doc.LoadXml(xmlString);
				//取字段输入的内容
				XmlNodeList nodelist = doc.SelectNodes("//PARAM");
				bool isGet = false;
				foreach(XmlNode node in nodelist)
				{
					if(node.Attributes.Count > 1)
					{
						for(int i=0;i<node.Attributes.Count; i++)
						{
							string name = node.Attributes.Item(0).Value.ToLower();
							if(name == fieldValueTag.ToLower() )
							{
								fieldInfo.FieldValue = node.Attributes.Item(1).Value;
								isGet = true;
								break;
							}
						}
						if(isGet) break;
					}
				}
				//取字段名称
				isGet = false;
				nodelist = doc.SelectNodes("//CUSTOMPARAM");
				foreach(XmlNode node in nodelist)
				{
					if(node.HasChildNodes)
					{
						for(int i=0;i<node.ChildNodes.Count; i++)
						{
							if(node.ChildNodes[i].Name.ToLower() == fieldNameTag.ToLower() )
							{
								fieldInfo.FieldName = node.ChildNodes[i].InnerXml;
								isGet = true;
								break;
							}
						}
						if(isGet) break;
					}
				}
				//释放变量
				nodelist = null;
				doc = null;
			}
			catch//(System.Exception ex)
			{
				//MessageBox.Show(ex.Message);
			}
			finally
			{
			}
			return fieldInfo;
		}

		




		/// <summary>
		/// 拆分文件为路径和文件名称两部分
		/// 返回SplitFileName类
		/// </summary>
		/// <param name="fileName">文件名称（带路径）</param>
		/// <param name="split">分隔符，一般为“/”</param>
		/// <returns>SplitFileName类</returns>
		public static SplitFileName getSplitFileName(string fileName,char[] split)
		{
			SplitFileName splitFileName = new SplitFileName();
			splitFileName.Path = "";
			splitFileName.FileName = "";
			splitFileName.Split = split;
			if(fileName == "")
				return splitFileName;
			string[] splitArray = fileName.Split(splitFileName.Split);
			if(splitArray.Length == 0)
			{
				splitFileName.FileName = "";
				splitFileName.Path = "";
			}
			else
			{
				splitFileName.FileName = splitArray[splitArray.Length-1];//文件名称
				if(splitFileName.FileName == "")
					splitFileName.Path = fileName;
				else
					splitFileName.Path = fileName.Replace(splitFileName.FileName,"");
			}
			return splitFileName;
		}
	


		/// <summary>
		/// 拆分由指定分隔符的字符串，按照一定的顺序存到相应的类型中
		/// 返回SplitParamName类
		/// </summary>
		/// <param name="paramName">参数名称</param>
		/// <param name="split">分隔符，一般为“_”</param>
		/// <returns>SplitParamName类</returns>
		public static SplitParamName getSplitParamName(string paramName, char[] split)
		{
			SplitParamName splitParamName = new SplitParamName();
			splitParamName.Split = split;//new char[] {','};
			string[] splitArray = paramName.Split(splitParamName.Split);
			if(splitArray.Length <= 1)
			{
				splitParamName.Name = paramName;
				splitParamName.Style = "";
			}
			else
			{
				splitParamName.Style = splitArray[splitArray.Length-1].Trim();//文件名称
				string replaceString = new string(split) + splitParamName.Style;
				splitParamName.Name = paramName.Replace(replaceString,"").Trim();
			}
			return splitParamName;
		}
	


		
		
		/// <summary>
		/// 将取出的html的属性值没有双引号的加上双引号
		/// </summary>
		/// <param name="oldHtmlString"></param>
		/// <returns></returns>
		public static string addDoubleQuotationMarks(string oldHtmlString)
		{
			string retHtmlString = "";
			string addPosition ; //添加双引号的位置，为空则不加，=“left”加在左边，=“right”加在右边
			bool hasBeginMark = false, hasEqualmark = false, hasDoubleQuotationMark = false;
			string lastS = "";//上一字符
			string s;         //当前字符
			string nextS;     //下一字符
			//HTML的Head加，只从BODY加
			int beginLoc = oldHtmlString.IndexOf("<body>",0);
			if(beginLoc == -1)
				beginLoc = oldHtmlString.IndexOf("<BODY>",0);
			//取到头
			if(beginLoc != -1)
				retHtmlString = oldHtmlString.Substring(0,beginLoc);
			else
				beginLoc = 0;
			for(int i = beginLoc ; i < oldHtmlString.Length; i++)
			{
				addPosition = "" ; //默认为不加双引号
				s = oldHtmlString.Substring(i,1);
				nextS = "";
				if(i+1 < oldHtmlString.Length)
					nextS = oldHtmlString.Substring(i+1,1);
				if((s == "<") && (nextS != "?"))
				{
					hasBeginMark = true;
					hasEqualmark = false;
				}
				if(s == "=")
				{
					hasEqualmark = true;
					if((hasBeginMark) && (nextS == " "))//忽略跟在等号后的空格
					{
						i++;
						if(i+1 < oldHtmlString.Length)
							nextS = oldHtmlString.Substring(i+1,1);
						else continue;
					}
				}
				if(hasBeginMark) //处在Element定义内
				{
					if(hasEqualmark) //出在等号后
					{
						switch(s)
						{
							case "=" ://等号后面应该由引号
								hasDoubleQuotationMark = false;//等号后是否有双引号:没有
								if(nextS != "\"")
									addPosition = "right";
								else
									hasDoubleQuotationMark = true;//有
								break;
							case " " ://如果是属性值，属性之间用空格隔开，空格前面应该由引号（但如果空格前面是冒号，则不加）
								hasEqualmark = false;
								if(nextS == " ") //忽略连续的空格
									continue;
								else if((lastS != "\"") && (lastS != ":") && (hasDoubleQuotationMark == false))
									addPosition = "left";
								break;
							case ">" :
								if(lastS != "\"")
									addPosition = "left";
								break;
						}
					}
				}
				if(s == ">")
				{
					hasBeginMark = false;
					hasEqualmark = false;
				}
				switch(addPosition.ToLower())
				{
					case "left" :
						retHtmlString += "\"" + s;
						break;
					case "right" :
						retHtmlString += s + "\"";
						break;
					default:
						retHtmlString += s;
						break;
				}
				lastS = s;  //上一字符
			}
			return retHtmlString;
		}


		/// <summary>
		/// 去点跟在</OBJECT>后面的回车(0d0a)
		/// </summary>
		/// <param name="htmlString"></param>
		/// <returns></returns>
		public static string removeObjectBackCR(string htmlString)
		{
			string xmlString = htmlString;
			while( 2>1)
			{
				string xmlStringNew = xmlString.Replace(" \n \n"," \n");
				if(xmlStringNew.Length < xmlString.Length)
				{
					xmlString = xmlStringNew;
				}
				else
				{
					break;
				}
			}
			while( 2>1)
			{
				string xmlStringNew = xmlString.Replace("</OBJECT>\r\n\r\n","</OBJECT>\r\n");
				if(xmlStringNew.Length < xmlString.Length)
				{
					xmlString = xmlStringNew;
				}
				else
				{
					break;
				}
			}
			while( 2>1)
			{
				string xmlStringNew = xmlString.Replace("</OBJECT>\n\n","</OBJECT>\n");
				if(xmlStringNew.Length < xmlString.Length)
				{
					xmlString = xmlStringNew;
				}
				else
				{
					break;
				}
			}
			return xmlString;
		}




		/// <summary>
		/// 将以<OBJECT ></OBJECT>之间的多余的空格和回车去掉
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string removeCRandBlank(string objectString)
		{
			string xmlString = objectString;
			//去掉OBJECT多余的空格和回车
			if(xmlString.StartsWith("<OBJECT "))
			{
				string oldString = xmlString;
				int len = oldString.Length;
				xmlString = "";
				for(int i=0; i< len; i++)
				{
					string c = oldString.Substring(i, 1);
					if(c == " ")//空格
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i+1, 1);
							if(nextC == " ")//空格
							{
								continue;
							}

						}
					}
					xmlString += c;
				}
				oldString = xmlString;
				len = oldString.Length;
				xmlString = "";
				//先去掉回车后的空格
				for(int i=0; i< len; i++)
				{
					string c = "";
					if(i >= len -1)
						c = oldString.Substring(i, 1);
					else
						c = oldString.Substring(i, 2);
					if(c == "\r\n")//空格
					{
						if(i < len -2)
						{
							string nextC = oldString.Substring(i + 2, 1);
							if(nextC == " ")//空格
							{
								continue;
							}

						}
					}
					xmlString += c;
					i+=c.Length -1;
				}
				oldString = xmlString;
				len = oldString.Length;
				xmlString = "";
				//先去掉\n后的空格
				for(int i=0; i< len; i++)
				{
					string c = oldString.Substring(i, 1);
					if(c == "\n")//\n
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i + 1, 1);
							if(nextC == " ")//空格
							{
								xmlString += c;
								continue;
							}

						}
					}
					xmlString += c;
				}
				oldString = xmlString;
				len = oldString.Length;
				xmlString = "";
				//先去掉多余的回车
				for(int i=0; i< len; i++)
				{
					string c = "";
					if(i >= len -1)
						c = oldString.Substring(i, 1);
					else
						c = oldString.Substring(i, 2);
					if(c == "\r\n")//回车
					{
						if(i < len -2)
						{
							string nextC = oldString.Substring(i + 2, 2);
							if(nextC == "\r\n")//回车
							{
								continue;
							}

						}
					}
					xmlString += c;
					i+=c.Length -1;
				}
				oldString = xmlString;
				len = oldString.Length;
				xmlString = "";
				for(int i=0; i< len; i++)
				{
					string c = oldString.Substring(i, 1);
					if(c == " ")//空格
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i+1, 1);
							if(nextC == " ")//空格
							{
								continue;
							}

						}
					}
					xmlString += c;
				}
				//DrmsMessageBox.Show(xmlString);
			}
			return xmlString;
		}

		

		/// <summary>
		/// 将以<OBJECT ></OBJECT>之间的多余的空格和回车去掉
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string removeObjectCRandBlank(string objectString)
		{
			string xmlString = objectString.Replace(" ", " ");  
			//去掉OBJECT多余的空格和回车
			if(xmlString.StartsWith("<OBJECT "))
			{
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("  "," ");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("\n ","\n");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("\n\n","\n");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("\r\n\r\n","\r\n");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace(" \n"," ");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("\t ","\t");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
				while( 2>1)
				{
					string xmlStringNew = xmlString.Replace("\t\t","\t");
					if(xmlStringNew.Length < xmlString.Length)
					{
						xmlString = xmlStringNew;
					}
					else
					{
						break;
					}
				}
			}
			return xmlString;
		}

		


		/// <summary>
		/// 将<OBJECT>的每个PARAM项没有结束标记“/”加上结束标记“/”
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string addEndMarks(string objectString, string startTag, string endTag)
		{
			//取选中的内容，并转换成HTML
			string htmlString = objectString;
			//加双引号
			//htmlString = Tools.addDoubleQuotationMarks(htmlString);
			//将PARAM拆分到数组
			string[,] paramString = Tools.getObjectFromHTML(htmlString, startTag, endTag);
			//将选中的Object的Param加上结束编辑标记
			string xmlString = "";
			int locParam = 0;
			for(int i=0; i <= paramString.GetUpperBound(0); i++)
			{
				int start = int.Parse(paramString[i,0]);
				int end = int.Parse(paramString[i,1]);
				//PARAM的前面部分
				xmlString += htmlString.Substring(locParam, start - locParam);
				locParam = end;
				string theParam = htmlString.Substring(start, end - start);
				if(theParam.EndsWith("/>"))
					xmlString += theParam;
				else
					xmlString += theParam.Substring(0, theParam.Length -1) + "/>";

			}
			//加剩余部分
			xmlString += htmlString.Substring(locParam);
			return xmlString;
		}


	

		/// <summary>
		/// 初始化WebServiceData，并返回一个WebServiceData类
		/// </summary>
		/// <returns></returns>
		public static WebServiceData initWebServiceData()
		{
			WebServiceData WebServiceData = new WebServiceData();
			WebServiceData.SiteId = "";
			WebServiceData.Opid = "";
			WebServiceData.Id = "";
			WebServiceData.ParentID = "";
			WebServiceData.Action = "";
			return WebServiceData;
		}

		
		/// <summary>
		/// 初始化发送文件的调用WebService类
		/// </summary>
		/// <param name="dataLength">发送的数据块的大小</param>
		/// <returns>FileSenderData类</returns>
		public static FileSenderData initFileSenderData(long dataLength)
		{
			FileSenderData fileSenderData = new FileSenderData();
			fileSenderData.Opid = "";
			fileSenderData.Action = "";
			fileSenderData.Type = "";
			fileSenderData.Token = "";
			fileSenderData.InfoBag = "";
			fileSenderData.Url = "";
			fileSenderData.FileSize = 0;
			fileSenderData.BlockSize = 0;
			fileSenderData.OffSet = 0;
			fileSenderData.Data = new byte[dataLength];
			return fileSenderData;
		}

		
		/// <summary>
		/// 初始化一个在页面编辑器中的Table属性，并返回一个InsertTableParam类(参数)
		/// </summary>
		/// <returns></returns>
		public static InsertTableParam initInsertTableParam()
		{
			InsertTableParam tagInsertTableParam = new InsertTableParam();
			tagInsertTableParam.Caption = "";
			tagInsertTableParam.NumRows = 2;
			tagInsertTableParam.NumCols = 3;
			tagInsertTableParam.TableAttrs = "border=1 cellPadding=1 cellSpacing=1 width=75%";
			tagInsertTableParam.CellAttrs = "";
			return tagInsertTableParam;
		}

		


		/// <summary>
		/// 初始化AuditInfo(操作审计类)，并返回一个AuditInfo类
		/// </summary>
		/// <returns></returns>
		public static AuditInfo initAuditInfo()
		{
			AuditInfo auditInfo = new AuditInfo();
			//操作类型的基本信息
			auditInfo.ClassNo = 0;
			auditInfo.ClassID = "";
			auditInfo.ClassDescription = "";
			//操作动作的基本信息
			auditInfo.MethodNo = 0;
			auditInfo.MethodName = "";
			auditInfo.MethodDescription = "";
			auditInfo.MethodLevel = 0;
			auditInfo.MethodMemo = "";
			//审计序号、操作时间和IP
			auditInfo.AuditNo = 0;
			auditInfo.OperationIP = "";
			auditInfo.OperationTime = "";
			//用户信息
			auditInfo.UserName = "";
			auditInfo.PassWord = "";
			auditInfo.Token = "";
			auditInfo.LoginTime = System.DateTime.Now;
			//操作对象
			auditInfo.Opid = "AUDIT";
			auditInfo.Action = "";
			auditInfo.ParentGuid = "";
			auditInfo.Guid = "";
			//操作内容
			auditInfo.OperationContent = "";
			auditInfo.OperationResult = "";
			auditInfo.AuditMemo = "";
			//查询条件
			auditInfo.SQL = "";
			auditInfo.SortField = "";
			auditInfo.PageNum = 20;
			auditInfo.StartLoc = 0;
			auditInfo.RecNum = 0;
			auditInfo.PageNo = 0;
			return auditInfo;
		}



		/// <summary>
		/// 初始化ReporterInfo(报送信息类)，并返回一个ReporterInfo类
		/// </summary>
		/// <returns></returns>
		public static ReporterInfo initReporterInfo()
		{
			ReporterInfo reporterInfo = new ReporterInfo();
			reporterInfo.UserName = "";
			reporterInfo.PassWord = "";
			reporterInfo.Token = "";
			reporterInfo.LoginTime = System.DateTime.Now;//登录时间
			reporterInfo.OnLine = false;//默认为脱机
			reporterInfo.PassValidate = false;//默认为没有通过验证
			reporterInfo.Path = Tools.getPathParent(System.AppDomain.CurrentDomain.BaseDirectory) + "cache\\report\\";//默认目录
			reporterInfo.Opid = "";
			reporterInfo.Action = "";
			reporterInfo.ResCatGuid = "";
			reporterInfo.ContentTypeGuid = "";
			reporterInfo.DocGuid = "";
			reporterInfo.AttachGuid = "";
			reporterInfo.ContentTypeName = "";
			reporterInfo.DocHTML = "";
			reporterInfo.ReportXML = "";
			reporterInfo.SQL = "";
			reporterInfo.SortField = "";
			reporterInfo.PageNum = 20;
			reporterInfo.StartLoc = 0;
			reporterInfo.RecNum = 0;
			reporterInfo.PageNo = 0;
			return reporterInfo;
		}


		/// <summary>
		/// 初始化UserLoginInfo，并返回一个UserLoginInfo类
		/// </summary>
		/// <returns></returns>
		public static UserLoginInfo initUserLoginInfo()
		{
			UserLoginInfo userLoginInfo = new UserLoginInfo();
			userLoginInfo.UserName = "";
			userLoginInfo.PassWord = "";
			userLoginInfo.Token = "";
			userLoginInfo.Roles = "";
			userLoginInfo.ResGuid = "";
			userLoginInfo.ResType = "";
			userLoginInfo.ResPowerValue = "";
			userLoginInfo.LoginTime = System.DateTime.Now;
			return userLoginInfo;
		}



		/// <summary>
		/// 初始化UserLoginInfo，并返回一个UserLoginInfo类
		/// </summary>
		/// <returns></returns>
		public static UserLoginInfo initUserLoginInfo(UserLoginInfo oldUserInfo)
		{
			UserLoginInfo userLoginInfo = new UserLoginInfo();
			userLoginInfo.UserName = oldUserInfo.UserName;
			userLoginInfo.PassWord = oldUserInfo.PassWord;
			userLoginInfo.Token = oldUserInfo.Token;
			userLoginInfo.Roles = oldUserInfo.Roles;
			userLoginInfo.ResGuid = oldUserInfo.ResGuid;
			userLoginInfo.ResType = oldUserInfo.ResType;
			userLoginInfo.ResPowerValue = oldUserInfo.ResPowerValue;
			userLoginInfo.LoginTime = oldUserInfo.LoginTime;
			return userLoginInfo;
		}



		/// <summary>
		/// 初始化ListItemData，并返回一个ListItemData类
		/// </summary>
		/// <param name="fieldNum"></param>
		/// <returns></returns>
		public static ListItemData initTagListItemData(int fieldNum)
		{
			ListItemData listItemData = new ListItemData();
			listItemData.formFieldNum = fieldNum;
			listItemData.formFieldLength = 0;
			listItemData.maxTitleLength = 10;
			listItemData.SiteId = "";
			listItemData.Opid = "";
			listItemData.Id = "";
			listItemData.IsTree = false;
			listItemData.ParentID = "";
			listItemData.ParentPath = "";
			listItemData.Action = "";
			listItemData.ActionDes = "";
			listItemData.Title = "";
			listItemData.TableName = "";
			listItemData.InputFieldValue = new string[listItemData.formFieldNum, 7];
			listItemData.DictObject = new object[listItemData.formFieldNum];
			listItemData.ReadOnly = new bool[listItemData.formFieldNum];
			listItemData.RelationFieldName = new string[listItemData.formFieldNum, 4];
			for(int i = 0 ; i < listItemData.formFieldNum ; i++)
			{
				listItemData.InputFieldValue[i,0] = "" ;  //字段标题
				listItemData.InputFieldValue[i,1] = "" ;  //字段名称
				listItemData.InputFieldValue[i,2] = "" ;  //字段内容
				listItemData.InputFieldValue[i,3] = "" ;  //字段长度
				listItemData.InputFieldValue[i,4] = "" ;  //正则表达式
				listItemData.InputFieldValue[i,5] = "" ;  //字段类型
				listItemData.InputFieldValue[i,6] = "" ;  //字段的显示风格，即控件类
				listItemData.DictObject[i] = null;        //字段的字典
				listItemData.ReadOnly[i] = false;         //默认不是只读
				listItemData.RelationFieldName[i, 0] = "";   //关联表名称
				listItemData.RelationFieldName[i, 1] = "";   //关联字段名称
				listItemData.RelationFieldName[i, 2] = "";   //关联的保存字段名称
				listItemData.RelationFieldName[i, 3] = "";   //关联的显示字段名称
			}
			listItemData.IDENTITY_Name = "PkID";
			listItemData.IDENTITY_Value = 0;
			return listItemData;
		}



		/// <summary>
		/// 将原来的ListItemData类复制成一个新的ListItemData类，并返回一个ListItemData类
		/// </summary>
		/// <param name="oldData">原来的ListItemData类</param>
		/// <returns>新的ListItemData类</returns>
		public static ListItemData initTagListItemData(ListItemData oldData)
		{
			ListItemData listItemData = new ListItemData();
			listItemData.formFieldNum = oldData.formFieldNum;
			listItemData.formFieldLength = oldData.formFieldLength;
			listItemData.maxTitleLength = oldData.maxTitleLength;
			listItemData.SiteId = oldData.SiteId;
			listItemData.Opid = oldData.Opid;
			listItemData.Id = oldData.Id;
			listItemData.IsTree = oldData.IsTree;
			listItemData.ParentID = oldData.ParentID;
			listItemData.ParentPath = oldData.ParentPath;
			listItemData.Action = oldData.Action;
			listItemData.ActionDes = oldData.ActionDes;
			listItemData.Title = oldData.Title;
			listItemData.TableName = oldData.TableName;
			listItemData.InputFieldValue = new string[listItemData.formFieldNum, 7];
			listItemData.DictObject = new object[listItemData.formFieldNum];
			listItemData.ReadOnly = new bool[listItemData.formFieldNum];
			listItemData.RelationFieldName = new string[listItemData.formFieldNum, 4];
			for(int i = 0 ; i < listItemData.formFieldNum ; i++)
			{
				listItemData.InputFieldValue[i,0] = oldData.InputFieldValue[i,0];  //字段标题
				listItemData.InputFieldValue[i,1] = oldData.InputFieldValue[i,1];  //字段名称
				listItemData.InputFieldValue[i,2] = oldData.InputFieldValue[i,2];  //字段内容
				listItemData.InputFieldValue[i,3] = oldData.InputFieldValue[i,3];  //字段长度
				listItemData.InputFieldValue[i,4] = oldData.InputFieldValue[i,4];  //正则表达式
				listItemData.InputFieldValue[i,5] = oldData.InputFieldValue[i,5];  //字段类型
				listItemData.InputFieldValue[i,6] = oldData.InputFieldValue[i,6];  //字段的显示风格，即控件类
				listItemData.DictObject[i] = oldData.DictObject[i];        //字段的字典
				listItemData.ReadOnly[i] = oldData.ReadOnly[i];         //默认不是只读
				listItemData.RelationFieldName[i, 0] = oldData.RelationFieldName[i, 0];   //关联表名称
				listItemData.RelationFieldName[i, 1] = oldData.RelationFieldName[i, 1];   //关联字段名称
				listItemData.RelationFieldName[i, 2] = oldData.RelationFieldName[i, 2];   //关联的保存字段名称
				listItemData.RelationFieldName[i, 3] = oldData.RelationFieldName[i, 3];   //关联的显示字段名称
			}
			listItemData.IDENTITY_Name = oldData.IDENTITY_Name;
			listItemData.IDENTITY_Value = oldData.IDENTITY_Value;
			return listItemData;
		}




		/// <summary>
		/// 初始化FindRichTextBoxData，并返回一个FindRichTextBoxData类
		/// </summary>
		/// <returns></returns>
		public static FindRichTextBoxData initFindRichTextBoxData()
		{
			FindRichTextBoxData findInfo = new FindRichTextBoxData();
			findInfo.Text = "";
			findInfo.MatchCase = false;
			findInfo.WholeWord = false;
			findInfo.IsUp = false;
			return findInfo;
		}




	}


	
	
	//以上是过程方法
	//以下是自定义的类
	



	/// <summary>
	/// ListView结点增加的一些属性
	/// 当选择TreeView时，将其初始化(字段，值)
	/// 当选中ListView的某条记录时，将其内容更新(值)
	/// </summary>
	public class ListItemData
	{
		public string SiteId;//选取的站点ID
		public string Opid;
		public string Id;
		public string ParentID;//父结点ID
		public string ParentPath;//父结点Path
		public bool IsTree;//该结点是否是棵树
		public string TableName;
		public string Title; //从XML取到的显示到Tree的字段对应的标题，用来设置ListView 的column标题
		public string Action;  //用户所做的操作,取name的 值
		public string ActionDes;  //用户所做的操作,取desciption的 值
		public int formFieldNum ; //表单里能创建的字段的个数
		public int formFieldLength ; //表单里能创建的字段的总长度
		public int maxTitleLength ; //表单里能创建的字段标题的最大长度
		public object[] DictObject; //字典字段，字段的显示内容和数据库里存放的内容数组[ShowField,CodeField]
		public bool[] ReadOnly; //只读
		public string[,] RelationFieldName; //存放关联的子表的树型，分别是：表名称、关联的字段名称、保存的字段名称、显示的字段名称
		public string[,] InputFieldValue ;//存放字段属性，分别是：字段标题、字段名称、字段内容、字段长度、正则表达式、字段类型、字段的显示风格，即用什么控件显示
		public string IDENTITY_Name;//主键字段，系统自动生成的IDENTITY字段的名称
		public int IDENTITY_Value;//主键字段，系统自动生成的IDENTITY字段的内容
		
		public ListItemData()
		{}

		public ListItemData(string siteId, string opid,string id, bool isTree,string parentID, string parentPath, 
			string[,] inputFieldValue, int FormFieldNum, int FormFieldLength, int MaxTitleLength, 
			string tableName, string action, 
			string title,string actionDes, object[] dictObject,bool[] readOnly, string[,] relationFieldName,
			string iDENTITY_Name, int iDENTITY_Value)
		{
			SiteId = siteId;
			Opid = opid;
			Id = id;
			IsTree = isTree;
			ParentID = parentID;
			ParentPath = parentPath;
			TableName = tableName ;
			formFieldNum = FormFieldNum ;
			formFieldLength = FormFieldLength;
			maxTitleLength = MaxTitleLength;
			Title = title;
			Action = action;
			ActionDes = actionDes;
			DictObject = dictObject;
			InputFieldValue = new string[formFieldNum,7];
			DictObject = new object[formFieldNum];
			ReadOnly = new bool[formFieldNum];
			RelationFieldName = new string[formFieldNum, 4];
			for(int i=0 ; i < formFieldNum ;i++)
			{
				InputFieldValue[i,0] = inputFieldValue[i,0];  //字段标题
				InputFieldValue[i,1] = inputFieldValue[i,1];  //字段名称
				InputFieldValue[i,2] = inputFieldValue[i,2];  //字段内容
				InputFieldValue[i,3] = inputFieldValue[i,3];  //字段长度
				InputFieldValue[i,4] = inputFieldValue[i,4];  //正则表达式
				InputFieldValue[i,5] = inputFieldValue[i,5];  //字段类型
				InputFieldValue[i,6] = inputFieldValue[i,6];  //字段的显示风格，即用什么控件显示
				DictObject[i] = dictObject[i];                //字段的字典
				ReadOnly[i] = readOnly[i];                    //是否只读
				RelationFieldName[i, 0] = relationFieldName[i,0];  //关联的表名称
				RelationFieldName[i, 1] = relationFieldName[i,1];  //关联的字段名称
				RelationFieldName[i, 2] = relationFieldName[i,2];  //关联的保存的字段名称
				RelationFieldName[i, 3] = relationFieldName[i,3];  //关联的显示的字段名称
			}
			IDENTITY_Name = iDENTITY_Name;
			IDENTITY_Value = iDENTITY_Value;
		}
	}



	/// <summary>
	/// 调用WebService参数
	/// </summary>
	public class WebServiceData
	{
		public string SiteId;//选取的站点ID
		public string Opid;
		public string Id;//选取ID
		public string ParentID;//父结点ID
		public string Action;  //用户所做的操作,取name的 值
		public WebServiceData()
		{}

		public WebServiceData(string siteId, string opid,string id, string parentID, string action)
		{
			SiteId = siteId;
			Opid = opid;
			Id = id;
			ParentID = parentID ;
			Action = action;
		}
	}


	/// <summary>
	/// 调用WebService发送文件的参数
	/// </summary>
	public class FileSenderData
	{
		public string Opid;    //操作类型，如：FILESENDER
		public string Action;  //用户所做的操作,取WebService方法
		public string Url;     //WebService方法在的URL
		public string Type;    //发送文件的类型
		public string Token;   //文件的token
		public string InfoBag; //文件的基本信息包
		public long FileSize;  //要发送的文件大小
		public long BlockSize; //要发送的文件块大小
		public long OffSet;    //要发送的文件的偏移量
		public byte[] Data;    //要发送的文件内容
		public FileSenderData()
		{}

		public FileSenderData(string opid,string action,string url,string type,string token,string infoBag,long fileSize,long blockSize,long offSet,byte[] data)
		{
			Opid = opid;
			Action = action;
			Url = url;
			Type = type;
			Token = token;
			InfoBag = infoBag;
			FileSize = fileSize;
			BlockSize = blockSize;
			OffSet = offSet;
			Data = data;
		}
	}
	

	/// <summary>
	/// 页面编辑器中的Table属性(插入表格参数)
	/// </summary>
	public class InsertTableParam
	{
		public string Caption;//
		public int NumRows;//
		public int NumCols ;//
		public string TableAttrs;//
		public string CellAttrs;//
		public InsertTableParam()
		{}

		public InsertTableParam(string caption, int numRows,int numCols, string tableAttrs, string cellAttrs)
		{
			Caption = caption;
			NumRows = numRows;
			NumCols = numCols;
			TableAttrs = tableAttrs ;
			CellAttrs = cellAttrs;
		}
	}



	/// <summary>
	/// 拆分PARAM的NAME，将NAME中含有的NAME和STYLE分开
	/// </summary>
	public class SplitParamName
	{
		public string Name; //名称
		public string Style;//显示风格，如CHANNEL,RESDIR
		public char[] Split;//分隔符
		public SplitParamName()
		{}

		public SplitParamName(string name, string style, char[] split)
		{
			Name = name;
			Style = style;
			Split = split;
		}
	}
	


	/// <summary>
	/// 拆分带路径的文件Path和FileName两部分
	/// </summary>
	public class SplitFileName
	{
		public string Path;//文件路径
		public string FileName; //文件名称
		public char[] Split;//分隔符
		public SplitFileName()
		{}

		public SplitFileName(string path, string fileName, char[] split)
		{
			Path = path;
			FileName = fileName;
			Split = split;
		}
	}


	/// <summary>
	/// 上载资源文件需要的一些参数
	/// </summary>
	public class UpLoadResFileData
	{
		public string CacheRisPath;  //资源文件存放的临时路径,由主程序所在路径+siteID组成
		public string SourceRisPath; //源资源文件所在的绝对路径，如c:\site\images\
		public UpLoadResFileData()
		{}

		public UpLoadResFileData(string cacheRisPath, string sourceRisPath)
		{
			CacheRisPath = cacheRisPath;
			SourceRisPath = sourceRisPath;
		}
	}

	
	/// <summary>
	/// 字段类
	/// </summary>
	public class FieldInfo
	{
		public string FieldName;  //字段名称
		public string FieldValue; //字段内容
		public FieldInfo()
		{}

		public FieldInfo(string fieldName, string fieldValue)
		{
			FieldName = fieldName;
			FieldValue = fieldValue;
		}
	}

	
	

	/// <summary>
	/// 操作审计类
	/// </summary>
	public class AuditInfo
	{
		public int ClassNo;//操作类型序号
		public string ClassID;//操作类型标识
		public string ClassDescription;//操作类型描述

		public int MethodNo;//操作动作序号
		public string MethodName;//操作动作标识
		public string MethodDescription;//操作动作描述
		public int MethodLevel;//操作动作级别
		public string MethodMemo;//操作动作备注
		
		public int AuditNo;//审计序号
		public string OperationIP;//操作机器IP
		public string OperationTime;//
		
		public string UserName;//操作员：用户名，即ID
		public string PassWord;//密码
		public string Token; //token
		public DateTime LoginTime;//登录时间

		public string Opid;    //操作类型，如：RESDIR
		public string Action;  //用户所做的操作,取WebService方法
		public string ParentGuid; //当前操作数据的父亲guid
		public string Guid; //当前操作数据的guid

		public string OperationContent; //操作内容：操作的标题
		public string OperationResult; //操作结果
		public string AuditMemo; //备注

		public string SQL; //查询条件的where子句
		public string SortField; //查询的排序字段
		public int PageNum; //每次查询取回的记录数
		public int StartLoc; //每次查询取回的记录起始记录号
		public int RecNum;//满足条件的总记录数
		public int PageNo; //当前显示的页码

		public AuditInfo()
		{}

		public AuditInfo(int classNo, string classID, string classDescription,
			int methodNo, string methodName, string methodDescription, int methodLevel, string methodMemo,
			int auditNo, string operationIP, string operationTime,
			string userName, string passWord, string token, DateTime loginTime,
			string opid, string action, string parentGuid, string guid,
			string operationContent, string operationResult, string auditMemo,
			string sql, string sortField, int pageNum, int startLoc, int recNum, int pageNo)
		{
			//操作类型的基本信息
			ClassNo = classNo;
			ClassID = classID;
			ClassDescription = classDescription;
			//操作动作的基本信息
			MethodNo = methodNo;
			MethodName = methodName;
			MethodDescription = methodDescription;
			MethodLevel = methodLevel;
			MethodMemo = methodMemo;
			//操作时间和IP
			AuditNo = auditNo;
			OperationIP = operationIP;
			OperationTime = operationTime;
			//用户信息
			UserName = userName;
			PassWord = passWord;
			Token = token;
			LoginTime = loginTime;
			//操作对象
			Opid = opid;
			Action = action;
			ParentGuid = parentGuid;
			Guid = guid;
			//操作内容
			OperationContent = operationContent;
			OperationResult = operationResult;
			AuditMemo = auditMemo;
			//查询条件
			SQL = sql;
			SortField = sortField;
			PageNum = pageNum;
			StartLoc = startLoc;
			RecNum = recNum;
			PageNo = pageNo;
		}
	}




	/// <summary>
	/// 报送信息类
	/// </summary>
	public class ReporterInfo
	{
		public string UserName;//用户名，即ID
		public string PassWord;//密码
		public string Token; //token
		public DateTime LoginTime;//登录时间
		public bool OnLine;    //是否是联机
		public bool PassValidate;//是否通过验证，用户第一次使用时，可以不通过验证，进入系统后，通过联机后设置验证码
		public string Path;      //脱机报送时，临时文件存放到的目录...cache\report
		public string Opid;    //操作类型，如：RESDIR
		public string Action;  //用户所做的操作,取WebService方法
		public string ResCatGuid; //资源目录guid
		public string ContentTypeGuid; //内容类型guid
		public string DocGuid; //报送记录的guid
		public string AttachGuid; //报送记录附件的guid
		public string ContentTypeName; //内容类型名称
		public string DocHTML; //正文的内容，HTML格式
		public string ReportXML; //报送基本信息，XML格式
		public string SQL; //查询条件的where子句
		public string SortField; //查询的排序字段
		public int PageNum; //每次查询取回的记录数
		public int StartLoc; //每次查询取回的记录起始记录号
		public int RecNum;//满足条件的总记录数
		public int PageNo; //当前显示的页码

		public ReporterInfo()
		{}

		public ReporterInfo(string userName, string passWord, string token, DateTime loginTime,
			bool onLine, bool passValidate, 
			string path, string opid, string action, 
			string resCatGuid, string contentTypeGuid, 	string docGuid, string attachGuid, 
			string contentTypeName, string docHTML, string reportXML, string sql, string sortField,
			int pageNum, int startLoc, int recNum, int pageNo)
		{
			UserName = userName;
			PassWord = passWord;
			Token = token;
			LoginTime = loginTime;
			OnLine = onLine;
			PassValidate = passValidate;
			Path = path;
			Opid = opid;
			Action = action;
			ResCatGuid = resCatGuid;
			ContentTypeGuid = contentTypeGuid;
			DocGuid = docGuid;
			AttachGuid = attachGuid;
			ContentTypeName = contentTypeName;
			DocHTML = docHTML;
			ReportXML = reportXML;
			SQL = sql;
			SortField = sortField;
			PageNum = pageNum;
			StartLoc = startLoc;
			RecNum = recNum;
			PageNo = pageNo;
		}
	}



	/// <summary>
	/// TextBox文本输入框增加的一些属性，存放到TextBox的TAG中
	/// </summary>
	public class TextBoxData
	{
		public string Title;  //对应Label标题
		public string Opid;
		public string Id;
		public TextBoxData()
		{}

		public TextBoxData(string title, string opid, string id)
		{
			Title = title;
			Opid = opid;
			Id = id;

		}

	}


	/// <summary>
	/// RichTextBox的文本查找输入的一些属性
	/// </summary>
	public class FindRichTextBoxData
	{
		public string Text;  //查找内容
		public bool MatchCase;//区分大小写
		public bool WholeWord;//全字匹配
		public bool IsUp;//方向，true=UP;false=Down
		public FindRichTextBoxData()
		{}

		public FindRichTextBoxData(string text, bool matchCase, bool wholeWord, bool isUp)
		{
			Text = text;
			MatchCase = matchCase;
			WholeWord = wholeWord;
			IsUp = isUp;
		}

	}








}
