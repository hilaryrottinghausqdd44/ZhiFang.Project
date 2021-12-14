/**
 * 学历维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.educationlevel.Grid', {
	extend:'Shell.ux.grid.Panel',
	title:'学历列表',
	width:440,
	hieght:600,
	
	//获取数据服务路径
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBEducationLevelByHQL?isPlanish=true',
	//删除数据服务路径
	delUrl:'/SingleTableService.svc/ST_UDTO_DelBEducationLevel',
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
			emptyText:'学历名称/简称',
			fields:['beducationlevel.Name','beducationlevel.SName']
		};
		me.columns = [{
			text:'学历名称',dataIndex:'BEducationLevel_Name',
			width:150,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BEducationLevel_SName',
			width:80,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BEducationLevel_ShortCode',
			width:60,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'BEducationLevel_PinYinZiTou',
			width:80,defaultRenderer:true
		},{
			text:'使用',dataIndex:'BEducationLevel_IsUse',
			width:40,isBool:true
		},{
			text:'备注',dataIndex:'BEducationLevel_Comment',
			width:200,defaultRenderer:true,hidden:true
		},{
			text:'数据新增时间',dataIndex:'BEducationLevel_DataAddTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'最后修改时间',dataIndex:'BEducationLevel_DataUpdateTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'主键ID',dataIndex:'BEducationLevel_Id',
			width:170,hidden:true,isKey:true
		}];
		me.callParent(arguments);
	},
	//新增处理
	onAddClick:function(){
		this.fireEvent('addclick',this);
	}
});