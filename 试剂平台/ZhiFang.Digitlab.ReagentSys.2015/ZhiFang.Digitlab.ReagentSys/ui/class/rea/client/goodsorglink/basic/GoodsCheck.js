/**
 * 产品选择列表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.GoodsCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '产品选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 860,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reagoods.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 230,
			isLike: true,
			itemId: 'Search',
			emptyText: '产品名称/同系列码/产品编码/英文名',
			fields: ['reagoods.CName','reagoods.ShortCode','reagoods.GoodsNo',  'reagoods.EName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoods_CName',
			text: '产品名称',
			//flex:1,
			width: 210,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ShortCode',
			text: '同系列码',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '产品编号',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
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