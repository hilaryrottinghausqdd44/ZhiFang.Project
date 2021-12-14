/**
 * 医院等级列表
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.level.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '医院等级列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalLevelByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalLevelByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospitalLevel',
	
	/**是否使用字段名*/
    IsUseField:'BHospitalLevel_IsUse',
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			itemdblclick:function(view,record){
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['bhospitallevel.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'BHospitalLevel_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编码',dataIndex:'BHospitalLevel_Code',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BHospitalLevel_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BHospitalLevel_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'汉字拼音字头',dataIndex:'BHospitalLevel_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BHospitalLevel_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'备注',dataIndex:'BHospitalLevel_Comment',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BHospitalLevel_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});