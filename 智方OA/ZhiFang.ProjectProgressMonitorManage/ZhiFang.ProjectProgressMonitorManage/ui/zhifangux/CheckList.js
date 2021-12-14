/**
 * 【必配参数】
 * valueField   列属性数组
 * primaryKey 主键
 * serverUrl 列表获取后台数据服务地址
 * isbutton 修改保存按钮显示隐藏
 * primaryKey 主键字段
 * isload  加载数据开关
 * 【可选参数】
 *  internalDockedItem
 *  internalFields 列表的model的Field(内部使用)
 *  FieldStr 列表前台显示的字段(内部使用)
 *  【公开的属性】
 *  counts:总记录行数
 *  isCheckColum:是否显示或隐藏复选框列
 * 【公开的方法】
 * setCheckedIds：用于给列表勾选，接收参数是一个匹配字段值数组；(勾选之前先取消所有的勾选)
 * getAllChecked：用于获取所有勾选的数据，返回records；
 * getAllChanged：用户获取所有改变的数据，返回records；
 * getAllChangedRecords:获取所有改变的数据，返回records(包含自定义复选框列)；
 * 【公开的事件】
 * okClick：确定按钮点击事件；
 */
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux/CheckColumn.js');
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.CheckList', {
    extend:'Ext.grid.Panel',
    alias:['widget.CheckList'],
    autoLoad:false,
    /***
     * 是否显示false或隐藏true复选框列
     * @type Boolean
     */
    isCheckColum:false,
    obj:'',
    //requires:['Ext.ux.CheckColumn' ]
    /***
     * 总记录行数
     * @type Number
     */
    counts:0,
    width:600,
    height:250,
    externalWhere:'',
    objectName:'RBACRole',
    layout:'fit',
    autoSelect:true,
    border:false,
    //列表的model的Field(内部使用)
    internalFields:[],
    /***
     * 外部的dockedItem
     * @type 
     */
    internalDockedItem:[],
    //数据列表值字段,可以是外面做好数据适配后传进来
    valueField:[],
    internalWhere:'',
    //列表的内部hql
    externalWhere:'',
    //列表前台显示的字段(内部使用)
    fieldStr:'',
    //列表获取后台数据服务地址
    serverUrl:'',
    //允许多选
    multiSelect:true,
   //定义变量，存放勾选的数组
    lastValue:[],
    //修改保存按钮显示隐藏
    isbutton:true,
    //主键字段
    primaryKey:'RBACRole_Id',
    keyColumn:'checkBoxColumn',
    isload:true,
