Ext.ns('Ext.iqc');
Ext.define('Ext.iqc.outofcontrol.List',{
	extend:'Ext.grid.Panel',
	alias:'widget.iqcoutofcontrollist',
	requires: ['Ext.iqc.YearMonthComponent'],
	
	title:'失控处理',
	/**获取质控项目列表服务地址*/
	searchUrl:getRootPath()+'/QCService.svc/QC_UDTO_SearchQCDValueByHQL',
	/**数据列信息*/
	columnFields:[
		{dataIndex:'QCDValue_QCItem_QCMat_CName',text:'质控物'},
		{dataIndex:'QCDValue_QCItem_QCMat_EPBEquip_CName',text:'仪器'},
		{dataIndex:'QCDValue_QCItem_ItemAllItem_CName',text:'项目'},
		{dataIndex:'QCDValue_ReceiveTime',text:'接收日期',width:90,xtype :'datecolumn',format:'Y-m-d'},
		{dataIndex:'QCDValue_ReceiveTime',text:'接收时间',width:80,xtype :'datecolumn',format:'H:i:s'},
		
		{dataIndex:'QCDValue_OriglValue',text:'原始数据',width:60},
		{dataIndex:'QCDValue_ReportValue',text:'用户数据',width:60},
		{dataIndex:'QCDValue_IsUse',text:'使用',width:40},
		
		{dataIndex:'QCDValue_IsControl',text:'失控',width:60},
		{dataIndex:'QCDValue_QCControlInfo',text:'失控规则'},
		
		{dataIndex:'QCDValue_QCDLoseValue_LoseReason',text:'失控原因分析',sortable:false},
		{dataIndex:'QCDValue_QCDLoseValue_CorrectMeasure',text:'纠正处理措施',sortable:false},
		
		{dataIndex:'QCDValue_Id',text:'主键ID',hidden:true,hideable:false},
		{dataIndex:'QCDValue_QCDLoseValue_Id',text:'失控处理主键ID',hidden:true,hideable:false}
	],
	/**主键字段*/
	keyDataIndex:'QCDValue_Id',
	/**默认排序字段*/
	defaultSorters:[
		{property:'QCDValue_QCItem_QCMat_CName',direction:'ASC'},
		{property:'QCDValue_QCItem_QCMat_EPBEquip_CName',direction:'ASC'},
		{property:'QCDValue_QCItem_ItemAllItem_CName',direction:'ASC'},
		{property:'QCDValue_ReceiveTime',direction:'ASC'}
	],
	/**默认加载数据*/
	defaultLoad:true,
	/**默认条件-只显示失控的数据*/
	defaultWhere:'qcdvalue.IsControl=3',
	/**内部条件*/
	internalWhere:'',
	/**外部条件*/
	externalWhere:'',
	
	/**最后一次合法的开始时间*/
	lastValidStartDate:null,
	/**最后一次合法的结束时间*/
	lastValidEndDate:null,
	
	/**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
        var me = this;
        me.callParent(arguments);
        //初始化监听
		me.initListeners();
		//默认加载数据
		if(me.defaultLoad){
			me.load(null,true);
		}else{
			me.disableControl();//禁用所有的操作功能 
		}
    },
	/**
	 * 初始化面板信息
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.initDate();//初始化质控数据时间
		me.store = me.createStore();//创建数据集
		me.columns = me.createColumns();//创建数据列内容
		me.dockedItems = me.createDockedItems();//创建挂靠功能
		me.callParent(arguments);
	},
	/**
	 * 初始化质控数据时间
	 * @private
	 */
	initDate:function(){
		var me = this,
			systemTime = getSystemTime();//系统时间
			
		me.value = systemTime;
		
		me.lastValidStartDate = getCurrentMonthFirst(me.value);//最后一次合法的开始时间
		me.lastValidEndDate = me.value;//最后一次合法的结束时间
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getStoreFields(),
			sorters:me.defaultSorters,
			remoteSort:false,
			pageSize:1000,
			proxy:{
				type:'ajax',
				url:'',
				reader:{type:'json',totalProperty:'count',root:'list'},
				//内部数据匹配方法
	            extractResponseData:function(response){
			    	return me.changeData(response); 
			  	}
			}
		});
		return store;
	},
	/**
	 * 获取数据集需要的字段
	 * @private
	 * @return {}
	 */
	getStoreFields:function(){
		var me = this;
		var fields = [];
		for(var i in me.columnFields){
			var dataIndex = me.columnFields[i].dataIndex;
			fields.push(dataIndex);
		}
		return fields;
	},
	/**
	 * 数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeData:function(response){
		var me = this;
		var data = Ext.JSON.decode(response.responseText);
		var success = (data.success + '' == 'true' ? true : false);
    	if(!success){
    		me.showError(data.ErrorInfo);
    	}
    	if(data.ResultDataValue && data.ResultDataValue != ''){
    		data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,'');
    		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	    	data.list = ResultDataValue.list;
	    	data.count = ResultDataValue.count;
    	}else{
    		data.list = [];
    		data.count = 0;
    	}
    	
		response.responseText = Ext.JSON.encode(data);
		return response;
	},
	/**
	 * 显示错误信息
	 * @private
	 * @param {} value
	 */
	showError:function(value){
		var me = this;
		var html = "<center><b style='color:red;font-size:x-large'>" + value + "</b></center>";
		me.getView().update(html);
	},
	/**
	 * 创建数据列内容
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this,
			list = me.columnFields,
			columns = [];
		
		for(var i in list){
			var con = list[i];
			con.doSort = function(state) {
		        var ds = this.up('tablepanel').store;
		        me.defaultLoad && ds.sort({
		            property: this.getSortParam(),
		            direction: state
		        });
		    };
			columns.push(con);
		}
		
		return columns;
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		//顶部功能按钮栏
		var toptoolbar = {
			xtype:'toolbar',dock:'top',itemId:'toptoolbar',
			items:[{
				itemId:'refresh',text:'刷新',iconCls:'build-button-refresh',
				handler:function(but){me.load(null,true);}
			},'-',{
				xtype:'combo',width:80,itemId:'dataType',value:'1',
				displayField:'text',valueField:'value',
				store:new Ext.data.Store({
					fields:['text','value'],
					data:[
						{text:'所有',value:'1'},
						{text:'已处理',value:'2'},
						{text:'未处理',value:'3'}
					]
				})
			},'-',{
				xtype:'datefield',fieldLabel:'质控日期',itemId:'start',
				labelWidth:60,width:160,format:'Y-m-d',
				value:me.lastValidStartDate,maxValue:me.lastValidEndDate
			},{
				xtype:'datefield',fieldLabel:'-',labelSeparator:'',itemId:'end',
				labelWidth:10,width:110,format:'Y-m-d',minValue:me.lastValidStartDate,
				value:me.lastValidEndDate,maxValue:me.lastValidEndDate
			}]
		};
		
		var dockedItems = [toptoolbar];
		return dockedItems;
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			dataType = toptoolbar.getComponent('dataType'),
			start = toptoolbar.getComponent('start'),
			end = toptoolbar.getComponent('end');
			
		//数据集监听
		me.store.on({
		    beforeload:function(store,operation){me.beforeLoad(store,operation);},
		    load:function(store,records,successful,eOpts){me.afterLoad(store,records,successful,eOpts);}
		});
		
		//过滤数据监听
		dataType.on({
			select:function(combo,records){
				var v = combo.getValue();
				me.showData(v);
			}
		});
		
		//时间监听
		start.on({
			blur:function(com){
				var bo = com.isValid();
				if(!bo) 
					com.setValue(me.lastValidStartDate);
					
				var date = com.getValue();
				me.lastValidStartDate = date;
				end.setMinValue(date);//设置结束日期的最小值
				
				me.load(null,true);
			}
		});
		
		//时间监听
		end.on({
			blur:function(com){
				var bo = com.isValid();
				if(!bo) 
					com.setValue(me.lastValidEndDate);
					
				var date = com.getValue();
				me.lastValidEndDate = date;
				start.setMinValue(date);//设置结束日期的最小值
				
				me.load(null,true);
			}
		});
	},
	/**
	 * 显示已处理或未处理的数据
	 * @private
	 * @param type
	 */
	showData:function(type){
		var me = this,
			store = me.store,
			datatype = type || '1';
			
		store.clearFilter(true);
		store.filter({
			filterFn:function(item){
				if(datatype == '1')
					return true;
					
				var value = item.get("QCDValue_QCDLoseValue_Id");
				var bo = value && value != '';
					
				return datatype == '2' ? bo : !bo;
			}
		});
	},
	/**
  	 * 加载数据前
  	 * @private
  	 * @param {} store
  	 * @param {} operation
  	 */
  	beforeLoad:function(store,operation){
  		var me = this;
  		me.disableControl();//禁用 所有的操作功能
  	},
  	/**
  	 * 加载数据后
  	 * @private
  	 * @param {} store
  	 * @param {} records
  	 * @param {} successful
  	 * @param {} eOpts
  	 */
  	afterLoad:function(store,records,successful,eOpts){
  		var me = this;
  		me.enableControl();//启用所有的操作功能
  		
  		//加载数据后默认选中一行
		if(successful && records.length > 0){
			var num = -1;//需要选中的行号
			if(me.deleteIndex && me.deleteIndex != '' && me.deleteIndex != -1){
				//选中删除下标的那一行或者最后一行
				num = (records.length-1 > me.deleteIndex) ? me.deleteIndex : records.length-1;
			}else{
				if(me.autoSelect && me.autoSelect != ''){
					num = me.store.find(me.keyDataIndex,me.autoSelect);
				}
			}
			//还原参数
			me.deleteIndex=-1;
			//选中行号为num的数据行
			if(num >= 0){
				me.getSelectionModel().select(num);
			}else{
				if(me.store.getCount() > 0){
					me.getSelectionModel().select(0);
				}
			}
         }
         
         me.fireEvent('afterload',store,records,successful,eOpts);
  	},
  	/**
	 * 启用所有的操作功能
	 * @private
	 */
	enableControl:function(){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar');
		
		var items = toptoolbar.items.items;
		for(var i in items){
			items[i].enable();
		}
		me.defaultLoad = true;
	},
	/**
	 * 禁用所有的操作功能
	 * @private
	 */
	disableControl:function(){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar');
		
		var items = toptoolbar.items.items;
		for(var i in items){
			items[i].disable();
		}
		me.defaultLoad = false;
	},
	/**
	 * 获取列表服务地址
	 * @private
	 * @return {}
	 */
	getUrl:function(){
		var me = this,
			fields = me.getStoreFields();
			
		if(me.lastValidStartDate && me.lastValidEndDate){
			var start = getDateString(me.lastValidStartDate);
			var end = getDateString(getNextDate(me.lastValidEndDate));
			me.internalWhere = "(qcdvalue.ReceiveTime between '" + start + "' and '" + end + "')";
		}
			
		var w = '';
		//外部条件
        if(me.externalWhere && me.externalWhere != ''){
            w += me.externalWhere +' and ';
        }
        //默认条件
        if(me.defaultWhere && me.defaultWhere != ''){
            w += me.defaultWhere +' and ';
        }
        //内部条件
        if(me.internalWhere && me.internalWhere != ''){
            w += me.internalWhere + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
		
        var url = me.searchUrl + "?isPlanish=true&fields=" + fields.join(',') + "&where=" + encodeString(w);
		return url;
	},
	/**
	 * 加载数据
	 * @public
	 * @param {} where
	 * @param {} isPrivate
	 */
	load:function(where,isPrivate){
		var me = this;
		//外部调用:外部条件变化
		if(!isPrivate){me.externalWhere = where;}
		
		var url = me.getUrl();
        me.store.currentPage = 1;
        me.store.proxy.url = url;
        me.store.load();
	}
});