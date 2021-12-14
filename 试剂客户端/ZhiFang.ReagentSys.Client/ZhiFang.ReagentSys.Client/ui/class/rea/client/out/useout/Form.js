/**
 * 出库查询
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.out.useout.Form', {
	extend: 'Shell.ux.form.Panel',

	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '出库信息',

	height: 175,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocById?isPlanish=true',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 6 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 175,
		labelAlign: 'right'
	},
	/**内容周围距离*/
	bodyPadding: '10px 0px 0px 0px',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	formtype: "show",
	PK: null,
	ReaBmsOutDocOutType: 'ReaBmsOutDocOutType',
	/**移库总单状态Key*/
	ReaBmsOutDocStatus: 'ReaBmsOutDocStatus',
	ReaBmsOutDocIOFlag:'ReaBmsOutDocIOFlag',
	ReaBmsOutDocThirdFlag:'ReaBmsOutDocThirdFlag',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocOutType, false, false, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocStatus, false, false, null);
		me.addEvents('setDefaultStorage');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: 'Id',
			name: 'ReaBmsOutDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '出库单号',
			name: 'ReaBmsOutDoc_OutDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '出库类型',
			name: 'ReaBmsOutDoc_OutType',
			itemId: 'ReaBmsOutDoc_OutType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsOutDocOutType].List,
			colspan: 1,
			hidden: false,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '出库库房',
			name: 'ReaBmsOutDoc_StorageName',
			itemId: 'ReaBmsOutDoc_StorageName',
			//xtype: 'uxSimpleComboBox',
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '货品总额',
			name: 'ReaBmsOutDoc_TotalPrice',
			itemId: 'ReaBmsOutDoc_TotalPrice',
			readOnly: true,
			locked: true,
			xtype: 'numberfield'
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push(
		 {
			fieldLabel: '使用部门',
			name: 'ReaBmsOutDoc_DeptName',
			itemId: 'ReaBmsOutDoc_DeptName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '单据状态',
			name: 'ReaBmsOutDoc_Status',
			itemId: 'ReaBmsOutDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '数据标志',
			name: 'ReaBmsOutDoc_IOFlag',
			itemId: 'ReaBmsOutDoc_IOFlag',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '第三方标记',
			name: 'ReaBmsOutDoc_IsThirdFlag',
			itemId: 'ReaBmsOutDoc_IsThirdFlag',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {fieldLabel: '登记时间',
			name: 'ReaBmsOutDoc_DataAddTime',
			itemId: 'ReaBmsOutDoc_DataAddTime',
			//xtype: 'datefield',
			//format: 'Y-m-d',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			colspan: 2,
			width: me.defaults.width * 2
		});

		items.push({
			fieldLabel: '需要审核',
			xtype: 'checkboxfield',
			boxLabel: '',
			inputValue: true,
			name: 'ReaBmsOutDoc_IsHasCheck',
			itemId: 'ReaBmsOutDoc_IsHasCheck',
			colspan: 1,
			width: me.defaults.width * 1,
			hidden: true,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
		}, {
			fieldLabel: '审核人',
			name: 'ReaBmsOutDoc_CheckName',
			itemId: 'ReaBmsOutDoc_CheckName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审核时间',
			name: 'ReaBmsOutDoc_CheckTime',
			itemId: 'ReaBmsOutDoc_CheckTime',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审核备注',
			name: 'ReaBmsOutDoc_CheckMemo',
			itemId: 'ReaBmsOutDoc_CheckMemo',
			xtype: 'textarea',
			height: 22,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '需要审批',
			xtype: 'checkboxfield',
			boxLabel: '',
			inputValue: true,
			name: 'ReaBmsOutDoc_IsHasApproval',
			itemId: 'ReaBmsOutDoc_IsHasApproval',
			colspan: 1,
			width: me.defaults.width * 1,
			hidden: true,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
		}, {
			fieldLabel: '审批人',
			name: 'ReaBmsOutDoc_ApprovalCName',
			itemId: 'ReaBmsOutDoc_ApprovalCName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审批时间',
			name: 'ReaBmsOutDoc_ApprovalTime',
			itemId: 'ReaBmsOutDoc_ApprovalTime',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审批备注',
			name: 'ReaBmsOutDoc_ApprovalMemo',
			itemId: 'ReaBmsOutDoc_ApprovalMemo',
			xtype: 'textarea',
			height: 22,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			height: 22,
			fieldLabel: '出库说明',
			emptyText: '出库说明',
			name: 'ReaBmsOutDoc_Memo',
			xtype: 'textarea',
			colspan: 6,
			width: me.defaults.width * 6
		});
		items.push({
			fieldLabel: '供应商确认人',
			name: 'ReaBmsOutDoc_SupplierConfirmName',
			itemId: 'ReaBmsOutDoc_SupplierConfirmName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '供应商确认时间',
			name: 'ReaBmsOutDoc_SupplierConfirmTime',
			itemId: 'ReaBmsOutDoc_SupplierConfirmTime',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '供应商确认备注',
			name: 'ReaBmsOutDoc_SupplierConfirmMemo',
			itemId: 'ReaBmsOutDoc_SupplierConfirmMemo',
			xtype: 'textarea',
			height: 22,
			colspan: 4,
			width: me.defaults.width * 4
		});
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var dataAddTime = data.ReaBmsOutDoc_DataAddTime;
		if (dataAddTime) {
			data.ReaBmsOutDoc_DataAddTime = JcallShell.Date.toString(dataAddTime.replace(/\//g, "-"));
		}
		var SupplierConfirmTime = data.ReaBmsOutDoc_SupplierConfirmTime;
		if (SupplierConfirmTime) {
			data.ReaBmsOutDoc_SupplierConfirmTime = JcallShell.Date.toString(SupplierConfirmTime.replace(/\//g, "-"));
		}
		var checkTime = data.ReaBmsOutDoc_CheckTime;
		if (checkTime) {
			data.ReaBmsOutDoc_CheckTime = JcallShell.Date.toString(checkTime.replace(/\//g, "-"));
		}
		var approvalTime = data.ReaBmsOutDoc_ApprovalTime;
		if (approvalTime) {
			data.ReaBmsOutDoc_ApprovalTime = JcallShell.Date.toString(approvalTime.replace(/\//g, "-"));
		}
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	isShow: function(id) {
		var me = this;
		me.setReadOnly(true);
		me.formtype = 'show';
		me.changeTitle(); //标题更改
		me.disableControl();
		me.load(id);
	}
});