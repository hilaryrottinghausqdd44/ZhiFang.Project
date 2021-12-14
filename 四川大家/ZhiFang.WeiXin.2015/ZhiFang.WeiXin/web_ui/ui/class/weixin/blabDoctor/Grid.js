/**
 * 实验室医生维护
 * @author GHX
 * @version 2021-02-05
 */
Ext.define('Shell.class.weixin.blabDoctor.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室医生列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabDoctorByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabDoctorByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBLabDoctor',
	
	/**是否使用字段名*/
    IsUseField:'BLabDoctor_Visible',
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
			fields:['blabdoctor.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'主键ID',dataIndex:'BLabDoctor_Id',isKey:true,hidden:false,hideable:false
		},{
			text:'名称',dataIndex:'BLabDoctor_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BLabDoctor_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室编码',dataIndex:'BLabDoctor_LabCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编号',dataIndex:'BLabDoctor_LabDoctorNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}/* ,{
			text:'排序',dataIndex:'BLabDoctor_DispOrder',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		} */,{
			text:'HIS编码',dataIndex:'BLabDoctor_HisOrderCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BLabDoctor_Visible',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'bool'
		}]
		
		return columns;
	}
});