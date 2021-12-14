/**
 * 项目表单
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'项目信息',
    width:640,
	height:450,
	bodyPadding:'10px 5px 0px 0px',
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddItemAllItem',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateItemAllItemByField', 
	 /**获取数据服务路径(校验)*/
	selectUrl2:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
	 /**颜色服务地址*/
    selectColorUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictByHQL?isPlanish=true',
 
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	border:true,
    layout:{
        type:'table',
        columns:3//每行有几列
    },
	/**每个组件的默认属性*/
	defaults: {
//		anchor: '100%',
        width:220,
		labelWidth: 100,
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
	formtype:'show',
    buttonDock:'top',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
//		if(me.PK){
//			me.disableTool(true);
//		}else{
//			me.disableTool(false);
//		}
	},
	initComponent:function(){
		var me = this;
		me.addEvents('changeResult','load','beforesave','save','saveerror','onSaveClick');
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
			fieldLabel:'项目编号',name:'TestItem_Id',itemId:'TestItem_Id',
			 emptyText:'必填项',allowBlank:false,regex: /^[0-9]\d*$/,colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'项目中文名称',name:'TestItem_CName',
			 emptyText:'必填项',allowBlank:false, colspan:2,width: me.defaults.width * 2
		},{
			fieldLabel: '项目简称',name: 'TestItem_ShortName',
			emptyText:'必填项',allowBlank:false,colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'项目简码',name:'TestItem_ShortCode',colspan:1,
		    emptyText:'必填项',allowBlank:false,width: me.defaults.width * 1
		}, {
			fieldLabel:'是否医嘱项目',name:'TestItem_IsDoctorItem',
				xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'项目英文名称',
			name:'TestItem_EName',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'项目价格',name:'TestItem_Price',colspan:1,
			decimalPrecision: 4,width: me.defaults.width * 1,itemId:'TestItem_Price'
		},{
			fieldLabel:'是否收费项目',name:'TestItem_IschargeItem',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'标准代码',name:'TestItem_StandardCode',
		    colspan:1,hidden:true,width: me.defaults.width * 1
		},
		{
			fieldLabel:'单位',name:'TestItem_Unit',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'工作量参数',name:'TestItem_FWorkLoad',
		    decimalPrecision: 4,value:0,xtype:'numberfield',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否是组套项目',name:'TestItem_IsCombiItem',
			xtype: 'uxBoolComboBox',value: true
		},
		{
			fieldLabel:'精度',xtype:'numberfield', name:'TestItem_Prec',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'检验方法',name:'TestItem_DiagMethod',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否是组合项目',name:'TestItem_IsProfile',
			xtype: 'uxBoolComboBox',value: true
		},
		{
			fieldLabel:'三甲价格',name:'TestItem_MarketPrice',
			decimalPrecision: 4, hidden:true,
			colspan:1,width: me.defaults.width * 1,itemId:'TestItem_MarketPrice'
		}, 
		{
			fieldLabel: '提示等级',name: 'TestItem_Cuegrade',
			itemId: 'TestItem_Cuegrade', xtype: 'uxSimpleComboBox',hasStyle: true,
			value: me.DefautleCuegrade,data: me.CuegradeList,
			colspan: 1,width: me.defaults.width * 1
		},
		{
			fieldLabel:'内部价格',name:'TestItem_GreatMasterPrice',colspan:1,
		    hidden:true,
		    decimalPrecision: 4,width: me.defaults.width * 1,itemId:'TestItem_GreatMasterPrice'
		},{
			fieldLabel:'颜色ID',hidden:true,xtype:'textfield',
			name:'TestItem_ColorID',itemId:'TestItem_ColorID'
	    },{
			fieldLabel: '颜色',xtype: 'uxCheckTrigger',emptyText: '',
			name: 'TestItem_Color',itemId: 'TestItem_Color',
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
		},
		
		{
			fieldLabel:'是否计算项目',name:'TestItem_IsCalc',
			xtype: 'uxBoolComboBox',value: true
		}, {
			fieldLabel: '保密等级',name: 'TestItem_Secretgrade',
			itemId: 'TestItem_Secretgrade', xtype: 'uxSimpleComboBox',
			hasStyle: true,value: me.DefaluteSecretgrade,data: me.SecretgradeList,
			colspan: 1,width: me.defaults.width * 1
		},
		{
			fieldLabel:'显示次序',xtype:'numberfield', name:'TestItem_DispOrder',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否使用',name:'TestItem_Visible',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'咨询费',name:'TestItem_BonusPercent',xtype:'numberfield',
		    colspan:1,width: me.defaults.width * 1
		},
