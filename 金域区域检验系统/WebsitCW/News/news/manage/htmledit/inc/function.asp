<%
' ********************************************
' 以下为常用函数
' ********************************************
' ============================================
' 错误信息返回
' 参数:str 返回的错误信息
' ============================================
function ShowError(str)
	Response.Write "<script language=javascript>alert('" & str & "\n\n系统将自动返回前一页面...');history.back();</script>"
	'Response.End
End function

' ============================================
' 成功信息返回
' 参数:str 返回的错误信息
'      goUrl 返回的页面
' ============================================
function ShowSuccess(str,goUrl)
	Response.Write "<script language=javascript>alert('" & str & "');location.href='"&goUrl&"';</script>"
	'Response.End
End function
' ============================================
' 格式化时间(显示)
' 参数：n_Flag
'	y:年
'	m:月
'   d:日
'   h:时
'   mi:分
'   s:秒
' ============================================
Function FormatTime(s_Time, n_FormatText)
	Dim y, m, d, h, mi, s
	FormatTime = ""
	If IsDate(s_Time) = False Then Exit Function
	y = cstr(year(s_Time))
	m = cstr(month(s_Time))
	d = cstr(day(s_Time))
	h = cstr(hour(s_Time))
	mi= cstr(minute(s_Time))
	s = cstr(second(s_Time))
	If len(m) = 1 Then m = "0" & m
	d = cstr(day(s_Time))
	If len(d) = 1 Then d = "0" & d
	h = cstr(hour(s_Time))
	If len(h) = 1 Then h = "0" & h
	mi = cstr(minute(s_Time))
	If len(mi) = 1 Then mi = "0" & mi
	s = cstr(second(s_Time))
	If len(s) = 1 Then s = "0" & s
	n_FormatText=replace(n_FormatText,"y",y)
	n_FormatText=replace(n_FormatText,"m",m)
	n_FormatText=replace(n_FormatText,"d",d)
	n_FormatText=replace(n_FormatText,"h",h)
	n_FormatText=replace(n_FormatText,"mi",mi)
	n_FormatText=replace(n_FormatText,"s",s)
	FormatTime=n_FormatText
End Function

' ============================================
' 把字符串进行HTML解码,替换server.htmlencode
' 去除Html格式，用于显示输出
' ============================================
Function disHtml(str)
	Dim sTemp
	sTemp = trim(str)
	disHtml = ""
	If IsNull(sTemp) = True Then
		Exit Function
	End If
	sTemp = Replace(sTemp, "&", "&amp;")
	sTemp = Replace(sTemp, "<", "&lt;")
	sTemp = Replace(sTemp, ">", "&gt;")
	sTemp = Replace(sTemp, Chr(34), "")
	sTemp = Replace(sTemp, Chr(10), "<br>")
	disHtml = sTemp
End Function

' ============================================
' 去除Html格式，用于从数据库中取出值填入输入框时
' 注意：value="?"这边一定要用双引号
' ============================================
Function InHTML(str)
	Dim sTemp
	sTemp = trim(str)
	InHTML = ""
	If IsNull(sTemp) = True Then
		Exit Function
	End If
	sTemp = Replace(sTemp, "&", "&amp;")
	sTemp = Replace(sTemp, "<", "&lt;")
	sTemp = Replace(sTemp, ">", "&gt;")
	sTemp = Replace(sTemp, Chr(34), "&quot;")
	InHTML = sTemp
End Function

' ============================================
' 用于插入数据库的时候替换单引号"'"
' ============================================
Function InDb(str)
	Dim sTemp
	sTemp = trim(str)
	sTemp=replace(sTemp,"'","''")
	InDb=sTemp
End Function

' ============================================
' 检测上页是否从本站提交
' 返回:True,False
' ============================================
Function IsSelfRefer()
	Dim sHttp_Referer, sServer_Name
	sHttp_Referer = CStr(Request.ServerVariables("HTTP_REFERER"))
	sServer_Name = CStr(Request.ServerVariables("SERVER_NAME"))
	If Mid(sHttp_Referer, 8, Len(sServer_Name)) = sServer_Name Then
		IsSelfRefer = True
	Else
		IsSelfRefer = False
	End If
End Function

' ============================================
' 得到安全字符串,在查询中使用
' ============================================
Function Get_SafeStr(str)
	Get_SafeStr = Replace(Replace(Replace(Trim(str), "'", ""), Chr(34), ""), ";", "")
