
<!--#include file="upload.inc" -->
<%
Class LBUpload
	Public Id
	Public FileName
	Public DateAndTime
	Public FileSize
	Public Extension
	Public Category
	Public Description

	Public Version
	Public ActionConn

	Private limExtension
	Private limFileSize
	Private Up
	Private Upid

	'构造函数
	Private Sub Class_Initialize
		Upid=1
		Reset()
		UpObject()
	end sub
	
	'解析函数
	Private Sub Class_Terminate 
		Reset()
		Set Up=Nothing
	end sub
	
	'重置属性函数
	private Sub ReSet()
		Version="LBNOW CREATE @ 2004-4-5 AND LAST UPDATE @ 2004-04-07"
		FileName=""
		DateAndTime=Now()
		FileSize=0
		Extension=""
		Category=""
		limExtension="jpg,gif,doc,png,txt,swf,mdb,ppt,dat,bmp,rtf,htm,html,exe,zip,war,rar,xls,pdf"
		limFileSize=0
	End Sub
'# ----------------------------------------------------------------------------
'# 函数：upobject
'# 描述：建立上传对象
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Private Function UpObject()
		Select Case Upid
		  Case 1
			Set Up=new upload_5xsoft
		  Case Else
		End Select
	End Function
'# ----------------------------------------------------------------------------
'# 函数：PageHtmlForm
'# 描述：显示页面上的表单
'# 参数：
'# 返回：页面输出Html代码
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Public Function PageHtmlForm()
		%>
		<BR>
		<div align="center">
		<form method="post" enctype="multipart/form-data" action="?action=<%=request.querystring("action")%>&fieldname=<%=request.querystring("fieldname")%>&formname=<%=request.querystring("formname")%>">
			<table class="table" border=1 width=100% border=0 cellpadding=0 cellspacing=0>
			<tr>
				<td class="lefttd" align="center" width="100">文件:</td>
				<td class="righttd">&nbsp;&nbsp;<input type="file" size=25 class="file" name="filesource" value=""></td>
			</tr>
			<tr style="display:none">
				<td class="lefttd" align="center">请选择上传文件分类</td>
				<td class="righttd">&nbsp;&nbsp;
				<select class="select" name="category" size="">
				<option value=""></option>
				</select></td>
			</tr>
			<tr style="display:none">
				<td class="lefttd" align="center">填写文件描述，方便管理</td>
				<td class="righttd">&nbsp;&nbsp;<textarea name="description" class="textarea" rows="8" cols="40"><%=Description%></textarea></td>
			</tr>
			</table>
			<BR>
			<table width=100% border=0 cellpadding=0 cellspacing=0>
			<tr>
				<td align="center" height="25"><input type="submit" class="button" value="确认上传文件"></td>
			</tr>
			</table>
		</form></div>
		<%
	End Function
'# ----------------------------------------------------------------------------
'# 函数：pageuplist
'# 描述：显示已经上传的文件列表,以及管理选项
'# 参数： psize-每页显示多少内容
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Public Function PageUpList(psize) 
		%>
		<div align="center">
		<table width=100% border=1 class="table" cellpadding=0 cellspacing=0>
		<tr class="headtr" align="center">
			<td class="td">文件名</td>
			<td width="100" class="td">大小</td>
			<td width="120" class="td">上传日期</td>
			<td width="100" class="td">管理选项</td>
		</tr>
		<%
		Dim rs
		Dim count,pagecount,nowpage,a
		Set rs=Server.CreateObject("adodb.recordset")
		sql="select * from upfiles order by id desc"
		rs.open sql,conn,3,1
		totalcount=rs.recordcount
		'***********************************分页
		count=psize
		if count<=0 then
			count=pize
		end if
		if not rs.eof then
			rs.pagesize=count
			pagecount=rs.pagecount
			if request("page")="" then
				nowpage=1
			else
				nowpage=int(request("page"))
			end if
			if nowpage>=rs.pagecount then
				nowpage=rs.pagecount
			elseif nowpage<=1 then
				nowpage=1
			end if
			rs.absolutepage=nowpage
		else
			pagecount=1
			nowpage=1
		end if
		a=1
		do while not rs.eof and a<=count
		%>
			<tr height="20">
				<td class="td">&nbsp;<%=FileName2Pic(rs("extension"))%>&nbsp;<A HREF="upfiles/<%=rs("filename")%>" target=_blank title="<%=rs("description")%>"><font color=#000000><%=rs("filename")%></font></A></td>
				<td class="td" align="center" width="100"><%=FormatNumber(rs("filesize"),2,-1)%> &nbsp;K</td>
				<td class="td" align="center" width="120"><%=rs("dateandtime")%></td>
				<td class="td" align="center" width="100">
					<table width=100% border=0 cellpadding=0 cellspacing=0>
					<tr>
						
						<td class="td" align="center"><a title="" href="?action=delete&id=<%=rs("id")%>">删除</a></td>
					</tr>
					</table>
				</td>
			</tr>
		<%
			a=a+1
			rs.movenext
		loop 
		%>
		</table>
		<BR>
		<table width=100% class="table1" border=0 cellpadding=0 cellspacing=0>
		<tr>
			<td><%showpage totalcount,pagecount,nowpage,"up.asp"%></td>
		</tr>
		</table>
		</div>
		<%
	End Function
