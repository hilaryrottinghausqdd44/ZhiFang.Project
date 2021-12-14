---2018/12/3 郭海祥
---为了可以让开发人员控制显示列,在B_ColumnsUnit和B_ColumnsSetting表中增加了IsUse是否启用字段
---解决审核时间和审核日期显示异常的问题   添加两条数据审核时间和审核日期
---解决字段冲突问题，报告时间和审核日期字段冲突   修改Render数据，将报告日期IsUse='false'

ALTER TABLE [dbo].[B_ColumnsUnit] ADD IsUse bit
GO

update [dbo].[B_ColumnsUnit] set IsUse = 'True'
GO 

ALTER TABLE [dbo].[B_ColumnsSetting] ADD IsUse bit
GO

update [dbo].[B_ColumnsSetting] set IsUse = 'True'
GO 

update [dbo].[B_ColumnsSetting] set IsUse = 'False' where ColID = '2'
GO 

update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) {
	//显示审核时间
				var Cdate = Shell.util.Date.toString(record.get("CHECKDATE"), true);
				var Ctime = Shell.util.Date.toString(record.get("CHECKTIME"), false);
				var value = '''';
				if(Cdate !=null){
					value = Cdate;
				}
				if(Ctime !=null){
				  var arry = Ctime.split('' '');
				  if(arry!=null && arry.length >1){
					 value +='' ''+ arry[1];
					}
				}
	//var value = v ? Shell.util.Date.toString(v, true) : "";
	//meta.tdAttr = ''data-qtip="<b>'' + value + ''</b>"'';
	return value;
}}',IsUse='FALSE' where COlID = 2
GO 

update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (value, meta, record) {
	            if (value) meta.tdAttr = ''data-qtip="<b>'' + value + ''</b>"'';
	            return value;
	        }}' where COlID = 4
GO 

update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) {
	 var value = record.get("RECEIVEDATE").split('' '')[0];
	 return value;
}}' where COlID = 6
GO 

update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record) {
	                var imgName = (v && v >= me.maxPrintTimes) ? "unprint" : "print",
		                tootip = "已经打印<b style=''color:red;''> " + v + " </b>次",
	                    value = v ? "  <b>" + v + "</b>" : "";

	                meta.tdAttr = ''data-qtip="'' + tootip + ''"'';
	                
	                //var result = '''';
	                //if(v >= 0){
	                    //result = "<img src=''" + Shell.util.Path.uiPath + "/ReportPrint/images/" + imgName + ".png''/>" + v;
	                //}
	                
	                return v;
	            }}' where COlID = 7
GO 

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'8', N'审核日期', N'CHECKDATE', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {
	//显示审核日期
				var Cdate = record.get("CHECKDATE");
				var value = '''';
				if(Cdate !=null){
 				  var arry = Cdate.substr(0,10);
				  if(arry!=null && arry.length >1){
					 value =arry;
					}
				}
				
	return value;
}}', N'1')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'9', N'审核时间', N'CHECKTIME', NULL, N'110', NULL, NULL, N'
{renderer:function (v, meta, record, index) {
	//显示审核时间
				var Ctime = record.get("CHECKTIME");
				var value = '''';
				if(Ctime !=null){
				  var arry = Ctime.substring(Ctime.length-9);
				  if(arry!=null && arry.length >1){
					 value =arry;
					}
				}
	return value;
}}', N'1')
GO
