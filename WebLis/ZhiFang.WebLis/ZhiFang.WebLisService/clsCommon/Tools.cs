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
using System.Runtime.InteropServices;//����DLL��



namespace ZhiFang.WebLisService.clsCommon
{
	/// <summary>
	/// Tools ��ժҪ˵����
	/// 
	/// �޸���ʷ
	/// </summary> 
	
	public class Tools
	{

		public Tools()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}



        /// <summary>
        /// ת����ϣ��ļ�ֵΪ��д
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
        /// ��ĳ���ַ���д��ָ�����ļ��У��ļ��ı���ΪUTF8
        /// </summary>
        /// <param name="file">ָ�����ļ�����</param>
        /// <param name="text">д���ļ���ָ���ı�</param>
        public static void writeStringToLocalFile1(string file, string text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //����Ŀ¼
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //ɾ�����ļ�
                FileAttributes oldAttr = FileAttributes.Normal;
                if (System.IO.File.Exists(file))
                {
                    //ȡԭ���ļ�������
                    oldAttr = System.IO.File.GetAttributes(file);
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, false, Encoding.UTF8);//���´���
                fileWriter.Write(text);
                fileWriter.Close();
                //�������ļ�������
                System.IO.File.SetAttributes(file, oldAttr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
        }




        /// <summary>
        /// ��ĳ���ַ���д��ָ�����ļ��У��ļ��ı���ΪUTF8
        /// </summary>
        /// <param name="file">ָ�����ļ�����</param>
        /// <param name="text">д���ļ���byte����</param>
        public static void writeBytesToLocalFile(string file, byte[] text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //����Ŀ¼
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //ɾ�����ļ�
                FileAttributes oldAttr = FileAttributes.Normal;
                if (System.IO.File.Exists(file))
                {
                    //ȡԭ���ļ�������
                    oldAttr = System.IO.File.GetAttributes(file);
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.FileStream fileStream = new System.IO.FileStream(file, FileMode.OpenOrCreate);
                fileStream.Write(text, 0, text.Length);
                fileStream.Close();
                //�������ļ�������
                System.IO.File.SetAttributes(file, oldAttr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
        }



        /// <summary>
        /// �������ڿ�ܺͿ��֮�䴫�ݵ�ҳ��URL���н���
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string urlEscape(string url)
        {
            //��LIKE���н���
            string urlNew = url;
            urlNew = urlNew.Replace("+LIKE+", " LIKE ");
            //���ֺŽ��н���
            urlNew = urlNew.Replace("%3b", ";");
            //�������Ž��н���
            urlNew = urlNew.Replace("&apos;", "'");
            //���ո���н���
            urlNew = urlNew.Replace("%20", " ");
            //���ٷֺŽ��н���
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
        /// ����ַ�������ϣ��:���մ�ɾ��
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
        /// ��һ��ʱ��ת�����ַ���
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns></returns>
        public static string getTowTimeString(DateTime dtBegin, DateTime dtEnd)
		{
			string useTimes = "";
			System.TimeSpan ts = dtEnd - dtBegin;
			if(ts.Days > 0)
			{
				useTimes += ts.Days.ToString() + "��";
			}
			if(ts.Hours > 0)
			{
				useTimes += ts.Hours.ToString() + "Сʱ";
			}
			if(ts.Minutes > 0)
			{
				useTimes += ts.Minutes.ToString() + "����";
			}
			if(ts.Seconds > 0)
			{
				useTimes += ts.Seconds.ToString() + "��";
			}
			if(ts.Milliseconds > 0)
			{
				//����
				if(useTimes == "")
					useTimes += ts.Milliseconds.ToString() + "����";
			}
			return useTimes;
		}



		/// <summary>
		/// �ӱ����ļ���ȡ���е�����,��������쳣������null
		/// </summary>
		/// <param name="file"></param>
		/// <returns>�ļ�������</returns>
		public static string readFromLocalFile(string file)
		{
			string text = "";
			try
			{
				if(!System.IO.File.Exists(file))
					return null;
				System.IO.StreamReader fileReader = new System.IO.StreamReader(file,Encoding.Default);
				//ȡ�ļ�����
				text = fileReader.ReadToEnd();
				fileReader.Close();
			}
			catch(System.Exception ex)
			{
				throw new Exception("���ļ���" + file + "����\n" + ex.Message);
			}
			return text;
		}





        /// <summary>
        /// �ӱ����ļ���ȡ���е�����,��������쳣������null
        /// </summary>
        /// <param name="path"></param>
        /// <returns>�ļ�������</returns>
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
		/// ��ĳ���ַ���д��ָ�����ļ��У��ļ��ı���ΪUTF8
		/// </summary>
		/// <param name="file">ָ�����ļ�����</param>
		/// <param name="text">д���ļ���ָ���ı�</param>
		public static void writeStringToLocalFile(string file, string text)
		{
			try
			{
				SplitFileName splitFileName = Tools.getSplitFileName(file, new char[]{'\\'});
				string path = splitFileName.Path;
				//����Ŀ¼
				if(path.Length > 0)
				{
					if(!System.IO.Directory.Exists(path))
						System.IO.Directory.CreateDirectory(path);
				}
				//ɾ�����ļ�
				if(System.IO.File.Exists(file))
				{
					System.IO.File.SetAttributes(file, FileAttributes.Normal);
					System.IO.File.Delete(file);
				}
				System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, false,Encoding.UTF8);//���´���
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
        /// ��ȡӦ�ó�������Ŀ¼
        /// </summary>
        /// <returns></returns>
		public static string getSystemBasePath()
		{
			return getPathParent(System.AppDomain.CurrentDomain.BaseDirectory);
		}




	

		/// <summary>
		/// �������ļ�ȡϵͳ�ı���
		/// </summary>
		/// <returns></returns>
		public static string getSystemTitle()
		{
			string title = System.Configuration.ConfigurationSettings.AppSettings["System.Title.Name"].ToString();
			return title;
		}

		
		/// <summary>
		/// ȡĳ��Ŀ¼�ĸ�Ŀ¼
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
		/// ����ĳ���ļ� 
		/// </summary>
		/// <param name="url">Ҫ���ص��ļ��ķ��������ӵ�ַ</param>
		/// <param name="destFile">Ҫ����ı����ļ����ƣ�����������·����</param>
		public static bool downLoadFile(string url, string destFile)
		{
			try
			{
				WebClient myWebClient = new WebClient();
				SplitFileName  spfFile = Tools.getSplitFileName(destFile, new char[]{'\\'});
				//�ȴ���Ŀ¼
				if(!System.IO.Directory.Exists(spfFile.Path))
					System.IO.Directory.CreateDirectory(spfFile.Path);
				//����ļ��Ѿ����ڣ�������������ϵͳ�ܶԸ��ļ����и���
				if(System.IO.File.Exists(destFile))
				{
					System.IO.File.SetAttributes(destFile, System.IO.FileAttributes.Normal);
					//��ɾ��Ŀ���ļ�
					System.IO.File.SetAttributes(destFile, System.IO.FileAttributes.Archive);
					System.IO.File.Delete(destFile);
				}
				myWebClient.DownloadFile(url, destFile);
			}
			catch(System.Net.WebException ex)
			{
				//�����쳣
				throw ex;
			}
			return true;
		}


		/// <summary>
		/// ��ָ����XML�ҳ����е�Item��������
		/// </summary>
		/// <param name="xmlFileName">����ֵ�ı����ļ�����</param>
		/// <returns>��ά��ֵ����һάΪ���ƴ�name��ȡ���ڶ�άΪ�����value��ȡ������άΪĬ��ֵ�ı�־</returns>
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
					retString[2, itemNum] = node.InnerXml;//Ĭ��ֵ�ı�־
					if( node.Attributes.Count > 0)
					{
						retString[1, itemNum] = node.Attributes.GetNamedItem("name").Value;
						retString[0, itemNum] = node.Attributes.GetNamedItem("value").Value;
					}
					itemNum ++;
				}
				//�ͷű���
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
		/// ��ĳ���ַ����н�ȡĳ�Ӵ�
		/// </summary>
		/// <param name="oldString"></param>
		/// <param name="tagStart">�Ӵ���ǰ��</param>
		/// <param name="tagEnd">�Ӵ��ĺ���</param>
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
		/// ��ָ��HTML�ı��к���<OBJECT>�Ĳ��ַ���(<OBJECT>������ɲ�������)
		/// ����<OBJECT>������ÿ��<OBJECT>����ԭ������ʼλ�úͽ�ֹλ��
		/// ÿ��<OBJECT>�ĸ�ʽΪ����ʼλ�á���ֹλ�á�<OBJECT>�ı�
		/// </summary>
		/// <param name="objectString">ָ����HTML</param>
		/// <param name="tagStart">OBJECT�Ŀ�ʼ���</param>
		/// <param name="tagEnd">OBJECT�Ľ������</param>
		/// <returns></returns>
		public static string[,] getObjectFromHTML(string objectString, string tagStart, string tagEnd)
		{
			string tmpStringRight = "";
			string[,] retHtmlString = new string[0,3] ;//���ص�����
			int objectLocationBegin;//������OBJECT����ʼλ��
			int objectLocationEnd;//������OBJECT�Ľ���λ��
			int objectNum;
			try
			{
				//�ȱ���OBJECT���鿴���м���OBJECT
				string tmpFindString = objectString;
				objectNum = 0;
				objectLocationBegin = 0;//������OBJECT����ʼλ��
				objectLocationEnd = 0;//������OBJECT�Ľ���λ��
				while((objectLocationBegin = tmpFindString.IndexOf(tagStart)) != -1)
				{
					tmpStringRight = tmpFindString.Substring(objectLocationBegin);
					objectLocationEnd = tmpStringRight.IndexOf(tagEnd);
					if(objectLocationEnd != -1) //�ҵ��������
					{
						objectNum ++;
						tmpFindString = tmpStringRight.Substring(objectLocationEnd + tagEnd.Length); //����ƥ���ұ�
					}
					else
						tmpFindString = tmpFindString.Substring(objectLocationBegin + tagStart.Length); //����ƥ���ұ�
				}
				if(objectNum == 0)
					return retHtmlString;
				//�������ŵ�����
				retHtmlString = new string[objectNum,3];
				tmpFindString = objectString;
				objectLocationBegin = 0;//������OBJECT����ʼλ��
				objectLocationEnd = 0;//������OBJECT�Ľ���λ��
				objectNum = 0;
				while((objectLocationBegin = tmpFindString.IndexOf(tagStart, objectLocationEnd)) != -1)
				{
					objectLocationEnd = tmpFindString.IndexOf(tagEnd, objectLocationBegin + tagStart.Length);
					if(objectLocationEnd != -1) //�ҵ��������
					{
						//Object����
						retHtmlString[objectNum, 2] = tmpFindString.Substring(objectLocationBegin, objectLocationEnd - objectLocationBegin + tagEnd.Length);
						retHtmlString[objectNum, 0] = objectLocationBegin.ToString(); //��ʼλ��
						retHtmlString[objectNum, 1] = (objectLocationEnd + tagEnd.Length).ToString();//����λ��
						objectNum ++;
					}
				}
			}
			catch//(System.Exception ex)
			{
				//DrmsMessageBox.Show("����HTML����",ex);
				throw;
			}
			return retHtmlString;
		}

		


		/// <summary>
		/// ��tagListItemData������ָ���ֶζ�Ӧ�ı���
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="tagListItemData"></param>
		/// <returns></returns>
		public static string getTitlefromName(string fieldName , ListItemData tagListItemData)
		{
			for(int i = 0 ; i < tagListItemData.formFieldNum ; i++)
			{
				if (tagListItemData.InputFieldValue[i,1] == fieldName)
					return tagListItemData.InputFieldValue[i,0] ;  //�ֶα���
			}
			return "";
		}


		/// <summary>
		/// �Ӳ���Ԫ��������ģ�棨�ֶΣ�ȡ�����ֶε����ƺ����������
		/// </summary>
		/// <param name="xmlString"></param>
		/// <param name="fieldNameTag">�ֶ����Ƶı�ǣ��ڱ�ϵͳ����<fieldname></fieldname>��ʾ</param>
		/// <param name="fieldValueTag">�ֶ����ݵı�ǣ��ڱ�ϵͳ����<PARAM name="textValue" value="���������"></PARAM></param>
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
				//ȡ�ֶ����������
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
				//ȡ�ֶ�����
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
				//�ͷű���
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
		/// ����ļ�Ϊ·�����ļ�����������
		/// ����SplitFileName��
		/// </summary>
		/// <param name="fileName">�ļ����ƣ���·����</param>
		/// <param name="split">�ָ�����һ��Ϊ��/��</param>
		/// <returns>SplitFileName��</returns>
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
				splitFileName.FileName = splitArray[splitArray.Length-1];//�ļ�����
				if(splitFileName.FileName == "")
					splitFileName.Path = fileName;
				else
					splitFileName.Path = fileName.Replace(splitFileName.FileName,"");
			}
			return splitFileName;
		}
	


		/// <summary>
		/// �����ָ���ָ������ַ���������һ����˳��浽��Ӧ��������
		/// ����SplitParamName��
		/// </summary>
		/// <param name="paramName">��������</param>
		/// <param name="split">�ָ�����һ��Ϊ��_��</param>
		/// <returns>SplitParamName��</returns>
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
				splitParamName.Style = splitArray[splitArray.Length-1].Trim();//�ļ�����
				string replaceString = new string(split) + splitParamName.Style;
				splitParamName.Name = paramName.Replace(replaceString,"").Trim();
			}
			return splitParamName;
		}
	


		
		
