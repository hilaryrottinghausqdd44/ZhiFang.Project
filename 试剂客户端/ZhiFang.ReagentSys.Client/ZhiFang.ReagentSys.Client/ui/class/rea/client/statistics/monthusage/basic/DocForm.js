/**
 * 出库使用量统计
 * @author longfc
 * @version 2018-10-24
 */
Ext.define('Shell.class.rea.client.statistics.monthusage.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.YearAndMonthComboBox'
	],
	title: '出库使用量统计',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDocById?isPlanish=true',

	buttonDock: "top",
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**内容周围距离*/
	bodyPadding: '5px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},

	/**月结最小年份*/
	minYearValue: 2018,
	/**月结最大年份*/
	maxYearValue: 2018,
	/**月结最小选择项*/
	roundMinValue: null,
	/**月结最大选择项*/
	roundMaxValue: null,
	/**统计类型*/
	TypeIDKey: "ReaMonthUsageStatisticsDocType",
	/**周期类型*/
	RoundTypeKey: "ReaMonthUsageStatisticsDocRoundType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 185) me.defaults.width = 185;

		JShell.REA.StatusList.getStatusList(me.TypeIDKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.RoundTypeKey, false, true, null);

		var Sysdate = JcallShell.System.Date.getDate();
		me.maxYearValue = Sysdate.getFullYear();
		me.roundMaxValue = Ext.util.Format.date(Sysdate, "Y-m");
		me.roundMinValue = me.minYearValue + "-01";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		var typeIDList = JShell.REA.StatusList.Status["ReaMonthUsageStatisticsDocType"];
		if(typeIDList) {
			typeIDList = typeIDList.List;
		} else {
			typeIDList = [];
		}
		var roundTypeList = JShell.REA.StatusList.Status["ReaMonthUsageStatisticsDocRoundType"];
		if(roundTypeList) {
			roundTypeList = roundTypeList.List;
		} else {
			roundTypeList = [];
		}
		//统计类型
		items.push({
			fieldLabel: '统计类型',
			xtype: 'uxSimpleComboBox',
			name: 'ReaMonthUsageStatisticsDoc_TypeID',
			itemId: 'ReaMonthUsageStatisticsDoc_TypeID',
			hasStyle: true,
			data: typeIDList,
			colspan: 1,
			width: me.defaults.width * 1,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(com, newValue,oldValue, eOpts) {
					me.onTypeChange(newValue);
				}
			}
		});
		//周期类型
		items.push({
			fieldLabel: '周期类型',
			xtype: 'uxSimpleComboBox',
			name: 'ReaMonthUsageStatisticsDoc_RoundTypeId',
			itemId: 'ReaMonthUsageStatisticsDoc_RoundTypeId',
			hasStyle: true,
			data: roundTypeList,
			colspan: 1,
			width: me.defaults.width * 1,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(com, newValue,oldValue, eOpts) {
					me.onRoundTypeChange(newValue);
				}
			}
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaMonthUsageStatisticsDoc_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//统计周期
		items.push({
			fieldLabel: '统计周期',
			name: 'ReaMonthUsageStatisticsDoc_Round',
			itemId: 'ReaMonthUsageStatisticsDoc_Round',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxYearAndMonthComboBox',
			minYearValue: me.minYearValue,
			maxYearValue: me.maxYearValue,
			minValue: me.roundMinValue,
			maxValue: me.roundMaxValue,
			listeners: {
				change: function(com, newValue,oldValue, eOpts) {
					//me.onTypeChange(newValue);
				}
			}
		}, {
			xtype: 'datefield',
			fieldLabel: '起始日期',
			format: 'Y-m-d',// H:m:s
			name: 'ReaMonthUsageStatisticsDoc_StartDate',
			itemId: 'ReaMonthUsageStatisticsDoc_StartDate',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'datefield',
			fieldLabel: '结束日期',
			format: 'Y-m-d',// H:m:s
			name: 'ReaMonthUsageStatisticsDoc_EndDate',
			itemId: 'ReaMonthUsageStatisticsDoc_EndDate',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//根据登录者的部门id 查询
		var depID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		items.push({
			fieldLabel: '部门选择',
			emptyText: '部门选择',
			name: 'ReaMonthUsageStatisticsDoc_DeptName',
			itemId: 'ReaMonthUsageStatisticsDoc_DeptName',
			colspan: 1,
			width: me.defaults.width * 1,
			snotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			value: me.ParentName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.CheckOrgTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**显示所有部门树:false;只显示用户自己的树:true*/
					ISOWN: true,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onDeptAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '部门主键ID',
			hidden: true,
			name: 'ReaMonthUsageStatisticsDoc_DeptID',
			itemId: 'ReaMonthUsageStatisticsDoc_DeptID'
		});
		//操作者
		items.push({
			fieldLabel: '操作人',
			name: 'ReaMonthUsageStatisticsDoc_CreaterName',
			itemId: 'ReaMonthUsageStatisticsDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		//统计单号
		items.push({
			fieldLabel: '统计单号',
			name: 'ReaMonthUsageStatisticsDoc_DocNo',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaMonthUsageStatisticsDoc_Id',
			hidden: true,
			type: 'key'
		});

		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaMonthUsageStatisticsDoc_Memo',
			itemId: 'ReaMonthUsageStatisticsDoc_Memo',
			colspan: 3,
			width: me.defaults.width * 3,
			height: 38
		});
		return items;
	},
	/**部门选择*/
	onDeptAccept: function(record) {
		var me = this,
			DeptID = me.getComponent('ReaMonthUsageStatisticsDoc_DeptID'),
			DeptName = me.getComponent('ReaMonthUsageStatisticsDoc_DeptName');
		var text = record.get('text') || '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(record.get('tid') || '');
		DeptName.setValue(text);
		me.fireEvent('hrdptcheck', me, record);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {

		var DataAddTime = data.ReaMonthUsageStatisticsDoc_DataAddTime;
		if(DataAddTime) data.ReaMonthUsageStatisticsDoc_DataAddTime = Ext.util.Format.date(DataAddTime, "Y-m-d H:m:s");

		var StartDate = data.ReaMonthUsageStatisticsDoc_StartDate;
		var EndDate = data.ReaMonthUsageStatisticsDoc_EndDate;
		if(StartDate) data.ReaMonthUsageStatisticsDoc_StartDate = Ext.util.Format.date(StartDate, "Y-m-d");// H:m:s
		if(EndDate) data.ReaMonthUsageStatisticsDoc_EndDate = Ext.util.Format.date(EndDate, "Y-m-d");// H:m:s

		var reg = new RegExp("<br />", "g");
		data.ReaMonthUsageStatisticsDoc_Memo = data.ReaMonthUsageStatisticsDoc_Memo.replace(reg, "\r\n");
		var visible = data.ReaMonthUsageStatisticsDoc_Visible;
		if(visible == "1" || visible == 1 || visible == "true" || visible == true) visible = true;
		else visible = false;
		data.ReaMonthUsageStatisticsDoc_Visible = visible;
		return data;
	},
	/**@description 统计类型选择后处理*/
	onTypeChange: function(newValue) {
		var me = this;
		var deptID = me.getComponent('ReaMonthUsageStatisticsDoc_DeptID');
		var deptName = me.getComponent('ReaMonthUsageStatisticsDoc_DeptName');
		//按使用部门
		if(""+newValue == "2") {
			deptName.allowBlank = false;
			deptName.locked = false;
			var deptIDValue = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
			var deptNameValue = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
			deptID.setValue(deptIDValue);
			deptName.setValue(deptNameValue);
			deptName.setReadOnly(false);	
		} else {
			deptName.allowBlank = true;
			deptName.locked = true;
			deptID.setValue("");
			deptName.setValue("");
			deptName.setReadOnly(true);
		}
	},
	/****@description周期类型选择后联动*/
	onRoundTypeChange: function(newValue) {
		var me = this;
		var round = me.getComponent('ReaMonthUsageStatisticsDoc_Round');
		var startDate = me.getComponent('ReaMonthUsageStatisticsDoc_StartDate');
		var endDate = me.getComponent('ReaMonthUsageStatisticsDoc_EndDate');
		//按自然月
		if(""+newValue== "1") {
			round.allowBlank = false;
			round.locked = false;
			round.setReadOnly(false);
			
			startDate.allowBlank = true;
			startDate.locked = true;
			startDate.setReadOnly(true);
			
			endDate.allowBlank = true;
			endDate.locked = true;
			endDate.setReadOnly(true);
		} else {
			round.locked = true;
			round.setReadOnly(true);			
			round.allowBlank = true;
			round.setValue('');
			
			startDate.allowBlank = false;
			startDate.locked = false;
			startDate.setReadOnly(false);	
			
			endDate.allowBlank = false;
			endDate.locked = false;
			endDate.setReadOnly(false);	
		}
	},
	/**统计周期选择*/
	onRoundChange: function(newValue) {
		var me = this;
	}
});