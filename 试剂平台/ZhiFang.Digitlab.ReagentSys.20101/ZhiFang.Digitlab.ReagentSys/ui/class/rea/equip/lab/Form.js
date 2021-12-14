/**
 * 实验室仪器表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.lab.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
	title: '实验室仪器表单',
	
	width:220,
    height:300,
    
    /**实验室ID*/
    LabId:null,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchTestEquipLabById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddTestEquipLab',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateTestEquipLabByField',
    
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
        labelWidth:75,
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
		
		items.push({fieldLabel:'主键ID',name:'TestEquipLab_Id',hidden:true,type:'key'});
		
		//实验室
		items.push({
			fieldLabel:'实验室',hidden:true,
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipLab_Lab_CName',
			itemId:'TestEquipLab_Lab_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				title:'实验室选择',
				defaultWhere:"cenorg.CenOrgType.ShortCode='3'"
			}
		},{
			fieldLabel:'实验室主键ID',hidden:true,
			name:'TestEquipLab_Lab_Id',
			itemId:'TestEquipLab_Lab_Id'
		});
		//供应商
		items.push({
			fieldLabel:'供应商',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipLab_Comp_CName',
			itemId:'TestEquipLab_Comp_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				title:'供应商选择',
				defaultWhere:"cenorg.CenOrgType.ShortCode='1' or cenorg.CenOrgType.ShortCode='2'"
			}
		},{
			fieldLabel:'供应商主键ID',hidden:true,
			name:'TestEquipLab_Comp_Id',
			itemId:'TestEquipLab_Comp_Id'
		});
		//仪器厂商
		items.push({
			fieldLabel:'仪器厂商',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipLab_ProdOrg_CName',
			itemId:'TestEquipLab_ProdOrg_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				title:'仪器厂商选择',
				defaultWhere:"cenorg.CenOrgType.ShortCode='1'"
			}
		},{
			fieldLabel:'仪器厂商主键ID',hidden:true,
			name:'TestEquipLab_ProdOrg_Id',
			itemId:'TestEquipLab_ProdOrg_Id'
		});
		
		//厂商仪器
		items.push({
			fieldLabel:'厂商仪器',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipLab_TestEquipProd_CName',
			itemId:'TestEquipLab_TestEquipProd_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.equip.prod.CheckGrid',
			classConfig:{
				title:'厂商仪器选择'
			}
		},{
			fieldLabel:'厂商仪器主键ID',hidden:true,
			name:'TestEquipLab_TestEquipProd_Id',
			itemId:'TestEquipLab_TestEquipProd_Id'
		});
		
		//检验仪器分类
		items.push({
			fieldLabel:'仪器分类',
			//emptyText:'必填项',allowBlank:false,
			name:'TestEquipLab_TestEquipType_CName',
			itemId:'TestEquipLab_TestEquipType_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.equip.type.CheckGrid',
			classConfig:{
				title:'仪器分类选择'
			}
		},{
			fieldLabel:'仪器分类主键ID',hidden:true,
			name:'TestEquipLab_TestEquipType_Id',
			itemId:'TestEquipLab_TestEquipType_Id'
		});
		
		//中文名
		items.push({
			fieldLabel:'中文名',name:'TestEquipLab_CName',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'英文名',name:'TestEquipLab_EName'
		},{
			fieldLabel:'代码',name:'TestEquipLab_ShortCode'
		},{
			fieldLabel:'LIS仪器编号',name: 'TestEquipLab_LisCode'
		},{
			fieldLabel:'显示次序',name:'TestEquipLab_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		},{
			xtype:'textarea',fieldLabel:'备注',
			name:'TestEquipLab_Memo',
			height:60
		},{
			boxLabel:'是否使用',name:'TestEquipLab_Visible',
			xtype:'checkbox',checked:true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Lab:{Id:me.LabId},
			CName:values.TestEquipLab_CName,
			EName:values.TestEquipLab_EName,
			ShortCode:values.TestEquipLab_ShortCode,
			DispOrder:values.TestEquipLab_DispOrder,
			Visible:values.TestEquipLab_Visible ? '1' : '0',
			Memo:values.TestEquipLab_Memo,
			LisCode:values.TestEquipLab_LisCode
		};
		
		if(values.TestEquipLab_Comp_Id){
			entity.Comp = {Id:values.TestEquipLab_Comp_Id};
		}
		if(values.TestEquipLab_ProdOrg_Id){
			entity.ProdOrg = {Id:values.TestEquipLab_ProdOrg_Id};
		}
		if(values.TestEquipLab_TestEquipProd_Id){
			entity.TestEquipProd = {Id:values.TestEquipLab_TestEquipProd_Id};
		}
		if(values.TestEquipLab_TestEquipType_Id){
			entity.TestEquipType = {Id:values.TestEquipLab_TestEquipType_Id};
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','CName','EName','ShortCode','DispOrder','Visible','Memo','LisCode',
				'Lab_Id','Comp_Id','ProdOrg_Id','TestEquipProd_Id','TestEquipType_Id'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.TestEquipLab_Id;
		entity.entity.Lab.Id = values.TestEquipLab_Lab_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.CenOrgType_Visible = data.TestEquipLab_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
			
		//实验室
		var LabName = me.getComponent('TestEquipLab_Lab_CName'),
			LabId = me.getComponent('TestEquipLab_Lab_Id');
		LabName.on({
			check: function(p, record) {
				LabName.setValue(record ? record.get('CenOrg_CName') : '');
				LabId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		//供应商
		var CompName = me.getComponent('TestEquipLab_Comp_CName'),
			CompId = me.getComponent('TestEquipLab_Comp_Id');
		CompName.on({
			check: function(p, record) {
				CompName.setValue(record ? record.get('CenOrg_CName') : '');
				CompId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		//仪器厂商
		var ProdOrgName = me.getComponent('TestEquipLab_ProdOrg_CName'),
			ProdOrgId = me.getComponent('TestEquipLab_ProdOrg_Id');
		ProdOrgName.on({
			check: function(p, record) {
				ProdOrgName.setValue(record ? record.get('CenOrg_CName') : '');
				ProdOrgId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		
		//厂商仪器
		var TestEquipProdName = me.getComponent('TestEquipLab_TestEquipProd_CName'),
			TestEquipProdId = me.getComponent('TestEquipLab_TestEquipProd_Id');
		TestEquipProdName.on({
			check: function(p, record) {
				TestEquipProdName.setValue(record ? record.get('TestEquipProd_CName') : '');
				TestEquipProdId.setValue(record ? record.get('TestEquipProd_Id') : '');
				p.close();
			}
		});
		
		//仪器类型
		var TestEquipTypeName = me.getComponent('TestEquipLab_TestEquipType_CName'),
			TestEquipTypeId = me.getComponent('TestEquipLab_TestEquipType_Id');
		TestEquipTypeName.on({
			check: function(p, record) {
				TestEquipTypeName.setValue(record ? record.get('TestEquipType_CName') : '');
				TestEquipTypeId.setValue(record ? record.get('TestEquipType_Id') : '');
				p.close();
			}
		});
	}
});