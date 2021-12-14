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
Ext.define('Ext.build.FunButFieldSetTree',{
	extend:'Ext.form.FieldSet',
	alias:'widget.funbutfieldsettree',
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
	},
	/**
	 * 创建内部按钮组
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//过滤栏设置
		var filterTreeConfig = me.createFilterTreeConfig();
		//功能栏按钮组设置
		var toolbarConfig = me.createToolbarConfig();
		//操作列按钮组设置
		var actioncolumnConfig = me.createToolbarConfigTwo();
	
		//弹出表单设置
		var winformConfig = me.createWinformConfig();
		//组合
		var items = filterTreeConfig.concat(toolbarConfig,actioncolumnConfig,winformConfig);
		return items;
	},
	/**
	 * 过滤栏设置
	 * @private
	 * @return {}
	 */
	createFilterTreeConfig:function(){
		var Width = 120;
		var EmptyText = "填写显示文字";
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'filter-config',margin:0,columns:2,vertical:true,
			items:[{
				xtype:'checkbox',boxLabel:'过滤栏设置',width:170,style:'fontWeight:bold;',
				itemId:'toolbar-filter',name:'toolbar-filter'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:5,height:23,
				itemId:'filter-number',name:'filter-number'
			}]
		},{
			xtype:'fieldcontainer',itemId:'toolbar-config-position',margin:0, columns:3,vertical:true,
			items:[{
				xtype:'radiogroup',itemId:'filter-position',margin:0,
	            columns:2,vertical:true,width:100,
	            items:[{
	            	boxLabel:'顶部',name:'filter-position',inputValue:'top',checked:true
	            },{
	            	boxLabel:'底部',name:'filter-position',inputValue:'bottom'
	            }]
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-filter-t',padding:2,hidden:true,
			items:[{
				xtype:'checkbox',boxLabel:'过滤',width:45,
				itemId:'toolbar-filter-checkbox',name:'toolbar-filter-checkbox',checked:true
			},{
				xtype:'textfield',value:'按过滤',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-filter-text',name:'toolbar-filter-text'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-filter-number',name:'toolbar-filter-number'
			}]
		}];
		return items;
	},
	/**
	 * 开启按钮组选择（一）
	 * @private
	 * @return {}
	 */
	createToolbarConfig:function(){
		var Width = 120;
		var EmptyText = "填写显示文字";
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-config',padding:2,margin:0,
			items:[{
				xtype:'checkbox',boxLabel:'开启按钮组选择（一）',width:170,style:'fontWeight:bold;',
				itemId:'toolbar-all',name:'toolbar-all'
			},{
				   xtype:'numberfield',value:2,width:40,minValue:1,maxValue:5,height:23,
				   itemId:'toolbar-number',name:'toolbar-number'
		    }]
		},
		{
			xtype:'fieldcontainer',itemId:'toolbar-config-o',margin:0, columns:2,vertical:true,
			items:[{
				xtype:'radiogroup',itemId:'toolbar-position',padding:2,
	            columns:2,vertical:true,width:100,
	            items:[{
	            	boxLabel:'顶部',name:'toolbar-position',inputValue:'top',checked:true
	            },{
	            	boxLabel:'底部',name:'toolbar-position',inputValue:'bottom'
	            }]
			}]
		},
		{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-refresh',padding:2,
			items:[{
				xtype:'checkbox',boxLabel:'刷新',width:45,
				itemId:'toolbar-refresh-checkbox',name:'toolbar-refresh-checkbox'
			},{
				xtype:'textfield',value:'刷新数据',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-refresh-text',name:'toolbar-refresh-text'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-refresh-number',name:'toolbar-refresh-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-Minus',
			items:[{
				xtype:'checkbox',boxLabel:'收缩',width:45,
				itemId:'toolbar-Minus-checkbox',name:'toolbar-Minus-checkbox'
			},{
				xtype:'textfield',value:'收缩全部',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-Minus-text',name:'toolbar-Minus-text'
			},{
				xtype:'numberfield',value:2,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-Minus-number',name:'toolbar-Minus-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-plus',
			items:[{
				xtype:'checkbox',boxLabel:'展开',width:45,itemId:'edit',
				itemId:'toolbar-plus-checkbox',name:'toolbar-plus-checkbox'
			},{
				xtype:'textfield',value:'展开全部',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-plus-text',name:'toolbar-plus-text'
			},{
				xtype:'numberfield',value:3,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-plus-number',name:'toolbar-plus-number'
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
				xtype:'numberfield',value:4,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-add-number',name:'toolbar-add-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-edit',
			items:[{
				xtype:'checkbox',boxLabel:'修改',width:45,itemId:'edit',
				itemId:'toolbar-edit-checkbox',name:'toolbar-edit-checkbox'
			},{
				xtype:'textfield',value:'修改',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-edit-text',name:'toolbar-edit-text'
			},{
				xtype:'numberfield',value:5,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-edit-number',name:'toolbar-edit-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-show',
			items:[{
				xtype:'checkbox',boxLabel:'查看',width:45,itemId:'show',
				itemId:'toolbar-show-checkbox',name:'toolbar-show-checkbox'
			},{
				xtype:'textfield',value:'查看',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-show-text',name:'toolbar-show-text'
			},{
				xtype:'numberfield',value:6,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-show-number',name:'toolbar-show-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-del',
			items:[{
				xtype:'checkbox',boxLabel:'删除',width:45,itemId:'del',
				itemId:'toolbar-del-checkbox',name:'toolbar-del-checkbox'
			},{
				xtype:'textfield',value:'删除',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-del-text',name:'toolbar-del-text'
			},{
				xtype:'numberfield',value:7,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-del-number',name:'toolbar-del-number'
			}]
		}];
		return items;
	},
	/**
	 * 操作列按钮组设置
	 * @private
	 * @return {}
	 */
	createToolbarConfigTwo:function(){
		var me = this;
		var Width = 120;
		var EmptyText = "填写提示信息";
		var items = [{
			xtype:'fieldcontainer',layout:'hbox',itemId:'actioncolumn-config',
			items:[{
				xtype:'checkbox',boxLabel:'开启按钮组选择（二）',width:170,style:'fontWeight:bold;',
				itemId:'actioncolumn-all',name:'actioncolumn-all'
			},{
				xtype:'numberfield',value:3,width:40,minValue:1,maxValue:5,height:23,
				itemId:'toolbar-number-two',name:'toolbar-number-two'
			}]
		},{
			xtype:'fieldcontainer',itemId:'toolbar-config-Two',margin:0, columns:2,vertical:true,
			items:[{
				xtype:'radiogroup',itemId:'toolbar-position-two',padding:2,
	            columns:2,vertical:true,width:100,
	            items:[{
	            	boxLabel:'顶部',name:'toolbar-position-two',inputValue:'top',checked:true
	            },{
	            	boxLabel:'底部',name:'toolbar-position-two',inputValue:'bottom'
	            }]
			}]
	    },{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-confirm',
			items:[{
				xtype:'checkbox',boxLabel:'确定',width:45,
				itemId:'toolbar-confirm-checkbox',name:'toolbar-confirm-checkbox'
			},{
				xtype:'textfield',value:'确定',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-confirm-text',name:'toolbar-confirm-text'
			},{
				xtype:'numberfield',value:1,width:40,minValue:1,maxValue:3,height:23,
				itemId:'toolbar-confirm-number',name:'toolbar-confirm-number'
			}]
		},{
			xtype:'fieldcontainer',layout:'hbox',itemId:'toolbar-cancel',
			items:[{
				xtype:'checkbox',boxLabel:'取消',width:45,itemId:'cancel',
				itemId:'toolbar-cancel-checkbox',name:'toolbar-cancel-checkbox'
			},{
				xtype:'textfield',value:'取消',height:22,padding:'1 2 0 2',
				emptyText:EmptyText,width:Width,
				itemId:'toolbar-cancel-text',name:'toolbar-cancel-text'
			},{
				xtype:'numberfield',value:2,width:40,minValue:1,maxValue:3,height:23,
				itemId:'toolbar-cancel-number',name:'toolbar-cancel-number'
			}]
		},{
			xtype:'combobox',width:224,
			emptyText:'请选择删除数据服务',
			editable:true,typeAhead:true,forceSelection:true,
        	queryMode:'local',hidden:true,
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
			itemId:'del-server-combobox',name:'del-server-combobox'
		}];
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
			fieldLabel:'交互字段',labelWidth:55,hidden:true,
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
		//过滤栏-全选/全不选
		var filter = me.getComponent('filter-config').getComponent('toolbar-filter');
		filter.on({
			change:function(field,newValue){
				//过滤栏选中
				var isToolfilterChecked = me.isToolfilterChecked(true);
				//功能栏按钮组是否全部未选中
				var isToolfilterAllNotChecked  = me.isToolfilterChecked(false);
				//在全选中的状态下全不选、在全未选中的状态下全选
				if((!newValue && isToolfilterChecked) || (newValue && isToolfilterAllNotChecked)){
					me.checkToolfilter(newValue);
				}
			}
		});
		//过滤栏
		var toolbarfilter = me.getComponent('toolbar-filter-t').getComponent('toolbar-filter-checkbox');
		toolbarfilter.on({
			change:function(field,newValue){
				//操作列按钮组是否全部选中
				var isToolfilterChecked = me.isToolfilterChecked(true);
				filter.setValue(isToolfilterChecked);
			}
		});
		//按钮组一-全选/全不选
		var toolbar = me.getComponent('toolbar-config').getComponent('toolbar-all');
		toolbar.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				//功能栏按钮组是否全部未选中
				var isToolbarAllNotChecked = me.isToolbarAllChecked(false);
				//在全选中的状态下全不选、在全未选中的状态下全选
				if((!newValue && isToolbarAllChecked) || (newValue && isToolbarAllNotChecked)){
					me.checkToolbarAll(newValue);
				}
			}
		});
		//功能栏按钮组-刷新
		var toolbarrefresh = me.getComponent('toolbar-refresh').getComponent('toolbar-refresh-checkbox');
		toolbarrefresh.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});
		//功能栏按钮组-新增
		var toolbarminus = me.getComponent('toolbar-add').getComponent('toolbar-add-checkbox');
		toolbarminus.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});
		//功能栏按钮组-展开
		var toolbarminus = me.getComponent('toolbar-Minus').getComponent('toolbar-Minus-checkbox');
		toolbarminus.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});
		//功能栏按钮组-修改
		var toolbarplus = me.getComponent('toolbar-plus').getComponent('toolbar-plus-checkbox');
		toolbarplus.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});
		//功能栏按钮组-查看
		var toolbarshow = me.getComponent('toolbar-show').getComponent('toolbar-show-checkbox');
		toolbarshow.on({
			change:function(field,newValue){
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});
		//功能栏按钮组-删除
		var toolbardel = me.getComponent('toolbar-del').getComponent('toolbar-del-checkbox');
		toolbardel.on({
			change:function(field,newValue){
				me.showDelServerCom();
				//功能栏按钮组是否全部选中
				var isToolbarAllChecked = me.isToolbarAllChecked(true);
				toolbar.setValue(isToolbarAllChecked);
			}
		});		
 
		//按钮组（二）-全选/全不选
		var actioncolumn = me.getComponent('actioncolumn-config').getComponent('actioncolumn-all');
		actioncolumn.on({
			change:function(field,newValue){
				//操作列按钮组是否全部选中
				var isActioncolumnAllChecked = me.isActioncolumnAllChecked(true);
				//操作列按钮组是否全部未选中
				var isActioncolumnAllNotChecked = me.isActioncolumnAllChecked(false);
				//在全选中的状态下全不选、在全未选中的状态下全选
				if((!newValue && isActioncolumnAllChecked) || (newValue && isActioncolumnAllNotChecked)){
					me.checkActioncolumnAll(newValue);
				}
			}
		});
		//操作列按钮组-修改
		var actioncolumnedit = me.getComponent('toolbar-confirm').getComponent('toolbar-confirm-checkbox');
		actioncolumnedit.on({
			change:function(field,newValue){
				//操作列按钮组是否全部选中
				var isActioncolumnAllChecked = me.isActioncolumnAllChecked(true);
				actioncolumn.setValue(isActioncolumnAllChecked);
			}
		});
		//操作列按钮组-查看
		var actioncolumnshow = me.getComponent('toolbar-cancel').getComponent('toolbar-cancel-checkbox');
		actioncolumnshow.on({
			change:function(field,newValue){
				//操作列按钮组是否全部选中
				var isActioncolumnAllChecked = me.isActioncolumnAllChecked(true);
				actioncolumn.setValue(isActioncolumnAllChecked);
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
    				Ext.Msg.alert("提示","请选择一个应用！");
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
		var bo = (toolbardel );
		
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
		var minus = me.getComponent('toolbar-Minus').getComponent('toolbar-Minus-checkbox').value;
		var plus = me.getComponent('toolbar-plus').getComponent('toolbar-plus-checkbox').value;
		var add = me.getComponent('toolbar-add').getComponent('toolbar-add-checkbox').value;
		var edit = me.getComponent('toolbar-edit').getComponent('toolbar-edit-checkbox').value;
		var show = me.getComponent('toolbar-show').getComponent('toolbar-show-checkbox').value;
		var del = me.getComponent('toolbar-del').getComponent('toolbar-del-checkbox').value;
		return checked ? (refresh && minus && plus && add && edit && show && del) : !(refresh || minus || plus || add || edit || show || del);
	},
	/**
	 * 操作列按钮组是否全部选中
	 * @private
	 * @param checked true(全部选中)；false(全部未选中)
	 * @return {}
	 */
	isActioncolumnAllChecked:function(checked){
		var me = this;
		var confirm = me.getComponent('toolbar-confirm').getComponent('toolbar-confirm-checkbox').value;
		var cancel = me.getComponent('toolbar-cancel').getComponent('toolbar-cancel-checkbox').value;
		return checked ? (confirm && cancel ) : !(confirm || cancel );
	},
	/**
	 * 过滤栏是否全部选中
	 * @private
	 * @param checked true(全部选中)；false(全部未选中)
	 * @return {}
	 */
	isToolfilterChecked:function(checked){
		var me = this;
		var filter = me.getComponent('toolbar-filter-t').getComponent('toolbar-filter-checkbox').value;
		return checked ? (filter) : !(filter);
	},
	/**
	 * 设置过滤栏选中状态
	 * @private
	 * @param {} checked
	 */
	checkToolfilter:function(checked){
		var me = this;
		me.getComponent('toolbar-filter-t').getComponent('toolbar-filter-checkbox').setValue(checked);
	       
	},
	/**
	 * 设置功能栏按钮组选中状态
	 * @private
	 * @param {} checked
	 */
	checkToolbarAll:function(checked){
		var me = this;
		me.getComponent('toolbar-refresh').getComponent('toolbar-refresh-checkbox').setValue(checked);
		me.getComponent('toolbar-Minus').getComponent('toolbar-Minus-checkbox').setValue(checked);
		me.getComponent('toolbar-plus').getComponent('toolbar-plus-checkbox').setValue(checked);
		me.getComponent('toolbar-add').getComponent('toolbar-add-checkbox').setValue(checked);
		me.getComponent('toolbar-edit').getComponent('toolbar-edit-checkbox').setValue(checked);
		me.getComponent('toolbar-show').getComponent('toolbar-show-checkbox').setValue(checked);
		me.getComponent('toolbar-del').getComponent('toolbar-del-checkbox').setValue(checked);        
	},
	/**
	 * 设置操作列按钮组选中状态
	 * @private
	 * @param {} checked
	 */
	checkActioncolumnAll:function(checked){
		var me = this;
		me.getComponent('toolbar-confirm').getComponent('toolbar-confirm-checkbox').setValue(checked);
		me.getComponent('toolbar-cancel').getComponent('toolbar-cancel-checkbox').setValue(checked);
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
		if(values['toolbar-Minus-checkbox']){
			list.push({type:'minus',text:values['toolbar-Minus-text']});
		}
		if(values['toolbar-plus-checkbox']){
			list.push({type:'plus',text:values['toolbar-plus-text']});
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
		if(values['toolbar-cancel-checkbox']){
			list.push({type:'show',text:values['toolbar-cancel-text']});
		}
		if(values['toolbar-confirm-checkbox']){
			list.push({type:'confirm',text:values['toolbar-confirm-text']});
		}
		return list;
	},
	
	/**
	 * 获取选中的过滤栏
	 * @public
	 * @return {}
	 */
	getfilter:function(){
		var me = this;
		var list = [];
		var values = me.getFieldSetValues();
		if(values['toolbar-filter-checkbox']){
			list.push({type:'filter',text:values['toolbar-filter--text']});
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
		//功能栏按钮组一
		var toolbarconfig = me.getComponent('toolbar-config');
		var toolbarposition=me.getComponent('toolbar-config-o').getComponent('toolbar-position');
		//功能栏按钮组二
		var toolbarpositionTwo=me.getComponent('toolbar-config-Two').getComponent('toolbar-position-two');
	
		//过滤栏
		var toolbarfilter=me.getComponent('filter-config');
		var toolbarpositionfilter=me.getComponent('toolbar-config-position');
		var toolfilter = me.getComponent('toolbar-filter-t');
		
		var toolbarrefresh = me.getComponent('toolbar-refresh');
		var toolbarminus = me.getComponent('toolbar-Minus');
		var toolbarplus = me.getComponent('toolbar-plus');
		var toolbarshow = me.getComponent('toolbar-show');
		var toolbardel = me.getComponent('toolbar-del');
		
		var toolbaradd = me.getComponent('toolbar-add');
		var toolbaredit = me.getComponent('toolbar-edit');
        
		//操作列按钮组
		var actioncolumnconfig = me.getComponent('actioncolumn-config');
		var actioncolumnadd = me.getComponent('actioncolumn-add');
		var toolbarconfirm = me.getComponent('toolbar-confirm');
		var actioncolumnshow = me.getComponent('toolbar-cancel');
		//弹出的表单设置
		var winformconfig = me.getComponent('winform-config');
		var winformapp = me.getComponent('winform-app');
		//整体对象
		var obj = {
			//功能栏按钮组
			'toolbar-all':toolbarconfig.getComponent('toolbar-all').value,
			'toolbar-position':toolbarposition.getChecked()[0].inputValue,
			'toolbar-number':toolbarconfig.getComponent('toolbar-number').value,

			//功能按钮组二
			'actioncolumn-all':actioncolumnconfig.getComponent('actioncolumn-all').value,
			'toolbar-position-two':toolbarpositionTwo.getChecked()[0].inputValue,
			'toolbar-number-two':actioncolumnconfig.getComponent('toolbar-number-two').value,
			//过滤栏
			'toolbar-filter':toolbarfilter.getComponent('toolbar-filter').value,
			'filter-position':toolbarpositionfilter.getComponent('filter-position').getChecked()[0].inputValue,
			'filter-number':toolbarfilter.getComponent('filter-number').value,
			
			//过滤
			'toolbar-filter-checkbox':toolfilter.getComponent('toolbar-filter-checkbox').value,
			'toolbar-filter-text':toolfilter.getComponent('toolbar-filter-text').value,
			'toolbar-filter-number':toolfilter.getComponent('toolbar-filter-number').value,
			
			//刷新
			'toolbar-refresh-checkbox':toolbarrefresh.getComponent('toolbar-refresh-checkbox').value,
			'toolbar-refresh-text':toolbarrefresh.getComponent('toolbar-refresh-text').value,
			'toolbar-refresh-number':toolbarrefresh.getComponent('toolbar-refresh-number').value,
			//功能栏按钮组-收缩
			'toolbar-Minus-checkbox':toolbarminus.getComponent('toolbar-Minus-checkbox').value,
			'toolbar-Minus-text':toolbarminus.getComponent('toolbar-Minus-text').value,
			'toolbar-Minus-number':toolbarminus.getComponent('toolbar-Minus-number').value,
			//功能栏按钮组-展开
			'toolbar-plus-checkbox':toolbarplus.getComponent('toolbar-plus-checkbox').value,
			'toolbar-plus-text':toolbarplus.getComponent('toolbar-plus-text').value,
			'toolbar-plus-number':toolbarplus.getComponent('toolbar-plus-number').value,
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
            
			//按钮组二
			'actioncolumn-all':actioncolumnconfig.getComponent('actioncolumn-all').value,
			
			//按钮组二-确定
			'toolbar-confirm-checkbox':toolbarconfirm.getComponent('toolbar-confirm-checkbox').value,
			'toolbar-confirm-text':toolbarconfirm.getComponent('toolbar-confirm-text').value,
			'toolbar-confirm-number':toolbarconfirm.getComponent('toolbar-confirm-number').value,
			
			//按钮组二-取消
			'toolbar-cancel-checkbox':actioncolumnshow.getComponent('toolbar-cancel-checkbox').value,
			'toolbar-cancel-text':actioncolumnshow.getComponent('toolbar-cancel-text').value,
			'toolbar-cancel-number':actioncolumnshow.getComponent('toolbar-cancel-number').value,

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