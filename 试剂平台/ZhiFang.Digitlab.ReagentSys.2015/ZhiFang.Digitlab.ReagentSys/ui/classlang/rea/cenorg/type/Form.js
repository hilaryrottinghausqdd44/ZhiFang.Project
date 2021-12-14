/**
 * 机构类型表单
 * @author Jcall
 * @version 2015-07-02
 * @version 2017-08-10
 */
Ext.define('Shell.classlang.rea.cenorg.type.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.BoolComboBox'
    ],
	
	width:240,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddCenOrgType',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateCenOrgTypeByField',
    
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
			
		items.push({fieldLabel:'KEY_ID',name:'CenOrgType_Id',hidden:true,type:'key'});
		
		//中文名
		items.push({
			fieldLabel:JShell.Lang.CENORG_TYPE_CNAME,
			name:'CenOrgType_CName',
			emptyText:'必填项',allowBlank:false
		});
		//英文名
		items.push({
			fieldLabel:JShell.Lang.CENORG_TYPE_ENAME,
			name:'CenOrgType_EName',
			emptyText:'必填项',allowBlank:false
		});
		//代码
		items.push({
			fieldLabel:JShell.Lang.CENORG_TYPE_SHORTCODE,
			name:'CenOrgType_ShortCode',
			emptyText:'必填项',allowBlank:false
		});
		//显示次序
		items.push({
			fieldLabel:JShell.Lang.CENORG_TYPE_DISPORDER,
			name:'CenOrgType_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		//启用
		items.push({
			fieldLabel:JShell.Lang.CENORG_TYPE_VISIBLE,
			name:'CenOrgType_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:false
		});
		
		//备注
		items.push({
			xtype:'textarea',
			fieldLabel:JShell.Lang.CENORG_TYPE_MEMO,
			name:'CenOrgType_Memo',
			height:60
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.CenOrgType_CName,
			EName:values.CenOrgType_EName,
			ShortCode:values.CenOrgType_ShortCode,
			DispOrder:values.CenOrgType_DispOrder,
			Visible:values.CenOrgType_Visible ? '1' : '0',
			Memo:values.CenOrgType_Memo
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
		
		entity.entity.Id = values.CenOrgType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.CenOrgType_Visible = data.CenOrgType_Visible == '1' ? true : false;
		return data;
	}
});