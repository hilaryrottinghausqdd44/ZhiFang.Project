/**
 * 经销商项目阶梯价格表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.stepprice.Form',{
    extend:'Shell.ux.form.Panel',
    
    title:'经销商阶梯价格表单',
    date_error:'开始和截止时间保证为整月!',
    width:430,
    height:170,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDStepPriceById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddDStepPrice',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateDStepPriceByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**经销商ID*/
	DealerId:null,
	/**经销商时间戳*/
	DealerDataTimeStamp:null,
	/**项目ID*/
	ItemId:null,
	/**项目时间戳*/
	ItemDataTimeStamp:null,
	
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
		
		items.push({fieldLabel:'主键ID',name:'DStepPrice_Id',hidden:true});
		
		items.push({x:0,y:10,fieldLabel:'开始日期',name:'DStepPrice_BeginDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'截止日期',name:'DStepPrice_EndDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		
		items.push({x:0,y:40,fieldLabel:'数量要求',name:'DStepPrice_SampleCount',
			xtype:'numberfield',allowBlank:false});
		items.push({x:210,y:40,fieldLabel:'价格',name:'DStepPrice_StepPrice',
			xtype:'numberfield',allowBlank:false});
		
		items.push({x:0,y:70,width:410,fieldLabel:'合同编号',name:'DStepPrice_ContractNo'});
		
		items.push({fieldLabel:'经销商ID',name:'DStepPrice_BDealer_Id',hidden:true});
		items.push({fieldLabel:'项目ID',name:'DStepPrice_BTestItem_Id',hidden:true});
//		items.push({fieldLabel:'经销商时间戳',name:'DStepPrice_Dealer_DataTimeStamp',hidden:true});
//		items.push({fieldLabel:'项目时间戳',name:'DStepPrice_BTestItem_DataTimeStamp',hidden:true});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var isFullMonth = JShell.Date.isFullMonth(values.DStepPrice_BeginDate,values.DStepPrice_EndDate);
		
		if(!isFullMonth){
			JShell.Msg.error(me.date_error);
			return;
		}
			
		var entity = {
			BeginDate:JShell.Date.toServerDate(values.DStepPrice_BeginDate),
			EndDate:JShell.Date.toServerDate(values.DStepPrice_EndDate),
			SampleCount:values.DStepPrice_SampleCount,
			StepPrice:values.DStepPrice_StepPrice,
			ContractNo:values.DStepPrice_ContractNo,
			BDealer:{Id:me.DealerId},
			BTestItem:{Id:me.ItemId},
			StepPriceMemo: '数量＞' + values.DStepPrice_SampleCount + 
				'时，价格=' + values.DStepPrice_StepPrice + '元'
		};
		
		if(me.DealerDataTimeStamp){
			entity.BDealer.DataTimeStamp = me.DealerDataTimeStamp.split(',');
		}
		if(me.ItemDataTimeStamp){
			entity.BTestItem.DataTimeStamp = me.ItemDataTimeStamp.split(',');
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
			
		if(!entity) return;
		
		for(var i in fields){
			fields[i] = fields[i].split('_').slice(1).join('_');
		}
		entity.fields = fields.join(',') + ',StepPriceMemo,ConfirmUser,ConfirmTime';
		
		entity.entity.Id = values.DStepPrice_Id;
		
		entity.entity.BDealer.Id = values.DStepPrice_BDealer_Id;
		entity.entity.BTestItem.Id = values.DStepPrice_BTestItem_Id;
		
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.DStepPrice_BeginDate = JShell.Date.getDate(data.DStepPrice_BeginDate);
		data.DStepPrice_EndDate = JShell.Date.getDate(data.DStepPrice_EndDate);
		return data;
	}
});