End Function

' ============================================
' 取实际字符长度
' ============================================
Function GetLen(str)
	Dim l, t, c, i
	l = Len(str)
	t = l
	For i = 1 To l
		c = Asc(Mid(str, i, 1))
		If c < 0 Then c = c + 65536
		If c > 255 Then t = t + 1
	Next
	GetLen = t
End Function

'# ----------------------------------------------------------------------------
'# 函数：LeftStr(text,length)
'# 描述：代替left函数按照中文两个字符,字母一个字符的方式截长
'# 参数： text-字符串,length-要截取的长度
'# 返回：
'# 作者：雷の龙
'# 日期：2004
'#-----------------------------------------------------------------------------
Function LeftStr(text,length)  
	Dim t
	t=""
	Dim mt
	Dim l
	l=0
	Dim c
	For i= 1 To Len(text)
		mt=mid(text,i,1)
		c=Asc(mt)
		If c<0 Then c=c+65536
		If c > 255 Then
			l=l+2
		Else 
			l=l+1
		End If
		If l<=CLng(length) Then
			t=t&mt
		else
			exit for
		End If
	Next
	LeftStr=t
End Function
' ============================================
' 判断是否安全字符串,在注册登录等特殊字段中使用
' ============================================
Function IsSafeStr(str)
	Dim s_BadStr, n, i
	s_BadStr = "' 　&<>?%,;:()`~!@#$^*{}[]|\/+-=" & Chr(34) & Chr(9) & Chr(32)
	n = Len(s_BadStr)
	IsSafeStr = True
	For i = 1 To n
		If Instr(str, Mid(s_BadStr, i, 1)) > 0 Then
			IsSafeStr = False
			Exit Function
		End If
	Next
End Function

' ============================================
' 并经过处理返回数组形式(去除空行）
' 一般用于返回文本区的内容。
' ============================================

function GetSplit(text)
	dim textsplit
	text=replace(text,chr(13),"")
	textsplit=split(text,chr(10))
	text=""
	for i=0 to ubound(textsplit)
		if not isnull(textsplit(i)) and textsplit(i)<>"" then
			text=text&trim(textsplit(i))&"$$"
		end if
	next
	text=left(text,len(text)-2)
	textsplit=split(text,"$$")
	GetSplit=textsplit
end function

'# ----------------------------------------------------------------------------
'# 函数：IsPost
'# 描述：判断页面提交方式是不是post的
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004
'#-----------------------------------------------------------------------------
Function IsPost()
	If LCase(Request.ServerVariables("request_method"))="post" Then
		IsPost=true
	Else
		IsPost=false
	End If
End Function
'# ----------------------------------------------------------------------------
'# 函数：showpage
'# 描述：分页
'# 参数： -
'# 返回：
'# 作者：雷の龙
'# 日期：2004
'#-----------------------------------------------------------------------------
function showpage(totalnumber,pagecount,CurrentPage,filename)
	If instr(filename,"?")=0 Then
	filename=filename&"?"
	else
	filename=filename&"&"
	End If
	
	Response.Write "<table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' width='100%'><tr>"
	Response.Write "<td align='left'>当前第<font color=ff6600><b>"&CurrentPage&"</b></font>页&nbsp;&nbsp;共有<font color=ff6600><B>"&pagecount&"</B></font>页&nbsp;&nbsp;共有<font color=ff6600><B>"&totalnumber&"</B></font>条信息</td>"
	Response.Write "<td align='right'>"
	If CurrentPage>1 Then
		Response.Write "【<a href='"&filename&"page=1'>最前页</a>】【<a href='"&filename&"page="&CurrentPage-1&"'>上一页</a>】"
	End If
	If CurrentPage<pagecount Then
		Response.Write "【<a href='"&filename&"page="&CurrentPage+1&"'>下一页</a>】【<a href='"&filename&"page="&pagecount&"'>最后页</a>】"
	End If
	response.write "&nbsp;&nbsp;<select name='jumpMenu' onChange='location.href=jumpMenu.value;'>"
	For i= 1 To pagecount
		response.write "<option value="&filename&"page="&i&" "
		If i=CurrentPage Then
			response.write "selected"
		End If
		response.write ">第"&i&"页</option>"
	Next
	response.write "</select>"
	Response.Write "</td>"
	Response.Write "</tr></table>"
end	function

%>