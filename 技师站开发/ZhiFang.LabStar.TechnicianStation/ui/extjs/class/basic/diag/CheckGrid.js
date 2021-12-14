/**
 * 诊断选择列表
 * @author Jcall
 * @version 2019-12-19
 */
Ext.define('Shell.class.basic.diag.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'诊断选择列表',
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagByHQL?isPlanish=true',
	
	//序号列宽度
	rowNumbererWidth:35,
    //是否单选
	checkOne:true,
	
	//排序
	defaultOrderBy:[{property:'LBDiag_DispOrder',direction:'ASC'}],
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = "(" + me.defaultWhere + ") and ";
		}
		me.defaultWhere += "lbdiag.IsUse=1";
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,fields:['lbdiag.CName']};
		
		//数据列
		me.columns = [{
			text:'名称',dataIndex:'LBDiag_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编码',dataIndex:'LBDiag_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBDiag_Id',isKey:true,hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});