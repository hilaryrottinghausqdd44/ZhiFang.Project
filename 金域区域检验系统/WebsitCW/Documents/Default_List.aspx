<%@ Page language="c#" Codebehind="Default_List.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents._Default_List" codePage="936" culture="zh-CN" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System"%>
<%@ Import Namespace="NewsWebService"%>
<%@ Import Namespace="System.Collections"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="VBScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<LINK href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<!--script language="vbscript" id="clientEventHandlersVBS">
<!--

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' FileSystemObject Sample Code
' Copyright 1998 Microsoft Corporation.   All Rights Reserved. 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'Option Explicit

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Regarding code quality:
' 1) The following code does a lot of string manipulation by 
'    concatenating short strings together with the "&" operator. 
'    Since string concatenation is expensive, this is a very 
'    inefficient way to write code. However, it is a very 
'    maintainable way to write code, and is used here because this 
'    program performs extensive disk operations, and because the 
'    disk is much slower than the memory operations required to 
'    concatenate the strings. Keep in mind that this is demonstration 
'    code, not production code.
'
' 2) "Option Explicit" is used, because declared variable access is 
'    slightly faster than undeclared variable access. It also prevents 
'    bugs from creeping into your code, such as when you misspell 
'    DriveTypeCDROM as DriveTypeCDORM.
'
' 3) Error handling is absent from this code, to make the code more 
'    readable. Although precautions have been taken to ensure that the 
'    code will not error in common cases, file systems can be 
'    unpredictable. In production code, use On Error Resume Next and 
'    the Err object to trap possible errors.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Some handy global variables
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
on error resume next
Dim FSO
'Set FSO = CreateObject("Scripting.FileSystemObject")
Dim TabStop
Dim NewLine

Const TestDrive = "C"
Const TestFilePath = "C:\test"

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Constants returned by Drive.DriveType
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Const DriveTypeRemovable = 1
Const DriveTypeFixed = 2
Const DriveTypeNetwork = 3
Const DriveTypeCDROM = 4
Const DriveTypeRAMDisk = 5

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Constants returned by File.Attributes
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Const FileAttrNormal   = 0
Const FileAttrReadOnly = 1
Const FileAttrHidden = 2
Const FileAttrSystem = 4
Const FileAttrVolume = 8
Const FileAttrDirectory = 16
Const FileAttrArchive = 32 
Const FileAttrAlias = 1024
Const FileAttrCompressed = 2048

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Constants for opening files
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Const OpenFileForReading = 1 
Const OpenFileForWriting = 2 
Const OpenFileForAppending = 8 



''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' ����,iif���������ո�
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function AddSpace(Byval str,iCount)
	If len(str)>iCount Then
		AddSpace=left(str,iCount-2) + ".."
	Else
		AddSpace=str + Replace(space(iCount-len(str))," ","&nbsp;")
	End IF
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' �ӳ���,��ʼ��ȫ������
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub init()
   
   ' Set up global data.
   TabStop = Chr(9)
   NewLine = Chr(10)
   
   Set FSO = CreateObject("Scripting.FileSystemObject")

