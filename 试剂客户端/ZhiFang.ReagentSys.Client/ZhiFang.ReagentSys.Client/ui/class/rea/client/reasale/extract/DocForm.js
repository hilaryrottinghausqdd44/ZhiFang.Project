/**
 * 供货总单表单-查看
 * @author longfc
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.reasale.extract.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '供货总单查看',

	formtype: 'show',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,

	/**内容周围距离*/
	bodyPadding: '10px 10px 0 10px',
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			name: 'Id',
			hidden: true,
			type: 'key'
		});

		//供货单号
		items.push({
			fieldLabel: '供货单号',
			name: 'SaleDocNo',
			emptyText: '必填项',
			allowBlank: false
		});
		//订货方
		items.push({
			fieldLabel: '订货方',
			emptyText: '必填项',
			allowBlank: false,
			name: 'LabCName',
			itemId: 'LabCName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false
		});
		//供货商
		items.push({
			fieldLabel: '供货商',
			name: 'CompCName',
			itemId: 'CompCName',
			readOnly: true,
			locked: true
		});
		//操作人员
		items.push({
			fieldLabel: '操作人员',
			name: 'UserName',
			itemId: 'UserName',
			readOnly: true,
			locked: true
		});
		//紧急标志
		items.push({
			fieldLabel: '紧急标志',
			xtype: 'uxSimpleComboBox',
			name: 'UrgentFlag',
			itemId: 'UrgentFlag',
			data: JShell.REA.Enum.getList('UrgentFlag'),
			allowBlank: false,
			value: '0'
		});
		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d H:m:s',
			name: 'OperDate',
			itemId: 'OperDate',
			allowBlank: false
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'Memo',
			itemId: 'Memo',
			height: 60
		});

		items.push({
			fieldLabel: '发票号',
			name: 'InvoiceNo',
			xtype: 'textarea',
			height: 60,
			itemId: 'InvoiceNo',
			readOnly: true,
			allowBlank: false,
			locked: true
		});

		return items;
	}
});