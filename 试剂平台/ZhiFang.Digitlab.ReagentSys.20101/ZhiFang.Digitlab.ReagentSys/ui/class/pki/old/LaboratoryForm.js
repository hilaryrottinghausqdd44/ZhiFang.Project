/**
 * 送检单位表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.LaboratoryForm',{
    extend:'Shell.ux.form.Panel',
    
    requires:['Shell.ux.form.field.SimpleComboBox'],
    
    title:'送检单位表单',
    
    width:300,
    height:200,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBLaboratoryById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddBLaboratory',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateBLaboratoryByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	layout:'anchor',
	bodyPadding:10,
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:70,
        labelAlign:'right'
    },
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			levelObj = JShell.PKI.Enum.CoopLevel,
			coopLevelList = [],
			items = [];
			
		for(var i in levelObj){
			coopLevelList.push([i.slice(1),levelObj[i]]);
		}
		
		items.push({fieldLabel:'主键ID',name:'BLaboratory_Id',hidden:true});
		
		items.push({
			fieldLabel:'单位名称',name:'BLaboratory_CName',allowBlank:false
		});
		items.push({fieldLabel:'单位简称',name:'BLaboratory_ShortCode'});
		
//		items.push({
//			fieldLabel:'合作分级',name:'BLaboratory_CoopLevel',
//			xtype:'uxSimpleComboBox',data:coopLevelList
//		});
		items.push({
			fieldLabel:'默认开票方',name:'BLaboratory_BBillingUnit_Name',
			itemId:'BLaboratory_BBillingUnit_Name',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.pki.BillingUnitCheckGrid',{
					resizable:false,
					listeners:{
						accept:function(p,record){me.onAccept(record);p.close();}
					}
				}).show();
			}
		});
		
		items.push({
			fieldLabel:'默认开票方主键ID',hidden:true,
			name:'BLaboratory_BBillingUnit_Id',
			itemId:'BLaboratory_BBillingUnit_Id'
		});
		items.push({
			fieldLabel:'默认开票方时间戳',hidden:true,
			name:'BLaboratory_BBillingUnit_DataTimeStamp',
			itemId:'BLaboratory_BBillingUnit_DataTimeStamp'
		});
		
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues();
			entity = {};
		entity.entity = {
			CName:values.BLaboratory_CName,
			ShortCode:values.BLaboratory_ShortCode,
			//CoopLevel:values.BLaboratory_CoopLevel,
			BBillingUnit:{
				Id:values.BLaboratory_BBillingUnit_Id,
				DataTimeStamp:values.BLaboratory_BBillingUnit_DataTimeStamp.split(',')
			}
		};
		
		entity.fields = 'Id,CName,ShortCode,BBillingUnit_Id';
		
		entity.entity.Id = values.BLaboratory_Id;
		return entity;
	},
	/**选择确认处理*/
	onAccept:function(record){
		var me = this;
		var Id = me.getComponent('BLaboratory_BBillingUnit_Id');
		var Name = me.getComponent('BLaboratory_BBillingUnit_Name');
		var DataTimeStamp = me.getComponent('BLaboratory_BBillingUnit_DataTimeStamp');
		
		Id.setValue(record ? record.get('BBillingUnit_Id') : '');
		Name.setValue(record ? record.get('BBillingUnit_Name') : '');
		DataTimeStamp.setValue(record ? record.get('BBillingUnit_DataTimeStamp') : '');
	}
});