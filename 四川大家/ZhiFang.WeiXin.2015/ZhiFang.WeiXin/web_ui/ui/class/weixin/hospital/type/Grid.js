/**
 * 医院列表
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.type.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '医院分类列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalTypeByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospitalType',
	
	
	/**是否使用字段名*/
    IsUseField:'BHospitalType_IsUse',
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	
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
			fields:['bhospitaltype.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'BHospitalType_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编码',dataIndex:'BHospitalType_Code',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BHospitalType_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BHospitalType_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'汉字拼音字头',dataIndex:'BHospitalType_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BHospitalType_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'备注',dataIndex:'BHospitalType_Comment',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BHospitalType_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});