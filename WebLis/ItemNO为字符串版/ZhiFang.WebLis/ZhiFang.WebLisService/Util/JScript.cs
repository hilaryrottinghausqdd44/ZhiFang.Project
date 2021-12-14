using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Configuration;


namespace ZhiFang.Util
{
	/// <summary>
	/// JScript ��ժҪ˵����
	/// ͨ�õ�JScript����
	/// �ṩ��ҳ������ͻ��˴���ʵ�����⹦�ܵķ���
	/// </summary>
	public class JScript
	{
		public JScript()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����JavaScriptС����
		/// </summary>
		/// <param name="js">������Ϣ(�ַ���)</param>
		public static void Alert(string message)
		{
			string js=@"<Script language='JavaScript'>
	                    alert('"+ message +"');</Script>";
			HttpContext.Current.Response.Write(js);
        }


		/// <summary>
		/// ����JavaScriptС����
		/// </summary>
		/// <param name="message">������Ϣ(����)</param>
		public static void Alert(object message)
		{
			string js=@"<Script language='JavaScript'>
                    alert('{0}');  
                  </Script>";
			HttpContext.Current.Response.Write(string.Format(js,message.ToString()));
		} 


		

		/// <summary>
		/// ����JavaScriptС���ڣ�����Ϣ������ʾ������ҳ�����¶���
		/// </summary>
		/// <param name="message">��ʾ��Ϣ</param>
		/// <param name="toURL">���¶����ҳ��</param>
		public static void AlertAndRedirect(string message,string toURL)
		{
			string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
			HttpContext.Current.Response.Write(string.Format(js,message ,toURL));
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="strWinCtrl"></param>
		public static void RtnRltMsgbox(object message,string strWinCtrl)
		{   
			string js = @"<Script language='JavaScript'>
                     strWinCtrl = true;
                     strWinCtrl = if(!confirm('"+ message +"'))return false;</Script>";
			HttpContext.Current.Response.Write(string.Format(js,message.ToString()));
		}




		/// <summary>
		/// �ص���ʷҳ��
		/// </summary>
		/// <param name="value">-1/1</param>
		public static void GoHistory(int value)
		{
			string js=@"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
			HttpContext.Current.Response.Write(string.Format(js,value));
		} 


		/// <summary>
		/// �رյ�ǰ����
		/// </summary>
		public static void CloseWindow()
		{
			string js=@"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
			HttpContext.Current.Response.Write(js);     
			HttpContext.Current.Response.End();  
		}

		/// <summary>
		/// ���¼��ظ�����
		/// </summary>
		public static void ReloadParent()
		{
			string js=@"<Script language='JavaScript'>
                    parent.location.reload();
                  </Script>";
			HttpContext.Current.Response.Write(js);     
		}


		/// <summary>
		/// ˢ�¸�����
		/// </summary>
		public static void RefreshParent()
		{
			string js=@"<Script language='JavaScript'>
				var openerObj = window.opener;
				openerObj.document.location = openerObj.document.location;
                </Script>";
			HttpContext.Current.Response.Write(js);     
		}

		/// <summary>
		/// �رյ�ǰ���ڲ�ˢ�¸�����
		/// </summary>
		public static void CloseWindowRefreshParent()
		{
			string js=@"<Script language='JavaScript'>
				var openerObj = window.opener;
				openerObj.document.location = openerObj.document.location;
				window.close();
                </Script>";
			HttpContext.Current.Response.Write(js);     
		}


		/// <summary>
		/// ��ʽ��ΪJS�ɽ��͵��ַ���
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string JSStringFormat(string s)
		{
			return s.Replace("\r","\\r").Replace("\n","\\n").Replace("'","\\'").Replace("\"","\\\"");
		} 


		/// <summary>
		/// ˢ�´򿪴���
		/// </summary>
		public static void RefreshOpener()
		{
			string js=@"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
			HttpContext.Current.Response.Write(js);     
		}

		/// <summary>
		/// �򿪴���:���ڵĴ�С���Ե���
		/// </summary>
		/// <param name="url">Ҫ�򿪵�ҳ��URL</param>
		public static void OpenWebForm(string url)
		{
			string js=@"<Script language='JavaScript'>
            window.open('"+url+@"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no'); </Script>";
			HttpContext.Current.Response.Write(js);     
		}



		public static void ShowModalWindow(string url)
		{
			string js = @"<Script language='JavaScript'>
            window.showModalWindow('{0}','','dialogWidth:760px;dialogHeight:600px');</Script>";
			js = string.Format(js, url);
			HttpContext.Current.Response.Write(js);     
		}




		/// <summary>        
		/// ��������:��WEB����
		/// ҳ��Ĵ�С���Ե��� 
		/// </summary>
		/// <param name="url">WEB����</param>
		/// <param name="isFullScreen">�Ƿ�ȫ��Ļ</param>
		public static void OpenWebForm(string url,bool isFullScreen)
		{            
			string js=@"<Script language='JavaScript'>";
			if(isFullScreen)
			{
				js += "var iWidth = 0;";
				js += "var iHeight = 0;";
				js += "iWidth=window.screen.availWidth-10;";
				js += "iHeight=window.screen.availHeight-50;";
				js += "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,Directories=no';";
				js += "window.open('"+url+@"','',szFeatures);";
			}
			else
			{
				js += "window.open('"+url+@"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');";

			}
			js += "</Script>";
			HttpContext.Current.Response.Write(js);     
		} 


		/// <summary>
		/// �򿪴���:���ڵĴ�С�����Ե���
		/// </summary>
		/// <param name="url">>Ҫ�򿪵�ҳ��URL</param>
		/// <param name="height">ҳ��ĸ߶�</param>
		/// <param name="width">ҳ��Ŀ��</param>
		public static void OpenWebForm(string url, int height, int width)
		{
			/*��������������������������������������������������������������������*/
			/*�޸�Ŀ��:    �¿�ҳ��ȥ��ie�Ĳ˵�������                        */

			string js=@"<Script language='JavaScript'>
            window.open('{0}','','height={1},width={2},top=0,left=0,location=no,menubar=no,resizable=no,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no'); </Script>";
			/*����*/
			/*��������������������������������������������������������������������*/
			js = string.Format(js, url, height, width);
			HttpContext.Current.Response.Write(js);     
		}



				
		/// <summary>
		/// �򿪴���:���ڵĴ�С�����Ե���
		/// </summary>
		/// <param name="url">>Ҫ�򿪵�ҳ��URL</param>
		/// <param name="height">ҳ��ĸ߶�</param>
		/// <param name="width">ҳ��Ŀ��</param>
		/// <param name="top">ҳ����ʾ�����Ͻǵ�������</param>
		/// <param name="left">ҳ����ʾ�����Ͻǵĺ�����/param>
		public static void OpenWebForm(string url, int height, int width, int top, int left)
		{
			string js=@"<Script language='JavaScript'>
            window.open('{0}','','height={1},width={2},top={3},left={4},location=no,menubar=no,resizable=no,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no'); </Script>";
			js = string.Format(js, url, height, width, top, left);
			HttpContext.Current.Response.Write(js);     
		}



		
		public static void OpenWebForm(string url,string name,string future)
		{
			string js=@"<Script language='JavaScript'>
                     window.open('"+url+@"','"+name+@"','"+future+@"')
                  </Script>";
			HttpContext.Current.Response.Write(js);     
		}
		public static void OpenWebForm(string url,string formName)
		{
			/*��������������������������������������������������������������������*/
			/*�޸�Ŀ��:    �¿�ҳ��ȥ��ie�Ĳ˵�������                        */
			/*ע������:                                */
			/*��ʼ*/
			string js=@"<Script language='JavaScript'>
            window.open('"+url+@"','"+formName+@"','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');

        </Script>";
			/*����*/
			/*��������������������������������������������������������������������*/ 

			HttpContext.Current.Response.Write(js);     
		}



		/// <summary>
		/// ת��Url�ƶ���ҳ��
		/// </summary>
		/// <param name="url"></param>
		public static void JavaScriptLocationHref(string url)
		{
			string js=@"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
			js=string.Format(js,url);
			HttpContext.Current.Response.Write(js);  
		}



		/// <summary>
		/// ָ���Ŀ��ҳ��ת��
		/// </summary>
		/// <param name="FrameName"></param>
		/// <param name="url"></param>
		public static void JavaScriptFrameHref(string FrameName,string url)
		{
			string js=@"<Script language='JavaScript'>
                    @obj.location.replace(""{0}"");
                  </Script>";
			js = js.Replace("@obj",FrameName );
			js=string.Format(js,url);
			HttpContext.Current.Response.Write(js);  
		}
 
 

		/// <summary>
		/// ���ÿؼ��Ľ���
		/// </summary>
		/// <param name="objectID">ҳ��ؼ���ID</param>
		public static void setFocus(string objectID)
		{
			string js=@"<Script language='JavaScript'>
						document.getElementById('{0}').focus()
                  </Script>";
			js = string.Format(js, objectID);
			HttpContext.Current.Response.Write(js);
		}


        /// <summary>
        /// ���ÿؼ��Ľ���
        /// </summary>
        /// <param name="objectID">ҳ��ؼ���ID</param>
        public static void setFocus(System.Web.UI.Page page, string objectID)
        {
            string js = @"<Script language='JavaScript'>
						document.getElementById('{0}').focus()
                  </Script>";
            js = string.Format(js, objectID);
            page.SetFocus(objectID);
        }



		/// <summary>
		/// ��ȡ���ÿؼ�����Ľű�
		/// </summary>
		/// <param name="objectID">ҳ��ؼ���ID</param>
        public static string setFocusJS(string objectID)
        {
            string js = @"<script language='javascript' type='text/javascript'> 
						document.getElementById('{0}').focus();
                  </script>";
            js = string.Format(js, objectID);
            return js;
        }



        /// <summary>
        /// ��ʾ��Ϣ��IE��״̬��
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="message">Ҫ��ʾ����Ϣ</param>
        public static void showMessageToIEStatus(System.Web.UI.Page page, string message)
        {
            message = ZhiFang.Util.JScript.JSStringFormat(message);
            string js = "window.status = '{0}';";
            js = string.Format(js, message);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "��ʾ" + System.Guid.NewGuid().ToString(), js, true);
        }




