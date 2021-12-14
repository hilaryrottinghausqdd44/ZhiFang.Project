Ext.ns('Ext.iqc');
Ext.define('Ext.iqc.config.QCItemCheck',{
	extend:'Ext.panel.Panel',
	alias:'widget.iqcqcitemcheck',
	
	title:'质控项目设置',
	width:600,
	height:400,
	/**加载次数*/
	loadcount:0,
	/**初始已选数据*/
	defaultSelectedItems:null,
	/**处理的总条数*/
	resultCount:0,
	/**当前处理的条数*/
	nowCount:0,
	/**错误信息数组*/
	ErrorInfo:[],
	/**待选项目服务地址*///仪器项目关系表
	unSelectedItemUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchEPEquipItemByHQL',
	/**已选项目服务地址*///质控项目表
	selectedItemUrl:getRootPath()+'/QCService.svc/QC_UDTO_SearchQCItemByHQL',
	/**新增质控项目*/
	addQCItemUrl:getRootPath()+'/QCService.svc/QC_UDTO_AddQCItem',
	/**删除质控项目*/
	delQCItemUrl:getRootPath()+'/QCService.svc/QC_UDTO_DelQCItem',
	/**列表的属性字段*/
	fields:['QCItem_Id','ItemAllItem_Id','ItemAllItem_CName','ItemAllItem_SName','ItemAllItem_DataTimeStamp'],
	/**列表数据列*/
	columns:[
		{dataIndex:'QCItem_Id',text:'质控项目ID',hidden:true,hideable:false},
		{dataIndex:'ItemAllItem_Id',text:'项目ID',hidden:true,hideable:false},
		{dataIndex:'ItemAllItem_DataTimeStamp',text:'项目时间戳',hidden:true,hideable:false},
		{dataIndex:'ItemAllItem_CName',text:'项目名称'},
		{dataIndex:'ItemAllItem_SName',text:'项目简称'}
	],
	/**枚举类*/
	Enum:Ext.create('Ext.iqc.Enum'),
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initListeners();//初始化监听
		me.load();//加载数据
	},
	/**
	 * 初始化面板信息
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = 'border';
		me.bodyPadding = 2;
		me.addEvents('save');//开启save监听
		me.items = me.createItems();//创建面板内容
		me.dockedItems = me.createDockedItems();//创建挂靠功能
		me.callParent(arguments);
	},
	/**
	 * 创建面板内容
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var unSelectedItemList = me.createUnSelectedItemList();//待选项目列表
		var selectedItemList = me.createSelectedItemList();//已选项目列表
		
		selectedItemList.region = 'center';
		unSelectedItemList.region = 'east';
		unSelectedItemList.width = '50%';
		unSelectedItemList.collapsible = true;
		unSelectedItemList.split = true;
		
		var items = [unSelectedItemList,selectedItemList];
		return items;
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		//顶部功能按钮栏
		var bottomtoolbar = {
			xtype:'toolbar',dock:'bottom',itemId:'bottomtoolbar',
			items:['->',{
				itemId:'add',text:'保存',iconCls:'build-button-save',
				handler:function(but){me.save(but);}
			},{
				itemId:'refresh',text:'刷新',iconCls:'build-button-refresh',
				handler:function(but){me.load();}
			}]
		};
		
		var dockedItems = [bottomtoolbar];
		return dockedItems;
	},
	/**
	 * 创建待选项目列表
	 * @private
	 * @return {} 
	 */
	createUnSelectedItemList:function(){
		var me = this;
		//需要加载的数据：项目时间戳、项目名称、项目简称
		var fields = "EPEquipItem_ItemAllItem_Id,EPEquipItem_ItemAllItem_DataTimeStamp," +
				"EPEquipItem_ItemAllItem_CName,EPEquipItem_ItemAllItem_SName",
			where = "epequipitem.EPBEquip.Id=" + me.QCMat.EPBEquip_Id,
			url = me.unSelectedItemUrl + "?isPlanish=true&fields=" + fields + "&where=" + where;
		
		var list = {
			xtype:'grid',multiSelect:true,stripeRows:true,title:'待选项目',
			columns:me.columns,itemId:'unSelectedItemList',
			store:Ext.create('Ext.data.Store',{
		        fields:me.fields,
		        pageSize:1000,
				proxy:{
					type:'ajax',url:url,
					reader:{type:'json',totalProperty:'count',root:'list'},
					processResponse: function(success, operation, request, response, callback, scope) {
						//过滤已选项目的数据集
			    		response = me.changeUnSelectedItemListData(response);
				        var reader,
				            result;
				        if (success === true) {
				            reader = this.getReader();
				            reader.applyDefaults = operation.action === 'read';
				            result = reader.read(this.extractResponseData(response));
				            if (result.success !== false) {
				                Ext.apply(operation, {
				                    response: response,
				                    resultSet: result
				                });
				                operation.commitRecords(result.records);
				                operation.setCompleted();
				                operation.setSuccessful();
				            } else {
				                operation.setException(result.message);
				                this.fireEvent('exception', this, response, operation);
				            }
				        } else {
				            this.setException(operation, response);
				            this.fireEvent('exception', this, response, operation);
				        }
				        if (typeof callback == 'function') {
				            callback.call(scope || this, operation);
				        }
				        this.afterRequest(request, success);
				    }
				}
		    }),
	        viewConfig:{
	        	loadMask:false,
	            plugins:{
	            	ptype:'gridviewdragdrop',
	            	dragGroup:'unSelectedItemList',
	            	dropGroup:'selectedItemList',
	            	dragText:'{0}行选中'
	            },
	            listeners:{
	            	drop:function(node,data,dropRec,dropPosition){
	            		me.afterDrop(node,data,dropRec,dropPosition);
	            	}
	            }
	        },
	        //拖动排序需要配置 enableDragDrop 属性 和 dropConfig属性  
	        enableDragDrop:true,
	        dropConfig:{appendOnly:true}
		};
		return list;
	},
	/**
	 * 创建已选项目列表
	 * @private
	 * @return {}
	 */
	createSelectedItemList:function(){
		var me = this;
		//需要加载的数据：质控项目ID、项目ID、项目名称、项目简称
		var fields = "QCItem_Id,QCItem_ItemAllItem_Id,QCItem_ItemAllItem_CName,QCItem_ItemAllItem_SName",
			where = "qcitem.QCMat.Id=" + me.QCMat.Id,
			url = me.selectedItemUrl + "?isPlanish=true&fields=" + fields + "&where=" + where;
		
		var list = {
			xtype:'grid',multiSelect:true,stripeRows:true,title:'已选项目',
			columns:me.columns,itemId:'selectedItemList',
			store:Ext.create('Ext.data.Store',{
		        fields:me.fields,
		        pageSize:1000,
				proxy:{
					type:'ajax',url:url,
					reader:{type:'json',totalProperty:'count',root:'list'},
					//内部数据匹配方法
		            extractResponseData:function(response,callback){
				    	return me.changeSelectedItemListData(response,callback); 
				  	}
				}
		    }),
	        viewConfig:{
	        	loadMask:false,
	            plugins:{
	            	ptype:'gridviewdragdrop',
	            	dragGroup:'selectedItemList',
	            	dropGroup:'unSelectedItemList',
	            	dragText:'{0}行选中'
	            },
	            listeners:{
	            	drop:function(node,data,dropRec,dropPosition){
	            		me.afterDrop(node,data,dropRec,dropPosition);
	            	}
	            }
	        },
	        //拖动排序需要配置 enableDragDrop 属性 和 dropConfig属性  
	        enableDragDrop:true,
	        dropConfig:{appendOnly:true}
		};
		return list;
	},
	/**
	 * 初始化监听
	 * @private
	 * @return {}
	 */
	initListeners:function(){
		var me = this;
		
		//待选项目监听
		var unSelectedItemList = me.getComponent('unSelectedItemList');
		unSelectedItemList.store.on({
			load:function(store,records,successful,eOpts){
				me.allLoad();
			}
		});
		
		//已选项目监听
		var selectedItemList = me.getComponent('selectedItemList');
		selectedItemList.store.on({
			load:function(store,records,successful,eOpts){
				me.defaultSelectedItems = Ext.clone(records);
				me.allLoad();
				unSelectedItemList.store.sorters.clear();
				unSelectedItemList.store.load();
			}
		});
	},
	/**
	 * 保存修改的数据
	 * @private
	 */
	save:function(){
		var me = this,
			defaultSelectedItems = me.defaultSelectedItems,
			store = me.getComponent('selectedItemList').store;
			
		var add = [],del = [];//需要新增和需要删除的质控项目
			
		for(var i in defaultSelectedItems){
			var id = defaultSelectedItems[i].get('QCItem_Id');
			var record = store.findRecord('QCItem_Id',id);
			if(!record){//需要删除的质控项目
				del.push(defaultSelectedItems[i].get('QCItem_Id'));
			}
		}
		
		store.each(function(rec){
			var id = rec.get('QCItem_Id');
			if(!id || id == ''){
				add.push(rec);//需要新增的质控项目
			}
		});
		
		me.resultCount = add.length + del.length;//处理的总条数
		if(me.resultCount == 0){
			alertInfo("没有做任何修改不需要保存数据！");
		}else{
			me.disableControl();//禁用的操作所有功能
			me.openMask(true,'数据保存中...');
			//新增质控项目
			for(var i in add){
				me.addQCItem(add[i],function(text){me.showResult(text);});
			}
			//删除质控项目
			for(var i in del){
				me.delQCItem(del[i],function(text){me.showResult(text);});
			}
		}
	},
	/**
	 * 新增一条质控项目
	 * @private
	 * @param {} record
	 * @param {} callback
	 */
	addQCItem:function(record,callback){
		var me = this;
			
		var QCItem = {
			ValueType:me.Enum.QCValueType[0].key,
			QCMat:{
				Id:me.QCMat.Id,
				DataTimeStamp:me.QCMat.DataTimeStamp.split(',')
			},
			ItemAllItem:{
				Id:record.get('ItemAllItem_Id'),
				DataTimeStamp:record.get('ItemAllItem_DataTimeStamp').split(',')
			}
		};
		
		var params = Ext.JSON.encode({entity:QCItem});
		postToServer(me.addQCItemUrl,params,callback);
	},
	/**
	 * 删除一条质控项目
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	delQCItem:function(id,callback){
		var me = this,
			url = me.delQCItemUrl + "?id=" + id;
		getToServer(url,callback);
	},
	/**
	 * 加载数据
	 * @private
	 */
	load:function(){
		var me = this;
		me.disableControl();//禁用操作
		me.openMask(true);//显示遮罩层
		//me.getComponent('unSelectedItemList').store.load();
		var store = me.getComponent('selectedItemList').store;
		store.sorters.clear();
		store.load();
	},
	/**
	 * 显示错误信息
	 * @private
	 * @param {} value
	 */
	showError:function(panel,value){
		var html = "<center><b style='color:red;font-size:x-large'>" + value + "</b></center>";
		panel.getView().update(html);
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
	 * 每个列表加载完毕后都需要调用的方法
	 * @private
	 */
	allLoad:function(){
		var me = this;
		me.loadcount++;
		if(me.loadcount == 2){
			me.loadcount = 0;//清零
			me.openMask(false);//隐藏遮罩层
			me.enableControl();//启用操作
		}
	},
	/**
	 * 待选项目数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeUnSelectedItemListData:function(response){
		var me = this;
		var data = Ext.JSON.decode(response.responseText);
		var success = (data.success + '' == 'true' ? true : false);
    	if(!success){
    		var panel = me.getComponent('unSelectedItemList');
    		me.showError(panel,data.ErrorInfo);
    	}else{
    		//已选项目的数据集
			var store = me.getComponent('selectedItemList').store;
			var data = Ext.JSON.decode(response.responseText);
			//处理数据,过滤掉已选项目
    		data = me.changeData(data);
    		for(var i in data.list){
    			var record = store.findRecord('ItemAllItem_Id',data.list[i]['EPEquipItem_ItemAllItem_Id']);
    			if(record){
    				delete data.list[i];
    				data.count--;
    			}else{
    				data.list[i]['ItemAllItem_Id'] = data.list[i]['EPEquipItem_ItemAllItem_Id'];
	    			data.list[i]['ItemAllItem_DataTimeStamp'] = data.list[i]['EPEquipItem_ItemAllItem_DataTimeStamp'];
	    			data.list[i]['ItemAllItem_CName'] = data.list[i]['EPEquipItem_ItemAllItem_CName'];
	    			data.list[i]['ItemAllItem_SName'] = data.list[i]['EPEquipItem_ItemAllItem_SName'];
    			}
    		}
    		//删除数组中没有属性的对象
    		var list = [];
    		for(var i in data.list){
    			if(data.list[i]){
    				list.push(data.list[i]);
    			}
    		}
    		data.list = list;
    	}
    	
		response.responseText = Ext.JSON.encode(data);
		return response;
	},
	/**
	 * 已选项目数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeSelectedItemListData:function(response){
		var me = this;
		var data = Ext.JSON.decode(response.responseText);
		var success = (data.success + '' == 'true' ? true : false);
    	if(!success){
    		var panel = me.getComponent('selectedItemList');
    		me.showError(panel,data.ErrorInfo);
    	}else{
    		data = me.changeData(data);
    		//处理数据
    		for(var i in data.list){
    			data.list[i]['ItemAllItem_Id'] = data.list[i]['QCItem_ItemAllItem_Id'];
    			data.list[i]['ItemAllItem_CName'] = data.list[i]['QCItem_ItemAllItem_CName'];
    			data.list[i]['ItemAllItem_SName'] = data.list[i]['QCItem_ItemAllItem_SName'];
    		}
    	}
    	
		response.responseText = Ext.JSON.encode(data);
		return response;
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
	 * 移动后处理
	 * @private
	 * @param {} node
	 * @param {} data
	 * @param {} dropRec
	 * @param {} dropPosition
	 */
	afterDrop:function(node,data,dropRec,dropPosition){
		
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
			me.nowCount = 0;//当期处理条数清零
			if(me.ErrorInfo.length > 0){
				alertError(me.ErrorInfo.join('</br>'));//错误信息提示
				me.ErrorInfo = [];//信息清空
				me.load();
			}else{//保存成功
				me.fireEvent('save');
			}
		}
	}
});