'# ----------------------------------------------------------------------------
'# 函数：pageshowurl
'# 描述：显示上传结果连接地址的页面
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Private Function PageShowUrl(url) 
		%>
		<form method="post" action="" name="formresult">
		<table width=100% align="center" border=0 cellpadding=0 cellspacing=0>
		<tr>
			<td align="center">以下文本框的内容就是你刚才上传的文件的访问地址.</td>
		</tr>
		<tr>
		  <td height="20"></td>
		</tr>
		<TR><TD align="center"><input type="text" name="url" size="50" class="text" value="<%=url%>"></td></tr>
		<tr>
		  <td height="25" align="center"><a title="" href="#" onclick="window.close();">关闭窗口</a></td>
		</tr>
		</table>
		</form>
		<SCRIPT LANGUAGE="JavaScript">
		<!--
		formresult.url.focus();
		formresult.url.select();
		//-->
		</SCRIPT>
		<%
	End Function
'# ----------------------------------------------------------------------------
'# 函数：GetUrl
'# 描述：取得文件的URl地址
'# 参数：fm-要取得url的文件
'# 返回：取得的URl，文本型
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Private Function GetUrl(fm)
		Dim addr0,addr1,url
		addr0=request.servervariables("server_name")
		if request.servervariables("server_port")<>"80" then addr0=addr0 & ":" & request.servervariables("server_port")
		addr1=request.servervariables("url")
		addr1=replace(addr1,"up.asp","upfiles/"&fm)
		url="http://"&addr0&addr1
		GetUrl=url
	End Function
