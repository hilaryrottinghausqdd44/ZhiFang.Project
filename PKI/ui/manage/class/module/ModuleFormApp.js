Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleFormApp',{
	extend:'Ext.form.Panel',
	alias: 'widget.moduleformapp',
	//=====================可配参数=======================
    /***
     * 表单页面属性:
     * load方法是否加载完成
     * @type Boolean
     */
    isLoadingComplete:false,
	/**
	 * 模块编号
	 * @type Number
	 */
	Id:'-1',
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
	isSuccess:false,
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
	
	title:'模块信息',
	//width:565,
	height:290,
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
	editModuleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_UpdateRBACModuleByField',
	/**
	 * 获取数据服务地址
	 * @type String
	 */
	getModuleInfoServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_GetRBACModuleById',
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
    isShow:function(Id) {
        var me=this;
        var buts = me.getComponent("dockedItems-buttons");
        if (buts) {
            if (me.type != "show") {
                me.setHeight(me.getHeight() - 25);
            }
            buts.hide();
        }
        var DataAddTime = me.getComponent("DataAddTime");
        if (DataAddTime) {
            DataAddTime.show();
        }
        var DataUpdateTime = me.getComponent("DataUpdateTime");
        if (DataUpdateTime) {
            DataUpdateTime.show();
        }
        var fileImg = me.getComponent("fileImg");
        if (fileImg) {
            fileImg.show();
        }
        var file = me.getComponent("file");
        if (file) {
            file.hide();
        }
        var toobar = me.getComponent("toobar");
        if (toobar) {
            toobar.hide();
            var saveas = toobar.getComponent("saveas");
            if(saveas){
                saveas.hide();
            }
            var save = toobar.getComponent("save");
            if(save){
                save.hide();
            }
            var refresh = toobar.getComponent("refresh");
            if(refresh){
                refresh.hide();
            }
        }
        me.type = "show";
        me.setReadOnly(true);
        me.load(Id);
    },
    isAdd:function() {
        var me=this;
        me.getForm().reset();
        var buts = me.getComponent("dockedItems-buttons");
        if (buts) {
            if (me.type == "add") {
                me.setHeight(me.getHeight() + 25);
            }
            buts.show();
        }
        var DataAddTime = me.getComponent("DataAddTime");
        if (DataAddTime) {
            DataAddTime.show();
            DataAddTime.setReadOnly(false);
        }
        var DataUpdateTime = me.getComponent("DataUpdateTime");
        if (DataUpdateTime) {
            DataUpdateTime.show();
            DataUpdateTime.setReadOnly(false);
        }
        var linkToApp = me.getComponent("linkToApp");
        if (linkToApp) {
            linkToApp.show();
        }
        
        var file = me.getComponent("file");
        if (file) {
            file.show();
        }
        var fileImg = me.getComponent("fileImg");
        if (fileImg) {
            fileImg.hide();
        }
        var parentModuleButton = me.getComponent("parentModuleButton");
        if (parentModuleButton) {
            parentModuleButton.show();
        }   
        var selectIcon = me.getComponent("selectIcon");
        if (selectIcon) {
            selectIcon.show();
        }
        var toobar = me.getComponent("toobar");
        if (toobar) {
            toobar.show();
            var saveas = toobar.getComponent("saveas");
            if(saveas){
                saveas.hide();
            }
            var save = toobar.getComponent("save");
            if(save){
                save.show();
            }
            var refresh = toobar.getComponent("refresh");
            if(refresh){
                refresh.show();
            }
        }
        
        me.type = "add";
        me.setReadOnly(false);
        me.getForm().reset();
    },
    isEdit:function(Id) {
        var me=this;
        var buts = me.getComponent("dockedItems-buttons");
        if (buts) {
            if (me.type == "edit") {
                me.setHeight(me.getHeight() + 25);
            }
            buts.show();
        } 
        var DataAddTime = me.getComponent("DataAddTime");
        if (DataAddTime) {
            DataAddTime.show();
            //加入时间
            DataAddTime.setReadOnly(true);
        }
        var DataUpdateTime = me.getComponent("DataUpdateTime");
        if (DataUpdateTime) {
            DataUpdateTime.show();
            //更新时间
            DataUpdateTime.setReadOnly(true);
        }
        var linkToApp = me.getComponent("linkToApp");
        if (linkToApp) {
            linkToApp.hide();
        }
        var file = me.getComponent("file");
        if (file) {
            file.show();
        }
        var fileImg = me.getComponent("fileImg");
        if (fileImg) {
            fileImg.hide();
        }
        var parentModuleButton = me.getComponent("parentModuleButton");
        if (parentModuleButton) {
            parentModuleButton.show();
        }   
        var selectIcon = me.getComponent("selectIcon");
        if (selectIcon) {
            selectIcon.show();
        } 
        var toobar = me.getComponent("toobar");
        if (toobar) {
            toobar.show();
            var saveas = toobar.getComponent("saveas");
            if(saveas){
                saveas.hide();
            }
            var save = toobar.getComponent("save");
            if(save){
                save.show();
            }
            var refresh = toobar.getComponent("refresh");
            if(refresh){
                refresh.show();
            }
        }
        me.type = "edit";
        me.setReadOnly(false);
        me.load(Id);
    },
    setReadOnly:function(bo) {
        var me=this;
            var items2 = me.items.items;
            for (var i in items2) {
                if (!items2[i].hasReadOnly) {
                    var type = items2[i].xtype;
                    if (type == "button" || type == "label"|| type == "image") {} else {
                        items2[i].setReadOnly(bo);
                    }
                }
            }
    },
	/**
	 * 初始化模块表单
	 */
	initComponent:function(){
		var me = this;
		
		Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.ux',getRootPath()+'/extjs/ux/');
		Ext.Loader.setPath('Ext.zhifangux.DateField',getRootPath()+'/ui/zhifangux/DateField.js');
		Ext.Loader.setPath('Ext.manage.module.ModuleTree', getRootPath() + '/ui/manage/class/module/ModuleTree.js');
        Ext.Loader.setPath('Ext.build.AppListPanel', getRootPath() + '/ui/build/class/AppListPanel.js');
        
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
		
        
        me.setValueByItemId = function(key, value) {
            me.getForm().setValues([ {
                id:key,
                value:value
            } ]);
        };
        me.changeStoreData = function(response) {
            var data = Ext.JSON.decode(response.responseText);
            if (data.ResultDataValue && data.ResultDataValue != "") {
                var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                data.ResultDataValue = ResultDataValue;
                data.list = ResultDataValue.list;
            } else {
                data.list = [];
            }
            response.responseText = Ext.JSON.encode(data);
            return response;
        };
        
        if (me.type == "show") {
            me.height -= 25;
        } else {
            me.dockedItems = me.createDockedItems(me.type);
        }
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
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
        if (me.type == "add") {
            me.isAdd();
            
        } else if (me.type == "edit") {
            me.isEdit(me.Id);
           
        } else if (me.type == "show") {
            me.isShow(me.Id);
        }
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
			{xtype:'textfield',name:'Id',itemId:'Id',fieldLabel:'模块ID',hidden:true,value:me.Id},
			{xtype:'textfield',name:'LabID',itemId:'LabID',fieldLabel:'实验室ID',hidden:true},
			{xtype:'textfield',name:'ParentID',itemId:'ParentID',fieldLabel:'树形结构父级ID',hidden:true,value:me.ParentID},
			{xtype:'textfield',name:'LevelNum',itemId:'LevelNum',fieldLabel:'树形结构层级',hidden:true,value:me.LevelNum},
			{xtype:'textfield',name:'TreeCatalog',itemId:'TreeCatalog',fieldLabel:'树形结构层级Code',hidden:true,value:me.TreeCatalog},
			//{xtype:'textfield',name:'IsLeaf',fieldLabel:'是否是叶节点',hidden:true},
			{xtype:'textfield',name:'Owner',itemId:'Owner',fieldLabel:'所有者',hidden:true},
			{xtype:'textfield',name:'DataTimeStamp',itemId:'DataTimeStamp',fieldLabel:'时间戳',hidden:true},
			{xtype:'textfield',name:'PrimaryKey',itemId:'PrimaryKey',fieldLabel:'PrimaryKey',hidden:true},
			{xtype:'textfield',name:'PicFile',itemId:'PicFile',fieldLabel:'模块图标文件名',hidden:true,value:'default.PNG'},
			{xtype:'textfield',name:'BTDAppComponents_Id',itemId:'BTDAppComponents_Id',fieldLabel:'对应应用ID',hidden:true},
			{xtype:'textfield',name:'BTDAppComponents_DataTimeStamp',itemId:'BTDAppComponents_DataTimeStamp',fieldLabel:'对应应用时间戳',hidden:true}
		];
		
		//显示的字段
		var showFields = [
			{xtype:'textfield',name:'CName',itemId:'CName',fieldLabel:'中文名称',x:10,y:5,width:170,labelWidth:55,
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
                        if(newValue!=''&&me.isLoadingComplete==true&&(me.type=='edit'||me.type=='add')){
						     me.CNameChange(newValue);
                        }else{
                            me.isLoadingComplete=true;
                        }
					}
				}
			},
			{xtype:'textfield',name:'EName',itemId:'EName',fieldLabel:'英文名称',x:10,y:30,width:170,labelWidth:55},
			{xtype:'textfield',name:'PinYinZiTou',itemId:'PinYinZiTou',fieldLabel:'拼音字头',x:10,y:55,width:170,labelWidth:55},
			{xtype:'textfield',name:'Shortcode',itemId:'Shortcode',fieldLabel:'快捷码',x:10,y:80,width:170,labelWidth:55},
			
			{xtype:'textfield',name:'SName',itemId:'SName',fieldLabel:'模块简称',x:190,y:5,width:170,labelWidth:55},
			{xtype:'numberfield',name:'DispOrder',itemId:'DispOrder',fieldLabel:'显示次序',x:190,y:30,width:170,labelWidth:55,value:1,
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
						if(newValue == null || newValue == ""){
							this.setValue(0);
						}
					}
				}
			},
			{xtype:'combobox',name:'ModuleType',itemId:'ModuleType',fieldLabel:'模块类型',x:190,y:55,width:170,labelWidth:55,
				mode:'local',editable:false,displayField:'text',valueField:'value',value:"0",
				store:new Ext.data.Store({//Simple
				    fields:['value','text'],
                    autoLoad:true,
				    data:[
                        {'value':'0','text':'构建'},
                        {'value':'1','text':'非构建'}
                        ]
				}),
				listeners:{
					change:function(field,newValue,oldValue,eOpts){
						me.ModuleTypeChange(newValue);
					}
				}
			},
			
			{xtype:'textfield',name:'UseCode',itemId:'UseCode',fieldLabel:'系统代码',x:370,y:5,width:180,labelWidth:65},
			{xtype:'textfield',name:'StandCode',itemId:'StandCode',fieldLabel:'标准代码',x:370,y:30,width:180,labelWidth:65},
			{xtype:'textfield',name:'DeveCode',itemId:'DeveCode',fieldLabel:'开发商代码',x:370,y:55,width:180,labelWidth:65},
			
			{xtype:'image',name:'showImg',itemId:'showImg',tpl:'模块图标效果展示',x:345,y:80,width:16,height:16,src:getIconRootPathBySize(16)+"/default.png"},
			{xtype:'checkbox',name:'IsUse',itemId:'IsUse',boxLabel:'是否使用',x:486,y:80,width:70,checked:true},
			
			{xtype:'textfield',name:'Url',itemId:'Url',fieldLabel:'入口地址',x:10,y:105,width:450,labelWidth:55,readOnly:true},
			
			{xtype:'textfield',name:'Para',itemId:'Para',fieldLabel:'入口参数',x:10,y:130,width:540,labelWidth:55},
			{xtype:'textareafield',name:'Comment',itemId:'Comment',fieldLabel:'模块描述',x:10,y:155,width:540,labelWidth:55,height:55},
			
			{xtype:'textfield',name:'parentModuleName',itemId:'parentModuleName',fieldLabel:'上级模块',x:10,y:215,width:170,labelWidth:55,value:me.ParentName,readOnly:true}
		];
		
		var items = hideFields.concat(showFields);
		var DataAddTime=Ext.create('Ext.zhifangux.DateField',{
            name:'DataAddTime',itemId:'DataAddTime',fieldLabel:'加入时间',x:220,y:215,width:160,labelWidth:55,format :'Y-m-d'
        });
        var DataUpdateTime=Ext.create('Ext.zhifangux.DateField',{
            name:'DataUpdateTime',itemId:'DataUpdateTime',fieldLabel:'更新时间',x:390,y:215,width:160,labelWidth:55,format :'Y-m-d'
        });

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
			xtype:'button',name:'getParentModuleButton',itemId:'getParentModuleButton',fieldLabel:'选择模块',x:185,y:215,
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
            DataAddTime.hidden=false;
            DataUpdateTime.hidden=false;
  
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
            DataAddTime.hidden=true;
            DataUpdateTime.hidden=true;
            linkToApp.hidden=false;
            file.hidden=false;
            parentModuleButton.hidden=false;
            selectIcon.hidden=false;
		}else if(me.type == "edit"){//修改面板特有的显示字段
			DataAddTime.hidden=true;
            DataUpdateTime.hidden=true;
            linkToApp.hidden=false;
            file.hidden=false;
            parentModuleButton.hidden=false;
            selectIcon.hidden=false;
			//加入时间
			DataAddTime.readOnly = true;
			//更新时间
			DataUpdateTime.readOnly = true;
		}
		//修改
        //加入时间
        items.push(DataAddTime);
        //更新时间
        items.push(DataUpdateTime);
        //关联应用
        items.push(linkToApp);
        var fileImg={xtype:'label',itemId:'fileImg',text:'模块图标:',x:193,y:80,width:60};
        if(me.type == "show"){//查看面板特有的显示字段
        //模块图标
        fileImg.hidden=false;
        file.hidden=true;
        }else{
        //模块图标
            fileImg.hidden=true;
            file.hidden=false;
        }
        items.push(fileImg);
        items.push(file);
        //选择上级模块按钮
        items.push(parentModuleButton);
        //图标下拉框
        items.push(selectIcon);
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
        var save={text:'保存',iconCls:'build-button-save',itemId:'save',handler:function(button){me.save();}};
        var saveas={text:'另存',iconCls:'build-button-save',itemId:'saveas',handler:function(button){me.saveAs();}};
        var refresh={text:'重置',iconCls:'build-button-refresh',itemId:'refresh',handler:function(button){me.getForm().reset();}};
        
        var toolbar={
            xtype:'toolbar',
            itemId:'toobar',
            dock:'bottom',
            items:items
        };
		
		if(type == "add"){//新增面板
            toolbar.hidden=false;
            save.hidden=false;
            saveas.hidden=false;
            refresh.hidden=false;
        }else if(type == "edit"){//修改面板
            toolbar.hidden=false;
            save.hidden=false;
            saveas.hidden=false;
            refresh.hidden=false;
        }else{//查看面板
            toolbar.hidden=true;
            save.hidden=true;
            saveas.hidden=true;
            refresh.hidden=true;
        }
        items.push(save);
        items.push(saveas);
        items.push(refresh);
        var dockedItems = [toolbar];
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
        var toobar = me.getComponent("toobar");
        var saveBtn =null;
		if (toobar) {
		    var saveBtn = toobar.getComponent("save");
		    if (saveBtn&&saveBtn!=null) {
		      saveBtn.setDisabled(true);
		    }
		}
        me.isSuccess=false;
		var params = me.getForm().getValues();
		var id = (params.Id && params.Id != "" ? params.Id : me.Id);
		if(saveType == "saveAs") {
            id = '-1';
        }
        
		//模块对象
		var entity = {
			Id:id,
			ParentID:params.ParentID,
			//DataTimeStamp:(params.DataTimeStamp == "") ? [] : params.DataTimeStamp.split(","),
			PicFile:(params.PicFile == "") ? "default.PNG" : params.PicFile,
			CName:params.CName,
			EName:params.EName,
			PinYinZiTou:params.PinYinZiTou,
			Shortcode:params.Shortcode,
			SName:params.SName,
			DispOrder:params.DispOrder,
			ModuleType:params.ModuleType,
			UseCode:params.UseCode,
			IsUse:(params.IsUse == "on") ? true : false,
			StandCode:params.StandCode,
			DeveCode:params.DeveCode,
			Url:params.Url,
			Para:params.Para,
			Comment:params.Comment,
			Owner:Ext.util.Cookies.get("EmployeeName") || '',
			LabID:'1'
		};
