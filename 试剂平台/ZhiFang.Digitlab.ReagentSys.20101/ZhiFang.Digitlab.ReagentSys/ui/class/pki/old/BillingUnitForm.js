/**
 * 开票方表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.BillingUnitForm',{
    extend:'Shell.ux.form.Panel',
    
    requires:[
	    'Shell.ux.form.field.BoolComboBox',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'开票方表单',
    width:430,
    height:250,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBBillingUnitById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddBBillingUnit',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateBBillingUnitByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			unitTypeObj = JShell.PKI.Enum.UnitType,
			unitTypeList = [],
			items = [];
			
		for(var i in unitTypeObj){
			unitTypeList.push([i.slice(1),unitTypeObj[i]]);
		}
		
		items.push({fieldLabel:'主键ID',name:'BBillingUnit_Id',hidden:true});
		
		items.push({x:0,y:10,fieldLabel:'名称',name:'BBillingUnit_Name',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'简称',name:'BBillingUnit_SName'});
		
		items.push({x:0,y:40,fieldLabel:'快捷码',name:'BBillingUnit_Shortcode'});
		items.push({x:210,y:40,fieldLabel:'拼音字头',name:'BBillingUnit_PinYinZiTou'});
		
		items.push({
			x:0,y:70,width:130,xtype:'uxSimpleComboBox',
			fieldLabel:'开票类型',name:'BBillingUnit_BillingUnitType',
			data:unitTypeList
		});
		items.push({x:140,y:70,width:130,xtype:'uxBoolComboBox',
			fieldLabel:'使用',name:'BBillingUnit_IsUse',value:true});
		items.push({x:280,y:70,width:130,xtype:'numberfield',
			fieldLabel:'显示次序',name:'BBillingUnit_DispOrder',value:0});
		
		items.push({x:0,y:100,width:410,height:80,xtype:'textarea',
			fieldLabel:'备注',name:'BBillingUnit_Comment'});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BBillingUnit_Name,
			SName:values.BBillingUnit_SName,
			Shortcode:values.BBillingUnit_Shortcode,
			PinYinZiTou:values.BBillingUnit_PinYinZiTou,
			BillingUnitType:values.BBillingUnit_BillingUnitType || '0',
			IsUse:values.BBillingUnit_IsUse,
			DispOrder:values.BBillingUnit_DispOrder,
			Comment:values.BBillingUnit_Comment
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
		
		entity.entity.Id = values.BBillingUnit_Id;
		return entity;
	}
});