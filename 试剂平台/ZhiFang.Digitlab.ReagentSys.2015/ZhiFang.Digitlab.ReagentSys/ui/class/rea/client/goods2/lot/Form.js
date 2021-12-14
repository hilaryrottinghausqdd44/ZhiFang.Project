/**
 * 批次信息
 * @author liangyl
 * @version 2017-10-12
 */
Ext.define('Shell.class.rea.client.goods2.lot.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '批次信息',
	
	width:240,
    height:185,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,

	/**内容周围距离*/
	bodyPadding:'10px 15px 0px 0px',
	/**布局方式*/
	layout:'anchor',
	
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
    /**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	
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
		
		items.push({fieldLabel:'主键ID',name:'ReaGoodsLot_Id',hidden:true,type:'key'});

		//批次号
		items.push({
			fieldLabel:'批次号',name:'ReaGoodsLot_LotNo',
			emptyText:'必填项',allowBlank:false
		});
		//有效期
		items.push({
			fieldLabel: '有效期',
			width: me.defaults.width * 1,
			name: 'ReaGoodsLot_InvalidDate',
			itemId: 'ReaGoodsLot_InvalidDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		});
//		//价格
//		items.push({
//			xtype:'numberfield',
//			fieldLabel:'价格',name:'Price',
//			emptyText:'必填项',allowBlank:false,
//			decimalPrecision:4,value:0
//		});
		//启用
		items.push({
			fieldLabel:'启用',name:'ReaGoodsLot_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			
//			ReaGoodsLotType:{
//				Id:values.ReaGoodsLot_ReaGoodsLotType_Id
//			},
//			OrgNo:values.ReaGoodsLot_OrgNo,
//			CName:values.ReaGoodsLot_CName,
//			EName:values.ReaGoodsLot_EName,
//			ServerIP:values.ReaGoodsLot_ServerIP,
//			ServerPort:values.ReaGoodsLot_ServerPort,
//			ShortCode:values.ReaGoodsLot_ShortCode,
//			DispOrder:values.ReaGoodsLot_DispOrder,
//			Visible:values.ReaGoodsLot_Visible ? 1 : 0,
//			Address:values.ReaGoodsLot_Address,
//			Contact:values.ReaGoodsLot_Contact,
//			Tel:values.ReaGoodsLot_Tel,
//			Fox:values.ReaGoodsLot_Fox,
//			Email:values.ReaGoodsLot_Email,
//			WebAddress:values.ReaGoodsLot_WebAddress,
//			Memo:values.ReaGoodsLot_Memo
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id',
//				'CName','EName','ServerIP','ServerPort','ShortCode','OrgNo',
//				'DispOrder','Visible','Address','Contact','Tel','Fox','Email',
//				'WebAddress','Memo','ReaGoodsLotType_Id'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.ReaGoodsLot_Id;
		//delete entity.entity.OrgNo;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.ReaGoodsLot_Visible = data.ReaGoodsLot_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle:function(){
	}
});