'# ----------------------------------------------------------------------------
'# 函数：Add
'# 描述：上传添加文件
'# 参数： id-使用的上传组件  1-化境编程的无组件上传
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Public Function Add()
		Dim file
		Select Case Upid
		  Case 1
			Set file=Up.file("filesource")

			'取得表单的内容已经文件的信息
			FileName=LCase(file.filename)
			DateAndTime=Now()
			FileSize=file.filesize
			'大小转换成以K为单位
			'FileSize= formatnumber(FileSize/1024,2)
			FileSize= cstr(FileSize/1024)
			

			'判断有效性
			If FileSize=0 And FileName="" Then
				Call ShowError("请选择要上传的文件")
				Exit Function
			End If
			If limFileSize>0 And CInt(FileSize)>LimFileSize Then
				Call ShowError("文件大小超过限制,请上传不大于"&limFileSize&"K的文件")
				Exit Function
			End If

			Extension=split(FileName,".")(ubound(split(FileName,".")))
			'判断有效性
			If InStr(limExtension,Extension)=0 Then
				Call ShowError("该文件类型已经禁止上传")
				Exit Function
			End If

			Category=up.Form("category")
			Description=up.Form("description")



			'保存文件
			myFile=FileName
			FileName=Year(dateandtime)&month(dateandtime)&day(dateandtime)&hour(dateandtime)&minute(dateandtime)&second(dateandtime)&"."&Extension
			myFile=myFile + "变为" + FileName
			file.saveas server.mappath("upfiles/"&FileName)
			Set file=Nothing
		  Case Else
		End Select

		'记录数据库信息
		'Dim rs
		'Set rs=Conn.execute("select max(id) from upfiles")
		'Dim idd
		'If IsNull(rs(0)) Or rs(0)="" Then
		'	idd=1
		'Else
		'	idd=rs(0)+1
		'End If
		'ActionConn.execute("insert into upfiles (id,filename,dateandtime,filesize,extension,category,description) values ("&Idd&",'"&FileName&"','"&Dateandtime&"',"&FileSize&",'"&extension&"','"&category&"','"&description&"')")

		Response.Write "<script>window.returnValue ='"&GetUrl(FileName)&"';window.close();</script>"
		Response.Write myFile
		Response.Write "<br><img src=""upfiles/" + FileName + """>"
		Response.Write "<br><a href=""up.asp?action=add"">继续上传</a><br>jpg,gif,doc,png,txt,swf,mdb,ppt,dat,bmp,rtf,htm,html,exe,zip,war,rar,xls,pdf"
	End Function


'# ----------------------------------------------------------------------------
'# 函数：eWebList
'# 描述：eWeb文件上传页面
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004
'#-----------------------------------------------------------------------------
Public Function eWebList() 
	%>
		<HTML>
		<HEAD>
		<TITLE>文件上传</TITLE>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<style type="text/css">
		body, a, table, div, span, td, th, input, select{font:9pt;font-family: "宋体", Verdana, Arial, Helvetica, sans-serif;}
		body {padding:0px;margin:0px}
		</style>

		<script language="JavaScript" src="dialog/dialog.js"></script>

		</head>
		<body oncontextmenu="return false" onselectstart="return false" ondragstart="return false"  bgcolor=menu>

		<form action="?action=ewebsave&type=<%=sType%>&style=<%=sStyleName%>" method=post name=myform enctype="multipart/form-data">
		<input type=file name=uploadfile size=1 style="width:100%">
		<input type="submit" class="button" value="dddd">
		</form>

		<script language=javascript>
		var sAllowExt = "<%=limExtension%>";
		// 是否有效的扩展名
		function IsExt(url, opt){
			var sTemp;
			var b=false;
			var s=opt.toUpperCase().split(",");
			for (var i=0;i<s.length ;i++ ){
				sTemp=url.substr(url.length-s[i].length-1);
				sTemp=sTemp.toUpperCase();
				s[i]="."+s[i];
				if (s[i]==sTemp){
					b=true;
					break;
				}
			}
			return b;
		}
		// 检测上传表单
		function CheckUploadForm() {
			if (!IsExt(document.myform.uploadfile.value,sAllowExt)){
				parent.UploadError("提示：\n\n请选择一个有效的文件，\n支持的格式有（"+sAllowExt+"）！");
				return false;
			}
			return true
		}
		// 提交事件加入检测表单
		var oForm = document.myform ;
		oForm.attachEvent("onsubmit", CheckUploadForm) ;
		if (! oForm.submitUpload) oForm.submitUpload = new Array() ;
		oForm.submitUpload[oForm.submitUpload.length] = CheckUploadForm ;
		if (! oForm.originalSubmit) {
			oForm.originalSubmit = oForm.submit ;
			oForm.submit = function() {
				if (this.submitUpload) {
					for (var i = 0 ; i < this.submitUpload.length ; i++) {
						this.submitUpload[i]() ;
					}
				}
				this.originalSubmit() ;
			}
		}

		// 上传表单已装入完成
		try {
			parent.UploadLoaded();
		}
		catch(e){
		}

		</script>

		</body>
		</html>
	<%
End Function
'# ----------------------------------------------------------------------------
'# 函数：eWebAdd
'# 描述：上传添加文件
'# 参数： id-使用的上传组件  1-化境编程的无组件上传
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Public Function eWebAdd()
		Dim file
		Select Case Upid
		  Case 1
			Set file=Up.file("uploadfile")

			'取得表单的内容已经文件的信息
			FileName=LCase(file.filename)
			DateAndTime=Now()
			FileSize=file.filesize
			'大小转换成以K为单位
			FileSize=formatnumber(FileSize/1024,2)

			'判断有效性
			If FileSize=0 And FileName="" Then
				Call OutScript("parent.UploadError('请选择有效的上传文件！')")
				Exit Function
			End If
			If limFileSize>0 And CInt(FileSize)>LimFileSize Then
				Call OutScript("parent.UploadError('文件大小超过限制,请上传不大于"&limFileSize&"K的文件')")
				Exit Function
			End If

			Extension=split(FileName,".")(ubound(split(FileName,".")))
			'判断有效性
			If InStr(limExtension,Extension)=0 Then
				Call OutScript("parent.UploadError('该文件类型已经禁止上传')")
				Exit Function
			End If

			Category="新闻内容图片"
			Description=up.Form("description")



			'保存文件
			FileName=Year(dateandtime)&month(dateandtime)&day(dateandtime)&hour(dateandtime)&minute(dateandtime)&second(dateandtime)&"."&Extension
			file.saveas server.mappath("../upload/upfiles/"&FileName)
			Set file=Nothing
		  Case Else
		End Select

		'记录数据库信息
		'Dim rs
		'Set rs=Conn.execute("select max(id) from upfiles")
		'Dim idd
		'If IsNull(rs(0)) Or rs(0)="" Then
		'	idd=1
		'Else
		'	idd=rs(0)+1
		'End If
		'ActionConn.execute("insert into upfiles (id,filename,dateandtime,filesize,extension,category,description) values ("&Idd&",'"&FileName&"','"&Dateandtime&"',"&FileSize&",'"&extension&"','"&category&"','"&description&"')")

		Call OutScript("parent.UploadSaved('" & ewebGeturl(FileName) & "')")
	End Function
'# ----------------------------------------------------------------------------
'# 函数：ewebGetUrl
'# 描述：取得文件的URl地址
'# 参数：fm-要取得url的文件
'# 返回：取得的URl，文本型
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Private Function ewebGetUrl(fm)
		Dim addr0,addr1,url
		addr0=request.servervariables("server_name")
		if request.servervariables("server_port")<>"80" then addr0=addr0 & ":" & request.servervariables("server_port")
		addr1=request.servervariables("url")
		addr1=replace(addr1,"htmledit/upload.asp","upload/upfiles/"&fm)
		url="http://"&addr0&addr1
		ewebGetUrl=url
	End Function

'# ----------------------------------------------------------------------------
'# 函数：delete
'# 描述：删除一个文件
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004-04-07
'#-----------------------------------------------------------------------------
	Public Function Delete() 
		Id=Request.QueryString("id")
		'读取文件名称
		Dim rs
		Set rs=ActionConn.execute("select * from upfiles where id="&Id)
		Dim filename
		If Not rs.eof Then
			filename=rs("filename")
		Else 
			filename="error"
		End If
		rs.Close
		Set rs = Nothing
		'从数据库中删除文件信息
		ActionConn.execute("Delete from upfiles where id="&Id)
		'使用fso组件删除文件
		Set fso=Server.CreateObject("Scripting.FileSystemObject")
		'如果出错则结束
		If err Then
			Call Showsuccess("服务器不支持Fso组件,数据已经从数据库中删除,要删除文件请手动删除.","?action=list")
			exit function
		End If
		'如果文件存在就删除
		If fso.fileExists("upfiles/"&filename) Then
			fso.DeleteFile "upfiles/"&filename
		End If
		Set fso=Nothing
		'返回信息
		If err Then
			Call ShowError("删除文件失败.")
		Else
			Call ShowSuccess("文件删除成功.","?action=list")
		End If
	End Function
'# ----------------------------------------------------------------------------
'# 函数：
'# 描述：
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004
'#-----------------------------------------------------------------------------
Function FileName2Pic(sExt)
	Select Case LCase(sExt)
	case "txt"
		spicname = "txt.gif"
	case "chm", "hlp"
		spicname = "hlp.gif"
	case "doc"
		spicname = "doc.gif"
	case "pdf"
		spicname = "pdf.gif"
	case "mdb"
		spicname = "mdb.gif"
	case "gif", "jpg", "png", "bmp"
		spicname = "pic.gif"
	case "asp", "jsp", "js", "php", "php3", "aspx"
		spicname = "code.gif"
	case "htm", "html", "shtml"
		spicname = "htm.gif"
	case "zip", "rar"
		spicname = "zip.gif"
	case "exe"
		spicname = "exe.gif"
	case "avi", "mpg", "mpeg", "asf"
		spicname = "mp.gif"
	case "ra", "rm"
		spicname = "rm.gif"
	case "mid", "wav", "mp3", "midi"
		spicname = "audio.gif"
	case "xls"
		spicname = "xls.gif"
	case "ppt", "pps"
		spicname = "ppt.gif"
	case else
		spicname = "unknow.gif"
	end select
	FileName2Pic = "<img border=0 src='../htmledit/sysimage/file/" & sPicName & "'>"
End Function
End Class
%>