/**
 * 入库管理
 * 提取物资接口的出库信息进行入库确认处理
 * 将提取的物资接口的出库信息的入库单进行退库操作?
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.manage.extract.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库信息',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
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
			value: "1",
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
		//登记人
		items.push({
			fieldLabel: '登记人',
			name: 'ReaBmsInDoc_CreaterName',
			itemId: 'ReaBmsInDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '登记ID',
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
		//供货单号
		items.push({
			xtype: 'textarea',
			fieldLabel: '供货单号',
			name: 'ReaBmsInDoc_SaleDocNo',
			itemId: 'ReaBmsInDoc_SaleDocNo',
			colspan: 1,
			width: me.defaults.width * 1,
			height: 20,
			hidden: true
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
		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsInDoc_InvoiceNo',
			itemId: 'ReaBmsInDoc_InvoiceNo',
			colspan: 2,
			width: me.defaults.width * 2,
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
			readOnly: false,
			locked: true,
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
		items.push({
			fieldLabel: '自定义1',
			name: 'ReaBmsInDoc_ZX1',
			itemId: 'ReaBmsInDoc_ZX1',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '自定义2',
			name: 'ReaBmsInDoc_ZX2',
			itemId: 'ReaBmsInDoc_ZX2',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '自定义3',
			name: 'ReaBmsInDoc_ZX3',
			itemId: 'ReaBmsInDoc_ZX3',
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
			colspan: 5,
			width: me.defaults.width * 5,
			height: 50
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
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var createrID = me.getComponent('ReaBmsInDoc_CreaterID');
		var createrName = me.getComponent('ReaBmsInDoc_CreaterName');
		createrID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		createrName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var deptIdV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptNameV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		var deptId = me.getComponent('ReaBmsInDoc_DeptID');
		var deptName = me.getComponent('ReaBmsInDoc_DeptName');
		if(deptId) deptId.setValue(deptIdV);
		if(deptName) deptName.setValue(deptNameV);

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var OperDate = me.getComponent('ReaBmsInDoc_OperDate');
		OperDate.setValue(curDateTime);
		var InType = me.getComponent('ReaBmsInDoc_InType');
		InType.setValue("1");

		if(me.getComponent('buttonsToolbar')) me.getComponent('buttonsToolbar').hide();
	},
	setTotalPrice: function(totalPrice) {
		var me = this;
		var TotalPrice = me.getComponent('ReaBmsInDoc_TotalPrice');
		TotalPrice.setValue(totalPrice);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Status: me.Status,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			SaleDocNo: values.ReaBmsInDoc_SaleDocNo,
			Carrier: values.ReaBmsInDoc_Carrier,
			InType: values.ReaBmsInDoc_InType,
			CreaterName: values.ReaBmsInDoc_CreaterName,
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo,
			OtherDocNo: values.ReaBmsInDoc_OtherDocNo,
			ZX1: values.ReaBmsInDoc_ZX1,
			ZX2: values.ReaBmsInDoc_ZX2,
			ZX3: values.ReaBmsInDoc_ZX3
		};
		if(values.ReaBmsInDoc_SaleDocID) entity.SaleDocID = values.ReaBmsInDoc_SaleDocID;
		if(values.ReaBmsInDoc_SaleDocConfirmID) entity.SaleDocConfirmID = values.ReaBmsInDoc_SaleDocConfirmID;

		if(values.ReaBmsInDoc_DeptID) entity.DeptID = values.ReaBmsInDoc_DeptID;
		if(values.ReaBmsInDoc_DeptName) entity.DeptName = values.ReaBmsInDoc_DeptName;
		entity.Memo = values.ReaBmsInDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		if(values.ReaBmsInDoc_CreaterID) entity.CreaterID = values.ReaBmsInDoc_CreaterID;
		if(values.ReaBmsInDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsInDoc_OperDate);
		if(values.ReaBmsInDoc_TotalPrice) entity.TotalPrice = parseFloat(values.ReaBmsInDoc_TotalPrice);
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		// 'OperDate', 'TotalPrice',后台处理
		var fields = [
			'Id', 'Status', 'InDocNo', 'Carrier', 'InType', 'CreaterName', 'InvoiceNo', 'Memo', 'CreaterID', 'OtherDocNo'
		];
		if(values.ReaBmsInDoc_DeptID) fields.push("DeptID");
		if(values.ReaBmsInDoc_DeptName) fields.push("DeptName");
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
});