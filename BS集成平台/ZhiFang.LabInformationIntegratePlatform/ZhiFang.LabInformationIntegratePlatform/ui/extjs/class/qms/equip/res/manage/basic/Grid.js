/**
 * 职责基础列表
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.manage.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',
	title: '职责列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEResponsibilityByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEResponsibilityByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelEResponsibility',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'EResponsibility_CName',direction: 'ASC'}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**后台排序*/
	remoteSort: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**默认每页数量*/
//	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {width: 160,emptyText: '职责名称',isLike: true,itemId: 'search',fields: ['eresponsibility.CName']};
		buttonToolbarItems.unshift('refresh','-');

		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'主键',dataIndex:'EResponsibility_Id',width:170,hidden:true,
			sortable:false,isKey: true,defaultRenderer:true
		},{
			text:'职责名称',dataIndex:'EResponsibility_CName',width:180,
			sortable:true,defaultRenderer:true
		}];
		return columns;
	}
});