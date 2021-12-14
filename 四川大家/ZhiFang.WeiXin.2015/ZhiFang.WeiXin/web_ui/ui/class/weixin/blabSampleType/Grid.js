/**
 * 实验室样本类型维护
 * @author GHX
 * @version 2021-02-02
 */
Ext.define('Shell.class.weixin.blabSampleType.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室样本类型列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSampleTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabSampleTypeByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBLabSampleType',
	
	/**是否使用字段名*/
    IsUseField:'BLabSampleType_Visible',
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
			fields:['blabsampletype.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'主键ID',dataIndex:'BLabSampleType_Id',isKey:true,hidden:false,hideable:false
		},{
			text:'实验室样本名称',dataIndex:'BLabSampleType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BLabSampleType_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室编码',dataIndex:'BLabSampleType_LabCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室样本类型编号',dataIndex:'BLabSampleType_LabSampleTypeNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'排序',dataIndex:'BLabSampleType_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'HIS编码',dataIndex:'BLabSampleType_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BLabSampleType_Visible',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		}]
		
		return columns;
	}
});