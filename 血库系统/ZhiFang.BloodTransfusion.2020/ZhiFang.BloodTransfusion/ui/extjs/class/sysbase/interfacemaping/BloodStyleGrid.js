/**
 * 血制品对照列表
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.BloodStyleGrid', {
	extend: 'Shell.class.sysbase.interfacemaping.MapingVOGrid',

	title: '血制品对照列表',

	defaultOrderBy: [{
		property: 'BloodStyle_DispOrder',
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
			fields: ['bloodstyle.CName']
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
			dataIndex: 'BDictMapingVO_BloodStyle_CName',
			width: 140,
			menuDisabled: true,
			doSort: function(state) {
				var field = "BloodStyle_CName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		});
		return columns;
	}
});
