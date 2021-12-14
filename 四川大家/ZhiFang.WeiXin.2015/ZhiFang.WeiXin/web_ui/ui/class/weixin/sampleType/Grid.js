/**
 * 样本类型维护
 * @author GHX
 * @version 2021-01-29
 */
Ext.define('Shell.class.weixin.sampleType.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '样本类型列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSampleTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateSampleTypeByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelSampleType',
	
	/**是否使用字段名*/
    IsUseField:'SampleType_Visible',
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
			fields:['sampletype.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'样本名称',dataIndex:'SampleType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'SampleType_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'排序',dataIndex:'SampleType_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'HIS编码',dataIndex:'SampleType_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'SampleType_Visible',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'主键ID',dataIndex:'SampleType_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});