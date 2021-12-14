/**
 * Rea_User_Storage_Link_出库库房货架权限
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.BasicLinkGrid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'出库库房货架权限表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaUserStorageLinkByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaUserStorageLink',
	 /**修改服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaUserStorageLink',
	/**默认加载数据*/
	defaultLoad:false,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaUserStorageLink_DispOrder',direction:'ASC'}],
    /**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);		
	},
	initComponent:function(){
		var me = this;		
		//数据列
		me.columns = me.createGridColumns();
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'ReaUserStorageLink_OperName',
			text: '人员名称',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'ReaUserStorageLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaUserStorageLink_OperID',
			text: '人员ID',
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-','del','-',{
			text:'人员选择',tooltip:'人员选择',
			iconCls:'button-add',
			handler:function(){
			   me.onAddClick();
			}
		}];
		return items;
	},
	onAddClick:function(){
		
	}
});