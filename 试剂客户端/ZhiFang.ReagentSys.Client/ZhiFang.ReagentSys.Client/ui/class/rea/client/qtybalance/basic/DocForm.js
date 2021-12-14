/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存结转信息',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance',
	/**修改服务地址*/
	editUrl: '',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	buttonDock: "top",
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 85,
		width: 185,
		labelAlign: 'right'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 185) me.defaults.width = 185;

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//上次结转日期
		items.push({
			fieldLabel: '上次结转ID',
			name: 'ReaBmsQtyBalanceDoc_PreQtyBalanceDocID',
			hidden: true,
			readOnly: true,
			locked: true
		}, {
			//xtype: 'datefield',
			fieldLabel: '上次结转日期',
			//format: 'Y-m-d H:m:s',
			name: 'ReaBmsQtyBalanceDoc_PreBalanceDateTime',
			itemId: 'ReaBmsQtyBalanceDoc_PreBalanceDateTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//上次结转单号
		items.push({
			fieldLabel: '上次结转单号',
			name: 'ReaBmsQtyBalanceDoc_PreQtyBalanceDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		//结转日期
		items.push({
			//xtype: 'datefield',
			fieldLabel: '结转日期',
			//format: 'Y-m-d H:m:s',
			name: 'ReaBmsQtyBalanceDoc_DataAddTime',
			itemId: 'ReaBmsQtyBalanceDoc_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//结转单号
		items.push({
			fieldLabel: '结转单号',
			name: 'ReaBmsQtyBalanceDoc_QtyBalanceDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		//操作人
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsQtyBalanceDoc_OperName',
			itemId: 'ReaBmsQtyBalanceDoc_OperName',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d H:m:s',
			name: 'ReaBmsQtyBalanceDoc_OperDate',
			itemId: 'ReaBmsQtyBalanceDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//创建人
		items.push({
			fieldLabel: '创建人',
			name: 'ReaBmsQtyBalanceDoc_CreaterName',
			itemId: 'ReaBmsQtyBalanceDoc_CreaterName',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaBmsQtyBalanceDoc_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsQtyBalanceDoc_Id',
			hidden: true,
			type: 'key'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsQtyBalanceDoc_Memo',
			itemId: 'ReaBmsQtyBalanceDoc_Memo',
			colspan: 3,
			width: me.defaults.width * 3,
			height: 40
		});
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {

		var DataAddTime = data.ReaBmsQtyBalanceDoc_DataAddTime;
		var OperDate = data.ReaBmsQtyBalanceDoc_OperDate;
		var PreBalanceDateTime = data.ReaBmsQtyBalanceDoc_PreBalanceDateTime;
		if(PreBalanceDateTime) data.ReaBmsQtyBalanceDoc_PreBalanceDateTime = JcallShell.Date.toString(PreBalanceDateTime);
		if(OperDate) data.ReaBmsQtyBalanceDoc_OperDate = JcallShell.Date.toString(OperDate); 
		if(DataAddTime) data.ReaBmsQtyBalanceDoc_DataAddTime = JcallShell.Date.toString(DataAddTime);

		var reg = new RegExp("<br />", "g");
		data.ReaBmsQtyBalanceDoc_Memo = data.ReaBmsQtyBalanceDoc_Memo.replace(reg, "\r\n");

		var visible = data.ReaBmsQtyBalanceDoc_Visible;
		if(visible == "1" || visible == 1 || visible == "true" || visible == true) visible = true;
		else visible = false;
		data.ReaBmsQtyBalanceDoc_Visible = visible;
		return data;
	}
});