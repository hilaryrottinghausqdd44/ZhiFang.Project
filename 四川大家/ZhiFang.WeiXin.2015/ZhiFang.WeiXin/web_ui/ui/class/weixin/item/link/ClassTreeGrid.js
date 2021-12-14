/**
 * 检测项目产品分类树
 * @author liangyl	
 * @version 2017-01-18
 */
Ext.define('Shell.class.weixin.item.link.ClassTreeGrid', {
    extend: 'Shell.ux.grid.Panel',
	title: '检测项目产品分类树 ',
	
	width: 400,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelOSItemProductClassTree',

	/**默认加载*/
	defaultLoad: true,

	/**是否启用查询框*/
	hasSearch:true,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**查询栏参数设置*/
	searchToolbarConfig:{},
    defaultWhere:'ositemproductclasstree.IsUse=1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			itemdblclick:function(view,record){
//				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields: ['ositemproductclasstree.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'OSItemProductClassTree_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'OSItemProductClassTree_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'OSItemProductClassTree_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'区域',dataIndex:'OSItemProductClassTree_AreaID',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'OSItemProductClassTree_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.fireEvent('addclick',me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		me.fireEvent('editclick',me,records[0]);
	}
});