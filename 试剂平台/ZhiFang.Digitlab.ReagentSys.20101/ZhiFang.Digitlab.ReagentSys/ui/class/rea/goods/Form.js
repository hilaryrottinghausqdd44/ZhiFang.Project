/**
 * 产品信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '产品信息',
	
	width:650,
    height:510,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchGoodsById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddGoods',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**机构信息*/
    CenOrg:{Id:'',Name:'',readOnly:false},
    /**供应商信息*/
    Comp:{Id:'',Name:'',readOnly:false},
    /**厂商信息*/
    Prod:{Id:'',Name:'',readOnly:false},
	
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
			
		//主键ID
		items.push({fieldLabel:'主键ID',name:'Goods_Id',hidden:true,type:'key'});
		
		//1,1
		//中文名
		items.push({
			x:5,y:5,
			fieldLabel:'中文名',name:'Goods_CName',
			emptyText:'必填项',allowBlank:false
		});
		//英文名
		items.push({
			x:5,y:30,
			fieldLabel:'英文名',name:'Goods_EName'
		});
		//代码
		items.push({
			x:5,y:55,
			fieldLabel:'代码',name:'Goods_ShortCode'
		});
		
		//1,2
		//机构
		items.push({
			x:205,y:5,
			fieldLabel:'机构',
			emptyText:'必填项',allowBlank:false,
			name:'Goods_CenOrg_CName',
			itemId:'Goods_CenOrg_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			readOnly:me.CenOrg.readOnly,
			value:me.CenOrg.Name,
			locked:me.CenOrg.readOnly
		},{
			fieldLabel:'机构主键ID',hidden:true,
			name:'Goods_CenOrg_Id',
			itemId:'Goods_CenOrg_Id',
			value:me.CenOrg.Id
		});
		//供应商
		items.push({
			x:205,y:30,
			fieldLabel:'供应商',
			emptyText:'必填项',allowBlank:false,
			name:'Goods_Comp_CName',
			itemId:'Goods_Comp_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			readOnly:me.Comp.readOnly,
			value:me.Comp.Name,
			locked:me.Comp.readOnly
		},{
			fieldLabel:'供应商主键ID',hidden:true,
			name:'Goods_Comp_Id',
			itemId:'Goods_Comp_Id',
			value:me.Comp.Id
		});
		//厂商
		items.push({
			x:205,y:55,
			fieldLabel:'厂商',
			emptyText:'必填项',allowBlank:false,
			name:'Goods_Prod_CName',
			itemId:'Goods_Prod_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			readOnly:me.Prod.readOnly,
			value:me.Prod.Name,
			locked:me.Prod.readOnly
		},{
			fieldLabel:'厂商主键ID',hidden:true,
			name:'Goods_Prod_Id',
			itemId:'Goods_Prod_Id',
			value:me.Prod.Id
		});
		
		//1,3
		//产品编号
		items.push({
			x:405,y:5,labelWidth:80,width:220,
			fieldLabel:'产品编号',name:'Goods_GoodsNo',
			emptyText:'产品编号,必填项',allowBlank:false
		});
		//供应商产品编号
		items.push({
			x:405,y:30,labelWidth:80,width:220,
			fieldLabel:'产品编号[供]',name:'Goods_CompGoodsNo',
			emptyText:'供应商产品编号'
		});
		//厂商产品编号
		items.push({
			x:405,y:55,labelWidth:80,width:220,
			fieldLabel:'产品编号[厂]',name:'Goods_ProdGoodsNo',
			emptyText:'厂商产品编号,必填项',allowBlank:false
		});
		
		//2,1
		//产地
		items.push({
			x:5,y:80,
			fieldLabel:'产地',name:'Goods_ProdEara'
		});
		//生产厂家
		items.push({
			x:5,y:105,
			fieldLabel:'生产厂家',name:'Goods_ProdOrgName'
		});
		
		//2,2
		//一级分类
		items.push({
			x:205,y:80,
			fieldLabel:'一级分类',name:'Goods_GoodsClass',
			emptyText:'必填项',allowBlank:false
		});
		//二级分类
		items.push({
			x:205,y:105,
			fieldLabel:'二级分类',name:'Goods_GoodsClassType',
			emptyText:'必填项',allowBlank:false
		});
		
		//2,3
		//超级管理员可以修改单位和单位描述
		var UnitReadOnly = true;
		var UnitLocked = true;
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == JShell.System.ADMINNAME){
			UnitReadOnly = false;
			UnitLocked = false;
		}
		//单位
		items.push({
			x:405,y:80,labelWidth:80,width:220,locked:UnitLocked,readOnly:UnitReadOnly,
			fieldLabel:'单位',name:'Goods_UnitName',itemId:'Goods_UnitName',
			emptyText:'必填项',allowBlank:false
		});
		//单位描述
		items.push({
			x:405,y:105,labelWidth:80,width:220,locked:UnitLocked,readOnly:UnitReadOnly,
			fieldLabel:'规格',name:'Goods_UnitMemo',itemId:'Goods_UnitMemo',
			emptyText:'必填项',allowBlank:false
		})
		
		//3
		//批准文号
		items.push({
			x:5,y:130,
			fieldLabel:'批准文号',name:'Goods_ApproveDocNo'
		});
		//招标号
		items.push({
			x:205,y:130,
			fieldLabel:'招标号',name:'Goods_BiddingNo'
		});
		//国标
		items.push({
			x:405,y:130,labelWidth:80,width:220,
			fieldLabel:'国标',name:'Goods_Standard'
		});
		
		//4
		//注册号
		items.push({
			x:5,y:155,
			fieldLabel:'注册号',name:'Goods_RegistNo'
		});
		//注册日期
		items.push({
			x:205,y:155,xtype:'datefield',format:'Y-m-d',
			fieldLabel:'注册日期',name:'Goods_RegistDate'
		});
		//注册证有效期
		items.push({
			x:405,y:155,xtype:'datefield',format:'Y-m-d',labelWidth:80,width:220,
			fieldLabel:'注册证有效期',name:'Goods_RegistNoInvalidDate'
		});
		
		//5
		//单价
		items.push({
			x:5,y:180,xtype:'numberfield',
			fieldLabel:'单价',name:'Goods_Price',
			emptyText:'必填项',allowBlank:false,
			decimalPrecision:4
		});
		//显示次序
		items.push({
			x:205,y:180,
			fieldLabel:'显示次序',name:'Goods_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		
		//是否使用
		items.push({
			x:405,y:180,labelWidth:80,width:220,
			fieldLabel:'启用',name:'Goods_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		});
		
		//有注册证
		items.push({
			x:5,y:205,
			fieldLabel:'有注册证',name:'Goods_IsRegister',
			xtype:'uxBoolComboBox',value:false,hasStyle:true
		});
		//条码类型
		items.push({
			x:205,y:205,
			fieldLabel:'条码类型',name:'Goods_BarCodeMgr',
			xtype:'uxSimpleComboBox',value:'0',hasStyle:true,
			data:[
				['0','批条码','color:green;'],
				['1','盒条码','color:orange;'],
				['2','无条码','color:black;']
			]
		});
		//是否打印条码
		items.push({
			x:405,y:205,labelWidth:80,width:220,
			fieldLabel:'是否打印条码',name:'Goods_IsPrintBarCode',
			xtype:'uxBoolComboBox',value:false,hasStyle:true
		});
		//适用机型
		items.push({
			x:5,y:230,
			fieldLabel:'适用机型',name:'Goods_SuitableType'
		});
		
		//6
		//储藏条件
		items.push({
			x:5,y:265,width:620,height:40,
			xtype:'textarea',fieldLabel:'储藏条件',
			name:'Goods_StorageType'
		});
		//结构组成
		items.push({
			x:5,y:310,width:620,height:40,
			xtype:'textarea',fieldLabel:'结构组成',
			name:'Goods_Constitute'
		});
		//用途
		items.push({
			x:5,y:355,width:620,height:40,
			xtype:'textarea',fieldLabel:'用途',
			name:'Goods_Purpose'
		});
		//货品描述
		items.push({
			x:5,y:400,width:620,height:40,
			xtype:'textarea',fieldLabel:'货品描述',
			name:'Goods_GoodsDesc'
		});
		
		return items;
	},
	/**重写新增方法*/
	isAdd:function(){
		var me = this;
		
		//当新增时，单位、单位描述可编辑
		me.getComponent('Goods_UnitName').setReadOnly(false);
		me.getComponent('Goods_UnitMemo').setReadOnly(false);
		
		me.callParent(arguments);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.Goods_CName,
			EName:values.Goods_EName,
			ShortCode:values.Goods_ShortCode,
			
			CenOrg:{Id:values.Goods_CenOrg_Id},
			Comp:{Id:values.Goods_Comp_Id},
			Prod:{Id:values.Goods_Prod_Id},
			
			GoodsNo:values.Goods_GoodsNo,
			CompGoodsNo:values.Goods_CompGoodsNo,
			ProdGoodsNo:values.Goods_ProdGoodsNo,
			
			ProdEara:values.Goods_ProdEara,
			ProdOrgName:values.Goods_ProdOrgName,
			
			GoodsClass:values.Goods_GoodsClass,
			GoodsClassType:values.Goods_GoodsClassType,
			
			UnitName:values.Goods_UnitName,
			UnitMemo:values.Goods_UnitMemo,
			
			ApproveDocNo:values.Goods_ApproveDocNo,
			BiddingNo:values.Goods_BiddingNo,
			Standard:values.Goods_Standard,
			
			RegistNo:values.Goods_RegistNo,
			RegistDate:JShell.Date.toServerDate(values.Goods_RegistDate),
			RegistNoInvalidDate:JShell.Date.toServerDate(values.Goods_RegistNoInvalidDate),
			
			Price:values.Goods_Price == "" ? null : values.Goods_Price,
			DispOrder:values.Goods_DispOrder,
			Visible:values.Goods_Visible ? 1 : 0,
			
			StorageType:values.Goods_StorageType,
			Constitute:values.Goods_Constitute,
			Purpose:values.Goods_Purpose,
			GoodsDesc:values.Goods_GoodsDesc,
			BarCodeMgr:values.Goods_BarCodeMgr,
			IsRegister:values.Goods_IsRegister ? 1 : 0,
			IsPrintBarCode:values.Goods_IsPrintBarCode ? 1 : 0,
			SuitableType:values.Goods_SuitableType
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = [
			'Id','CName','EName','ShortCode',
			'CenOrg_Id','Comp_Id','Prod_Id',
			'GoodsNo','CompGoodsNo','ProdGoodsNo',
			'ProdEara','ProdOrgName',
			'GoodsClass','GoodsClassType',
			'UnitName','UnitMemo',
			'ApproveDocNo','BiddingNo','Standard',
			'RegistNo','RegistDate','RegistNoInvalidDate',
			'Price','DispOrder','Visible',
			'StorageType','Constitute','Purpose','GoodsDesc',
			'BarCodeMgr','IsRegister','IsPrintBarCode','SuitableType'
		];
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.Goods_Id;
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			CenOrg_CName = me.getComponent('Goods_CenOrg_CName'),
			CenOrg_Id = me.getComponent('Goods_CenOrg_Id'),
			Comp_CName = me.getComponent('Goods_Comp_CName'),
			Comp_Id = me.getComponent('Goods_Comp_Id'),
			Prod_CName = me.getComponent('Goods_Prod_CName'),
			Prod_Id = me.getComponent('Goods_Prod_Id');
		
		CenOrg_CName.on({
			check: function(p, record) {
				CenOrg_CName.setValue(record ? record.get('CenOrg_CName') : '');
				CenOrg_Id.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		Comp_CName.on({
			check: function(p, record) {
				Comp_CName.setValue(record ? record.get('CenOrg_CName') : '');
				Comp_Id.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		Prod_CName.on({
			check: function(p, record) {
				Prod_CName.setValue(record ? record.get('CenOrg_CName') : '');
				Prod_Id.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.Goods_Visible = data.Goods_Visible == '1' ? true : false;
		data.Goods_IsRegister = data.Goods_IsRegister == '1' ? true : false;
		data.Goods_IsPrintBarCode = data.Goods_IsPrintBarCode == '1' ? true : false;
		data.Goods_RegistDate = JShell.Date.getDate(data.Goods_RegistDate);
		data.Goods_RegistNoInvalidDate = JShell.Date.getDate(data.Goods_RegistNoInvalidDate);
		return data;
	}
});