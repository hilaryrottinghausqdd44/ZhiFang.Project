/**
 * 机构产品列表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '产品列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoods',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
	
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoods_CName',
			text: '名称',
			width: 100,
			type:'int',editor:{}
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '产品编号',
			width: 100,
			editor:{}
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 100,
			editor:{}
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			editor:{}
		},{
			dataIndex: 'ReaGoods_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		},{
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'产品编号/中文名/英文名/简称',
			fields:['reagoods.GoodsNo','reagoods.CName','reagoods.EName','reagoods.ShortCode']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	}
	
});