        /// <summary>
        /// ��ʾ��Ϣ��IE��״̬��
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="message">Ҫ��ʾ����Ϣ</param>
        public static void setPageTitle(System.Web.UI.Page page, string title)
        {
            title = ZhiFang.Util.JScript.JSStringFormat(title);
            string js = "window.document.title = '{0}';";
            js = string.Format(js, title);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "��ʾ" + System.Guid.NewGuid().ToString(), js, true);
        }



        /// <summary>
        ///  ����JavaScriptС����
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="message">Ҫ��ʾ����Ϣ</param>
        public static void Alert(System.Web.UI.Page page, string message)
        {
            message = ZhiFang.Util.JScript.JSStringFormat(message);
            string js = "alert('{0}');";
            js = string.Format(js, message);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "��ʾ" + System.Guid.NewGuid().ToString(), js, true);
        }



        /// <summary>
        /// �ӷ����������ļ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="path"></param>
        /// <param name="forceDownload"></param>
        public static void downloadFile(System.Web.UI.Page page, string path, bool forceDownload)
        {
            if (!System.IO.File.Exists(path))
                return;
            //�ж��ļ��Ĵ�С
            //long fileLength = System.IO.File.
            string name = System.IO.Path.GetFileName(path);
            string ext = System.IO.Path.GetExtension(path);
            string type = "";//����ĵ�����
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    //�ı�
                    case ".ini":
                        type = "text/ini";
                        break;
                    case ".sql":
                        type = "text/sql";
                        break;
                    case ".bat":
                        type = "text/bat";
                        break;
                    case ".css":
                        type = "text/css";
                        break;
                    case ".asc":
                    case ".txt":
                        type = "text/plain";
                        break;
                    case ".mht":
                        type = "text/mht";
                        break;
                    case ".htm":
                    case ".html":
                        type = "text/html";
                        break;
                    case ".xsl":
                    case ".xml":
                        type = "text/xml";
                        break;
                    case ".rtx":
                        type = "text/richtext";
                        break;
                    case ".rtf":
                        type = "text/rtf";
                        break;
                    //ͼ��
                    case ".bmp":
                        type = "image/bmp";
                        break;
                    case ".gif":
                        type = "image/gif";
                        break;
                    case ".ief":
                        type = "image/ief";
                        break;
                    case ".jpg":
                    case ".jpe":
                    case ".jpeg":
                        type = "image/jpeg";
                        break;
                    case ".png":
                        type = "image/png";
                        break;
                    case ".tif":
                    case ".tiff":
                        type = "image/tiff";
                        break;
                    //��Ƶ
                    case ".mpe":
                    case ".mpg":
                    case ".mpeg":
                        type = "image/mpeg";
                        break;
                    case ".avi":
                        type = "video/x-msvideo";
                        break;
                    case ".pff":
                        type = "application/ms-powerpoint";
                        break;
                    case ".pdf":
                        type = "application/pdf";
                        break;
                    case ".zip":
                        type = "application/zip";
                        break;
                    case ".doc":
                        type = "application/msword'";
                        break;
                    case ".xls":
                        type = "application/vnd.ms-excel";
                        break;
                    case ".bin":
                    case ".exe":
                    case ".dll":
                    case ".class":
                        type = "application/octet-stream";
                        break;
                    case ".ppt":
                        type = "'application/vnd.ms-powerpoint";
                        break;
                    case ".pgn":
                        type = "'application/x-chess-pgn";
                        break;
                    case ".js":
                        type = "'application/x-javascript";
                        break;
                    case ".swf":
                        type = "'application/x-shockwave-flash";
                        break;
                    case ".src":
                        type = "'application/x-wais-source";
                        break;
                    case ".rm":
                        type = "'audio/x-pn-realaudio";
                        break;
                }
            }
            if (forceDownload)
            {
                page.Response.AppendHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(name.Substring(name.IndexOf("_"))));
            }
            if (type != "")
                page.Response.ContentType = type;
            page.Response.WriteFile(path);
            page.Response.End();
        }

        /// <summary>
        /// �򿪴���:���ڵĴ�С�����Ե���,���ھ���
        /// </summary>
        /// <param name="url">>Ҫ�򿪵�ҳ��URL</param>
        /// <param name="height">ҳ��ĸ߶�</param>
        /// <param name="width">ҳ��Ŀ��</param>
        public static void OpenWebForm(System.Web.UI.Page page, string url, int height, int width)
        {
            /*��������������������������������������������������������������������*/
            /*�޸�Ŀ��:    �¿�ҳ��ȥ��ie�Ĳ˵�������                        */
            string js = @"
            window.open('{0}','','height={1},width={2},top='+(window.screen.availHeight - 30 - " + height + ") / 2+',left='+(window.screen.availWidth - 10 - " + width + ") / 2+',location=no,menubar=no,resizable=no,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');";
            /*����*/
            /*��������������������������������������������������������������������*/
            js = string.Format(js, url, height, width);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "����", js, true);
        }






	}
}
