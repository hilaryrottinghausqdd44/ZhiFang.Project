/**
 * 模块角色操作列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleRoleOperateList',{
	extend:'Ext.grid.Panel',
	alias: 'widget.moduleroleoperatelist',
	minWidth:350,
	//=====================可配参数======================
	/**
	 * 获取模块信息
	 * @type 
	 */
	getModuleInfoUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACModuleById',
	/**
	 * 获取角色列表服务地址
	 * @type 
	 */
	getRoleListUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL',
	/**
	 * 获取模块操作列表服务地址
	 * @type 
	 */
	getModuleOperateListUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchModuleOperByModuleID',
	/**
	 * 获取选中的角色列表服务地址
	 * @type 
	 */
	getCheckedRoleListUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
	/**
	 * 获取选中的模块操作列表服务地址
	 * @type 
	 */
	getCheckedModuleOperateListUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRoleModuleOperByModuleID',
	/**
	 * 删除模块角色关系服务地址
	 * @type 
	 */
	delModuleRoleUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',
	/**
	 * 删除模块角色操作服务地址
	 * @type 
	 */
	delModuleRoleOperateUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
	/**
	 * 新增模块角色服务地址
	 * @type 
	 */
	addModuleRoleUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
	/**
	 * 新增模块角色操作服务地址
	 * @type 
	 */
	addModuleRoleOperateUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_AddRBACRoleRight',
	/**
	 * 模块ID
	 * @type 
	 */
	moduleId:-1,
	/**
	 * 模块时间戳
	 * @type String
	 */
	moduleDataTimeStamp:'',
	/**
	 * 角色列表
	 * @type 
	 */
	roleList:null,
	/**
	 * 模块操作列表
	 * @type 
	 */
	moduleOperateList:null,
	/**
	 * 选中的角色列表
	 * @type 
	 */
	checkedRoleList:null,
	/**
	 * 选中的模块操作列表
	 * @type 
	 */
	checkedModuleOperateList:null,
	/**
	 * 数据字段
	 * @type 
	 */
	dataFieldObj:{
		Checked:'Checked',
		Id:'Id',
		CName:'CName',
		DataTimeStamp:'DataTimeStamp',
		CheckedId:'CheckedId'
	},
	count:0,
	//=====================视图渲染======================
	/**
	 * 渲染后后执行
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.moduleId && me.moduleId != -1){
			me.changeListByModuleId(me.moduleId);
		}
	},
	/**
	 * 初始化组件
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
		//me.sortableColumns = false;
		me.columnLines = true;//在行上增加分割线
    	me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		me.columns = [];
		//创建挂靠
		me.dockedItems = me.createDockedItems();
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var com = [{
			xtype:'toolbar',
			itemId:'buttonstoolbar',
			dock:'top',
			items:[{
				text:'更新',tooltip:'更新列表数据',
				itemId:'refresh',disabled:true,
				iconCls:'build-button-refresh',handler:function(button){
					me.changeListByModuleId(me.moduleId);
				}
			},{
				text:'保存',tooltip:'保存数据',
				itemId:'save',disabled:true,
				iconCls:'build-button-save',handler:function(button){
					me.save();
				}
			},{
				xtype:'textfield',width:160,
				itemId:'searchText',disabled:true,
	        	emptyText:'角色名称',
	        	listeners:{
				    change:function(){
				    	me.search();
				    }
	        	}
			},{
				xtype:'button',text:'查询',iconCls:'search-img-16 ',
				itemId:'search',disabled:true,
	        	tooltip:'按照角色名称进行模糊查询',
	        	handler:function(button){me.search();}
			}]
		}];
		return com;
	},
	
	//========================事件处理=====================
	/**
	 * 查询
	 * @private
	 */
	search:function(){
		var me = this;
		var toolbar = me.getComponent('buttonstoolbar');
		var value = toolbar.getComponent('searchText').getValue();
		me.store.clearFilter(true);
		if(value && value != ""){
			var str = eval("/.*?" + value + ".*?/");
			me.store.filter('RBACRole_CName',str);
		}else{
			me.store.filter([{filterFn: function(item){return true;}}]);
		}
	},
	/**
	 * 保存信息
	 * @private
	 */
	save:function(){
		var me = this;
		me.disableRefreshButton(true);
		var callback = function(){
			//需要新增的模块角色
			var addModuleRoleList = [];
			//需要删除的模块角色
			var delModuleRoleList = [];
			
			//需要新增的模块角色操作
			var addModuleRoleOperateList = [];
			//需要删除的模块角色操作
			var delModuleRoleOperateList = [];
			
			var items = me.store.data.items;
			for(var i in items){
				var record = items[i];
				var dirty = record.dirty;//是否是脏数据
	    		if(dirty){
	    			//本行数据中改变的数据
	    			var changedObj = record.getChanges();
	    			for(var j in changedObj){
	    				var objName = j.split('_')[0];
	    				if(objName == 'RBACRole'){//角色
	    					if(changedObj[j]){
	    						addModuleRoleList.push({
				    				RBACModule:{
				    					Id:me.moduleId,
				    					DataTimeStamp:me.moduleDataTimeStamp
				    				},
				    				RBACRole:{
				    					Id:record.get('RBACRole' + '_' + me.dataFieldObj.Id),
				    					DataTimeStamp:record.get('RBACRole' + '_' + me.dataFieldObj.DataTimeStamp)
				    				}
				    			});
	    					}else{
	    						delModuleRoleList.push(record.get(objName + '_' + me.dataFieldObj.CheckedId));
	    					}
	    				}else{//角色操作
	    					if(changedObj[j]){
	    						addModuleRoleOperateList.push({
				    				RBACModuleOper:{
				    					Id:record.get(objName + '_' + me.dataFieldObj.Id),
				    					DataTimeStamp:record.get(objName + '_' + me.dataFieldObj.DataTimeStamp)
				    				},
				    				RBACRole:{
				    					Id:record.get('RBACRole' + '_' + me.dataFieldObj.Id),
				    					DataTimeStamp:record.get('RBACRole' + '_' + me.dataFieldObj.DataTimeStamp)
				    				}
				    			});
	    					}else{
	    						delModuleRoleOperateList.push(record.get(objName + '_' + me.dataFieldObj.CheckedId));
	    					}
	    				}
	    			}
	    		}
			}
			me.addModuleRole(addModuleRoleList);
			me.addModuleRoleOperate(addModuleRoleOperateList);
			me.delModuleRole(delModuleRoleList);
			me.delModuleRoleOperate(delModuleRoleOperateList);
		};
		me.getModuleInfo(callback);
	},
	/**
	 * 勾选处理
	 * @private
	 * @param {} c
	 * @param {} rowIndex
	 * @param {} checked
	 * @param {} eOpts
	 */
	checkedChange:function(c,rowIndex,checked,eOpts){
		var me = this;
		
		var objName = c.dataIndex.split('_')[0];
		var record = me.store.getAt(rowIndex);
		if(record){
			var data = record.data;
			if(objName == 'RBACRole'){
				if(!checked){//取消
					var list = [];
					for(var i in data){
						if(i.split('_').slice(-1) == me.dataFieldObj.Checked && i.split('_')[0] != 'RBACRole'){
							list.push(i);
						}
					}
					for(var i in list){
						record.set(list[i],false);
					}
				}
			}else{
				var bo = false;
				for(var i in data){
					if(i.split('_').slice(-1) == me.dataFieldObj.Checked && i.split('_')[0] != 'RBACRole'){
						if(data[i] == true){bo = true;}
					}
				}
				bo && record.set('RBACRole' + '_' + me.dataFieldObj.Checked,true);
			}
		}
		
	},
	//==========================内容变更===========================
	/**
	 * 创建角色操作列表数据列
	 * @private
	 * @return {}
	 */
	createListColumns:function(list){
		var me = this;
		var columns = [];
		//角色列
		var roleColumn = me.createColumn('角色','RBACRole');
		columns.push(roleColumn);
		
		for(var i in list){
			var text = "";
			var objName = "";
			var data = list[i];
			for(var i in data){
				if(i.split('_').slice(-1) == 'CName'){
					text = data[i];
				}
				if(i.split('_').slice(-1) == me.dataFieldObj.Id){
					objName = data[i];
				}
			}
			var column = me.createColumn(text,objName);
			columns.push(column);
		}
		return columns;
	},
	/**
	 * 根据抬头和列名创建数据列
	 * @private
	 * @param {} header
	 * @param {} objName
	 * @return {}
	 */
	createColumn:function(header,objName){
		var me = this;
		var dataIndex = objName + "_Checked";
		var column = {
			xtype:'checkcolumn',
			width:150,
			text:header || '',
			dataIndex:dataIndex,
			editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },
            renderer:function(value,metaData,record){
            	var text = record.get(objName + '_CName');//显示名称
		        var cssPrefix = Ext.baseCSSPrefix,
		        	cls = [cssPrefix + 'grid-checkheader'];
		        if(value){cls.push(cssPrefix + 'grid-checkheader-checked');}
		        return '<div class="' + cls.join(' ') + '" style="background-position:0px;text-indent:14px;">&#160;' + text + '</div>';
		    },
		    listeners:{
		    	checkchange:function(c,rowIndex,checked,eOpts){
		    		me.checkedChange(c,rowIndex,checked,eOpts);
		    	}
		    }
		};
		return column;
	},
	/**
	 * 改变视图内容
	 * @private
	 */
	changeListView:function(){
		var me = this;
    	var store = [];
    	var columns = [];
    	
    	columns = me.createListColumns(me.moduleOperateList);
    	
    	var fields = me.getFields();
    	var data = me.getData();
    	
    	var store = Ext.create('Ext.data.Store',{
			fields:fields,
			data:data
		});
    	
    	//更改列表数据集
    	me.reconfigure(store,columns);
    	//启用刷新按钮
		me.disableRefreshButton(false);
    	
    	//获取选中的角色列表
		me.getCheckedRoleList();
		//获取选中的模块操作
		me.getCheckedModuleOperateList();
	},
	/**
	 * 默认还原勾选
	 * @private
	 */
	checkedComponent:function(){
		var me = this;
		var checkedRoleList = me.checkedRoleList;
		var checkedModuleOperateList = me.checkedModuleOperateList;
		
		for(var i in checkedRoleList){
			var rmId = checkedRoleList[i]['RBACRoleModule_Id'];
			var rId = checkedRoleList[i]['RBACRoleModule_RBACRole_Id'];
			var record = me.store.findRecord('RBACRole' + '_' + me.dataFieldObj.Id,rId);
			if(record){
				record.set('RBACRole' + '_' + me.dataFieldObj.Checked,true);
				record.set('RBACRole' + '_' + me.dataFieldObj.CheckedId,rmId);
			}
		}
		for(var i in checkedModuleOperateList){
			var roleId = checkedModuleOperateList[i]['roleId'];//角色ID
			var operateList = checkedModuleOperateList[i]['operateList'];
			for(var j in operateList){
				var rightID = operateList[j]['rightID'];//关系ID
				var moduleOperID = operateList[j]['moduleOperID'];//模块操作ID
				
				var record = me.store.findRecord('RBACRole' + '_' + me.dataFieldObj.Id,roleId);
				if(record){
					record.set(moduleOperID + '_' + me.dataFieldObj.Checked,true);
					record.set(moduleOperID + '_' + me.dataFieldObj.CheckedId,rightID);
				}
			}
		}
		//更新视图中的脏数据
		me.commitDirtyData();
	},
	/**
	 * 获取数据fields
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me = this;
			
		var fields = [
			'RBACRole' + '_' + me.dataFieldObj.Checked,
			'RBACRole' + '_' + me.dataFieldObj.Id,
			'RBACRole' + '_' + me.dataFieldObj.CName,
			'RBACRole' + '_' + me.dataFieldObj.DataTimeStamp,
			'RBACRole' + '_' + me.dataFieldObj.CheckedId
		];
		
		if(me.moduleOperateList && me.moduleOperateList.length > 0){
			for(var i in me.moduleOperateList){
				var obj = me.moduleOperateList[i];
				var GUID = "";
				for(var j in obj){
					var value = obj[j];
					var arr = j.split('_');
					var last = arr.slice(-1);
					if(last == me.dataFieldObj.Id){
						GUID = value;
					}
				}
				var field = [
					GUID + '_' + me.dataFieldObj.Checked,
					GUID + '_' + me.dataFieldObj.Id,
					GUID + '_' + me.dataFieldObj.CName,
					GUID + '_' + me.dataFieldObj.DataTimeStamp,
					GUID + '_' + me.dataFieldObj.CheckedId
				];
				fields = fields.concat(field);
			}
		}
		
		return fields;
	},
	/**
	 * 获取数据
	 * @private
	 * @return {}
	 */
	getData:function(){
		var me = this;
		var data = [];
		//角色
		for(var i in me.roleList){
			var obj = {};
			obj['RBACRole' + '_' + me.dataFieldObj.Checked] = false,
			obj['RBACRole' + '_' + me.dataFieldObj.Id] = me.roleList[i]['RBACRole' + '_' + me.dataFieldObj.Id],
			obj['RBACRole' + '_' + me.dataFieldObj.CName] = me.roleList[i]['RBACRole' + '_' + me.dataFieldObj.CName],
			obj['RBACRole' + '_' + me.dataFieldObj.DataTimeStamp] = me.roleList[i]['RBACRole' + '_' + me.dataFieldObj.DataTimeStamp],
			obj['RBACRole' + '_' + me.dataFieldObj.CheckedId] = '';
			
			var operateData = me.getOperateData();
			for(var i in operateData){
				obj[i] = operateData[i];
			}
			
			data.push(obj);
		}
		//模块操作
		
		return data;
	},
	/**
	 * 获取操作数据
	 * @private
	 * @return {}
	 */
	getOperateData:function(){
		var me = this;
		var data = {};
		
		if(me.moduleOperateList && me.moduleOperateList.length > 0){
			for(var i in me.moduleOperateList){
				var obj = me.moduleOperateList[i];
				var objName = "";
				var GUID = "";
				for(var j in obj){
					var value = obj[j];
					var arr = j.split('_');
					var last = arr.slice(-1);
					if(last == me.dataFieldObj.Id){
						objName = arr[0];
						GUID = value;
					}
				}
				
				data[GUID + '_' + me.dataFieldObj.Checked] = false,
				data[GUID + '_' + me.dataFieldObj.Id] = obj[objName + '_' + me.dataFieldObj.Id],
				data[GUID + '_' + me.dataFieldObj.CName] = obj[objName + '_' + me.dataFieldObj.CName],
				data[GUID + '_' + me.dataFieldObj.DataTimeStamp] = obj[objName + '_' + me.dataFieldObj.DataTimeStamp],
				data[GUID + '_' + me.dataFieldObj.CheckedId] = '';
			}
		}
		
		return data;
	},
	/**
	 * 是否禁用刷新按钮
	 * @private
	 * @param {} bo
	 */
	disableRefreshButton:function(bo){
		var me = this,
			buttonstoolbar = me.getComponent('buttonstoolbar'),
			refresh = buttonstoolbar.getComponent('refresh'),
			save = buttonstoolbar.getComponent('save'),
			searchText = buttonstoolbar.getComponent('searchText'),
			search = buttonstoolbar.getComponent('search');
		if(bo){
			refresh.disable();
			save.disable();
			searchText.disable();
			search.disable();
		}else{
			refresh.enable();
			save.enable();
			searchText.enable();
			search.enable();
		}
	},
	/**
	 * 对象中时间戳处理
	 * @private
	 * @param {} obj
	 * @return {}
	 */
	changeData:function(obj){
		var me  =this;
		var o = obj;
		var c = function(ob){
			for(var i in ob){
				if(Ext.typeOf(ob[i]) == 'object'){
					c(ob[i]);
				}else{
					if(i.split('_').slice(-1) == me.dataFieldObj.DataTimeStamp){
						ob[i] = ob[i].split(',');
					}
				}
			}
		};
		c(o);
		return o;
	},
	/**
	 * 更新视图中的数据
	 * @private
	 */
	commitData:function(){		
		var me = this;
		me.count++;
		if(me.count == 4){
			me.commitDirtyData();
			me.count = 0;
		}
	},
	/**
	 * 更新视图中的脏数据
	 * @private
	 */
	commitDirtyData:function(){
		var me = this;
		var items = me.store.data.items;
		for(var i in items){
			var record = items[i];
			var dirty = record.dirty;//是否是脏数据
			if(dirty){
				record.commit();
			}
		}
		me.disableRefreshButton(false);
	},
	//==========================与后台交互===========================
	/**
	 * 获取角色信息列表
	 * @private
	 */
	getRoleList:function(){
		var me = this;
		var url = me.getRoleListUrl + "?page=1&limit=1000&isPlanish=true&fields=RBACRole_Id,RBACRole_CName,RBACRole_DataTimeStamp";
		var errorText = '获取角色列表失败！';
		
		var callback = function(list){
			me.roleList = list;
			if(me.moduleOperateList != null){
				me.changeListView();
			}
		};
		
		me.roleList = null;
		me.getList(url,callback,errorText);
	},
	/**
	 * 根据模块ID获取模块操作列表
	 * @private
	 */
	getModuleOperateList:function(){
		var me = this;
		var url = me.getModuleOperateListUrl + "?id=" + me.moduleId + "&page=1&limit=1000&isPlanish=true&fields=RBACModuleOper_Id,RBACModuleOper_CName,RBACModuleOper_DataTimeStamp";
		var errorText = '获取模块操作列表失败！';
		
		var callback = function(list){
			me.moduleOperateList = list;
			if(me.roleList != null){
				me.changeListView();
			}
		};
		
		me.moduleOperateList = null;
		me.getList(url,callback,errorText);
	},
	/**
	 * 根据模块ID获取选中的角色列表
	 * @private
	 */
	getCheckedRoleList:function(){
		var me = this;
		var where = "rbacrolemodule.RBACModule.Id=" + me.moduleId;
		var url = me.getCheckedRoleListUrl + "?&page=1&limit=1000&isPlanish=true&fields=RBACRoleModule_Id,RBACRoleModule_RBACRole_Id&where=" + where;
		var errorText = '获取选中角色列表失败！';
		
		var callback = function(list){
			me.checkedRoleList = list;
			if(me.checkedModuleOperateList != null){
				me.checkedComponent();
			}
		};
		
		me.checkedRoleList = null;
		me.getList(url,callback,errorText);
	},
	/**
	 * 根据模块ID获取选中的模块操作列表
	 * @private
	 */
	getCheckedModuleOperateList:function(){
		var me = this;
		var url = me.getCheckedModuleOperateListUrl + "?id=" + me.moduleId;
		var errorText = '获取选中角色操作列表失败！';
		
		var callback = function(list){
			me.checkedModuleOperateList = list;
			if(me.checkedRoleList != null){
				me.checkedComponent();
			}
		};
		
		me.checkedModuleOperateList = null;
		//me.getList(url,callback,errorText);
		
		var c = function(text){
			var result = Ext.JSON.decode(text);
			var list = [];
			if(result.success){
				if(result.ResultDataValue && result.ResultDataValue != ""){
		    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
			    	list = ResultDataValue;
		    	}
		    	if(Ext.typeOf(callback) === 'function'){
		    		callback(list);
		    	}
			}else{
				alertError((errorText||'')+'错误信息:'+result.ErrorInfo);
			}
		};
		//util-GET方式与后台交互
		getToServer(url,c);
	},
	/**
	 * 获取数据
	 * @private
	 * @param {} url
	 * @param {} callback
	 * @param {} errorText
	 */
	getList:function(url,callback,errorText){
		var me = this;
		var c = function(text){
			var result = Ext.JSON.decode(text);
			var list = [];
			if(result.success){
				if(result.ResultDataValue && result.ResultDataValue != ""){
		    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
			    	list = ResultDataValue['list'];
		    	}
		    	if(Ext.typeOf(callback) === 'function'){
		    		callback(list);
		    	}
			}else{
				alertError((errorText||'')+'错误信息:'+result.ErrorInfo);
			}
		};
		//util-GET方式与后台交互
		getToServer(url,c);
	},
	/**
	 * 获取模块信息
	 * @private
	 * @param {} callback
	 */
	getModuleInfo:function(callback){
		var me = this;
		var url = me.getModuleInfoUrl + "?id=" + me.moduleId + "&isPlanish=true&fields=RBACModule_Id,RBACModule_DataTimeStamp";
		var c = function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				if(result.ResultDataValue && result.ResultDataValue != ""){
		    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
			    	me.moduleDataTimeStamp = ResultDataValue['RBACModule_DataTimeStamp'];
		    	}
		    	if(Ext.typeOf(callback) === 'function'){
		    		callback();
		    	}
			}else{
				alertError('获取模块信息失败！错误信息:'+result.ErrorInfo);
			}
		};
		//util-GET方式与后台交互
		getToServer(url,c);
	},
	/**
	 * 新增模块角色
	 * @private
	 * @param {} list
	 */
	addModuleRole:function(list){
		var me = this,
			url = me.addModuleRoleUrl,
			count = 0;
		if(list.length == 0){
			me.commitData();
		}else{
			for(var i in list){
				var obj = me.changeData(list[i]);
				var callback = function(){
					count++;
					if(count == list.length){
						me.commitData();
					}
				};
				obj = {entity:obj};
				var params = Ext.JSON.encode(obj);
				//util-POST方式与后台交互
				postToServer(url,params,callback);
			}
		}
	},
	/**
	 * 新增模块角色操作
	 * @private
	 * @param {} list
	 */
	addModuleRoleOperate:function(list){
		var me = this,
			url = me.addModuleRoleOperateUrl,
			count = 0;
		if(list.length == 0){
			me.commitData();
		}else{
			for(var i in list){
				var obj = me.changeData(list[i]);
				var callback = function(){
					count++;
					if(count == list.length){
						me.commitData();
					}
				};
				obj = {entity:obj};
				var params = Ext.JSON.encode(obj);
				//util-POST方式与后台交互
				postToServer(url,params,callback);
			}
		}
	},
	/**
	 * 删除模块角色
	 * @private
	 * @param {} ids
	 */
	delModuleRole:function(ids){
		var me = this,
			url = me.delModuleRoleUrl,
			count = 0;
		if(ids.length == 0){
			me.commitData();
		}else{
			for(var i in ids){
				var callback = function(){
					count++;
					if(count == ids.length){
						me.commitData();
					}
				};
				//util-GET方式与后台交互
				getToServer(url+'?id='+ids[i],callback);
			}
		}
	},
	/**
	 * 删除模块角色操作
	 * @private
	 * @param {} ids
	 */
	delModuleRoleOperate:function(ids){
		var me = this,
			url = me.delModuleRoleOperateUrl,
			count = 0;
		if(ids.length == 0){
			me.commitData();
		}else{
			for(var i in ids){
				var callback = function(){
					count++;
					if(count == ids.length){
						me.commitData();
					}
				};
				//util-GET方式与后台交互
				getToServer(url+'?id='+ids[i],callback);
			}
		}
	},
	//========================公开方法=====================
	/**
	 * 根据模块ID改变列表内容
	 * @public
	 * @param {} id
	 */
	changeListByModuleId:function(id){
		var me = this;
		//禁用刷新按钮
		me.disableRefreshButton(true);
		//模块ID
		me.moduleId = id;
		//获取角色列表
		me.getRoleList();
		//获取模块操作
		me.getModuleOperateList();
	}
});