<%
'########################################����Ϊ���ݿ��������

'���ݿ����(ʹ��sql���)
'���ڰ����ݲ������ݿ�
function InsertSql(tablename,trname,valuesname)	
		'///ִ�����ݿ���������سɹ���Ϣ����ִ�д�����Ϣ��ʾ	
		dim isql
		dim getid
		set rs=conn.execute("select max(id) from "&tablename)
		if isnull(rs(0)) then
			getid=1
		else
			getid=rs(0)+1
		end if
		rs.close
		set rs=nothing
		isql="insert into "&tablename&" (id,"&trname&") values ("&getid&","&valuesname&")"
		'response.write isql
		'response.end
		conn.execute(isql)
		InsertSql=getid
end function

'���ݿ���£�ʹ��sql��䣩
'���ڸ������ݿ��е���������
function UpdateSql(tablename,trname,valuesname,wherename)
	
		'ִ�����ݿ���������سɹ���Ϣ����ִ�д�����ʾ
		dim usql
		usql="update "&tablename&" set "
		dim arrtrname,arrvaluesname
		arrtrname=split(trname,",")
		arrvaluesname=split(valuesname,",")
		for i=0 to ubound(arrtrname)
			if arrtrname(i)<>"" then
				usql=usql&""&arrtrname(i)&"="&arrvaluesname(i)&","
			end if
		next
		usql=left(usql,len(usql)-1)
		if wherename<>"" then
			usql=usql&" where "&wherename&""
		end if

		conn.execute (usql)
end function
		
'���ݿ�ɾ����ʹ��sql��䣩
'����ɾ��ĳ����¼
function DeleteSql(tablename,wherename)
		dim dsql
		dsql="delete from ["&tablename&"]"
		if id<>"" then
			dsql=dsql&" where "&wherename&""
		end if
		conn.execute (dsql)
end function

'���ݿ�ɾ����ʹ��sql�������id��
'����ɾ��ĳ����¼
function DeleteSql_id(tablename,id)
		dim dsql
		dsql="delete from ["&tablename&"]"
		if id<>"" then
			dsql=dsql&" where id="&id&""
		end if
		conn.execute (dsql)
end function
%>