/**
 * 厂商仪器表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.prod.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
	title: '厂商仪器表单',
	
	width:220,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchTestEquipProdById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddTestEquipProd',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateTestEquipProdByField',
    
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
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'TestEquipProd_Id',hidden:true,type:'key'});
		
		//仪器厂商
		items.push({
			fieldLabel:'仪器厂商',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipProd_ProdOrg_CName',
			itemId:'TestEquipProd_ProdOrg_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				title:'仪器厂商选择',
				defaultWhere:"cenorg.CenOrgType.ShortCode='1'"
			}
		},{
			fieldLabel:'仪器厂商主键ID',hidden:true,
			name:'TestEquipProd_ProdOrg_Id',
			itemId:'TestEquipProd_ProdOrg_Id'
		});
		//检验仪器分类
		items.push({
			fieldLabel:'仪器分类',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipProd_TestEquipType_CName',
			itemId:'TestEquipProd_TestEquipType_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.equip.type.CheckGrid'
		},{
			fieldLabel:'仪器分类主键ID',hidden:true,
			name:'TestEquipProd_TestEquipType_Id',
			itemId:'TestEquipProd_TestEquipType_Id'
		});
		
		//中文名
		items.push({
			fieldLabel:'中文名',name:'TestEquipProd_CName',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'英文名',name:'TestEquipProd_EName'
		},{
			fieldLabel:'代码',name:'TestEquipProd_ShortCode'
		},{
			fieldLabel:'显示次序',name:'TestEquipProd_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		},{
			xtype:'textarea',fieldLabel:'备注',
			name:'TestEquipProd_Memo',
			height:60
		},{
			boxLabel:'是否使用',name:'TestEquipProd_Visible',
			xtype:'checkbox',checked:true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.TestEquipProd_CName,
			EName:values.TestEquipProd_EName,
			ShortCode:values.TestEquipProd_ShortCode,
			DispOrder:values.TestEquipProd_DispOrder,
			Visible:values.TestEquipProd_Visible ? '1' : '0',
			Memo:values.TestEquipProd_Memo
		};
		
		if(values.TestEquipProd_ProdOrg_Id){
			entity.ProdOrg = {Id:values.TestEquipProd_ProdOrg_Id};
		}
		if(values.TestEquipProd_TestEquipType_Id){
			entity.TestEquipType = {Id:values.TestEquipProd_TestEquipType_Id};
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = ['Id','CName','EName','ShortCode','DispOrder','Visible','Memo','ProdOrg_Id','TestEquipType_Id'],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.TestEquipProd_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.CenOrgType_Visible = data.TestEquipProd_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			ProdOrgName = me.getComponent('TestEquipProd_ProdOrg_CName'),
			ProdOrgId = me.getComponent('TestEquipProd_ProdOrg_Id'),
			TestEquipTypeName = me.getComponent('TestEquipProd_TestEquipType_CName'),
			TestEquipTypeId = me.getComponent('TestEquipProd_TestEquipType_Id');
			
		ProdOrgName.on({
			check: function(p, record) {
				ProdOrgName.setValue(record ? record.get('CenOrg_CName') : '');
				ProdOrgId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		
		TestEquipTypeName.on({
			check: function(p, record) {
				TestEquipTypeName.setValue(record ? record.get('TestEquipType_CName') : '');
				TestEquipTypeId.setValue(record ? record.get('TestEquipType_Id') : '');
				p.close();
			}
		});
	}
});