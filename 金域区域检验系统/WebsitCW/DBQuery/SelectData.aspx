<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.SelectData" Codebehind="SelectData.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ����ֶ�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
			body{font-size:13px;margin:0px;}
			td{font-size:13px;}
		</style>
		<script>
			function SelectAll()
			{
				var rowCollection = document.getElementById("Table1").rows;
				var chkObj;
				for(var i=0, j=rowCollection.length; i<j; i++)
				{
					chkObj = rowCollection[i].cells[0].childNodes[0];
					if(chkObj.nodeType == "1")
					{
						if(chkObj.checked == false)
							chkObj.checked = true;
					}
				}
			}
			var SelTD = null;
			function Row_Onclick(cid)
			{
				cid.style.backgroundColor='skyblue';
				//alert(cid.HiddText);
				if(SelTD!=null&&SelTD!=cid)
				{
					SelTD.style.backgroundColor='Transparent';
				}
				SelTD = cid;
			}
			function Row_Ondbclick(cid)
			{
				var obj = document.getElementById(cid);
				Row_Onclick(obj);
				var ss = SelTD.HiddText;
				//alert(SelTD.id);
				parent.frames["eWebEditor1"].frames["eWebEditor"].focus();
				parent.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML(ss);
				
			}
			
			function Row_OndbclickColumnCName(cid)
			{
				//var obj = document.getElementById(cid);
				//Row_Onclick(obj);
				//var ss = SelTD.HiddText;
				//alert(SelTD.id);
				parent.frames["eWebEditor1"].frames["eWebEditor"].focus();
				parent.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML("<b>" + cid + "</b>");
				
			}
			
			
			function func()
			{
				alert('ok');
			}

		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" language="javascript" >

			<!--<div>ϵͳ��<%=Request.QueryString["Name"]%> ������<%=Request.QueryString["TableName"]%></div>-->
			<div><strong>�����ֶ�ѡ��</strong></div>

			
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="170px" border="1" style="BORDER-COLLAPSE: collapse;"  borderColor="#4199d8">
				<TR>
					<TD width="30px">ѡ��</TD>
					<TD width="110px">��������</TD> 
					<TD width=30px">����</TD>
				</TR>
				<%
				string ColumnEName,ColumnCName,ColumnTitle,ColumnKeyIndex,ColumnNoChange,ColumnAllowNull="No",ColumnDefault,strColumnType;
				int i=0,ColumnHeight=1;
				
				strPara +="<tr bgcolor=white>";          //������һ��<tr>
				foreach(XmlNode eachSource in nodeListSource)
				{
					string strColumnPrecision=eachSource.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
					//�ռ�����
					strColumnType =eachSource.Attributes.GetNamedItem("ColumnType").InnerXml;
					//�ֶ�Ӣ������
					ColumnEName=eachSource.Attributes.GetNamedItem("ColumnEName").InnerXml;
					//�ֶ���������
					ColumnCName = eachSource.Attributes.GetNamedItem("ColumnCName").InnerXml;
					
					strPara += "<td width=\"10%\" nowrap align=right>"+ColumnCName+": </td><td width=\"40%\">";                //�����ֶ���������
					
					//�ֶ�����
					ColumnKeyIndex=eachSource.Attributes.GetNamedItem("KeyIndex").InnerXml;

					//ֻ������
					ColumnNoChange = eachSource.Attributes.GetNamedItem("ReadOnly").InnerXml;

					//Ϊ������
					try
					{
					ColumnAllowNull = eachSource.Attributes.GetNamedItem("AllowNull").InnerXml;
					}
					catch{}
					
					//Ĭ��ֵ����
					ColumnDefault=eachSource.Attributes.GetNamedItem("ColumnDefault").InnerXml;
					

					//����¼��Ĺ���
					XmlNode myFunction=eachSource.SelectSingleNode("Input/@FunctionOnInput");
					XmlNode NodeData=null;
					string strTdData="";
					if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
					{
						NodeData=NodeTrBodyList[0].SelectSingleNode("td[@Column='"+ColumnEName+"']");
						if(NodeData!=null)
						strTdData=NodeData.InnerXml;
					}
					try
					{
					if(eachSource.SelectSingleNode("Input/@ColumnHeight").InnerXml!="")
					{

							ColumnHeight=Int32.Parse(eachSource.SelectSingleNode("Input/@ColumnHeight").InnerXml);
							
					}
					}
					catch
					{
						ColumnHeight=1;
					}
					
					string ValidateValue=" ";
					switch(strColumnType)
					{
						case "0":
							ValidateValue=" onfocus=\"window.status=\'���������ַ�\'\" ONKEYPRESS=\"window.status=\'���������ַ�\';\" ";
							break;
													
						case "1":
							//ValidateValue=" onfocus=\"window.status=\'ֻ����������\'\" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
							ValidateValue=" onfocus=\"window.status='ֻ����������'\" ONKEYPRESS=\"event.returnValue=IsValidateDigit(this);\" ";
							break;
						case "2":
							ValidateValue=" onchange=\"IsDate(this);\" ";
							break;
						case "3":
							break;
						case "4":
							break;
						case "5":
							break;
						case "6":
							break;						
					}
					ValidateValue="onfocus=\"window.status=1\" ";
					strControl="";
					switch(strColumnType)
					{
					case "0":
					case "1":
					case "2":
							if(ColumnHeight == 1)        //�ж������ĸ߶�
							{
							strControl +="<input id="+ColumnEName+" title=" +ColumnCName + "  ";
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange;
							strControl +=" style=\"WIDTH:100%\"  type=text value=\"\" databand  disabled  method=\"=\" ColumnDefault=\"\"  />";		
							}
							else
							{
							strControl +="<textarea id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange + "rows=" + ColumnHeight;
							strControl +=" style=\"WIDTH:100%\"  type=text databand    method=\"=\" ColumnDefault=\"\" ></textarea>";
							}
						break;
					case "3"://�ļ�
							strControl +="<input id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
							strControl +=" style=\"WIDTH:100%\" databand columnType=\"�ļ�\" value=\"\" disabled  method=\"=\" />";
						break;
					case "4"://����
							strControl +="<input id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
							strControl +=" style=\"WIDTH:100%\" databand columnType=\"����\" value=\"\" size=\"1\"  disabled  method=\"=\" />";
						break;
					case "5":
							switch(strColumnPrecision)//����
							{
								case "1": //������ѡ
										strControl +="<select id="+ColumnEName+" title=" +ColumnCName;
										strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
										strControl +=" style=\"WIDTH:100%\" databand  size=\"1\" readonly disabled  method=\"=\" ColumnDefault=\"\" /><option></option></select>";
										
										break;
								case "2"://������ѡ
										strControl +="<select id="+ColumnEName+" title=" +ColumnCName;
										strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
										strControl +=" databand  multiple size=\"3\" style=\"WIDTH:100%\" readonly disabled  method=\"=\" /><option></option></select>";
										
										break;
								case "3": //Radio ��ѡ
										strControl +="<input id="+ColumnEName ;
										strControl +=" databand type=Radio disabled  style=\"WIDTH:100%\" method=\"=\" />";
										break;
								case "4"://Check ��ѡ
										strControl +="<input id="+ColumnEName ;
										strControl +=" databand type=Check disabled  style=\"WIDTH:100%\" method=\"=\" />";
										break;
							}
					    break;
					case "6":
					
							strControl +="<select id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
							strControl +=" databand  multiple size=\"3\" style=\"WIDTH:100%\" readonly disabled  method=\"=\" /></select>";
						break;
					}
					
					strPara += strControl;
					//���Ϊ2��
					i++;
					if(i%2 ==0 )
					{
						strPara +="</td></tr>";
						if(i<nodeListSource.Count)
							strPara +="<tr bgcolor=white>";
					}
					else
					{
						strPara +="</td>";
					}
					if((i==nodeListSource.Count)&&(nodeListSource.Count%2==1))
							strPara +="<td></td><td></td>";
							
					string strColumnCName=eachSource.Attributes.GetNamedItem("ColumnCName").InnerXml;
				%>
				<TR id="aa<%=ColumnEName%>" style="BACKGROUND-COLOR: #ffffff;cursor:hand" 
				   onmouseover="this.style.color= 'red';" onmouseout="this.style.color= 'black';" HiddText='<%=strControl%>'>
					<TD><input id="<%=ColumnEName%>" type="checkbox"  value="<%=ColumnEName%>"
					   title="<%=strColumnCName%>" ></TD>
					<TD><a href="#" onclick="Row_OndbclickColumnCName('<%=strColumnCName%>')"><%=strColumnCName%></a></TD>
					<TD><input type="button" value="����" onclick="Row_Ondbclick('aa'+'<%=ColumnEName%>')"></TD>
				</TR>
				<%}%>
			</TABLE><br>
			<input type="button" value="ȫѡ" id="btnSelectAll" onclick="SelectAll()">&nbsp;&nbsp;
			<INPUT type="button" value="ȷ��" id="buttConfig" onclick=" return_Value()">&nbsp;&nbsp;
			<INPUT type="button" value="ȡ��" id="buttCancel" onclick="javascript:window.close()"><BR>
			<input type=radio name="position" checked title="���Զ�λ"/>���Զ�λ<input type=radio name="position">��Զ�λ
			<BR>
			<FONT size="2"><FONT size="3"><STRONG>ע�⣺</STRONG></FONT><BR>
				���û����������ֶΣ�<br>�뷵�ص��������д���</FONT><BR>
		</form>
		<script  language="javascript">
			SelectAll();
			var strpara="";
			function return_Value()
			{
				var str_returnValue='<TABLE  cellSpacing=1 cellPadding=0 width="100%" border=0 bgcolor=skyblue><TBODY>';
				str_returnValue += '<%=strPara%></TBODY></TABLE>';
				

				//alert('<%=strPara%>');
				//window.returnValue = str_returnValue;
				parent.frames["eWebEditor1"].frames["eWebEditor"].focus();
				parent.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML(str_returnValue);
				//window.close();
			}

	
	
	//===================ȫѡ��ť���===============

	//======================End=====================
		</script>
	</body>
</HTML>
