/**
 * 客户端订货总单列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '订单信息',

	width: 340,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**当前选择的供应商Id*/
	ReaCompID: null,
	/**申请单当前状态*/
	Status: '0',
	/**申请单当前中文名称状态*/
	StatusName: "",
	StatusList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('reacompcheck', 'isEditAfter');
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.getStatusListData();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'BmsCenOrderDoc_Id',
			hidden: true,
			type: 'key'
		});

		//供货方
		items.push({
			fieldLabel: '供货方',
			emptyText: '必填项',
			allowBlank: false,
			name: 'BmsCenOrderDoc_ReaCompName',
			itemId: 'BmsCenOrderDoc_ReaCompName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.order.CenOrgCheck', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'BmsCenOrderDoc_ReaCompID',
			itemId: 'BmsCenOrderDoc_ReaCompID'
		}, {
			fieldLabel: '供应商机平台构码',
			hidden: true,
			name: 'BmsCenOrderDoc_ReaServerCompCode',
			itemId: 'BmsCenOrderDoc_ReaServerCompCode'
		});
		//订货单号
		items.push({
			fieldLabel: '订货单号',
			name: 'BmsCenOrderDoc_OrderDocNo',
			colspan: 2,
			width: me.defaults.width * 2
		});

		//操作人员
		items.push({
			fieldLabel: '订购人员',
			name: 'BmsCenOrderDoc_UserName',
			itemId: 'BmsCenOrderDoc_UserName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			allowBlank: false,
			locked: true
		}, {
			fieldLabel: '订购人员ID',
			name: 'BmsCenOrderDoc_UserID',
			itemId: 'BmsCenOrderDoc_UserID',
			hidden: true
		});
		//订购日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '申请日期',
			format: 'Y-m-d', // H:m:s
			name: 'BmsCenOrderDoc_OperDate',
			itemId: 'BmsCenOrderDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false
		});
		//紧急标志
		items.push({
			fieldLabel: '紧急标志',
			xtype: 'uxSimpleComboBox',
			name: 'BmsCenOrderDoc_UrgentFlag',
			itemId: 'BmsCenOrderDoc_UrgentFlag',
			data: JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_UrgentFlag'),
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			value: '0'
		});
		items.push({
			fieldLabel: '是否启用',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'BmsCenOrderDoc_DeleteFlag',
			itemId: 'BmsCenOrderDoc_DeleteFlag',
			data: [
				[0, "启用"],
				[1, "禁用"]
			],
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'BmsCenOrderDoc_Memo',
			itemId: 'BmsCenOrderDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 40
		});
		//单据状态
		items.push({
			xtype: 'uxSimpleComboBox',
			fieldLabel: '单据状态',
			name: 'BmsCenOrderDoc_Status',
			itemId: 'BmsCenOrderDoc_Status',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			hasStyle: true,
			border: false,
			data: me.StatusList,
			value: me.Status
		});

		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人员',
			name: 'BmsCenOrderDoc_Checker',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '审核时间',
			name: 'BmsCenOrderDoc_CheckTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '总价',
			itemId: 'BmsCenOrderDoc_TotalPrice',
			name: 'BmsCenOrderDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核意见',
			itemId: 'BmsCenOrderDoc_CheckMemo',
			name: 'BmsCenOrderDoc_CheckMemo',
			colspan: 4,
			width: me.defaults.width * 4
		});
		return items;
	},

	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsOrderDocStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		});
		me.StatusList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsOrderDocStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsOrderDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('BmsCenOrderDoc_ReaCompID');
		var ComName = me.getComponent('BmsCenOrderDoc_ReaCompName');
		var ReaServerCompCode = me.getComponent('BmsCenOrderDoc_ReaServerCompCode');

		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		ReaServerCompCode.setValue(record ? record.get('ReaCenOrg_CenterOrgNo') : '');
		me.fireEvent('reacompcheck', me, record);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var BmsCenOrderDoc_UserID = me.getComponent('BmsCenOrderDoc_UserID');
		var BmsCenOrderDoc_UserName = me.getComponent('BmsCenOrderDoc_UserName');
		BmsCenOrderDoc_UserID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		BmsCenOrderDoc_UserName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var BmsCenOrderDoc_OperDate = me.getComponent('BmsCenOrderDoc_OperDate');
		BmsCenOrderDoc_OperDate.setValue(new Date());
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var DeleteFlag = data.BmsCenOrderDoc_DeleteFlag;
		if(DeleteFlag == "true" || DeleteFlag == true) DeleteFlag = '1';
		DeleteFlag = (DeleteFlag == '1' ? 1 : 0);
		data.BmsCenOrderDoc_DeleteFlag = DeleteFlag;
		if(data.BmsCenOrderDoc_OperDate) data.BmsCenOrderDoc_OperDate = JcallShell.Date.toString(data.BmsCenOrderDoc_OperDate, true);
		if(data.BmsCenOrderDoc_ConfirmTime) data.BmsCenOrderDoc_ConfirmTime = JcallShell.Date.toString(data.BmsCenOrderDoc_ConfirmTime, true);
		if(data.BmsCenOrderDoc_CheckTime) data.BmsCenOrderDoc_CheckTime = JcallShell.Date.toString(data.BmsCenOrderDoc_CheckTime, true);
		return data;
	},
	/**@description 获取保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var entity = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!me.saveParams) {
			me.saveParams = {
				entity: entity.entity,
				"dtAddList": []
			}
		}
		me.saveParams.entity = entity.entity;
		if(me.formtype == 'edit') {
			me.saveParams.fields = entity.fields;
		}
		return me.saveParams;
	},
	/**@description 保存前的提交参数再次处理*/
	setSaveParams: function(params) {
		var me = this;
		if(!me.saveParams) {
			me.saveParams = {
				"entity": null,
				"dtAddList": []
			}
		}
		me.saveParams = params;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			OrderDocNo: values.BmsCenOrderDoc_OrderDocNo,
			ReaServerCompCode: values.BmsCenOrderDoc_ReaServerCompCode,
			ReaCompName: values.BmsCenOrderDoc_ReaCompName,
			Memo: values.BmsCenOrderDoc_Memo,
			Status: values.BmsCenOrderDoc_Status,
			UrgentFlag: values.BmsCenOrderDoc_UrgentFlag,
			DeleteFlag: values.BmsCenOrderDoc_DeleteFlag
		};
		if(values.BmsCenOrderDoc_ReaCompID) entity.ReaCompID = values.BmsCenOrderDoc_ReaCompID;
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		var applyTime = curDateTime;
		if(values.BmsCenOrderDoc_OperDate) {
			applyTime = values.BmsCenOrderDoc_OperDate;
			if(JShell.Date.toServerDate(applyTime)) {
				entity.OperDate = JShell.Date.toServerDate(applyTime);
			}
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'OrderDocNo', 'ReaCompName', 'Memo', 'DeleteFlag',
			'OperDate', 'Status', 'UrgentFlag', 'ReaCompID', 'ReaServerCompCode'
		];
		entity.fields = fields.join(',');

		entity.entity.Id = values.BmsCenOrderDoc_Id;
		return entity;
	}
});