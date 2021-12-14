<%
' ********************************************
' ����Ϊ���ú���
' ********************************************
' ============================================
' ������Ϣ����
' ����:str ���صĴ�����Ϣ
' ============================================
function ShowError(str)
	Response.Write "<script language=javascript>alert('" & str & "\n\nϵͳ���Զ�����ǰһҳ��...');history.back();</script>"
	'Response.End
End function

' ============================================
' �ɹ���Ϣ����
' ����:str ���صĴ�����Ϣ
'      goUrl ���ص�ҳ��
' ============================================
function ShowSuccess(str,goUrl)
	Response.Write "<script language=javascript>alert('" & str & "');location.href='"&goUrl&"';</script>"
	'Response.End
End function
' ============================================
' ��ʽ��ʱ��(��ʾ)
' ������n_Flag
'	y:��
'	m:��
'   d:��
'   h:ʱ
'   mi:��
'   s:��
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
' ���ַ�������HTML����,�滻server.htmlencode
' ȥ��Html��ʽ��������ʾ���
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
' ȥ��Html��ʽ�����ڴ����ݿ���ȡ��ֵ���������ʱ
' ע�⣺value="?"���һ��Ҫ��˫����
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
' ���ڲ������ݿ��ʱ���滻������"'"
' ============================================
Function InDb(str)
	Dim sTemp
	sTemp = trim(str)
	sTemp=replace(sTemp,"'","''")
	InDb=sTemp
End Function

' ============================================
' �����ҳ�Ƿ�ӱ�վ�ύ
' ����:True,False
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
' �õ���ȫ�ַ���,�ڲ�ѯ��ʹ��
' ============================================
Function Get_SafeStr(str)
	Get_SafeStr = Replace(Replace(Replace(Trim(str), "'", ""), Chr(34), ""), ";", "")
End Function

' ============================================
' ȡʵ���ַ�����
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
'# ������LeftStr(text,length)
'# ����������left�����������������ַ�,��ĸһ���ַ��ķ�ʽ�س�
'# ������ text-�ַ���,length-Ҫ��ȡ�ĳ���
'# ���أ�
'# ���ߣ��פ���
'# ���ڣ�2004
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
' �ж��Ƿ�ȫ�ַ���,��ע���¼�������ֶ���ʹ��
' ============================================
Function IsSafeStr(str)
	Dim s_BadStr, n, i
	s_BadStr = "' ��&<>?%,;:()`~!@#$^*{}[]|\/+-=" & Chr(34) & Chr(9) & Chr(32)
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
' ������������������ʽ(ȥ�����У�
' һ�����ڷ����ı��������ݡ�
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
'# ������IsPost
'# �������ж�ҳ���ύ��ʽ�ǲ���post��
'# ������ -
'# ���أ�
'# ���ߣ��פ���
'# ���ڣ�2004
'#-----------------------------------------------------------------------------
Function IsPost()
	If LCase(Request.ServerVariables("request_method"))="post" Then
		IsPost=true
	Else
		IsPost=false
	End If
End Function
'# ----------------------------------------------------------------------------
'# ������showpage
'# ��������ҳ
'# ������ -
'# ���أ�
'# ���ߣ��פ���
'# ���ڣ�2004
'#-----------------------------------------------------------------------------
function showpage(totalnumber,pagecount,CurrentPage,filename)
	If instr(filename,"?")=0 Then
	filename=filename&"?"
	else
	filename=filename&"&"
	End If
	
	Response.Write "<table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' width='100%'><tr>"
	Response.Write "<td align='left'>��ǰ��<font color=ff6600><b>"&CurrentPage&"</b></font>ҳ&nbsp;&nbsp;����<font color=ff6600><B>"&pagecount&"</B></font>ҳ&nbsp;&nbsp;����<font color=ff6600><B>"&totalnumber&"</B></font>����Ϣ</td>"
	Response.Write "<td align='right'>"
	If CurrentPage>1 Then
		Response.Write "��<a href='"&filename&"page=1'>��ǰҳ</a>����<a href='"&filename&"page="&CurrentPage-1&"'>��һҳ</a>��"
	End If
	If CurrentPage<pagecount Then
		Response.Write "��<a href='"&filename&"page="&CurrentPage+1&"'>��һҳ</a>����<a href='"&filename&"page="&pagecount&"'>���ҳ</a>��"
	End If
	response.write "&nbsp;&nbsp;<select name='jumpMenu' onChange='location.href=jumpMenu.value;'>"
	For i= 1 To pagecount
		response.write "<option value="&filename&"page="&i&" "
		If i=CurrentPage Then
			response.write "selected"
		End If
		response.write ">��"&i&"ҳ</option>"
	Next
	response.write "</select>"
	Response.Write "</td>"
	Response.Write "</tr></table>"
end	function

%>