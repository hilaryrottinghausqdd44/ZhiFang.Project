/**
 * 库房表
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.BasicGrid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'库房表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaStorage',
	 /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField',
	/**默认加载数据*/
	defaultLoad:true,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaStorage_DispOrder',direction:'ASC'}],


    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent:function(){
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width:135,emptyText:'库房名称/代码',isLike:true,itemId:'Search',
			fields:['reastorage.CName','reastorage.ShortCode']
		};		
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'ReaStorage_CName',
			text: '库房名称',
			width: 150,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_ShortCode',
			text: '代码',width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_DispOrder',
			text: '次序',
			width: 55,
			align:'center',
			type:'int',	
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},
		{
			dataIndex: 'ReaStorage_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});