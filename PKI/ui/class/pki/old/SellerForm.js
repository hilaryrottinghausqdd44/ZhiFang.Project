/**
 * 销售表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.SellerForm',{
    extend:'Shell.ux.form.Panel',
    
    requires:['Shell.ux.form.field.BoolComboBox'],
    
    title:'销售表单',
    width:430,
    height:280,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBSellerById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddBSeller',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateBSellerByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		//maxLength:20,enforceMaxLength:true
		items.push({fieldLabel:'主键ID',name:'BSeller_Id',hidden:true});
		
		items.push({x:0,y:10,fieldLabel:'名称',name:'BSeller_Name',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'简称',name:'BSeller_SName'});
		
		items.push({x:0,y:40,fieldLabel:'快捷码',name:'BSeller_Shortcode'});
		items.push({x:210,y:40,fieldLabel:'拼音字头',name:'BSeller_PinYinZiTou'});
		
		items.push({x:0,y:70,fieldLabel:'联系电话',name:'BSeller_PhoneCode'});
		items.push({x:210,y:70,fieldLabel:'联系方式',name:'BSeller_ContactInfo'});
		
		items.push({x:0,y:100,width:130,fieldLabel:'职位',name:'BSeller_Position'});
		items.push({x:140,y:100,width:130,xtype:'uxBoolComboBox',fieldLabel:'使用',name:'BSeller_IsUse',value:true});
		items.push({x:280,y:100,width:130,xtype:'numberfield',fieldLabel:'显示次序',name:'BSeller_DispOrder',value:0});
		
		items.push({x:0,y:130,width:410,height:80,xtype:'textarea',fieldLabel:'备注',name:'BSeller_Comment'});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BSeller_Name,
			SName:values.BSeller_SName,
			Shortcode:values.BSeller_Shortcode,
			PinYinZiTou:values.BSeller_PinYinZiTou,
			PhoneCode:values.BSeller_PhoneCode,
			ContactInfo:values.BSeller_ContactInfo,
			Position:values.BSeller_Position,
			IsUse:values.BSeller_IsUse,
			DispOrder:values.BSeller_DispOrder,
			Comment:values.BSeller_Comment
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		for(var i in fields){
			fields[i] = fields[i].split('_').slice(1);
		}
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.BSeller_Id;
		return entity;
	}
});