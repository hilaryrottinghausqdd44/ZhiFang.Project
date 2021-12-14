/**
 * 发票邮寄
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.invoice.assistant.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '发票邮寄',
	width: 240,
	height: 430,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	hasButtontoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPInvoice',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePInvoiceByField',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: '',
	OperMsg: '已邮寄',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		//		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键',
			name: 'PInvoice_Id',
			hidden: true,
			itemId: 'PInvoice_Id'
		}, {
			fieldLabel: '收票人姓名',
			name: 'PInvoice_ReceiveInvoiceName',
			itemId: 'PInvoice_ReceiveInvoiceName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '收票人电话',
			name: 'PInvoice_ReceiveInvoicePhoneNumbers',
			itemId: 'PInvoice_ReceiveInvoicePhoneNumbers',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
//			xtype: 'numberfield',
			width: me.defaults.width * 1
		}, {
			fieldLabel: '货运单号',
			allowBlank: false,
			emptyText: '必填项',
			colspan: 2,
			width: me.defaults.width * 2,
			name: 'PInvoice_FreightOddNumbers',
			itemId: 'PInvoice_FreightOddNumbers'
		}, {
			fieldLabel: '货运公司名称',
			name: 'PInvoice_FreightName',
			itemId: 'PInvoice_FreightName',
			colspan: 2,
			width: me.defaults.width * 2,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '收票人地址',
			name: 'PInvoice_ReceiveInvoiceAddress',
			itemId: 'PInvoice_ReceiveInvoiceAddress',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this,
			type = me.formtype;

		if(type == 'add') {
			me.setTitle(me.defaultTitle);
		} else if(type == 'edit') {
			me.setTitle(me.defaultTitle);
		} else if(type == 'show') {
			me.setTitle(me.defaultTitle + '-' + JShell.All.SHOW);
		} else {
			me.setTitle(me.defaultTitle);
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Status: 8,
			InvoiceDate: JShell.Date.toServerDate(values.PInvoice_InvoiceDate)
		};
		if(values.PInvoice_FreightOddNumbers) {
			entity.FreightOddNumbers = values.PInvoice_FreightOddNumbers;
		}
		if(values.PInvoice_FreightName) {
			entity.FreightName = values.PInvoice_FreightName;
		}
		if(values.PInvoice_ReceiveInvoicePhoneNumbers) {
			entity.ReceiveInvoicePhoneNumbers = values.PInvoice_ReceiveInvoicePhoneNumbers;
		}
		if(values.PInvoice_ReceiveInvoiceName) {
			entity.ReceiveInvoiceName = values.PInvoice_ReceiveInvoiceName;
		}
		if(values.PInvoice_ReceiveInvoiceAddress) {
			entity.ReceiveInvoiceAddress = values.PInvoice_ReceiveInvoiceAddress;
		}
		if(me.PK) {
			entity.Id = me.PK;
		}
//		entity.OperationMemo = me.OperMsg;

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id', 'FreightName', 'FreightOddNumbers', ' Status','ReceiveInvoicePhoneNumbers','ReceiveInvoiceName','ReceiveInvoiceAddress'];
		entity.fields = fields.join(',');
		return entity;
	}
});