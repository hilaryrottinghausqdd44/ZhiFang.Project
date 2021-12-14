-- 郭海祥 2019-1-17 增加查询项和显示列配置   操作B_SearchUnit和B_ColumnsUnit两张表

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'10', N'样本号', N'SampleNo', NULL, N'60', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'17', N'条码号', N'SERIALNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SERIALNO'', fieldLabel: ''条码号'', labelWidth: 50, width: 150 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'18', N'小组类型', N'SECTIONNO', N'search', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:"SECTIONNO", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext
            {
               
                xtype: ''uxCheckTrigger'',
                emptyText: ''小组过滤'',
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''secname'',
                className: ''Shell.class.pgroup.section2'',
                listeners: {
                    check: function (p, record) {
                    var me=  this.ownerCt.ownerCt;
                    if (record == null) {
                    me.getItem("SECTIONNO").setValue("");
                    me.getItem("secname").setValue("");
                    return;
	                }
	               	me.getItem("SECTIONNO").setValue(record.get("SectionNo"));
	                me.getItem("secname").setValue(record.get("CName"));
	                p.close();
                    }
                }
            }')
GO 

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'19', N'检验类型', N'TESTTYPENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{
			    fieldLabel: ''检验类型'',
                type: ''other'', 
				xtype: ''combobox'',
                itemId: ''testType'',
                width: 130,
				labelWidth:60,
                displayField: ''text'', 
				valueField: ''value'',
				store:Ext.create(''Ext.data.Store'', {
	                    fields: [''text'', ''value''],
	                    data: [
                            { text: ''常规'', value: ''1'' },
                            { text: ''急诊'', value: ''2'' },
                            { text: ''质控'', value: ''3'' } 
	                    ]
	                }),
                /*alue: [''1''],*/
                listeners: {
                    change: function (m, v) {
                        var testTypeNo = this.ownerCt.ownerCt.getItem(''TestTypeNo'');
                        testTypeNo.setValue(v);                        
                    }
                }
            }searchAndNext
		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "TestTypeNo", name: ''TESTTYPENO'', width: 130,hidden:true }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'20', N'收费类型', N'CHARGENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{
			    fieldLabel: ''收费类型'',
                type: ''other'', 
				xtype: ''combobox'',
                itemId: ''charge'',
                width: 130,
				labelWidth:60,
                displayField: ''text'', 
				valueField: ''value'',
				store:Ext.create(''Ext.data.Store'', {
	                    fields: [''text'', ''value''],
	                    data: [
                            { text: ''国产'', value: ''1'' },
                            { text: ''进口'', value: ''2'' }                      
	                    ]
	                }),
                listeners: {
                    change: function (m, v) {
                        var cHARGENO = this.ownerCt.ownerCt.getItem(''CHARGENO'');
                        cHARGENO.setValue(v);                        
                    }
                }
            }searchAndNext
		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "CHARGENO", name: ''CHARGENO'', width: 130,hidden:true }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'21', N'样本类型', N'SAMPLETYPENO', N'search', N'55', N'150', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext
            {
                fieldLabel: '''',
                xtype: ''uxCheckTrigger'',
                emptyText: ''样本类型'',
                zIndex:1,
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''SampleTypeName'',
                className: ''Shell.class.sampletype.SampleType'',
                listeners: {
                    check: function (p, record) {
                        var item = "";
                        var clientName = "";
                        var me = this.ownerCt.ownerCt;
	                    if (record == null) {
                            item = me.getItem("SAMPLETYPENO");
                            clientName = me.getItem("SampleTypeName");
                            item.setValue("");
                            clientName.setValue("");
                            return;
                        }
                        item = me.getItem("SAMPLETYPENO");
                        clientName = me.getItem("SampleTypeName");
                        item.setValue(  record.get("SampleTypeNo") );
                        clientName.setValue(record.get("CName"));
                        p.close();
                    }
                }
            }')
GO

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext
            {
                fieldLabel: '''',
                xtype: ''uxCheckTrigger'',
                emptyText: ''就诊类型'',
                zIndex:1,
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''SickTypeNo'',
                className: ''Shell.class.dictionaries.sicktype.SickType'',
                listeners: {
                    check: function (p, record) {
                        var item = "";
                        var clientName = "";
                        var me = this.ownerCt.ownerCt;
	                    if (record == null) {
                            item = me.getItem("SICKTYPENAME");
                            clientName = me.getItem("SickTypeNo");
                            item.setValue("");
                            clientName.setValue("");
                            return;
                        }
						
                        item = me.getItem("SICKTYPENAME");
                        clientName = me.getItem("SickTypeNo");
                        item.setValue(  record.get("CName") );
                        clientName.setValue(record.get("CName"));
                        p.close();
                    }
                }
            }' where SID = 2
