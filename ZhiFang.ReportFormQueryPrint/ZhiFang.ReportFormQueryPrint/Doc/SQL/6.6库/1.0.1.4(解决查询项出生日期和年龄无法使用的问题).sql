--2019-5-6 郭海祥
--解决查询项出生日期和年龄无法使用的问题  更新表数据B_SearchUnit



update [dbo].[B_SearchUnit]  set JsCode='{type: ''other'', 
xtype: ''datefield'',
format:''Y-m-d'',
itemId: ''selectBirthday'', 
name: ''selectBirthday'', 
fieldLabel: ''出生日期'', 
labelWidth: 60, 
width: 150,
listeners: {
                    change: function (m, v) {
                        var Birthday = this.ownerCt.ownerCt.getItem(''Birthday'');
						var selectBirthday = this.ownerCt.ownerCt.getItem(''selectBirthday'');
						var m = parseInt(v.getMonth())+1;
						if(m<=9){
							m = "0"+m;
						}
						var d = v.getDate();
						if(d<=9){
							d = "0"+d;
						}
                        Birthday.setValue(v.getFullYear()+"-"+m+"-"+d);
                    }
                } }
searchAndNext
{ type: ''search'',mark: ''='', xtype: ''textfield'',format:''Y-m-d'', itemId: ''Birthday'', name: ''Birthday'', labelWidth: 0, width: 190,hidden: true }
' where SID = '23'
GO


update [dbo].[B_SearchUnit]  set JsCode='{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }' where SID = '32'
GO


INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'34', N'卡号', N'ZDY3', N'search', N'50', N'210', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='',itemId: ''ZDY3'', name: ''ZDY3'', fieldLabel: ''卡号'', labelWidth: 50, width: 210,selectOnFocus:true }')
GO



