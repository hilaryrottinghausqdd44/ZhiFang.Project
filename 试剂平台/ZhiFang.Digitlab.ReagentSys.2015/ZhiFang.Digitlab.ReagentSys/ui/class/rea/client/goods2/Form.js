/**
 * 产品信息
 * @author liangyl
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goods2.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '产品信息',
	
	width:670,
    height:510,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoods',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 210,
		labelAlign: 'right'
	},
	/**内容周围距离*/
	bodyPadding:'10px',
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
		items.push({fieldLabel:'主键ID',name:'ReaGoods_Id',hidden:true,type:'key'});
		//1,1
        items.push({
			fieldLabel:'产品名称',name:'ReaGoods_CName',
			emptyText:'必填项',allowBlank:false,
			colspan: 1,width: me.defaults.width * 1
		});
		 //英文名
		items.push({
			fieldLabel:'英文名称',name:'ReaGoods_EName',
			colspan: 1,width: me.defaults.width * 1
		});
		//简称
		items.push({
			fieldLabel:'简称',emptyText:'简称',name:'ReaGoods_SName',
			colspan: 1,width: me.defaults.width * 1
		});
	    //PinYinZiTou
		items.push({
			fieldLabel:'拼音字头',name:'ReaGoods_PinYinZiTou',
			colspan: 2,width: me.defaults.width * 1,hidden:true
		});
		//1,3
		items.push({
			fieldLabel:'产品编号',name:'ReaGoods_GoodsNo',
//			emptyText:'货品编号,必填项',allowBlank:false,
			colspan: 1,width: me.defaults.width * 1
		});
       //厂商产品编号
		items.push({
			fieldLabel:'厂商货品编号',name:'ReaGoods_ProdGoodsNo',
//			emptyText:'厂商货品编号,产品必填项',allowBlank:false,
			colspan: 1,width: me.defaults.width * 1
		});
