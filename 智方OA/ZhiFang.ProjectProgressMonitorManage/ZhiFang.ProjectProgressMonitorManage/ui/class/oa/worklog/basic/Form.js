/**
 * 工作日志表单
 * @author longfc
 * @version 2016-08-02
 */
Ext.define('Shell.class.oa.worklog.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Ext.tip.ToolTip',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.oa.worklog.basic.PWorkLogCopyFor'
	],
	bodyPadding: '2px 2px 2px 2px',
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 160,
		labelAlign: 'right'
	},
	formtype: "show",
	title: '工作日志',
	width: 465,
	height: 560,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkMonthLogById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLogByWeiXin',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkMonthLogByField',

	/**显示成功信息*/
	showSuccessInfo: false,

	autoScroll: true,
	PK: '',
	/**查询对象*/
	objectEName: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		Ext.QuickTips.init();
		me.objectEName=me.objectEName||"";
		var width = document.body.clientWidth * 0.54;

		me.width = document.body.clientWidth - width;
		me.defaults.width = (me.width - me.defaults.labelWidth) / me.layout.columns;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建文档可见组件*/
	createAddShowItems: function() {
		var me = this;

		var ToDayContentLabel = "总结";
		var NextDayContentLabel = "计划";

//		switch(me.objectEName) {
//			case "WorkLogDay":
//				ToDayContentLabel = "今天总结";
//				NextDayContentLabel = "明天计划";
//				break;
//			case "PWorkWeekLog":
//				ToDayContentLabel = "本周总结";
//				NextDayContentLabel = "下周计划";
//				break;
//			case "PWorkMonthLog":
//				ToDayContentLabel = "本月总结";
//				NextDayContentLabel = "下月计划";
//				break;
//			default:
//				break;
//		}

		me.createToDayContent(ToDayContentLabel);
		me.createNextDayContent(NextDayContentLabel);
		me.createWorkLogExportLevel("范围");
		me.createStatus('是否提交');
		me.createCopyUser("抄送人");
	},
	/**当天/本周/本月计划*/
	createToDayContent: function(fieldLabel) {
		var me = this;
		me.ToDayContent = {
			fieldLabel: fieldLabel,
			name: me.objectEName + '_ToDayContent',
			itemId: me.objectEName + '_ToDayContent',
			allowBlank: false,
			minHeight: 155,
			emptyText: '工作计划',
			tooltip: '工作计划',
			style: {
				marginBottom: '0px 0px 0px 5px'
			},
			xtype: 'textarea'
		};
		
	},
	/**明天/下周/下月计划*/
	createNextDayContent: function(fieldLabel) {
		var me = this;
		me.NextDayContent = {
			fieldLabel: fieldLabel,
			name: me.objectEName + '_NextDayContent',
			itemId: me.objectEName + '_NextDayContent',
			//allowBlank: false,
			minHeight: 140,
			emptyText: '工作计划',
			style: {
				marginBottom: '0px 0px 0px 5px'
			},
			xtype: 'textarea'
		};
	},

	/**抄送人*/
	createCopyUser: function(fieldLabel) {
		var me = this;
		me.PWorkLogCopyFor = {
			boxLabel: fieldLabel,
			name: 'PWorkLogCopyFor',
			itemId: 'PWorkLogCopyFor',
			xtype: 'pworklogcopyfor',
			fieldLabel: fieldLabel,
			defaultLoad: false,
			allowBlank: true,
			emptyText: '请选择',
			labelWidth: me.defaults.labelWidth,
			formtype: me.formtype,
			PK: me.PK
		};
	},

	createStatus: function(fieldLabel) {
		var me = this;
		var hidden = false;
		switch(me.objectEName) {
			case "WorkDayLog":
				hidden = true;
				break;
			default:
				break;
		}
		me.Status = {
			boxLabel: fieldLabel,
			name: me.objectEName + '_Status',
			itemId: me.objectEName + '_Status',
			xtype: 'checkbox',
			hidden: hidden,
			checked: true,
			style: {
				marginLeft: '0px'
			}
		};
	},
	createWorkLogExportLevel: function(fieldLabel) {
		var me = this;
		me.WorkLogExportLevel = {
			name: me.objectEName + '_WorkLogExportLevel',
			itemId: me.objectEName + '_WorkLogExportLevel',
			fieldLabel: fieldLabel,
			xtype: 'uxSimpleComboBox',
			hasStyle: false,
			editable: false,
			value: '0',
			emptyText: "请选择",
			data: [
				['0', '仅自己和直接主管可见'],
				['1', '所属部门可见'],
				['2', '全公司可见']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {

				}
			}
		};
	},
	createImage1: function(fieldLabel) {
		var me = this;
		me.Image1 = {
			xtype: 'filefield',
			name: me.objectEName + '_Image1',
			itemId: me.objectEName + '_Image1',
			fieldLabel: fieldLabel,
			emptyText: '请选择',
			buttonConfig: {
				iconCls: 'search-img',
				text: ''
			},
			listeners: {
				change: function(field, value) {
					//me.ImgChange();
				}
			}
		};
	},

	/**@overwrite 获取列表布局组件内容*/
	getAddTableLayoutItems: function() {
		var me = this,
			items = [];
		//当天/本周/本月计划
		me.ToDayContent.colspan = 3;
		me.ToDayContent.width = me.defaults.width * me.ToDayContent.colspan;
		items.push(me.ToDayContent);

		//明天/下周/下月计划
		me.NextDayContent.colspan = 3;
		me.NextDayContent.width = me.defaults.width * me.NextDayContent.colspan;
		items.push(me.NextDayContent);

		//抄送人
		me.PWorkLogCopyFor.colspan = 3;
		me.PWorkLogCopyFor.width = me.defaults.width * me.PWorkLogCopyFor.colspan;
		items.push(me.PWorkLogCopyFor);

		//可见级别
		me.WorkLogExportLevel.colspan = 1;
		me.WorkLogExportLevel.width = 220; //me.defaults.width * me.WorkLogExportLevel.colspan;
		items.push(me.WorkLogExportLevel);

		//是否提交
		me.Status.colspan = 1;
		me.Status.width = me.defaults.width * me.Status.colspan;
		items.push(me.Status);

		return items;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var reg = new RegExp("<br />", "g");
		var toDayContent = me.objectEName + '_ToDayContent';
		var NextDayContent = me.objectEName + '_NextDayContent';
		var nameKey = me.objectEName + '_WorkLogExportLevel';
		switch(data[nameKey]) {
			case "仅自己和直接主管可见":
				data[nameKey] = "0";
				break;
			case "所属部门可见":
				data[nameKey] = "1";
				break;
			case "全公司可见":
				data[nameKey] = "2";
				break;
			default:
				break;
		}
		data[toDayContent] = data[toDayContent].replace(reg, "\r\n");
		data[NextDayContent] = data[NextDayContent].replace(reg, "\r\n");
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},

	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: me.objectEName + '_Id'
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var values = me.getForm().getValues();
		var params = {
			entity: {}
		};
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var EmpName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		var nextDayContent = "";
		var nameKey = me.objectEName + '_NextDayContent';
		if(values[nameKey]) {
			nextDayContent = values[nameKey];
			nextDayContent = values[nameKey].replace(/\\/g, '&#92');
//			nextDayContent = nextDayContent.replace(/[\r\n]/g, '<br />');
		}
		var toDayContent = "";
		nameKey = me.objectEName + '_ToDayContent';
		if(values[nameKey]) {
			toDayContent = values[nameKey];
			toDayContent = toDayContent.replace(/\\/g, '&#92');
//			toDayContent = toDayContent.replace(/[\r\n]/g, '<br />');
		}
		var workLogExportLevel = "";
		nameKey = me.objectEName + '_WorkLogExportLevel';
		if(values[nameKey]) {
			workLogExportLevel = values[nameKey];
		}

		var WorkLogExportLevel = me.getComponent(me.objectEName + '_WorkLogExportLevel').getValue();
		var entity = {
			NextDayContent: nextDayContent,
			ToDayContent: toDayContent,
			IsUse: true,
			WorkLogExportLevel: workLogExportLevel,
			Status: values[me.objectEName + '_Status'] ? true : false,
			CopyForEmpIdList: null, //抄送人Id集合
			CopyForEmpNameList: null //抄送人名称集合
		};
		switch(me.objectEName) {
			case "PWorkWeekLog":
				entity.Status = values[me.objectEName + '_Status'] ? true : false;
				break;
			case "PWorkMonthLog":
				entity.Status = values[me.objectEName + '_Status'] ? true : false;
				break;
			default:
				break;
		}
		//抄送人
		var copyUserValue = null;
		var copyUser = me.getComponent('PWorkLogCopyFor');
		copyUserValue = copyUser.getValue();
		if(copyUserValue && copyUserValue != null) {
			var EmpIdArr = [],
				EmpNameArr = [];
			EmpNameArr = copyUserValue.EmpNameArr;
			EmpIdArr = copyUserValue.EmpIdArr;
			if(EmpIdArr != null || EmpIdArr != "") {
				entity.CopyForEmpIdList = EmpIdArr;
			}
			if(EmpNameArr != null || EmpNameArr != "") {
				entity.CopyForEmpNameList = EmpNameArr;
			}
		}
		params.AttachmentUrlList = [];
		params.entity = entity;
		return params;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		entity = me.getAddParams();
		var values = me.getForm().getValues();
		var fields = [];
		var fields = ['Id', 'ToDayContent', 'NextDayContent', 'WorkLogExportLevel'];
		switch(me.objectEName) {
			case 'PWorkWeekLog':
				fields.push('Status');
				break;
			case "PWorkMonthLog":
			case '':
				fields.push('Status');
				break;
			default:
				break;
		}
		entity.fields = fields.join(',');
		delete entity.AttachmentUrlList;
		entity.entity.Id = values[me.objectEName + '_Id'];
		return entity;
		return;
	}
});