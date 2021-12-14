/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '验货单信息',

	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfManualInput',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfManualInput',
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
	Status: "1",
	/**申请单当前中文名称状态*/
	StatusName: "",
	StatusList: [],
	SourceTypeList: [],
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
		me.getSourceTypeListData();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供货方
		items.push({
			fieldLabel: '供货方',
			emptyText: '必填项',
			allowBlank: false,
			name: 'BmsCenSaleDocConfirm_ReaCompName',
			itemId: 'BmsCenSaleDocConfirm_ReaCompName',
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
		});
		items.push({
			xtype: 'datefield',
			fieldLabel: '加入时间',
			format: 'Y-m-d',
			name: 'BmsCenSaleDocConfirm_DataAddTime',
			itemId: 'BmsCenSaleDocConfirm_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'BmsCenSaleDocConfirm_Status',
			itemId: 'BmsCenSaleDocConfirm_Status',
			hasStyle: true,
			data: me.StatusList,
			value: me.Status,
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			readOnly: true,
			locked: true
		});
		//供货单号
		items.push({
			fieldLabel: '供货单号',
			name: 'BmsCenSaleDocConfirm_SaleDocNo',
			readOnly: true,
			locked: true,
			colspan: 2,
			width: me.defaults.width * 2
		});
		//数据来源(供单ID和订单ID)
		items.push({
			fieldLabel: '数据来源',
			xtype: 'uxSimpleComboBox',
			name: 'BmsCenSaleDocConfirm_SourceType',
			data: me.SourceTypeList,
			hasStyle: true,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//验收时间
		items.push({
			xtype: 'datefield',
			fieldLabel: '验收时间',
			format: 'Y-m-d',
			name: 'BmsCenSaleDocConfirm_AcceptTime',
			itemId: 'BmsCenSaleDocConfirm_AcceptTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		//主验收人
		items.push({
			fieldLabel: '主验收人',
			name: 'BmsCenSaleDocConfirm_AccepterName',
			itemId: 'BmsCenSaleDocConfirm_AccepterName',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '主验收人ID',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_AccepterID',
			itemId: 'BmsCenSaleDocConfirm_AccepterID'
		});

		//次验收人
		items.push({
			fieldLabel: '次验收人',
			name: 'BmsCenSaleDocConfirm_SecAccepterName',
			itemId: 'BmsCenSaleDocConfirm_SecAccepterName',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '次验收人ID',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_SecAccepterID',
			itemId: 'BmsCenSaleDocConfirm_SecAccepterID'
		});

		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'BmsCenSaleDocConfirm_Memo',
			itemId: 'BmsCenSaleDocConfirm_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 50
		});
		//验收单号
		items.push({
			fieldLabel: '验收单号',
			name: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//次验收时间
		items.push({
			xtype: 'datefield',
			hidden: true,
			fieldLabel: '次验收时间',
			format: 'Y-m-d',
			name: 'BmsCenSaleDocConfirm_SecAcceptTime',
			itemId: 'BmsCenSaleDocConfirm_SecAcceptTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'BmsCenSaleDocConfirm_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_ReaCompID',
			itemId: 'BmsCenSaleDocConfirm_ReaCompID'
		});
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var AccepterID = me.getComponent('BmsCenSaleDocConfirm_AccepterID');
		var AccepterName = me.getComponent('BmsCenSaleDocConfirm_AccepterName');
		AccepterID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		AccepterName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('BmsCenSaleDocConfirm_DataAddTime');
		DataAddTime.setValue(curDateTime);
		var AcceptTime = me.getComponent('BmsCenSaleDocConfirm_AcceptTime');
		AcceptTime.setValue(curDateTime);
	},
	/**订货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('BmsCenSaleDocConfirm_ReaCompID');
		var ComName = me.getComponent('BmsCenSaleDocConfirm_ReaCompName');

		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		me.fireEvent('reacompcheck', me, record);
	},
	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "BmsCenSaleDocConfirmStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		});
		me.StatusList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].BmsCenSaleDocConfirmStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].BmsCenSaleDocConfirmStatus, function(obj, index) {
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
	/**获取数据来源信息*/
	getSourceTypeListData: function(callback) {
		var me = this;
		if(me.SourceTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "BmsCenSaleDocConfirmSourceType",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		});
		me.SourceTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].BmsCenSaleDocConfirmSourceType.length > 0) {
						me.SourceTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].BmsCenSaleDocConfirmSourceType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.SourceTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.BmsCenSaleDocConfirm_DataAddTime) data.BmsCenSaleDocConfirm_DataAddTime = JcallShell.Date.toString(data.BmsCenSaleDocConfirm_DataAddTime, true);
		if(data.BmsCenSaleDocConfirm_AcceptTime) data.BmsCenSaleDocConfirm_AcceptTime = JcallShell.Date.toString(data.BmsCenSaleDocConfirm_AcceptTime, true);
		if(data.BmsCenSaleDocConfirm_SecAcceptTime) data.BmsCenSaleDocConfirm_SecAcceptTime = JcallShell.Date.toString(data.BmsCenSaleDocConfirm_SecAcceptTime, true);

		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			SaleDocConfirmNo: values.BmsCenSaleDocConfirm_SaleDocConfirmNo,
			ReaCompName: values.BmsCenSaleDocConfirm_ReaCompName,
			Memo: values.BmsCenSaleDocConfirm_Memo,
			Status: values.BmsCenSaleDocConfirm_Status,
			AccepterName: values.BmsCenSaleDocConfirm_AccepterName,
			DeleteFlag: 0
		};
		if(values.BmsCenSaleDocConfirm_ReaCompID) entity.ReaCompID = values.BmsCenSaleDocConfirm_ReaCompID;
		if(values.BmsCenSaleDocConfirm_AccepterID) entity.AccepterID = values.BmsCenSaleDocConfirm_AccepterID;

		if(values.BmsCenSaleDocConfirm_DataAddTime) entity.DataAddTime = JShell.Date.toServerDate(values.BmsCenSaleDocConfirm_DataAddTime);
		if(values.BmsCenSaleDocConfirm_AcceptTime) entity.AcceptTime = JShell.Date.toServerDate(values.BmsCenSaleDocConfirm_AcceptTime);
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
			'Id', 'SaleDocConfirmNo', 'Status', 'ReaCompID', 'ReaCompName', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.BmsCenSaleDocConfirm_Id;
		return entity;
	}
});