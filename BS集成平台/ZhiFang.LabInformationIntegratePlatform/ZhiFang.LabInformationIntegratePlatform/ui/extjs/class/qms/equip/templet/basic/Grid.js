/**
 *仪器模板列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateETempletByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETemplet',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETemplet_DispOrder',
		direction: 'ASC'
	}],

	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**主键列*/
	PKField: 'ETemplet_Id',
	/**后台排序*/
	remoteSort: true,
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);  
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
		buttonToolbarItems = me.buttonToolbarItems || [];
	   //查询框信息
		me.searchInfo = {width: 145,emptyText: '模板名称',isLike: true,
			itemId: 'search',fields: ['etemplet.CName']
		};
		buttonToolbarItems.unshift('refresh','->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',dataIndex: 'ETemplet_Id',width: 160,sortable: false,hidden:true,defaultRenderer: true
		},{
			text: '模板名称',dataIndex: 'ETemplet_CName',flex:1,minWidth:280,sortable: true,defaultRenderer: true
		}];
		return columns;
	}
 
});