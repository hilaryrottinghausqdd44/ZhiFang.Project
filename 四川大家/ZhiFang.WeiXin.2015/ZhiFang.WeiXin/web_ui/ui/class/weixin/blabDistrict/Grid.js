/**
 * 实验室病区维护
 * @author GHX
 * @version 2021-02-04
 */
Ext.define('Shell.class.weixin.blabDistrict.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室病区列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabDistrictByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabDistrictByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBLabDistrict',
	
	/**是否使用字段名*/
    IsUseField:'BLabDistrict_Visible',
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
			fields:['blabdistrict.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'主键ID',dataIndex:'BLabDistrict_Id',isKey:true,hidden:false,hideable:false
		},{
			text:'名称',dataIndex:'BLabDistrict_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BLabDistrict_ShortName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BLabDistrict_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室编码',dataIndex:'BLabDistrict_LabCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编号',dataIndex:'BLabDistrict_LabDistrictNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'排序',dataIndex:'BLabDistrict_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'HIS编码',dataIndex:'BLabDistrict_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BLabDistrict_Visible',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'bool'
		}]
		
		return columns;
	}
});