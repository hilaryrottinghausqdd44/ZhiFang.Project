/**
 * 仪器类型表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.type.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '仪器类型表单',
	
	width:220,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchTestEquipTypeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddTestEquipType',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateTestEquipTypeByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'TestEquipType_Id',hidden:true,type:'key'});
		
		//中文名
		items.push({
			fieldLabel:'中文名',name:'TestEquipType_CName',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'英文名',name:'TestEquipType_EName'
		},{
			fieldLabel:'代码',name:'TestEquipType_ShortCode'
		},{
			fieldLabel:'显示次序',name:'TestEquipType_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		},{
			xtype:'textarea',fieldLabel:'备注',
			name:'TestEquipType_Memo',
			height:60
		},{
			boxLabel:'是否使用',name:'TestEquipType_Visible',
			xtype:'checkbox',checked:true
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.TestEquipType_CName,
			EName:values.TestEquipType_EName,
			ShortCode:values.TestEquipType_ShortCode,
			DispOrder:values.TestEquipType_DispOrder,
			Visible:values.TestEquipType_Visible ? '1' : '0',
			Memo:values.TestEquipType_Memo
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = ['Id','CName','EName','ShortCode','DispOrder','Visible','Memo'],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.TestEquipType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.CenOrgType_Visible = data.TestEquipType_Visible == '1' ? true : false;
		return data;
	}
});