---2018/10/16 郭海祥
---抽离查询和显示列配置
---创建B_ColumnsSetting,B_ColumnsUnit,B_Parameter,B_SearchSetting,B_SearchUnit五个表
---给每个表添加数据
---特注：如果已有当前几个表就不必运行此脚本


-- ----------------------------
-- Table structure for B_ColumnsSetting
-- ----------------------------

CREATE TABLE [dbo].[B_ColumnsSetting] (
  [OrderFlag] bit  NULL,
  [OrderDesc] int  NULL,
  [OrderMode] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CTID] bigint  NOT NULL,
  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Width] int  NULL,
  [OrderNo] int  NULL,
  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DataAddTime] datetime  NULL,
  [DataUpdateTime] datetime  NULL,
  [IsShow] bit  NULL,
  [ColID] bigint  NULL,
  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Render] text COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[B_ColumnsSetting] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据加入时间',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsSetting',
'COLUMN', N'DataAddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据更新时间',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsSetting',
'COLUMN', N'DataUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 设置列表显示模板',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsSetting'
GO


-- ----------------------------
-- Records of B_ColumnsSetting
-- ----------------------------



-- ----------------------------
-- Table structure for B_ColumnsUnit
-- ----------------------------

CREATE TABLE [dbo].[B_ColumnsUnit] (
  [ColID] bigint  NOT NULL,
  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Width] int  NULL,
  [DataAddTime] datetime  NULL,
  [DataUpdateTime] datetime  NULL,
  [Render] text COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[B_ColumnsUnit] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据加入时间',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsUnit',
'COLUMN', N'DataAddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据更新时间',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsUnit',
'COLUMN', N'DataUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报告列表显示列配置',
'SCHEMA', N'dbo',
'TABLE', N'B_ColumnsUnit'
GO


