/**
 * 双表移动面板
 * 
 * 【功能】
 * 双表移动,提供确定、重置功能
 * 结果排序功能,可以拖动数据进行排序
 * 
 * 【可配属性】
 * [width] 面板宽度，默认600
 * [height] 面板高度，默认300
 * 
 * [checkedWidth] 已选列表宽度(百分比,例如'0.7')，默认(面板宽度-按钮栏宽度)/2;
 * [unckeckedWidth] 待选列表宽度(百分比,例如'0.3')，默认(面板宽度-按钮栏宽度)/2;
 * 
 * [checkedTitle] 已选列表标题,默认‘已选列表’
 * [unckeckedTitle] 待选列表标题,默认‘待选列表’
 * 
 * [buttonsToolbar] 功能按钮栏,默认4个按钮(全部已选,选中已选,选中待选,全部待选),可以重写
 * 
 * --------isRealtime 是否实时处理数据,即移动动一次数据就存储,默认false
 * [checkedGridDock] 已选列表的位置(left、right),默认left
 * [checkedGridDblClick] 已选列表是否支持双击移动数据,默认true
 * [checkedGridDblClick] 待选列表是否支持双击移动数据,默认true
 * [canDrag] 是否可拖动排序,默认true
 * 
 * [searchField] 查询组件,默认false,可以配置已选和待选的列表的查询组件
 * 
 * [defaultLoad] 是否默认加载数据,默认false;
 * [saveToReLoad] 保存后重新加载数据,默认true
 * 
 * [checkedGridPageSize] 已选列表加载的最大数据数量,默认1000;
 * [uncheckedGridPageSize] 待选列表加载的最大数据数量,默认1000;
 * 
 * @example
 * searchField:{
 * 		checkedGrid:{//已选列表
 * 			dock:'top',//默认是top,可以是bottom
 * 			emptyText:'',//查询框为空时显示的文字
 * 			searchColumns:[]//需要过滤的列对应的dataIndex
 * 		},
 * 		uncheckedGrid:{//待选列表
 * 			dock:'top',//默认是top,可以是bottom
 * 			emptyText:'',//查询框为空时显示的文字
 * 			searchColumns:[]//需要过滤的列对应的dataIndex
 * 		}
 * }
 * 
 * 【必配属性】
 * addUrl 新增服务地址
 * delUrl 删除服务地址
 * [editUrl] 修改服务地址,用于更新排序字段或其他需要修改的字段，如果没有需要修改的字段可以不配
 * checkedUrl 已选数据服务地址
 * uncheckedUrl 待选数据服务地址
 * filterColumn 过滤列,已选与待选列表匹配的列
 * keyColumn 主键列
 * [orderColumn] 排序列,用以保存数据的顺序,没有排序列可以不配
 * 
 * columns 需要显示的列表视图信息
 * @example
 * columns:[{
 * 		dataIndex:'Id',//主键ID
 * 		text:'主键ID',
 * 		hidden:true,
 * 		hideable:false,
 * 		
 * 		checkedGrid:'Item_Id',//已选列中存在该列,对应的列字段是Item_Id
 * 		uncheckedGrid:'ItemAllItem_Id',//待选列中不存在该列,对应的列字段是ItemAllItem_Id
 * }]
 * 
 * getAddParamsByRecord 获取需要新增的数据;
 * 		该方法需要重写,提供record,返回params
 * [getEditParamsByRecord] 获取需要修改的数据;
 * 		该方法需要重写,提供record,返回params
 * [hasToEdit] 判断本行数据是否需要修改
 * 		该方法可以重写，默认只判断排序列,提供record，返回bool
 * 
 * 【公开事件】
 * aftersave 保存成功后执行,提供本面板
 * 
 * 【提供的内部方法】
 * selectAll 所有的待选变已选,重写buttonsToolbar时会用到
 * selectChecked 选中的待选变已选,重写buttonsToolbar时会用到
 * cancelAll 所有的已选变待选,重写buttonsToolbar时会用到
 * cancelChecked 选中的已选变待选,重写buttonsToolbar时会用到
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.BasicTwoGrid',{
	extend:'Ext.panel.Panel',
	type:'tabpanel',
	alias:'widget.zhifanguxbasictwogrid',
	
	width:600,
	height:300,
	bodyPadding:'4 10 0 10',
	
	/**初始已选数据*/
	defaultCheckedList:[],
	/**多选*/
	multiSelect:true,
	/**处理的总条数*/
	resultCount:0,
	/**当前处理的条数*/
	nowCount:0,
	/**错误信息数组*/
	ErrorInfo:[],
	
	/**新增服务地址*/
	addUrl:'',
	/**删除服务地址*/
	delUrl:'',
	/**修改服务地址*/
	editUrl:'',
	/**已选数据服务地址*/
	checkedUrl:'',
	/**待选数据服务地址*/
	uncheckedUrl:'',
	/**主键列*/
	keyColumn:'Id',
	/**过滤列*/
	filterColumn:null,
	/**排序列*/
	orderColumn:null,
	/**需要显示的列表视图信息*/
	columns:[],
	
	/**是否实时处理数据*/
	isRealtime:false,
	/**已选列表的位置*/
	checkedGridDock:'left',
	
	/**已选列表是否支持双击移动数据*/
	checkedGridDblClick:true,
	/**待选列表是否支持双击移动数据*/
	uncheckedGridDblClick:true,
	/**是否可拖动排序*/
	canDrag:true,
	
	/**数据查询框信息*/
	searchField:false,
	/**默认加载数据*/
	defaultLoad:false,
	/**保存后重新加载数据*/
	saveToReLoad:true,
	/**已选列表加载的最大数据数量*/
	checkedGridPageSize:1000,
	/**待选列表加载的最大数据数量*/
	uncheckedGridPageSize:1000,
	
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initListeners();//初始化监听
		if(me.defaultLoad){
			me.load();//加载数据
		}else{
			me.disableControl();
		}
	},
	initComponent:function(){
		var me = this;
		//初始化属性参数
		me.initParams();
		//初始化视图
		me.initView();
		me.callParent(arguments);
	},
	/**
	 * 初始化属性参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		me.addEvents('aftersave');
		me.layout = 'column';
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		//创建内部组件
		me.items = me.items || me.createItems();
		//创建挂靠
		me.dockedItems = me.dockedItems || me.createDoeckedItems();
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//操作按钮
		var toolbar = me.createButtonsToolbar();
		//已选列表
		var checkedGrid = me.createCheckedGrid();
		checkedGrid.columnWidth = me.checkedWidth || '0.5';
		//待选列表
		var uncheckedGrid = me.createUncheckedGrid();
		uncheckedGrid.columnWidth = me.uncheckedWidth || '0.5';
		
		var items = [checkedGrid,toolbar,uncheckedGrid];
		
		for(var i in items){
			items[i].margin = 0;
		}
		return items;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDoeckedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',dock:'bottom',itemId:'bottomtoolbar',
			items:['->',{
                xtype:'button',text:'保存',iconCls:'build-button-save',
                handler:function(but){me.submit(but);}
            },{
                xtype:'button',text:'重置',iconCls:'build-button-refresh',
                handler:function(but){me.load();}
            }]
		}];
		return dockedItems;
	},
	/**
	 * 创建操作按钮栏
	 * @private
	 * @return {}
	 */
	createButtonsToolbar:function(){
		var me = this;
		var toolbar = me.buttonsToolbar || {
			xtype:'toolbar',width:70,defaults:{margins:'0 0 5 0'},style:{background:'#fff'},
			layout:{type:'vbox',padding:'5',pack:'center',align:'center'},
            border:false,
			items:[{
				text:'全部',iconCls:'left16_2',iconAlign:'left',
				handler:function(){me.selectAll();}
			},{
				text:'选择',iconCls:'left16',iconAlign:'left',
				handler:function(){me.selectChecked();}
			},{
				text:'选择',iconCls:'right16',iconAlign:'right',
				handler:function(){me.cancelChecked();}
			},{
				text:'全部',iconCls:'right16_2',iconAlign:'right',
				handler:function(){me.cancelAll();},margins:'0'
			}]
		};
		return toolbar;
	},
	/**
	 * 创建已选列表
	 * @private
	 * @return {}
	 */
	createCheckedGrid:function(){
		var me = this,
			fields = me.getFields(),
			columns = me.getColumns('checkedGrid');
			
		var grid = {
			xtype:'grid',
			itemId:'checkedGrid',
			multiSelect:me.multiSelect || false,
			viewConfig:{loadMask:false},
			columns:columns,
			store:new Ext.data.Store({
				fields:fields,
				pageSize:me.checkedGridPageSize || 1000,
				proxy:{
					type:'ajax',url:me.checkedUrl,
					reader:{type:'json',totalProperty:'count',root:'list'},
					//内部数据匹配方法
		            extractResponseData:function(response){
				    	return me.changeCheckedGridData(response); 
				  	}
				}
			})
		};
		//是否可以拖动排序
		if(me.canDrag){
			grid.viewConfig.trackOver = false;
			grid.viewConfig.plugins = {ptype:'gridviewdragdrop'};
			grid.viewConfig.listeners = {
				drop:function(node,data,overModel,dropPosition,eOpts){
					me.reorder();//重新排序
				}
			};
		}
		
		//查询框组件
		var searchField = me.getSearchField('checkedGrid');
		if(searchField){
			var dock = me.searchField.checkedGrid.dock || 'top';
			grid.dockedItems = [{
				xtype:'toolbar',itemId:'toptoolbar',dock:dock,
				items:[searchField]
			}];
		}
		
		var fieldset = {
			xtype:'fieldset',title:me.checkedTitle || '已选列表',
			padding:'0 2 2 2',collapsible:false,layout:'fit',
	        itemId:'checkedGrid',
	        items:[grid]
		};
		return fieldset;
	},
	/**
	 * 创建待选列表
	 * @private
	 * @return {}
	 */
	createUncheckedGrid:function(){
		var me = this,
			fields = me.getFields(),
			columns = me.getColumns('uncheckedGrid');
			
		var grid = {
			xtype:'grid',
			itemId:'uncheckedGrid',
			multiSelect:me.multiSelect || false,
			viewConfig:{loadMask:false},
			columns:columns,
			store:new Ext.data.Store({
				fields:fields,
				pageSize:me.uncheckedGridPageSize || 1000,
				proxy:{
					type:'ajax',url:me.uncheckedUrl,
					reader:{type:'json',totalProperty:'count',root:'list'},
					//内部数据匹配方法
		            extractResponseData:function(response){
				    	return me.changeUncheckedGridData(response); 
				  	}
				}
			})
		};
		
		//查询框组件
		var searchField = me.getSearchField('uncheckedGrid');
		if(searchField){
			var dock = me.searchField.uncheckedGrid.dock || 'top';
			grid.dockedItems = [{
				xtype:'toolbar',itemId:'toptoolbar',dock:dock,
				items:[searchField]
			}];
		}
		
		var fieldset = {
			xtype:'fieldset',title:me.checkedTitle || '待选列表',
			padding:'0 2 2 2',collapsible:false,layout:'fit',
	        itemId:'uncheckedGrid',
	        items:[grid]
		};
		return fieldset;
	},
	/**
	 * 获取fields属性
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me = this,
			columns = me.columns;
			fields = [];
			
		for(var i in columns){
			fields.push(columns[i].dataIndex);
		}
		return fields;
	},
	/**
	 * 启用所有的操作功能
	 * @private
	 */
	enableControl:function(){
		var me = this,
			bottomtoolbar = me.getComponent('bottomtoolbar');
		
		var items = bottomtoolbar.items.items;
		for(var i in items){
			items[i].enable();
		}
	},
	/**
	 * 禁用所有的操作功能
	 * @private
	 */
	disableControl:function(){
		var me = this,
			bottomtoolbar = me.getComponent('bottomtoolbar');
		
		var items = bottomtoolbar.items.items;
		for(var i in items){
			items[i].disable();
		}
	},
	/**
	 * 显示隐藏数据加载遮罩层
	 * @private
	 * @param {} bo
	 */
	openMask:function(bo,loading){
		var me = this;
		if(bo){
			if(me.mk){delete me.mk;}
			var msg = loading || '数据加载中...';
			me.mk = new Ext.LoadMask(me.getEl(),{msg:msg,removeMask:true});
			me.mk.show();//显示遮罩层
		}else{
			me.mk.hide();//隐藏遮罩层
		}
	},
	/**
	 * 提交数据
	 * @private
	 */
	submit:function(){
		var me = this,
			defaultCheckedList = me.defaultCheckedList,
			store = me.getGridByKey('checkedGrid').store;
			
		var add = [],del = [],edit= [];//需要新增、删除、修改的数据
			
		for(var i in defaultCheckedList){
			var id = defaultCheckedList[i].get(me.keyColumn);
			var record = store.findRecord(me.keyColumn,id);
			if(!record){//需要删除的数据
				del.push(defaultCheckedList[i].get(me.keyColumn));
			}else{
				var bo = me.hasToEdit(defaultCheckedList[i],record);//是否需要修改数据
				if(bo){	
					edit.push(record);
				}
			}
		}
		
		store.each(function(rec){
			var id = rec.get(me.keyColumn);
			if(!id || id == ''){
				add.push(rec);//需要新增的数据
			}
		});
		
		me.resultCount = add.length + del.length + edit.length;//处理的总条数
		
		if(me.resultCount == 0){
			alertInfo("没有做任何修改,不需要保存数据！");
		}else{
			me.disableControl();//禁用的操作所有功能
			me.openMask(true,'数据保存中...');
			//新增数据
			for(var i in add){
				me.addOne(add[i],function(text){me.showResult(text);});
			}
			//删除数据
			for(var i in del){
				me.delOne(del[i],function(text){me.showResult(text);});
			}
			//修改数据
			for(var i in edit){
				me.editOne(edit[i],function(text){me.showResult(text);});
			}
		}
	},
	/**
	 * 新增一条数据
	 * @private
	 * @param {} record
	 * @param {} callback
	 */
	addOne:function(record,callback){
		var me = this,
			params = me.getAddParamsByRecord(record);
		
		params = Ext.JSON.encode(params);
		postToServer(me.addUrl,params,callback,null,false);
	},
	/**
	 * 删除一条数据
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	delOne:function(id,callback){
		var me = this,
			url = me.delUrl + "?id=" + id;
			
		getToServer(url,callback,false);
	},
	/**
	 * 修改一条数据
	 * @private
	 * @param {} record
	 * @param {} callback
	 */
	editOne:function(record,callback){
		var me = this,
			params = me.getEditParamsByRecord(record);
		
		params = Ext.JSON.encode(params);
		postToServer(me.editUrl,params,callback,null,false);
	},
	/**
	 * 转化已选列表的数据
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeCheckedGridData:function(response){
		var me = this,
			data = Ext.JSON.decode(response.responseText);
			
		var success = (data.success + '' == 'true' ? true : false);
    	if(success){
    		//数据转换
    		data = me.changeData(data);
    		//处理数据
    		var dataIndexInfo = me.getDataIndexInfo(true);
    		var showColumns = dataIndexInfo.showColumns;
    		var resouceColumns = dataIndexInfo.resouceColumns;
    		
    		for(var i=0;i<data.list.length;i++){
    			for(var j=0;j<showColumns.length;j++){
    				if(me.orderColumn && me.orderColumn == showColumns[j]){
    					data.list[i][showColumns[j]] = parseInt(data.list[i][resouceColumns[j]]);
    				}else{
    					data.list[i][showColumns[j]] = data.list[i][resouceColumns[j]];
    				}
    			}
    		}
    	}else{
    		me.ErrorInfo.push(data.ErrorInfo);
    	}
    	
		response.responseText = Ext.JSON.encode(data);
		return response;
	},
	/**
	 * 转化待选列表的数据
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeUncheckedGridData:function(response){
		var me = this,
			data = Ext.JSON.decode(response.responseText);
			
		var success = (data.success + '' == 'true' ? true : false);
    	if(success){
    		//数据转换
    		data = me.changeData(data);
    		//处理数据
    		var dataIndexInfo = me.getDataIndexInfo(false);
    		var showColumns = dataIndexInfo.showColumns;
    		var resouceColumns = dataIndexInfo.resouceColumns;
    		
    		for(var i=0;i<data.list.length;i++){
    			for(var j=0;j<showColumns.length;j++){
    				data.list[i][showColumns[j]] = data.list[i][resouceColumns[j]];
    			}
    		}
    		//过滤已选的数据
    		data.list = me.filterData(data.list);
    	}else{
    		me.ErrorInfo.push(data.ErrorInfo);
    	}
    	
		response.responseText = Ext.JSON.encode(data);
		return response;
	},
	/**
	 * 过滤已选数据
	 * @private
	 * @param {} list
	 * @return {}
	 */
	filterData:function(list){
		var me = this,
			defaultCheckedList = me.defaultCheckedList,
			filterColumnInfo = me.getFilterColumnInfo(),
			result = [];
		
		if(!filterColumnInfo)
			return list;
		if(!filterColumnInfo.dataIndex)
			return list;
		if(!filterColumnInfo.uncheckedGrid)
			return list;
			
		var checked,unchecked;
		for(var i in list){
			var bo = true;
			for(var j in defaultCheckedList){
				checked = defaultCheckedList[j].get(filterColumnInfo.dataIndex);
				unchecked = list[i][filterColumnInfo.uncheckedGrid];
				if(checked == unchecked){
					bo = false;break;
				}
			}
			if(bo){result.push(list[i]);}
		}
		
		return result;
	},
	/**
	 * 数据转换
	 * @private
	 * @param {} data
	 * @return {}
	 */
	changeData:function(data){
    	if(data.ResultDataValue && data.ResultDataValue != ''){
    		data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,'');
    		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	    	data.list = ResultDataValue.list;
	    	data.count = ResultDataValue.count;
    	}else{
    		data.list = [];
    		data.count = 0;
    	}
    	return data;
	},
	/**
	 * 获取显示列与原始列的信息
	 * @private
	 * @param {} isChecked 是否是已选列表
	 */
	getDataIndexInfo:function(isChecked){
		var me = this,
			columns = me.columns,
			key = isChecked ? 'checkedGrid' : 'uncheckedGrid';
			info = {showColumns:[],resouceColumns:[]};
			
		for(var i in columns){
			if(columns[i][key]){
				info.showColumns.push(columns[i].dataIndex);
				info.resouceColumns.push(columns[i][key]);
			}
		}
		
		return info;
	},
	/**
	 * 获取过滤列信息
	 * @private
	 * @return {}
	 */
	getFilterColumnInfo:function(){
		var me = this,
			columns = me.columns,
			filterColumn = me.filterColumn,
			info = {dataIndex:null,uncheckedGrid:null};
		
		if(!filterColumn)
			return null;
			
		for(var i in columns){
			if(columns[i].dataIndex == filterColumn){
				info.dataIndex = columns[i].dataIndex;
				info.uncheckedGrid = columns[i].uncheckedGrid;
			}
		}
		
		return info;
	},
	/**
	 * 获取排序列信息
	 * @private
	 * @return {}
	 */
	getOrderColumnInfo:function(){
		var me = this,
			columns = me.columns,
			orderColumn = me.orderColumn,
			info = {dataIndex:null,checkedGrid:null};
		
		if(!orderColumn)
			return null;
			
		for(var i in columns){
			if(columns[i].dataIndex == orderColumn){
				info.dataIndex = columns[i].dataIndex;
				info.checkedGrid = columns[i].checkedGrid;
			}
		}
		
		return info;
	},
	/**
	 * 显示错误信息
	 * @private
	 */
	showError:function(){
		var me = this,
			ErrorInfo = me.ErrorInfo;
			
		if(ErrorInfo.length > 0){
			alertError(ErrorInfo.join('</br>'));
		}
		
		me.ErrorInfo = [];
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this,
			checkedGrid = me.getGridByKey('checkedGrid'),
			uncheckedGrid = me.getGridByKey('uncheckedGrid');
			
		//待选项目双击监听
		uncheckedGrid.on({
			itemdblclick:function(view,record){
				if(me.uncheckedGridDblClick){
					me.checkedRecords(true);
				}
			}
		});
		//待选项目数据加载监听
		uncheckedGrid.store.on({
			beforeload:function(store){store.sorters.clear();},
			load:function(store,records,successful){
				me.openMask(false);//隐藏遮罩层
				me.enableControl();//启用操作
				me.showError();//显示错误
			}
		});
		
		//已选项目双击监听
		checkedGrid.on({
			itemdblclick:function(view,record){
				if(me.checkedGridDblClick){
					me.checkedRecords(false);
				}
			}
		});
		//已选项目数据加载监听
		checkedGrid.store.on({
			beforeload:function(store){
				//store.sorters.clear();
			},
			load:function(store,records,successful){
				//用以保存时候判断增删改数据
				me.defaultCheckedList = [];
				for(var i in records){
					me.defaultCheckedList.push(records[i].copy());
				}
				uncheckedGrid.store.sorters.clear();
				//加载待选数据
				uncheckedGrid.store.load();
			}
		});
		//面板高度变化时内部组件高度作出调整
		me.on({
			resize:function(com,width,height){
				var items = me.items.items;
				
				var dHeight = 65;
				if(me.preventHeader || !me.header || !me.title)
					dHeight -= 25;
				
				for(var i in items){
					items[i].setHeight(height - dHeight);
				}
			}
		});
	},
	/**
	 * 选中数据处理
	 * @private
	 * @param {} isChecked 变成已选(true),变成待选(false),默认false
	 * @param {} isAll 移动所有数据
	 * @param {} callback 数据处理
	 */
	checkedRecords:function(isChecked,isAll,callback){
		var me = this,
			gridArr = isChecked ? ['uncheckedGrid','checkedGrid'] : ['checkedGrid','uncheckedGrid'],
			grid1 = me.getGridByKey(gridArr[0]),//选中方
			grid2 = me.getGridByKey(gridArr[1]);//接收方
			
		if(isAll){
			grid1.getSelectionModel().selectAll();
		}
		
		var records = grid1.getSelectionModel().getSelection();
		
		if(records && records.length > 0){
			grid1.store.remove(records);
			
			if(isChecked){
				records = me.changeRecordsIndex(records);//内部序号赋值
			}else{
				me.reorder();//重新排序
			}
			//如果回调存在,则回调处理数据
			if(Ext.typeOf(callback) == 'function'){
				callback(records);
			}else{
				grid2.store.insert(grid2.store.getCount(),records);
			}
		}
	},
	/**
	 * 结果处理
	 * @private
	 * @param {} text
	 */
	showResult:function(text){
		var me = this;
		me.nowCount++;
		
		var result = Ext.JSON.decode(text);
		if(!result.success){//错误信息暂存
			me.ErrorInfo.push(result.ErrorInfo);
		}
		
		if(me.nowCount == me.resultCount){
			me.openMask(false);//隐藏遮罩层
			me.enableControl();//启用功能栏
			me.nowCount = 0;//当期处理条数清零
			if(me.ErrorInfo.length > 0){
				alertError(me.ErrorInfo.join('</br>'));//错误信息提示
				me.ErrorInfo = [];//信息清空
				if(me.saveToReLoad){me.load();}
			}else{//保存成功
				if(me.saveToReLoad){me.load();}
				me.fireEvent('aftersave',me);
			}
		}
	},
	/**
	 * 获取已选/待选列表
	 * @private
	 * @return {}
	 */
	getGridByKey:function(key){
		var me = this,
			grid = null;
			
		if(key == 'checkedGrid'){
			grid = me.getComponent('checkedGrid').getComponent('checkedGrid');
		}else if(key == 'uncheckedGrid'){
			grid = me.getComponent('uncheckedGrid').getComponent('uncheckedGrid');
		}
			
		return grid;
	},
	/**
	 * 获取需要新增的数据
	 * @fileOverview
	 * @param {} record
	 * @return {}
	 */
	getAddParamsByRecord:function(record){
		return null;
	},
	/**
	 * 获取需要修改的数据
	 * @fileOverview
	 * @param {} record
	 * @return {}
	 */
	getEditParamsByRecord:function(record){
		return null;
	},
	/**
	 * 获取列信息
	 * @private
	 * @param {} name
	 */
	getColumns:function(name){
		var me = this,
			columns = [];
			
		for(var i in me.columns){
			var column = Ext.clone(me.columns[i]);
			if(!column[name]){
				column.hidden = true;
				column.hideable = false;
			}
			columns.push(column);
		}
		
		//内部序号,用于排序
		columns.push({
			dataIndex:'innerIndex',
			hidden:true,hideable:false,
			text:'内部序号'
		});
		
		return columns;
	},
	/**
	 * 内部序号赋值
	 * @private
	 * @param {} records
	 * @return {}
	 */
	changeRecordsIndex:function(records){
		var me = this,
			checkedGrid = me.getGridByKey('checkedGrid'),
			count = checkedGrid.store.getCount(),
			length = records.length,
			innerIndex = 0;
			
		for(var i=0;i<length;i++){
			innerIndex = count + 1 + i;
			records[i].set('innerIndex',innerIndex + '');
			if(me.orderColumn){
				records[i].set(me.orderColumn,innerIndex + '');
			}
			records[i].commit();
		}
		return records;
	},
	/**
	 * 重新排序
	 * @private
	 */
	reorder:function(){
		var me = this,
			checkedGrid = me.getGridByKey('checkedGrid'),
			count = checkedGrid.store.getCount(),
			records = checkedGrid.store.data.items,
			length = records.length,
			list = [],
			index1,index2,temp,num;
			
		for(var m=0;m<length;m++){	
			list.push(m);
		}
		
		for(var i=0;i<length-1;i++){
			for(var j=i+1;j<length;j++){
				index1 = parseInt(records[j].innerIndex);
				index2 = parseInt(records[i].innerIndex);
				if(index1 < index2){
					temp = list[i];
					list[i] = list[j];
					list[j] = temp;
				}
			}
		}
		
		for(var i=0;i<length;i++){
			num = (i + 1) + '';
			records[list[i]].set('innerIndex',num);
			if(me.orderColumn){
				records[list[i]].set(me.orderColumn,num);
			}
			//records[list[i]].commit();
		}
	},
	/**
	 * 所有的待选变已选
	 * @private
	 * @param {} callback 处理数据
	 */
	selectAll:function(callback){
		var me = this,
			bo = me.checkedGridDock == 'left' ? true : false;
			
		if(Ext.typeOf(callback) == 'function'){
			me.checkedRecords(bo,true,callback);
		}else{
			me.checkedRecords(bo,true);
		}
	},
	/**
	 * 选中的待选变已选
	 * @private
	 * @param {} callback 处理数据
	 */
	selectChecked:function(callback){
		var me = this,
			bo = me.checkedGridDock == 'left' ? true : false;
			
		if(Ext.typeOf(callback) == 'function'){
			me.checkedRecords(bo,false,callback);
		}else{
			me.checkedRecords(bo,false);
		}
	},
	/**
	 * 所有的已选变待选
	 * @private
	 * @param {} callback 处理数据
	 */
	cancelAll:function(callback){
		var me = this,
			bo = me.checkedGridDock == 'left' ? false : true;
		
		if(Ext.typeOf(callback) == 'function'){
			me.checkedRecords(bo,true,callback);
		}else{
			me.checkedRecords(bo,true);
		}
	},
	/**
	 * 选中的已选变待选
	 * @private
	 * @param {} callback 处理数据
	 */
	cancelChecked:function(callback){
		var me = this,
			bo = me.checkedGridDock == 'left' ? false : true;
		
		if(Ext.typeOf(callback) == 'function'){
			me.checkedRecords(bo,false,callback);
		}else{
			me.checkedRecords(bo,false);
		}
	},
	/**
	 * 是否需要修改
	 * @private
	 * @param {} dRecord
	 * @param {} record
	 * @return {}
	 */
	hasToEdit:function(dRecord,record){
		var me = this,
			order1 = dRecord.get(me.orderColumn),
			order2 = record.get(me.orderColumn);
			
		var bo = order1 != order2;
		return bo;
	},
	/**
	 * 获取查询框组件
	 * @private
	 * @param {} name
	 * @return {}
	 */
	getSearchField:function(name){
		var me = this,
			searchField = me.searchField;
			
		//查询框信息
		if(!searchField)
			return null;
			
		//本列表查询框信息
		var info = searchField[name];
		if(!info)
			return null;
			
		//需要过滤的列	
		var searchColumns = info.searchColumns;
		if(searchColumns.length == 0)
			return null;
			
		//查询框组件
		var search = {
			xtype:'textfield',itemId:'searchText',width:'100%',
            emptyText:info.emptyText,
            listeners:{
            	change:function(field,newValue){
            		var grid = me.getGridByKey(name);
            		grid.store.filterBy(function(record,id){
						for(var i in searchColumns){
							var text = record.get(searchColumns[i]);
							if(text.indexOf(newValue)!=-1)
								return true;
						}
                		return false;
                	});
            	}
            }
		};
			
		return search;
	},
	/**
	 * 加载数据
	 * @public
	 * @param {} [checkedUrl] 已选列表的url,非空字符串时,替换原有的已选列表url
	 * @param {} [uncheckedUrl] 待选列表的url,非空字符串时,替换原有的待选列表url
	 */
	load:function(checkedUrl,uncheckedUrl){
		var me = this;
			checkedGridStore = me.getGridByKey('checkedGrid').store,
			uncheckedGridStore = me.getGridByKey('uncheckedGrid').store;
			
		if(Ext.typeOf(checkedUrl) == 'string' && checkedUrl != '')
			checkedGridStore.proxy.url = checkedUrl;
			
		if(Ext.typeOf(uncheckedUrl) == 'string' && uncheckedUrl != '')
			uncheckedGridStore.proxy.url = uncheckedUrl;
		
		me.disableControl();//禁用操作
		me.openMask(true);//显示遮罩层
		
		checkedGridStore.sorters.clear();
		
		var info = me.getOrderColumnInfo();
		if(info){
			var sorter = new Ext.util.Sorter({
				property:info.checkedGrid,
				direction:'ASC',
				root:'data'
			});
			checkedGridStore.sorters.add(sorter);
		}
		
		checkedGridStore.load();
	}
});