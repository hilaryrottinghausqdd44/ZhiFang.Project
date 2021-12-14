<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Documents.Share" Codebehind="Share.aspx.cs" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="VBScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
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
' 函数,iif函数，补空格
' 李子佳,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function AddSpace(Byval str,iCount)
	If len(str)>iCount Then
		AddSpace=left(str,iCount-2) + ".."
	Else
		AddSpace=str + Replace(space(iCount-len(str))," ","&nbsp;")
	End IF
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' 子程序,初始化全部环境
' 李子佳,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub init()
   
   ' Set up global data.
   TabStop = Chr(9)
   NewLine = Chr(10)
   
   Set FSO = CreateObject("Scripting.FileSystemObject")

End Sub
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' 子程序,列出所有的驱动器
' 李子佳,2004-09-15
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
		window.status="不能读取" + Drive.DriveLetter + "驱动器，请检查原因"
	  End If
   Next
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' 子程序,列出指定目录
' 李子佳,2004-09-15
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function GetFolders(FolderString)
	window.status="正在打开文件列表，请稍候...."
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
			"上一级目录...</div>"
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
	window.status="完成"
	html.click 
	selectDrives.blur
	
	'如何载获双击事件，不要让它传到下一级窗口??
End Function

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' 子程序,列出指定目录内的全部文件
' 李子佳,2004-09-15
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
			'If File.Attributes(32) Then 'Archive 文件能读
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
' 子程序,退回到根目录
' 李子佳,2004-09-15
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
		msgbox "不能读取 [" + selectDrives.options(selectDrives.selectedIndex).Text + ":] 驱动器，请检查原因"
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
		msgbox "不能读取 [" + selectDrives.options(selectDrives.selectedIndex).Text + ":] 驱动器，请检查原因"
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
		msgbox "本机设置不容许执行客户端脚本，不能查看本地文件" + vbcrlf + _
			"请启用脚本执行许可，或把浏览器的安全性降低:" + vbcrlf + vbcrlf + _
			"* 浏览器 [Internet选项]" + vbcrlf + _
			"* [安全]->[自定义级别]->[ActiveX控件与插件]" + vbcrlf + _
			"* [对没有标记为安全的ActiveX控件进行初始化和脚本运行]" + vbcrlf + _
			"* [启用]->>最后确定" ,vbcritical,"文件上传设置"
	End if
	<%}%>
End Sub

-->
		</script-->

	<script id=clientEventHandlersJS language=javascript>
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
		document.all("lblMessage").innerHTML = "<br><b>请指定上传的文件名！　红色框的部分必须填写！</b>";
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
		document.all("lblMessage").innerHTML = "<br><b>文件夹名不能为空，红色框的部分必须填写！</b>";
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
		alert("此目录不能重命名！");
		return false;
	}
	if(window.document.all["foldername"].value=='')
	{
		alert("请选定文件或文件夹！")
		return false;
	}
	if(window.document.all['Com'].value=='True')
	{
		alert("文件夹或者子文件夹已共享，请先取消共享！");
		return false;
	}
	else
	{
		if (window.confirm("确认要更改文件名？"))
		{
			//frmContent.type.value=window.document.all["type"].value;		
			//frmContent.foldername.value=window.document.all["foldername"].value;
			//frmContent.txtRename.value=window.document.all["txtReName"].value;
			
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
		alert("此目录不能删除！");
		return false;
	}
	if(window.document.all["foldername"].value=="")
	{
		alert("请选择文件或者文件夹！");
		return false;
	}
	if (window.confirm("确认要删除文件 \""+strFile.substring(strFile.lastIndexOf("\\")+1,strFile.length)+"\"？"))
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
		alert("此目录不能删除！");
		return false;
	}
	if(window.document.all["foldername"].value=="")
	{
		alert("请选择文件或者文件夹！");
		return false;
	}
	if (window.confirm("确认要删除目录 \""+strDir.substring(strDir.lastIndexOf("\\")+1,strDir.length)+"\"？"))
	{
		frmContent.action.value = "DeleteDir";
		frmContent.actionParam.value = strDir;
		frmContent.submit();
	}
}

