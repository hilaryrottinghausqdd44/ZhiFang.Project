/**
 * 样本类型选择列表
 * @author liangyl
 * @version 2019-12-26
 */
Ext.define('Shell.class.lts.sampletype.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'样本类型选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
	//排序字段
	defaultOrderBy: [{property:'LBSampleType_DispOrder',direction:'ASC'}],
    //是否单选
	checkOne:true,
	//已选择过的小组
	checkedIds:null,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		
		me.defaultWhere += 'lbsampletype.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'小组名称/编码',isLike:true,
			fields:['lbsampletype.CName','lbsampletype.UseCode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'样本类型名称',dataIndex:'LBSampleType_CName',width:100,defaultRenderer:true
		},{
			text:'编码',dataIndex:'LBSampleType_UseCode',width:100,defaultRenderer:true
		},{
			text:'排序',dataIndex:'LBSampleType_DispOrder',width:60,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBSampleType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});