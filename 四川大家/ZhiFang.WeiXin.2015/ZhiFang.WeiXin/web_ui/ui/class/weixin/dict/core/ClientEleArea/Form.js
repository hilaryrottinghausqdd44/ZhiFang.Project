Ext.define("Shell.class.weixin.dict.core.ClientEleArea.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.class.weixin.dict.core.ClientEleArea.Combox',
	],
    title:'区域',
	width:240,
	height:400,
	bodyPadding:10,
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**主键字段*/
	PKField:'ClientEleArea_Id',
	/**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddClientEleArea',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateClientEleAreaByField',
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:80,
        labelAlign:'right'
    },
    
   
	createItems:function(){
		var me =this;
		var items=[
		{
			fieldLabel:'区域id',
			name:'ClientEleArea_Id',
			itemId:'ClientEleArea_Id',
			readOnly:true,
			locked:true
		},{
			fieldLabel:'区域名称',
			name:'ClientEleArea_AreaCName',
			itemId:'ClientEleArea_AreaCName',
		},{
			fieldLabel:'区域简称',
			name:'ClientEleArea_AreaShortName',
			itemId:'ClientEleArea_AreaShortName',
		},{
			fieldLabel:'区域总医院',
			xtype:'ClientEleAreaCombox',
			name:'ClientEleArea_clienteleName',
			itemId:'ClientEleArea_clienteleName',
			listeners:{
				change:function(m,value){
					me.setClienteleId(value);
				}
			},
		},
		{
			xtype:'textfield',
			fieldLabel:'CLIENTELE_CId',
			name:'CLIENTELE_CId',
			itemId:'CLIENTELE_CId',
			hidden:true,
		},
		];
		return items;
	},
	
	setClienteleId:function(id){
		if(!isNaN(id) && id > 0){
		var cLIENTELE_Id=this.getComponent("CLIENTELE_CId");
		cLIENTELE_Id.setValue(id);
		}
	},
	
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			console.log(values);
		var entity = {
			Id:values.ClientEleArea_Id,
			AreaCName:values.ClientEleArea_AreaCName,
			AreaShortName:values.ClientEleArea_AreaShortName,
			ClientNo:values.CLIENTELE_CId,
		};
		
		return {entity:entity};
	},
	
	getEditParams:function(){
		var me =this;
		entity=me.getAddParams();
		entity.fields = 'Id,AreaCName,AreaShortName,ClientNo';
		return entity;
	},
});
