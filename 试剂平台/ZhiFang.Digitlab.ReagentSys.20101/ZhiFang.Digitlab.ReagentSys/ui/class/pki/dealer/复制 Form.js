/**
 * 经销商表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.Form',{
    extend:'Shell.ux.form.Panel',
    
    requires:['Shell.ux.form.field.BoolComboBox'],
    
    title:'经销商表单',
    width:430,
    height:310,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBDealerById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddBDealer',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateBDealerByField',
    
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
		items.push({fieldLabel:'主键ID',name:'BDealer_Id',hidden:true});
		
		items.push({x:0,y:10,fieldLabel:'名称',name:'BDealer_Name',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'简称',name:'BDealer_SName'});
		
		items.push({x:0,y:40,fieldLabel:'快捷码',name:'BDealer_Shortcode'});
		items.push({x:210,y:40,fieldLabel:'拼音字头',name:'BDealer_PinYinZiTou'});
		
		items.push({x:0,y:70,fieldLabel:'联系电话',name:'BDealer_PhoneCode'});
		items.push({x:210,y:70,fieldLabel:'联系方式',name:'BDealer_ContactInfo'});
		
		//开票方
		items.push({
			x:0,y:100,fieldLabel:'开票方',
			name:'BDealer_BBillingUnit_Name',
			itemId:'BDealer_BBillingUnit_Name',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.pki.billingunit.CheckGrid',{
					resizable:false,
					listeners:{
						accept:function(p,record){me.onBillingUnitAccept(record);p.close();}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel:'开票方主键ID',hidden:true,
			name:'BDealer_BBillingUnit_Id',
			itemId:'BDealer_BBillingUnit_Id'
		});
		items.push({
			fieldLabel:'开票方时间戳',hidden:true,
			name:'BDealer_BBillingUnit_DataTimeStamp',
			itemId:'BDealer_BBillingUnit_DataTimeStamp'
		});
		
		//items.push({x:0,y:130,width:130,xtype:'uxBoolComboBox',fieldLabel:'阶梯价',name:'BDealer_BStepPrice'});
		items.push({x:140,y:130,width:130,xtype:'uxBoolComboBox',fieldLabel:'使用',name:'BDealer_IsUse',value:true});
		items.push({x:280,y:130,width:130,xtype:'numberfield',fieldLabel:'显示次序',name:'BDealer_DispOrder',value:0});
		
		items.push({x:0,y:170,width:410,height:80,xtype:'textarea',fieldLabel:'备注',name:'BDealer_Comment'});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BDealer_Name,
			SName:values.BDealer_SName,
			Shortcode:values.BDealer_Shortcode,
			PinYinZiTou:values.BDealer_PinYinZiTou,
			PhoneCode:values.BDealer_PhoneCode,
			ContactInfo:values.BDealer_ContactInfo,
			//BStepPrice:values.BDealer_BStepPrice,
			IsUse:values.BDealer_IsUse,
			DispOrder:values.BDealer_DispOrder,
			Comment:values.BDealer_Comment
		};
		
		if(values.BDealer_BBillingUnit_Id){
			entity.BBillingUnit = {
				Id:values.BDealer_BBillingUnit_Id,
				DataTimeStamp:values.BDealer_BBillingUnit_DataTimeStamp.split(','),
			}
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		entity.fields += ',BBillingUnit_Id';
		
		entity.entity.Id = values.BDealer_Id;
		return entity;
	},
	/**开票方选择确认处理*/
	onBillingUnitAccept:function(record){
		var me = this;
		var Id = me.getComponent('BDealer_BBillingUnit_Id');
		var Name = me.getComponent('BDealer_BBillingUnit_Name');
		var DataTimeStamp = me.getComponent('BDealer_BBillingUnit_DataTimeStamp');
		
		Id.setValue(record ? record.get('BBillingUnit_Id') : '');
		Name.setValue(record ? record.get('BBillingUnit_Name') : '');
		DataTimeStamp.setValue(record ? record.get('BBillingUnit_DataTimeStamp') : '');
	}
});