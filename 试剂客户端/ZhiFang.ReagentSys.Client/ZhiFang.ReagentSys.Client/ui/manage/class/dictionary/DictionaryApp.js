Ext.ns('Ext.manage');
Ext.define('Ext.manage.dictionary.DictionaryApp',{
	extend:'Ext.panel.Panel',
	alias: 'widget.dictionaryapp',
	/**
	 * 面板标题
	 * @type String
	 */
	title:'字典维护',
	header:false,//不显示title
	/**
	 * 是否默认加载数据
	 * @type Boolean
	 */
	defaultLoad:true,
	/**
	 * 需要过滤的数据字段
	 * @type 
	 */
	filterFields:['DataTimeStamp','DataAddTime','LabID','dataStatus'],
	//=====================内部变量=======================
	/**
	 * 字典类型列表服务地址
	 * @type 
	 */
    getDictionaryTypeListUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchBDicClassByHQL?fields=BDicClass_Id,BDicClass_Name,BDicClass_Code',
    /**
     * 字典列表服务地址
     * @type 
     */
    getDictionaryListUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchBDicByHQL?fields=BDic_Id,BDic_Name,BDic_Code',
    /**
     * 根据对象名获取对象结构及增删改查服务
     * @type 
     */
    getEntityStructureAndServiceByEntityNameUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetCRUDAndFrameByEntityName',
    //=====================内部视图渲染=======================
    /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	//初始化监听
    	me.initListeners();
    	
    	//字典类型列表是否默认加载数据
    	var dictionaryTypeList = me.getComponent('dictionaryTypeList');
    	me.defaultLoad && dictionaryTypeList.store.load();
    },
	/**
	 * 初始化应用
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//初始化参数
		me.initParams();
		//初始化视图
		me.initView();
		me.callParent(arguments);
	},
	/**
	 * 初始化参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		me.layout = "border";
	},
	/**
	 * 初始化视图
	 * @private
	 */
    initView:function(){
    	var me = this;
    	//内部模块
    	me.items = me.createItems();
    },
    /**
     * 创建内部模块
     * @private
     */
    createItems:function(){
    	var me = this;
    	//字典类型列表
    	var dictionaryTypeList = me.createDictionaryTypeList();
    	//字典列表
    	var dictionaryList = me.createDictionaryList();
    	//字典内容列表
    	var dictionaryContentsList = me.createDictionaryContentsList();
    	
    	//功能模块ItemId
    	dictionaryTypeList.itemId = "dictionaryTypeList";
    	dictionaryList.itemId = "dictionaryList";
    	dictionaryContentsList.itemId = "dictionaryContentsList";
    	
    	//功能块位置
    	dictionaryTypeList.region = "west";
    	dictionaryList.region = "west";
    	dictionaryContentsList.region = "center";
    	
    	//功能块大小
    	dictionaryTypeList.width = 150;
    	dictionaryList.width = 250;
    	
    	//功能块收缩属性
		dictionaryTypeList.split = true;
		dictionaryTypeList.collapsible = true;
		dictionaryList.split = true;
    	dictionaryList.collapsible = true;
    	
    	//功能标题
    	dictionaryTypeList.title = "字典类型列表";
    	dictionaryList.title = "字典列表";
    	dictionaryContentsList.title = "字典内容列表";
    	
    	var items = [dictionaryTypeList,dictionaryList,dictionaryContentsList];
    	return items;
    },
    //------------------字典类型列表-------------------
    /**
     * 创建字典类型列表
     * @private
     * @return {}
     */
    createDictionaryTypeList:function(){
    	var me = this;
    	//数据集
    	var url = me.getDictionaryTypeListUrl;
    	var fields = ['Id','Name','Code'];
    	var store = me.createStore(url,fields);
    	//数据列
    	var columns = me.createDictionaryTypeListColumns();
    	
    	//字典类型列表
    	var com = {
    		xtype:'grid',
    		store:store,
    		columns:columns,
    		tools:[{//面板功能栏
				type:'refresh',tooltip:'刷新数据',itemId:'refresh',
				handler:function(event,toolEl,owner,tool){
					tool.disable();com.load();
				}
    		}],
    		viewConfig:{
    			emptyText:'没有数据！',
    			loadingText:'数据加载中...',
    			loadMask:true
    		},
    		load:function(where){
    			com.store.load();
    		}
    	};
    	return com;
    },
    /**
     * 创建字典类型列表数据列
     * @private
     * @return {}
     */
    createDictionaryTypeListColumns:function(){
    	var me = this;
    	var columns = [
    		{dataIndex:'Id',text:'类型列表编号',hidden:true},
    		{dataIndex:'Name',text:'类型名称'}
    	];
    	return columns;
    },
    //--------------------字典列表--------------------
    /**
     * 创建字典列表
     * @private
     * @return {}
     */
    createDictionaryList:function(){
    	var me = this;
    	//数据集
    	var url = me.getDictionaryListUrl;
    	var fields = ['Id','Name','Code'];
    	var store = me.createStore(url,fields);
    	//数据列
    	var columns = me.createDictionaryListColumns();
    	//字典类型列表
    	var com = {
    		xtype:'grid',
    		store:store,
    		columns:columns,
    		tools:[{//面板功能栏
				type:'refresh',tooltip:'刷新数据',itemId:'refresh',disabled:true,
				handler:function(event,toolEl,owner,tool){
					tool.disable();com.load();
				}
    		}],
    		viewConfig:{
    			emptyText:'没有数据！',
    			loadingText:'数据加载中...',
    			loadMask:true
    		},
    		load:function(where){
    			com.store.load();
    		}
    	};
    	return com;
    },
    /**
     * 创建字典列表数据列
     * @private
     * @return {}
     */
    createDictionaryListColumns:function(){
    	var me = this;
    	var columns = [
    		{dataIndex:'Id',text:'编号',hidden:true},
    		{dataIndex:'Name',text:'名称'}
    	];
    	return columns;
    },
    //------------------字典内容列表-------------------
    /**
     * 创建字典内容列表
     * @private
     * @return {}
     */
    createDictionaryContentsList:function(){
    	var me = this;
    	var com = {
    		xtype:'grid',
    		columnLines:true,//在行上增加分割线
    		plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
    		columns:[],
    		tools:[{//面板功能栏
				type:'refresh',tooltip:'刷新数据',itemId:'refresh',disabled:true,
				handler:function(event,toolEl,owner,tool){
					tool.disable();com.load();
				}
    		}],
    		viewConfig:{
    			emptyText:'没有数据！',
    			loadingText:'数据加载中...',
    			loadMask:true
    		},
    		load:function(){
    			var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    			dictionaryContentsList.store.load();
    		},
    		dockedItems:[{
				xtype:'toolbar',
				itemId:'buttonstoolbar',
				dock:'top',
				items:[{
					text:'增加',tooltip:'新增一条记录',
					itemId:'add',disabled:true,
					iconCls:'build-button-add',handler:function(button){
						me.insertEmptyRecord();
					}
				},{
					text:'取消',tooltip:'重新获取数据，不保存修改的数据',
					itemId:'cancel',disabled:true,
					iconCls:'build-button-cancel',handler:function(button){
						me.cancelContents();
					}
				},{
					text:'保存',tooltip:'批量保存数据，凡是修改过的数据都会保存到数据库',
					itemId:'save',disabled:true,
					iconCls:'build-button-save',handler:function(button){
						me.saveContents();
					}
				}]
			}]
			//键盘监听
			//listeners:{render:function(component){me.initKeyEvent(component);}}
    	};
    	return com;
    },
    //--------------------------------------------------------------
    /**
     * 更改字典内容列表需要的字段
     * @private
     * @param {} obj
     */
    changeDictionaryContentsListFields:function(obj){
    	var me = this;
    	me.dictionaryContentsListFields = [];
    	var EntityFrameTree = obj.EntityFrameTree;
    	for(var i in EntityFrameTree){
    		var columnName = EntityFrameTree[i].InteractionField;
    		if(!me.isFilterField(columnName) && columnName.split('_').slice(-1) != 'Id'){
	    		me.dictionaryContentsListFields.push({
	    			text:EntityFrameTree[i].text || '',
	    			dataIndex:columnName
	    		});
    		}
    	}
    },
    /**
     * 根据对象名改变字典内容
     * @private
     * @param {} entityName
     */
    changeDictionaryContentsList:function(entityName){
    	var me = this;
    	var callback = function(responseText){
    		var result = Ext.JSON.decode(responseText);
    		if(result.success){
    			if(result.ResultDataValue && result.ResultDataValue != ""){
		    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
		    		//更改字典内容列表需要的字段
		    		me.changeDictionaryContentsListFields(ResultDataValue);
		    		//更改字典内容列表新增、删除、修改服务地址属性
		    		me.changeDictionaryContentsListServerInfo(ResultDataValue);
		    		//创建字典内容列表数据集
		    		var store = me.createDictionaryContentsListStore(ResultDataValue);
		    		//创建字典内容列表数据列
		    		var columns = me.createDictionaryContentsListColumns(ResultDataValue);
		    		//改变字典内容列表视图
		    		me.changeDictionaryContentsListView(store,columns);
		    	}
    		}else{
    			Ext.Msg.alert('提示','错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
    		}
    	};
    	//util-GET方式与后台交互
    	var url = me.getEntityStructureAndServiceByEntityNameUrl + "?EntityName=" + entityName;
    	getToServer(url,callback);
    },
    /**
     * 创建字典内容列表数据集
     * @private
     * @param {} obj
     * @return {}
     */
    createDictionaryContentsListStore:function(obj){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var url =dictionaryContentsList.selectUrl+"?isPlanish=true";
    	
    	var fields = ['dataStatus'];
    	var EntityFrameTree = obj.EntityFrameTree;
    	for(var i in EntityFrameTree){
    		fields.push(EntityFrameTree[i].InteractionField);
    	}
    	
    	var store = me.createStore(url,fields);
    	store.on({
    		load:function(com,records,successful,eOpts){
				me.disableButtons(false);
    		}
    	});
    	return store;
    },
    /**
     * 创建字典内容列表数据列
     * @private
     * @param {} obj
     * @return {}
     */
    createDictionaryContentsListColumns:function(obj){
    	var me = this;
    	var columns = [];
    	
    	var fields = me.dictionaryContentsListFields || [];
    	
    	for(var i in fields){
    		var column = {
    			text:fields[i].text || '',
    			dataIndex:fields[i].dataIndex,
    			editor:{}
    		};
    		var lastWord = fields[i].dataIndex.split('_').slice(-1);
    		if(lastWord == 'IsUse'){
    			column.xtype = 'checkcolumn';
    			column.align = 'center';
    			column.width = 60;
    			column.editor = {
	                xtype:'checkbox',
	                cls:'x-grid-checkheader-editor'
	            };
    		}else if(lastWord == 'DispOrder'){
    			column.xtype = 'numbercolumn';
    			column.align = 'right';
    			column.width = 60;
    			column.format = '0';
    			column.allowBlank = false;
    			column.editor = {
	                xtype:'numberfield',
	                allowBlank:false
	            };
    		}
    		columns.push(column);
    	}
    	columns.push({
    		text:'状态',width:70,align:'center',hideable:false,
    		dataIndex:'dataStatus'
    	});
    	columns.push({
    		xtype:'actioncolumn',text:'操作',width:40,align:'center',hideable:false,
			items:[{
				iconCls:'build-button-delete hand',
                tooltip:'删除该条记录',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	me.delContents(grid,rowIndex,colIndex,item,e,record);
                }
			}]
    	});
    	
    	return columns;
    },
    /**
     * 更改字典内容列表新增、删除、修改服务地址属性
     * @private
     * @param {} obj
     */
    changeDictionaryContentsListServerInfo:function(obj){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	dictionaryContentsList.addUrl = getRootPath() + "/" + obj.CreatServiceAddress;
    	dictionaryContentsList.editUrl = getRootPath() + "/" +  obj.UpdateServiceAddress;
    	dictionaryContentsList.delUrl = getRootPath() + "/" +  obj.DeleteServiceAddress.split('?')[0];
    	dictionaryContentsList.selectUrl = getRootPath() + "/" +  obj.RetrieveServiceAddress.split('?')[0];
    	dictionaryContentsList.editfields = me.getEditFields(obj);
    },
    /**
     * 改变字典内容列表视图
     * @private
     * @param {} store 数据集
     * @param {} columns 数据列信息
     */
    changeDictionaryContentsListView:function(store,columns){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	//更改分页栏数据集
    	//dictionaryContentsList.getComponent('pagingtoolbar').bindStore(store);
    	me.store = store;
    	//更改列表数据集
    	dictionaryContentsList.reconfigure(store,columns);
    	//加载数据
    	dictionaryContentsList.store.load();
    },
    //=======================字典内容列表按钮事件处理=====================
    /**
     * 插入一条空数据
     * @private
     */
    insertEmptyRecord:function(){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var isUse = dictionaryContentsList.entityName+'_IsUse';
    	var obj = {};
    	
    	var fields = me.dictionaryContentsListFields || [];
    	for(var i in fields){
    		var lastWord = fields[i].dataIndex.split('_').slice(-1);
    		if(lastWord == 'DispOrder'){
    			obj[fields[i].dataIndex] = 0;
    		}
    	}
    	
    	obj[isUse] = false;
    	var rec = ('Ext.data.Model',obj);
		dictionaryContentsList.store.add(rec);
    },
    /**
     * 保存按钮事件处理
     * @private
     */
    saveContents:function(){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var store = dictionaryContentsList.store;
    	
    	var count = 0;
    	var call = function(){
    		count++;
    		var length = addArr.length+editArr.length;
    		if(count == length){
    			me.disableButtons(false);//启用功能按钮
    		}
    	};
    	
    	var addArr = [];//需要新增的数据
    	var editArr = [];//需要修改的数据
    	var hasDirtyData = false;//是否有脏数据
    	store.each(function(record){
    		var dirty = record.dirty;//是否是脏数据
    		if(dirty){
    			var id = record.get(dictionaryContentsList.entityName+'_Id');
    			if(id == ''){//新增的数据
    				addArr.push(record);
    			}else{//修改的数据
    				editArr.push(record);
    			}
    			hasDirtyData = true;
    			record.set('dataStatus','');
    		}else{
    			record.set('dataStatus','');
    			record.commit();
    		}
    	});
    	
    	hasDirtyData && me.disableButtons(true);//禁用功能按钮
    	
    	//需要新增的数据
    	for(var i in addArr){
    		addArr[i].dataStatus = ''//状态置空
    		var entity = me.getEntityByRecord(addArr[i]);
    		entity.Id = -1;
    		entity.LabId = 0;
    		var obj = {entity:entity};
    		//创建新增数据回调函数
    		var callback = me.createAddCallback(addArr[i],call);
    		var params = Ext.JSON.encode(obj);
    		//util-POST方式与后台交互
    		postToServer(dictionaryContentsList.addUrl,params,callback);
		}
		//需要修改的数据
		for(var i in editArr){
			editArr[i].dataStatus = ''//状态置空
    		var entity = me.getEntityByRecord(editArr[i]);
    		var obj = {
    			entity:entity,
    			fields:dictionaryContentsList.editfields
    		};
    		//创建修改数据回调函数
    		var callback = me.createEditCallback(editArr[i],call);
    		var params = Ext.JSON.encode(obj);
    		//util-POST方式与后台交互
    		postToServer(dictionaryContentsList.editUrl,params,callback);
		}
    },
    /**
     * 取消按钮事件处理
     * @private
     * @param button
     */
    cancelContents:function(){
    	var me = this;
    	me.disableButtons(true);
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	dictionaryContentsList.store.load();
    },
    /**
     * 删除按钮事件处理
     * @private
     * @param {} record
     */
    delContents:function(grid,rowIndex,colIndex,item,e,record){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var id = record.get(dictionaryContentsList.entityName+'_Id');
    	
    	if(id && id != ''){
    		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
				if(button == "yes"){
					me.disableButtons(true);//禁用功能按钮
					item.disable();//删除按钮置于不可用状态
		    		var callback = function(responseText){
			    		var result = Ext.JSON.decode(responseText);
			    		if(result.success){
			    			grid.store.remove(record);
			    		}else{
			    			Ext.Msg.alert('提示','删除失败！错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
			    		}
			    		me.disableButtons(false);//启用功能按钮
			    		item.enable();//删除按钮置于可用状态
			    	};
			    	//util-GET方式与后台交互
			    	var url = dictionaryContentsList.delUrl + "?id=" + id;
			    	getToServer(url,callback);
				}
			});
    	}else{//不与后台交互，直接前台视图中删除
    		grid.store.remove(record);
    	}
    },
    /**
     * 根据record获取需要的数据对象
     * @private
     * @param {} record
     * @return {}
     */
    getEntityByRecord:function(record){
    	var me = this;
    	var obj = {};
    	var data = record.data;
    	for(i in data){
    		var isFilterField = me.isFilterField(i);
    		if(!isFilterField){//非过滤字段
    			obj[i.split('_').slice(-1)] = data[i];
    		}
    	}
    	return obj;
    },
    //======================字典内容列表快捷键处理====================
    initKeyEvent:function(com){
    	var me = this;
    	new Ext.KeyMap(com.getEl(),[{
	      	key:Ext.EventObject.UP,//方向键-上
	      	fn:function(){me.keyUp(com);}
     	},{
	      	key:Ext.EventObject.DOWN,//方向键-下
	      	fn:function(){me.keyDown(com);}
     	},{
	      	key:Ext.EventObject.LEFT,//方向键-左
	      	fn:function(){me.keyLeft(com);}
     	},{
	      	key:Ext.EventObject.RIGHT,//方向键-右
	      	fn:function(){me.keyRight(com);}
     	}]);
    },
    keyUp:function(com){alert(com.title+'方向键-上');},
    keyDown:function(com){alert(com.title+'方向键-下');},
    keyLeft:function(com){alert(com.title+'方向键-左');},
    keyRight:function(com){alert(com.title+'方向键-右');},
    //=====================内部方法代码=======================
    /**
     * 初始化监听
     * @private
     */
    initListeners:function(){
    	var me = this;
    	//字典类型列表
    	var dictionaryTypeList = me.getComponent('dictionaryTypeList');
    	//字典列表
    	var dictionaryList = me.getComponent('dictionaryList');
    	//字典内容列表
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	
    	dictionaryTypeList.store.on({
			load:function(com,records,successful,eOpts){
				//加载数据完毕后刷新按钮置为可用状态
				dictionaryTypeList.header.getComponent('refresh').enable();
				//加载数据时默认选中第一行
				if(records.length > 0){
					successful && dictionaryTypeList.getSelectionModel().select(0);
				}
			}
		});
		
    	dictionaryList.store.on({
			load:function(com,records,successful,eOpts){
				//加载数据完毕后刷新按钮置为可用状态
				dictionaryList.header.getComponent('refresh').enable();
				//加载数据时默认选中第一行
				if(records.length > 0){
					successful && dictionaryList.getSelectionModel().select(0);
				}
			}
		});
		
    	//选择一行字典类型联动字典列表更新数据
    	dictionaryTypeList.on({
    		select:function(com,record,index,eOpts){
    			var id = record.get('Id');
    			var where = "bdic.BDicClass.Id="+id;
    			dictionaryList.store.proxy.url = me.getDictionaryListUrl + "&where=" + where;
    			dictionaryList.store.load();
    		}
    	});
    	//选择一行字典联动字典内容列表更新数据
    	dictionaryList.on({
    		select:function(com,record,index,eOpts){
    			var entityName = record.get('Code');
    			//确定字典内容列表的主键
    			dictionaryContentsList.entityName = entityName;
    			//根据对象名改变字典内容
    			me.changeDictionaryContentsList(entityName);
    		}
    	});
    },
    /**
     * 创建数据集
     * @private
     * @param {} url 服务地址
     * @param {} fields 数据字段
     * @param {} changeData 数据适配方法，如不传则不处理
     */
    createStore:function(url,fields,changeData){
    	var me = this;
    	var store = Ext.create('Ext.data.Store',{
    		fields:fields || [],
    		proxy:{
    			type:'ajax',
	            url:url,
	            reader:{
	            	type:'json',
	            	totalProperty:'count',
	                root:'list'
	            },
	            extractResponseData:function(response){
	            	if(Ext.typeOf(changeData) == 'function'){
	            		return changeData(response);
	            	}else{
	            		return me.changeStoreData(response);
	            	}
	            }
    		}
    	});
    	return store;
    },
	/**
	 * 该字段是否需要过滤
	 * @private
	 * @param {} field
	 * @return {Boolean}
	 */
	isFilterField:function(field){
		var me = this;
		var filterFields = me.filterFields || [];
		for(var i in filterFields){
			if(filterFields[i] == field.split('_').slice(-1)){
				return true;
			}
		}
		return false;
	},
    /**
     * 获取修改的字段
     * @private
     * @param {} obj
     * @return {}
     */
    getEditFields:function(obj){
    	var me = this;
    	var str = "";
    	var EntityFrameTree = obj.EntityFrameTree;
    	for(var i in EntityFrameTree){
    		var columnName = EntityFrameTree[i].InteractionField;
    		if(!me.isFilterField(columnName)){
	    		str += columnName.split('_').slice(-1) + ",";
    		}
    	}
    	str = str != '' ? str.slice(0,-1) : '';
    	return str;
    },
    /**
     * 禁用功能按钮
     * @private
     * @param {} bo
     */
    disableButtons:function(bo){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var buttonstoolbar = dictionaryContentsList.getComponent('buttonstoolbar');
    	var add = buttonstoolbar.getComponent('add');
    	var save = buttonstoolbar.getComponent('save');
    	var cancel = buttonstoolbar.getComponent('cancel');
    	if(bo){
    		dictionaryContentsList.header.getComponent('refresh').disable();
    		add.disable();
	    	save.disable();
	    	cancel.disable();
    	}else{
    		dictionaryContentsList.header.getComponent('refresh').enable();
    		add.enable();
	    	save.enable();
	    	cancel.enable();
    	}
    },
    /**
     * 创建新增回调函数
     * @private
     * @param {} record
     * @param {} callback
     */
    createAddCallback:function(record,callback){
    	var me = this;
    	var dictionaryContentsList = me.getComponent('dictionaryContentsList');
    	var c = function(responseText){
			var result = Ext.JSON.decode(responseText);
    		if(result.success === true || result.success === 'true'){
    			var data = Ext.JSON.decode(result.ResultDataValue);
    			record.set(dictionaryContentsList.entityName+'_Id',data.id);
    			record.set('dataStatus','<b style="color:green">新增成功</b>');
    			record.commit();
    		}else{
    			record.set('dataStatus','<b style="color:red">新增失败</b>');
    		}
    		if(Ext.typeOf(callback) === 'function'){
    			callback();
    		}
		};
		return c;
    },
    /**
     * 创建修改回调函数
     * @private
     * @param {} record
     * @param {} callback
     * @return {}
     */
    createEditCallback:function(record,callback){
    	var me = this;
    	var c = function(responseText){
			var result = Ext.JSON.decode(responseText);
    		if(result.success === true || result.success === 'true'){
    			record.set('dataStatus','<b style="color:green">修改成功</b>');
    			record.commit();
    		}else{
    			record.set('dataStatus','<b style="color:red">修改失败</b>');
    		}
    		if(Ext.typeOf(callback) === 'function'){
    			callback();
    		}
		};
		return c;
    },
    //=====================公共方法代码=======================
	/**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData:function(response){
		var me = this;
    	var result = Ext.JSON.decode(response.responseText);
		result.count = 0;
		result.list = [];
		if(result.ResultDataValue && result.ResultDataValue != ""){
    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
    		result.count = ResultDataValue['count'];
	    	result.list = ResultDataValue['list'];
    	}
		
		if(!result.success){
    		Ext.Msg.alert('提示','错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
    	}
		
		response.responseText = Ext.JSON.encode(result);
		return response;
  	}
  	//=====================对外公开方法=======================
});