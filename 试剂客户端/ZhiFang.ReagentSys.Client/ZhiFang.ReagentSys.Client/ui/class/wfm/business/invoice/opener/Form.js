/**
 * 发票开具
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.invoice.opener.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
	],
	title: '发票开具',
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
	OperMsg: '发票开具',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
			boxLabel: '是否已收款',
			name: 'PInvoice_IsReceiveMoney',
			itemId: 'PInvoice_IsReceiveMoney',
			xtype: 'checkbox',
			checked:true,
			inputValue: 'true',
			colspan: 2,
			width: me.defaults.width * 1,
			style: {
				marginLeft: '85px'
			}
		}, {
			fieldLabel: '处理意见',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 80,
			xtype: 'textarea',
			name: 'PInvoice_InvoiceInfo',
			itemId: 'PInvoice_InvoiceInfo'
		});
		return items;
	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Status: 7
		};
		entity.IsReceiveMoney = values.PInvoice_IsReceiveMoney ? true : false;
//		if(values.PInvoice_IncomeTypeID) {
//			entity.IncomeTypeName = values.PInvoice_IncomeTypeID;
//		}
		if(values.PInvoice_InvoiceInfo) {
			entity.InvoiceInfo = values.PInvoice_InvoiceInfo.replace(/\\/g, '&#92');
			entity.OperationMemo = values.PInvoice_InvoiceInfo.replace(/\\/g, '&#92');
		}
		if(me.PK) {
			entity.Id = me.PK;
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
		var fields = ['Id', ' Status', 'InvoiceInfo', 'IsReceiveMoney'];
		entity.fields = fields.join(',');
		return entity;
	}
});