<%@ Register TagPrefix="uc1" TagName="DataListWebControl" Src="../../WebControlLib/DataListWebControl.ascx" %>

<%@ Page validateRequest="false" enableEventValidation="false" Language="c#"
    AutoEventWireup="True" Inherits="OA.News.Browse.BrowseAppSystemDataForm" Codebehind="BrowseAppSystemDataForm.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>����Ӧ�ó���</title>
    <link href="../../WebControlLib/CSS/WebControlDefault.css" type="text/css" rel="Stylesheet"/>

    <script type="text/jscript">
			//�Զ�������ܵĴ�СΪҳ��Ĵ�С
			function iframeAutoFit()
			{
				//�ӳ�
				//setTimeout("alert('aaa');",2000);
				try
				{
					if(window!=parent)
					{
						var a = parent.document.getElementsByTagName("IFRAME");
						for(var i=0; i<a.length; i++) 
						{
							if(a[i].contentWindow==window)
							{
								//����Ӧ�߶�
								var h1=0, h2=0, d=document, dd=d.documentElement;
								a[i].parentNode.style.height = a[i].offsetHeight +"px";
								a[i].style.height = "10px";

								if(dd && dd.scrollHeight) h1=dd.scrollHeight;
								if(d.body) h2=d.body.scrollHeight;
								var h=Math.max(h1, h2);
								//��͵ĸ߶�
								if(h < 100)
									h = 100;

								if(document.all) {h += 16;}//4
								if(window.opera) {h += 12;}//1
								a[i].style.height = a[i].parentNode.style.height = h +"px";
            
								//����Ӧ���
								var W1=0, W2=0, d=document, dd=d.documentElement;
								//a[i].parentNode.style.width = a[i].offsetWidth +"px";
								//a[i].style.width = "10px";

								if(dd && dd.scrollWidth) W1=dd.scrollWidth;
								if(d.body) W2=d.body.scrollWidth;
								var W=Math.max(W1, W2);

								if(document.all) {W += 24;}
								if(window.opera) {W += 21;}
								//a[i].style.width = a[i].parentNode.style.width = W +"px";
							}
						}              
					}
				}
				catch (ex){}
			}

			function getParamValueFromUrl(url, getParamName)
			{
				//�� url ��ȡ �����������
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//ȡ�������б����
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//��ֵ�ǰ��������
					var paramName = splitParam[0];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == getParamName)
						return paramValue;
				}
				return "";
			}

			//���ع�������ģ�������б�
			function makeAndResetIframeUrl(selectFieldName, selectFieldValue, iframeID)
			{
				//alert(selectFieldName);
				iframeUrl = "";
				var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
				var selectIFRAME = 0;//����ѡ�еĿ��ID
				for (var i=0; i < allFrame.length; i++)
				{
					//�ҵ����
					if(allFrame[i].id == iframeID)
					{
						iframeUrl = window.parent.document.frames(i).location.href;
						//ȡ��Ӧ��������ֶ�����
						var primaryFieldName = getParamValueFromUrl(iframeUrl, "primaryFieldName");
						if(primaryFieldName == "")
							return;
						//���ǹ������ֶ�
						if(primaryFieldName != selectFieldName)
						{
							return;
						}
						selectIFRAME = i;
						break;
					}
				}
				if (iframeUrl == "")
				{
					return "";
				}
				
				//�� url ��ȡ �����������
				var splitUrl = iframeUrl.split("?");
				var splitParams = splitUrl[1].split("&");//ȡ�������б����
				//�������
				var systemName = "";//��Ӧ��Ӧ��ϵͳ����
				var tableName = "";//��Ӧ�����ݿ��(ģ�������)
				var pageSize = "";//ÿҳ��ʾ�ļ�¼��
				var modalName = "";//ģ������
				var modalID = "";//ģ���ʶ
				var relationModalName = "";//��������ģ������
				var relationFieldName = "";//�������ֶ�����
				var primaryFieldName = "";//������������ֶ�����
				var relationFieldValue = "";//�������ֶ�����
				var selectFields = "";//ѡ����ֶ�(�������б�����ʾ���ֶ�)
				var whereSQL = "";//�̶��Ĳ�ѯ����
				var sql = "";//�����Ĳ�ѯ���(��������˱����,��ֻ���б����,����������ѯ�йصĲ�������������)
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//��ֵ�ǰ��������
					var paramName = splitParam[0];
					//var paramValue = splitParam[1];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == "systemName") systemName = paramValue;//��Ӧ��Ӧ��ϵͳ����
					else if(paramName == "tableName") tableName = paramValue;
					else if(paramName == "pageSize") pageSize = paramValue;
					else if(paramName == "modalName") modalName = paramValue;
					else if(paramName == "modalID") modalID = paramValue;
					else if(paramName == "relationModalName") relationModalName = paramValue;
					else if(paramName == "primaryFieldName") primaryFieldName = paramValue;
					else if(paramName == "relationFieldName") relationFieldName = paramValue;
					else if(paramName == "relationFieldValue") relationFieldValue = paramValue;
					else if(paramName == "selectFields") selectFields = paramValue;
					else if(paramName == "whereSQL") whereSQL = paramValue;
					else if(paramName == "sql") sql = paramValue;
				}
				//��ǰѡ��Ĺ���������
				relationFieldValue = selectFieldValue;
				//ҳ������
				var pageName = "../../News/Browse/BrowseAppSystemDataForm.aspx";
				//�����б�
				var paramUrl = "systemName=" + systemName + "&amp;tableName=" + tableName + "&amp;pageSize=" + pageSize + "&amp;modalName=" + modalName + "&amp;modalID=" + modalID + "&amp;relationModalName=" + relationModalName + "&amp;relationFieldName=" + relationFieldName + "&amp;primaryFieldName=" + primaryFieldName + "&amp;relationFieldValue=" + relationFieldValue +  "&amp;selectFields=" + selectFields + "&amp;whereSQL=" + whereSQL + "&amp;sql=" + sql;
				//��ܵ�URL
				var newUrl = pageName + "?" + paramUrl;
				//��URL���ȥ
				window.parent.document.frames(selectIFRAME).location.href = newUrl;
				return relationModalName;
			}
			 
			function AfterCellClick(selectFieldName, selectFieldValue)
			{
				//alert(selectFieldName);
				//ȡ��ǰ�Ŀ�ܵ�id(��Ϊֻˢ�¸���ǰѡ�������й����Ŀ��)
				var currentId = window.frameElement.name;
				var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
				for (var i=0; i < allFrame.length; i++)
				{
					//��ܵ�id
					var id = allFrame[i].id;
					if(currentId != id)//���ǵ�ǰѡ�еĿ��,������һ�����
					{
						continue;
					}
					//alert("��ǰģ��Ϊ:" + currentId);
					//ȡurl
					var url = allFrame[i].src;
					//alert(url);
					//ȡ��������ģ������
					var relationModalName = getParamValueFromUrl(url, "relationModalName");
					if(relationModalName == "")
						continue;
					//alert(relationModalName);
					//���
					var relModalList = relationModalName.split(",");
					//alert(relModalList.length);
					for(var j=0;j<relModalList.length; j++)
					{
						var relationIframeID = relModalList[j];
						//alert("ˢ�µĹ�������ģ��Ϊ:" + relationIframeID);
						var url = makeAndResetIframeUrl(selectFieldName, selectFieldValue, relationIframeID);
					}
				}
			}
			
    </script>

</head>
<body onload="iframeAutoFit()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <uc1:DataListWebControl ID="DataListWebControl1" runat="server">
                </uc1:DataListWebControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
