/**
 * 检验项目LIS对照列表
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.BTestItemGrid', {
	extend: 'Shell.class.sysbase.interfacemaping.MapingVOGrid',

	title: '检验项目LIS对照列表',

	defaultOrderBy: [{
		property: 'BloodUnit_DispOrder',
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
			width: 170,
			emptyText: '名称',
			value:me.searchValue,
			isLike: true,
			fields: ['bloodbtestitem.CName']
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
			text: '中文名称',
			dataIndex: 'BDictMapingVO_BloodBTestItem_CName',
			width: 140,
			menuDisabled: true,
			doSort: function(state) {
				var field = "BloodBTestItem_CName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		});
		return columns;
	}
});
