<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.DBSetting.TableConfigLeft" Codebehind="TableConfigLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>TableConfigLeft</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<style type="text/css">
			INPUT { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-WEIGHT: 500; BORDER-LEFT: 1px solid; COLOR: #ffffff; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: #0033ff }
			</style>
		<script language="javascript">
		<!--
			var db='<%=Request.QueryString["db"]%>';
			var CurrentTable=null;
			var LastObj=null
			function LinkMainTable(obj)
			{			
				CurrentTable=obj;	
				
				//obj.style.backgroundColor="skyblue";
				obj.style.backgroundColor = "#ff9900";
				if(LastObj!=null&&LastObj!=obj)
					LastObj.style.backgroundColor="white";
				LastObj=obj;
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===================���ݱ�Ӣ����===========
				var strEName = new String();
				strEName = obj.TableEName;
				strEName = strEName.substring(0, strEName.length-1);
				//===================
				
				window.parent.frames["Content"].document.location.href = "TablesConfigMain.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str + "&TableEName=" + strEName;
			
				return false;
			}
			
			function createNew()
			{
			    
				var r;
				if(CurrentTable==null)
				{
					var confirm=window.confirm("Ҫ����������\n���Ҫ�����ӱ����ȵ��������ϼ���");
					
					if(!confirm)
						return false;
						
					var name;
					r=window.showModalDialog('PromptNewTable.aspx?dbname=' + db,'','dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
					if (r == '' || typeof(r) == 'undefined')
					{
						return;
					}
				}
				else
				{
					var confirm=window.confirm("Ҫ��[" + CurrentTable.title.substr(0,CurrentTable.title.length-1) + "]���½�����");
					
					if(!confirm)
						return false;
						
					var name;
					r=window.showModalDialog('PromptNewTable.aspx?dbname=' + db,'','dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
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
					r=window.showModalDialog('PromptNewTable.aspx?DataTableName=' + curTableName + '&DataTableID=' + curTableID + '&dbname=' +db, '', 'dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
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
				
				obj.style.backgroundColor="#ff9900";
			
				if(LastDictObj!=null&&LastDictObj!=obj)
					LastDictObj.style.backgroundColor="white";
				LastDictObj=obj;
				var str = new String();
				str = obj.title;
				window.parent.frames["Content"].document.location.href = "../Admin/DictionariesConfigMain.aspx?DictName=" + str;//��Name����
				
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
		<!--�ֵ��Ľű�-->
		<script language="javascript">
			function createNewDict()
			{	
				var name;
				r=window.showModalDialog('../Admin/PromptNewDictTable.aspx','','dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
				if (r == '' || typeof(r) == 'undefined')
				{
					return;
				}
				
				document.all["CollectionNewDictTable"].value=r;
				document.all["DictActions"].value="New";
				Form1.submit();
				return true;
			}
			
			function modifyDictTable()
			{
				var r;
				if(CurrentDictionaryTable==null)
				{
					alert('��ѡ��Ҫ�޸ĵ��ֵ����');
					return false;
				}
				else
				{
					var dictName = CurrentDictionaryTable.title.substr(0,CurrentDictionaryTable.title.length);
					var dictID = CurrentDictionaryTable.id;
					
					var confirm=window.confirm("ȷ��Ҫ�޸��ֵ�� [" + CurrentDictionaryTable.title.substr(0,CurrentDictionaryTable.title.length) + "]��");
					
					if(!confirm)
						return false;
						
					var name;
					r=window.showModalDialog('../Admin/PromptNewDictTable.aspx?DictTableName=' + dictName + '&DictTableID=' + dictID, '',  'dialogWidth:258px;dialogHeight:128px;help:no;scroll:auto;status:no');
					if (r == '' || typeof(r) == 'undefined')
					{
						return;
					}
					
				}
				
				if( r != (dictName + "," + dictID) )
				{
					document.all["CollectionNewDictTable"].value=CurrentDictionaryTable.title + "," + r;
					document.all["DictActions"].value="Modify";
					Form1.submit();
					return true;
				}
				
				return false;
			}
			
			function deleteDictTable()
			{
				var r;
				if(CurrentDictionaryTable==null)
				{
					alert('��ѡ��Ҫɾ���ı���');
					return false;
				}
				else
				{
					var confirm=window.confirm("ȷ��Ҫɾ���ֵ�� [" + CurrentDictionaryTable.title.substr(0,CurrentDictionaryTable.title.length) + "]��");
					
					if(!confirm)
						return false;
						
					r=CurrentDictionaryTable.title.substr(0,CurrentDictionaryTable.title.length);
				}
				
				document.all["CollectionNewDictTable"].value=r;
				document.all["DictActions"].value="Delete";
				Form1.submit();
				return true;
			}
		</script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>���ݱ����<%//Response.Write(CollectionNewTable.Value);%>
				<TABLE id="tableDataName" style="BORDER-RIGHT: #0033ff 1px solid; BORDER-TOP: #0033ff 1px solid; BORDER-LEFT: #0033ff 1px solid; BORDER-BOTTOM: #0033ff 1px solid"
					runat="server">
					<TR>
						<TD><FONT face=����></FONT></TD>
					</TR>
				</TABLE>
			</P>
			<P><INPUT id="buttCreateTable" type="button" value="�½���"><br>
				<br>
				<INPUT id="buttModifyTable" type="button" value="�޸ı�"><br>
				<br>
				<INPUT id="ButtDeleteTable" type="button" value="ɾ����"></P>
			<p></p>
			�ֵ�����
			<TABLE id="tableDictionary" style="BORDER-RIGHT: #0033ff 1px solid; BORDER-TOP: #0033ff 1px solid; BORDER-LEFT: #0033ff 1px solid; BORDER-BOTTOM: #0033ff 1px solid"
				runat="server">
			</TABLE>
			<P><INPUT id="btnCreateDictTable" onclick="createNewDict()" type="button" value="�½���"><br>
				<br>
				<INPUT id="btnModifyDictTable" type="button" value="�޸ı�" onclick="modifyDictTable()"><br>
				<br>
				<INPUT id="btnDeleteDictTable" type="button" value="ɾ����" onclick="deleteDictTable()">
			</P>
			<P><input type="hidden" name="Actions"> <input id="CollectionNewTable" type="hidden" runat="server">
			</P>
			<p><input type="hidden" name="DictActions"> <input id="CollectionNewDictTable" type="hidden" runat="server">
			</p>
			<P><asp:label id="LabMsg" runat="server" ForeColor="Blue" BorderWidth="1px" BorderStyle="Solid"
					BorderColor="#8080FF" Width="128px"></asp:label><input type="hidden" value="<%=Request.QueryString["db"] %>" id="hHidden" name="hHidden" /></P>
		</form>
	</body>
</HTML>
