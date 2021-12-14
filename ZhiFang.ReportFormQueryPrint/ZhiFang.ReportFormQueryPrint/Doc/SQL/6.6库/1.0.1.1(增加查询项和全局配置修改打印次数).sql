---郭海祥2019-3-18 插入数据到B_SearchUnit、B_Parameter，增加表wardType，修改表B_ColumnsUnit中打印次数


INSERT INTO [dbo].[B_Parameter]  VALUES (N'27', N'页面公共配置', N'allPageType', N'config', N'isCaseSensitive', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO
INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'22', N'检验者', N'Technician',NULL, N'70', N'200', NULL, NULL, N'combobox', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "technicianNO", name: ''Technician'', width: 130, hidden: true } searchAndNext
				{
	                fieldLabel: ''检验者:'',
	                xtype: ''uxCheckTrigger'',
	                emptyText: ''检验者'',
	                zIndex:1,
	                width: 200,
	                labelSeparator: '''',
	                labelWidth: 70,
	                labelAlign: ''right'',
	                itemId: ''technician'',
	                className: ''Shell.class.dictionaries.higquery.checker'',
	                listeners: {
	                    check: function (p, record) {
						    var item = "";
	                        var Technician = "";
							var container = this.ownerCt;
							var toolbar = container.ownerCt;
							if (record == null) {
							    item = toolbar.getItem("technicianNO");
	                            Technician = container.getComponent("technician");
								item.setValue("");
	                            Technician.setValue("");
	                            return;
	                       }
						    item = toolbar.getItem("technicianNO");
	                        Technician = container.getComponent("technician");
							item.setValue(record.get("CName"));
	                        Technician.setValue(record.get("CName"));
	                        p.close();
	                    }
	                }
	            }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'23', N'出生日期', N'Birthday', N'other', N'60', N'170', NULL, NULL, N'textfield', N'=', NULL, N'{type: ''other'', xtype: ''datefield'',format:''Y-m-d'', mark: ''='', name: ''Birthday'', fieldLabel: ''出生日期'', labelWidth: 60, width: 150 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'24', N'性别', N'GenderName', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''GenderName'', fieldLabel: ''性别'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'25', N'病床', N'Bed', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''病床'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'26', N'备注', N'FormMemo', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''FormMemo'', fieldLabel: ''备注'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'27', N'大备注', N'FormComment', N'search', N'50', N'150', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''FormComment'', fieldLabel: ''大备注'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'28', N'检验项目', N'ItemName', N'search', N'60', N'150', NULL, NULL, N'textfield', N'like', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ItemName'', fieldLabel: ''检验项目'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'29', N'细菌标注类型', N'ZDY4', N'search', N'80', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ZDY4'', fieldLabel: ''细菌标注类型'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'30', N'病房', N'WardNo', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "WardNos", name: ''WardNo'', width: 130, hidden: true } searchAndNext
				{
	                fieldLabel: ''病房:'',
	                xtype: ''uxCheckTrigger'',
	                emptyText: ''病房'',
	                zIndex:1,
	                width: 200,
	                labelSeparator: '''',
	                labelWidth: 60,
	                labelAlign: ''right'',
	                itemId: ''wardName'',
	                className: ''Shell.class.dictionaries.higquery.wardNo'',
	                listeners: {
	                    check: function (p, record) {
						    var item = "";
	                        var wardNames = "";
							var container = this.ownerCt;
							var toolbar = container.ownerCt;
							if (record == null) {
							    item = toolbar.getItem("WardNos");
	                            wardNames = container.getComponent("wardName");
								item.setValue("");
	                            wardNames.setValue("");
	                            return;
	                       }
						    item = toolbar.getItem("WardNos");
	                        wardNames = container.getComponent("wardName");
							item.setValue(record.get("WardNo"));
	                        wardNames.setValue(record.get("CName"));
	                        p.close();
	                    }
	                }
	               }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'32', N'年龄', N'Age', N'other', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''other'', xtype: ''numberfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }')
GO

update B_ColumnsUnit set Render = '{renderer:function (v, meta, record) {
	                var imgName = (v && v >= me.maxPrintTimes) ? "unprint" : "print",
		                tootip = "已经打印<b style=''color:red;''> " + v + " </b>次",
	                    value = v ? "  <b>" + v + "</b>" : "";

	                meta.tdAttr = ''data-qtip="'' + tootip + ''"'';
	                
	                var result = '''';
	                //if(v >= 0){
	                    //result = "<img src=''" + Shell.util.Path.uiPath + "/ReportPrint/images/" + imgName + ".png''/>" + v;
	                //}
	                if(v > 0){
                            meta.style="background-color:red";
	                    result = v;
	                }else{
				result = v;
                        }
	                return result;
	            }}' where ColID = 7



