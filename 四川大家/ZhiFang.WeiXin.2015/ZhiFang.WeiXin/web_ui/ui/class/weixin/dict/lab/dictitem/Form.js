/**
 * 项目表单
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'项目信息',
    width:640,
	height:400,
	bodyPadding:'10px 5px 0px 0px',
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddBLabTestItem',
    /**修改服务地址*/
    editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByField', 
    /**颜色服务地址*/
    selectColorUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictByHQL?isPlanish=true',
    /**获取数据服务路径(校验)*/
    selectUrl2:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?isPlanish=true',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	border:true,
    layout:{
        type:'table',
        columns:3//每行有几列
    },
    buttonDock:'top',
	/**每个组件的默认属性*/
	defaults: {
//		anchor: '100%',
        width:220,
		labelWidth: 105,
		labelAlign: 'right'
	},
	/**保密等级*/
	SecretgradeList:[
	    ['1', '保密'],
	     ['0', '不保密']
	],
	/**提示等级*/
	CuegradeList:[
	    ['0', '普通'],
	    ['1', '特殊']
	],
	/**保密等级默认值*/
	DefaluteSecretgrade:'0',
	/**提示等级默认值*/
	DefautleCuegrade:'0',
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	/**三甲价格是否只读,用于判断是否联动*/
	IsMarketPriceReadOnly:'0',
	/**是否需要根据项目价格联动修改项目折扣率*/
	IsPriceLoad:true,
	/**是否需要根据项目折扣率联动修改项目价格*/
	IsDiscountPrice:true,
	/**是否需要根据内部价格联动修改内部价格折扣率*/
	IsGreatMasterPriceLoad:true,
	/**是否需要内部价格折扣率联动修改内部价格*/
	IsDiscountGreatMasterPrice:true,
	/**是否需要根据三甲价格联动计算*/
	IsMarketPrice:true,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.PK){
			me.disableTool(true);
		}else{
			me.disableTool(false);
		}
		//表单监听
		me.initFromListeners();
	},
    initComponent:function(){
		var me = this;
		me.addEvents('changeResult','load','beforesave','save','saveerror','onSaveClick');
		me.addEvents('changeDiscountGreatMasterPriceClick','changeGreatMasterPriceClick',
		'changeDiscountPriceClick','changePriceClick',
		'beforePriceChange','beforeGreatMasterPriceChange',
		'beforeMarketPriceChange');
		me.defaultTitle = me.title;
		me.items = me.items || me.createItems();
		me._thisfields = [];
		me.initPKField();//初始化主键字段
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0){
			me.dockedItems = dockedItems;
		}
		
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		var items = [{
			fieldLabel:'项目编号',emptyText:'必填项',allowBlank:false,
			colspan:1,width: me.defaults.width * 1,
			name:'BLabTestItem_ItemNo',itemId:'BLabTestItem_ItemNo'
		},{ fieldLabel:'实验室编码',name:'BLabTestItem_LabCode',
		    itemId:'BLabTestItem_LabCode',hidden:true,
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'标准代码',name:'BLabTestItem_StandardCode',
		    colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否医嘱项目',name:'BLabTestItem_IsDoctorItem',
				xtype: 'uxBoolComboBox',value: true,itemId:'BLabTestItem_IsDoctorItem'
		},{
			fieldLabel:'项目中文名称',name:'BLabTestItem_CName',
			 emptyText:'必填项',allowBlank:false, colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'项目简码',name:'BLabTestItem_ShortCode',colspan:1,
		    emptyText:'必填项',allowBlank:false,width: me.defaults.width * 1
		},{
			fieldLabel:'是否收费项目',name:'BLabTestItem_IschargeItem',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'项目英文名称',
			name:'BLabTestItem_EName',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel: '项目简称',name: 'BLabTestItem_ShortName',
			emptyText:'必填项',allowBlank:false,colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否是组套项目',name:'BLabTestItem_IsCombiItem',
			xtype: 'uxBoolComboBox',value: true,itemId:'BLabTestItem_IsCombiItem'
		}, {
			fieldLabel:'成本价格',name:'BLabTestItem_CostPrice',
			xtype:'numberfield',decimalPrecision: 4,minValue:0,
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'检验大组',name:'BLabTestItem_LabSuperGroupNo',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否是组合项目',name:'BLabTestItem_IsProfile',
			xtype: 'uxBoolComboBox',value: true,itemId:'BLabTestItem_IsProfile'
		},{
			fieldLabel:'项目价格',name:'BLabTestItem_Price',colspan:1,
			xtype:'numberfield',decimalPrecision: 4,minValue:0,
			width: me.defaults.width * 1,itemId:'BLabTestItem_Price'
		}, {
			fieldLabel:'单位',name:'BLabTestItem_Unit',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否计算项目',name:'BLabTestItem_IsCalc',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'项目价格折扣率%',name:'BLabTestItem_DiscountPrice',
			xtype:'numberfield',decimalPrecision: 4,minValue:0,
			colspan:1,width: me.defaults.width * 1,value:0,
			itemId:'BLabTestItem_DiscountPrice'
		}, {
			fieldLabel:'精度',xtype:'numberfield', name:'BLabTestItem_Prec',
			minValue:0,colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel: '提示等级',name: 'BLabTestItem_Cuegrade',
			itemId: 'BLabTestItem_Cuegrade', xtype: 'uxSimpleComboBox',hasStyle: true,
			value: me.DefautleCuegrade,data: me.CuegradeList,
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'三甲价格',name:'BLabTestItem_MarketPrice',
			xtype:'numberfield',decimalPrecision: 4,minValue:0,
			colspan:1,width: me.defaults.width * 1,value:0,
			itemId:'BLabTestItem_MarketPrice'
		},{
			fieldLabel:'检验方法',name:'BLabTestItem_DiagMethod',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel: '保密等级',name: 'BLabTestItem_Secretgrade',
			itemId: 'BLabTestItem_Secretgrade', xtype: 'uxSimpleComboBox',
			hasStyle: true,value: me.DefaluteSecretgrade,data: me.SecretgradeList,
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'咨询费',name:'BLabTestItem_BonusPercent',
		    xtype:'numberfield',decimalPrecision: 4,
		    minValue:0,colspan:1,width: me.defaults.width * 1
		},
		{fieldLabel:'颜色ID',hidden:true,xtype:'textfield',
			name:'BLabTestItem_ColorID',itemId:'BLabTestItem_ColorID'
	    },{
			fieldLabel: '颜色',xtype: 'uxCheckTrigger',emptyText: '',
			name: 'BLabTestItem_Color',itemId: 'BLabTestItem_Color',
			className: 'Shell.class.weixin.dict.core.itemallitem.ColorCheckGrid',
			classConfig: {
				title:'颜色选择',
				/**是否单选*/
	            checkOne:true
			},
			listeners: {
				check: function(p, record) {
					me.onColorAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel:'是否显示',name:'BLabTestItem_Visible',
				xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'内部价格',name:'BLabTestItem_GreatMasterPrice',colspan:1,
			xtype:'numberfield',decimalPrecision: 4,minValue:0,value:0,
//          regex: /^\d+(?=\.{0,1}\d+$|$)/,invalidText:'请输入有效数字',
		    width: me.defaults.width * 1,itemId:'BLabTestItem_GreatMasterPrice'
		},{
			fieldLabel:'工作量参数',value:0,name:'BLabTestItem_FWorkLoad',
		   minValue:0, decimalPrecision: 4,xtype:'numberfield',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'显示次序',xtype:'numberfield', name:'BLabTestItem_DispOrder',
			value:0,colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'内部价格折扣率%',name:'BLabTestItem_DiscountGreatMasterPrice',
//			emptyText: '百分率在0-100之间',
			minValue:0,itemId:'BLabTestItem_DiscountGreatMasterPrice',
			xtype:'numberfield',decimalPrecision: 4,value:0,
//          regex: /^\d+(?=\.{0,1}\d+$|$)/,invalidText:'请输入有效数字',
			colspan:1,width: me.defaults.width * 1,itemId:'BLabTestItem_DiscountGreatMasterPrice'
		},{
			fieldLabel:'对应医嘱系统编码',name:'BLabTestItem_OrderNo',colspan:1,
			width: me.defaults.width * 1,itemId:'BLabTestItem_OrderNo'
		},
		{
			fieldLabel:'是否使用',name:'BLabTestItem_UseFlag',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'项目说明',height:40,//labelAlign:'top',
				name:'BLabTestItem_ItemDesc',xtype:'textarea',
				colspan:3,width: me.defaults.width * 3
		},{fieldLabel:'主键ID',name:'BLabTestItem_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			ItemNo:values.BLabTestItem_ItemNo,
			LabCode:values.BLabTestItem_LabCode,
			IsDoctorItem:values.BLabTestItem_IsDoctorItem ? 1 : 0,
			CName:values.BLabTestItem_CName,
			StandardCode:values.BLabTestItem_StandardCode,
			IschargeItem:values.BLabTestItem_IschargeItem? 1 : 0,
			EName:values.BLabTestItem_EName,
			ShortCode:values.BLabTestItem_ShortCode,
			IsCombiItem:values.BLabTestItem_IsCombiItem ? 1 : 0,
			ShortName:values.BLabTestItem_ShortName,
			IsProfile:values.BLabTestItem_IsProfile ? 1 : 0,
			Unit:values.BLabTestItem_Unit,
			IsCalc:values.BLabTestItem_IsCalc ? 1 : 0,
			Cuegrade:values.BLabTestItem_Cuegrade,
			DiagMethod:values.BLabTestItem_DiagMethod,
			Secretgrade:values.BLabTestItem_Secretgrade,
			Visible:values.BLabTestItem_Visible ? 1 : 0,
			UseFlag:values.BLabTestItem_UseFlag ? 1 : 0,
			OrderNo:values.BLabTestItem_OrderNo,
			ItemDesc:values.BLabTestItem_ItemDesc
		};
		if(values.BLabTestItem_CostPrice){
			entity.CostPrice=values.BLabTestItem_CostPrice;
		}
		if(values.BLabTestItem_Color){
			entity.Color=values.BLabTestItem_Color;
		}
		if(values.BLabTestItem_LabSuperGroupNo){
			entity.LabSuperGroupNo=values.BLabTestItem_LabSuperGroupNo;
		}
		if(values.BLabTestItem_FWorkLoad){
			entity.FWorkLoad=values.BLabTestItem_FWorkLoad;
		}
		if(values.BLabTestItem_DispOrder){
			entity.DispOrder=values.BLabTestItem_DispOrder;
		}
		if(values.BLabTestItem_Price){
			entity.Price=values.BLabTestItem_Price;
		}
		if(values.BLabTestItem_MarketPrice){
			entity.MarketPrice=values.BLabTestItem_MarketPrice;
		}
		if(values.BLabTestItem_GreatMasterPrice){
			entity.GreatMasterPrice=values.BLabTestItem_GreatMasterPrice;
		}
		if(values.BLabTestItem_BonusPercent){
			entity.BonusPercent=values.BLabTestItem_BonusPercent;
		}
	    if(values.BLabTestItem_Prec){
			entity.Prec=values.BLabTestItem_Prec;
		}
	    return entity;
//		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
//			fields = me.getStoreFields(),
			entity = me.getAddParams();
	    var fields = ['ItemNo', 'IsDoctorItem','CName',
			'StandardCode', 'IschargeItem', 'EName', 'ShortCode',
			'IsCombiItem', 'ShortName', 'IsProfile','Price','Unit',
			'IsCalc','MarketPrice','Prec','Cuegrade',
			'GreatMasterPrice','DiagMethod','Secretgrade',
			'BonusPercent','Color','Visible',
			'LabSuperGroupNo','DispOrder','UseFlag','OrderNo',
			'ItemDesc','Id'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.BLabTestItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		var me =this;
	    data.BLabTestItem_IsDoctorItem = data.BLabTestItem_IsDoctorItem=='1' ? true :false;
	    data.BLabTestItem_IschargeItem = data.BLabTestItem_IschargeItem=='1' ? true :false;
	    data.BLabTestItem_IsCombiItem = data.BLabTestItem_IsCombiItem=='1' ? true :false;
	    data.BLabTestItem_IsProfile = data.BLabTestItem_IsProfile=='1' ? true :false;
	    data.BLabTestItem_IsCalc = data.BLabTestItem_IsCalc=='1' ? true :false;
	    data.BLabTestItem_Visible = data.BLabTestItem_Visible=='1' ? true :false;
	    data.BLabTestItem_UseFlag = data.BLabTestItem_UseFlag=='1' ? true :false;
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
		var DiscountGreatMasterPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
		var Price= data.BLabTestItem_Price;
		var MarketPrice= data.BLabTestItem_MarketPrice;
        var GreatMasterPrice= data.BLabTestItem_GreatMasterPrice;
        if(!Price) Price=0;     
        if(!MarketPrice) MarketPrice=0;
        if(!GreatMasterPrice) GreatMasterPrice=0;
        if(MarketPrice !=0){
          	 //内部价格折扣率=项目价格/三甲价格
	        var DiscountPriceValue=Price/MarketPrice;
	        var DiscountGreatMasterPriceValue=GreatMasterPrice/MarketPrice;
			if(!DiscountPriceValue || DiscountPriceValue=='NaN')DiscountPriceValue=0;
			if(!DiscountGreatMasterPriceValue || DiscountPriceValue=='NaN')DiscountGreatMasterPriceValue=0;
        }else{
        	var DiscountPriceValue=0;
        	var DiscountGreatMasterPriceValue=0;
        }
        DiscountPrice.setValue(DiscountPriceValue*100);
		DiscountGreatMasterPrice.setValue(DiscountGreatMasterPriceValue*100);
		
		return data;
	},
	disableTool:function(bo){
		var me =this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        if(bo) {
        	buttonsToolbar.show();
        }else{
        	buttonsToolbar.hide();
        }
	},
	//设置颜色
	onColorAccept:function(record){
		var me= this;
		var Color = me.getComponent('BLabTestItem_ColorID');
		var ColorName = me.getComponent('BLabTestItem_Color');
		Color.setValue(record ? record.get('ItemColorDict_ColorValue') : '');
		ColorName.setValue(record ? record.get('ItemColorDict_ColorName') : '');
	},
	
		/**保存按钮点击处理方法*/
	onSave:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				me.fireEvent('update',me,id);

				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	isAdd:function(id){
		var me = this;
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.disableTool(true);//显示功能按钮栏
		me.setReadOnly(false);
		
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
		var LabCode = me.getComponent('BLabTestItem_LabCode');
        LabCode.setValue(id);
        var buttonsToolbar = me.getComponent('buttonsToolbar');
        var showText = buttonsToolbar.getComponent('showText');
        showText.setText('项目信息-新增');
        
	},
	isEdit:function(id){
		var me = this;
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.disableTool(true);//显示功能按钮栏
		me.setReadOnly(false);
		
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.load(id);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        var showText = buttonsToolbar.getComponent('showText');
        showText.setText('项目信息-修改');
	},
	/**
	 * 项目价格改变
	 * 项目价格折扣率 = 项目价格/三甲价格 
	 * */
	changePrice:function(){
		var me=this;
		var Price = me.getComponent('BLabTestItem_Price');
		var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
        var PriceVal=Price.getValue();
        var MarketPriceVal=MarketPrice.getValue();
       
        if(!PriceVal)PriceVal=0;
        if(!MarketPriceVal)MarketPriceVal=0;
        if(PriceVal==0 || MarketPriceVal==0) return;
        var val=0;
        if(PriceVal>0 && MarketPriceVal>0){
            val=PriceVal/MarketPriceVal;
        }
        DiscountPrice.setValue(val*100);
	},
	/**
	 * 三甲价格改变
	 * 项目价格折扣率=项目价格/三甲价格
	 * 内部价格折扣率=内部价格/三甲价格
	 * */
	changeMasterPrice:function(value){
		var me=this;
		var GreatMasterPrice = me.getComponent('BLabTestItem_GreatMasterPrice');
		var Price = me.getComponent('BLabTestItem_Price');
	   //内部价格折扣率
		var DiscountGreatMasterPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        //项目价格折扣率
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
	    var PriceVal=Price.getValue();
        if(PriceVal) {
        	var val1=PriceVal/value;
        	DiscountPrice.setValue(val1*100);
        }
        var GreatMasterPriceVal=GreatMasterPrice.getValue();
        if(GreatMasterPriceVal) {
        	var val2=GreatMasterPriceVal/value;
        	DiscountGreatMasterPrice.setValue(val2);
        }
	},

	/**
	 * 内部价格改变
	 * 内部价格折扣率 = 内部价格/三甲价格 
	 * */
	changeGreatMasterPrice:function(){
		var me=this;
		var Price = me.getComponent('BLabTestItem_GreatMasterPrice');
		var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        var PriceVal=Price.getValue();
        var MarketPriceVal=MarketPrice.getValue();
        if(!PriceVal)PriceVal=0;
        if(!MarketPriceVal)MarketPriceVal=0;
        if(PriceVal==0 || MarketPriceVal==0) return;
        DiscountPrice.setValue((PriceVal/MarketPriceVal)*100);
	},
	/**
	 * 项目价格折扣率改变
	 * 项目价格 = 项目价格折扣率 * 三甲价格 
	 * */
	changeDiscountPrice:function(){
		var me =this;
		var Price = me.getComponent('BLabTestItem_Price');
		var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
        var DiscountPriceVal=DiscountPrice.getValue();
        var MarketPriceVal=MarketPrice.getValue();
        if(!DiscountPriceVal)DiscountPriceVal=0;
        DiscountPriceVal=DiscountPriceVal/100;	
        if(!MarketPriceVal)MarketPriceVal=0;
        if(DiscountPriceVal==0 || MarketPriceVal==0) return;
        Price.setValue(DiscountPriceVal*MarketPriceVal);
	},
	/**
	 * 内部价格折扣率改变
	 * 内部价格 = 内部价格折扣率 * 三甲价格 
	 * */
	changeDiscountGreatMasterPrice:function(){
		var me =this;
		var Price = me.getComponent('BLabTestItem_GreatMasterPrice');
		var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        var DiscountPriceVal=DiscountPrice.getValue();
        var MarketPriceVal=MarketPrice.getValue();
        if(!DiscountPriceVal)DiscountPriceVal=0;
        DiscountPriceVal=DiscountPriceVal/100;	
        if(!MarketPriceVal)MarketPriceVal=0;
        if(DiscountPriceVal==0 || MarketPriceVal==0) return;
        Price.setValue(DiscountPriceVal*MarketPriceVal);
	},
	/**创建功能按钮栏*/
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		var msg='信息查看';
		items.push('save','->',{
	        xtype: 'label',
	        itemId:'showText',
	        text: msg,
	        style: "font-weight:bold;color:blue;",// margin: '10 0 5 5',
		});
		
		if(items.length == 0) return null;
		
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
    },
    onSaveClick: function(){
    	var me =this;
    	me.fireEvent('onSaveClick',me);
    },
   
    /**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
        var Price = me.getComponent('BLabTestItem_Price');
		var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
		var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
   
        var GreatMasterPrice = me.getComponent('BLabTestItem_GreatMasterPrice');
		var DiscountGreatMasterPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        //项目价格改变
    	Price.on({
        	change:function(com, newValue,oldValue,  eOpts ){
        		if(!me.IsPriceLoad) return;
        		me.fireEvent('changePriceClick',me);
        	},
        	focus :function(com, eOpts ){
        		if(!me.IsbeforePriceChangeLoad) return;
				me.fireEvent('beforePriceChange',com);
			}
        });
        //项目价格折扣率
        DiscountPrice.on({
        	change:function(com, newValue,oldValue,  eOpts ){
        		if(!me.IsDiscountPrice) return;
        		me.fireEvent('changeDiscountPriceClick',me);
        	}
        });
        //三甲价格改变
        MarketPrice.on({
        	focus :function(com, eOpts ){
        		if(!me.IsbeforeMarketPriceChange) return;
				me.fireEvent('beforeMarketPriceChange',com);
			},
        	change:function(com, newValue,oldValue,  eOpts ){
    			if(!newValue)newValue=0;
    			if(newValue==0){
    				me.noLinkDiscountPrice(0);
    				me.noLinkDiscountGreatMasterPrice(0);
    				return false;
    			}
    			//是否需要改变计算
    			if(!me.IsMarketPrice) return;
    		    me.changeMasterPrice(newValue);
        	}
        })
      
	    GreatMasterPrice.on({
	    	focus :function(com, eOpts ){
	    		if(!me.IsFocusGreatMasterPriceLoad) return;
				me.fireEvent('beforeGreatMasterPriceChange',com);
			},
        	change:function(com, newValue,oldValue,  eOpts ){
        		if(!me.IsGreatMasterPriceLoad) return;
        		me.fireEvent('changeGreatMasterPriceClick',com);
//      		me.changeGreatMasterPrice();
        	}
        });
        DiscountGreatMasterPrice.on({
        	change:function(com, newValue,oldValue,  eOpts ){
        		if(!me.IsDiscountGreatMasterPrice) return;
        		me.fireEvent('changeDiscountGreatMasterPriceClick',me);
        	}
        });
	},
	/**校验是否存在相同的项目编号*/
	getisValidItemNo:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT +me.selectUrl2;
		var values = me.getForm().getValues();		
		url += "&fields=BLabTestItem_ItemNo";
		var where="&where=blabtestitem.ItemNo='"+values.BLabTestItem_ItemNo +"' and blabtestitem.LabCode="+values.BLabTestItem_LabCode;
		if(values.BLabTestItem_Id){
			where+=" and blabtestitem.Id!="+values.BLabTestItem_Id;
		}
		url +=where;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	  /**不需要联动改变项目价格*/
    noLinkPrice:function(val){
    	var me = this;
    	var Price = me.getComponent('BLabTestItem_Price');
    	me.IsPriceLoad=false;
        Price.setValue(val);
        me.IsPriceLoad=true;
    },
     /**改变项目价格，根据价格改变折扣率但不根据折扣率联动改变其他*/
    noLinkPrice2:function(val){
    	var me = this;
    	var Price = me.getComponent('BLabTestItem_Price');
    	me.IsDiscountPrice=false;
        Price.setValue(val);
        me.IsDiscountPrice=true;
    },
    /**不需要联动改变项目价格折扣率*/
    noLinkDiscountPrice:function(val){
    	var me = this;
    	var DiscountPrice = me.getComponent('BLabTestItem_DiscountPrice');
    	me.IsDiscountPrice=false;
        DiscountPrice.setValue(val);
        me.IsDiscountPrice=true;
    },
     /**不需要联动改变内部价格*/
    noLinkGreatMasterPrice:function(val){
    	var me = this;
    	var GreatMasterPrice = me.getComponent('BLabTestItem_GreatMasterPrice');
    	me.IsGreatMasterPriceLoad=false;
        GreatMasterPrice.setValue(val);
        me.IsGreatMasterPriceLoad=true;
    },
     /**改变价格联动改变折扣率，但不根据折扣率改变其他*/
    noLinkGreatMasterPrice2:function(val){
    	var me = this;
    	var GreatMasterPrice = me.getComponent('BLabTestItem_GreatMasterPrice');
    	me.IsDiscountGreatMasterPrice=false;
        GreatMasterPrice.setValue(val);
        me.IsDiscountGreatMasterPrice=true;
    },
      /**不需要联动改变内部价格折扣率*/
    noLinkDiscountGreatMasterPrice:function(val){
    	var me = this;
    	var DiscountGreatMasterPrice = me.getComponent('BLabTestItem_DiscountGreatMasterPrice');
    	me.IsDiscountGreatMasterPrice=false;
        DiscountGreatMasterPrice.setValue(val);
        me.IsDiscountGreatMasterPrice=true;
    },
     /**不需要联动改变三甲价格*/
    noLinkMasterPrice:function(val){
    	var me = this;
    	var MarketPrice = me.getComponent('BLabTestItem_MarketPrice');
    	me.IsMarketPrice=false;
        MarketPrice.setValue(val);
        me.IsMarketPrice=true;
    },
    /**
     * 项目价格联动
     * 只改变项目折扣率，但不根据项目折扣率改变其他
     * */
    LinkPriceNoDiscountPrice:function(val){
    	var me = this;
    	me.IsDiscountPrice=false;
        me.changePrice();
        me.IsDiscountPrice=true;
    },
    /**设置组合项目
     * 根据条件判断
     * */
    setCombo:function(bo){
    	var me = this;
    	var IsCombiItem = me.getComponent('BLabTestItem_IsCombiItem');
        var IsProfile = me.getComponent('BLabTestItem_IsProfile');
    	var IsDoctorItem = me.getComponent('BLabTestItem_IsDoctorItem');
        IsCombiItem.setValue(bo);
    	IsProfile.setValue(bo);
    	IsDoctorItem.setValue(bo);
       
    }
});