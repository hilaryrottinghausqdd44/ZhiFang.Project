/**
 * 费用项目选择列表
 * @author longfc
 * @version 2020-07-07
 */
Ext.define('Shell.class.sysbase.chargeitem.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '费用项目选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BloodChargeItem_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bloodchargeitem.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['bloodchargeitem.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '名称',
			dataIndex: 'BloodChargeItem_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'BloodChargeItem_Memo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BloodChargeItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});
