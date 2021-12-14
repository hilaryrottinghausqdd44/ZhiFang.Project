/**
 * 组合项目子项选择列表
 * @author liangyl
 * @version 2019-11-29
 */
Ext.define('Shell.class.lts.itemmerge.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'组合项目子项选择列表',
    width:300,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:false,
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:145,emptyText:'项目名称/用户代码',isLike:true,
			fields:['lbitemgroupvo.LBItem.CName','lbitemgroupvo.LBItem.UseCode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text: '项目名称', dataIndex:'LBItemGroup_LBItem_CName',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'用户代码',dataIndex:'LBItemGroup_LBItem_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目显示次序',dataIndex:'LBItemGroup_LBItem_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'主键ID',dataIndex:'LBItemGroup_LBItem_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	},
	changeResult:function(data) {
		var list = data.list;
		for(var i in list){
			
		}
		return data;
	}
});