//        //更新时间的处理
//		var DataUpdateTime=params.DataUpdateTime;
//        if(DataUpdateTime&&DataUpdateTime!= ''&&DataUpdateTime!=null ){
//            DataUpdateTime=convertJSONDateToJSDateObject(DataUpdateTime);
//            entity.DataUpdateTime=DataUpdateTime;
//        }
        
		if(params['BTDAppComponents_Id'] && params['BTDAppComponents_Id'] != ''){
			entity.BTDAppComponents = {Id:params['BTDAppComponents_Id']};
			if(params['BTDAppComponents_DataTimeStamp'] &&params['BTDAppComponents_DataTimeStamp']!= ''){
				entity.BTDAppComponents.DataTimeStamp = params['BTDAppComponents_DataTimeStamp'].split(",");
			}
		}
		var url = me.addModuleServerUrl;
		var obj = {entity:entity};
		if(id != '-1'){
			url = me.editModuleServerUrl;
			//需要更新的字段,DataTimeStamp,DataUpdateTime
			obj.fields = "Id,ParentID,PicFile,CName,EName,PinYinZiTou,Shortcode,SName,DispOrder,ModuleType,UseCode,IsUse,StandCode,DeveCode,Url,Para,Comment";
			if(params['BTDAppComponents_Id'] && params['BTDAppComponents_Id']!= ''){
                obj.fields += ",BTDAppComponents_Id";
                if(params['BTDAppComponents_DataTimeStamp'] && params['BTDAppComponents_DataTimeStamp']!= ''){
	                obj.fields += ",BTDAppComponents_DataTimeStamp";
	            }
            }
		}
		//回调函数
		var callback = function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				var data = {};
				if(result.ResultDataValue && result.ResultDataValue != ''){
					data = Ext.JSON.decode(result.ResultDataValue);
				}
				data.id && (me.Id = data.id);
                me.isSuccess=true;
			}else{
                me.isSuccess=false;
				Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】');
			}
			params.Id = me.Id;
			me.fireEvent('saveClick');
            if (saveBtn&&saveBtn!=null) {
               saveBtn.setDisabled(false);
            }
		};
		var params = Ext.JSON.encode(obj);
		//POST方式与后台交互
        var defaultPostHeader='application/json';
        var async=false;
		postToServer(url,params,callback);
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
		if(newValue == 0||newValue =='0'){
			if(me.type=='add'){
                var obj = {Url:'',Para:''};
                me.setFormValues(obj);
            }
            Url.setReadOnly(true);
            Url.setWidth(450);
            linkToApp.show();
        }
		
		if(newValue == 1||newValue =='1'){
            if(me.type=='add'||me.type=='edit'){
                var obj = {Url:'',Para:''};
                me.setFormValues(obj);
            }
			Url.setReadOnly(false);
			Url.setWidth(540);
			linkToApp.hide();
		}else if(newValue == 1 && me.type != "show"){
			//清空应用信息
			var o = {'BTDAppComponents_Id':'','BTDAppComponents_DataTimeStamp':''};
			me.setFormValues(o);
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
            width:520,
            height:300,
            getAppListServerUrl:me.getAppListServerUrl,
            defaultLoad:true,
            readOnly:true,
            pageSize:10//每页数量
    	}).show();
        
    	appList.on({
    		okClick:function(){
    			var records = appList.getSelectionModel().getSelection();
    			if(records.length == 0){
    				Ext.Msg.alert("提示","请选择一个应用！");
    			}else if(records.length == 1){
    				var id = records[0].get('BTDAppComponents_Id');
    				var obj = {
	    				Url:me.appUrl + "?id=" + id,
	    				'BTDAppComponents_Id':records[0].get('BTDAppComponents_Id'),
						'BTDAppComponents_DataTimeStamp':records[0].get('BTDAppComponents_DataTimeStamp')
					};
	    			me.setFormValues(obj);
    				appList.close();
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
    			var id = record.get('BTDAppComponents_Id');
    			var obj = {
    				Url:me.appUrl + "?id=" + id,
    				'BTDAppComponents_Id':record.get('BTDAppComponents_Id'),
                    'BTDAppComponents_DataTimeStamp':record.get('BTDAppComponents_DataTimeStamp')
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
			//util中获取拼音字头的公共方法
			getPinYinZiTouFromServer(newValue,changePinYinZiTou);
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
		var win = Ext.create('Ext.manage.module.ModuleTree',{
			modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:200,
			height:340,
			title:'选择上级模块',
			selectId:ParentID.value,
            /***
		     * 是否过滤子节点
		     * @type Boolean
		     */
		    isFilterChildren:true,
			hideNodeId:me.Id
		}).show();
		win.on({
			okClick:function(){
    			var records = win.getSelectionModel().getSelection();
    			if(records.length == 0){
    				Ext.Msg.alert("提示","请选择一个模块！");
    			}else if(records.length == 1){
    				var id = records[0].get('tid');
    				ParentID.setValue(id);
    				
    				var name = records[0].get('text');
    				var parentModuleName = me.getForm().findField('parentModuleName');
    				parentModuleName.setValue(name);
    				
    				win.close();
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
    			var id = record.get('tid');
				ParentID.setValue(id);
				
				var name = record.get('text');
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
		var str = "Id,LabID,ParentID,LevelNum,TreeCatalog,Owner,DataTimeStamp,PrimaryKey,PicFile,BTDAppComponents_Id,BTDAppComponents_DataTimeStamp," + 
			"CName,EName,PinYinZiTou,Shortcode,SName,DispOrder,ModuleType,UseCode,StandCode,DeveCode,IsUse,Url,Para,Comment," + 
			"DataAddTime,DataUpdateTime";
			
		var arr = str.split(",");
		for(var i in arr){
			arr[i] = "RBACModule_" + arr[i];
		}
		var fields = arr.join(",");
		var url = me.getModuleInfoServerUrl + "?isPlanish=true&id="+id+"&fields="+fields;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:3000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						result.ResultDataValue = result.ResultDataValue.replace(/\r\n/g,'');
						var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
						var obj = {};
						for(var i in ResultDataValue){
							obj[i.slice(11)] = ResultDataValue[i];
						}
						callback(obj);//回调函数
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
        me.isLoadingComplete=false;
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
				var DataAddTime = me.getComponent("DataAddTime");
		        if (DataAddTime) {
                    var val = values.DataAddTime;
			        //数据处理
			        if(Ext.typeOf(val) === 'string' && val.length == 19){
			            val = val.slice(0,10);
                        DataAddTime.setValue(val);
			        }
		        }
		        var DataUpdateTime = me.getComponent("DataUpdateTime");
		        if (DataUpdateTime) {
		            DataUpdateTime.show();
                    var val = values.DataUpdateTime;
                    //数据处理
                    if(Ext.typeOf(val) === 'string' && val.length == 19){
                        val = val.slice(0,10);
                        DataUpdateTime.setValue(val);
                    }else{
                        var date=new Date();
                        DataUpdateTime.setValue(date);
                    }
		        }
                var ModuleType = ""+values.ModuleType;
                var moduleTypeCom = me.getComponent('ModuleType');//模块类型
                
				var linkToApp = me.getComponent('linkToApp');//关联应用按钮
				if(linkToApp && ModuleType =="0"){
                    moduleTypeCom.setValue('0');
					linkToApp.show();
				}else if(linkToApp && ModuleType =="1"){
                    moduleTypeCom.setValue('1');
					linkToApp.hide();
				}else{
                    moduleTypeCom.setValue('0');
                    linkToApp.show();
                }
                
			};
			me.getModuleInfoFromServer(id,callback);
            me.isLoadingComplete=false;
		}
	}
});