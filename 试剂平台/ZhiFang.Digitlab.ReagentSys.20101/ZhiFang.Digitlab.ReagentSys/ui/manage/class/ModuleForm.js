Ext.ns('Ext.manage');
Ext.define('Ext.manage.ModuleForm',{
	extend:'Ext.form.Panel',
	alias: 'widget.moduleform',
	//=====================可配参数=======================
	/**
	 * 模块编号
	 * @type Number
	 */
	Id:-1,
	/**
	 * 树形结构父级ID
	 * @type Number
	 */
	ParentID:0,
	/**
	 * 树形结构父级名称
	 * @type String
	 */
	ParentName:'',
	/**
	 * 树形结构层级
	 * @type Number
	 */
	LevelNum:1,
	/**
	 * 树形结构层级Code
	 * @type Number
	 */
	TreeCatalog:1,
	
	/**
	 * 是否以弹出的形式展示
	 * @type Boolean
	 */
	isWindow:false,
	/**
	 * 面板的类型：共三种，默认show（查看面板）
	 * add：新增
	 * edit：修改
	 * show：查看
	 * @type String
	 */
	type:'show',
	
	title:'模块面板',
	width:565,
	height:325,
	/**
	 * 应用程序解释页面
	 * @type String
	 */
	appUrl:'getAppHtmlPath()@@',
	/**
	 * 新增保存的后台服务地址
	 * @type String
	 */
	addModuleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_AddRBACModule',
	/**
	 * 修改保存的后台服务地址
	 * @type String
	 */
	editModuleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_UpdateRBACModule',
	/**
	 * 获取数据服务地址
	 * @type String
	 */
	getModuleInfoServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_GetRBACModuleById',
	/**
	 * 根据中文获取拼音字头服务地址
	 * @type 
	 */
	getPinYinZiTouServerUrl:getRootPath()+'/ConstructionService.svc/GetPinYin',
	/**
	 * 上传图片文件服务地址
	 * @type 
	 */
	updateFileServerUrl:getRootPath()+'/ConstructionService.svc/ReceiveModuleIconService',
	/**
	 * 获取应用列表服务地址
	 * @type String
	 */
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
	/**
	 * 新增保存的后台服务地址
	 * @type 
	 */
	addServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_AddRBACModule',
	/**
	 * 修改保存的后台服务地址
	 * @type String
	 */
	editServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_UpdateRBACModule',
	//=====================内部变量=======================
	/**
	 * 图标下拉框数据
	 * @type String
	 */
	iconList:[
		['默认','default.PNG'],
		['文件夹','package.PNG'],
		['列表','list.PNG'],
		['检索','search.PNG'],
		['执行程序','program.PNG'],
		['设置','configuration.PNG'],
		['字典','dictionary.PNG']
	],
    //=====================内部视图渲染=======================
	/**
	 * 初始化模块表单
	 */
	initComponent:function(){
		var me = this;
		//布局方式
		me.layout = "absolute";
		me.bodyPadding = '8 16 8 16';
		//内部组件
		me.items = me.createItems(me.type);
		//停靠
		me.dockedItems = me.createDockedItems(me.type);
		//注册事件
		me.addEvents('saveClick');//保存按钮
		me.addEvents('saveAsClick');//另存按钮
		me.addEvents('cancelClick');//取消
		
		me.callParent(arguments);
	},
	/**
	 * 渲染完后执行
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化内容
		me.initValues();
	},
	//=====================创建内部元素=======================
	/**
	 * 内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(type){
		var me = this;
		me.defaults = {labelAlign:'right'};
		//隐藏的字段
		var hideFields = [
			{xtype:'textfield',name:'Id',fieldLabel:'模块ID',hidden:true,value:me.Id},
			{xtype:'textfield',name:'LabID',fieldLabel:'实验室ID',hidden:true},
			{xtype:'textfield',name:'ParentID',fieldLabel:'树形结构父级ID',hidden:true,value:me.ParentID},
			{xtype:'textfield',name:'LevelNum',fieldLabel:'树形结构层级',hidden:true,value:me.LevelNum},
			{xtype:'textfield',name:'TreeCatalog',fieldLabel:'树形结构层级Code',hidden:true,value:me.TreeCatalog},
			//{xtype:'textfield',name:'IsLeaf',fieldLabel:'是否是叶节点',hidden:true},
			{xtype:'textfield',name:'Owner',fieldLabel:'所有者',hidden:true},
			{xtype:'textfield',name:'DataTimeStamp',fieldLabel:'时间戳',hidden:true},
			{xtype:'textfield',name:'PrimaryKey',fieldLabel:'PrimaryKey',hidden:true},
			{xtype:'textfield',name:'PicFile',fieldLabel:'模块图标文件名',hidden:true,value:'default.PNG'}
		];
		
		//显示的字段
		var showFields = [
			{xtype:'textfield',name:'CName',fieldLabel:'中文名称',x:10,y:5,width:170,labelWidth:55,
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
						me.CNameChange(newValue);
					}
				}
			},
			{xtype:'textfield',name:'EName',fieldLabel:'英文名称',x:10,y:30,width:170,labelWidth:55},
			{xtype:'textfield',name:'PinYinZiTou',fieldLabel:'拼音字头',x:10,y:55,width:170,labelWidth:55},
			{xtype:'textfield',name:'Shortcode',fieldLabel:'快捷码',x:10,y:80,width:170,labelWidth:55},
			
			{xtype:'textfield',name:'SName',fieldLabel:'模块简称',x:190,y:5,width:170,labelWidth:55},
			{xtype:'numberfield',name:'DispOrder',fieldLabel:'显示次序',x:190,y:30,width:170,labelWidth:55,value:1,
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
						if(newValue == null || newValue == ""){
							this.setValue(0);
						}
					}
				}
			},
			{xtype:'combobox',name:'ModuleType',fieldLabel:'模块类型',x:190,y:55,width:170,labelWidth:55,
				mode:'local',editable:false,displayField:'text',valueField:'value',value:0,
				store:new Ext.data.SimpleStore({
				    fields:['value','text'],
				    data:[[0,'构建'],[1,'非构建']]
				}),
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
						me.ModuleTypeChange(newValue);
					}
				}
			},
			
			{xtype:'textfield',name:'UseCode',fieldLabel:'系统代码',x:370,y:5,width:180,labelWidth:65},
			{xtype:'textfield',name:'StandCode',fieldLabel:'标准代码',x:370,y:30,width:180,labelWidth:65},
			{xtype:'textfield',name:'DeveCode',fieldLabel:'开发商代码',x:370,y:55,width:180,labelWidth:65},
			
			{xtype:'image',name:'showImg',itemId:'showImg',tpl:'模块图标效果展示',x:345,y:80,width:16,height:16,src:getIconRootPathBySize(16)+"/default.png"},
			{xtype:'checkbox',name:'IsUse',boxLabel:'是否使用',x:486,y:80,width:70,checked:true},
			
			{xtype:'textfield',name:'Url',fieldLabel:'入口地址',x:10,y:105,width:450,labelWidth:55,readOnly:true},
			
			{xtype:'textfield',name:'Para',fieldLabel:'入口参数',x:10,y:130,width:540,labelWidth:55},
			{xtype:'textareafield',name:'Comment',fieldLabel:'模块描述',x:10,y:155,width:540,labelWidth:55,height:85},
			
			{xtype:'textfield',name:'parentModuleName',fieldLabel:'上级模块',x:10,y:245,width:170,labelWidth:55,value:me.ParentName,readOnly:true}
		];
		
		var items = hideFields.concat(showFields);
		
		var DataAddTime = {
			xtype:'datefield',name:'DataAddTime',fieldLabel:'加入时间',x:220,y:245,width:160,labelWidth:55
		};
		var DataUpdateTime = {
			xtype:'datefield',name:'DataUpdateTime',fieldLabel:'更新时间',x:390,y:245,width:160,labelWidth:55
		};
		var linkToApp = {
			xtype:'button',itemId:'linkToApp',text:'关联应用',x:471,y:105,iconCls:'build-button-configuration-blue',
			handler:function(button){me.linkToAppClick();}
		};
		var file = {
			xtype:'filefield',itemId:'file',fieldLabel:'模块图标',x:190,y:80,width:150,labelWidth:55,
			buttonConfig:{iconCls:'search-img-16',text:''},
			listeners:{change:function(field,value){me.ImgChange();}}
		};
		var parentModuleButton = {
			xtype:'button',name:'getParentModuleButton',fieldLabel:'选择模块',x:185,y:245,
			iconCls:'build-button-configuration-blue',tooltip:'选择一个上级模块',
			handler:function(button){me.getParentModuleButtonClick();}
		};
		var selectIcon = {
			xtype:'combobox',fieldLabel:'',mode:'local',editable:false,x:370,y:80,width:100,
			displayField:'text',valueField:'value',itemId:'iconList',value:'default.PNG',
			store:new Ext.data.SimpleStore({ 
			    fields:['text','value'], 
			    data:me.iconList
			}),
			listeners:{change:function(field,value){me.iconListChange(value);}},
			listConfig:{
				getInnerTpl:function(){
					return "<img src='" + getIconRootPathBySize(16) + "/{value}'/>&nbsp;{text}";
				}
			}
		};
		
		if(me.type == "show"){//查看面板特有的显示字段
			//加入时间
			items.push(DataAddTime);
			//更新时间
			items.push(DataUpdateTime);
			//模块图标
			items.push({xtype:'label',text:'模块图标:',x:193,y:80,width:60});
			//把每个组件都设置成只读
			for(var i in items){
				items[i].readOnly = true;
				if(items[i].name == "showImg"){
					items[i].x = 250;
				}else if(items[i].name == "Url"){
					items[i].width = 540;
				}
			}
		}else if(me.type == "add"){//新增面板特有的显示字段
			//关联应用
			items.push(linkToApp);
			//模块图标
			items.push(file);
			//选择上级模块按钮
			items.push(parentModuleButton);
			//图标下拉框
			items.push(selectIcon);
		}else if(me.type == "edit"){//修改面板特有的显示字段
			//关联应用
			items.push(linkToApp);
			//模块图标
			items.push(file);
			//选择上级模块按钮
			items.push(parentModuleButton);
			//加入时间
			DataAddTime.readOnly = true;
			items.push(DataAddTime);
			//更新时间
			DataUpdateTime.readOnly = true;
			items.push(DataUpdateTime);
			//图标下拉框
			items.push(selectIcon);
		}
		
		return items;
	},
	/**
	 * 创建停靠的功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(type){
		var me = this;
		
		var items = [];
		if(type == "add"){//新增面板
			items.push('->');
			items.push({text:'保存',iconCls:'build-button-save',handler:function(button){me.save();}});
			items.push({text:'重置',iconCls:'build-button-refresh',handler:function(button){me.getForm().reset();}});
		}else if(type == "edit"){//修改面板
			items.push('->');
			items.push({text:'保存',iconCls:'build-button-save',handler:function(button){me.save();}});
			items.push({text:'另存',iconCls:'build-button-save',handler:function(button){me.saveAs();}});
			items.push({text:'重置',iconCls:'build-button-refresh',handler:function(button){me.getForm().reset();}});
		}else{//查看面板
			items.push('->');
		}
		
		if(me.isWindow){
			items.push({text:'取消',iconCls:'build-button-cancel',margin:'0 10 0 0',handler:function(button){me.close();me.fireEvent('cancelClick');}});
		}
		
		var dockedItems = [{
			xtype:'toolbar',
			dock:'bottom',
			items:items
		}];
		
		return dockedItems;
	},
	//=====================事件处理=======================
	/**
	 * 保存模块信息
	 * @private
	 */
	save:function(){
		var me = this;
		//保存数据
		me.addModule("save");
	},
	/**
	 * 另存模块信息
	 * @private
	 */
	saveAs:function(){
		var me = this;
		//保存数据
		me.addModule("saveAs");
	},
	/**
	 * 保存数据
	 * @private
	 * @param {} callback
	 * @param {} isAdd
	 */
	addModule:function(saveType){
		var me = this;
		var params = me.getForm().getValues();
		if(params.Id && params.Id != ""){
			me.Id = params.Id;
		}else{
			me.Id = -1;
		}
		
		if(saveType == "saveAs"){//另存 
			params.Id = -1;
		}
		
		var callback;
		if(saveType == "save"){
			callback = function(){
				params.Id = me.Id;
				me.fireEvent('saveClick');
			};
		}else if(saveType == "saveAs"){
			callback = function(){
				params.Id = me.Id;
				me.fireEvent('saveAsClick');
			};
		}
		
		params.IsUse = (params.IsUse == "on") ? true : false;
		
		params.DataTimeStamp = (params.DataTimeStamp == "") ? [] : params.DataTimeStamp.split(",");
		
		params.DataAddTime = (params.DataAddTime == "") ? null : params.DataAddTime;
		
		params.DataUpdateTime = (params.DataUpdateTime == "") ? null : params.DataUpdateTime;
		
		params.LabID = "1";
		params.Owner = "1";
		
		params.PicFile = (params.PicFile == "") ? "default.PNG" : params.PicFile;
		
		me.saveToServer(params,callback);
	},
	/**
	 * 模块类型选中值更改处理
	 * @param {} newValue
	 */
	ModuleTypeChange:function(newValue){
		var me = this;
		var Url = me.getForm().findField('Url');
		var Para = me.getForm().findField('Para');
		
		var linkToApp = me.getComponent('linkToApp');//关联应用按钮
		
		var obj = {Url:"",Para:""};
		me.setFormValues(obj);
		
		if(newValue == 0){
			Url.setReadOnly(true);
			Url.setWidth(450);
			linkToApp.show();
		}else if(newValue == 1 && me.type != "show"){
			Url.setReadOnly(false);
			Url.setWidth(540);
			linkToApp.hide();
		}
	},
	/**
	 * 关联应用按钮处理
	 * @private
	 */
	linkToAppClick:function(){
		var me = this;
		var appList = Ext.create('Ext.build.AppListPanel',{
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:500,
			height:300,
			getAppListServerUrl:me.getAppListServerUrl,
			defaultLoad:true,
			readOnly:true,
			pageSize:9//每页数量
    	}).show();
    	appList.on({
    		okClick:function(){
    			var records = appList.getSelectionModel().getSelection();
    			if(records.length == 0){
    				Ext.Msg.alert("提示","请选择一个应用！");
    			}else if(records.length == 1){
    				var id = records[0].get('BTDAppComponents_Id');
    				var obj = {
	    				Url:me.appUrl + "?id=" + id
					};
	    			me.setFormValues(obj);
    				appList.close();
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
    			var id = record.get('BTDAppComponents_Id');
    			var obj = {
    				Url:me.appUrl + "?id=" + id
				};
    			me.setFormValues(obj);
    			appList.close();
    		}
    	});
	},
	/**
	 * 更改图片时处理
	 * @private
	 */
	ImgChange:function(){
		var me = this;
		var callback = function(value){
			me.getComponent('iconList').setValue("");
			me.setPicFile(value);
		}
		me.updateFileToServer(callback);
	},
	/**
	 * 更改中文名称时处理
	 * @private
	 */
	CNameChange:function(newValue){
		var me = this;
		var changePinYinZiTou = function(value){
			var obj = {PinYinZiTou:value};
			me.setFormValues(obj);
		}
		if(newValue != ""){
			me.getPinYinZiTouFromServer(newValue,changePinYinZiTou);
		}else{
			var obj = {PinYinZiTou:""};
			me.setFormValues(obj);
		}
	},
	/**
	 * 选择父节点
	 * @private
	 */
	getParentModuleButtonClick:function(){
		var me = this;
		var ParentID = me.getForm().findField('ParentID');
		var win = Ext.create('Ext.manage.ModuleTree',{
			modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:200,
			height:300,
			title:'选择上级模块',
			selectId:ParentID.value,
			hideNodeId:me.Id
		}).show();
		win.on({
			okClick:function(){
    			var records = win.getSelectionModel().getSelection();
    			if(records.length == 0){
    				Ext.Msg.alert("提示","请选择一个模块！");
    			}else if(records.length == 1){
    				var id = records[0].get('Id');
    				ParentID.setValue(id);
    				
    				var name = records[0].get('CName');
    				var parentModuleName = me.getForm().findField('parentModuleName');
    				parentModuleName.setValue(name);
    				
    				win.close();
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
    			var id = record.get('Id');
				ParentID.setValue(id);
				
				var name = record.get('CName');
				var parentModuleName = me.getForm().findField('parentModuleName');
				parentModuleName.setValue(name);
    				
				win.close();
    		}
		});
	},
	/**
	 * 图标下拉框值变化
	 * @private
	 * @param {} value
	 */
	iconListChange:function(value){
		var me = this;
		me.setPicFile(value);
	},
	//=====================设置获取参数=======================
	/**
	 * 设置图标名称值
	 * @private
	 * @param {} value
	 */
	setPicFile:function(value){
		var me = this;
		var obj = {PicFile:value};
		me.setFormValues(obj);
		var src = getIconRootPathBySize(16)+"/" + value;
		var showImg = me.getComponent('showImg');
		showImg.setSrc(src);
	},
	/**
	 * 获取表单上的所有值
	 * @private
	 * @return {}
	 */
	getFormValues:function(){
		return this.getForm().getValues();
	},
	/**
	 * 根据name获取表单中对应的数据
	 * @private
	 * @param {} name
	 * @return {}
	 */
	getFormValueByName:function(name){
		var values = this.getForm().getValues();
		return values[name];
	},
	/**
	 * 设置参数
	 * @private
	 * @param {} obj
	 */
	setFormValues:function(obj){
		this.getForm().setValues(obj);
	},
	/**
	 * 初始化数值
	 * @private
	 */
	initValues:function(){
		var me = this;
		var id = me.Id;
		//更新表单数据
		me.load(id);
	},
	//=====================公共方法代码=======================
	/**
	 * 压平的数据转换
	 * @private
	 * @param {} obj
	 * @return {}
	 */
	changeData:function(obj){
		var me = this;
		var data = {};
		for(var i in obj){
			var str = i.split("_")[1];
			data[str] = obj[i];
		}
		
		data['IsUse'] = (data['IsUse'] == "True") ? true : false;
		
		return data;
	},
	//=====================后台获取&存储=======================
	/**
	 * 将模块信息保存到数据库中
	 * @private
	 * @param {} obj
	 * @param {} callback
	 */
	saveToServer:function(obj,callback){
		var me = this;
		var url = "";
		if(obj.Id != -1){
			url = me.editServerUrl;//修改
		}else{
			url = me.addServerUrl;//新增
		}
		
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			params:Ext.JSON.encode({entity:obj}),
			method:'POST',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					//me.appId = result.id;
					//Ext.Msg.alert('提示','保存成功！');
					if(Ext.typeOf(callback) == "function"){
						callback();//回调函数
					}
				}else{
					Ext.Msg.alert('提示','保存模块信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure : function(response,options){ 
				Ext.Msg.alert('提示','连接保存模块服务失败！');
			}
		});
	},
	/**
	 * 根据中文名称获取拼音字头
	 * @private
	 * @param {} CName
	 * @param {} callback
	 */
	getPinYinZiTouFromServer:function(CName,callback){
		var me = this;
		var url = encodeURI(me.getPinYinZiTouServerUrl + "?chinese=" + CName);
		
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						callback(result.ResultDataValue);//回调函数
					}
				}else{
					Ext.Msg.alert('提示','获取拼音字头失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure : function(response,options){ 
				Ext.Msg.alert('提示','连接获取拼音字头服务失败！');
			}
		});
	},
	/**
	 * 上传图片文件到服务器
	 * @private
	 */
	updateFileToServer:function(callback){
		var form = this;
		var url = form.updateFileServerUrl;
		
       	if (!form.getForm().isValid()) return;
       	
		form.getForm().submit({
			//waitMsg:'正在提交数据',
			//waitTitle:'提示',
			url:url,
			success:function(form,action){
				if(Ext.typeOf(callback) == "function"){
					callback(action.result.ResultDataValue);//回调函数
				}
				//Ext.Msg.alert('提示','保存成功');
			},
			failure:function(form,action){
				if(action.result){
					Ext.Msg.alert('提示','<b>处理错误！原因如下：<font style="color:red">' + action.result.errorInfo + '</font></b>');
				}else{
					Ext.Msg.alert('提示','上传图片文件失败！错误信息【<b style="color:red">'+ action.result.ErrorInfo +'</b>】');
				}
				Ext.Msg.alert('提示','<b style="color:red">连接上传图片文件服务失败！<b>');
			}
		});
	},
	/**
	 * 从数据库获取模块信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getModuleInfoFromServer:function(id,callback){
		var me = this;
		var url = me.getModuleInfoServerUrl + "?isPlanish=false&id="+id;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
						callback(ResultDataValue);//回调函数
					}
				}else{
					Ext.Msg.alert('提示','获取模块信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','连接获取模块服务失败！');
			}
		});
	},
	//=====================对外公开方法=======================
	/**
	 * 更新表单数据
	 * @public
	 * @param {} obj
	 */
	load:function(obj){
		var me = this;
		var id = -1;
		if(typeof obj == 'number' || typeof obj == 'string'){
			id = obj;
		}else if(typeof obj == 'object' && (typeof (obj.id) == 'number' || typeof (obj.id) == 'string')){
			id = obj.Id;
		}else{
			Ext.Msg.alert("提示","参数格式错误!正确的参数格式为:1或者{Id:1}");
		}
		
		if(id > 0){
			var callback = function(values){
				me.setFormValues(values);
				//下拉框赋值
				var iconList = me.getComponent('iconList');
				if(iconList){
					var bo = true;
					for(var i in me.iconList){
						if(me.iconList[i][1] == values.PicFile){
							iconList.setValue(values.PicFile);
							bo = false;
							break;
						}
					}
					if(bo){
						iconList.setValue("");
					}
				}
				//更新图标
				me.setPicFile(values.PicFile);
				
				var linkToApp = me.getComponent('linkToApp');//关联应用按钮
				var values = me.getForm().getValues();
				var ModuleType = values.ModuleType;
				if(linkToApp && ModuleType == 0){
					linkToApp.show();
				}else if(linkToApp && ModuleType == 1){
					linkToApp.hide();
				}
			};
			me.getModuleInfoFromServer(id,callback);
		}
	}
});