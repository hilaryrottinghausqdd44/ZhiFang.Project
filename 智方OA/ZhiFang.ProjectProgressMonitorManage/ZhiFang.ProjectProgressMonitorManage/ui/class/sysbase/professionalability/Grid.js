/**
 * 专业级别维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.professionalability.Grid', {
	extend:'Shell.ux.grid.Panel',
	title:'专业级别维护',
	width:440,
	hieght:600,
	
	//获取数据服务路径
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityByHQL?isPlanish=true',
	//删除数据服务路径
	delUrl:'/SingleTableService.svc/ST_UDTO_DelBProfessionalAbility',
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
			emptyText:'职称名称/简称',
			fields:['bprofessionalability.Name','bprofessionalability.SName']
		};
		me.columns = [{
			text:'职称名称',dataIndex:'BProfessionalAbility_Name',
			width:150,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BProfessionalAbility_SName',
			width:80,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BProfessionalAbility_ShortCode',
			width:60,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'BProfessionalAbility_PinYinZiTou',
			width:80,defaultRenderer:true
		},{
			text:'使用',dataIndex:'BProfessionalAbility_IsUse',
			width:40,isBool:true
		},{
			text:'备注',dataIndex:'BProfessionalAbility_Comment',
			width:200,defaultRenderer:true,hidden:true
		},{
			text:'数据新增时间',dataIndex:'BProfessionalAbility_DataAddTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'最后修改时间',dataIndex:'BProfessionalAbility_DataUpdateTime',
			width:130,isDate:true,hasTime:true,hidden:true
		},{
			text:'主键ID',dataIndex:'BProfessionalAbility_Id',
			width:170,hidden:true,isKey:true
		}];
		me.callParent(arguments);
	},
	//新增处理
	onAddClick:function(){
		this.fireEvent('addclick',this);
	}
});