//		//顺序号
//		items.push({
//			fieldLabel:'顺序号',name:'ReaGoods_GoodsSN',
//		    locked:true,readOnly:true,
//			colspan: 1,width: me.defaults.width * 1
//		});
        //批准文号
		items.push({
			fieldLabel:'批准文号',name:'ReaGoods_ApproveDocNo',
			colspan: 1,width: me.defaults.width * 1
		});
		//国标
		items.push({
			fieldLabel:'国标',name:'ReaGoods_Standard',
			colspan: 1,width: me.defaults.width * 1
		});
		//产地
		items.push({
			fieldLabel:'产地',name:'ReaGoods_ProdEara',
			colspan: 1,width: me.defaults.width * 1
		});
		//单位
		items.push({
			fieldLabel:'单位',name:'ReaGoods_UnitName',itemId:'ReaGoods_UnitName',
			emptyText:'必填项',allowBlank:false,
			colspan: 1,width: me.defaults.width * 1
		});//单位描述
		items.push({
			fieldLabel:'规格',name:'ReaGoods_UnitMemo',itemId:'ReaGoods_UnitMemo',
			emptyText:'必填项',allowBlank:false,
			colspan:1,width: me.defaults.width * 1
		});
		//单价
		items.push({
			xtype:'numberfield',
			fieldLabel:'推荐价格',name:'ReaGoods_Price',
//			emptyText:'必填项',allowBlank:false,
			decimalPrecision:4,value:0,	
			colspan: 1,width: me.defaults.width * 1
		});
		//一级分类
		items.push({
			x:205,y:110,
			fieldLabel:'一级分类',name:'ReaGoods_GoodsClass',
			colspan: 1,width: me.defaults.width * 1
		});
		//二级分类
		items.push({
			x:205,y:135,
			fieldLabel:'二级分类',name:'ReaGoods_GoodsClassType',
			colspan: 1,width: me.defaults.width * 1
		});
		//测试数
		items.push({
			x:405,y:135,
			fieldLabel:'测试数',name:'ReaGoods_TestCount',
			xtype:'numberfield',value:0,
			colspan: 1,width: me.defaults.width * 1
		});
		//代码
		items.push({
			fieldLabel:'同系列码',emptyText:'标识货品为同系列货品',name:'ReaGoods_ShortCode',
			colspan: 1,width: me.defaults.width * 1,hidden:false
		});
		//显示次序
		items.push({
			x:205,y:185,
			fieldLabel:'显示次序',name:'ReaGoods_DispOrder',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0,
			colspan: 1,width: me.defaults.width * 1
		});
		//是否使用
		items.push({
			x:405,y:185,labelWidth:80,width:220,
			fieldLabel:'启用',name:'ReaGoods_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true,
			colspan: 1,width: me.defaults.width * 1
		});
		//有注册证
		items.push({
			x:5,y:210,
			fieldLabel:'有注册证',name:'ReaGoods_IsRegister',
			xtype:'uxBoolComboBox',value:false,hasStyle:true,
			colspan: 1,width: me.defaults.width * 1
		});
		//条码类型
		items.push({
			x:205,y:210,
			fieldLabel:'条码类型',name:'ReaGoods_BarCodeMgr',
			xtype:'uxSimpleComboBox',value:'0',hasStyle:true,
			data:[
				['0','批条码','color:green;'],
				['1','盒条码','color:orange;'],
				['2','无条码','color:black;']
			],
			colspan: 1,width: me.defaults.width * 1
		});
		//是否打印条码
		items.push({
			x:405,y:210,labelWidth:80,width:220,
			fieldLabel:'是否打印条码',name:'ReaGoods_IsPrintBarCode',
			xtype:'uxBoolComboBox',value:false,hasStyle:true,
			colspan: 2,width: me.defaults.width * 1
		});
		
		//注册号
        items.push({
			fieldLabel:'注册号',name:'ReaGoods_RegistNo',
			colspan: 1,width: me.defaults.width * 1
		});
		 //注册日期
		items.push({
			fieldLabel:'注册日期',name:'ReaGoods_RegistDate',
			xtype: 'datefield',format: 'Y-m-d',colspan: 1,width: me.defaults.width * 1
		});
		//注册证有效期
		items.push({
			fieldLabel:'注册证有效期',emptyText:'注册证有效期',name:'ReaGoods_RegistNoInvalidDate',
			xtype: 'datefield',format: 'Y-m-d',colspan: 1,width: me.defaults.width * 1
		});
		
		
		//6
		//适用机型
		items.push({
			x:5,y:245,width:620,
			fieldLabel:'适用机型',
			name:'ReaGoods_SuitableType',
			colspan: 3,width: me.defaults.width * 3
		});
		//储藏条件
		items.push({
			x:5,y:245,width:620,height:40,
			xtype:'textarea',fieldLabel:'储藏条件',
			name:'ReaGoods_StorageType',
			colspan: 3,width: me.defaults.width * 3
		});
		//结构组成
		items.push({
			x:5,y:290,width:620,height:40,
			xtype:'textarea',fieldLabel:'结构组成',
			name:'ReaGoods_Constitute',
			colspan: 3,width: me.defaults.width * 3
		});
		//用途
		items.push({
			x:5,y:335,width:620,height:40,
			xtype:'textarea',fieldLabel:'用途',
			name:'ReaGoods_Purpose',
			colspan: 3,width: me.defaults.width * 3
		});
		
		//货品描述
		items.push({
			x:5,y:380,width:620,height:40,
			xtype:'textarea',fieldLabel:'货品描述',
			name:'ReaGoods_GoodsDesc',
			colspan: 3,width: me.defaults.width * 3
		});
		
		return items;
	},
	

	isAdd:function(){
		var me = this;
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
	},
	isEdit:function(id){
		var me = this;
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.load(id);
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName:values.ReaGoods_CName,
			EName:values.ReaGoods_EName,
			ShortCode:values.ReaGoods_ShortCode,
            SName:values.ReaGoods_SName,
			GoodsNo:values.ReaGoods_GoodsNo,
			ProdGoodsNo:values.ReaGoods_ProdGoodsNo,
			ProdEara:values.ReaGoods_ProdEara,
			GoodsClass:values.ReaGoods_GoodsClass,
			GoodsClassType:values.ReaGoods_GoodsClassType,
			UnitName:values.ReaGoods_UnitName,
			UnitMemo:values.ReaGoods_UnitMemo,
			ApproveDocNo:values.ReaGoods_ApproveDocNo,
			Standard:values.ReaGoods_Standard,
			StorageType:values.ReaGoods_StorageType,
			Price:values.ReaGoods_Price == "" ? null : values.ReaGoods_Price,
			DispOrder:values.ReaGoods_DispOrder,
			Visible:values.ReaGoods_Visible ? 1 : 0,
			TestCount:values.ReaGoods_TestCount,
			SuitableType:values.ReaGoods_SuitableType,
			Constitute:values.ReaGoods_Constitute,
			Purpose:values.ReaGoods_Purpose,
			GoodsDesc:values.ReaGoods_GoodsDesc,
			BarCodeMgr:values.ReaGoods_BarCodeMgr,
			IsRegister:values.ReaGoods_IsRegister ? 1 : 0,
			IsPrintBarCode:values.ReaGoods_IsPrintBarCode ? 1 : 0,
			RegistNo:values.ReaGoods_RegistNo,
		    RegistDate : JShell.Date.toServerDate(values.ReaGoods_RegistDate),
		    RegistNoInvalidDate : JShell.Date.toServerDate(values.ReaGoods_RegistNoInvalidDate)
		};
		
		
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
	
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = [
			'Id','CName','EName','ShortCode',
			'GoodsNo','ProdGoodsNo',
			'ProdEara','SName',
			'GoodsClass','GoodsClassType',
			'UnitName','UnitMemo',
			'ApproveDocNo','Standard',
			'Price','DispOrder','Visible',
			'StorageType','Constitute','Purpose','GoodsDesc',
			'BarCodeMgr','IsRegister','IsPrintBarCode',
            'TestCount','SuitableType',
            'RegistNo','RegistDate','RegistNoInvalidDate'
		];
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.ReaGoods_Id;
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.ReaGoods_Visible = data.ReaGoods_Visible == '1' ? true : false;
		data.ReaGoods_IsRegister = data.ReaGoods_IsRegister == '1' ? true : false;
		data.ReaGoods_IsPrintBarCode = data.ReaGoods_IsPrintBarCode == '1' ? true : false;
		data.ReaGoods_RegistDate = JShell.Date.getDate(data.ReaGoods_RegistDate);
		data.ReaGoods_RegistNoInvalidDate = JShell.Date.getDate(data.ReaGoods_RegistNoInvalidDate);
		return data;
	},
	/**更改标题*/
	changeTitle:function(){
	},
	/**重新保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(me.formtype == 'add'){
					id=data.value.id;
					me.PK=id;
				}
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
	
});