/**
 * 输血过程记录:输血过程记录项列表
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.adversereaction.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '输血过程记录项',
	
	hasPagingtoolbar:false,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodTransItem_DispOrder',
		direction: 'ASC'
	}],
	//输血过程记录主单ID
	PK:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('onEditClick', me);
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			xtype: 'checkcolumn',
			dataIndex: 'BloodTransItem_CheckColumn',
			text: '<b style="color:blue;">选择</b>',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '内容分类ID',
			dataIndex: 'BloodTransItem_ContentTypeID',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '输血记录主单ID',
			dataIndex: 'BloodTransItem_BloodTransForm_Id',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '记录选择项ID',
			dataIndex: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '记录选择项',
			dataIndex: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			width: 80,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BloodTransItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]
		return columns;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.PK) return false;
				
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	}
});
