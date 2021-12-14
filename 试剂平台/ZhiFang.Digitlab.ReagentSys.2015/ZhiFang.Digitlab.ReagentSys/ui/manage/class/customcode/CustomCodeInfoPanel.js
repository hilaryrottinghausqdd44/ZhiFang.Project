/**
 * 定制程序信息类
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.customcode.CustomCodeInfoPanel',{
	extend:'Ext.form.Panel',
	alias:'widget.customcodeinfopanel',
	title:'定制程序维护',
	width:510,
	height:300,
	
	appId:-1,
	/**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    /**
     * 新增定制程序信息的后台服务地址
     * @type String
     */
    addAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponentsFileEx',
    /**
     * 修改定制程序信息的后台服务地址
     * @type String
     */
    editAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsFileEx',
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.setAppParams();
	},
	initComponent:function(){
		var me = this;
		//初始化面板参数
		me.initPanelParam();
		//创建面板内容
		me.items = me.createItems();
		//创建挂靠
		me.dockedItems = me.createDockedItems();
		//注册事件
		me.addEvents('saveClick');//保存按钮
		me.addEvents('cancelClick');//取消
		me.callParent(arguments);
	},
	/**
	 * 初始化面板参数
	 * @private
	 */
	initPanelParam:function(){
		var me = this;
		me.layout = "absolute";
		me.autoScroll = true;
		me.bodyPadding = 10;
		me.defaults = {
			labelWidth:60,
			width:230,
			labelAlgin:'right',
			padding:10
		};
	},
    /***
     * 应用类型
     */
    createComboboxDatas:function(){
        var me=this;
        var datas = Ext.create('Ext.data.Store', {
	    fields: ['value', 'text'],
	    data : [
	        {'value':'100', "text":"已录入项目"},
	        {'value':'101', "text":"检验项目分类"},
	        {'value':'102', "text":"模板录入"},
            {'value':'103', "text":"添加部门员工查询条件"},
            {'value':'104', "text":"申请帐号"},
            {'value':'105', "text":"帐号更新"},
            {'value':'106', "text":"员工帐号(员工维护)"}
	    ]
	   });
       return datas;
    },
	/**
	 * 创建面板内容
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//展现的内容
		var showItems = [{
			xtype:'textfield',fieldLabel:'功能名称',
			allowBlank:false,emptyText:'必填项,可以手工输入',
			name:'CName',itemId:'CName',
			x:0,y:0
		},{
			xtype:'textfield',fieldLabel:'功能编码',
			allowBlank:false,emptyText:'必填项',
			name:'ModuleOperCode',itemId:'ModuleOperCode',
			x:250,y:0
		},{
			xtype:'combobox',fieldLabel:'应用类型',
			allowBlank:false,emptyText:'必填项',
            store: me.createComboboxDatas(),
		    queryMode: 'local',
		    displayField: 'text',
		    valueField: 'value',
			name:'AppType',itemId:'AppType',
			x:0,y:25
		},{
			xtype:'filefield',fieldLabel:'程序文件',
			allowBlank:!(me.appId == -1),
			emptyText:(me.appId == -1 ? '必填项' : ''),
			buttonConfig:{iconCls:'search-img-16',text:'选择'},
			name:'CodeFile',itemId:'CodeFile',
			x:0,y:50,width:480
		},{
			xtype:'textarea',fieldLabel:'初始条件',
			name:'InitParameter',itemId:'InitParameter',
			x:0,y:75,width:480,height:60
		},{
			xtype:'textarea',fieldLabel:'功能简介',
			name:'ModuleOperInfo',itemId:'ModuleOperInfo',
			x:0,y:140,width:480,height:80
		}];
		//隐藏的内容
		var hideItems = [{
			xtype:'textfield',fieldLabel:'ID',hidden:true,
			name:'Id',itemId:'Id',value:me.appId
		},{
			xtype:'textfield',fieldLabel:'时间戳',hidden:true,
			name:'DataTimeStamp',itemId:'DataTimeStamp'
		},{
			xtype:'textfield',fieldLabel:'开发方式',hidden:true,
			name:'BuildType',itemId:'BuildType',value:'0'
		}];
		//面板内容
		var items = showItems.concat(hideItems);
		
		return items;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [];
		
		var buttons = me.createButtons();
		
		var toolbar = {
			xtype:'toolbar',
			dock:'bottom',
			items:buttons
		};
		
		dockedItems.push(toolbar);
		
		return dockedItems;
	},
	/**
	 * 创建按钮组
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		var buttons = ['->',{
			xtype:'button',text:'清空文件',
			iconCls:'build-button-save',
			handler:function(){me.getComponent('CodeFile').setRawValue('');}
		},{
			xtype:'button',text:'保存',
			iconCls:'build-button-save',
			handler:function(){me.save();}
		},{
			xtype:'button',text:'重置',
			iconCls:'build-button-refresh',
			handler:function(){me.getForm().reset();}
		},{
			xtype:'button',text:'取消',
			iconCls:'build-button-cancel',
			handler:function(){me.cancel();}
		}];
		return buttons;
	},
	/**
	 * 保存数据
	 * @private
	 */
	save:function(){
		var me = this;
		if (!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		//需要保存的对象
		var obj = {
			Id:values.Id,
			CName:values.CName,
			ModuleOperCode:values.ModuleOperCode,
			AppType:values.AppType,
			CodeFile:values.CodeFile,
			InitParameter:values.InitParameter,
			ModuleOperInfo:values.ModuleOperInfo
		};
		if(values.DataTimeStamp != ""){
			obj.DataTimeStamp = values.DataTimeStamp.split(",");
		}
		
		var callback = function(){
			me.fireEvent('saveClick');
		};
		me.saveToServer(obj,callback);
	},
	/**
	 * 取消
	 * @private
	 */
	cancel:function(){
		var me = this;
		me.fireEvent('cancelClick');
	},
	/**
     * 给页面赋值
     * @private
     */
	setAppParams:function(){
		var me = this;
		var callback = function(appInfo){
			var obj = {
				CName:appInfo['BTDAppComponents_CName'],
				ModuleOperCode:appInfo['BTDAppComponents_ModuleOperCode'],
				AppType:appInfo['BTDAppComponents_AppType'],
				InitParameter:appInfo['BTDAppComponents_InitParameter'],
				ModuleOperInfo:appInfo['BTDAppComponents_ModuleOperInfo'],
				Id:appInfo['BTDAppComponents_Id'],
				DataTimeStamp:appInfo['BTDAppComponents_DataTimeStamp']
			};
			me.getForm().setValues(obj);
		};
		//从后台获取应用信息
        me.getAppInfoFromServer(me.appId,callback);
	},
	//=====================后台获取&存储=======================
	/**
     * 从后台获取应用信息
     * @private
     * @param {} callback
     */
    getAppInfoFromServer:function(id,callback){
        var me = this;
        
        if(id != -1){
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:5000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        var appInfo = "";
                        if(result.ResultDataValue && result.ResultDataValue != ""){
                        	result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
                        	appInfo = Ext.JSON.decode(result.ResultDataValue);
                        }
                        
                        if(Ext.typeOf(callback) == "function"){
                        	if(appInfo == ""){
                        		alertError('没有获取到应用组件信息!');
                        	}else{
                        		callback(appInfo);//回调函数
                        	}
                        }
                    }else{
                        alertError(result.errorInfo);
                    }
                },
                failure : function(response,options){ 
                    alertError('获取应用组件信息请求失败!');
                }
            });
        }
    },
    /**
	 * 把数据提交到后台
	 * @private
	 * @param {} obj
	 * @param {} callback
	 */
	saveToServer:function(obj,callback){
		var me = this;
		var url = me.getForm().getValues().Id == -1 ? me.addAppInfoServerUrl : me.editAppInfoServerUrl;
//		Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
//		Ext.Ajax.request({
//			async:false,//非异步
//			url:url,
//			method:'POST',
//			params:obj,
//			timeout:2000,
//			success:function(response,opts){
//				var result = Ext.JSON.decode(response.responseText);
//				if(result.success){
//					if(Ext.typeOf(callback) == "function"){
//						callback();//回调函数
//					}
//				}else{
//					Ext.Msg.alert('提示','保存定制代码信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
//				}
//			},
//			failure:function(response,options){ 
//				Ext.Msg.alert('提示','保存定制代码服务失败！');
//			}
//		});
		
        if (me.getForm().isValid()) {
            me.getForm().submit({
                url:url,
                waitMsg:"提交中...",
                success: function (form,action) {
                	var result = action.result;
                	if(result.success){
                    	alertInfo('提交成功!');
                	}else{
                		alertError(result.ErrorInfo);
                	}
                },
                failure:function(form,action){
					alertError('提交请求服务失败!');
				}
            });
        }
	}
});