/**
 * 项目
 * @author gzj
 * @version 2019-12-26
 */
Ext.define('Shell.class.lts.sample.result.equip.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'样本类型选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
	//排序字段
	//defaultOrderBy: [{property:'LBSampleType_DispOrder',direction:'ASC'}],
	//是否单选
	checkOne: true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'项目名称',isLike:true,
			fields: ['LBItem.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text: '项目名称', dataIndex:'LisEquipItem_LBItem_CName',width:100,defaultRenderer:true
		},{
				text: '主键ID', dataIndex:'LisEquipItem_LBItem_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	},
	changeResult:function(data) {
		var list = data.list;
		for(var i in list){
			
		}
		return data;
	}
});