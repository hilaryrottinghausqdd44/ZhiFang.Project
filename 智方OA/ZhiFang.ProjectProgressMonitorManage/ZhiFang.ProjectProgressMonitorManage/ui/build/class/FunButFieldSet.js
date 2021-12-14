/**
 * 功能栏、操作列按钮组设置
 * 【可配参数】
 * title 标题
 * 【对外方法】
 * setDelServerStore 更换删除服务数据集
 * setWinformStore 更换交互字段数据集
 * getToolbarButtons 获取选中的功能栏按钮组
 * getActioncolumnButtons 获取选中的操作列按钮组
 * getFieldSetValues 获取功能按钮配置数据
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.FunButFieldSet',{
	extend:'Ext.form.FieldSet',
	alias:'widget.funbutfieldset',
	//=====================可配参数=======================
	/**
	 * 应用ID
	 * @type 
	 */
	appId:-1,
	/**
	 * 标题显示值
	 * @type String
	 */
	title:'功能按钮配置',
	/**
	 * 删除服务列表显示属性
	 * @type String
	 */
	delServerDisplayField:'text',
	/**
	 * 删除服务列表值属性
	 * @type String
	 */
	delServerValueField:'value',
	/**
	 * 删除服务列表地址
	 * @type String
	 */
	delServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
	/**
	 * 删除服务列表字段数组
	 * @type String
	 */
	delServerFields:['CName','ServerUrl'],
	/**
	 * 交互字段显示属性
	 * @type String
	 */
	keyDisplayField:'text',
	/**
	 * 交互字段值属性
	 * @type String
	 */
	keyValueField:'value',
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId',
	//=====================内部视图渲染=======================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
	},
	/**
	 * 初始化参数
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//内部按钮组
		me.items = me.createItems();
		//注册事件
		me.initEvents();
		
		me.callParent(arguments);
	},
	/**
	 * 注册事件
	 * @private
	 */
	initEvents:function(){
		var me = this;
		//me.addEvents('browseclick');//点击浏览按钮
	},
	/**
	 * 创建内部按钮组
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//功能栏按钮组设置
		var toolbarConfig = me.createToolbarConfig();
		//操作列按钮组设置
		var actioncolumnConfig = me.createActioncolumnConfig();
		//弹出表单设置
		var winformConfig = me.createWinformConfig();
		//组合
		var items = toolbarConfig.concat(actioncolumnConfig,winformConfig);
		return items;
	},
	/**
	 * 功能栏按钮组设置
	 * @private
	 * @return {}
	 */
	createToolbarConfig:function(){
		var Width = 120;
		var max = 10;
		var EmptyText = "填写显示文字";
		
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-config',padding:0,margin:0,
			items:[{
				xtype:'label',text:'功能栏按钮组',width:100,style:'fontWeight:bold;',
				itemId:'toolbar-all',name:'toolbar-all'
			},{
				xtype:'radiogroup',itemId:'toolbar-position',
                columns:2,vertical:true,width:100,
                items:[{
                	boxLabel:'顶部',name:'toolbar-position',inputValue:'top',checked:true
                },{
                	boxLabel:'底部',name:'toolbar-position',inputValue:'bottom'
                }]
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-refresh',
			items:[{
				xtype:'checkbox',boxLabel:'更新',width:45,
				itemId:'toolbar-refresh-checkbox',name:'toolbar-refresh-checkbox'
			},{
				xtype:'textfield',value:'更新',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-refresh-text',name:'toolbar-refresh-text'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-refresh-number',name:'toolbar-refresh-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-add',
			items:[{
				xtype:'checkbox',boxLabel:'新增',width:45,
				itemId:'toolbar-add-checkbox',name:'toolbar-add-checkbox'
			},{
				xtype:'textfield',value:'新增',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-add-text',name:'toolbar-add-text'
			},{
				xtype:'numberfield',value:2,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-add-number',name:'toolbar-add-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-edit',
			items:[{
				xtype:'checkbox',boxLabel:'修改',width:45,
				itemId:'toolbar-edit-checkbox',name:'toolbar-edit-checkbox'
			},{
				xtype:'textfield',value:'修改',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-edit-text',name:'toolbar-edit-text'
			},{
				xtype:'numberfield',value:3,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-edit-number',name:'toolbar-edit-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-show',
			items:[{
				xtype:'checkbox',boxLabel:'查看',width:45,
				itemId:'toolbar-show-checkbox',name:'toolbar-show-checkbox'
			},{
				xtype:'textfield',value:'查看',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-show-text',name:'toolbar-show-text'
			},{
				xtype:'numberfield',value:4,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-show-number',name:'toolbar-show-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-del',
			items:[{
				xtype:'checkbox',boxLabel:'删除',width:45,
				itemId:'toolbar-del-checkbox',name:'toolbar-del-checkbox'
			},{
				xtype:'textfield',value:'删除',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-del-text',name:'toolbar-del-text'
			},{
				xtype:'numberfield',value:5,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-del-number',name:'toolbar-del-number'
			}]
		}];
		//5个自定义按钮
		var defalut = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-default1',
			items:[{
				xtype:'checkbox',boxLabel:'自一',width:45,
				itemId:'toolbar-default1-checkbox',name:'toolbar-default1-checkbox'
			},{
				xtype:'textfield',value:'自定义一',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-default1-text',name:'toolbar-default1-text'
			},{
				xtype:'numberfield',value:6,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-default1-number',name:'toolbar-default1-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-default2',
			items:[{
				xtype:'checkbox',boxLabel:'自二',width:45,
				itemId:'toolbar-default2-checkbox',name:'toolbar-default2-checkbox'
			},{
				xtype:'textfield',value:'自定义二',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-default2-text',name:'toolbar-default2-text'
			},{
				xtype:'numberfield',value:7,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-default2-number',name:'toolbar-default2-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-default3',
			items:[{
				xtype:'checkbox',boxLabel:'自三',width:45,
				itemId:'toolbar-default3-checkbox',name:'toolbar-default3-checkbox'
			},{
				xtype:'textfield',value:'自定义三',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-default3-text',name:'toolbar-default3-text'
			},{
				xtype:'numberfield',value:8,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-default3-number',name:'toolbar-default3-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-default4',
			items:[{
				xtype:'checkbox',boxLabel:'自四',width:45,
				itemId:'toolbar-default4-checkbox',name:'toolbar-default4-checkbox'
			},{
				xtype:'textfield',value:'自定义四',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-default4-text',name:'toolbar-default4-text'
			},{
				xtype:'numberfield',value:9,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-default4-number',name:'toolbar-default4-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-default5',
			items:[{
				xtype:'checkbox',boxLabel:'自五',width:45,
				itemId:'toolbar-default5-checkbox',name:'toolbar-default5-checkbox'
			},{
				xtype:'textfield',value:'自定义五',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-default5-text',name:'toolbar-default5-text'
			},{
				xtype:'numberfield',value:10,width:40,minValue:1,maxValue:max,height:23,
				itemId:'toolbar-default5-number',name:'toolbar-default5-number'
			}]
		}];
		
		items = items.concat(defalut);
		
		return items;
	},
	/**
	 * 操作列按钮组设置
	 * @private
	 * @return {}
	 */
	createActioncolumnConfig:function(){
		var me = this;
		var max = 8;
		var Width = 120;
		var EmptyText = "填写提示信息";
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-config',
			items:[{
				xtype:'label',text:'操作列按钮组',width:100,style:'fontWeight:bold;',
				itemId:'actioncolumn-all',name:'actioncolumn-all'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-edit',
			items:[{
				xtype:'checkbox',boxLabel:'修改',width:45,
				itemId:'actioncolumn-edit-checkbox',name:'actioncolumn-edit-checkbox'
			},{
				xtype:'textfield',value:'修改',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-edit-text',name:'actioncolumn-edit-text'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-edit-number',name:'actioncolumn-edit-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-show',
			items:[{
				xtype:'checkbox',boxLabel:'查看',width:45,
				itemId:'actioncolumn-show-checkbox',name:'actioncolumn-show-checkbox'
			},{
				xtype:'textfield',value:'查看',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-show-text',name:'actioncolumn-show-text'
			},{
				xtype:'numberfield',value:2,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-show-number',name:'actioncolumn-show-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-del',
			items:[{
				xtype:'checkbox',boxLabel:'删除',width:45,
				itemId:'actioncolumn-del-checkbox',name:'actioncolumn-del-checkbox'
			},{
				xtype:'textfield',value:'删除',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-del-text',name:'actioncolumn-del-text'
			},{
				xtype:'numberfield',value:3,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-del-number',name:'actioncolumn-del-number'
			}]
		}];
		
		//5个自定义按钮
		var defalut = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-default1',
			items:[{
				xtype:'checkbox',boxLabel:'自一',width:45,
				itemId:'actioncolumn-default1-checkbox',name:'actioncolumn-default1-checkbox'
			},{
				xtype:'textfield',value:'自定义一',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-default1-text',name:'actioncolumn-default1-text'
			},{
				xtype:'numberfield',value:4,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-default1-number',name:'actioncolumn-default1-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-default2',
			items:[{
				xtype:'checkbox',boxLabel:'自一',width:45,
				itemId:'actioncolumn-default2-checkbox',name:'actioncolumn-default2-checkbox'
			},{
				xtype:'textfield',value:'自定义二',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-default2-text',name:'actioncolumn-default2-text'
			},{
				xtype:'numberfield',value:5,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-default2-number',name:'actioncolumn-default2-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-default3',
			items:[{
				xtype:'checkbox',boxLabel:'自三',width:45,
				itemId:'actioncolumn-default3-checkbox',name:'actioncolumn-default3-checkbox'
			},{
				xtype:'textfield',value:'自定义三',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-default3-text',name:'actioncolumn-default3-text'
			},{
				xtype:'numberfield',value:6,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-default3-number',name:'actioncolumn-default3-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-default4',
			items:[{
				xtype:'checkbox',boxLabel:'自四',width:45,
				itemId:'actioncolumn-default4-checkbox',name:'actioncolumn-default4-checkbox'
			},{
				xtype:'textfield',value:'自定义四',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-default4-text',name:'actioncolumn-default4-text'
			},{
				xtype:'numberfield',value:7,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-default4-number',name:'actioncolumn-default4-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-default5',
			items:[{
				xtype:'checkbox',boxLabel:'自五',width:45,
				itemId:'actioncolumn-default5-checkbox',name:'actioncolumn-default5-checkbox'
			},{
				xtype:'textfield',value:'自定义五',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'actioncolumn-default5-text',name:'actioncolumn-default5-text'
			},{
				xtype:'numberfield',value:8,width:40,minValue:1,maxValue:max,height:23,
				itemId:'actioncolumn-default5-number',name:'actioncolumn-default5-number'
			}]
		}];	
		
		//删除服务配置
		var items2 = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'del-server-label',
			items:[{
				xtype:'label',text:'选择删除数据服务',style:'fontWeight:bold;',
				itemId:'del-server-text',name:'del-server-text'
			}]
		},{
			xtype:'combobox',width:224,
			emptyText:'请选择删除数据服务',
			editable:true,typeAhead:true,forceSelection:true,
        	queryMode:'local',
			displayField:me.delServerDisplayField,
			valueField:me.delServerValueField,
			store:Ext.create('Ext.data.Store',{
				fields:me.delServerFields,
				proxy:{
			    	type:'ajax',
			    	url:me.delServerUrl+"?EntityName=Bool",
			    	reader:{type:'json',root:'ResultDataValue'},
			    	extractResponseData:me.changeStoreData
				},autoLoad:true
			}),
			listeners:{
				beforequery:function(e){
                	var combo = e.combo;
	                if(!e.forceAll){
	                	var value = e.query;
	                	combo.store.filterBy(function(record,id){
	                		var text = record.get(combo.displayField);
	                		return (text.indexOf(value)!=-1);
	                	});
	                	combo.expand();
	                	return false;
	                }
                }
			},
			itemId:'del-server-combobox',name:'del-server-combobox'
		}];
		
		items = items.concat(defalut,items2);
		
		return items;
	},
	/**
	 * 弹出的表单设置
	 * @private
	 * @return {}
	 */
	createWinformConfig:function(){
		var me = this;
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'winform-config',
			items:[{
				xtype:'checkbox',boxLabel:'弹出表单',width:100,style:'fontWeight:bold;',
				itemId:'winform-checkbox',name:'winform-checkbox'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'winform-app',
			items:[{
				xtype:'textfield',emptyText:'选择一个弹出表单',width:185,readOnly:true,
				itemId:'winform-text',name:'winform-text'
			},{
				xtype:'textfield',emptyText:'弹出表单的ID',width:185,hidden:true,
				itemId:'winform-id',name:'winform-id'
			},{
				xtype:'button',iconCls:'build-button-configuration-blue',
				tooltip:'选择表单',margin:'0 0 0 2',
				itemId:'winform-button',name:'winform-button'
			}]
		},{
			xtype:'combobox',width:224,
			fieldLabel:'交互字段',labelWidth:55,
			editable:true,typeAhead:true,forceSelection:true,
        	queryMode:'local',
			displayField:me.keyDisplayField,
			valueField:me.keyValueField,
			itemId:'winform-combobox',name:'winform-combobox'
		}];
		return items;
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		//功能栏按钮组-删除
		var toolbardel = me.getComponent('toolbar-del').getComponent('toolbar-del-checkbox');
		toolbardel.on({
			change:function(field,newValue){
				me.showDelServerCom();
			}
		});
		
		//操作列按钮组-删除
		var actioncolumndel = me.getComponent('actioncolumn-del').getComponent('actioncolumn-del-checkbox');
		actioncolumndel.on({
			change:function(field,newValue){
				me.showDelServerCom();
			}
		});
		
		//弹出表单设置
		var winformcheckbox = me.getComponent('winform-config').getComponent('winform-checkbox');
		winformcheckbox.on({
			change:function(field,newValue){
				var winformtext = me.getComponent('winform-app').getComponent('winform-text');
				var winformbutton = me.getComponent('winform-app').getComponent('winform-button');
				if(newValue){
					winformtext.show();
					winformbutton.show();
				}else{
					winformtext.hide();
					winformbutton.hide();
				}
			}
		});
		//弹出表单
		var winformbutton = me.getComponent('winform-app').getComponent('winform-button');
		winformbutton.on({
			click:function(){
				me.openAppListWin();
			}
		});
	},
	/**
	 * 打开应用列表窗口
	 * @private
	 */
	openAppListWin:function(){
		var me = this;
		var appList = Ext.create('Ext.build.AppListPanel',{
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:500,
			height:300,
			getAppListServerUrl:me.getAppListServerUrl,
			filterId:me.appId,
			defaultLoad:true,
			readOnly:true,
			pageSize:9//每页数量
    	}).show();
    	appList.on({
    		okClick:function(){
    			var records = appList.getSelectionModel().getSelection();
    			if(records.length == 0){
    				alertError("请选择一个应用！");
    			}else if(records.length == 1){
    				me.setWinformInfo(records[0]);
    				appList.close();//关闭应用列表窗口
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
		    	me.setWinformInfo(record);
    			appList.close();//关闭应用列表窗口
    		}
    	});
	},
	/**
	 * 设置弹出表单的属性
	 * @private
	 * @param {} record
	 */
	setWinformInfo:function(record){
		var me = this;
		var winformtext = me.getComponent('winform-app').getComponent('winform-text');
		var winformid = me.getComponent('winform-app').getComponent('winform-id');
		winformtext.setValue(record.get('BTDAppComponents_CName'));
		winformid.setValue(record.get('BTDAppComponents_Id'));
	},
	/**
	 * 是否需要开启删除服务选中下拉框
	 * @private
	 * @return {}
	 */
	showDelServerCom:function(){
		var me = this;
		var values = me.getFieldSetValues();
		var toolbardel = values['toolbar-del-checkbox'];
		var actioncolumndel = values['actioncolumn-del-checkbox'];
		var bo = (toolbardel || actioncolumndel);
		
		var delserverlabel = me.getComponent('del-server-label');
		var delservercombobox = me.getComponent('del-server-combobox');
	},
	/**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData: function(response){
    	var data = Ext.JSON.decode(response.responseText);
    	var ResultDataValue = [];
    	if(data.ResultDataValue && data.ResultDataValue != ""){
    		ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	}
    	data.ResultDataValue = ResultDataValue;
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	},
	/**
	 * 功能栏按钮组是否全部选中
	 * @private
	 * @param checked true(全部选中)；false(全部未选中)
	 * @return {}
	 */
	isToolbarAllChecked:function(checked){
		var me = this;
		var refresh = me.getComponent('toolbar-refresh').getComponent('toolbar-refresh-checkbox').value;
		var add = me.getComponent('toolbar-add').getComponent('toolbar-add-checkbox').value;
		var edit = me.getComponent('toolbar-edit').getComponent('toolbar-edit-checkbox').value;
		var show = me.getComponent('toolbar-show').getComponent('toolbar-show-checkbox').value;
		var del = me.getComponent('toolbar-del').getComponent('toolbar-del-checkbox').value;
        
		return checked ? (refresh && add && edit && show && del) : !(refresh || add || edit || show || del);
	},
  	//=====================对外公开方法=======================
	/**
	 * 更换删除服务数据集
	 * @public
	 * @param {} store
	 */
	setDelServerStore:function(store){
		var me = this;
		var com = me.getComponent('del-server-combobox');
		com.bindStore(store);
		//获取下拉框的第一条记录   
        //var record = store.getAt(0);  
        //把获取到第一条记录设置给下拉框   
        //com.setValue(record);
	},
	/**
	 * 更换交互字段数据集
	 * @public
	 * @param {} store
	 */
	setWinformStore:function(store){
		var me = this;
		var com = me.getComponent('winform-combobox');
		com.bindStore(store);
		//获取下拉框的第一条记录   
        //var record = store.getAt(0);  
        //把获取到第一条记录设置给下拉框   
        //com.setValue(record);
	},
	/**
	 * 获取选中的功能栏按钮组
	 * @public
	 * @return {}
	 */
	getToolbarButtons:function(){
		var me = this;
		var list = [];
		var values = me.getFieldSetValues();
		if(values['toolbar-refresh-checkbox']){
			list.push({type:'refresh',text:values['toolbar-refresh-text']});
		}
		if(values['toolbar-add-checkbox']){
			list.push({type:'add',text:values['toolbar-add-text']});
		}
		if(values['toolbar-show-checkbox']){
			list.push({type:'show',text:values['toolbar-show-text']});
		}
		if(values['toolbar-edit-checkbox']){
			list.push({type:'edit',text:values['toolbar-edit-text']});
		}
		if(values['toolbar-del-checkbox']){
			list.push({type:'del',text:values['toolbar-del-text']});
		}
		return list;
	},
	/**
	 * 获取选中的操作列按钮组
	 * @public
	 * @return {}
	 */
	getActioncolumnButtons:function(){
		var me = this;
		var list = [];
		var values = me.getFieldSetValues();
		if(values['actioncolumn-show-checkbox']){
			list.push({type:'show',text:values['actioncolumn-show-text']});
		}
		if(values['actioncolumn-edit-checkbox']){
			list.push({type:'edit',text:values['actioncolumn-edit-text']});
		}
		if(values['actioncolumn-del-checkbox']){
			list.push({type:'del',text:values['actioncolumn-del-text']});
		}
		return list;
	},
	/**
	 * 获取功能按钮配置数据
	 * @public
	 * @return {}
	 */
	getFieldSetValues:function(){
		var me = this;
		//功能栏按钮组
		var toolbarconfig = me.getComponent('toolbar-config');
		var toolbarrefresh = me.getComponent('toolbar-refresh');
		var toolbaradd = me.getComponent('toolbar-add');
		var toolbaredit = me.getComponent('toolbar-edit');
		var toolbarshow = me.getComponent('toolbar-show');
		var toolbardel = me.getComponent('toolbar-del');
        
		//操作列按钮组
		var actioncolumnconfig = me.getComponent('actioncolumn-config');
		var actioncolumnadd = me.getComponent('actioncolumn-add');
		var actioncolumnedit = me.getComponent('actioncolumn-edit');
		var actioncolumnshow = me.getComponent('actioncolumn-show');
		var actioncolumndel = me.getComponent('actioncolumn-del');
		//弹出的表单设置
		var winformconfig = me.getComponent('winform-config');
		var winformapp = me.getComponent('winform-app');
		//整体对象
		var obj = {
			//功能栏按钮组位置(上、下)
			'toolbar-position':toolbarconfig.getComponent('toolbar-position').getChecked()[0].inputValue,
			//刷新
			'toolbar-refresh-checkbox':toolbarrefresh.getComponent('toolbar-refresh-checkbox').value,
			'toolbar-refresh-text':toolbarrefresh.getComponent('toolbar-refresh-text').value,
			'toolbar-refresh-number':toolbarrefresh.getComponent('toolbar-refresh-number').value,
			//功能栏按钮组-新增
			'toolbar-add-checkbox':toolbaradd.getComponent('toolbar-add-checkbox').value,
			'toolbar-add-text':toolbaradd.getComponent('toolbar-add-text').value,
			'toolbar-add-number':toolbaradd.getComponent('toolbar-add-number').value,
			//功能栏按钮组-修改
			'toolbar-edit-checkbox':toolbaredit.getComponent('toolbar-edit-checkbox').value,
			'toolbar-edit-text':toolbaredit.getComponent('toolbar-edit-text').value,
			'toolbar-edit-number':toolbaredit.getComponent('toolbar-edit-number').value,
			//功能栏按钮组-查看
			'toolbar-show-checkbox':toolbarshow.getComponent('toolbar-show-checkbox').value,
			'toolbar-show-text':toolbarshow.getComponent('toolbar-show-text').value,
			'toolbar-show-number':toolbarshow.getComponent('toolbar-show-number').value,
			//功能栏按钮组-删除
			'toolbar-del-checkbox':toolbardel.getComponent('toolbar-del-checkbox').value,
			'toolbar-del-text':toolbardel.getComponent('toolbar-del-text').value,
			'toolbar-del-number':toolbardel.getComponent('toolbar-del-number').value,
            
			//操作列按钮组-修改
			'actioncolumn-edit-checkbox':actioncolumnedit.getComponent('actioncolumn-edit-checkbox').value,
			'actioncolumn-edit-text':actioncolumnedit.getComponent('actioncolumn-edit-text').value,
			'actioncolumn-edit-number':actioncolumnedit.getComponent('actioncolumn-edit-number').value,
			//操作列按钮组-查看
			'actioncolumn-show-checkbox':actioncolumnshow.getComponent('actioncolumn-show-checkbox').value,
			'actioncolumn-show-text':actioncolumnshow.getComponent('actioncolumn-show-text').value,
			'actioncolumn-show-number':actioncolumnshow.getComponent('actioncolumn-show-number').value,
			//操作列按钮组-删除
			'actioncolumn-del-checkbox':actioncolumndel.getComponent('actioncolumn-del-checkbox').value,
			'actioncolumn-del-text':actioncolumndel.getComponent('actioncolumn-del-text').value,
			'actioncolumn-del-number':actioncolumndel.getComponent('actioncolumn-del-number').value,
			//删除服务地址
			'del-server-combobox':me.getComponent('del-server-combobox').value,
			//弹出表单设置
			'winform-checkbox':winformconfig.getComponent('winform-checkbox').value,
			'winform-text':winformapp.getComponent('winform-text').value,
			'winform-id':winformapp.getComponent('winform-id').value,
			'winform-combobox':me.getComponent('winform-combobox').value
		};
		return obj;
	},
	/**
	 * 设置删除服务的值
	 * @public
	 * @param {} value
	 */
	setDelServerValue:function(value){
		var me = this;
		me.getComponent('del-server-combobox').setValue(value);
	},
	/**
	 * 设置交互字段的值
	 * @public
	 * @param {} value
	 */
	setWinFormComboboxValue:function(value){
		var me = this;
		me.getComponent('winform-combobox').setValue(value);
	}
});