End Sub
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' �ӳ���,�г����е�������
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function GetDrives(FSO)
   Dim Drives
   Dim Drive
   Dim S
   Dim myOption

   Set Drives = FSO.Drives
   selectDrives.innerHTML=""
   For Each Drive In Drives
	  If Drive.IsReady then
	  	set myOption=document.createElement("OPTION")
		myOption.Text=Drive.DriveLetter
		myOption.Value=Drive.DriveLetter
		selectDrives.add myOption
	 Else
		window.status="���ܶ�ȡ" + Drive.DriveLetter + "������������ԭ��"
	  End If
   Next
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' �ӳ���,�г�ָ��Ŀ¼
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function GetFolders(FolderString)
	window.status="���ڴ��ļ��б����Ժ�...."
    Dim SubFolders
    Dim myFolder
    Dim i
    Dim Folder
    Dim strFolderList
    
    'msgbox typename(FSO)
    'exit Function
   
    If typename(FSO)<>"FileSystemObject" then
		 Set FSO = CreateObject("Scripting.FileSystemObject")
    End if
    
    
    set Folder=FSO.GetFolder(FolderString)
    filepath.innerHTML=Folder.Path
    
    Set SubFolders = Folder.SubFolders
    strFolderList=""
    html.innerHTML=""
    'html.innerHTML="<table border=0 cellPadding=15><tr>"
    i=0
    If len(FolderString)>3 Then
			strFolderList="" + _
			"<div ondblclick='VBScript:GetFolders "+chr(34) + _
			Folder.ParentFolder.Path  + chr(34)+"'><img id='folder' src='../Documents/images/folder16.gif' title='" + _
			Folder.Path+"' ondblclick='VBScript:GetFolders "+chr(34) + _
			Folder.ParentFolder.Path  + chr(34)+"' style='CURSOR: hand'>&nbsp;" + _
			"��һ��Ŀ¼...</div>"
	End if
	
    For each myFolder in SubFolders
		j=doevents
		i=i+1
		j=doevents
			strFolderList=strFolderList + _
			"<div ondblclick='VBScript:GetFolders "+chr(34) + _
			myFolder.Path  + chr(34)+"'><input id='folderCheck'"+cstr(i)+" type='checkbox'>&nbsp;" + _
			"<img id='folder' src='../Documents/images/folder16.gif' title='" + _
			myFolder.Path+"' ondblclick='VBScript:GetFolders "+chr(34) + _
			myFolder.Path  + chr(34)+"' style='CURSOR: hand'>&nbsp;" + _
			myFolder.Name + "</div>"
		'If i mod 5=0 then
			'html.innerHTML=html.innerHTML + "</tr><tr>"
		'End If
		j=doevents
    Next
    'html.innerHTML= html.innerHTML + "</tr></table>"
    strFolderList=strFolderList+GetFiles(Folder)
    html.innerHTML=strFolderList
	window.status="���"
	html.click 
	selectDrives.blur
	
	'����ػ�˫���¼�����Ҫ����������һ������??
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' �ӳ���,�г�ָ��Ŀ¼�ڵ�ȫ���ļ�
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function GetFiles(Folder)
   Dim Files
   Dim File
   Dim strFilesList
   Dim i,j
   
   strFilesList=""
   i=0
   Set Files = Folder.Files
   For each File in Files
		attr= File.Attributes
		If Attr And FileAttrArchive Then 'S = S & "Archive "
			'If File.Attributes(32) Then 'Archive �ļ��ܶ�
			i=i+1
			j=doevents
			strFilesList=strFilesList + "<input id='fileCheck'"+cstr(i)+" type='checkbox'>&nbsp;" + _
				"<img id='folder' src='../Documents/images/file16.gif' title='" + _
				Folder.Path+"\" + File.Name + "' style='CURSOR: hand'>&nbsp;" + _
				AddSpace(File.Name,30) + AddSpace(Cstr(int(File.Size/1024)+1) + "KB",10) + _
				File.Type + "<br>"
		End If
   Next
   GetFiles=strFilesList
   
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' �ӳ���,�˻ص���Ŀ¼
' ���Ӽ�,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub browse_onclick
	If selectDrives.selectedIndex<0 Then
		call init
		GetDrives(FSO)
	End If
	If FSO.GetDrive(selectDrives.options(selectDrives.selectedIndex).Text).IsReady then
		If selectDrives.selectedIndex>-1 then
			filepath.innerHTML=selectDrives.options(selectDrives.selectedIndex).Text + ":\"
			GetFolders(selectDrives.options(selectDrives.selectedIndex).Text + ":\")
		End If
	Else
		msgbox "���ܶ�ȡ [" + selectDrives.options(selectDrives.selectedIndex).Text + ":] ������������ԭ��"
	End If
End Sub

Sub selectDrives_onchange
	call init
	'on error resume next
	If FSO.GetDrive(selectDrives.options(selectDrives.selectedIndex).Text).IsReady then
		If selectDrives.selectedIndex>-1 then
			GetFolders(selectDrives.options(selectDrives.selectedIndex).Text + ":\")
		End If
	Else
		msgbox "���ܶ�ȡ [" + selectDrives.options(selectDrives.selectedIndex).Text + ":] ������������ԭ��"
	End If
	  
End Sub

Sub window_onload
	<%if(Request.ServerVariables["REQUEST_METHOD"]=="GET"){%>
	on error resume next
	call init
	GetDrives(FSO)
	if selectDrives.options(selectDrives.selectedIndex).Text<>"C" Then
		selectDrives.options(selectDrives.selectedIndex+1).selected=true
	end if
	filepath.innerHTML=selectDrives.options(selectDrives.selectedIndex).Text + ":\"
	GetFolders(selectDrives.options(selectDrives.selectedIndex).Text + ":\")
	if err.number<>0 Then
		msgbox "�������ò�����ִ�пͻ��˽ű������ܲ鿴�����ļ�" + vbcrlf + _
			"�����ýű�ִ����ɣ����������İ�ȫ�Խ���:" + vbcrlf + vbcrlf + _
			"* ����� [Internetѡ��]" + vbcrlf + _
			"* [��ȫ]->[�Զ��弶��]->[ActiveX�ؼ�����]" + vbcrlf + _
			"* [��û�б��Ϊ��ȫ��ActiveX�ؼ����г�ʼ���ͽű�����]" + vbcrlf + _
			"* [����]->>���ȷ��" ,vbcritical,"�ļ��ϴ�����"
	End if
	<%}%>
		End Sub --> </script-->
		<script id="clientEventHandlersJS" language="javascript">
<!--


function showadd(sid)
{
	whichEl = eval("Div" +sid + "a");
	eval("deletefile" + ".style.display=\"\";");
	whichEl.style.backgroundColor="blue";
}
function hide(sid)
{
	whichEl = eval("Div" +sid + "a");
	eval("deletefile" + ".style.display=\"\";");
	whichEl.style.backgroundColor="blue";
}

function showMouseOver(sid)
{
	whichEl = document.all[sid];
	whichEl.style.backgroundColor = "gray";
}
function showMouseOut(sid)
{
	whichEl = document.all[sid];
	whichEl.style.backgroundColor= "transparent";
		
}
function trim_string(strOriginalValue) 
{
	var ichar, icount;
	var strValue = strOriginalValue;
	ichar = strValue.length - 1;
	icount = -1;
	while (strValue.charAt(ichar)==' ' && ichar > icount)
		--ichar;
	if (ichar!=(strValue.length-1))
		strValue = strValue.slice(0,ichar+1);
	ichar = 0;
	icount = strValue.length - 1;
	while (strValue.charAt(ichar)==' ' && ichar < icount)
		++ichar;
	if (ichar!=0)
		strValue = strValue.slice(ichar,strValue.length);
	return strValue;
}
function __doBeforeSubmit2()
{
	var bSucceeded = true;
	var strDir = trim_string(frmContent.fileUpload.value);
	
	
	if (strDir.length==0)
	{
		frmContent.fileUpload.style.borderColor = "red";
		bSucceeded = false;
	}
	if (!bSucceeded)
	{
		document.all("lblMessage").style.color = "red";
		document.all("lblMessage").innerHTML = "<br><b>��ָ���ϴ����ļ���������ɫ��Ĳ��ֱ�����д��</b>";
		event.returnValue = false;
		event.cancelBubble = true;
	}
	else
	{
		event.returnValue = false;
		event.cancelBubble = true;
		frmContent.action.value = "UploadFile";
		frmContent.submit();
	}
}
function __doBeforeSubmit()
{
	var bSucceeded = true;
	var strDir = trim_string(frmContent.txtNewDir.value);
	

	if (strDir.length==0)
	{
		frmContent.txtNewDir.style.borderColor = "red";
		bSucceeded = false;
	}
	if (!bSucceeded)
	{
		document.all("lblMessage").style.color = "red";
		document.all("lblMessage").innerHTML = "<br><b>�ļ���������Ϊ�գ���ɫ��Ĳ��ֱ�����д��</b>";
		event.returnValue = false;
		event.cancelBubble = true;
	}
	else
	{
		event.returnValue = false;
		event.cancelBubble = true;
		frmContent.action.value = "CreateDir";
		frmContent.submit();
	}
}
		
function __ReName()
{
	if(window.document.all["name"].value=="...")
	{
		alert("��Ŀ¼������������");
		return false;
	}
	if(window.document.all["foldername"].value=='')
	{
		alert("��ѡ���ļ����ļ��У�")
		return false;
	}
	if(window.document.all['Com'].value=='True')
	{
		alert("�ļ��л������ļ����ѹ�������ȡ������");
		return false;
	}
	else
	{
		if (window.confirm("ȷ��Ҫ�����ļ�����"))
		{
			frmContent.action.value = "ReName";			
			frmContent.submit();		
		}
	}
	return false;
}
function __doDeleteFile(strFile)
{
	if(window.document.all["name"].value=="...")
	{
		alert("��Ŀ¼����ɾ����");
		return false;
	}
	if(window.document.all["foldername"].value=="")
	{
		alert("��ѡ���ļ������ļ��У�");
		return false;
	}
	//if(window.document.all['Com'].value=='True')
	//{
	//	alert("�ļ��ѹ�������ȡ������");
	//	return false;
	//}
	if (window.confirm("ȷ��Ҫɾ���ļ� \""+strFile.substring(strFile.lastIndexOf("\\")+1,strFile.length)+"\"��"))
	{
		frmContent.action.value = "DeleteFile";
		frmContent.actionParam.value = strFile;
		frmContent.submit();
	}
}

function __doDeleteDir(strDir)
{
	if(window.document.all["name"].value=="...")
	{
		alert("��Ŀ¼����ɾ����");
		return false;
	}
	if(window.document.all["foldername"].value=="")
	{
		alert("��ѡ���ļ������ļ��У�");
		return false;
	}
	if(window.document.all['Com'].value=='True')
	{
		alert("�ļ����ѹ�������ȡ������");
		return false;
	}
	if (window.confirm("ȷ��Ҫɾ��Ŀ¼ \""+strDir.substring(strDir.lastIndexOf("\\")+1,strDir.length)+"\"��"))
	{
		frmContent.action.value = "DeleteDir";
		frmContent.actionParam.value = strDir;
		frmContent.submit();
	}
}

//-->
		</script>
		<script language="javascript">
					
					function AddShare()
					{
						if(window.document.all["name"].value=="...")
						{
							alert("���ļ��в��ܹ���");
							return false;
						}
						if(window.document.all["type"].value!="dir")
						{
							alert("�ļ����ܹ���");
							return false;
						}
						window.open('Shared.aspx?FolderDir='+escape(window.document.all["del"].value),'','width=520px,height=340px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
					}
					var SelDoc = '';
			
					function SelectDocument(doc,foldername,type,comsharedir,name)
					{
						window.document.all["del"].value=foldername;
						window.document.all["Com"].value=comsharedir;
						window.document.all["type"].value=type;
						window.document.all["foldername"].value=foldername;
						window.document.all["name"].value=name;
						//alert(window.document.all["foldername"].value)
						if (SelDoc != '')
						{
							document.all[SelDoc].style.backgroundColor = '';
							document.all[SelDoc].style.color = '';
						}
						SelDoc=doc.id;
						document.all[SelDoc].style.backgroundColor = 'skyblue';
						document.all[SelDoc].style.color = 'white';
					}
					function DelShare()
					{
						window.open('Shared.aspx?FolderDir=' + escape(window.document.all["del"].value));
					}
		</script>
		<link rel="stylesheet" type="text/css" href="../explorer.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottommargin="0" rightmargin="0" leftmargin="0" topmargin="0">
		<script type="text/javascript" src="../Includes/Tools/scripts/wz_dragdrop.js"></script>
		<input type=hidden value='<%=filePath.Replace("\\","\\\\")%>' id="idfolder"/>
		<TABLE id="Table1" style="BORDER-RIGHT: #ffe7d9 1px solid; BORDER-TOP: #ffe7d9 1px solid; Z-INDEX: 101; BORDER-LEFT: #ffe7d9 1px solid; WIDTH: 752px; BORDER-BOTTOM: #ffe7d9 1px solid; POSITION: relative; HEIGHT: 22px"
			cellSpacing="4" cellPadding="1" width="248" align="center" border="0">
			<%
					if(strfiletype == "2")
					{
					%>
			<tr>
				<td align="center">ͼ��</td>
				<td>����</td>
				<td>��С</td>
				<td>�޸�ʱ��</td>
				<td>����ʱ��</td>
			</tr>
			<%}%>
			<TR>
				<% 
				int i=0;
				foreach(DataRow dr in dt.Rows)
				{i+=1;
					%>
				<TD align="center">
					<%
					ComShare=false;
					ComShareDir=false;
					string myType=dr[1].ToString();
					string folder="";
					
					if (myType=="dir")
					{
						if(dr[0].ToString()=="...")
						{	if(filePath.LastIndexOf("\\")>-1)						
								folder=filePath.Substring(0,filePath.LastIndexOf("\\"));							
							else
								folder="\\";
							
						}
						else
						{
							folder=filePath+"\\" +dr[0].ToString();
							string ComSql="select ID from RBAC_Modules where Para like '"+"Folder="+"\\"+sEmployID+folder+"'";							
							SqlServerDB ComDb=new SqlServerDB();
							ComDs=ComDb.ExecDS(ComSql);
							
							if(ComDs.Tables[0].Rows.Count!=0)
								ComShare=true;
								
							string ComSqldir="select ID from RBAC_Modules where Para like '"+"Folder="+"\\"+sEmployID+folder+"%'";							
							SqlServerDB ComDbdir=new SqlServerDB();
							ComDsdir=ComDbdir.ExecDS(ComSqldir);
							
							if(ComDsdir.Tables[0].Rows.Count!=0)
								ComShareDir=true;
							
						}
					}
					else
					{
							string ComSqldir="select ID from RBAC_Modules where Para like '"+"Folder="+"\\"+sEmployID+folder+"'";							
							SqlServerDB ComDbdir=new SqlServerDB();
							ComDsdir=ComDbdir.ExecDS(ComSqldir);
							
							if(ComDsdir.Tables[0].Rows.Count!=0)
								ComShareDir=true;
						folder=filePath;
					}
					folder=folder.Replace("\\","\\\\");
					//strfolder = folder;	
					%>
					<div id="Div<%=i.ToString()%>a" title="<%=dr[0].ToString()%>"
						<%
						if(myType=="dir")
						{
						%>
							ondblclick="javascript:window.open('?filetype='+<%=strfiletype%>+'&&folder=' + escape('<%=folder%>'),'_self')" 
						<%if(dr[0].ToString()!="..."){%>
							onkeydown="javascript:if(window.event.keyCode==46) __doDeleteDir('<%=folder%>')"
							
						<%}%>
							onclick="SelectDocument(this,'<%=folder%>','<%=myType%>','<%=ComShareDir%>','<%=dr[0].ToString()%>')"
						<%}
						else
						{
						//ondblclick="window.open('../browse/download.aspx?file=<%=folder.Replace("\\\\","/")%%%%%&>/<%=dr[0].ToString()%%%%%%&>')" 
						%>
							
							ondblclick="javascript:window.open('download.aspx?file=' + escape('<%=folder%>\\<%=dr[0].ToString()%>'),'frmDownload','width=50px;,height=50px left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 )" 
							onfocus="javascript:showMouseOver('Div<%=i.ToString()%>a')"��tabindex="<%=i.ToString()%>"
							onkeydown="javascript:if(window.event.keyCode==46) __doDeleteFile('<%=folder%>\\<%=dr[0].ToString()%>')"
							onclick="SelectDocument(this,'<%=folder%>\\<%=dr[0].ToString()%>','<%=myType%>','<%=ComShareDir%>','<%=dr[0].ToString()%>')"
						<%}%>
						onmouseover="javascript:this.style.filter='progid:DXImageTransform.Microsoft.Wave(freq=2,LightStrength=20,Phase=80,Strength=2)';
								this.style.backgroundRepeat  = 'Repeat'" 
						onmouseout="javascript:this.style.filter='';this.style.backgroundRepeat  = 'no-Repeat'"  
						
						<%
					if(strfiletype == "1")
					{
					%>
						style="BORDER-RIGHT: #ff6600 1px solid; BORDER-TOP: #ff6600 1px solid; Z-INDEX: 110;  
						BACKGROUND-IMAGE: url(FileType/<%=(myType=="dir")?(ComShare?"share.gif":"mydocuments.gif"):FileName2Pic(dr[0].ToString())%>); 
						background-repeat:no-repeat;
						BORDER-LEFT: #ff6600 1px solid; WIDTH: 44px; CURSOR: hand; BORDER-BOTTOM: #ff6600 1px solid; HEIGHT: 36px; 
						BACKGROUND-COLOR: transparent; POSITION: relative"
					<%}else{%>	
					    style="BORDER-RIGHT: #ff6600 1px solid; BORDER-TOP: #ff6600 1px solid; Z-INDEX: 110;  
						BACKGROUND-IMAGE: url(FileType/<%=(myType=="dir")?(ComShare?"share1.gif":"mydocuments1.gif"):FileName2Pic(dr[0].ToString())%>); 
						background-repeat:no-repeat;
						BORDER-LEFT: #ff6600 1px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: #ff6600 1px solid; HEIGHT: 18px; 
						BACKGROUND-COLOR: transparent; POSITION: relative"
					
						<%}%>
						>
					</div>
					<%
					if(strfiletype == "1")
					{
					%>
					<DIV id="div<%=i.ToString()%>c" title="" dataFormatAs="text" style="BORDER-RIGHT: gray 1px dotted; BORDER-TOP: gray 1px dotted; FONT-SIZE: 10pt; BORDER-LEFT: gray 1px dotted; WIDTH: 76px; BORDER-BOTTOM: gray 1px dotted; POSITION: relative"
										ms_positioning="GridLayout"><%=dr[0].ToString()%></DIV>
					<%}%>
				</TD>
				<%
					if(strfiletype == "2")
					{
				%>
				<td><%=dr[0].ToString()%></td>
				<td><%=dr[2].ToString()%></td>
				<td><%=dr[3].ToString()%></td>
				<td><%=dr[4].ToString()%></td>
				<%}%>
				<%
				    if(strfiletype == "1")
				    {
						if((float)i/7==(int)i/7)
						{
							Response.Write("</tr><tr>");
						}
					}
					else
					{
					    Response.Write("</tr><tr>");
					}
				}%>
				<td style="WIDTH: 0px"></td>
			</TR>
		</TABLE>
		<input id="Folder" style="Z-INDEX: 102; LEFT: 296px; POSITION: absolute; TOP: 480px" type="hidden"
			name="Folder">
		<form id="frmContent" name="frmContent" method="post" encType="multipart/form-data" runat="server">
			<asp:label id="lblSubDirectories" style="Z-INDEX: 120; LEFT: 240px; POSITION: absolute; TOP: 360px"
				runat="server" Height="32px" Width="216px" Visible="False">lblSubDirectories</asp:label>
			<input style="DISPLAY: none" type="input" name="action"> <input style="DISPLAY: none" type="input" name="actionParam">
			<input style="DISPLAY: none" type="input" name="Folder">
			<TABLE id="deletefile" cellPadding="0" align="center" style="DISPLAY: none" cellspacing="0">
				<TR>
					<TD>
						ɾ���ĵ���<asp:TextBox class="txtInput" id="Textbox1" runat="server" Width="356px"></asp:TextBox>
						<SCRIPT></SCRIPT>
						<INPUT id="delButton" type="image" onmouseover="javascript:this.src='images/btnOK_over.gif'"
							onclick="javascript:__doBeforeSubmit();" src="images/btnOK.gif" onmouseout="javascript:this.src='images/btnOK.gif'">
					</TD>
				</TR>
			</TABLE>
			<TABLE id="addfile" border="0" cellPadding="0" align="center" style="HEIGHT: 12px; DISPLAY:none "
				cellspacing="0">
				<TBODY>
					<TR>
						<TD vAlign="top" colSpan="2">
							<input type=button value="����" onclick="AddShare();" <%=SharePermission?"":"Disabled" %>>
							<input type="button" value="ɾ��" onclick="javascript:if(window.document.all['type'].value!='dir') { __doDeleteFile(window.document.all['foldername'].value)} else{ __doDeleteDir(window.document.all['foldername'].value) } ">
							<asp:Button id="Button1" runat="server" Text="ȡ������" onclick="Button1_Click"></asp:Button>
							<input type="text" name="del" id="del" style="WIDTH:0" width="0" height="0"> <input type="text" name="Com" id="Com" style="WIDTH:0" width="0" height="0">
							<input type="text" name="type" id="type" style="WIDTH:0" width="0" height="0"> <input type="text" name="txtRename" id="txtRename" style="WIDTH:0" width="0" height="0">
							<input type="text" name="foldername" id="foldername" style="WIDTH:0" width="0" height="0">
							<input type="text" name="FolderName" id="FolderName" style="WIDTH:0" width="0" height="0">
							<input type="text" name="name" id="name" style="WIDTH:0" width="0" height="0">
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" colSpan="2">�����ļ���<INPUT class="btnFile" id="fileUpload" style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 356px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 22px"
								type="file" size="56" name="fileUpload" runat="server"> <INPUT id="imgButton" onmouseover="javascript:this.src='images/btnOK_over.gif'" onclick="javascript:__doBeforeSubmit2();"
								onmouseout="javascript:this.src='images/btnOK.gif'" type="image" alt="�����ļ�" src="images/btnOK.gif" align="absMiddle" name="imgButton">
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" colSpan="2">�½�Ŀ¼��<asp:TextBox class="txtInput" id="txtNewDir" runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
							<INPUT id="imgButton1" onmouseover="javascript:this.src='images/btnOK_over.gif'" onclick="javascript:__doBeforeSubmit();"
								onmouseout="javascript:this.src='images/btnOK.gif'" type="image" alt="�½�Ŀ¼" src="images/btnOK.gif"
								align="absMiddle" name="imgButton">
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" colSpan="2">&nbsp;&nbsp;��������<asp:TextBox class="txtInput" id="txtReName" runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
							<INPUT id="imgButton2" onmouseover="javascript:this.src='images/btnOK_over.gif'" onclick="javascript:return __ReName()"
								onmouseout="javascript:this.src='images/btnOK.gif'" type="image" alt="�½�Ŀ¼" src="images/btnOK.gif"
								align="absMiddle" name="imgButton">
						</TD>
					</TR>
					<tr>
						<td><asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 328px; POSITION: absolute; TOP: 192px" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td>
							<span id="lblMessage" runat="server" MaintainState="false"></span>
						</td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		<script language="javascript">
			//�ļ����϶�----------------------------------------------------------------------------

		
		//SET_DHTML(CURSOR_HAND, RESIZABLE, SCROLL,"Div1a","div1c" <%//=objs%>);

		//dd.elements.fateba_se.addChild('ko5_st');
		<%//=addchild%>
		function openwin(filetype)
		{
		   var strfolder = document.getElementById('idfolder').value;
		   if(strfolder.length >0)
		   {
		     window.open('?filetype='+filetype+'&&folder=' + escape(strfolder),'_self')
		   }
		   else
		   {
		      window.open('?filetype='+filetype,'_self')
		   }
		 
		}
		</script>
		<script language="javascript">
		window.document.all["txtReName"].value="";
		window.document.all["txtNewDir"].value="";
		</script>
		<iframe id="frmDownload" name="frmDownload" src="" width="0" height="0"></iframe>
	</body>
</HTML>
