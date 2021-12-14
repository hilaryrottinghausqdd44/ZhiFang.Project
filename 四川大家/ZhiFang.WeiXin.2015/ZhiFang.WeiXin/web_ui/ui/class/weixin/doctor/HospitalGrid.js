/**
 * 医院列表
 * @author liangyl	
 * @version 2017-03-03
 */
Ext.define('Shell.class.weixin.doctor.HospitalGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '医院列表 ',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospital',
	
  	/**获取医院服务路径*/
	selectHospitalUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: true,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:false,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
//	defaultPageSize: 2,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.addAllData();
	},
	/**添加默认行（全部)*/
	addAllData:function(){
		var me = this;
		var obj={BHospital_HTypeName:'',BHospital_HospitalCode:'',
		BHospital_Id:'',BHospital_LevelName:'',BHospital_Name:'全部',
		BHospital_SName:'',BHospital_Shortcode:'',BHospital_EName:''};
	    me.store.insert(0,obj);
	    me.getSelectionModel().select(0);
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		me.store.removeAll(); //清空数据
		me.addAllData();
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {width:120,emptyText:'医院名称',isLike:true,
			itemId: 'search',fields:['bhospital.Name']};
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
		   buttonToolbarItems = me.buttonToolbarItems || [];
		   
		buttonToolbarItems.unshift('refresh','-');
	    buttonToolbarItems.push({
			fieldLabel: '区域',
			emptyText: '区域',
			name: 'BHospital_Area',
			itemId: 'BHospital_Area',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
	        className: 'Shell.class.weixin.doctor.AreaCheckGrid',labelWidth: 32,
			width: 145,
			classConfig: {
				title: '区域选择',
				checkOne:true
			}
		}, {
			fieldLabel: '区域',
			emptyText: '区域',
			name: 'BHospital_AreaID',
			itemId: 'BHospital_AreaID',
			hidden: true,
			xtype: 'textfield'
		},'-');
		buttonToolbarItems.push('->', {
			type: 'search',
			itemId: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
	    obj={BHospital_HTypeName:'',BHospital_HospitalCode:'',
			BHospital_Id:'',BHospital_LevelName:'',BHospital_Name:'全部',
			BHospital_SName:'',BHospital_Shortcode:'',BHospital_EName:''
		};
		if(data.value){
			list=data.value.list;
			list.splice(0,0,obj);
		}else{
			list=[];
			list.push(obj);
		}
	    result.list =  list;
		return result;
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'医院名称',dataIndex:'BHospital_Name',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院编码',dataIndex:'BHospital_HospitalCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院分类',dataIndex:'BHospital_HTypeName',width:60,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院级别',dataIndex:'BHospital_LevelName',width:60,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'BHospital_EName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BHospital_SName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BHospital_Shortcode',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BHospital_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		//区域
		var BHospital_Area = me.getComponent('buttonsToolbar').getComponent('BHospital_Area');
		var BHospital_AreaID = me.getComponent('buttonsToolbar').getComponent('BHospital_AreaID');
		var search = me.getComponent('buttonsToolbar').getComponent('search');

		BHospital_Area.on({
			check: function(p, record) {
				var Id=record ? record.get('ClientEleArea_Id') : '';
				var Name=record ? record.get('ClientEleArea_AreaCName') : '';
				BHospital_Area.setValue(Name);
				BHospital_AreaID.setValue(Id);
				p.close();
				me.onSearch();
			}
		});
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		var BHospital_AreaID = me.getComponent('buttonsToolbar').getComponent('BHospital_AreaID');
//      if(!BHospital_AreaID.getValue()){
//			JShell.Msg.alert('请选择区域!');
//			return;
//		}
		me.defaultWhere ="bhospital.IsUse=1";
    	me.load(null, true, autoSelect);
	},

	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,AreaID=null,
			params = [];
			
		me.internalWhere = '';
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue(),
			AreaID = buttonsToolbar.getComponent('BHospital_AreaID').getValue();
		}
		//根据区域Id
		if(AreaID) {
			params.push("bhospital.AreaID=" + AreaID);
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