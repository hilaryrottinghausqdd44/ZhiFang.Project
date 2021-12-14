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
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',

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
		columns: 5 //每行有几列
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

	/**客户端验收单状态Key*/
	StatusKey: "ReaBmsCenSaleDocConfirmStatus",
	/**供货单数据来源Key*/
	SourceTypeKey: "ReaBmsCenSaleDocSource",
	/**供货单数据标志Key*/
	IOFlagKeyKey: "ReaBmsCenSaleDocIOFlag",
	
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

		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.SourceTypeKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);
		
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供货商
		items.push({
			fieldLabel: '供货商',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
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
			name: 'ReaBmsCenSaleDocConfirm_DataAddTime',
			itemId: 'ReaBmsCenSaleDocConfirm_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//数据来源(供单ID和订单ID)
		items.push({
			fieldLabel: '数据来源',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenSaleDocConfirm_SourceType',
			data: JShell.REA.StatusList.Status[me.SourceTypeKey].List,
			hasStyle: true,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenSaleDocConfirm_Status',
			itemId: 'ReaBmsCenSaleDocConfirm_Status',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			//value: me.Status,
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			readOnly: true,
			locked: true
		});
		//供货单号
		items.push({
			fieldLabel: '供货单号',
			name: 'ReaBmsCenSaleDocConfirm_SaleDocNo',
			readOnly: true,
			locked: true,
			colspan: 2,
			width: me.defaults.width * 2
		});
		//主验收人
		items.push({
			fieldLabel: '主验收人',
			name: 'ReaBmsCenSaleDocConfirm_AccepterName',
			itemId: 'ReaBmsCenSaleDocConfirm_AccepterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '主验收人ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_AccepterID',
			itemId: 'ReaBmsCenSaleDocConfirm_AccepterID'
		});

		//次验收人
		items.push({
			fieldLabel: '次验收人',
			name: 'ReaBmsCenSaleDocConfirm_SecAccepterName',
			itemId: 'ReaBmsCenSaleDocConfirm_SecAccepterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '次验收人ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_SecAccepterID',
			itemId: 'ReaBmsCenSaleDocConfirm_SecAccepterID'
		});
		//总单金额
		items.push({
			fieldLabel: '总单金额',
			name: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			itemId: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//货运单号
		items.push({
			xtype: 'textarea',
			fieldLabel: '货运单号',
			name: 'ReaBmsCenSaleDocConfirm_TransportNo',
			itemId: 'ReaBmsCenSaleDocConfirm_TransportNo',
			colspan: 2,
			width: me.defaults.width*2,
			height: 20
		});
		//发票号
		items.push({
			//xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsCenSaleDocConfirm_InvoiceNo',
			itemId: 'ReaBmsCenSaleDocConfirm_InvoiceNo',
			colspan: 2,
			width: me.defaults.width*2,
			height: 20
		});
		
		//验收时间
		items.push({
			xtype: 'datefield',
			fieldLabel: '验收时间',
			format: 'Y-m-d',
			name: 'ReaBmsCenSaleDocConfirm_AcceptTime',
			itemId: 'ReaBmsCenSaleDocConfirm_AcceptTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCenSaleDocConfirm_Memo',
			itemId: 'ReaBmsCenSaleDocConfirm_Memo',
			colspan: 5,
			width: me.defaults.width * 5,
			height: 50
		});
		//验收单号
		items.push({
			fieldLabel: '验收单号',
			name: 'ReaBmsCenSaleDocConfirm_SaleDocConfirmNo',
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
			name: 'ReaBmsCenSaleDocConfirm_SecAcceptTime',
			itemId: 'ReaBmsCenSaleDocConfirm_SecAcceptTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsCenSaleDocConfirm_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '供货商主键ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaCompID',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompID'
		}, {
			fieldLabel: '供应商机平台构码',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaServerCompCode',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaServerCompCode'
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

		var AccepterID = me.getComponent('ReaBmsCenSaleDocConfirm_AccepterID');
		var AccepterName = me.getComponent('ReaBmsCenSaleDocConfirm_AccepterName');
		AccepterID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		AccepterName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('ReaBmsCenSaleDocConfirm_DataAddTime');
		DataAddTime.setValue(curDateTime);
		var AcceptTime = me.getComponent('ReaBmsCenSaleDocConfirm_AcceptTime');
		AcceptTime.setValue(curDateTime);
	},
	/**订货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompID');
		var ComName = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompanyName');
		var ReaServerCompCode = me.getComponent('ReaBmsCenSaleDocConfirm_ReaServerCompCode');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		ComId.setValue(id);
		ComName.setValue(text);
		ReaServerCompCode.setValue(platformOrgNo);
		var objValue = {
			"ReaCompID": id,
			"ReaCompCName": text,
			"PlatformOrgNo": platformOrgNo
		};
		me.fireEvent('reacompcheck', me, objValue);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.ReaBmsCenSaleDocConfirm_DataAddTime) data.ReaBmsCenSaleDocConfirm_DataAddTime = JcallShell.Date.toString(data.ReaBmsCenSaleDocConfirm_DataAddTime, true);
		if(data.ReaBmsCenSaleDocConfirm_AcceptTime) data.ReaBmsCenSaleDocConfirm_AcceptTime = JcallShell.Date.toString(data.ReaBmsCenSaleDocConfirm_AcceptTime, true);
		if(data.ReaBmsCenSaleDocConfirm_SecAcceptTime) data.ReaBmsCenSaleDocConfirm_SecAcceptTime = JcallShell.Date.toString(data.ReaBmsCenSaleDocConfirm_SecAcceptTime, true);
		
		var reg = new RegExp("<br />", "g");
		data.ReaBmsCenSaleDocConfirm_Memo = data.ReaBmsCenSaleDocConfirm_Memo.replace(reg, "\r\n");
		
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			SaleDocNo: values.ReaBmsCenSaleDocConfirm_SaleDocNo,
			SaleDocConfirmNo: values.ReaBmsCenSaleDocConfirm_SaleDocConfirmNo,
			ReaCompanyName: values.ReaBmsCenSaleDocConfirm_ReaCompanyName,
			ReaServerCompCode: values.ReaBmsCenSaleDocConfirm_ReaServerCompCode,
			Status: values.ReaBmsCenSaleDocConfirm_Status,
			AccepterName: values.ReaBmsCenSaleDocConfirm_AccepterName,
			DeleteFlag: 0,
			TransportNo:values.ReaBmsCenSaleDocConfirm_TransportNo,
			InvoiceNo:values.ReaBmsCenSaleDocConfirm_InvoiceNo
		};
		entity.Memo = values.ReaBmsCenSaleDocConfirm_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		if(values.ReaBmsCenSaleDocConfirm_ReaCompID) entity.ReaCompID = values.ReaBmsCenSaleDocConfirm_ReaCompID;
		if(values.ReaBmsCenSaleDocConfirm_AccepterID) entity.AccepterID = values.ReaBmsCenSaleDocConfirm_AccepterID;

		if(values.ReaBmsCenSaleDocConfirm_DataAddTime) entity.DataAddTime = JShell.Date.toServerDate(values.ReaBmsCenSaleDocConfirm_DataAddTime);
		if(values.ReaBmsCenSaleDocConfirm_AcceptTime) entity.AcceptTime = JShell.Date.toServerDate(values.ReaBmsCenSaleDocConfirm_AcceptTime);
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
			'Id', 'SaleDocConfirmNo', 'Status', 'ReaCompID', 'ReaCompanyName', 'ReaServerCompCode', 'Memo','TransportNo','InvoiceNo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsCenSaleDocConfirm_Id;
		return entity;
	}
});