//-->
</script>
<link rel="stylesheet" type="text/css" href="../explorer.css">
</HEAD>
	<body MS_POSITIONING="GridLayout">
	<script type="text/javascript" src="../Includes/Tools/scripts/wz_dragdrop.js"></script>
	<!--IMG height="157" alt="" src="../Tools/walterzorn/images/dragdrop/fateba_se.jpg" width="200" name="fateba_se">
	<br>
	<IMG height="60" alt="" src="../Tools/walterzorn/images/dragdrop/ko5_st.jpg" width="130" name="ko5_st"-->
	
	
	<!--A class="code" onclick="if(window.dd &amp;&amp; dd.elements &amp;&amp; dd.elements.fateba_se.children.length > 0) dd.elements.fateba_se.attachChild(dd.elements.ko5_st);return false;"
										href="javascript:void(0);">dd.elements.fateba_se.attachChild(dd.elements.ko5_st);</A>
	<A class="code" onclick="if(window.dd &amp;&amp; dd.elements &amp;&amp; dd.elements.fateba_se.children.length > 0) dd.elements.fateba_se.detachChild('ko5_st');return false;"
										href="javascript:void(0);">dd.elements.fateba_se.detachChild('ko5_st');</A-->
										
		<TABLE id="TableTilte" style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; Z-INDEX: 101; BORDER-LEFT-COLOR: #0066ff; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: #0066ff; WIDTH: 752px; BORDER-TOP-COLOR: #0066ff; HEIGHT: 22px; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: #0066ff"
			cellSpacing=0 cellPadding=0 width="248" align="center" border="0">
			<tr>
				<td>
				<input id="Folder" style="Z-INDEX: 102; LEFT: 296px; POSITION: absolute; TOP: 480px" type="hidden"
				name="Folder">
				<h4>位置: <font color=#cc6633><%=ShareEmpName%></font><font color=#7d7d7c size=3 face="宋体"></font></h4>
				
				</td>
			</tr>
		</table>
		<TABLE id="Table1" style="BORDER-RIGHT: #ffe7d9 1px solid; BORDER-TOP: #ffe7d9 1px solid; Z-INDEX: 101; BORDER-LEFT: #ffe7d9 1px solid; WIDTH: 752px; BORDER-BOTTOM: #ffe7d9 1px solid; POSITION: relative; HEIGHT: 22px"
			cellSpacing="4" cellPadding="10" width="248" align="center" border="0">
			<TR>
				<% 
				int i=0;
				int n=0;
				string secID="";
				string thrID="";
				foreach(DataRow dr in dt.Rows)
				{
					
					i+=1;
					%>
					
				<TD style="WIDTH: 90px;HEIGHT: 88px" align="center"><FONT face="宋体"></FONT>
					<%
					
					string myType=dr[1].ToString();
					string folder="";
					if(x<2)
					{
						folder=firDir[n];
						//secID=firDirID[n];
					}
					else
					{
						if(x==2)
						{
							folder=firDir[n];
							//secID=SaveID;
							//thrID=firDirID[n];
						}
						else
						{
							if (myType=="dir")
							{
								if(dr[0].ToString()=="...")
								{	if(filePath.LastIndexOf("\\")>-1)
									{
										folder=filePath.Substring(0,filePath.LastIndexOf("\\"));
										//secID=SaveID;
										//thrID=AbilityID;
									}
									else
									{
										folder="\\";
										//secID=SaveID;
										//thrID=AbilityID;
									}
										
								}
								else
								{
									folder=filePath+"\\" +dr[0].ToString();
									//secID=SaveID;
									//thrID=AbilityID;
								}
							}
							else
							{
								folder=filePath;
								//secID=SaveID;
								//thrID=AbilityID;
							}
						}
					}	
					if(folder!=null)
						folder=folder.Replace("\\","\\\\");
					n++;
					%>
					
					<div id="Div<%=i.ToString()%>a" title="<%=dr[0].ToString()%>"
						<%
						if(myType=="dir")
						{
						%> 
							
							ondblclick="OpenDir('<%=dr[0].ToString()%>','<%=folder%>','<%=x%>','<%=secID%>','<%=SendShareName%>','<%=SendShareNamelen%>','<%=thrID%>');" 
						<%if(dr[0].ToString()!="..."){if(deleteAbility){%>
							onkeydown="javascript:if(window.event.keyCode==46) __doDeleteDir('<%=folder%>')"
						
						<%}}%>
						onclick="SelectDocument(this,'<%=folder%>','<%=myType%>','<%=ComShareDir%>','<%=dr[0].ToString()%>')"
						<%}
						else
						{
						//ondblclick="window.open('../browse/downloadfile.aspx?file=<%=folder.Replace("\\\\","/")%%%%%&>/<%=dr[0].ToString()%%%%%%&>')" 
						%>
							<%if(!unExist){%>
							ondblclick="javascript:window.open('download.aspx?file=' + escape('<%=folder%>\\<%=dr[0].ToString()%>'),'frmDownload','width=50px,height=50px left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 )" 
							
							<%if(uploadAbility){
							%>
							onfocus="javascript:showMouseOver('Div<%=i.ToString()%>a')"　tabindex="<%=i.ToString()%>"
							
							onclick="SelectDocument(this,'<%=folder%>\\<%=dr[0].ToString()%>','<%=myType%>','<%=ComShareDir%>')"
						<%}if(deleteAbility){%>
						onkeydown="javascript:if(window.event.keyCode==46) __doDeleteFile('<%=folder%>\\<%=dr[0].ToString()%>')"
						<%}}}%>
						onmouseover="javascript:this.style.filter='progid:DXImageTransform.Microsoft.Wave(freq=2,LightStrength=20,Phase=80,Strength=2)';
								this.style.backgroundRepeat  = 'Repeat'" 
						onmouseout="javascript:this.style.filter='';this.style.backgroundRepeat  = 'no-Repeat'"  
						
						
						style="BORDER-RIGHT: #ff6600 1px solid; BORDER-TOP: #ff6600 1px solid; Z-INDEX: 110; 
						BACKGROUND-IMAGE: url(FileType/<%=(myType=="dir")?"mydocuments.gif":FileName2Pic(dr[0].ToString())%>); 
						background-repeat:no-repeat;
						BORDER-LEFT: #ff6600 1px solid; WIDTH: 44px; CURSOR: hand; BORDER-BOTTOM: #ff6600 1px solid; HEIGHT: 36px; 
						BACKGROUND-COLOR: transparent; POSITION: relative">
					</div>
					<DIV id="div<%=i.ToString()%>c" title="" dataFormatAs="text" style="BORDER-RIGHT: gray 1px dotted; BORDER-TOP: gray 1px dotted; FONT-SIZE: 10pt; BORDER-LEFT: gray 1px dotted; WIDTH: 76px; BORDER-BOTTOM: gray 1px dotted; POSITION: relative"
										ms_positioning="GridLayout"><%=dr[0].ToString()%></DIV>
				</TD>
				<%
					if((float)i/7==(int)i/7)
					//if(i>2)
						Response.Write("</tr><tr>");
				
				}%>
				<td style="WIDTH: 0px"></td>
			</TR>
		</TABLE>
		<script language="javascript">
					function SelectDocument(doc,foldername,type,comsharedir,name)
					{
						
						window.document.all["del"].value=foldername;
						window.document.all["Com"].value=comsharedir;
						window.document.all["type"].value=type;
						window.document.all["foldername"].value=foldername;
						window.document.all["name"].value=name;
						if (SelDoc != '')
						{
							document.all[SelDoc].style.backgroundColor = '';
							document.all[SelDoc].style.color = '';
						}
						SelDoc=doc.id;
						document.all[SelDoc].style.backgroundColor = 'skyblue';
						document.all[SelDoc].style.color = 'white';
					}
					
					function OpenDir(Type,folder,q,secID,sharename,SendShareNamelen,thrID)
					{
						if(Type=="...")
						{	
							q--;
						}
						else
						{
							q++;
						}	
						
						//location.href='Share.aspx?folder='+folder+'&x='+q+'&secID='+secID+'&SendShareName='+sharename+'&SendShareNamelen='+SendShareNamelen+'&thrID='+thrID
						window.open('?folder='+escape(folder),'_self')
					}
					</script>
					<input id="Folder" style="Z-INDEX: 102; LEFT: 296px; POSITION: absolute; TOP: 480px" type="hidden"
				name="Folder">
		<form id="frmContent" name="frmContent" method="post" encType="multipart/form-data" runat="server">
				<asp:label id="lblSubDirectories" style="Z-INDEX: 120; LEFT: 240px; POSITION: absolute; TOP: 360px"
					runat="server" Height="32px" Width="216px" Visible="False">lblSubDirectories</asp:label>
				<input style="DISPLAY: none" type="input" name="action"> <input style="DISPLAY: none" type="input" name="actionParam">
				<input style="DISPLAY: none" type="input" name="Folder">
				<TABLE id="deletefile" cellPadding=0 align=center style="DISPLAY: none" cellspacing=0>
					<TR>
						<TD>	
						删除文档：<asp:TextBox class=txtInput id="Textbox1" runat="server" Width="356px"></asp:TextBox>
								<SCRIPT></SCRIPT>
								<INPUT id=delButton type="image" onmouseover="javascript:this.src='images/btnOK_over.gif'" 
								onclick=javascript:__doBeforeSubmit(); src="images/btnOK.gif" onmouseout="javascript:this.src='images/btnOK.gif'">
						</TD>
					</TR>
				</TABLE>
				<TABLE id="addfile" border=0 cellPadding=0 align=center style="HEIGHT: 12px; DISPLAY: " cellspacing=0>
					<TBODY>
					<%if(x>=3&&unExist==false)
					{%>
						<TR>

							<TD vAlign=top colSpan=2>
								<%if(deleteAbility){%>
									<input type=button id="delBut" value="删除" <%if(deleteAbility){%>onclick="javascript:if(window.document.all['type'].value!='dir') { __doDeleteFile(window.document.all['foldername'].value)} else{ __doDeleteDir(window.document.all['foldername'].value) } "<%}%>>
									<%}
								else
								{
									if(SharedDirectoryButtonsVisible=="true"){%>
										&nbsp;没有删除权限
										<input type=button value="删除" onclick="" style="COLOR: gray">
									<%}
								}%>
							</TD>
						</TR>
						
						<TR><%if(uploadAbility){%>
							<TD vAlign=top colSpan=2>上载文件：<INPUT class=btnFile id=fileUpload 
								style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 356px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 22px" type="file" size=56 name=fileUpload 
								runat="server"> <INPUT id=imgButton 
								onmouseover="javascript:this.src='images/btnOK_over.gif'" 
								<%if(uploadAbility){%>onclick=javascript:__doBeforeSubmit2();<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%> onmouseout="javascript:this.src='images/btnOK.gif'" 
								type=image alt=上载文件 src="images/btnOK.gif" align=absMiddle name=imgButton> 
							</td>
							<%}
							  else{
							  if(SharedDirectoryButtonsVisible=="true"){%>
							  <TD vAlign=top colSpan=2>上载文件：<INPUT disabled  class=btnFile id="File1" 
								style="BACKGROUND-COLOR: lightgrey; BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 356px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 22px" type="file" size=56 name=fileUpload 
								runat="server"> <INPUT id=imgButton 
								
								<%if(uploadAbility){%>onclick=javascript:__doBeforeSubmit2();<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%>  
								type=image alt=上载文件 src="images/btnOK_over1.gif" align=absMiddle name=imgButton> 
							</td>
							 <%}}%>
						</tr>
						<TR>
						<%if(createchildAbility){%>
							<TD vAlign=top colSpan=2>新建目录：<asp:TextBox class=txtInput id=txtNewDir runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
								<INPUT id=imgButton1 onmouseover="javascript:this.src='images/btnOK_over.gif'" 
								<%if(createchildAbility){%>onclick=javascript:__doBeforeSubmit();<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%> onmouseout="javascript:this.src='images/btnOK.gif'" 
								type=image alt=新建目录 src="images/btnOK.gif" align=absMiddle name=imgButton> 
							</TD>
							<%}else
							{
							if(SharedDirectoryButtonsVisible=="true"){%>
							<TD vAlign=top colSpan=2>新建目录：<asp:TextBox Enabled=False BackColor=lightgray class=txtInput id="Textbox2" runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
								<INPUT id=imgButton1 
								<%if(createchildAbility){%>onclick=javascript:__doBeforeSubmit();<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%> 
								type=image alt=新建目录 src="images/btnOK_over1.gif" align=absMiddle name=imgButton> 
							</TD>
							<%}}%>
						</TR>
						<TR>
						<%if(renameAbility){%>
							<TD vAlign=top colSpan=2>&nbsp;&nbsp;重命名：<asp:TextBox class=txtInput id="txtReName" runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
								<INPUT id=imgButton2 onmouseover="javascript:this.src='images/btnOK_over.gif'" 
								<%if(renameAbility){%>onclick="javascript:return __ReName()"<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%> onmouseout="javascript:this.src='images/btnOK.gif'" 
								type=image alt=重命名 src="images/btnOK.gif" align=absMiddle name=imgButton> 
							</TD>
							<%}
							else
							{
							if(SharedDirectoryButtonsVisible=="true")
							{%>
							<TD vAlign=top colSpan=2>&nbsp;&nbsp;重命名：<asp:TextBox Enabled=False BackColor=lightgray class=txtInput id="Textbox3" runat="server" Width="356px" BorderWidth="1" BorderColor="#cccccc"></asp:TextBox>
								<INPUT id=imgButton2  
								<%if(renameAbility){%>onclick="javascript:return __ReName()"<%}else{%>onclick="javascript:alert('您没有权限');return false" <%}%> 
								type=image alt=重命名 src="images/btnOK_over1.gif" align=absMiddle name=imgButton> 
							</TD>
							<%}}%>
						</TR>
						<%}%>
						<tr>
							<td>
							<span id="lblMessage" runat="server" MaintainState="false"></span>
							<input type="text" name="del" id="del" style="WIDTH:0" width=0 height=0>
								<input type="text" name="Com" id="Com" style="WIDTH:0" width=0 height=0>
								<input type="text" name="type" id="type" style="WIDTH:0" width=0 height=0>
								<input type="text" name="foldername" id="foldername" style="WIDTH:0" width=0 height=0>
								
								<input type="text" name="name" id="name" style="WIDTH:0" width=0 height=0>
								<input type="text" name="txtRename" id="txtRename" style="WIDTH:0" width=0 height=0>
								
								<input type="text" name="FolderName" id="FolderName" style="WIDTH:0" width=0 height=0>
							</td>
						</tr>
					</tbody>
				</table>
				<input id="filename" name="filename" style="WIDTH:0" type="text" >
				<script language="javascript">
					window.document.all['filename'].value='<%=fileName%>';
					
				</script>
		</form>
		<!--TABLE id="locallist" border=0 cellPadding=0 align=center style="HEIGHT: 44px; WIDTH: 252px;" cellspacing=5>
			<TR>
				<TD> 
					<INPUT id="browse" style="Z-INDEX: 101; " type="button"
						value="定位">
				</td>
				<td>
					<SELECT id="selectDrives" style="Z-INDEX: 103; WIDTH: 64px; ">
					</SELECT>
				</td>
				<td>
					<DIV style="DISPLAY: inline; Z-INDEX: 104; WIDTH: 108px; HEIGHT: 24px"
						ms_positioning="FlowLayout">本地文件列表:</DIV>
				</td>
				<td>
					<DIV id="filepath" style="Z-INDEX: 105; WIDTH: 424px; HEIGHT: 24px"
						ms_positioning="GridLayout">我的电脑</DIV>
				</td>

			</tr>
			<tr>
				<td colspan=4>
					<DIV id="html" style="BORDER-RIGHT: #ff6633 1px solid; PADDING-RIGHT: 12px; BORDER-TOP: #ff6633 1px solid; PADDING-LEFT: 12px; Z-INDEX: 102; PADDING-BOTTOM: 12px; BORDER-LEFT: #ff6633 1px solid; WIDTH: 752px; CLIP: rect(auto auto auto auto); PADDING-TOP: 12px; BORDER-BOTTOM: #ff6633 1px solid;  HEIGHT: 104px"
						ms_positioning="GridLayout">这里将列出文件夹的内容</DIV>
				</td>
			</tr>
		</table-->
		<%
		string objs="";
		string addchild="";
		for(int b=1;b<=i;b++)
		{
			objs=objs + ",\"Div" + b + "a\",\"div" + b + "c\"";
			addchild += "dd.elements.Div" + b + "a.addChild('div" + b + "c');\n";
		}%>
		
	<script language=javascript>
			//文件夹拖动----------------------------------------------------------------------------

		
		SET_DHTML(CURSOR_HAND, RESIZABLE, SCROLL,"Div1a","div1c" <%=objs%>);

		//dd.elements.fateba_se.addChild('ko5_st');
		<%=addchild%>
		
	</script>		
	<script language="javascript">
	if(window.document.all["txtReName"]!=null&&window.document.all["txtReName"]!="")
		{	window.document.all["txtReName"].value="";
			window.document.all["txtNewDir"].value="";		
		}
		
	</script>
	<script language="javascript">
		var SelDoc = '';
			
		//function SelectDocument(doc)
		//{
		//	if (SelDoc != '')
		//	{
		//		document.all[SelDoc].style.backgroundColor = '';
		//		document.all[SelDoc].style.color = '';
		//	}
		//	SelDoc=doc.id;
		//	document.all[SelDoc].style.backgroundColor = 'skyblue';
		//	document.all[SelDoc].style.color = 'white';
		//}
		
	</script>
	<iframe id="frmDownload" name="frmDownload" src="" width="0" height="0"></iframe>
	</body>
</HTML>
