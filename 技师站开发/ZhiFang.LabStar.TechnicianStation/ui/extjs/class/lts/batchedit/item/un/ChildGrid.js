/**
 * 组合项目子项
 * @author liangyl
 * @version 2019-12-20
 */
Ext.define('Shell.class.lts.batchedit.item.un.ChildGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '组合项目子项',
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: false,
	  //排序字段
	defaultOrderBy: [{ property: "LBItemGroup_LBItem_DispOrder", direction: "ASC" }],

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
			text: '项目id', dataIndex:'LBItemGroup_LBItem_Id',width:180,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LBItemGroup_LBItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'LBItemGroup_LBItem_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'用户代码',dataIndex:'LBItemGroup_LBItem_UseCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'LBItemGroup_LBItem_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目',dataIndex:'LBItemGroup_LBItem_GroupType',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医嘱项目',dataIndex:'LBItemGroup_LBItem_IsOrderItem',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	//根据组合项目ID加载子项
	loadDataByID : function(id){
		var me = this;
		me.defaultWhere = 'GroupItemID='+id;
		me.onSearch();
	}
});