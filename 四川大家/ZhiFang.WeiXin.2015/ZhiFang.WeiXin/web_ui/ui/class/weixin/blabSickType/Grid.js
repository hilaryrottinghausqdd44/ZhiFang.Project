/**
 * 实验室就诊类型维护
 * @author GHX
 * @version 2021-02-02
 */
Ext.define('Shell.class.weixin.blabSickType.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室就诊类型列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSickTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabSickTypeByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBLabSickType',
	
	/**是否使用字段名*/
    IsUseField:'BLabSickType_UseFlag',
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	IsUseType:'int',
	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['blabsicktype.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'主键ID',dataIndex:'BLabSickType_Id',isKey:true,hidden:false,hideable:false
		},{
			text:'实验室样本名称',dataIndex:'BLabSickType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BLabSickType_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室编码',dataIndex:'BLabSickType_LabCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室样本类型编号',dataIndex:'BLabSickType_LabSickTypeNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'排序',dataIndex:'BLabSickType_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'HIS编码',dataIndex:'BLabSickType_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BLabSickType_UseFlag',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		}]
		
		return columns;
	}
});