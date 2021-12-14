/**
 * 查询条件
 * @author longfc
 * @version 2017-03-27
 */
Ext.define('Shell.class.wfm.business.pproject.SearchToolbar', {
	extend: 'Ext.toolbar.Toolbar',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	height: 160,

	/**布局方式*/
	layout: 'absolute',
	/**默认组件*/
	defaultType: 'textfield',
	/** 每个组件的默认属性*/
	defaults: {
		width: 180,
		labelWidth: 65,
		labelAlign: 'right'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSearchClick,save,onCopyClick');
		//初始化送检时间
		me.initDate();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		if(Sysdate == null) Sysdate = new Date();

		var year = Sysdate.getFullYear();
		var month = Sysdate.getMonth() + 1;
		me.defaultBeginDate = JShell.Date.getMonthFirstDate(year, month);
		me.defaultEndDate = JShell.Date.getMonthLastDate(year, month);

	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		var ContentID = me.createComboBox("合同类型", "ContracType");
		ContentID.itemId = "ContentID";
		ContentID.multiSelect = true;
		ContentID.width = 155;
		ContentID.x = 10;
		ContentID.y = 5;
		items.push(ContentID);

		var ProvinceID = me.createComboBox("省份", "ProvinceID");
		ProvinceID.itemId = "ProvinceID";
		ProvinceID.multiSelect = true;
		ProvinceID.labelWidth = 45;
		ProvinceID.width = 125;
		ProvinceID.x = 170;
		ProvinceID.y = 5;
		items.push(ProvinceID);

		var typeID = me.createComboBox("项目类型", "ProjectType");
		typeID.itemId = "TypeID";
		typeID.multiSelect = true;
		typeID.x = 295;
		typeID.y = 5;
		typeID.emptyText='项目类型';
		typeID.width = 280;
		items.push(typeID);

		items.push({
			fieldLabel: '用户',
			emptyText: '用户',
			labelWidth: 35,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid',
			name: 'ClientName',
			itemId: 'ClientName',
			width: 190,
			x: 575,
			y: 5
		}, {
			fieldLabel: '用户',
			emptyText: '用户',
			name: 'PClientID',
			itemId: 'PClientID',
			hidden: true,
			xtype: 'textfield'
		}, {
			x: 755,
			y: 5,
			width: 185,
			labelWidth: 85,
			fieldLabel: '进场时间从',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.defaultBeginDate
		}, {
			x: 940,
			y: 5,
			width: 115,
			labelWidth: 15,
			fieldLabel: '到',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.defaultEndDate
		});

		//第二行
		items.push({
			x: 20,
			y: 32,
			width: 70,
			text: '已进场天数',
			tooltip: '已进场天数',
			xtype: 'label',
			itemId: 'label1'
		}, {
			x: 85,
			y: 30,
			width: 35,
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			text: '2天',
			tooltip: '2天',
			xtype: 'button',
			name: 'btnTwoDays',
			itemId: 'btnTwoDays',
			handler: function() {
				me.onBtnSearch(1, 2);
			}
		}, {
			x: 125,
			y: 30,
			width: 35,
			border: true,
			text: '5天',
			tooltip: '5天',
			xtype: 'button',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			name: 'btnFiveDays',
			itemId: 'btnFiveDays',
			handler: function() {
				me.onBtnSearch(1, 5);
			}
		}, {
			x: 165,
			y: 30,
			width: 40,
			border: true,
			text: '10天',
			tooltip: '10天',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			xtype: 'button',
			itemId: 'btnTenDays',
			handler: function() {
				me.onBtnSearch(1, 10);
			}
		}, {
			x: 210,
			y: 30,
			width: 40,
			text: '15天',
			tooltip: '15天',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			xtype: 'button',
			itemId: 'btnFifteenDays',
			handler: function() {
				me.onBtnSearch(1, 15);
			}
		}, {
			x: 250,
			y: 30,
			width: 40,
			text: '25天',
			tooltip: '25天',
			xtype: 'button',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			itemId: 'btnTwentyFiveDays',
			handler: function() {
				me.onBtnSearch(1, 25);
			}
		}, {
			x: 295,
			y: 30,
			width: 40,
			text: '30天',
			tooltip: '30天',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			xtype: 'button',
			itemId: 'btnThirtyDays',
			handler: function() {
				me.onBtnSearch(1, 30);
			}
		}, {
			x: 340,
			y: 32,
			width: 35,
			text: '计划',
			tooltip: '计划',
			xtype: 'label'
		}, {
			x: 375,
			y: 30,
			width: 35,
			text: '2天',
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			tooltip: '2天',
			xtype: 'button',
			itemId: 'btnPlanTwoDays',
			handler: function() {
				me.onBtnSearch(2, 2);
			}
		}, {
			x: 415,
			y: 30,
			width: 35,
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			text: '4天',
			tooltip: '4天',
			xtype: 'button',
			itemId: 'btnPlanFourDays',
			handler: function() {
				me.onBtnSearch(2, 4);
			}
		}, {
			x: 455,
			y: 30,
			width: 35,
			border: 1,
			style: {
				borderColor: '#D3D3D3',
				borderStyle: 'solid'
			},
			text: '7天',
			tooltip: '7天',
			xtype: 'button',
			itemId: 'btnPlanSevenDays',
			handler: function() {
				me.onBtnSearch(2, 7);
			}
		}, {
			x: 495,
			y: 30,
			width: 40,
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			text: '10天',
			tooltip: '10天',
			xtype: 'button',
			itemId: 'btnPlanTenDays',
			handler: function() {
				me.onBtnSearch(2, 10);
			}
		}, {
			x: 540,
			y: 30,
			width: 40,
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			text: '15天',
			tooltip: '15天',
			xtype: 'button',
			itemId: 'btnPlanFifteenDays',
			handler: function() {
				me.onBtnSearch(2, 15);
			}
		}, {
			x: 585,
			y: 30,
			width: 40,
			border: 1,
			style: {
				borderColor: '#DCDCDC',
				borderStyle: 'solid'
			},
			text: '20天',
			tooltip: '20天',
			xtype: 'button',
			itemId: 'btnPlanTwentyDays',
			handler: function() {
				me.onBtnSearch(2, 20);
			}
		}, {
			x: 630,
			y: 32,
			width: 95,
			text: '内验收',
			tooltip: '内验收',
			xtype: 'label'
		});

		var disabled = true;
		//第三行
		items.push({
			fieldLabel: '实施负责人',
			emptyText: '实施负责人',
			itemId: 'ProjectLeader',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.user.CheckApp',
			labelWidth: 75,
			width: 210,
			x: 10,
			y: 55
		}, {
			fieldLabel: '实施负责人',
			itemId: 'ProjectLeaderID',
			hidden: true,
			xtype: 'textfield'
		}, {
			fieldLabel: '销售负责人',
			emptyText: '销售负责人',
			itemId: 'Principal',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.user.CheckApp',
			labelWidth: 75,
			width: 210,
			x: 230,
			y: 55
		}, {
			fieldLabel: '销售负责人Id',
			itemId: 'PrincipalID',
			hidden: true,
			xtype: 'textfield'
		}, {
			fieldLabel: '进度监督人',
			emptyText: '进度监督人',
			itemId: 'PhaseManager',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.user.CheckApp',
			labelWidth: 75,
			width: 210,
			x: 440,
			y: 55
		}, {
			fieldLabel: '进度监督人Id',
			itemId: 'PhaseManagerID',
			hidden: true,
			xtype: 'textfield'
		});

		//第四行
		var RiskLevelID = me.createComboBox("初始风险等级", "RiskGrade");
		RiskLevelID.itemId = "RiskLevelID";
		RiskLevelID.multiSelect = true;
		RiskLevelID.labelWidth = 88;
		RiskLevelID.width = 223;
		RiskLevelID.x = -3;
		RiskLevelID.y = 80;
		items.push(RiskLevelID);

		var DynamicRiskLevelID = me.createComboBox("动态风险等级", "RiskGrade");
		DynamicRiskLevelID.itemId = "DynamicRiskLevelID";
		DynamicRiskLevelID.multiSelect = true;
		DynamicRiskLevelID.labelWidth = 85;
		DynamicRiskLevelID.width = 220;
		DynamicRiskLevelID.x = 220;
		DynamicRiskLevelID.y = 80;
		items.push(DynamicRiskLevelID);

		var DelayLevelID = me.createComboBox("延期程度", "Delay degree");
		DelayLevelID.itemId = "DelayLevelID";
		DelayLevelID.multiSelect = true;
		DelayLevelID.labelWidth = 75;
		DelayLevelID.width = 210;
		DelayLevelID.x = 440;
		DelayLevelID.y = 80;
		items.push(DelayLevelID);

		var PaceEvalIDStart = me.createComboBox("完成度从", "Completion degree");
		PaceEvalIDStart.itemId = "PaceEvalIDStart";
		PaceEvalIDStart.multiSelect = false;
		PaceEvalIDStart.labelWidth = 65;
		PaceEvalIDStart.width = 145;
		PaceEvalIDStart.x = 650;
		PaceEvalIDStart.y = 80;
		//PaceEvalIDStart.disabled = disabled;

		var PaceEvalIDEnd = me.createComboBox("到", "Completion degree");
		PaceEvalIDEnd.itemId = "PaceEvalIDEnd";
		PaceEvalIDEnd.labelWidth = 20;
		PaceEvalIDEnd.width = 95;
		PaceEvalIDEnd.x = 795;
		PaceEvalIDEnd.y = 80;
		//PaceEvalIDEnd.disabled = disabled;
		items.push(PaceEvalIDStart, PaceEvalIDEnd);

		//第五行
		var ScheduleDelayDaysStart = me.createComboBox("进度延期天数从", "DelayDays");
		ScheduleDelayDaysStart.itemId = "ScheduleDelayDaysStart";
		ScheduleDelayDaysStart.labelWidth = 105;
		ScheduleDelayDaysStart.width = 170;
		ScheduleDelayDaysStart.x = 5;
		ScheduleDelayDaysStart.y = 105;
		ScheduleDelayDaysStart.disabled = disabled;

		var ScheduleDelayDaysEnd = me.createComboBox("到", "DelayDays");
		ScheduleDelayDaysEnd.itemId = "ScheduleDelayDaysEnd";
		ScheduleDelayDaysEnd.labelWidth = 20;
		ScheduleDelayDaysEnd.width = 85;
		ScheduleDelayDaysEnd.x = 175;
		ScheduleDelayDaysEnd.y = 105;
		ScheduleDelayDaysEnd.disabled = disabled;
		items.push(ScheduleDelayDaysStart, ScheduleDelayDaysEnd);

		var ScheduleDelayPercentStart = me.createComboBox("进度延期百分比从", "DelayPercentage");
		ScheduleDelayPercentStart.itemId = "ScheduleDelayPercentStart";
		ScheduleDelayPercentStart.labelWidth = 120;
		ScheduleDelayPercentStart.width = 185;
		ScheduleDelayPercentStart.x = 275;
		ScheduleDelayPercentStart.y = 105;
		ScheduleDelayPercentStart.disabled = disabled;

		var ScheduleDelayPercentEnd = me.createComboBox("到", "DelayPercentage");
		ScheduleDelayPercentEnd.itemId = "ScheduleDelayPercentEnd";
		ScheduleDelayPercentEnd.labelWidth = 20;
		ScheduleDelayPercentEnd.width = 85;
		ScheduleDelayPercentEnd.x = 460;
		ScheduleDelayPercentEnd.y = 105;
		ScheduleDelayPercentEnd.disabled = disabled;
		items.push(ScheduleDelayPercentStart, ScheduleDelayPercentEnd);

		//第六行
		var MoreWorkDaysStart = me.createComboBox("超工作量天数从", "OverloadDays");
		MoreWorkDaysStart.itemId = "MoreWorkDaysStart";
		MoreWorkDaysStart.labelWidth = 105;
		MoreWorkDaysStart.width = 170;
		MoreWorkDaysStart.x = 5;
		MoreWorkDaysStart.y = 130;
		MoreWorkDaysStart.disabled = disabled;

		var MoreWorkDaysEnd = me.createComboBox("到", "OverloadDays");
		MoreWorkDaysEnd.itemId = "MoreWorkDaysEnd";
		MoreWorkDaysEnd.labelWidth = 20;
		MoreWorkDaysEnd.width = 85;
		MoreWorkDaysEnd.x = 175;
		MoreWorkDaysEnd.y = 130;
		MoreWorkDaysEnd.disabled = disabled;
		items.push(MoreWorkDaysStart, MoreWorkDaysEnd);

		var MoreWorkPercentStart = me.createComboBox("超工作量百分比从", "OverloadPercent");
		MoreWorkPercentStart.itemId = "MoreWorkPercentStart";
		MoreWorkPercentStart.labelWidth = 120;
		MoreWorkPercentStart.width = 185;
		MoreWorkPercentStart.x = 275;
		MoreWorkPercentStart.y = 130;
		MoreWorkPercentStart.disabled = disabled;

		var MoreWorkPercentEnd = me.createComboBox("到", "OverloadPercent");
		MoreWorkPercentEnd.itemId = "MoreWorkPercentEnd";
		MoreWorkPercentEnd.labelWidth = 20;
		MoreWorkPercentEnd.width = 85;
		MoreWorkPercentEnd.x = 460;
		MoreWorkPercentEnd.y = 130;
		MoreWorkPercentEnd.disabled = disabled;
		items.push(MoreWorkPercentStart, MoreWorkPercentEnd);

		//操作
		var buttons = me.createButtons();
		if(buttons) {
			items = items.concat(buttons);
		}

		return items;
	},
	/**创建功能按钮*/
	createButtons: function() {
		var me = this,
			items = [];
		items.push({
			x: 675,
			y: 130,
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onSearch();
			}
		}, {
			x: 740,
			y: 130,
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		});
		items.push({
			x: 805,
			y: 130,
			width: 60,
			iconCls: 'button-refresh',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '刷新',
			tooltip: '<b>刷新</b>',
			handler: function() {
				me.onSearch();
			}
		}, {
			x: 865,
			y: 130,
			width: 60,
			iconCls: 'button-add',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '新增',
			tooltip: '<b>新增</b>',
			handler: function() {
				me.onAddClick();
			}
		},{x: 938,width: 75,hidden:false,
			y: 130,xtype: 'button',text:'复制拷贝',tooltip:'复制拷贝',iconCls:'button-edit',
			handler:function(but,e){
				me.fireEvent('onCopyClick', me);
//				me.onCopyProject();
			}
		});

		return items;
	},
	/**新增*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.Form', {
			formtype: 'add',
			SUB_WIN_NO: '1',
			listeners: {
				isValid: function(p) {

				},
				save: function(form) {
					form.close();
					me.fireEvent('save', form, me);
				}
			}
		}).show();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.initClientListeners();

		//me.initCheckTriggerListeners('ProjectManager', 'ProjectManagerID');
		me.initCheckTriggerListeners('ProjectLeader', 'ProjectLeaderID');
		me.initCheckTriggerListeners('Principal', 'PrincipalID');
		me.initCheckTriggerListeners('PhaseManager', 'PhaseManagerID');
	},
	/**客户下拉框监听*/
	initClientListeners: function() {
		var me = this;
		var CName = me.getComponent('ClientName');
		var Id = me.getComponent('PClientID');
		if(!Id || !CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PClient_Name') : '');
				Id.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
	},
	/**人员下拉框监听*/
	initCheckTriggerListeners: function(cname, id) {
		var me = this;
		var comCname = me.getComponent(cname);
		var comId = me.getComponent(id);
		if(!comId || !comCname) return;
		comCname.on({
			check: function(p, record) {
				comId.setValue(record ? record.get("HREmployee_Id") : '');
				comCname.setValue(record ? record.get("HREmployee_CName") : '');
				p.close();
			}
		});
	},
	/**清空查询内容*/
	onClearSearch: function() {
		var me = this;
		var textList = ['BeginDate', 'EndDate', 'ProjectManager', 'ProjectLeader', 'ProjectLeaderID', 'Principal', 'PrincipalID', 'PhaseManager', 'PhaseManagerID'];
		var comboList = ["ContentID", "ProvinceID", "TypeID", "RiskLevelID", "DynamicRiskLevelID", "DelayLevelID", "PaceEvalIDStart", "PaceEvalIDEnd", "ScheduleDelayDaysStart", "ScheduleDelayDaysEnd", "ScheduleDelayPercentStart", "ScheduleDelayPercentEnd", "MoreWorkDaysStart", "MoreWorkDaysEnd", "MoreWorkPercentStart", "MoreWorkPercentEnd"];
		var checkList = ['ClientName'];

		for(var i in textList) {
			var text = me.getComponent(textList[i]);
			if(text) text.setValue('');
		}
		for(var i in comboList) {
			var combo = me.getComponent(comboList[i]);
			if(combo) combo.setValue(null);
		}
		for(var i in checkList) {
			var check = me.getComponent(checkList[i]);
			var check_Id = me.getComponent(checkList[i].split('_')[0] + '_Id');
			if(check) check.setValue('');
			if(check_Id) check_Id.setValue('');
		}
	},
	/**查询处理*/
	onSearch: function() {
		var me = this;
		var params = [];

		var BeginDate = me.getComponent('BeginDate').getValue();
		var EndDate = me.getComponent('EndDate').getValue();
		var StartDateValue = JcallShell.Date.toString(BeginDate, true);
		var EndDateValue = JcallShell.Date.toString(EndDate, true);
		if(StartDateValue > EndDateValue) {
			JShell.Msg.alert('进场结束日期不能小于进场开始日期!', null, 1000);
			return;
		}
		//下拉范围选择项验证及查询参数处理
		params = me.getSearchIdStrParam('PaceEvalID', '完成度', 'PaceEvalIDStart', 'PaceEvalIDEnd', params);

//		params = me.getSearchIdStrParam('ScheduleDelayDays', '进度延期天数', 'ScheduleDelayDaysStart', 'ScheduleDelayDaysEnd', params);
//		params = me.getSearchIdStrParam('ScheduleDelayPercent', '进度延期百分比', 'ScheduleDelayPercentStart', 'ScheduleDelayPercentEnd', params);
//		params = me.getSearchIdStrParam('MoreWorkDays', '超工作量天数', 'MoreWorkDaysStart', 'MoreWorkDaysEnd', params);
//		params = me.getSearchIdStrParam('MoreWorkPercent', '超工作量百分比', 'MoreWorkPercentStart', 'MoreWorkPercentEnd', params);

		params = me.getParams(params);
		me.fireEvent('onSearchClick', me, params);
	},
	/**
	 * 获取单个范围选择项的查询参数
	 * */
	getSearchIdStrParam: function(searchId, searchInfo, startItemId, endItemId, params) {
		var me = this;
		var IDStart = null,
			IDEnd = null,
			IDStartValue = null,
			IDEndValue = null;

		IDStart = me.getComponent(startItemId);
		IDEnd = me.getComponent(endItemId);
		IDStartValue = IDStart.getValue();
		IDEndValue = IDEnd.getValue();

		if(IDStartValue && IDEndValue) {
			var startIndex = -1,
				endIndex = -1;
			startIndex = IDStart.store.find('PDict_Id', IDStartValue);
			endIndex = IDEnd.store.find('PDict_Id', IDEndValue);
			if(endIndex < startIndex) {
				JShell.Msg.alert(searchInfo + '范围的结束不能小于开始!', null, 2000);
				return;
			} else {
				var records = IDStart.store.getRange(startIndex, endIndex);
				var idStr = "";
				if(records) {
					Ext.Array.each(records, function(record) {
						idStr += (record.get('PDict_Id') + ",");
					});
				}
				if(idStr && idStr != "" && idStr.length > 0) {
					idStr = idStr.substring(0, idStr.length - 1);
					params.push(searchId + " in (" + idStr + ")");
				}
			}
		}
		return params;
	},
	/**获取参数*/
	getParams: function(params) {
		var me = this;
		//var params = [];
		if(!params) params = [];
		var BeginDate = me.getComponent('BeginDate').getValue();
		var EndDate = me.getComponent('EndDate').getValue();

		var ContentID = me.getComponent('ContentID').getValue();
		var ProvinceID = me.getComponent('ProvinceID').getValue();
		var TypeID = me.getComponent('TypeID').getValue();
		var PClientID = me.getComponent('PClientID').getValue();

		//var ProjectManagerID = me.getComponent('ProjectManagerID').getValue();
		var ProjectLeaderID = me.getComponent('ProjectLeaderID').getValue();
		var PrincipalID = me.getComponent('PrincipalID').getValue();
		var PhaseManagerID = me.getComponent('PhaseManagerID').getValue();

		var RiskLevelID = me.getComponent('RiskLevelID').getValue();
		var DynamicRiskLevelID = me.getComponent('DynamicRiskLevelID').getValue();
		var DelayLevelID = me.getComponent('DelayLevelID').getValue();

		if(BeginDate && BeginDate != "" && BeginDate.length > 0) {
			params.push("EntryTime" + ">='" + JShell.Date.toString(BeginDate, true) + "'");
		}
		if(EndDate && EndDate != "" && EndDate.length > 0) {
			params.push("EntryTime" + "<'" + JShell.Date.toString(EndDate, true) + "  23:59:59'");
		}
		if(ContentID && ContentID != "" && ContentID.length > 0) {
			params.push("ContentID in(" + ContentID + ")");
		}
		if(ProvinceID && ProvinceID != "" && ProvinceID.length > 0) {
			params.push("ProvinceID in(" + ProvinceID + ")");
		}
		if(TypeID && TypeID != "" && TypeID.length > 0) {
			params.push("TypeID in(" + TypeID + ")");
		}
		if(PClientID && PClientID != "" && PClientID.length > 0) {
			params.push("PClientID in(" + PClientID + ")");
		}
		//第三行
		if(ProjectLeaderID && ProjectLeaderID != "" && ProjectLeaderID.length > 0) {
			params.push("ProjectLeaderID=" + ProjectLeaderID + "");
		}
		if(PrincipalID && PrincipalID != "" && PrincipalID.length > 0) {
			params.push("PrincipalID=" + PrincipalID + "");
		}
		if(PhaseManagerID && PhaseManagerID != "" && PhaseManagerID.length > 0) {
			params.push("PhaseManagerID=" + PhaseManagerID + "");
		}
		//第四行
		if(RiskLevelID && RiskLevelID != "" && RiskLevelID.length > 0) {
			params.push("RiskLevelID in(" + RiskLevelID + ")");
		}
		if(DynamicRiskLevelID && DynamicRiskLevelID != "" && DynamicRiskLevelID.length > 0) {
			params.push("DynamicRiskLevelID in(" + DynamicRiskLevelID + ")");
		}
		if(DelayLevelID && DelayLevelID != "" && DelayLevelID.length > 0) {
			params.push("DelayLevelID in(" + DelayLevelID + ")");
		}
		return params;
	},

	/**第二行的查询按钮的查询处理*/
	onBtnSearch: function(btnType, days) {
		var me = this;
		var params = [];
		var searchParam = "";
		switch(btnType) {
			case 1:
				searchParam = me.getSearchEntryTimeParam(days);
				break;
			case 2:
				searchParam = me.getSearchEstiEndTimeParam(days);
				break;
			default:
				break;
		}
		if(searchParam.length > 0) params.push(searchParam);
		if(params.length > 0) {
			me.fireEvent('onSearchClick', me, params);
		}
	},

	/**
	 * 获取已进场天数按钮的查询条件
	 * (今天-进场时间>=2 and 今天-进场时间<=5) and 项目完成度!=100%
	 * */
	getSearchEntryTimeParam: function(days) {
		var me = this;
		var searchParam = "";
		var Sysdate = JcallShell.System.Date.getDate();
		if(Sysdate == null) Sysdate = new Date();

		var BeginDate = null,
			EndDate = null;
		EndDate = JcallShell.Date.getNextDate(Sysdate, -days);

		switch(days) {
			case 2:
				BeginDate = JcallShell.Date.getNextDate(Sysdate, -5);
				break;
			case 5:
				BeginDate = JcallShell.Date.getNextDate(Sysdate, -10);
				break;
			case 10:
				BeginDate = JcallShell.Date.getNextDate(Sysdate, -15);
				break;
			case 15:
				BeginDate = JcallShell.Date.getNextDate(Sysdate, -25);
				break;
			case 25:
				BeginDate = JcallShell.Date.getNextDate(Sysdate, -30);
				break;
			case 30:
				break;
			default:
				break;
		}
		if(BeginDate) BeginDate = JcallShell.Date.toString(BeginDate, true);
		if(EndDate) EndDate = JcallShell.Date.toString(EndDate, true);
		if(BeginDate) {
			searchParam = "EntryTime" + ">='" + BeginDate + "'";
		}
		if(EndDate) {
			if(EndDate) EndDate = EndDate + "  23:59:59";
			searchParam = searchParam + " and EntryTime" + "<'" + EndDate + "'";
		}
		if(searchParam) searchParam = searchParam + " and PaceEvalID!=4686690116723555688";
		return searchParam;
	},
	/**
	 * 获取计划多少天内验收按钮的查询条件
	 * 计划多少天内验收:(计划完成时间-今天<=天数) and 项目完成度!=100%
	 * */
	getSearchEstiEndTimeParam: function(days) {
		var me = this;
		var searchParam = "";
		var Sysdate = JcallShell.System.Date.getDate();
		if(Sysdate == null) Sysdate = new Date();

		var BeginDate = null,
			EndDate = null;
		BeginDate = Sysdate;
		EndDate = JcallShell.Date.getNextDate(Sysdate, (days - 1));

		BeginDate = JcallShell.Date.toString(BeginDate, true);
		EndDate = JcallShell.Date.toString(EndDate, true);

		if(BeginDate && EndDate) {
			EndDate = EndDate + "  23:59:59";
			searchParam = "(EstiEndTime" + ">='" + BeginDate + "'";
			searchParam = searchParam + " and EstiEndTime" + "<'" + EndDate + "')";
			searchParam = searchParam + " and PaceEvalID!=4686690116723555688";
		}
		return searchParam;
	},
	/**创建下拉*/
	createComboBox: function(fieldLabel, dictTypeCode) {
		var me = this;
		var fields = "",
			selectUrl = "",
			displayField = 'PDict_CName',
			valueField = 'PDict_Id',
			defaultOrderBy = [{ property: 'PDict_DispOrder', direction: 'ASC' }],
			where = "";
		var storeFields = ['PDict_CName', 'PDict_Id', 'PDict_DispOrder'];
		switch(dictTypeCode) {
			case "ProvinceID":
				displayField = 'BProvince_Name';
				valueField = 'BProvince_Id';
				fields = "BProvince_Id,BProvince_Name";
				storeFields = ['BProvince_Id', 'BProvince_Name'];
				where = "bprovince.BCountry.Id='5742820397511247346'";
				selectUrl = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true';
				selectUrl = selectUrl + "&fields=" + fields;
				selectUrl = selectUrl + "&where=" + where;
				defaultOrderBy = [{ property: 'BProvince_Shortcode', direction: 'ASC' }];
				break;
			default:
				fields = "PDict_CName,PDict_Id,PDict_DispOrder";
				selectUrl = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true';
				selectUrl = selectUrl + "&fields=" + fields;
				where = "pdict.IsUse=1 and pdict.PDictType.DictTypeCode='" + dictTypeCode + "'";
				selectUrl = selectUrl + "&where=" + where;
				break;
		}
		var multiCombo = Ext.create('Ext.form.field.ComboBox', {
			fieldLabel: fieldLabel,
			//multiSelect: true,
			displayField: displayField,
			valueField: valueField,
			width: 180,
			labelWidth: 65,
			labelAlign: 'right',
			//mode: 'local',
			//queryMode: 'remote',//
			store: new Ext.data.Store({
				fields: storeFields,
				pageSize: 5000,
				sorters: defaultOrderBy,
				remoteSort: true,
				proxy: {
					type: 'ajax',
					url: selectUrl + "&t=" + (new Date()).getTime(),
					timeout: 30000,
					autoLoad: true,
					reader: {
						type: 'json',
						root: 'list'
					},
					extractResponseData: function(response) {
						var data = JShell.Server.toJson(response.responseText);
						if(data.success) {
							var info = data.value;
							if(info) {
								var type = Ext.typeOf(info);
								if(type == 'object') {
									info = info;
								} else if(type == 'array') {
									info.list = info;
									info.count = info.list.length;
								} else {
									info = {};
								}

								data.count = info.count || 0;
								data.list = info.list || [];
							} else {
								data.count = 0;
								data.list = [];
							}
							//data = me.changeResult(data);
						} else {
							me.errorInfo = data.msg;
						}
						response.responseText = Ext.JSON.encode(data);
						return response;
					}
				}
			})
		});
		return multiCombo;
	}
});