/**
 * 经销商销售表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.DealSellerForm',{
    extend:'Shell.ux.form.Panel',
    
    requires:['Shell.ux.form.field.BoolComboBox'],
    
    title:'经销商销售表单',
    width:430,
    height:280,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDDealSellerById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddDDealSeller',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateDDealSellerByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**经销商ID*/
	DealerId:null,
	/**经销商时间戳*/
	DealerDataTimeStamp:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initData();
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'DDealSeller_Id',hidden:true});
		
		items.push({
			x:0,y:10,width:410,fieldLabel:'销售',allowBlank:false,
			name:'DDealSeller_BDealer_Id',itemId:'DDealSeller_BDealer_Id',
			xtype:'combobox',queryMode:'local',editable:false,
			displayField:'BDealer_Name',valueField:'BDealer_Id',
			store:Ext.create('Ext.data.Store',{
				fields:['BDealer_Id','BDealer_Name','BDealer_DataTimeStamp'],
				data:[]
			})
		});
		
		items.push({x:0,y:40,fieldLabel:'开始日期',name:'DDealSeller_BeginDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		items.push({x:210,y:40,fieldLabel:'截止日期',name:'DDealSeller_EndDate',
			xtype:'datefield',format:'Y-m-d',allowBlank:false});
		
		items.push({x:0,y:70,width:410,fieldLabel:'说明',name:'DDealSeller_Explain'});
		items.push({x:0,y:100,width:410,height:80,xtype:'textarea',
			fieldLabel:'备注',name:'DDealSeller_Comment'});
		
		return items;
	},
	/**初始化数据*/
	initData:function(){
		var me = this,
			BDealer = me.getComponent('DDealSeller_BDealer_Id');
			
		var url = JShell.System.Path.ROOT + 
			'/BaseService.svc/ST_UDTO_SearchBSellerByHQL?isPlanish=true' +
			'&fields=BDealer_Id,BDealer_Name,BDealer_DataTimeStamp';
			
		me.showMask(me.loadingText);//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			var list = [];
			if(data.success){
				list = data.value.list;
			}
			BDealer.store.loadData(list);
			var value = BDealer.getValue();
			if(value) BDealer.setValue(value);
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			BeginDate:JShell.Date.toServerDate(values.DDealSeller_BeginDate),
			EndDate:JShell.Date.toServerDate(values.DDealSeller_EndDate),
			Explain:values.DDealSeller_Explain,
			Comment:values.DDealSeller_Explain,
			BDealer:{Id:me.DealerId},
			BSeller:{Id:values.DDealSeller_BDealer_Id}
		};
		
		if(me.DealerDataTimeStamp){
			entity.BDealer.DataTimeStamp = me.DealerDataTimeStamp.split(',');
		}
		
		var BDealer = me.getComponent('DDealSeller_BDealer_Id');
		var record = BDealer.store.findRecord('BDealer_Id',values.DDealSeller_BDealer_Id)
		if(record) entity.BSeller.DataTimeStamp = record.get('BDealer_DataTimeStamp').split(',');
		
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
		
		entity.entity.Id = values.DDealSeller_Id;
		return entity;
	}
});