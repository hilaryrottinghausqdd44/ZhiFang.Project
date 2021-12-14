/**
 * 货架选择列表
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.set.place.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '货架选择列表',
	width: 330,
	height: 420,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**传入的货品Id*/
	ReaGoodId: null,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reaplace.Visible=1';

		me.searchInfo = {
			width: 235,
			emptyText: '货位名称/代码',
			isLike: true,
			itemId: 'Search',
			fields: ['reaplace.CName', 'reaplace.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaPlace_CName',
			text: '货位名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaPlace_ShortCode',
			text: '代码',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaPlace_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	}
});