//		{
//			fieldLabel:'是否使用',name:'TestItem_UseFlag',
//			xtype: 'uxBoolComboBox',value: true,hidden:true
//		},
		{
			fieldLabel:'对应医嘱系统编码',name:'TestItem_OrderNo',colspan:1,
			width: me.defaults.width * 1
		},{
			fieldLabel:'检验大组',name:'TestItem_SuperGroupNo',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'项目说明',height:50,//labelAlign:'top',
				name:'TestItem_ItemDesc',
				xtype:'textarea',
				colspan:3,width: me.defaults.width * 3
		}
//		,{
//			fieldLabel:'项目编号',name:'TestItem_Id',hidden:true,
//			 colspan:1,width: me.defaults.width * 1
//		}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsDoctorItem:values.TestItem_IsDoctorItem ? 1 : 0,
			CName:values.TestItem_CName,
			StandardCode:values.TestItem_StandardCode,
			IschargeItem:values.TestItem_IschargeItem? 1 : 0,
			EName:values.TestItem_EName,
			ShortCode:values.TestItem_ShortCode,
			IsCombiItem:values.TestItem_IsCombiItem ? 1 : 0,
			ShortName:values.TestItem_ShortName,
			IsProfile:values.TestItem_IsProfile ? 1 : 0,
			Unit:values.TestItem_Unit,
			IsCalc:values.TestItem_IsCalc ? 1 : 0,
			Cuegrade:values.TestItem_Cuegrade,
			DiagMethod:values.TestItem_DiagMethod,
			Secretgrade:values.TestItem_Secretgrade,
			Visible:values.TestItem_Visible ? 1 : 0,
			OrderNo:values.TestItem_OrderNo,
			ItemDesc:values.TestItem_ItemDesc
		};
		if(values.TestItem_Color){
			entity.Color=values.TestItem_Color;
		}
		if(values.TestItem_SuperGroupNo){
			entity.SuperGroupNo=values.TestItem_SuperGroupNo;
		}
		if(values.TestItem_FWorkLoad){
			entity.FWorkLoad=values.TestItem_FWorkLoad;
		}
		if(values.TestItem_DispOrder){
			entity.DispOrder=values.TestItem_DispOrder;
		}
		if(values.TestItem_Price){
			entity.Price=values.TestItem_Price;
		}
		if(values.TestItem_MarketPrice){
			entity.MarketPrice=values.TestItem_MarketPrice;
		}
		if(values.TestItem_GreatMasterPrice){
			entity.GreatMasterPrice=values.TestItem_GreatMasterPrice;
		}
		if(values.TestItem_BonusPercent){
			entity.BonusPercent=values.TestItem_BonusPercent;
		}
	    if(values.TestItem_Prec){
			entity.Prec=values.TestItem_Prec;
		}
	    
	    if(values.TestItem_Id){
			entity.Id=values.TestItem_Id;
		}
	    return entity;
	},
    getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
	    var fields = [ 'IsDoctorItem','CName',
			'StandardCode', 'IschargeItem', 'EName', 'ShortCode',
			'IsCombiItem', 'ShortName', 'IsProfile','Price','Unit',
			'IsCalc','Prec','Cuegrade','DiagMethod','Secretgrade',
//			'GreatMasterPrice','MarketPrice',
			'BonusPercent','Color','Visible',
			'DispOrder','OrderNo','SuperGroupNo',
			'ItemDesc','Id'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.TestItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		var me =this;
	    data.TestItem_IsDoctorItem = data.TestItem_IsDoctorItem=='1' ? true :false;
	    data.TestItem_IschargeItem = data.TestItem_IschargeItem=='1' ? true :false;
	    data.TestItem_IsCombiItem = data.TestItem_IsCombiItem=='1' ? true :false;
	    data.TestItem_IsProfile = data.TestItem_IsProfile=='1' ? true :false;
	    data.TestItem_IsCalc = data.TestItem_IsCalc=='1' ? true :false;
	    data.TestItem_Visible = data.TestItem_Visible=='1' ? true :false;
	    data.TestItem_UseFlag = data.TestItem_UseFlag=='1' ? true :false;
		return data;
	},
	//设置颜色
	onColorAccept:function(record){
		var me= this;
		var Color = me.getComponent('TestItem_ColorID');
		var ColorName = me.getComponent('TestItem_Color');
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
		me.setReadOnly(false);
		
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        var showText = buttonsToolbar.getComponent('showText');
        showText.setText('项目信息-新增');
	},
	isEdit:function(id){
		var me = this;
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.setReadOnly(false);
		
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		
		me.load(id);
	    var Id = me.getComponent('TestItem_Id');
	    Id.setReadOnly(true);
	    var buttonsToolbar = me.getComponent('buttonsToolbar');
        var showText = buttonsToolbar.getComponent('showText');
        showText.setText('项目信息-修改');
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
		var hidden =  me.formtype == 'show' ? true : false;
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
    /**校验是否存在相同的项目编号*/
	getisValidItemNo:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT +me.selectUrl2;
		var values = me.getForm().getValues();		
		url += "&fields=TestItem_Id&where=testitem.Id="+values.TestItem_Id+"";
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});