GO 

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: "DeptNo", name: ''DeptNo'', width: 130, hidden: true } searchAndNext
            {
                fieldLabel: '''',
                xtype: ''uxCheckTrigger'',
                emptyText: ''科室过滤'',
                zIndex:1,
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''ClienteleName'',
                className: ''Shell.class.dictionaries.dept.CheckGrid'',
                listeners: {
                    check: function (p, record) {
                        var item = "";
                        var clientName = "";
                        var me = this.ownerCt.ownerCt;
						if (record == null) {
                            item = me.getItem("DeptNo");
                            clientName = me.getItem("ClienteleName");
                            item.setValue("");
                            clientName.setValue("");
                            return;
                        }
						
                        item = me.getItem("DeptNo");
                        clientName = me.getItem("ClienteleName");
                        item.setValue("(" + record.get("DeptNo") + ")");
                        clientName.setValue(record.get("CName"));
                        p.close();
                    }
                }
            }'	where SID = 1
GO

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "checkerNO", name: ''Checker'', width: 130, hidden: true } searchAndNext
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
	                className: ''Shell.class.dictionaries.higquery.checker'',
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
	            }'	where SID = 16
GO

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "districtNameNo", name: ''DistrictName'', width: 130, hidden: true } searchAndNext
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
	                className: ''Shell.class.dictionaries.higquery.district'',
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
	            }'	where SID = 10
GO

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: "operatorNO", name: ''Operator'', width: 130, hidden: true } searchAndNext
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
	                className: ''Shell.class.dictionaries.higquery.operator'',
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
	            }'	where SID = 15
GO

update [dbo].[B_SearchUnit] set JsCode = ' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:"SECTIONNO", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext
            {
               
                xtype: ''uxCheckTrigger'',
                emptyText: ''小组过滤'',
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''secname'',
                className: ''Shell.class.dictionaries.pgroup.section2'',
                listeners: {
                    check: function (p, record) {
                    var me=  this.ownerCt.ownerCt;
                    if (record == null) {
                    me.getItem("SECTIONNO").setValue("");
                    me.getItem("secname").setValue("");
                    return;
	                }
	               	me.getItem("SECTIONNO").setValue(record.get("SectionNo"));
	                me.getItem("secname").setValue(record.get("CName"));
	                p.close();
                    }
                }
            }'	where SID = 18
GO

update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext
            {
                fieldLabel: '''',
                xtype: ''uxCheckTrigger'',
                emptyText: ''样本类型'',
                zIndex:1,
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''SampleTypeName'',
                className: ''Shell.class.dictionaries.sampletype.SampleType'',
                listeners: {
                    check: function (p, record) {
                        var item = "";
                        var clientName = "";
                        var me = this.ownerCt.ownerCt;
	                    if (record == null) {
                            item = me.getItem("SAMPLETYPENO");
                            clientName = me.getItem("SampleTypeName");
                            item.setValue("");
                            clientName.setValue("");
                            return;
                        }
                        item = me.getItem("SAMPLETYPENO");
                        clientName = me.getItem("SampleTypeName");
                        item.setValue(  record.get("SampleTypeNo") );
                        clientName.setValue(record.get("CName"));
                        p.close();
                    }
                }
            }'	where SID = 21
GO

