<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.TableConfigLeft" Codebehind="TableConfigLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TableConfigLeft</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			
			var CurrentTable=null;
			var LastObj=null
			function LinkMainTable(obj)
			{			
				CurrentTable=obj;	
				
				obj.style.backgroundColor="skyblue";
				
				if(LastObj!=null&&LastObj!=obj)
					LastObj.style.backgroundColor="white";
				LastObj=obj;
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				var strEName = new String();
				strEName = obj.id;
				//strEName = strEName.substring(0, strEName.length-1);
				
				window.parent.frames["Content"].document.location.href = "TablesConfigMain.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" 
				    + str + '&TableEName='+ strEName;
			
				return false;
			}
			
			
			var CurrentTableButton=null;
			var LastObjButton=null
			function LinkMainTableButton(obj)
			{			
				CurrentTableButton=obj;	
				
				obj.style.backgroundColor="gray";
				
				if(LastObjButton!=null&&LastObjButton!=obj)
					LastObjButton.style.backgroundColor="white";
				LastObjButton=obj;
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//var strEName = new String();
				//strEName = obj.id;
				//strEName = strEName.substring(0, strEName.length-1);
				
				window.parent.frames["Content"].document.location.href = "TablesButtons.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
			
				return false;
			}
			
			
			function createNew()
			{
				var r;
				r=window.showModalDialog('../input/SelectModalDialog.aspx?../Admin/WinOpenSelectTable.aspx?<%=Request.ServerVariables["Query_String"]%>','','dialogWidth:458px;dialogHeight:528px;help:no;scroll:auto;status:no');
				if (r == '' || typeof(r) == 'undefined')
				{
					return false;
				}
				document.all["CollectionNewTable"].value=r;
				document.all["Actions"].value="New";
				Form1.submit();
				return true;
			}
			
			function modifyTable()
			{
				var r;
				if(CurrentTable==null)
				{
					alert('��ѡ��Ҫ�޸ĵı���');
					return false;
				}
				else
				{
					var curTableName = CurrentTable.title.substr(0,CurrentTable.title.length-1);
					var curTableID = CurrentTable.id;
					
					if(curTableName.lastIndexOf("/") != -1)
					{
						curTableName = curTableName.substr(curTableName.lastIndexOf("/")+1);
					}
					
					var confirm=window.confirm("ȷ��Ҫ�޸ı� [" + CurrentTable.title.substr(0,CurrentTable.title.length-1) + "]��");
					
					if(!confirm)
						return false;
						
					var name;
					r=window.showModalDialog('PromptNewTable.aspx?DataTableName=' + curTableName + '&DataTableID=' + curTableID, '', 'dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
					if (r == '' || typeof(r) == 'undefined')
					{
						return;
					}
					//����/�ӱ�/�±�,�±���
					//����/�±�,�±���
					//�±�,�±���
					r=CurrentTable.title+r;
				}
				document.all["CollectionNewTable"].value=r;
				document.all["Actions"].value="Modify";
				Form1.submit();
				return true;
			}
			
			function deleteTable()
			{
				var r;
				if(CurrentTable==null)
				{
					alert('��ѡ��Ҫɾ���ı���');
					return false;
				}
				else
				{
					var confirm=window.confirm("ȷ��Ҫɾ���� [" + CurrentTable.title.substr(0,CurrentTable.title.length-1) + "]��\n��ص�����Ҳ�ᱻɾ����");
					
					if(!confirm)
						return false;
						
					r=CurrentTable.title.substr(0,CurrentTable.title.length-1);
				}
				document.all["CollectionNewTable"].value=r;
				document.all["Actions"].value="Delete";
				Form1.submit();
				return true;
			}
			
			//======================�ֵ��==============================
			var CurrentDictionaryTable=null;
			var LastDictObj=null
			function LinkDictionaryTable(obj)
			{
				CurrentDictionaryTable=obj;	
				
				obj.style.backgroundColor="skyblue";
			
				if(LastDictObj!=null&&LastDictObj!=obj)
					LastDictObj.style.backgroundColor="white";
				LastDictObj=obj;
				var str = new String();
				str = obj.title;
				window.parent.frames["Content"].document.location.href = "DictionariesConfigMain.aspx?DictName=" + str;//��Name����
				
				return false;
			}
			//=======================End================================
		//-->
		</script>
		<script language="javascript" event="onclick" for="buttCreateTable">
			 return createNew();
		</script>
		<script language="javascript" event="onclick" for="buttModifyTable">
			 return modifyTable();
		</script>
		<script language="javascript" event="onclick" for="ButtDeleteTable">
			 return deleteTable();
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>���ܹ���<%//Response.Write(CollectionNewTable.Value);%>
				<TABLE id="tableDataName" style="BORDER-RIGHT: #0099cc 1px solid; BORDER-TOP: #0099cc 1px solid; BORDER-LEFT: #0099cc 1px solid; BORDER-BOTTOM: #0099cc 1px solid"
					runat="server">
					<TR>
						<TD></TD>
					</TR>
				</TABLE>
			</P>
			<P><INPUT id="buttCreateTable" type="button" value="ѡ����"><br>
				<br>
				<INPUT id="buttModifyTable" type="button" value="�� �� ��"><br>
				<br>
				<INPUT id="ButtDeleteTable" type="button" value="ɾ������"></P>
			<p></p>
			<P>
			</P>
			<P><input type="hidden" name="Actions" style="WIDTH: 8px; HEIGHT: 22px" size="1"> <input id="CollectionNewTable" type="hidden" runat="server" style="WIDTH: 40px; HEIGHT: 22px"
					size="1">
			</P>
			<P><asp:label id="LabMsg" runat="server" ForeColor="Blue" BorderWidth="1px" BorderStyle="Solid"
					BorderColor="#8080FF" Width="128px"></asp:label></P>
			<p>��ť����
				<TABLE id="tableButtons" style="BORDER-RIGHT: #0099cc 1px solid; BORDER-TOP: #0099cc 1px solid; BORDER-LEFT: #0099cc 1px solid; BORDER-BOTTOM: #0099cc 1px solid"
					runat="server">
					<TR>
						<TD><FONT face="����"></FONT></TD>
					</TR>
				</TABLE>
			</p>
			<p><a href="javascript:func()">�����ĵ�</a></p>
			
			<script>
			
			function func()
			{
						window.parent.frames["Content"].document.location.href ="helpdoc.aspx?<%=Request.ServerVariables["Query_String"]%>";
			
			}
			
			</script>
		</form>
	</body>
</HTML>
