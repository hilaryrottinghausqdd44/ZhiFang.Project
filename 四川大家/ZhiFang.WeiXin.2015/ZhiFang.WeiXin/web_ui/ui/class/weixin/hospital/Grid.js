/**
 * 医院列表
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '医院列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospital',
	
	/**是否使用字段名*/
    IsUseField:'BHospital_IsUse',
    /**是否启用修改按钮*/
	hasEdit:true,
	/**默认加载*/
	defaultLoad: false,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	/**区域ID*/
	AreaID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openEditForm(id);
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			itemId: 'search',fields:['bhospital.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'医院名称',dataIndex:'BHospital_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院编码',dataIndex:'BHospital_HospitalCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院分类',dataIndex:'BHospital_HTypeName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院级别',dataIndex:'BHospital_LevelName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'BHospital_EName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BHospital_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BHospital_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'汉字拼音字头',dataIndex:'BHospital_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BHospital_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'主键ID',dataIndex:'BHospital_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.weixin.hospital.Form', {
			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:'add',
			AreaID:me.AreaID,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		me.openEditForm(id);
	},
	/**打开修改页面*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.weixin.hospital.Form', {
			SUB_WIN_NO:'2',//内部窗口编号
			resizable: false,
			formtype:'edit',
			AreaID:me.AreaID,
			PK:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];
			
		me.internalWhere = '';
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据区域Id
		if(me.AreaID) {
			params.push("bhospital.AreaID=" + me.AreaID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});