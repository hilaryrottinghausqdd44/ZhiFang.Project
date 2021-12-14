/**
 * 性别列表
 * @author Jcall
 * @version 2018-09-14
 */
Ext.define('Shell.class.sysbase.sex.Grid', {
	extend:'Shell.ux.grid.Panel',
	title:'性别列表',
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexByHQL?isPlanish=true',
	//删除数据服务路径
	delUrl:'/ServerWCF/SingleTableService.svc/ST_UDTO_DelBSex',
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
	
	//引物类型ID
	TypeId:null,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:220,isLike:true,itemId:'Search',
			emptyText:'名称/简称',
			fields:['bsex.Name','bsex.SName']
		};
		me.columns = [{
			text:'名称',dataIndex:'BSex_Name',
			width:100,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BSex_SName',
			width:80,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BSex_Shortcode',
			width:60,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'BSex_PinYinZiTou',
			width:80,defaultRenderer:true
		},{
			text:'使用',dataIndex:'BSex_IsUse',
			width:40,isBool:true
		},{
			text:'备注',dataIndex:'BSex_Comment',
			width:200,defaultRenderer:true
		},{
			text:'数据新增时间',dataIndex:'BSex_DataAddTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'最后修改时间',dataIndex:'BSex_DataUpdateTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'主键ID',dataIndex:'BSex_Id',
			width:170,hidden:true,isKey:true
		}];
		me.callParent(arguments);
	},
	//新增处理
	onAddClick:function(){
		this.fireEvent('addclick');
	}
});