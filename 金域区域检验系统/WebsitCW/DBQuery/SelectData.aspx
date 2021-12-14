<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.SelectData" Codebehind="SelectData.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择表字段</title>
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

			<!--<div>系统：<%=Request.QueryString["Name"]%> 表名：<%=Request.QueryString["TableName"]%></div>-->
			<div><strong>数据字段选择</strong></div>

			
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="170px" border="1" style="BORDER-COLLAPSE: collapse;"  borderColor="#4199d8">
				<TR>
					<TD width="30px">选择</TD>
					<TD width="110px">中文名称</TD> 
					<TD width=30px">操作</TD>
				</TR>
				<%
				string ColumnEName,ColumnCName,ColumnTitle,ColumnKeyIndex,ColumnNoChange,ColumnAllowNull="No",ColumnDefault,strColumnType;
				int i=0,ColumnHeight=1;
				
				strPara +="<tr bgcolor=white>";          //先输入一个<tr>
				foreach(XmlNode eachSource in nodeListSource)
				{
					string strColumnPrecision=eachSource.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
					//空件类型
					strColumnType =eachSource.Attributes.GetNamedItem("ColumnType").InnerXml;
					//字段英文名字
					ColumnEName=eachSource.Attributes.GetNamedItem("ColumnEName").InnerXml;
					//字段中文名字
					ColumnCName = eachSource.Attributes.GetNamedItem("ColumnCName").InnerXml;
					
					strPara += "<td width=\"10%\" nowrap align=right>"+ColumnCName+": </td><td width=\"40%\">";                //输入字段中文名字
					
					//字段索引
					ColumnKeyIndex=eachSource.Attributes.GetNamedItem("KeyIndex").InnerXml;

					//只读属性
					ColumnNoChange = eachSource.Attributes.GetNamedItem("ReadOnly").InnerXml;

					//为空属性
					try
					{
					ColumnAllowNull = eachSource.Attributes.GetNamedItem("AllowNull").InnerXml;
					}
					catch{}
					
					//默认值属性
					ColumnDefault=eachSource.Attributes.GetNamedItem("ColumnDefault").InnerXml;
					

					//处理录入的功能
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
							ValidateValue=" onfocus=\"window.status=\'可以输入字符\'\" ONKEYPRESS=\"window.status=\'可以输入字符\';\" ";
							break;
													
						case "1":
							//ValidateValue=" onfocus=\"window.status=\'只能输入数字\'\" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
							ValidateValue=" onfocus=\"window.status='只能输入数字'\" ONKEYPRESS=\"event.returnValue=IsValidateDigit(this);\" ";
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
							if(ColumnHeight == 1)        //判断输入框的高度
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
					case "3"://文件
							strControl +="<input id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
							strControl +=" style=\"WIDTH:100%\" databand columnType=\"文件\" value=\"\" disabled  method=\"=\" />";
						break;
					case "4"://新闻
							strControl +="<input id="+ColumnEName+" title=" +ColumnCName;
							strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
							strControl +=" style=\"WIDTH:100%\" databand columnType=\"新闻\" value=\"\" size=\"1\"  disabled  method=\"=\" />";
						break;
					case "5":
							switch(strColumnPrecision)//精度
							{
								case "1": //下拉单选
										strControl +="<select id="+ColumnEName+" title=" +ColumnCName;
										strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
										strControl +=" style=\"WIDTH:100%\" databand  size=\"1\" readonly disabled  method=\"=\" ColumnDefault=\"\" /><option></option></select>";
										
										break;
								case "2"://下拉多选
										strControl +="<select id="+ColumnEName+" title=" +ColumnCName;
										strControl +="  AllowNull="+ColumnAllowNull + "  KeyIndex=" +ColumnKeyIndex + " NoChange="+ColumnNoChange ;
										strControl +=" databand  multiple size=\"3\" style=\"WIDTH:100%\" readonly disabled  method=\"=\" /><option></option></select>";
										
										break;
								case "3": //Radio 单选
										strControl +="<input id="+ColumnEName ;
										strControl +=" databand type=Radio disabled  style=\"WIDTH:100%\" method=\"=\" />";
										break;
								case "4"://Check 多选
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
					//输出为2列
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
					<TD><input type="button" value="插入" onclick="Row_Ondbclick('aa'+'<%=ColumnEName%>')"></TD>
				</TR>
				<%}%>
			</TABLE><br>
			<input type="button" value="全选" id="btnSelectAll" onclick="SelectAll()">&nbsp;&nbsp;
			<INPUT type="button" value="确定" id="buttConfig" onclick=" return_Value()">&nbsp;&nbsp;
			<INPUT type="button" value="取消" id="buttCancel" onclick="javascript:window.close()"><BR>
			<input type=radio name="position" checked title="绝对定位"/>绝对定位<input type=radio name="position">相对定位
			<BR>
			<FONT size="2"><FONT size="3"><STRONG>注意：</STRONG></FONT><BR>
				如果没有您所需的字段，<br>请返回单表数据中创建</FONT><BR>
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

	
	
	//===================全选按钮点击===============

	//======================End=====================
		</script>
	</body>
</HTML>
