/**
 * 样本结果详细列表
 * @author liangyl	
 * @version 2019-11-19
 */
Ext.define('Shell.class.lts.itemmerge.ResultGrid', {
	extend: 'Shell.ux.grid.Panel',	
	title: '样本结果详细列表 ',
	width: 800,
	height: 500,
	
    /**获取样本单项目数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**样本单ID*/
	LisTestFormID:null,
//	defaultOrderBy:[{property:'LisTestItem_GSampleNoForOrder',direction:'ASC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'样本单ID',dataIndex:'LisTestItem_LisTestForm_Id',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目编号',dataIndex:'LisTestItem_LBItem_Id',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LisTestItem_LBItem_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目结果',dataIndex:'LisTestItem_ReportValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	onSearch:function(LisTestFormID,autoSelect){
		var me = this;
		me.defaultWhere ="listestitem.LisTestForm.Id="+ LisTestFormID;
		me.load(null, true, autoSelect);
	}
});