		/// <summary>
		/// ��ȡ����html������ֵû��˫���ŵļ���˫����
		/// </summary>
		/// <param name="oldHtmlString"></param>
		/// <returns></returns>
		public static string addDoubleQuotationMarks(string oldHtmlString)
		{
			string retHtmlString = "";
			string addPosition ; //���˫���ŵ�λ�ã�Ϊ���򲻼ӣ�=��left��������ߣ�=��right�������ұ�
			bool hasBeginMark = false, hasEqualmark = false, hasDoubleQuotationMark = false;
			string lastS = "";//��һ�ַ�
			string s;         //��ǰ�ַ�
			string nextS;     //��һ�ַ�
			//HTML��Head�ӣ�ֻ��BODY��
			int beginLoc = oldHtmlString.IndexOf("<body>",0);
			if(beginLoc == -1)
				beginLoc = oldHtmlString.IndexOf("<BODY>",0);
			//ȡ��ͷ
			if(beginLoc != -1)
				retHtmlString = oldHtmlString.Substring(0,beginLoc);
			else
				beginLoc = 0;
			for(int i = beginLoc ; i < oldHtmlString.Length; i++)
			{
				addPosition = "" ; //Ĭ��Ϊ����˫����
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
					if((hasBeginMark) && (nextS == " "))//���Ը��ڵȺź�Ŀո�
					{
						i++;
						if(i+1 < oldHtmlString.Length)
							nextS = oldHtmlString.Substring(i+1,1);
						else continue;
					}
				}
				if(hasBeginMark) //����Element������
				{
					if(hasEqualmark) //���ڵȺź�
					{
						switch(s)
						{
							case "=" ://�Ⱥź���Ӧ��������
								hasDoubleQuotationMark = false;//�Ⱥź��Ƿ���˫����:û��
								if(nextS != "\"")
									addPosition = "right";
								else
									hasDoubleQuotationMark = true;//��
								break;
							case " " ://���������ֵ������֮���ÿո�������ո�ǰ��Ӧ�������ţ�������ո�ǰ����ð�ţ��򲻼ӣ�
								hasEqualmark = false;
								if(nextS == " ") //���������Ŀո�
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
				lastS = s;  //��һ�ַ�
			}
			return retHtmlString;
		}


		/// <summary>
		/// ȥ�����</OBJECT>����Ļس�(0d0a)
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
		/// ����<OBJECT ></OBJECT>֮��Ķ���Ŀո�ͻس�ȥ��
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string removeCRandBlank(string objectString)
		{
			string xmlString = objectString;
			//ȥ��OBJECT����Ŀո�ͻس�
			if(xmlString.StartsWith("<OBJECT "))
			{
				string oldString = xmlString;
				int len = oldString.Length;
				xmlString = "";
				for(int i=0; i< len; i++)
				{
					string c = oldString.Substring(i, 1);
					if(c == " ")//�ո�
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i+1, 1);
							if(nextC == " ")//�ո�
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
				//��ȥ���س���Ŀո�
				for(int i=0; i< len; i++)
				{
					string c = "";
					if(i >= len -1)
						c = oldString.Substring(i, 1);
					else
						c = oldString.Substring(i, 2);
					if(c == "\r\n")//�ո�
					{
						if(i < len -2)
						{
							string nextC = oldString.Substring(i + 2, 1);
							if(nextC == " ")//�ո�
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
				//��ȥ��\n��Ŀո�
				for(int i=0; i< len; i++)
				{
					string c = oldString.Substring(i, 1);
					if(c == "\n")//\n
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i + 1, 1);
							if(nextC == " ")//�ո�
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
				//��ȥ������Ļس�
				for(int i=0; i< len; i++)
				{
					string c = "";
					if(i >= len -1)
						c = oldString.Substring(i, 1);
					else
						c = oldString.Substring(i, 2);
					if(c == "\r\n")//�س�
					{
						if(i < len -2)
						{
							string nextC = oldString.Substring(i + 2, 2);
							if(nextC == "\r\n")//�س�
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
					if(c == " ")//�ո�
					{
						if(i < len -1)
						{
							string nextC = oldString.Substring(i+1, 1);
							if(nextC == " ")//�ո�
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
		/// ����<OBJECT ></OBJECT>֮��Ķ���Ŀո�ͻس�ȥ��
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string removeObjectCRandBlank(string objectString)
		{
			string xmlString = objectString.Replace(" ", " ");  
			//ȥ��OBJECT����Ŀո�ͻس�
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
		/// ��<OBJECT>��ÿ��PARAM��û�н�����ǡ�/�����Ͻ�����ǡ�/��
		/// </summary>
		/// <param name="objectString"></param>
		/// <returns></returns>
		public static string addEndMarks(string objectString, string startTag, string endTag)
		{
			//ȡѡ�е����ݣ���ת����HTML
			string htmlString = objectString;
			//��˫����
			//htmlString = Tools.addDoubleQuotationMarks(htmlString);
			//��PARAM��ֵ�����
			string[,] paramString = Tools.getObjectFromHTML(htmlString, startTag, endTag);
			//��ѡ�е�Object��Param���Ͻ����༭���
			string xmlString = "";
			int locParam = 0;
			for(int i=0; i <= paramString.GetUpperBound(0); i++)
			{
				int start = int.Parse(paramString[i,0]);
				int end = int.Parse(paramString[i,1]);
				//PARAM��ǰ�沿��
				xmlString += htmlString.Substring(locParam, start - locParam);
				locParam = end;
				string theParam = htmlString.Substring(start, end - start);
				if(theParam.EndsWith("/>"))
					xmlString += theParam;
				else
					xmlString += theParam.Substring(0, theParam.Length -1) + "/>";

			}
			//��ʣ�ಿ��
			xmlString += htmlString.Substring(locParam);
			return xmlString;
		}


	

		/// <summary>
		/// ��ʼ��WebServiceData��������һ��WebServiceData��
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
		/// ��ʼ�������ļ��ĵ���WebService��
		/// </summary>
		/// <param name="dataLength">���͵����ݿ�Ĵ�С</param>
		/// <returns>FileSenderData��</returns>
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
		/// ��ʼ��һ����ҳ��༭���е�Table���ԣ�������һ��InsertTableParam��(����)
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
		/// ��ʼ��AuditInfo(���������)��������һ��AuditInfo��
		/// </summary>
		/// <returns></returns>
		public static AuditInfo initAuditInfo()
		{
			AuditInfo auditInfo = new AuditInfo();
			//�������͵Ļ�����Ϣ
			auditInfo.ClassNo = 0;
			auditInfo.ClassID = "";
			auditInfo.ClassDescription = "";
			//���������Ļ�����Ϣ
			auditInfo.MethodNo = 0;
			auditInfo.MethodName = "";
			auditInfo.MethodDescription = "";
			auditInfo.MethodLevel = 0;
			auditInfo.MethodMemo = "";
			//�����š�����ʱ���IP
			auditInfo.AuditNo = 0;
			auditInfo.OperationIP = "";
			auditInfo.OperationTime = "";
			//�û���Ϣ
			auditInfo.UserName = "";
			auditInfo.PassWord = "";
			auditInfo.Token = "";
			auditInfo.LoginTime = System.DateTime.Now;
			//��������
			auditInfo.Opid = "AUDIT";
			auditInfo.Action = "";
			auditInfo.ParentGuid = "";
			auditInfo.Guid = "";
			//��������
			auditInfo.OperationContent = "";
			auditInfo.OperationResult = "";
			auditInfo.AuditMemo = "";
			//��ѯ����
			auditInfo.SQL = "";
			auditInfo.SortField = "";
			auditInfo.PageNum = 20;
			auditInfo.StartLoc = 0;
			auditInfo.RecNum = 0;
			auditInfo.PageNo = 0;
			return auditInfo;
		}



		/// <summary>
		/// ��ʼ��ReporterInfo(������Ϣ��)��������һ��ReporterInfo��
		/// </summary>
		/// <returns></returns>
		public static ReporterInfo initReporterInfo()
		{
			ReporterInfo reporterInfo = new ReporterInfo();
			reporterInfo.UserName = "";
			reporterInfo.PassWord = "";
			reporterInfo.Token = "";
			reporterInfo.LoginTime = System.DateTime.Now;//��¼ʱ��
			reporterInfo.OnLine = false;//Ĭ��Ϊ�ѻ�
			reporterInfo.PassValidate = false;//Ĭ��Ϊû��ͨ����֤
			reporterInfo.Path = Tools.getPathParent(System.AppDomain.CurrentDomain.BaseDirectory) + "cache\\report\\";//Ĭ��Ŀ¼
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
		/// ��ʼ��UserLoginInfo��������һ��UserLoginInfo��
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
		/// ��ʼ��UserLoginInfo��������һ��UserLoginInfo��
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
		/// ��ʼ��ListItemData��������һ��ListItemData��
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
				listItemData.InputFieldValue[i,0] = "" ;  //�ֶα���
				listItemData.InputFieldValue[i,1] = "" ;  //�ֶ�����
				listItemData.InputFieldValue[i,2] = "" ;  //�ֶ�����
				listItemData.InputFieldValue[i,3] = "" ;  //�ֶγ���
				listItemData.InputFieldValue[i,4] = "" ;  //������ʽ
				listItemData.InputFieldValue[i,5] = "" ;  //�ֶ�����
				listItemData.InputFieldValue[i,6] = "" ;  //�ֶε���ʾ��񣬼��ؼ���
				listItemData.DictObject[i] = null;        //�ֶε��ֵ�
				listItemData.ReadOnly[i] = false;         //Ĭ�ϲ���ֻ��
				listItemData.RelationFieldName[i, 0] = "";   //����������
				listItemData.RelationFieldName[i, 1] = "";   //�����ֶ�����
				listItemData.RelationFieldName[i, 2] = "";   //�����ı����ֶ�����
				listItemData.RelationFieldName[i, 3] = "";   //��������ʾ�ֶ�����
			}
			listItemData.IDENTITY_Name = "PkID";
			listItemData.IDENTITY_Value = 0;
			return listItemData;
		}



		/// <summary>
		/// ��ԭ����ListItemData�ิ�Ƴ�һ���µ�ListItemData�࣬������һ��ListItemData��
		/// </summary>
		/// <param name="oldData">ԭ����ListItemData��</param>
		/// <returns>�µ�ListItemData��</returns>
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
				listItemData.InputFieldValue[i,0] = oldData.InputFieldValue[i,0];  //�ֶα���
				listItemData.InputFieldValue[i,1] = oldData.InputFieldValue[i,1];  //�ֶ�����
				listItemData.InputFieldValue[i,2] = oldData.InputFieldValue[i,2];  //�ֶ�����
				listItemData.InputFieldValue[i,3] = oldData.InputFieldValue[i,3];  //�ֶγ���
				listItemData.InputFieldValue[i,4] = oldData.InputFieldValue[i,4];  //������ʽ
				listItemData.InputFieldValue[i,5] = oldData.InputFieldValue[i,5];  //�ֶ�����
				listItemData.InputFieldValue[i,6] = oldData.InputFieldValue[i,6];  //�ֶε���ʾ��񣬼��ؼ���
				listItemData.DictObject[i] = oldData.DictObject[i];        //�ֶε��ֵ�
				listItemData.ReadOnly[i] = oldData.ReadOnly[i];         //Ĭ�ϲ���ֻ��
				listItemData.RelationFieldName[i, 0] = oldData.RelationFieldName[i, 0];   //����������
				listItemData.RelationFieldName[i, 1] = oldData.RelationFieldName[i, 1];   //�����ֶ�����
				listItemData.RelationFieldName[i, 2] = oldData.RelationFieldName[i, 2];   //�����ı����ֶ�����
				listItemData.RelationFieldName[i, 3] = oldData.RelationFieldName[i, 3];   //��������ʾ�ֶ�����
			}
			listItemData.IDENTITY_Name = oldData.IDENTITY_Name;
			listItemData.IDENTITY_Value = oldData.IDENTITY_Value;
			return listItemData;
		}




		/// <summary>
		/// ��ʼ��FindRichTextBoxData��������һ��FindRichTextBoxData��
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


	
	
	//�����ǹ��̷���
	//�������Զ������
	



	/// <summary>
	/// ListView������ӵ�һЩ����
	/// ��ѡ��TreeViewʱ�������ʼ��(�ֶΣ�ֵ)
	/// ��ѡ��ListView��ĳ����¼ʱ���������ݸ���(ֵ)
	/// </summary>
	public class ListItemData
	{
		public string SiteId;//ѡȡ��վ��ID
		public string Opid;
		public string Id;
		public string ParentID;//�����ID
		public string ParentPath;//�����Path
		public bool IsTree;//�ý���Ƿ��ǿ���
		public string TableName;
		public string Title; //��XMLȡ������ʾ��Tree���ֶζ�Ӧ�ı��⣬��������ListView ��column����
		public string Action;  //�û������Ĳ���,ȡname�� ֵ
		public string ActionDes;  //�û������Ĳ���,ȡdesciption�� ֵ
		public int formFieldNum ; //�����ܴ������ֶεĸ���
		public int formFieldLength ; //�����ܴ������ֶε��ܳ���
		public int maxTitleLength ; //�����ܴ������ֶα������󳤶�
		public object[] DictObject; //�ֵ��ֶΣ��ֶε���ʾ���ݺ����ݿ����ŵ���������[ShowField,CodeField]
		public bool[] ReadOnly; //ֻ��
		public string[,] RelationFieldName; //��Ź������ӱ�����ͣ��ֱ��ǣ������ơ��������ֶ����ơ�������ֶ����ơ���ʾ���ֶ�����
		public string[,] InputFieldValue ;//����ֶ����ԣ��ֱ��ǣ��ֶα��⡢�ֶ����ơ��ֶ����ݡ��ֶγ��ȡ�������ʽ���ֶ����͡��ֶε���ʾ��񣬼���ʲô�ؼ���ʾ
		public string IDENTITY_Name;//�����ֶΣ�ϵͳ�Զ����ɵ�IDENTITY�ֶε�����
		public int IDENTITY_Value;//�����ֶΣ�ϵͳ�Զ����ɵ�IDENTITY�ֶε�����
		
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
				InputFieldValue[i,0] = inputFieldValue[i,0];  //�ֶα���
				InputFieldValue[i,1] = inputFieldValue[i,1];  //�ֶ�����
				InputFieldValue[i,2] = inputFieldValue[i,2];  //�ֶ�����
				InputFieldValue[i,3] = inputFieldValue[i,3];  //�ֶγ���
				InputFieldValue[i,4] = inputFieldValue[i,4];  //������ʽ
				InputFieldValue[i,5] = inputFieldValue[i,5];  //�ֶ�����
				InputFieldValue[i,6] = inputFieldValue[i,6];  //�ֶε���ʾ��񣬼���ʲô�ؼ���ʾ
				DictObject[i] = dictObject[i];                //�ֶε��ֵ�
				ReadOnly[i] = readOnly[i];                    //�Ƿ�ֻ��
				RelationFieldName[i, 0] = relationFieldName[i,0];  //�����ı�����
				RelationFieldName[i, 1] = relationFieldName[i,1];  //�������ֶ�����
				RelationFieldName[i, 2] = relationFieldName[i,2];  //�����ı�����ֶ�����
				RelationFieldName[i, 3] = relationFieldName[i,3];  //��������ʾ���ֶ�����
			}
			IDENTITY_Name = iDENTITY_Name;
			IDENTITY_Value = iDENTITY_Value;
		}
	}



	/// <summary>
	/// ����WebService����
	/// </summary>
	public class WebServiceData
	{
		public string SiteId;//ѡȡ��վ��ID
		public string Opid;
		public string Id;//ѡȡID
		public string ParentID;//�����ID
		public string Action;  //�û������Ĳ���,ȡname�� ֵ
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
	/// ����WebService�����ļ��Ĳ���
	/// </summary>
	public class FileSenderData
	{
		public string Opid;    //�������ͣ��磺FILESENDER
		public string Action;  //�û������Ĳ���,ȡWebService����
		public string Url;     //WebService�����ڵ�URL
		public string Type;    //�����ļ�������
		public string Token;   //�ļ���token
		public string InfoBag; //�ļ��Ļ�����Ϣ��
		public long FileSize;  //Ҫ���͵��ļ���С
		public long BlockSize; //Ҫ���͵��ļ����С
		public long OffSet;    //Ҫ���͵��ļ���ƫ����
		public byte[] Data;    //Ҫ���͵��ļ�����
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
	/// ҳ��༭���е�Table����(���������)
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
	/// ���PARAM��NAME����NAME�к��е�NAME��STYLE�ֿ�
	/// </summary>
	public class SplitParamName
	{
		public string Name; //����
		public string Style;//��ʾ�����CHANNEL,RESDIR
		public char[] Split;//�ָ���
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
	/// ��ִ�·�����ļ�Path��FileName������
	/// </summary>
	public class SplitFileName
	{
		public string Path;//�ļ�·��
		public string FileName; //�ļ�����
		public char[] Split;//�ָ���
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
	/// ������Դ�ļ���Ҫ��һЩ����
	/// </summary>
	public class UpLoadResFileData
	{
		public string CacheRisPath;  //��Դ�ļ���ŵ���ʱ·��,������������·��+siteID���
		public string SourceRisPath; //Դ��Դ�ļ����ڵľ���·������c:\site\images\
		public UpLoadResFileData()
		{}

		public UpLoadResFileData(string cacheRisPath, string sourceRisPath)
		{
			CacheRisPath = cacheRisPath;
			SourceRisPath = sourceRisPath;
		}
	}

	
	/// <summary>
	/// �ֶ���
	/// </summary>
	public class FieldInfo
	{
		public string FieldName;  //�ֶ�����
		public string FieldValue; //�ֶ�����
		public FieldInfo()
		{}

		public FieldInfo(string fieldName, string fieldValue)
		{
			FieldName = fieldName;
			FieldValue = fieldValue;
		}
	}

	
	

	/// <summary>
	/// ���������
	/// </summary>
	public class AuditInfo
	{
		public int ClassNo;//�����������
		public string ClassID;//�������ͱ�ʶ
		public string ClassDescription;//������������

		public int MethodNo;//�����������
		public string MethodName;//����������ʶ
		public string MethodDescription;//������������
		public int MethodLevel;//������������
		public string MethodMemo;//����������ע
		
		public int AuditNo;//������
		public string OperationIP;//��������IP
		public string OperationTime;//
		
		public string UserName;//����Ա���û�������ID
		public string PassWord;//����
		public string Token; //token
		public DateTime LoginTime;//��¼ʱ��

		public string Opid;    //�������ͣ��磺RESDIR
		public string Action;  //�û������Ĳ���,ȡWebService����
		public string ParentGuid; //��ǰ�������ݵĸ���guid
		public string Guid; //��ǰ�������ݵ�guid

		public string OperationContent; //�������ݣ������ı���
		public string OperationResult; //�������
		public string AuditMemo; //��ע

		public string SQL; //��ѯ������where�Ӿ�
		public string SortField; //��ѯ�������ֶ�
		public int PageNum; //ÿ�β�ѯȡ�صļ�¼��
		public int StartLoc; //ÿ�β�ѯȡ�صļ�¼��ʼ��¼��
		public int RecNum;//�����������ܼ�¼��
		public int PageNo; //��ǰ��ʾ��ҳ��

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
			//�������͵Ļ�����Ϣ
			ClassNo = classNo;
			ClassID = classID;
			ClassDescription = classDescription;
			//���������Ļ�����Ϣ
			MethodNo = methodNo;
			MethodName = methodName;
			MethodDescription = methodDescription;
			MethodLevel = methodLevel;
			MethodMemo = methodMemo;
			//����ʱ���IP
			AuditNo = auditNo;
			OperationIP = operationIP;
			OperationTime = operationTime;
			//�û���Ϣ
			UserName = userName;
			PassWord = passWord;
			Token = token;
			LoginTime = loginTime;
			//��������
			Opid = opid;
			Action = action;
			ParentGuid = parentGuid;
			Guid = guid;
			//��������
			OperationContent = operationContent;
			OperationResult = operationResult;
			AuditMemo = auditMemo;
			//��ѯ����
			SQL = sql;
			SortField = sortField;
			PageNum = pageNum;
			StartLoc = startLoc;
			RecNum = recNum;
			PageNo = pageNo;
		}
	}




	/// <summary>
	/// ������Ϣ��
	/// </summary>
	public class ReporterInfo
	{
		public string UserName;//�û�������ID
		public string PassWord;//����
		public string Token; //token
		public DateTime LoginTime;//��¼ʱ��
		public bool OnLine;    //�Ƿ�������
		public bool PassValidate;//�Ƿ�ͨ����֤���û���һ��ʹ��ʱ�����Բ�ͨ����֤������ϵͳ��ͨ��������������֤��
		public string Path;      //�ѻ�����ʱ����ʱ�ļ���ŵ���Ŀ¼...cache\report
		public string Opid;    //�������ͣ��磺RESDIR
		public string Action;  //�û������Ĳ���,ȡWebService����
		public string ResCatGuid; //��ԴĿ¼guid
		public string ContentTypeGuid; //��������guid
		public string DocGuid; //���ͼ�¼��guid
		public string AttachGuid; //���ͼ�¼������guid
		public string ContentTypeName; //������������
		public string DocHTML; //���ĵ����ݣ�HTML��ʽ
		public string ReportXML; //���ͻ�����Ϣ��XML��ʽ
		public string SQL; //��ѯ������where�Ӿ�
		public string SortField; //��ѯ�������ֶ�
		public int PageNum; //ÿ�β�ѯȡ�صļ�¼��
		public int StartLoc; //ÿ�β�ѯȡ�صļ�¼��ʼ��¼��
		public int RecNum;//�����������ܼ�¼��
		public int PageNo; //��ǰ��ʾ��ҳ��

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
	/// TextBox�ı���������ӵ�һЩ���ԣ���ŵ�TextBox��TAG��
	/// </summary>
	public class TextBoxData
	{
		public string Title;  //��ӦLabel����
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
	/// RichTextBox���ı����������һЩ����
	/// </summary>
	public class FindRichTextBoxData
	{
		public string Text;  //��������
		public bool MatchCase;//���ִ�Сд
		public bool WholeWord;//ȫ��ƥ��
		public bool IsUp;//����true=UP;false=Down
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
