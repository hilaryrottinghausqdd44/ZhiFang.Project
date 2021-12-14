/**
 * 入库类型选择列表
 * @author liangyl
 * @version 2017-11-10
 */
Ext.define('Shell.class.rea.client.storagetype.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'入库类型选择列表',
    width:500,
    height:300,
    
    /**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBStorageTypeByHQL?isPlanish=true',
    
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bstoragetype.IsUse=1';
		
		//查询框信息
		me.searchInfo = {
			width:145,isLike:true,itemId: 'Search',
			emptyText:'名称',
			fields:['bstoragetype.Name']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BStorageType_Name',text: '名称',width: 100,align: 'center',defaultRenderer: true
		},{
			dataIndex: 'BStorageType_ShortCode',text: '代码',width: 60,defaultRenderer: true
		},{
			dataIndex: 'BStorageType_Id',text: '主键ID',hidden: true,hideable: false,isKey: true
		}];
		
		return columns;
	}
});