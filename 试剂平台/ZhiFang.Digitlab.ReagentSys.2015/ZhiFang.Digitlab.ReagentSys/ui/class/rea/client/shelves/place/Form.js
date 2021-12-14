/**
 * 货位标
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.place.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '货位信息',
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	width: 240,
	height: 275,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaPlace',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaPlaceByField',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**库房id*/
	ReaStorageID:null,
	/**库房 名称*/
	ReaStorageCName:null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '货架名称',name: 'ReaPlace_CName',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel: '代码',name: 'ReaPlace_ShortCode',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel:'专项1',name:'ReaPlace_ZX1',hidden:true
		},{
			fieldLabel:'专项2',name:'ReaPlace_ZX2',hidden:true
		},{
			fieldLabel:'专项3',name:'ReaPlace_ZX3',hidden:true
		},{
			fieldLabel:'启用',name:'ReaPlace_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		},{
			fieldLabel:'显示次序',name:'ReaPlace_DispOrder',emptyText:'必填项',allowBlank:false,xtype:'numberfield',value:0
		},{
			height: 85,fieldLabel:'描述',emptyText: '描述',name: 'ReaPlace_Memo',xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',name: 'ReaPlace_Id',hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.ReaPlace_CName,
			ShortCode: values.ReaPlace_ShortCode,
			ZX1: values.ReaPlace_ZX1,
			ZX2: values.ReaPlace_ZX2,
			ZX3: values.ReaPlace_ZX3,
			DispOrder: values.ReaPlace_DispOrder,
			Memo: values.ReaPlace_Memo,
			Visible:values.ReaPlace_Visible ? 1 : 0
		};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreaterID=userId;
		    entity.CreaterName = userName;
		}
		if(me.ReaStorageID){
			entity.ReaStorage = {
				Id:me.ReaStorageID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'CName', 'Id', 'ShortCode', 'ZX1',
			'ZX2','ZX3','DispOrder','Memo','Visible'
		];
		entity.fields = fields.join(',');
		if(values.ReaPlace_Id != '') {
			entity.entity.Id = values.ReaPlace_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	  /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});