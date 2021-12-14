<%
'########################################以下为数据库操作函数

'数据库插入(使用sql语句)
'用于把内容插入数据库
function InsertSql(tablename,trname,valuesname)	
		'///执行数据库操作并返回成功信息或者执行错误信息提示	
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

'数据库更新（使用sql语句）
'用于更新数据库中的数据内容
function UpdateSql(tablename,trname,valuesname,wherename)
	
		'执行数据库操作并返回成功信息或者执行错误提示
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
		
'数据库删除（使用sql语句）
'用于删除某条记录
function DeleteSql(tablename,wherename)
		dim dsql
		dsql="delete from ["&tablename&"]"
		if id<>"" then
			dsql=dsql&" where "&wherename&""
		end if
		conn.execute (dsql)
end function

'数据库删除（使用sql语句用于id）
'用于删除某条记录
function DeleteSql_id(tablename,id)
		dim dsql
		dsql="delete from ["&tablename&"]"
		if id<>"" then
			dsql=dsql&" where id="&id&""
		end if
		conn.execute (dsql)
end function
%>