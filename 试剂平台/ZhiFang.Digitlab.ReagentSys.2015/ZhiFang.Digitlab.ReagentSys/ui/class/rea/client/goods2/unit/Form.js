/**
 * 单位换算表单
 * @author liangyl
 * @version 2017-10-12
 */
Ext.define('Shell.class.rea.client.goods2.unit.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '单位换算',
	
	width:240,
    height:185,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsUnit',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnitByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 5px 0px 0px',
	/**布局方式*/
	layout:'anchor',
	
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:70,
        labelAlign:'right'
    },
    /**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	PK:null,
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
		
		items.push({fieldLabel:'主键ID',name:'ReaGoodsUnit_Id',hidden:true,type:'key'});
		
		//主单位
		items.push({
			fieldLabel:'主单位',name:'ReaGoodsUnit_GoodsUnit',
			emptyText:'必填项',allowBlank:false
		});
		items.push({
			fieldLabel:'次单位',
			name:'ReaGoodsUnit_ChangeUnit',itemId:'ReaGoodsUnit_ChangeUnit',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.client.goods2.unit.CheckGrid',
			classConfig:{
				title:'单位选择',
				/**是否单选*/
	            checkOne:true
	
			}
		},{
			fieldLabel:'次单位主键ID',hidden:true,name:'ReaDeptGoods_ReaGoodsUnit_Id',itemId:'ReaDeptGoods_ReaGoodsUnit_Id'
		});
		//换算比列
		items.push({
			xtype:'numberfield',fieldLabel:'换算比列',name:'ReaGoods_ChangeQty',
			emptyText:'必填项',allowBlank:false,decimalPrecision:4,value:0
		});
	
		//启用
		items.push({
			fieldLabel:'启用',name:'ReaGoodsUnit_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		});
		//换算比列
		items.push({
			xtype:'numberfield',
			fieldLabel:'显示次序',name:'ReaGoodsUnit_DispOrder',hidden:true,
			emptyText:'必填项',allowBlank:false,value:0
		});
		//包装单位描述
		items.push({
		    height:40,xtype:'textarea',fieldLabel:'货品描述',hidden:true,
			fieldLabel:'描述',name:'ReaGoodsUnit_Memo',emptyText:'包装单位描述'
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			GoodsUnit:values.ReaGoodsUnit_GoodsUnit,
			ChangeUnit:values.ReaGoodsUnit_ChangeUnit,
			ChangeQty:values.ReaGoodsUnit_ChangeQty,
//			DispOrder:values.ReaGoodsUnit_DispOrder,
			Visible:values.ReaGoodsUnit_Visible ? 1 : 0
//			Memo:values.ReaGoodsUnit_Memo
			
		};
		//货品id
		if(me.GoodsID){
			entity.ReaGoods = {
				Id:me.GoodsID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.GoodsCName=me.GoodsCName;
		}
		//包装单位
		if(values.ReaDeptGoods_ReaGoodsUnit_Id){
			entity.ReaGoodsUnit = {
				Id:values.ReaDeptGoods_ReaGoodsUnit_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.GoodsCName=me.GoodsCName;
		}
		//创建者信息
		var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) ;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreatorID = userId;
		}
		if(userName){
			entity.CreatorName = userName;
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','GoodsID','Visible','GoodsUnit','Memo','ChangeQty','ChangeUnit'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.ReaGoodsUnit_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.ReaGoodsUnit_Visible = data.ReaGoodsUnit_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var GoodsUnitId=me.getComponent('ReaDeptGoods_ReaGoodsUnit_Id');
		var ChangeUnit=me.getComponent('ReaGoodsUnit_ChangeUnit');
        ChangeUnit.on({
        	check: function(p, record) {
				ChangeUnit.setValue(record ? record.get('ReaGoodsUnit_GoodsUnit') : '');
				GoodsUnitId.setValue(record ? record.get('ReaGoodsUnit_Id') : '');
				p.close();
			}
        });
	},
	/**更改标题*/
	changeTitle:function(){
	}
});