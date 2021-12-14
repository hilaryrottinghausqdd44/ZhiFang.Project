/**
 * 操作记录列表
 * @author Jcall
 * @version 2020-07-14
 */
Ext.define('Shell.class.lts.sample.operate.Grid',{
    extend:'Shell.ux.grid.Panel',
    title:'操作记录列表',
    width:510,
    height:300,
    
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateByHQL?isPlanish=true',
	
	//显示成功信息
	showSuccessInfo:false,
	//消息框消失时间
	hideTimes:3000,
	
	//默认加载
	defaultLoad:true,
	//默认每页数量
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:true,
	//是否启用刷新按钮
	hasRefresh:true,
	
	defaultOrderBy:[{property:'LisOperate_DataAddTime',direction:'ASC'}],
	
	//检验单ID
    testFormId:null,
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//默认数据条件
		if(me.testFormId){
			me.defaultWhere = "lisoperate.OperateObject='LisTestForm' and lisoperate.OperateObjectID=" + me.testFormId;
		}
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'记录ID',dataIndex:'LisOperate_Id',width:190,hidden:true,hideable:false
		},{
			text:'操作类型',dataIndex:'LisOperate_OperateType',width:120,defaultRenderer:true
		},{
			text:'类型编码',dataIndex:'LisOperate_OperateTypeID',width:80,defaultRenderer:true
		},{
			text:'操作时间',dataIndex:'LisOperate_DataAddTime',width:130,isDate:true,hasTime:true
		},{
			text:'说明',dataIndex:'LisOperate_OperateMemo',width:120,sortable:false,defaultRenderer:true
		}];
		
		return columns;
	}
});