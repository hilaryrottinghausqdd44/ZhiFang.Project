/**
 * 就诊类型选择列表
 * @author Jcall
 * @version 2020-06-21
 */
Ext.define('Shell.class.lts.sicktype.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'就诊类型选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true',
	//排序字段
	defaultOrderBy: [{property:'LBSickType_DispOrder',direction:'ASC'}],
    //是否单选
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		
		me.defaultWhere += 'lbsicktype.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称/快捷码',isLike:true,
			fields:['lbsicktype.CName','lbsicktype.Shortcode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'类型名称',dataIndex:'LBSickType_CName',width:100,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'LBSickType_Shortcode',width:100,defaultRenderer:true
		},{
			text:'排序',dataIndex:'LBSickType_DispOrder',width:60,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBSickType_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});