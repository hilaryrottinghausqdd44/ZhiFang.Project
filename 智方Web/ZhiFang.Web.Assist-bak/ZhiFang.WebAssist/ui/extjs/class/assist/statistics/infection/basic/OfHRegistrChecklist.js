/**
 * 院感统计--环境卫生学监测报告
 * @author longfc
 * @version 2020-12-15
 */
Ext.define('Shell.class.assist.statistics.infection.basic.OfHRegistrChecklist', {
	extend: 'Shell.class.assist.statistics.infection.basic.InfectionDtlGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '院感登记清单',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL?isPlanish=true',

	defaultOrderBy: [{
		property: 'GKSampleRequestForm_DataAddTime',
		direction: 'ASC'
	}],

	/**用户UI配置Key*/
	userUIKey: 'statistics.infection.OfHRegistrChecklist',
	/**用户UI配置Name*/
	userUIName: "院感登记清单",

	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 3,
	/**院感登记报表类型*/
	groupType:6,
	/**样本状态状态Key*/
	StatusKey: "GKSampleFormStatus",
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//me.initDateArea();
	},
	initComponent: function() {
		var me = this;
		JcallShell.BLTF.StatusList.getStatusList(me.StatusKey, false, true, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '申请单Id',
			dataIndex: 'GKSampleRequestForm_Id',
			isKey: true,
			hidden: true
		}, {
			text: '监测级别',
			dataIndex: 'GKSampleRequestForm_MonitorType',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value;
				var style = 'font-weight:bold;color:';
				if (value == "1") {
					style = style + "#ffffff;background-color:#2F4056;";
					v = "感控监测";
				}else {
					style = style + "#ffffff;background-color:#009688;";
					v = "科室监测";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		},{
			text: '登记日期',
			dataIndex: 'GKSampleRequestForm_DataAddTime',
			width: 140,
			isDate: true,
			hasTime: true
		}, {
			text: '申请单号',
			dataIndex: 'GKSampleRequestForm_ReqDocNo',
			width: 110,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '条码号',
			dataIndex: 'GKSampleRequestForm_BarCode',
			width: 110,
			defaultRenderer: true
		}, {
			text: '样本状态',
			dataIndex: 'GKSampleRequestForm_StatusID',
			width: 90,
			renderer: function(value, meta) {
				var v = value;
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum != null)
					v = JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JcallShell.BLTF.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JcallShell.BLTF.StatusList.Status[me.StatusKey].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '科室',
			dataIndex: 'GKSampleRequestForm_DeptCName',
			width: 110,
			defaultRenderer: true
		}, {
			text: '监测类型编码',
			dataIndex: 'GKSampleRequestForm_SCRecordType_Id',
			width: 110,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '监测类型',
			dataIndex: 'GKSampleRequestForm_SCRecordType_CName',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息1',
			dataIndex: 'GKSampleRequestForm_ItemResult1',
			width: 90,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息2',
			dataIndex: 'GKSampleRequestForm_ItemResult2',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息3',
			dataIndex: 'GKSampleRequestForm_ItemResult3',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息4',
			dataIndex: 'GKSampleRequestForm_ItemResult4',
			width: 100,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样本号',
			dataIndex: 'GKSampleRequestForm_SampleNo',
			width: 80,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 55,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			text: '样品信息(DtlJArray)',
			dataIndex: 'GKSampleRequestForm_DtlJArray',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '采样者',
			dataIndex: 'GKSampleRequestForm_Sampler',
			width: 90,
			defaultRenderer: true
		}, {
			text: '采样日期',
			dataIndex: 'GKSampleRequestForm_SampleDate',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '采样时间',
			dataIndex: 'GKSampleRequestForm_SampleTime',
			width: 65,
			renderer: function(value, meta, record) {
				if (value) {
					value = Ext.util.Format.date(value, 'H:i');
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				}
				return value;
			}
		}, {
			text: '检验结果',
			dataIndex: 'GKSampleRequestForm_TestResult',
			width: 100,
			defaultRenderer: true
		}, {
			text: '检验日期',
			dataIndex: 'GKSampleRequestForm_TestTime',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '评估判定',
			dataIndex: 'GKSampleRequestForm_Judgment',
			width: 100,
			renderer: function(value, meta) {
				var v = value;
				var style = 'font-weight:bold;color:';
				if (value == "1" || value == "true" || value == "合格") {
					style = style + "#ffffff;background-color:#009688;";
					v = "合格";
				} else if (value == "2" || value == "false" || value == "不合格") {
					style = style + "#ffffff;background-color:#FF5722;";
					v = "不合格";
				} else {
					style = style + "#ffffff;background-color:#FFB800;";
					v = "未评估";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '评估者',
			dataIndex: 'GKSampleRequestForm_Evaluators',
			width: 100,
			defaultRenderer: true
		}, {
			text: '评估日期',
			dataIndex: 'GKSampleRequestForm_EvaluationDate',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '检验者',
			dataIndex: 'GKSampleRequestForm_TesterName',
			width: 100,
			defaultRenderer: true
		}, {
			text: '显示次序',
			dataIndex: 'GKSampleRequestForm_DispOrder',
			width: 60,
			hidden: true,
			defaultRenderer: true,
			type: 'int'
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'GKSampleRequestForm_Visible',
			width: 40,
			align: 'center',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			dataIndex: 'GKSampleRequestForm_ReceiveFlag',
			text: '核收标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_ResultFlag',
			text: '结果回写',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_EvaluatorFlag',
			text: '评估标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_Archived',
			text: '归档标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**
	 * @description 监测类型及样品列信息样式处理
	 * @param {Object} value2
	 */
	getCellStyle: function(value2) {
		//var style = 'font-weight:bold;';color:#1c8f36;
		var style = ''; //font-weight:bold;
		if (value2 == "11") {
			style = style + "background-color:#C0FFC0;";
		} else if (value2 == "12") {
			style = style + "background-color:#FFE0C0;";
		} else if (value2 == "13") {
			style = style + "background-color:#FFC0FF;";
		} else if (value2 == "14") {
			style = style + "background-color:#C0FFFF;";
		} else if (value2 == "15") {
			style = style + "background-color:#C0C0FF;";
		} else if (value2 == "16") {
			style = style + "background-color:#FFFFC0;";
		} else if (value2 == "17") {
			style = style + "background-color:#00C0C0;";
		} else if (value2 == "18") {
			style = style + "background-color:#C0C000";
		} else {
			style = style + "background-color:#C0C000";
		}
		//style = style + "color:#ffffff;";
		return style;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		if (me.hasRefresh) items.push('refresh');

		items = me.createRecordTypeItem(items);
		items.push('-');
		
		//按科室查询项
		if (me.IsHaveDept == true) {
			items = me.createDeptItem(items);
			items.push('-');
		}
		
		items = me.createDateareaItem(items);
		items.push('-');

		items = me.createMonitorTypeItem(items);
		items.push('-');
		
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		items.push('-');
		
		items = me.createPDFEXCELItems(items);
		
		return items;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		
		me.setcolumns1(records);
	},
	setcolumns1:function(records){
		var me = this;
		if (records && records.length > 0 && me.CurRecordTypeValue != "") {
			var dtlJArray = records[0].get("GKSampleRequestForm_DtlJArray");
			var recordTypeId = records[0].get("GKSampleRequestForm_SCRecordType_Id");
			if (dtlJArray) dtlJArray = JShell.JSON.decode(dtlJArray);
			
			if (dtlJArray && dtlJArray.length > 0) {
				for (var i = 0; i < dtlJArray.length; i++) {
					var item = dtlJArray[i];
					if(!item)continue;
					
					for (var j = 0; j < me.columns.length; j++) {
						if (item["dataIndex"] == me.columns[j]["dataIndex"]) {
							me.hasSetColumnsText=true;
							me.columns[j].setText (item["CName"]);
							//记录项是否开单可见
							var isBillVisible=""+item["IsBillVisible"];
							if(isBillVisible=="1"||isBillVisible=="true"){
								isBillVisible=true;
							}else{
								isBillVisible=false;
							}
							me.columns[j].setVisible(isBillVisible);
							break;
						}
					}
				}
			} else {
				me.setColumns2();
			}
		} else {
			me.setColumns2();
		}
	},
	setColumns2: function() {
		var me = this;
		var isBreak=false;
		for (var j = 0; j < me.columns.length; j++) {
			if(isBreak==true){
				me.hasSetColumnsText=false;
				break;
			}
			var dataIndex = me.columns[j]["dataIndex"];
			switch (dataIndex) {
				case "GKSampleRequestForm_ItemResult1":
					me.columns[j].setText("样品信息1");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult2":
					me.columns[j].setText("样品信息2");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult3":
					me.columns[j].setText("样品信息3");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult4":
					me.columns[j].setText( "样品信息4");
					me.columns[j].setVisible(true);
					isBreak=true;
					break;	
				default:
					break;
			}
		}
	}
	
});
