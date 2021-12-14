/**
 * 定制代码列表类
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.customcode.CustomCodeListPanel',{
	extend:'Ext.grid.Panel',
	alias:'widget.customcodelistpanel',
	title:'定制程序列表',
	/**
	 * 是否默认加载数据
	 * @type Boolean
	 */
	defaultLoad:true,
	/**
	 * 是否开启远程排序
	 * @type Boolean
	 */
	remoteSort:false,
	/**
	 * 内部hql
	 * @type String
	 */
	internalWhere:'',
	/**
	 * 外部hql
	 * @type String
	 */
	externalWhere:'',
	/**
	 * 获取应用列表的服务地址
	 * @type String
	 */
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId',
	/**
	 * 删除应用信息的服务地址
	 * @type 
	 */
	deleteAppServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_DelBTDAppComponents',
	/**
	 * 根据ID获取一条应用信息的服务地址
	 * @type 
	 */
	getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	/**
	 * 应用字段对象
	 * @type 
	 */
	fieldsObj:{
		/**
		 * 应用组件ID
		 * @type String
		 */
		AppComID:'BTDAppComponents_Id',
		/**
		 * 中文名称
		 * @type String
		 */
		CName:'BTDAppComponents_CName',
		/**
		 * 英文名称
		 * @type String
		 */
		EName:'BTDAppComponents_EName',
		/**
		 * 功能编码
		 * @type String
		 */
		ModuleOperCode:'BTDAppComponents_ModuleOperCode',
		/**
		 * 功能简介
		 * @type String
		 */
		ModuleOperInfo:'BTDAppComponents_ModuleOperInfo',
		/**
		 * 初始化参数
		 * @type String
		 */
		InitParameter:'BTDAppComponents_InitParameter',
		/**
		 * 应用类型
		 * @type String
		 */
		AppType:'BTDAppComponents_AppType',
		/**
		 * 构建类型
		 * @type String
		 */
		BuildType:'BTDAppComponents_BuildType',
		/**
		 * 模块类型
		 * @type String
		 */
		BTDModuleType:'BTDAppComponents_BTDModuleType',
		/**
		 * 执行代码
		 * @type String
		 */
		ExecuteCode:'BTDAppComponents_ExecuteCode',
		/**
		 * 设计代码
		 * @type String
		 */
		DesignCode:'BTDAppComponents_DesignCode',
		/**
		 * 类代码
		 * @type String
		 */
		ClassCode:'BTDAppComponents_ClassCode',
		/**
		 * 创建者
		 * @type String
		 */
		Creator:'BTDAppComponents_Creator',
		/**
		 * 修改者
		 * @type String
		 */
		Modifier:'BTDAppComponents_Modifier',
		/**
		 * 汉字拼音字头
		 * @type String
		 */
		PinYinZiTou:'BTDAppComponents_PinYinZiTou',
		/**
		 * 数据加入时间
		 * @type String
		 */
		DataAddTime:'BTDAppComponents_DataAddTime',
		/**
		 * 数据更新时间
		 * @type String
		 */
		DataUpdateTime:'BTDAppComponents_DataUpdateTime',
		/**
		 * 实验室ID
		 * @type String
		 */
		LabID:'BTDAppComponents_LabID',
		/**
		 * 时间戳
		 * @type String
		 */
		DataTimeStamp:'BTDAppComponents_DataTimeStamp'
	},
	//=====================面板内容=======================
	afterRender:function(){
        var me = this;
		me.callParent(arguments);
		//是否加载数据
		if(me.defaultLoad){
			me.load(me.externalWhere);
		}
	},
	initComponent:function(){
		var me = this;
		//初始化面板参数
		me.initPanelParam();
		//创建数据集
		me.store = me.createStore();
		//创建数据列
		me.columns = me.createColumns();
		//创建挂靠
		me.dockedItems = me.createDockedItems();
		//监听
		me.initListeners();
		me.callParent(arguments);
	},
	/**
	 * 初始化面板参数
	 * @private
	 */
	initPanelParam:function(){
		var me = this;
		Ext.Loader.setPath('Ext.manage', '../../class');
		me.selType = 'checkboxmodel';//复选框
		me.multiSelect = true;//允许多选
		me.viewConfig = {
	        emptyText:'没有数据！',
	        loadingText:'获取数据中，请等待...'
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
	},
	/**
	 * 列表监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		me.listeners = {
			itemdblclick:function(com,record,item,index,e,eOpts){
				//处理代码
				var callback = function(appInfo){
					//中文名称
					var title = record.get(me.fieldsObj.CName);
					//类代码
					var ClassCode = "";
					if(appInfo && appInfo != ""){
						ClassCode = appInfo[me.fieldsObj.ClassCode];
					}
					
					if(ClassCode && ClassCode != ""){
						//打开应用效果窗口
						var id = appInfo[me.fieldsObj.AppComID];
						me.openAppShowWin(title,ClassCode,id);
					}else{
						alertError("没有类代码！");
					}
				}
				
				//ID号
				var id = record.get(me.fieldsObj.AppComID);
				var p = Ext.WindowManager.get(id);
				if(p){//已经打开了窗口
					Ext.WindowManager.bringToFront(p);
				}else{
					//与后台交互
					me.getInfoByIdFormServer(id,callback);
				}
			}
		};
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(),
			proxy:{
				type:'ajax',
				url:me.getDefaultUrl(),
				reader:{
	            	type:'json',
	            	totalProperty:'count',
	                root:'list'
	            },
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText);
			    	if(result.ResultDataValue && result.ResultDataValue != ""){
			    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
				    	result.list = ResultDataValue['list'];
				    	result.count = ResultDataValue['count'];
			    	}else{
			    		result.list = [];
			    		result.count = 0;
			    	}
			    	
			    	if(!result.success){
			    		alertError('获取应用列表失败！错误信息:'+result.ErrorInfo);
			    	}
			    	
			    	response.responseText = Ext.JSON.encode(result);
			    	return response;
				}
			}
		});
		return store;
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [{
			text:'序号',xtype:'rownumberer',width:50,align:'right'
		},{
			text:'应用ID',dataIndex:me.fieldsObj.AppComID,width:150,editor:{readOnly:true}
		},{
			text:'功能编码 ',dataIndex:me.fieldsObj.ModuleOperCode,width:150,editor:{readOnly:true}
		},{
			text:'中文名称',dataIndex:me.fieldsObj.CName,width:150,editor:{readOnly:true}
		},{
			dataIndex:me.fieldsObj.DataAddTime,
			width:140,
			text:'创建时间'
//			xtype:'datecolumn',
//			format:'Y-m-d H:i:s'
		},{
			text:'提示',dataIndex:'hasBeenDeleted',width:60,
	 		renderer:function(value, p, record){
				if(value == "true"){
					return Ext.String.format("<b style='color:gray'>已删除</b>");
				}else if(value == "false"){
					return Ext.String.format("<b style='color:red'>删除失败</b>");
				}else{
					return Ext.String.format("");
				}
	 		}
		},{
			xtype:'actioncolumn',text:'操作',width:60,align:'center',
			items:[{
				iconCls:'build-button-edit hand',
                tooltip:'修改定制程序信息',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	me.openCustomCodeInfoWin(record.get(me.fieldsObj.AppComID));
                }
			}]
		}];
		return columns;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		//按钮组
		var buttons = me.createButtons();
		//功能栏
		var toolbar = {
			xtype:'toolbar',
			itemId:'toolbar-top',
			items:buttons
		};
		//分页栏
		var pagingtoolbar = {
			xtype:'pagingtoolbar',
			store:me.store,
			dock:'bottom',
			displayInfo:true
		};
		
		var dockedItems = [toolbar,pagingtoolbar];
		return dockedItems;
	},
	/**
	 * 创建按钮组
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		var buttons = [{
			xtype:'button',text:'上传',iconCls:'build-button-add',
        	tooltip:'上传定制代码',
        	handler:function(button){me.openCustomCodeInfoWin();}
		},{
			xtype:'button',text:'删除',itemId:'DeleteButton',
			iconCls:'build-button-delete',
			handler:function(button){me.deleteAppInfo();}
		},{
			xtype:'button',text:'更新',itemId:'ListLoadButton',
			iconCls:'build-button-refresh',
			handler:function(button){me.load(me.externalWhere);}
		},'->',{
			xtype:'textfield',itemId:'searchText',width:180,
        	emptyText:'应用ID、功能编号、中文名称',
        	listeners:{
        		render:function(input){
			    	new Ext.KeyMap(input.getEl(),[{
				      	key:Ext.EventObject.ENTER,
				      	fn:function(){me.search();}
			     	}]);
			    }
        	}
		},{
			xtype:'button',text:'查询',iconCls:'search-img-16 ',
        	tooltip:'按照应用类型、功能编号、中文名称进行查询',
        	handler:function(button){me.search();}
		}];
		return buttons;
	},
	//=====================内部方法=======================
	/**
     * 列表中的数据列
     * @private
     * @return {}
     */
    getFields:function(){
    	var me = this;
    	var fields = [
			{name:me.fieldsObj.AppComID,type:'string'},//应用组件ID
			{name:me.fieldsObj.CName,type:'string'},//中文名称
//			{name:me.fieldsObj.EName,type:'string'},//英文名称
			{name:me.fieldsObj.ModuleOperCode,type:'string'},//功能编码
			{name:me.fieldsObj.ModuleOperInfo,type:'string'},//功能简介
//			{name:me.fieldsObj.InitParameter,type:'string'},//初始化参数
			{name:me.fieldsObj.AppType,type:'string'},//应用类型
//			{name:me.fieldsObj.BuildType,type:'string'},//构建类型
//			{name:me.fieldsObj.BTDModuleType,type:'string'},//模块类型
//			{name:me.fieldsObj.ExecuteCode,type:'string'},//执行代码
//			{name:me.fieldsObj.DesignCode,type:'string'},//设计代码
//			{name:me.fieldsObj.ClassCode,type:'string'},//类代码
//			{name:me.fieldsObj.Creator,type:'string'},//创建者
//			{name:me.fieldsObj.Modifier,type:'string'},//修改者
//			{name:me.fieldsObj.PinYinZiTou,type:'string'},//汉字拼音字头
			{name:me.fieldsObj.DataAddTime,type:'string'},//数据加入时间
//			{name:me.fieldsObj.DataUpdateTime,type:'string'},//数据更新时间
//			{name:me.fieldsObj.LabID,type:'string'},//实验室ID
//			{name:me.fieldsObj.DataTimeStamp,type:'string'},//时间戳
			
			{name:'hasBeenDeleted',type:'string'}//已经被删的标记
		];
		return fields;
    },
    /**
     * 获取需要的数据列Str
     * @private
     */
    getFieldsStr:function(){
    	var me = this;
    	var fieldsStr = "";
    	var fields = me.getFields();
    	for(var i in fields){
    		if(fields[i].name != "hasBeenDeleted"){
    			fieldsStr += fields[i].name + ",";
    		}
    	}
    	if(fieldsStr != ""){
    		fieldsStr = fieldsStr.substring(0,fieldsStr.length-1);
    	}
    	return fieldsStr;
    },
    /**
     * 获取默认的路径
     * @private
     */
    getDefaultUrl:function(){
    	var me = this;
    	var url = me.getAppListServerUrl + "?isPlanish=true&fields=" + me.getFieldsStr();
		if(me.filterId && me.filterId != "" && me.filterId != -1){
			url += "&AppId=" + me.filterId;
		}
		return url;
    },
    /**
     * 获取带查询参数的URL
     * @private
     * @return {}
     */
    getLoadUrl:function(){
    	var me = this;
    	var url = me.getDefaultUrl();
		if(me.remoteSort && me.sort && me.sort != ""){
			url += "&sort=" + me.sort;
		}
    	
    	//var w="";
    	var w="btdappcomponents.BuildType=0"//非构建类型
		if(me.internalWhere){
			w += me.internalWhere;
		}
		if(me.externalWhere && me.externalWhere != ""){
			if(w != ""){
				w += " and " + me.externalWhere;
			}else{
				w += me.externalWhere;
			}
		} 
		
		return (url + "&where=" + w);
    },
    /**
     * 检索
     * @private
     */
    search:function(){
    	var me = this;
    	var toolbar = me.getComponent('toolbar-top');
		var value = toolbar.getComponent('searchText').getValue();
		
		var where = "";
		if(value && value != ""){
			where += "(btdappcomponents.Id like '%25" + value + "%25' or btdappcomponents.CName like '%25" + value + "%25' or btdappcomponents.ModuleOperCode like '%25" + value + "%25')";
		}
		
		me.store.currentPage = 1;
		
		me.load(where);
    },
    /**
     * 打开定值程序信息维护界面
     * @private
     * @param {} id
     */
    openCustomCodeInfoWin:function(id){
    	var me = this;
    	var appId = id || -1;
    	var title = appId == -1 ? "定制程序【新增】" : "定制程序【修改】";
    	var win = Ext.create('Ext.manage.customcode.CustomCodeInfoPanel',{
    		modal:true,//模态
    		resizable:true,//可变大小
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			title:title,
			appId:appId
    	}).show();
    	//保存监听
		win.on({
			saveClick:function(){
				win.close();
				me.load(me.externalWhere);
			}
		});
    },
    /**
     * 删除记录信息
     * @private
     */
    deleteAppInfo:function(){
    	var me = this;
    	var records = me.getSelectionModel().getSelection();
    	if(records.length > 0){
    		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
    			if(button == "yes"){
    				Ext.Array.each(records,function(record){
		    			//没有被删除的才去后台删除
			    		if(record.get('hasBeenDeleted') != "true"){
			    			me.deleteAppServer(record);
			    		}
		    		});
    			}
    		});
    	}else{
    		alertError('请勾选需要删除的记录！');
    	}
    },
    /**
	 * 打开应用效果窗口
	 * @private
	 * @param {} title
	 * @param {} ClassCode
	 * @param {} id
	 */
	openAppShowWin:function(title,ClassCode,id){
		var me = this;
		var panel = eval(ClassCode);
		var maxHeight = document.body.clientHeight*0.98;
		var maxWidth = document.body.clientWidth*0.98;
		var win = Ext.create(panel,{
			id:id,
			maxWidth:maxWidth,
			autoScroll:true,
    		modal:false,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true//可移动
    	});
    	
		if(win.height > maxHeight){
			win.height = maxHeight;
		}
    	//解决chrome浏览器的滚动条问题
    	var callback = function(){
    		win.hide();
    		win.show();
    	}
    	win.show(null,callback);
	},
    //=====================与后台交互=======================
    /**
     * 后台删除应用信息
     * @private
     * @param {} id
     */
    deleteAppServer:function(record){
    	var me = this;
    	var url = me.deleteAppServerUrl + "?id=" + record.get(me.fieldsObj.AppComID);
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
		    		record.set("hasBeenDeleted","true");
		    		record.commit();
				}else{
					record.set("hasBeenDeleted","false");
		    		record.commit();
				}
			},
			failure:function(response,options){ 
				record.set("hasBeenDeleted","false");
		    	record.commit();
			}
		});
    },
    /**
     * 根据ID获取一条应用信息
     * @private
     * @param {} id
     * @param {} callback
     */
    getInfoByIdFormServer:function(id,callback){
   		var me = this;
   		var url = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
   		Ext.Ajax.defaultPostHeader = 'application/json';
   		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					var appInfo = "";
					if(result.ResultDataValue && result.ResultDataValue != ""){
						result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
						appInfo = Ext.JSON.decode(result.ResultDataValue);
					}
		    		if(Ext.typeOf(callback) == "function"){
						callback(appInfo);//回调函数
					}
				}else{
					alertError('获取应用信息失败！');
				}
			},
			failure:function(response,options){ 
				alertError('获取应用信息请求失败！');
			}
		});
   	},
    //=====================对外公开方法=======================
    /**
     * 刷新列表
     * @public
     */
    load:function(where){
    	var me = this;
    	me.externalWhere = where;
    	me.store.proxy.url = me.getDefaultUrl();
		me.store.proxy.url = me.getLoadUrl();
    	me.store.load();
    }
});