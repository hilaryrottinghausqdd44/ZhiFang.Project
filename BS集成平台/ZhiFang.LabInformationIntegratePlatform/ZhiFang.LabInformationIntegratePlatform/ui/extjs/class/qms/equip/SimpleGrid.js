/**
 * 仪器列表(简单)
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.SimpleGrid', {
extend: 'Shell.ux.grid.Panel',
	title: '仪器列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEEquipByHQL?isPlanish=true',
	PKField:'EEquip_Id',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEEquipByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelEEquip',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'EEquip_CName',direction: 'ASC'}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**后台排序*/
	remoteSort: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {width: 160,emptyText: '仪器名称',isLike: true,itemId: 'search',fields: ['eequip.CName']};
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'编号',dataIndex:'EEquip_Id',width:170,hidden:true,
			sortable:false,defaultRenderer:true
		},{
			text:'仪器名称',dataIndex:'EEquip_CName',flex:1,minWidth:200,
			sortable:true,defaultRenderer:true
		},{
			text:'代码',dataIndex:'EEquip_UseCode',flex:1,minWidth:120,
			sortable:true,defaultRenderer:true
		}];
		return columns;
	},
	onAddClick:function(){
		var me = this;
		me.fireEvent('onAddClick', me);
	}
});