//    //全选
//    isallcheck:true,
//    //全不选
//    isnotcheck:true,
    //勾选的数据
    /**
     * 创建列表对象数据源 
     */
    listStore:function(where) {
        var me = this;
        var myUrl = me.getUrl(where);
        //数据代理
        var proxy = me.createProxy(myUrl);
        var obj = {
            pageSize:10000,
            fields:me.internalFields,
            autoLoad:me.isload,
            proxy:proxy
        };
        
        //------------------------------------
        //Jcall 2016-07-27 修改，用于默认排序字段
        if(me.defaultOrderBy){
			obj.remoteSort = true;//后台排序
        	obj.sorters = me.defaultOrderBy;
        }
        //------------------------------------
        
        idProperty:me.primaryKey;
        var store = Ext.create('Ext.data.Store', obj);
        return store;
    },
    getUrl:function(where) {
        var me = this;
        me.setField();
        var myUrl = '';
        //前台需要显示的字段
        var fields = me.fieldStr;
        if (me.serverUrl == '') {
            Ext.Msg.alert('提示', '没有配置列表的获取数据的服务！');
        }
        if (!fields) {
            fields = '';
        }
        myUrl = me.serverUrl + '?isPlanish=true&fields=' + fields;
        //服务地址
        me.externalWhere = where;
        var w = '';
        if (me.internalWhere) {
            w += me.internalWhere;
        }
        if (where && where != '') {
            if (w != '') {
                w += ' and ' + where;
            } else {
                w += where;
            }
        }
        myUrl = myUrl + '&where=' + w;
        return myUrl;
    },
    /**
 	 * 创建服务代理
 	 * @private
 	 * @param {} url
 	 * @return {}
 	 */
    createProxy:function(url) {
        var me=this;
        var proxy = {
            type:'ajax',
            url:url,
            reader:{
                type:'json',
                root:'list',
                totalProperty:'count'
            },
            extractResponseData:function(response) {
                var data = Ext.JSON.decode(response.responseText);
                me.counts=0;
                if (data.ResultDataValue && data.ResultDataValue != '') {
                	data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.count = ResultDataValue.count;
                    me.counts=ResultDataValue.count;
                    data.list = ResultDataValue.list;
                } else {
                    data.count = 0;
                    me.counts=0;
                    data.list = [];
                }
                response.responseText = Ext.JSON.encode(data);
                return response;
            }
        };
        return proxy;
    },
    load:function(where) {
        var me = this;
        var myUrl=me.getUrl(where);
        me.store.proxy.url=myUrl; 
        me.store.load();
    },
    //传参
    loadobj:function(obj,where) {
        var me = this;
        var myUrl=me.getUrlobj(obj,where);
        me.store.proxy.url=myUrl; 
        me.store.load();
    },
    //传参
    getUrlobj:function(obj,where) {
        var me = this;
        me.setField();
        var myUrl = '';
        //前台需要显示的字段
        var fields = me.fieldStr;
        if (me.serverUrl == '') {
            Ext.Msg.alert('提示', '没有配置列表的获取数据的服务！');
        }
        if (!fields) {
            fields = '';
        }
        myUrl = me.serverUrl + '?isPlanish=true&fields=' + fields+obj;
       
        //服务地址
        me.externalWhere = where;
        var w = '';
        if (me.internalWhere) {
            w += me.internalWhere;
        }
        if (where && where != '') {
            if (w != '') {
                w += ' and ' + where;
            } else {
                w += where;
            }
        }
        myUrl = myUrl + '&where=' + w;
        return myUrl;
    },
    
    /**
     * 解析右列表处理传列表值字段,
     * 封装store数据的Field数组
     * 
     */
    setField:function() {
        var me = this;
        me.valueField = [];
        me.valueField = me.columns;
        if (me.valueField.length > 0) {
            me.fieldStr = '';
            me.internalFields = [];
            me.internalFields.push(me.keyColumn);
            for (var i = 0; i < me.valueField.length; i++) {
                var test = me.valueField[i].dataIndex;
                if (i == me.valueField.length - 1) {
                    me.fieldStr = me.fieldStr + test;
                } else {
                    me.fieldStr = me.fieldStr + (test + ',');
                }
                me.internalFields.push(test);
            }
        }
    },
    /**
	 * 初始化属性
	 * @private
	 */
    initComponent:function() {
        var me = this;
        var checkcolum = [];
        checkcolum.push(me.createcheckcolum());
        me.columns = checkcolum.concat(me.valueField);
        me.store = me.listStore('');
        me.createdockedItems();
        //注册事件
        me.addEvents('okClick');
        me.addAppEvents();
        
        me.setCount = function(count) {
            var me = this;
            var com = me.getComponent("toolbar-bottom").getComponent("count");
            var str = "共" + count + "条";
            com.setText(str, false);
        };
        
        
        me.callParent(arguments);
    },
    /**
	 * 创建工具栏
	 * @private
	 * @return {}
	 */
    createdockedItems:function() {
        var me = this;
        me.dockedItems='';
        //内部的dockedItems
        var idockedItemsArr = [];
        idockedItemsArr.push(me.createtopItems());
        
        var arrTops=[];//dockedItems的顶部所有工具栏
        var arrBottoms=[];//dockedItems的顶部所有工具栏
        //内部的dockedItems
        for (var i in idockedItemsArr) {
            var obj=idockedItemsArr[i]; 
            var dock = obj.dock;//工具栏位置
            if (dock == 'top') {
                arrTops.push(obj);
            }else if (dock == 'bottom') {
                arrBottoms.push(obj);
            }
         }
        
        //外部的dockedItems
        var externalDockedItem = me.internalDockedItem;
        if(externalDockedItem&&externalDockedItem!=null){
	        for (var j in externalDockedItem) {
                var obj=externalDockedItem[j]; 
	            var dock = obj.dock;//工具栏位置
                if (dock == 'top') {
                    arrTops.push(obj);
                }else if (dock == 'bottom') {
                    arrBottoms.push(obj);
                }
	        }
        }
        
        if(arrBottoms.length>0&&arrTops.length>0){
            me.dockedItems = [ {
                xtype:'toolbar',
                dock:'top',
                itemId:'toolbar',
                border:false,
                items:arrTops
            }, {
                xtype:'toolbar',
                dock:'bottom',
                itemId:'toolbarbottom',
                border:false,
                items:arrBottoms
            } ];
        }else if(arrBottoms.length==0&&arrTops.length>0){
            me.dockedItems = [ {
                xtype:'toolbar',
                dock:'top',
                itemId:'toolbar',
                border:false,
                items:arrTops
           }];
        }else if(arrBottoms.length>0&&arrTops.length==0){
            me.dockedItems = [ {
                xtype:'toolbar',
                dock:'bottom',
                itemId:'toolbarbottom',
                border:false,
                items:arrBottoms
           }];
        }
    },
    /**
	 * 创建复选列
	 * @private
	 * @return {}
	 */
    createcheckcolum:function() {
        var me = this;
        var com = {
            text:'选择',
            align:'center',
            dataIndex:me.keyColumn,
            hidden:me.isCheckColum,
            xtype:'checkcolumn',
            width:60,
            editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },
            listeners:{
            	checkchange:function() {
            	    //me.getSelectionModel().select(0); 
                }
            }
        };
        return com;
    },
    /**
	 * 创建全选、全不选、确定按钮
	 * @private
	 * @return {}
	 */
    createtopItems:function() {
        var me = this;
        var btnItems=[];
        btnItems.push(me.checkAllitem());
        btnItems.push(me.checkdelitem());
        if (me.isbutton==true){
        	btnItems.push(me.okClickitem());
        }
        var com = {
            xtype:'toolbar',
            dock:'top',
            itemId:'toolbar',
            items:btnItems
        };
        return com;
    },
    /**
	 * 修改保存按钮 
	 * @private
	 * @return {}
	 */
    okClickitem:function(){
    	var me=this;
    	var com={
			xtype:'button',
            text:'修改保存',
            itemId:'buttonsave',
            iconCls:'build-button-save',
            handler:function(but, e) {
                me.fireEvent('okClick');
            }
        };
       return com;
    },
    /**
	 * 全选按钮 
	 * @private
	 * @return {}
	 */
    checkAllitem:function(){
    	var me=this;
    	var com={
            xtype:'button',
            text:'全选',
            itemId:'checkAll',
            iconCls:'build-button-unchecked',
            handler:function(but, e) {
		        var delcheckAll = me.getComponent('toolbar').getComponent('toolbar').getComponent('delcheckAll');
    		    delcheckAll.setIconCls('build-button-unchecked');
    		    but.setIconCls('build-button-checked');
    		    me.store.each(function(record) {
                    record.set(me.keyColumn, true);
                });
            }
        };
       return com;
    },
    /**
	 * 全否按钮 
	 * @private
	 * @return {}
	 */
    checkdelitem:function(){
    	var me=this;
    	var com={
		    xtype:'button',
            text:'全不选',
            itemId:'delcheckAll',
            iconCls:'build-button-unchecked',
            handler:function(but, e) {
  		        var checkAll = me.getComponent('toolbar').getComponent('toolbar').getComponent('checkAll');
    		    checkAll.setIconCls('build-button-unchecked');
     		    but.setIconCls('build-button-checked');
     		    me.store.each(function(record) {
	                record.set(me.keyColumn, false);
	            });
            }
        };
       return com;
    },
    /**
	 * 用于获取所有勾选的数据，返回records
	 * @private
	 * @return {}
	 */
    getAllChecked:function() {
        var me = this;
        var lastValue = [];
        me.store.each(function(record) {
        	if(record.get(me.keyColumn)==true && record.dirty == true ){
        		lastValue.push(record.raw);
        	}
        });
        return lastValue;
    },
    /**
	 * 用户获取所有改变的数据，返回records；
	 * @private
	 * @return {}
	 */
    getAllChanged:function() {
        var me = this;
        var allChangedValue = [];
        me.store.each(function(record) {
        	if (record.dirty == true) {
                allChangedValue.push(record.raw);
        	}
        });
        return allChangedValue;
    },
    /**
     * 获取所有改变的数据，返回records,包含自定义复选框列；
     * @private
     * @return {}
     */
    getAllChangedRecords:function() {
        var me = this;
        var allChangedValue = [];
        me.store.each(function(record) {
            if (record.dirty == true) {
                allChangedValue.push(record.data);
            }
        });
        return allChangedValue;
    },
    /**
	 * 用于给列表勾选，接收参数是一个匹配字段值数组；
	 * (勾选之前先取消所有的勾选)
	 * @public
	 * @return {}
	*/
	setCheckedIds:function(arr) {
		var me = this;
		//清空勾选
		 me.store.each(function(record) {
			 record.set(me.keyColumn,false);
		     record.commit();
        });
		//勾选还原
        if(arr&&arr!=null){
            var result=Ext.isArray(arr);//为数组时才处理
            if(result){
				Ext.Array.each(arr,function(model){
					var field=me.keyColumn;
					var value=model[me.primaryKey];
					var record = me.store.findRecord(me.primaryKey,value);
					if (record){
						record.set(me.keyColumn,true);
						record.commit();
				    }
				});
            }
        }
	},
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function() {
        var me = this;
        me.addEvents('okClick');
    }
});