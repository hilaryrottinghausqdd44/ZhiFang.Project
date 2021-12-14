/**
 * @description 待归档处理
 * @author longfc
 * @version 2020-11-27
 */
Ext.define('Shell.class.assist.infection.alertinfo.ToBeArchivedGrid', {
	extend: 'Shell.class.assist.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '待归档处理',
	width: 800,
	height: 500,

	/**效期预警类型:2:效期已过期报警;3:效期将过期报警*/
	qtyType: 2,
	/**合并条件：默认不合并*/
	groupType: 0,
	/**预警类型:效期已过期预警*/
	AlertTypeId: '3',
	/**效期预警默认已过期天数*/
	expiredDays: 1,
	/**用户UI配置Key*/
	userUIKey: 'alertinfo.ToBeArchivedGrid',
	/**用户UI配置Name*/
	userUIName: "待归档处理",
	
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestFormByField',
	
	/**样本状态状态Key*/
	StatusKey: "GKSampleFormStatus",
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	defaultOrderBy: [{
		property: 'GKSampleRequestForm_EvaluationDate',
		direction: 'ASC'
	}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//评估标志为是并且归档标志为否
		me.defaultWhere="gksamplerequestform.EvaluatorFlag=1 and gksamplerequestform.Archived=0";
		
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
			text: '评估日期',
			dataIndex: 'GKSampleRequestForm_EvaluationDate',
			width: 140,
			isDate: true,
			hasTime: true
		},{
			dataIndex: 'GKSampleRequestForm_Archived',
			text: '归档标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		},{
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
			width: 120,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '检验结果',
			dataIndex: 'GKSampleRequestForm_TestResult',
			width: 100,
			defaultRenderer: true
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
			text: '样品信息',
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
			/* isDate: true,
			hasTime: true, */
			renderer: function(value, meta, record) {
				//var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) {
					value = Ext.util.Format.date(value, 'H:i');
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				}
				//meta.style = me.getCellStyle(value2);
				return value;
			}
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
		} ];
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
		var dataList = [];
		if(JcallShell.BLTF.StatusList.Status[me.StatusKey]) {
			dataList = JcallShell.BLTF.StatusList.Status[me.StatusKey].List;
		}
		var items = ['refresh'];
		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'chooseRecordType',
			labelWidth: 0,
			width: 120,
			hasStyle: true,
			value: "",
			data: [
				['', '所有', 'background-color:#FFC0C0;font-weight:bold;'],
				['11', '手卫生', 'background-color:#C0FFC0;'],
				['12', '空气培养', 'background-color:#FFE0C0;'],
				['13', '物体表面', 'background-color:#FFC0FF;'],
				['14', '消毒剂', 'background-color:#C0FFFF;'],
				['15', '透析液及透析用水', 'background-color:#C0C0FF;'],
				['16', '医疗器材', 'background-color:#FFFFC0;'],
				['17', '污水', 'background-color:#00C0C0;'],
				['18', '其它', 'background-color:#C0C000;']
			],
			/* 
			data: [
				['', '所有'],
				['11', '手卫生'],
				['12', '空气培养'],
				['13', '物体表面'],
				['14', '消毒剂'],
				['15', '透析液及透析用水'],
				['16', '医疗器材'],
				['17', '污水'],
				['18', '其它']
			],*/
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		
		items.push('-', {
			labelWidth: 45,
			width: 125,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '天数选择',
			fieldLabel: '已评估',
			value: 30,
			name: 'EvaluationDate',
			itemId: 'EvaluationDate',
			xtype: 'numberfield',
			minValue: 0,
			maxValue: 365,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '天未进行归档',
			labelSeparator: '',
			width: 85
		});
		
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			style: {
				marginLeft: "5px"
			},
			handler: function() {
				me.onSearch();
			}
		});
		items.push('-',{
			xtype: 'button',
			iconCls: 'button-check',
			text: '归档',
			tooltip: '归档',
			handler: function() {
				me.onSave();
			}
		});
		return items;
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var chooseRecordType = buttonsToolbar.getComponent('chooseRecordType');
		var days = buttonsToolbar.getComponent('EvaluationDate').getValue();
		
		//监测类型
		if (chooseRecordType && chooseRecordType.getValue()) {
			params.push("gksamplerequestform.SCRecordType.Id=" + chooseRecordType.getValue());
		}
		
		if(days && days >= 0) {
			var dateValue = me.calcDateArea(-days);
			//InvalidWarningDate
			if(dateValue.start)
				where.push("gksamplerequestform.EvaluationDate>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
			if(dateValue.end)
				where.push("gksamplerequestform.EvaluationDate<'" + JShell.Date.toString(dateValue.end, true) + " 23:59:59'");
		}
		if(where.length>0)where=where.join(" and ");
		return where;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: [{
				xtype: 'displayfield',
				itemId: "btnInfo",
				disabled: false,
				value: '说明:<b style="color:blue;">以评估完成的日期进行过滤提示;</b>'
			}]
		};
		return items;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		return data;
	},
	/**加载数据*/
	loadData: function() {
		var me = this;
		me.onSearch();
	},
	/**
	 * @description 归档处理
	 */
	onSave: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
	
		JShell.Msg.confirm({
			title: "确定对当前选择的行进行归档操作吗?"
		}, function(but) {
			if (but != "ok") return;
	
			me.showMask(me.saveText); //显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = records.length;
	
			for (var i = 0; i < records.length; i++) {
				var rec = records[i];
				me.updateInfo(i, rec);
			}
		});
	},
	/**
	 * @param {Object} index
	 * @param {Object} rec
	 */
	updateInfo: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
	
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
	
		var id = rec.get(me.PKField);
		var params = Ext.JSON.encode({
			"entity": {
				"Id": id,
				"Archived":1,//归档标志
				"StatusID": 6//已归档
			},
			"fields": 'Id,StatusID',
			"empID": empID,
			"empName": empName
		});
	
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	}
});