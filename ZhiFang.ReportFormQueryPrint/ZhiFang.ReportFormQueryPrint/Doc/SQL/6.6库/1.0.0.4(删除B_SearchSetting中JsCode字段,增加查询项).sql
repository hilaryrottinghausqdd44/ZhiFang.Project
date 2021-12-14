---2018-11-27 郭海祥 修改6.6库B_Parameter,B_SearchSetting,B_SearchUnit三张表
---解决问题：增加高级查询
---增加高级查询是否启用选项，B_Parameter增加一条高级查询全局配置数据
---B_SearchSetting表增加SearchType字段 用来区分普通查询和高级查询
---B_SearchUnit表增加查询项数据
---删除B_SearchSetting表中JsCode字段; 解决修改B_SearchUnit中JsCode数据改变是需要在后台设置删除查询项重新添加

INSERT INTO [dbo].[B_Parameter]  VALUES (N'25', N'页面公共配置', N'allPageType', N'config', N'isSeniorSetting', N'false', NULL, N'bool', N'2018-10-16 15:20:00.000', N'0', NULL, NULL)
GO

ALTER TABLE [dbo].[B_SearchSetting] ADD SearchType int
GO

ALTER TABLE B_SearchSetting DROP COLUMN JsCode

UPDATE [dbo].[B_SearchSetting] SET SearchType=1
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'10', N'病区', N'DistrictName', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "districtNameNo", name: ''DistrictName'', width: 130, hidden: true } searchAndNext
				{
	                fieldLabel: '' 病区:'',
	                xtype: ''uxCheckTrigger'',
	                emptyText: ''病区'',
	                zIndex:1,
	                width: 200,
	                labelSeparator: '''',
	                labelWidth: 70,
	                labelAlign: ''right'',
	                itemId: ''districtName'',
	                className: ''Shell.class.higquery.district'',
	                listeners: {
	                    check: function (p, record) {
	                        var item = "";
	                        var DistrictName = "";
							var container = this.ownerCt;
							var toolbar = container.ownerCt;
							
							if (record == null) {  
								item = toolbar.getItem("districtNameNo");								
	                            DistrictName = container.getComponent("districtName");
								item.setValue("");
	                            DistrictName.setValue("");
	                            return;
	                       }
	                        item = toolbar.getItem("districtNameNo");
	                        DistrictName = container.getComponent("districtName");
							item.setValue(record.get("CName"));
	                        DistrictName.setValue(record.get("CName"));
	                        p.close();
	                    }
	                }
	            }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'14', N'临床诊断', N'ZDY7', N'search', N'70', N'200', NULL, NULL, N'textfield', NULL, NULL, N'{
	                fieldLabel: '' 临床诊断:'',
	                xtype: ''textfield'',
	                emptyText: ''临床诊断'',
	                width: 200,
	                labelWidth: 70,
	                labelAlign: ''right'',
	                itemId: ''zdy7'',	
					name: ''zdy7''					
            	}')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'15', N'录入者', N'Operator', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "operatorNO", name: ''Operator'', width: 130, hidden: true } searchAndNext
				{
	                fieldLabel: '' 录入者:'',
	                xtype: ''uxCheckTrigger'',
	                emptyText: ''录入者'',
	                zIndex:1,
	                width: 200,
	                labelSeparator: '''',
	                labelWidth: 70,
	                labelAlign: ''right'',
	                itemId: ''operator'',
	                className: ''Shell.class.higquery.operator'',
	                listeners: {
	                    check: function (p, record) {
							var item = "";
	                        var Operator = "";
							var container = this.ownerCt;
							var toolbar = container.ownerCt;
							if (record == null) {
								item = toolbar.getItem("operatorNO");
	                            Operator = container.getComponent("operator");
								item.setValue("");
	                            Operator.setValue("");
	                            return;
	                       }
						    item = toolbar.getItem("operatorNO");
	                        Operator = container.getComponent("operator");
							item.setValue(record.get("CName"));
	                        Operator.setValue(record.get("CName"));
	                        p.close();
	                    }
	                }
	            }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'16', N'审核者', N'Checker', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "checkerNO", name: ''Checker'', width: 130, hidden: true } searchAndNext
				{
	                fieldLabel: '' 审核者:'',
	                xtype: ''uxCheckTrigger'',
	                emptyText: ''审核者'',
	                zIndex:1,
	                width: 200,
	                labelSeparator: '''',
	                labelWidth: 70,
	                labelAlign: ''right'',
	                itemId: ''checker'',
	                className: ''Shell.class.higquery.checker'',
	                listeners: {
	                    check: function (p, record) {
						    var item = "";
	                        var Checker = "";
							var container = this.ownerCt;
							var toolbar = container.ownerCt;
							if (record == null) {
							    item = toolbar.getItem("checkerNO");
	                            Checker = container.getComponent("checker");
								item.setValue("");
	                            Checker.setValue("");
	                            return;
	                       }
						    item = toolbar.getItem("checkerNO");
	                        Checker = container.getComponent("checker");
							item.setValue(record.get("CName"));
	                        Checker.setValue(record.get("CName"));
	                        p.close();
	                    }
	                }
	            }')
GO