-- ----------------------------
-- Records of B_ColumnsUnit
-- ----------------------------
INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'1', N'床号', N'Bed', NULL, N'40', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'2', N'报告日期', N'CHECKDATE', NULL, N'150', NULL, NULL, N'function (v, meta, record, index) {
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
	        }')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'3', N'姓名', N'CNAME', NULL, N'60', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'4', N'检验项目', N'ItemName', NULL, N'140', NULL, NULL, N'function (value, meta, record) {
	            if (value) meta.tdAttr = ''data-qtip="<b>'' + value + ''</b>"'';
	            return value;
	        }')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'5', N'病历号', N'PatNo', NULL, N'60', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'6', N'核收日期', N'RECEIVEDATE', NULL, N'80', NULL, NULL, N'function (v, meta, record, index) {
	 var value = record.get("RECEIVEDATE").split('' '')[0];
	 return value;
}')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'7', N'打印次数', N'PRINTTIMES', NULL, N'60', NULL, NULL, N'function (v, meta, record) {
	                var imgName = (v && v >= me.maxPrintTimes) ? "unprint" : "print",
		                tootip = "已经打印<b style=''color:red;''> " + v + " </b>次",
	                    value = v ? "  <b>" + v + "</b>" : "";

	                meta.tdAttr = ''data-qtip="'' + tootip + ''"'';
	                
	                var result = '''';
	                if(v >= 0){
	                    result = "<img src=''" + Shell.util.Path.uiPath + "/ReportPrint/images/" + imgName + ".png''/>" + v;
	                }
	                
	                return result;
	            }')
GO


-- ----------------------------
-- Table structure for B_Parameter
-- ----------------------------

CREATE TABLE [dbo].[B_Parameter] (
  [Id] bigint  NOT NULL,
  [Name] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParaType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParaNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParaValue] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParaDesc] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DataUpdateTime] datetime  NULL,
  [IsUse] bit  NULL,
  [ShortCode] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PinYinZiTou] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[B_Parameter] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据更新时间',
'SCHEMA', N'dbo',
'TABLE', N'B_Parameter',
'COLUMN', N'DataUpdateTime'
GO


-- ----------------------------
-- Records of B_Parameter
-- ----------------------------
INSERT INTO [dbo].[B_Parameter]  VALUES (N'1', N'页面公共配置', N'allPageType', N'config', N'defaultWhere', N'', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'2', N'页面公共配置', N'allPageType', N'config', N'requestParamsArr', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'3', N'页面公共配置', N'allPageType', N'config', N'hisRequestParamsArr', N'OLDSERIALNO', NULL, N'stringArry', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'4', N'页面公共配置', N'allPageType', N'config', N'defaultDates', N'1', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'5', N'页面公共配置', N'allPageType', N'config', N'defaultPageSize', N'50', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'6', N'页面公共配置', N'allPageType', N'config', N'hasPrint', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'7', N'页面公共配置', N'allPageType', N'config', N'A4Type', N'1', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'8', N'页面公共配置', N'allPageType', N'config', N'printType', N'A4', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'9', N'页面公共配置', N'allPageType', N'config', N'maxPrintTimes', N'100', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'10', N'页面公共配置', N'allPageType', N'config', N'mergePageCount', N'100', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'11', N'页面公共配置', N'allPageType', N'config', N'ForcedPagingField', N'', NULL, N'hash', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'12', N'页面公共配置', N'allPageType', N'config', N'openAddPrintTimes', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'13', N'页面公共配置', N'allPageType', N'config', N'checkUnprint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'14', N'页面公共配置', N'allPageType', N'config', N'checkFilter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'15', N'页面公共配置', N'allPageType', N'config', N'headCollapsed', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'16', N'页面公共配置', N'allPageType', N'config', N'autoSelect', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'17', N'页面公共配置', N'allPageType', N'config', N'CheckOnly', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'18', N'页面公共配置', N'allPageType', N'config', N'hasReportPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'19', N'页面公共配置', N'allPageType', N'config', N'hasResultPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'20', N'页面公共配置', N'allPageType', N'config', N'defaultCheckedPage', N'1', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'21', N'页面公共配置', N'allPageType', N'config', N'hasPdfPrinter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'22', N'页面公共配置', N'allPageType', N'config', N'pdfPrinterList', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'23', N'页面公共配置', N'allPageType', N'config', N'DateField', NULL, NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'24', N'页面公共配置', N'allPageType', N'config', N'isListHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'66', N'查询打印页面配置', N'自助打印', N'config', N'printPageType', N'双A5', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'67', N'查询打印页面配置', N'自助打印', N'config', N'printtimes', N'1', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'68', N'查询打印页面配置', N'自助打印', N'config', N'selectColumn', N'PatNo', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'69', N'查询打印页面配置', N'自助打印', N'config', N'lastDay', N'100', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'70', N'查询打印页面配置', N'自助打印', N'config', N'tackTime', N'5', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'71', N'查询打印页面配置', N'自助打印', N'config', N'showName', N'卡名', N'', N'string', N'2018-10-16 11:19:54.000', N'0', NULL, NULL)
GO


-- ----------------------------
-- Table structure for B_SearchSetting
-- ----------------------------

CREATE TABLE [dbo].[B_SearchSetting] (
  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL,
  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL,
  [STID] bigint  NOT NULL,
  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TextWidth] int  NULL,
  [Width] int  NULL,
  [ShowOrderNo] int  NULL,
  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DataAddTime] datetime  NULL,
  [DataUpdateTime] datetime  NULL,
  [IsShow] bit  NULL,
  [SID] bigint  NULL
)
GO

ALTER TABLE [dbo].[B_SearchSetting] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据加入时间',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchSetting',
'COLUMN', N'DataAddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据更新时间',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchSetting',
'COLUMN', N'DataUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配置查询项的模板',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchSetting'
GO


-- ----------------------------
-- Records of B_SearchSetting
-- ----------------------------



-- ----------------------------
-- Table structure for B_SearchUnit
-- ----------------------------

CREATE TABLE [dbo].[B_SearchUnit] (
  [SID] bigint  NOT NULL,
  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TextWidth] int  NULL,
  [Width] int  NULL,
  [DataAddTime] datetime  NULL,
  [DataUpdateTime] datetime  NULL,
  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL,
  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[B_SearchUnit] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据加入时间',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchUnit',
'COLUMN', N'DataAddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据更新时间',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchUnit',
'COLUMN', N'DataUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'查询条件配置',
'SCHEMA', N'dbo',
'TABLE', N'B_SearchUnit'
GO


-- ----------------------------
-- Records of B_SearchUnit
-- ----------------------------
INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'1', N'科室过滤', N'DeptNo', N'other', N'55', N'150', NULL, NULL, N'uxCheckTrigger', NULL, N'{
                    check: function (p, record) {
                        if (record == null) {
                            me.items.items[1].getComponent("DeptNo").setValue("");
                            me.items.items[1].getComponent("ClienteleName").setValue("");
                            return;
                        }
                        me.items.items[1].getComponent("DeptNo").setValue("(" + record.get("DeptNo") + ")");
                        me.items.items[1].getComponent("ClienteleName").setValue(record.get("CName"));
                        p.close();
                    }
                }', N'{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: "DeptNo", name: ''DeptNo'', width: 130, hidden: true } searchAndNext
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
                className: ''Shell.class.dept.CheckGrid'',
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
            }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'2', N'就诊类型', N'SickTypeName', N'other', NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{
                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType'', boxLabel: ''门诊'', width: 50,
                listeners:{
                    change: function (m, v) {
					    var me = this.ownerCt.ownerCt;
                        var sick = me.getItem("mgSickType");
                        if (v) {
                            if (sick.getValue().length < 3 || sick.getValue() == "" || sick.getValue == null) {
                                sick.setValue("(''门诊'')");
                            } else {
                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",''门诊'')");
                            }
                            sick.type = ''search'';
                        } else {
                            var index = sick.getValue().indexOf("门诊");
                            if (index == 2) {
                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
                            } else {
                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
                            }

                        }
                        if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
                            sick.type = ''other'';
                            sick.setValue("");
                        }
                    }
	            }
            }searchAndNext
            { type: ''search'', xtype: ''textfield'', itemId: ''mgSickType'', mark: ''in'', name: ''SickTypeName'', hidden: true }searchAndNext
            {
                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType1'', boxLabel: ''住院'', width: 50,
                listeners: {
                    change: function (m, v) {
					    var me = this.ownerCt.ownerCt;
                        var sick = me.getItem("mgSickType");
                        if (v) {
                            if (sick.getValue().length < 3 || sick.getValue() == "" || sick.getValue == null) {
                                sick.setValue("(''住院'')");
                            } else {
                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",''住院'')");
                            }
                            sick.type = ''search'';
                        } else {
                            var index = sick.getValue().indexOf("住院");
                            if (index == 2) {
                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
                            } else {
                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
                            }

                        }

                        if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
                            sick.type = ''other'';
                        }
                    }
                }
            }searchAndNext
            {
                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType2'', boxLabel: ''体检'', width: 50,
                listeners: {
                    change: function (m, v) {
				     	var me = this.ownerCt.ownerCt;
                        var sick = me.getItem("mgSickType");
                        if (v) {
                            if (sick.getValue().length < 3 || sick.getValue() == "" || sick.getValue == null) {
                                sick.setValue("(''体检'')");
                            } else {
                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",''体检'')");
                            }
                            sick.type = ''search'';
                        } else {
                            var index = sick.getValue().indexOf("体检");
                            if (index == 2) {
                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
                            } else {
                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
                            }

                        }

                        if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
                            sick.type = ''other'';
                        }
                    }
                }
            }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'3', N'姓名', N'CNAME', N'search', N'35', N'110', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''CNAME'', fieldLabel: ''姓名'', labelWidth: 35, width: 110 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'4', N'样本号', N'SAMPLENO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SAMPLENO'', fieldLabel: ''样本号'', labelWidth: 50, width: 150 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'5', N'病历号', N'PATNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''PATNO'', fieldLabel: ''病历号'', labelWidth: 50, width: 150 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'6', N'床号', N'Bed', N'search', N'35', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''床号'', labelWidth: 30, width: 130 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'7', N'本周', N'本周', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: "本周", tooltip: "查询本周数据", vType:1,where: "datediff(week,RECEIVEDATE,getdate())=0" }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'8', N'本月', N'本月', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: "本月", tooltip: "查询本月数据", vType:2,where: "datediff(month,RECEIVEDATE,getdate())=0" }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'9', N'时间', N'时间', NULL, NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{
                type: ''other'', 
								xtype: ''combobox'',
                name: ''defaultPageSize'',
                itemId: ''defaultPageSize'',
                width: 130,
                displayField: ''text'', 
valueField: ''value'',
store:Ext.create(''Ext.data.Store'', {
	                    fields: [''text'', ''value''],
	                    data: [
                            { text: ''审核（报告）时间'', value: ''CHECKDATE'' },
                            { text: ''核收日期'', value: ''RECEIVEDATE'' },
                            { text: ''采样日期'', value: ''COLLECTDATE'' },
                            { text: ''签收日期'', value: ''INCEPTDATE'' },
                            { text: ''检测（上机）日期'', value: ''TESTDATE'' },
                            { text: ''录入（操作）日期'', value: ''OPERDATE'' }
	                    ]
	                }),
                value: [''CHECKDATE''],
                listeners: {
                    change: function (m, v) {
                        var selectDate = this.ownerCt.ownerCt.getItem(''selectdate'');
                        selectDate.name = v;
                        this.ownerCt.ownerCt.DateField = v;
                    }
                }
            }searchAndNext
		    { type: ''search'', xtype: ''uxdatearea'', itemId: ''selectdate'', name: ''CHECKDATE'', labelWidth: 0, width: 190 }
')
GO


-- ----------------------------
-- Primary Key structure for table B_ColumnsSetting
-- ----------------------------
ALTER TABLE [dbo].[B_ColumnsSetting] ADD CONSTRAINT [PK_B_COLUMNSSETTING] PRIMARY KEY CLUSTERED ([CTID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table B_ColumnsUnit
-- ----------------------------
ALTER TABLE [dbo].[B_ColumnsUnit] ADD CONSTRAINT [PK_B_COLUMNSUNIT] PRIMARY KEY CLUSTERED ([ColID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table B_Parameter
-- ----------------------------
ALTER TABLE [dbo].[B_Parameter] ADD CONSTRAINT [PK_B_PARAMETER] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table B_SearchSetting
-- ----------------------------
ALTER TABLE [dbo].[B_SearchSetting] ADD CONSTRAINT [PK_B_SELECTSETTING] PRIMARY KEY CLUSTERED ([STID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table B_SearchUnit
-- ----------------------------
ALTER TABLE [dbo].[B_SearchUnit] ADD CONSTRAINT [PK_B_SELECTUNIT] PRIMARY KEY CLUSTERED ([SID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

