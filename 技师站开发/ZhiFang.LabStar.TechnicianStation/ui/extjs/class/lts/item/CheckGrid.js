/**
 * 项目选择列表
 * @author liangyl
 * @version 2019-11-18
 */
Ext.define('Shell.class.lts.item.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'项目选择列表',
    width:270,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:false,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'lbitem.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'项目名称/用户代码',isLike:true,
			fields:['lbitem.CName','lbitem.UseCode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'项目名称',dataIndex:'LBItem_CName',width:140,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'LBItem_SName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'用户代码',dataIndex:'LBItem_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBItem_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	}
});