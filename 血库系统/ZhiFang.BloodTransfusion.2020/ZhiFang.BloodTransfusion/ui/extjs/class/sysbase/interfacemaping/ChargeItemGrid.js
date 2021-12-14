/**
 * 费用项目对照列表
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.ChargeItemGrid', {
	extend: 'Shell.class.sysbase.interfacemaping.MapingVOGrid',

	title: '费用项目对照列表',
	
	defaultOrderBy: [{
		property: 'BloodChargeItem_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 220,
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
		var columns = me.callParent(arguments);
		columns.unshift({
			text: '费用项目',
			dataIndex: 'BDictMapingVO_BloodChargeItem_CName',
			width: 140,
			menuDisabled: true,
			doSort: function(state) {
				var field = "BloodChargeItem_CName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		});
		return columns;
	}
});
