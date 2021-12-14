Ext.ns('Ext.demo');
Ext.define('Ext.demo.ModelDemo',{
	extend:'Ext.panel.Panel',
	alias:'widget.modeldemo',
	width:1000,
	height:500,
	//==========================可变部分============================
	title:'单表操作demo',
	getListUrl:'AAA',//获取数据列表的服务地址
	getInfoByIdUrl:'AAA',//根据ID获取一条数据信息的服务地址
	deleteByIdUrl:'AAA',//根据ID删除一条信息的服务地址
	addUrl:'AAA',//新增一条数据的服务地址
	updateUrl:'AAA',//更新一条数据的服务地址[实体保存]
	updateByFieldsUrl:'AAA',//更新一条数据的服务地址[按字段保存]
	//国家对象所有的字段
	fields:[
		{key:'BCountry_Name',value:'名称'},
		{key:'BCountry_SName',value:'简称'},
		{key:'BCountry_Shortcode',value:'快捷码'},
		{key:'BCountry_PinYinZiTou',value:'汉子拼音字头'},
		{key:'BCountry_Comment',value:'备注'},
		{key:'BCountry_IsUse',value:'是否使用'},
		{key:'BCountry_Id',value:'主键ID'},
		{key:'BCountry_LabID',value:'实验室ID'},
		{key:'BCountry_DataAddTime',value:'数据加入时间'},
		{key:'BCountry_DataTimeStamp',value:'时间戳'}
	],
	objectName:'国家',//对象名称
	key:'BCountry_Id',//主键字段
	//============================================================
	remoteSort:true,//后台排序
	pageSize:10,//列表每页的数量
	initComponent:function(){
		var me = this;
		me.initView();//初始化内部组件面板
		me.initDockedItems();//初始化挂靠
		me.callParent(arguments);
	},
	afterRender:function(){
		var me = this;
		me.ininListeners();//初始化监听
		me.callParent(arguments);
	},
	/**
	 * 初始化内部组件面板
	 * @private
	 */
	initView:function(){
		var me = this;
		me.layout = "border";
		me.store = me.getGridStore();
		var grid = me.createGrid();
		var form = me.createForm();
		
		grid.region = "center";
		form.region = "east";
		form.width = 250;
		form.collapsible = true;
		form.split = true;
		
		me.items = [grid,form];
	},
	/**
	 * 创建列表
	 * @private
	 * @return {}
	 */
	createGrid:function(){
		var me = this;
		var grid = {
			xtype:'grid',
			itemId:'grid',
			title:me.objectName+'列表',
			columns:me.getGridColumns(),
			store:me.store,
			dockedItems:me.getGridDockedItems()
		};
		return grid;
	},
	/**
	 * 创建表单
	 * @private
	 * @return {}
	 */
	createForm:function(){
		var me = this;
		var com = {
			xtype:'form',
			titles:me.objectName+'表单',
			title:me.objectName+'表单',
			itemId:'form',
			type:'show',
			autoScroll:true,
			bodyPadding:5,
			items:me.getFormItems(),
			dockedItems:me.getFormDockedItems(),
			load:function(id){
				var callback = function(info){
					me.getComponent('form').getForm().setValues(info);
				}
				me.getInfoById(id,callback);
			},
			isAdd:function(){
				this.type = "add";
				this.getForm().reset();
				this.getComponent('buttons').show();
				this.setTitle(this.titles+"[新增]");
				this.setReadOnly(false);
				this.getComponent('buttons').getComponent('objSaveBut').hide();
			},
			isEdit:function(id){
				this.type = "edit";
				this.load(id);
				this.getComponent('buttons').show();
				this.setTitle(this.titles+"[修改]");
				this.setReadOnly(false);
				this.getComponent('buttons').getComponent('objSaveBut').show();
			},
			isShow:function(id){
				this.type = "show";
				this.load(id);
				this.getComponent('buttons').hide();
				this.setTitle(this.titles+"[查看]");
				this.setReadOnly(true);
				this.getComponent('buttons').getComponent('objSaveBut').hide();
			},
			setReadOnly:function(bo){
           		var items = this.items.items;
           		for(var i in items){
           			items[i].setReadOnly(bo);
           		}
           	}
		};
		return com;
	},
	/**
	 * 获取数据对象fields
	 * @private
	 * @param {} hasDataTimeStamp
	 * @return {}
	 */
	getFields:function(hasDataTimeStamp){
		var me = this;
		var fields = [];
		for(var i in me.fields){
			if(hasDataTimeStamp || me.fields[i].key.indexOf('DataTimeStamp') == -1){
				fields.push(me.fields[i].key);
			}
		}
		return fields;
	},
	/**
	 * 获取需要保存的数据
	 * @private
	 * @return {}
	 */
	getFormDataObj:function(hasDataTimeStamp){
		var me = this;
		var values = me.getComponent('form').getForm().getValues();
		
		var v = {};//过滤掉时间戳后的数据对象
		for(var i in values){
			var a = i.split("_");
			if(i.indexOf('DataTimeStamp') == -1){
				v[a[a.length-1]] = values[i];
			}else{
				if(hasDataTimeStamp){
					v[a[a.length-1]] = values[i].split(",");
				}
			}
		}
		var fields = me.getFields(hasDataTimeStamp);
		
		var obj = {entity:v,fields:fields.join(',')};
		return obj;
	},
	/**
	 * 获取数据对象列表的列属性
	 * @private
	 * @return {}
	 */
	getGridColumns:function(){
		var me = this;
		var columns = [];
		for(var i in me.fields){
			columns.push({dataIndex:me.fields[i].key,text:me.fields[i].value});
		}
		columns.push({dataIndex:'Mark',text:'',width:60});
		return columns;
	},
	/**
	 * 获取列表的数据集对象
	 * @private
	 * @return {}
	 */
	getGridStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(true),
			remoteSort:me.remoteSort,
			pageSize:me.pageSize ? me.pageSize : 25,
			proxy:{
				type:'ajax',
				url:me.getListUrl+'?isPlanish=true&&fields='+me.getFields(true),
				reader:{
					type:'json',
	            	totalProperty:'count',
	                root:'list'
				},
				extractResponseData:function(response){
			    	var data = Ext.JSON.decode(response.responseText);
			    	if(!data.ResultDataValue || data.ResultDataValue == ""){
			    		data.list = [];
			    		data.count = 0;
			    	}else{
			    		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
				    	data.list = ResultDataValue['list'];
				    	data.count = ResultDataValue['count'];
			    	}
			    	response.responseText = Ext.JSON.encode(data);
			    	return response;
			  	}
			},
			autoLoad:true
		});
		return store;
	},
	/**
	 * 获取列表的挂靠
	 * @private
	 * @return {}
	 */
	getGridDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',
			dock:'top',
			items:[{
				xtype:'button',
				text:'更新',
				handler:function(){
					this.ownerCt.ownerCt.store.load();
				}
			},{
				xtype:'button',
				text:'新增',
				handler:function(){
					var form = me.getComponent('form');
					form.isAdd();
				}
			},{
				xtype:'button',
				text:'修改',
				handler:function(){
					var grid = this.ownerCt.ownerCt;
					var records = grid.getSelectionModel().getSelection();
					if(records.length == 1){
						var form = me.getComponent('form');
						form.isEdit(records[0].get(me.key));
					}else{
						Ext.Msg.alert('提示','请选择一条数据进行操作！');
					}
				}
			},{
				xtype:'button',
				text:'查看',
				handler:function(){
					var grid = this.ownerCt.ownerCt;
					var records = grid.getSelectionModel().getSelection();
					if(records.length == 1){
						var form = me.getComponent('form');
						form.isShow(records[0].get(me.key));
					}else{
						Ext.Msg.alert('提示','请选择一条数据进行操作！');
					}
				}
			},{
				xtype:'button',
				text:'删除',
				handler:function(){
					var grid = this.ownerCt.ownerCt;
					var records = grid.getSelectionModel().getSelection();
					if(records.length > 0){
						for(var i in records){
							var callback = function(bo){
								if(bo){
									records[i].set('Mark','<b style="color:gray;">已删除</b>');
								}else{
									records[i].set('Mark','<b style="color:red;">删除失败</b>');
								}
							}
							me.deleteInfoById(records[i].get(me.key),callback);
						}
					}else{
						Ext.Msg.alert('提示','没有选择数据！');
					}
				}
			}]
		},{
			xtype:'pagingtoolbar',
			store:me.store,
			dock:'bottom',
			displayInfo:true
		}];
		return dockedItems;
	},
	/**
	 * 获取表单内部数据项
	 * @private
	 */
	getFormItems:function(){
		var me = this;
		var items = [];
		for(var i in me.fields){
			var com = {
				xtype:'textfield',
				name:me.fields[i].key,
				fieldLabel:me.fields[i].value,
				labelWidth:60,
				anchor:'100%',
				readOnly:true
			};
			if(me.fields[i].key.indexOf('DataTimeStamp') != -1){
				com.hidden = true;
			}
			items.push(com);
		}
		return items;
	},
	/**
	 * 获取表单的挂靠
	 * @private
	 * @return {}
	 */
	getFormDockedItems:function(){
		var me =this;
		var dockedItems = [{
			xtype:'toolbar',
			itemId:'buttons',
			hidden:true,
			dock:'bottom',
			items:['->',{
				xtype:'button',
				itemId:'objSaveBut',
				text:'实体保存',
				hidden:true,
				handler:function(){
					me.saveObjInfo();
				}
			},{
				xtype:'button',
				text:'保存',
				handler:function(){
					me.saveInfo();
				}
			},{
				xtype:'button',
				text:'重置',
				handler:function(){
					me.resetForm();
				}
			}]
		}];
		return dockedItems;
	},
	/**
	 * 初始化挂靠
	 * @private
	 */
	initDockedItems:function(){
		//不做处理
	},
	//===================动作处理==================
	ininListeners:function(){
		var me = this;
		var grid = me.getComponent('grid');
		var form = me.getComponent('form');
		grid.on({
			itemclick:function(view,record,item,index,e,eOpts){
				var id = record.get(me.key);
				form.load(id);
			}
		});
	},
	/**
	 * 保存表单数据
	 * @private
	 */
	saveInfo:function(){
		var me = this;
		var form = me.getComponent('form');
		if(!form.getForm().isValid()) return;
		var url = "";
		if(form.type == 'add'){
			url = me.addUrl;
		}else if(form.type == 'edit'){
			url = me.updateByFieldsUrl;
		}else{
			Ext.Msg.alert('提示','表单不是新增或修改状态！');
			return;
		}
		
		var params = Ext.JSON.encode(me.getFormDataObj(false));
		var callback = function(){
			me.getComponent('grid').store.load();
		};
		//将表单数据保存到数据库中
		me.saveToServer(url,params,callback);
	},
	/**
	 * 实体保存（修改）
	 * @private
	 */
	saveObjInfo:function(){
		var me = this;
		var form = me.getComponent('form');
		if(!form.getForm().isValid()) return;
		var url = "";
		if(form.type == 'edit'){
			url = me.updateUrl;
		}else{
			Ext.Msg.alert('提示','表单不是修改状态！');
			return;
		}
		
		var params = Ext.JSON.encode({entity:me.getFormDataObj(true).entity});
		var callback = function(){
			me.getComponent('grid').store.load();
		};
		//将表单数据保存到数据库中
		me.saveToServer(url,params,callback);
	},
	/**
	 * 重置表单信息
	 * @private
	 */
	resetForm:function(){
		var me = this;
		var form = me.getComponent('form');
		form.getForm().reset();
	},
	//===================后台处理==================
	/**
	 * 根据ID获取一条信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getInfoById:function(id,callback){
		var me = this;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:me.getInfoByIdUrl+'?isPlanish=true&&id='+id+'&fields='+me.getFields(true).join(","),
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(Ext.typeOf(callback) == "function"){
                    	var info = {};
	                	if(result.ResultDataValue && result.ResultDataValue != ""){
	                		info = Ext.JSON.decode(result.ResultDataValue);
	                	}
                        callback(info);//回调函数
                    }
                }else{
                    Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','删除信息请求失败！');
			}
		});
	},
	/**
	 * 将信息保存到数据库中
	 * @private
	 * @param {} url
	 * @param {} params
	 * @param {} callback
	 */
	saveToServer:function(url,params,callback){
		var me = this;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			params:params,
			method:'POST',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(Ext.typeOf(callback) == "function"){
                        callback();//回调函数
                    }
                }else{
                    Ext.Msg.alert('提示','保存信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','保存信息请求失败！');
			}
		});
	},
	/**
	 * 更具ID删除数据信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	deleteInfoById:function(id,callback){
		var me = this;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:me.deleteByIdUrl+'?id='+id,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(Ext.typeOf(callback) == "function"){
                        callback(true);//回调函数
                    }
                }else{
                    Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                    if(Ext.typeOf(callback) == "function"){
                        callback(false);//回调函数
                    }
                }
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','删除信息请求失败！');
				if(Ext.typeOf(callback) == "function"){
                    callback(false);//回调函数
                }
			}
		});
	}
	
});