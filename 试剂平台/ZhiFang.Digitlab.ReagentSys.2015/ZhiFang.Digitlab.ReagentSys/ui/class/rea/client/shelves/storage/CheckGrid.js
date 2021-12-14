/**
 * 库房选择列表
 * @author liangyl
 * @version 2017-11-10
 */
Ext.define('Shell.class.rea.client.shelves.storage.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'库房选择列表',
    width:500,
    height:300,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
    
    /**是否单选*/
	checkOne:false,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reastorage.Visible=1';
		
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
			text: '代码',width: 80,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});