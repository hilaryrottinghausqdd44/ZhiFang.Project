/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '手工入库',
	formtype: 'show',
	width: 780,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',

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
		labelWidth: 55,
		width: 155,
		labelAlign: 'right'
	},

	/**客户端入库总单状态*/
	StatusKey: "ReaBmsInDocStatus",
	/**客户端入库类型*/
	InTypeKey: "ReaBmsInDocInType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 155) me.defaults.width = 155;
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.InTypeKey, false, true, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//入库总单号
		items.push({
			fieldLabel: '入库单号',
			name: 'ReaBmsInDoc_InDocNo',
			colspan: 4,
			width: me.defaults.width * 4,
			hidden: true,
			readOnly: true,
			locked: true
		});
		//入库类型
		items.push({
			fieldLabel: '入库类型',
			emptyText: '必填项',
			name: 'ReaBmsInDoc_InType',
			itemId: 'ReaBmsInDoc_InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.InTypeKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			className: 'Shell.class.rea.client.storagetype.CheckGrid',
			listeners: {
				check: function(p, record) {
					p.close();
				}
			}
		});
		//数据来源
		items.push({
			fieldLabel: '数据来源',
			name: 'ReaBmsInDoc_SourceType',
			itemId: 'ReaBmsInDoc_SourceType',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//供货单ID
		items.push({
			fieldLabel: '供货单ID',
			name: 'ReaBmsInDoc_SaleDocID',
			itemId: 'ReaBmsInDoc_SaleDocID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//供货验收单ID
		items.push({
			fieldLabel: '供货验收单ID',
			name: 'ReaBmsInDoc_SaleDocConfirmID',
			itemId: 'ReaBmsInDoc_SaleDocConfirmID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsInDoc_Status',
			itemId: 'ReaBmsInDoc_Status',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			//allowBlank: false,
			readOnly: true,
			locked: true
		});
		//送货人
		items.push({
			fieldLabel: '送货人',
			name: 'ReaBmsInDoc_Carrier',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d',
			name: 'ReaBmsInDoc_OperDate',
			itemId: 'ReaBmsInDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//操作者
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsInDoc_CreaterName',
			itemId: 'ReaBmsInDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsInDoc_CreaterID',
			itemId: 'ReaBmsInDoc_CreaterID'
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsInDoc_Id',
			hidden: true,
			type: 'key'
		});
		//提取单号
		items.push({
			xtype: 'textarea',
			fieldLabel: '提取单号',
			name: 'ReaBmsInDoc_OtherDocNo',
			itemId: 'ReaBmsInDoc_OtherDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 20
		});
		//供货单号
		items.push({
			xtype: 'textarea',
			fieldLabel: '供货单号',
			name: 'ReaBmsInDoc_SaleDocNo',
			itemId: 'ReaBmsInDoc_SaleDocNo',
			colspan: 1,
			width: me.defaults.width * 1,
			height: 20
		});
		//	总单金额
		items.push({
			fieldLabel: '合计金额',
			name: 'ReaBmsInDoc_TotalPrice',
			itemId: 'ReaBmsInDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//自定义1
		items.push({
			fieldLabel: '自定义1',
			name: 'ReaBmsInDoc_ZX1',
			itemId: 'ReaBmsInDoc_ZX1',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});	
		//所属部门
		items.push({
			fieldLabel: '所属部门',
			emptyText: '所属部门',
			name: 'ReaBmsInDoc_DeptName',
			itemId: 'ReaBmsInDoc_DeptName',
			colspan: 2,
			width: me.defaults.width * 2,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
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
			name: 'ReaBmsInDoc_DeptID',
			itemId: 'ReaBmsInDoc_DeptID'
		});
		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsInDoc_InvoiceNo',
			itemId: 'ReaBmsInDoc_InvoiceNo',
			colspan: 2,
			width: me.defaults.width*2,
			height: 20
		});
		//自定义2
		items.push({
			fieldLabel: '自定义2',
			name: 'ReaBmsInDoc_ZX2',
			itemId: 'ReaBmsInDoc_ZX2',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsInDoc_Memo',
			itemId: 'ReaBmsInDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 50
		});
		//自定义3
		items.push({
			fieldLabel: '自定义3',
			name: 'ReaBmsInDoc_ZX3',
			itemId: 'ReaBmsInDoc_ZX3',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});	
		return items;
	},
	/**部门选择*/
	onDeptAccept: function(record) {
		var me = this,
			deptID = me.getComponent('ReaBmsInDoc_DeptID'),
			deptName = me.getComponent('ReaBmsInDoc_DeptName');
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		deptID.setValue((record ? record.get('tid') : ''));
		deptName.setValue(text);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.ReaBmsInDoc_OperDate) data.ReaBmsInDoc_OperDate = JcallShell.Date.toString(data.ReaBmsInDoc_OperDate, true);
		var reg = new RegExp("<br />", "g");
		data.ReaBmsInDoc_Memo = data.ReaBmsInDoc_Memo.replace(reg, "\r\n");
		return data;
	}
});