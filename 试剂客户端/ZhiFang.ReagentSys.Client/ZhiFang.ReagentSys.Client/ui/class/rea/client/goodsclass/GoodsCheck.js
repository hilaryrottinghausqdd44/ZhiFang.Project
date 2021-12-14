/**
 * 货品分类
 * @author longfc
 * @version 2019-01-02
 */
Ext.define('Shell.class.rea.client.goodsclass.GoodsCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '货品分类',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	width: 320,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**货品分类*/
	ClassType: null,
	/**是否能编辑列*/
	canEdit: false,

	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "&classType=" + me.ClassType;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reagoods.Visible=1';
		if(me.canEdit) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品分类',
			fields: ['reagoods.' + me.ClassType]
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
				dataIndex: 'ReaGoodsClassVO_CName',
				text: '货品分类',
				flex: 1,
				sortable: false,
				menuDisabled: true,
				hideable: false
			},
			{
				dataIndex: 'ReaGoodsClassVO_Id',
				text: '主键ID',
				hidden: true,
				hideable: false,
				isKey: true
			}
		];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.ClassType) return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	}
});