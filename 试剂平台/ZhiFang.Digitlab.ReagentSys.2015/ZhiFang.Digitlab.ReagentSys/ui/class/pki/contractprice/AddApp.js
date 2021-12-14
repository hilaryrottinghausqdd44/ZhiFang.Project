/**
 * 新增合同价格
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.AddApp',{
    extend:'Ext.panel.Panel',
    title:'新增合同价格',
    
    layout:'border',
    bodyPadding:1,
    
    width:600,
    height:300,
    
    
    /**新增服务地址*/
    saveUrl:'/BaseService.svc/ST_UDTO_AddDContractPrice',
    
    /**送检单位ID*/
	LaboratoryId:null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp:null,
	
	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId:null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName:null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
//		var DContractPrice_BDealer_Id = ContractPriceAddForm.getComponent('DContractPrice_BDealer_Id');
//		
//		DContractPrice_BDealer_Id.on({
//			change:function(field,newValue){
//				var v = newValue || '';
//				var where = '(dunititem.UnitType=2 and dunititem.UnitID=' + me.LaboratoryId + ')';
//				if(v) where += ' or (dunititem.UnitType=1 and dunititem.UnitID=' + v + ')';
//				UnitItemCheckGrid.defaultWhere = where;
//				UnitItemCheckGrid.onSearch();
//			}
//		});
		ContractPriceAddForm.on({
			save:function(){me.onSaveInfo();}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(Ext.create('Shell.class.pki.unititem.CheckGrid',{
			region:'west',header:false,checkOne:false,
			split:true,collapsible:true,
			itemId:'UnitItemCheckGrid',hasAcceptButton:false,
			defaultWhere:'dunititem.UnitType=2 and dunititem.UnitID=' + me.LaboratoryId,
			searchInfo:{width:'100%',emptyText:'项目名称',isLike:true,
				fields:['dunititem.BTestItem.CName']}
		}));
		items.push(Ext.create('Shell.class.pki.contractprice.AddForm',{
			region:'center',header:false,
			itemId:'ContractPriceAddForm',
			LaboratoryId:me.LaboratoryId,//送检单位ID
			LaboratoryDataTimeStamp:me.LaboratoryDataTimeStamp,//送检单位时间戳
			LaboratoryBillingUnitId:me.LaboratoryBillingUnitId,//送检单位默认开票方ID
			LaboratoryBillingUnitName:me.LaboratoryBillingUnitName,//送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp:me.LaboratoryBillingUnitDataTimeStamp//送检单位默认开票方时间戳
		}));
			
		return items;
	},
	/**保存数据*/
	onSaveInfo:function(){
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
		
		//表单数据校验
		if(!ContractPriceAddForm.getForm().isValid()) return;
		
		//项目必须勾选
		var records = UnitItemCheckGrid.getSelectionModel().getSelection();
		var len = records.length;
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var entity = me.getEntity();
		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];
		
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			var rec = records[i];
			//entity.IsStepPrice = rec.get('DUnitItem_IsStepPrice') == true ? '1' : '0';
			entity.CoopLevel = rec.get('DUnitItem_CoopLevel');
			entity.BTestItem = {
				Id:rec.get('DUnitItem_BTestItem_Id'),
				DataTimeStamp:rec.get('DUnitItem_BTestItem_DataTimeStamp').split(',')
			};
			me.saveOne(entity);
		}
	},
	/**保存一条信息*/
	saveOne:function(entity){
		var me = this;
		var url = (me.saveUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;
		
		var params = {entity:entity};
		params = Ext.JSON.encode(params);
		
		JShell.Server.post(url,params,function(data){
			me.saveIndex++;
			if(!data.success){
				me.saveError.push(data.msg);
			}
			if(me.saveIndex == me.saveCount){
				me.hideMask();//隐藏遮罩层
				if(me.saveError.length == 0){
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
					me.fireEvent('save',me);
				}else{
					JShell.Msg.error(me.saveError.join('</br>'));
				}
			}
		});
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
		
    	UnitItemCheckGrid.disableControl();//禁用所有的操作功能
    	ContractPriceAddForm.disableControl();//禁用所有的操作功能
    	
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
		
    	UnitItemCheckGrid.enableControl();//启用所有的操作功能
    	ContractPriceAddForm.enableControl();//启用所有的操作功能
    	
    	me.body.unmask();//隐藏遮罩层
	},
	/**获取对象数据*/
	getEntity:function(){
		var me = this,
			form = me.getComponent('ContractPriceAddForm'),
			values = form.getForm().getValues();
			
		var entity = {
			BeginDate:JShell.Date.toServerDate(values.DContractPrice_BeginDate),
			EndDate:JShell.Date.toServerDate(values.DContractPrice_EndDate),
			ContractNo:values.DContractPrice_ContractNo,
			BLaboratory:{
				Id:me.LaboratoryId,
				DataTimeStamp:me.LaboratoryDataTimeStamp.split(',')
			},
			BDealer:{
				Id:values.DContractPrice_BDealer_Id,
				DataTimeStamp:values.DContractPrice_BDealer_DataTimeStamp.split(',')
			},
			BBillingUnit:{
				Id:values.DContractPrice_BBillingUnit_Id,
				DataTimeStamp:values.DContractPrice_BBillingUnit_DataTimeStamp.split(',')
			}
		};
		//开票类型
		if(values.DContractPrice_BillingUnitType){
			entity.BillingUnitType = values.DContractPrice_BillingUnitType;
		}
		//科室
		if(values.DContractPrice_BDept_Id && values.DContractPrice_BDealer_DataTimeStamp){
			entity.BDept = {
				Id:values.DContractPrice_BDept_Id,
				DataTimeStamp:values.DContractPrice_BDept_DataTimeStamp.split(',')
			};
		}
		
		return entity;
	}
});