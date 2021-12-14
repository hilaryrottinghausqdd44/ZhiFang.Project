/**
 * 实验室性别维护
 * @author GHX
 * @version 2021-02-03
 */
Ext.define('Shell.class.weixin.bSex.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '性别列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBSexByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBSexByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBSex',
	
	/**是否使用字段名*/
    IsUseField:'BSex_IsUse',
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	IsUseType:'bool',
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
			fields:['bsex.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'BSex_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BSex_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室编码',dataIndex:'BSex_LabID',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BSex_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'BSex_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'备注',dataIndex:'BSex_Comment',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:false
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BSex_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'主键ID',dataIndex:'BSex_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});