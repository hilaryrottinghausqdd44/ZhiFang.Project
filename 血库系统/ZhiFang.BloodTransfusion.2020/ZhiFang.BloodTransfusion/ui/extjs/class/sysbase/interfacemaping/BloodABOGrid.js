/**
 * 血型ABO血站对照列表
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.BloodABOGrid', {
	extend: 'Shell.class.sysbase.interfacemaping.MapingVOGrid',

	title: '血型ABO血站对照列表',
	
	defaultOrderBy: [{
		property: 'BloodABO_DispOrder',
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
			fields: ['bloodabo.CName']
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
			dataIndex: 'BDictMapingVO_BloodABO_CName',
			width: 140,
			menuDisabled: true,
			doSort: function(state) {
				var field = "BloodABO_CName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		});
		return columns;
	}
});
