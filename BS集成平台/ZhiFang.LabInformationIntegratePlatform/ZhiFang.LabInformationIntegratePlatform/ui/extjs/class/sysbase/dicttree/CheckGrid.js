/**
 * 下级类型树选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dicttree.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'下级类型树选择列表',
    width:270,
    height:300,
    
    /**父类型ID*/
    ParentID:null,
	
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 40,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bdicttree.IsUse=1 and bdicttree.ParentID=' + me.ParentID;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['bdicttree.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'名称',dataIndex:'BDictTree_CName',width:210,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BDictTree_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});