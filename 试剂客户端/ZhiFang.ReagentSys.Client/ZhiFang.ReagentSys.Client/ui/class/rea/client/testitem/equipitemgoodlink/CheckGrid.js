/**
 * 仪器试剂选择
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '货品选择',
	width: 450,
	height: 350,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL?isPlanish=true',
	/**是否单选*/
	checkOne: false,
	ReaGoodsArr: [],
	/**默认每页数量*/
	defaultPageSize: 1000,
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 155,
			emptyText: '货品名称/货品编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reagoods.CName', 'reagoods.ReaGoodsNo']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaEquipReagentLink_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_ReaGoods_CName',
			text: '货品名称',
			minWidth: 150,
			flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_ReaGoods_UnitName',
			text: '单位',
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_ReaGoods_UnitMemo',
			text: '规格',
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_ReaGoods_TestCount',
			text: '测试数',
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_GoodsID',
			text: '货品Id',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];
		return columns;
	}
});