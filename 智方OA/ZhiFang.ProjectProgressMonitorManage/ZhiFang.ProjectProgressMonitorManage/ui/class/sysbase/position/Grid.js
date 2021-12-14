/**
 * 职务维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.position.Grid', {
	extend:'Shell.ux.grid.Panel',
	title:'职务列表',
	width:440,
	hieght:600,
	
	//获取数据服务路径
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHRPositionByHQL?isPlanish=true',
	//删除数据服务路径
	delUrl:'/RBACService.svc/RBAC_UDTO_DelHRPosition',
	//默认加载数据
	defaultLoad:true,
	//是否启用序号列
	hasRownumberer:true,
	
	//是否启用刷新按钮
	hasRefresh:true,
	//是否启用新增按钮
	hasAdd:true,
	//是否开启删除按钮
	hasDel:true,
	//是否启用查询框
	hasSearch:true,
	
	//复选框
	multiSelect:true,
	selType:'checkboxmodel',
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:220,isLike:true,itemId:'Search',
			emptyText:'职务名称/简称',
			fields:['hrposition.CName','hrposition.SName']
		};
		me.columns = [{
			text:'职务名称',dataIndex:'HRPosition_CName',
			width:150,defaultRenderer:true
		},{
			text:'简称',dataIndex:'HRPosition_SName',
			width:80,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'HRPosition_ShortCode',
			width:60,defaultRenderer:true
		},{
			text:'代码',dataIndex:'HRPosition_UseCode',
			width:60,defaultRenderer:true
		},{
			text:'使用',dataIndex:'HRPosition_IsUse',
			width:40,isBool:true
		},{
			text:'备注',dataIndex:'HRPosition_Comment',
			width:200,defaultRenderer:true,hidden:true
		},{
			text:'数据新增时间',dataIndex:'HRPosition_DataAddTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'最后修改时间',dataIndex:'HRPosition_DataUpdateTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'主键ID',dataIndex:'HRPosition_Id',
			width:170,hidden:true,isKey:true
		}];
		me.callParent(arguments);
	},
	//新增处理
	onAddClick:function(){
		this.fireEvent('addclick',this);
	}
});