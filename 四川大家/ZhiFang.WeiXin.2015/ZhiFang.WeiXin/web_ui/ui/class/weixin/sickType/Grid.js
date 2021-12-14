/**
 * 就诊类型维护
 * @author GHX
 * @version 2021-01-29
 */
Ext.define('Shell.class.weixin.sickType.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '就诊类型列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSickTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateSickTypeByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelSickType',
	
	/**是否使用字段名*/
    IsUseField:'',
	hasSave:false,
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
			fields:['sicktype.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'就诊类型名称',dataIndex:'SickType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'SickType_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'排序',dataIndex:'SickType_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'HIS编码',dataIndex:'SickType_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'主键ID',dataIndex:'SickType_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});