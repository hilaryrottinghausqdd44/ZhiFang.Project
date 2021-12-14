//已录入项目
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.InputProject', {
    extend:'Ext.grid.Panel',
    alias:[ 'widget.inputproject' ],
    title:'已录入项目',
    autoLoad:false,
    externalWhere:'',
    layout:'fit',
    autoSelect:true,
    width:300,
    height:200,
//    border:false,
    internalWhere:'',
    //列表的内部hql
    externalWhere:'',
    //列表前台显示的字段(内部使用)
    fieldStr:[ 'MEPTOrderItem_ItemAllItem_Id', 'MEPTOrderItem_Id', 'MEPTOrderItem_ItemAllItem_CName', 'gridid', 'modeId', 'MEPTOrderItem_ItemAllItem_DataTimeStamp', 'MEPTOrderItem_DataTimeStamp'],
    itemrow:'',
    //列表获取后台数据服务地址
    serverUrl:getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTOrderItemByHQL?isPlanish=true&fields=MEPTOrderItem_ItemAllItem_DataTimeStamp,MEPTOrderItem_ItemAllItem_Id,MEPTOrderItem_Id,MEPTOrderItem_ItemAllItem_DataTimeStamp,MEPTOrderItem_ItemAllItem_CName',
    //主键字段
    primaryKey:'RBACRole_Id',
    isload:false,
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
            fields:me.fieldStr,
            autoLoad:me.isload,
            proxy:proxy
        };
        idProperty:me.primaryKey;
        var store = Ext.create('Ext.data.Store', obj);
        return store;
    },
    getUrl:function(where) {
        var me = this;
//        me.setField();
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
    /**
	 * 初始化属性
	 * @private
	 */
    initComponent:function() {
        var me = this;
        
        me.setAppItems();
        me.setCount = function(count) {
            var me = this;
            var com = me.getComponent('toolbar-bottom').getComponent('count');
            var str = '共' + count + '条';
            com.setText(str, false);
        };
        me.columns=me.createColumn();
        me.store=me.listStore('');
        me.addEvents('actionClick');
        me.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        //加载数据后默认选中第一行
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
        var button= me.createColumn();
        if(button)
            items.push(button);
    	me.items = items;
    },
    /**
     * 创建列
     * @private
     */
    createColumn:function() {
        var me = this;
        var com=[{
    		text:'项目编码',
	        dataIndex:'MEPTOrderItem_ItemAllItem_Id',
	        hidden:true
	    }, {
	        text:'项目主键',
	        dataIndex:'MEPTOrderItem_Id',
	        hidden:true
	    }, {
	        text:'医嘱项目',
	        dataIndex:'MEPTOrderItem_ItemAllItem_CName',
	        width:120
	    }, {
	        xtype:'actioncolumn',
	        width:50,
	        text:'操作',
	        items:[ {
	            iconCls:'build-button-delete',
	            tooltip:'删除',
	            itemId:'del',
	            handler:function(grid, rowIndex, colIndex) {
	                var rec = grid.getStore().getAt(rowIndex);
	                var gridid = rec.get('gridid');
	                var modeId = rec.get('modeId');
	                me.itemrow=rec.get('MEPTOrderItem_ItemAllItem_Id');
	                var gridsel = Ext.getCmp(gridid);
	                if (gridsel){
	            	   var store = gridsel.getStore();
	                    if (modeId != '') {
	                        var record = store.findRecord('MEPTDefaultItemMode_Id', modeId, 0, false, false, true);
	                    } else {
	                        var record = grid.getStore().findRecord('MEPTOrderItem_ItemAllItem_Id', rec.get('MEPTOrderItem_ItemAllItem_Id'), 0, false, false, true);
	                    }
	               }else{
	            	   var record = grid.getStore().findRecord('MEPTOrderItem_Id', rec.get('MEPTOrderItem_Id'), 0, false, false, true);
	               }
                   if (record) {
                       record.set('checked', false);
                       record.commit(false);
                       grid.getStore().remove(rec);
	                  
	               }
                   
                   me.fireEvent('actionClick');
	            }
	        
	        }]
	    }];
        return com;
    },
  
    /**
     *增加行记录的方法
     * @private 
     * @return {}
   */
    addTabItemData:function(addData) {
        var me = this;
        var itemNo = addData.MEPTOrderItem_ItemAllItem_Id;
        var store = me.store;
        var rec = store.findRecord('MEPTOrderItem_ItemAllItem_Id', itemNo, 0, false, false, true);
        if (!rec) {
            store.insert(store.getCount(), addData);
        }
    },
    /**
     *增加行记录的方法
     * @private 
     * @return {}
   */
    addModeItemData:function(id, gridId) {
        var me = this;
        var url = getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchModuleItemList?id=' + id + '&fields=MEPTDefaultItemModeRelation_ItemAllItem_CName,' + 'MEPTDefaultItemModeRelation_ItemAllItem_DataTimeStamp,' + 'MEPTDefaultItemModeRelation_ItemAllItem_Id&isPlanish=true';
        Ext.Ajax.request({
            async:false,
            //非异步
            url:url,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    if (result.ResultDataValue == '') {
                        return;
                    } else {
                        var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                        for (var i = 0; i < ResultDataValue.count; i++) {
                            var itemNo = ResultDataValue.list[i].MEPTDefaultItemModeRelation_ItemAllItem_Id;
                            var itemName = ResultDataValue.list[i].MEPTDefaultItemModeRelation_ItemAllItem_CName;
                            var DataTimeStamp = ResultDataValue.list[i].MEPTDefaultItemModeRelation_ItemAllItem_DataTimeStamp;
                            var addData = {
                                MEPTOrderItem_ItemAllItem_Id:itemNo,
                                MEPTOrderItem_ItemAllItem_CName:itemName,
                                gridid:gridId,
                                modeId:id,
                                MEPTOrderItem_ItemAllItem_DataTimeStamp:DataTimeStamp
                            };
                            var store = me.store;
                            var rec = store.findRecord('MEPTOrderItem_ItemAllItem_Id', itemNo, 0, false, false, true);
                            if (!rec) {
                                store.insert(store.getCount(), addData);
                            }
                        }
                    }
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '调用服务失败！' + response.responseText);
            }
        });
    },
    /**
     *删除行记录的方法
     * @private 
     * @return {}
     */
    delModeItemData:function(id) {
        var me = this;
        var count = me.getStore().getCount();
        for (var i = 0; i < count; i++) {
            var rec = me.store.findRecord('modeId', id, 0, false, false, true);
            if (rec) {
                me.store.remove(rec);
            }
        }
    },
    /**
     *获取列表的行记录
     * @private 
     * @return {}
     */
    getValue:function() {
        var me = this;
        var store = me.store;
        var ItemValue = [];
        store.data.each(function(item) {
            var itemNo = item.data.MEPTOrderItem_ItemAllItem_Id;
            var OrderItem_Id = item.data.MEPTOrderItem_Id;
//            var itemSName = item.data.MEPTOrderItem_ItemAllItem_SName;
            var itemName = item.data.MEPTOrderItem_ItemAllItem_CName;
            var DataTimeStamp = item.data.MEPTOrderItem_ItemAllItem_DataTimeStamp;
            var addData = {
	    		MEPTOrderItem_ItemAllItem_Id:itemNo,
	    		MEPTOrderItem_ItemAllItem_DataTimeStamp:DataTimeStamp
            };
            ItemValue.push(addData);
        });
        return ItemValue;
    },
    getValueID:function(){
    	 var me = this;
         var store = me.store;
         var ItemValue = [];
         store.data.each(function(item) {
             var itemNo = item.data.MEPTOrderItem_ItemAllItem_Id;
             var OrderItem_Id = item.data.MEPTOrderItem_Id;
//             var itemSName = item.data.MEPTOrderItem_ItemAllItem_SName;
             var itemName = item.data.MEPTOrderItem_ItemAllItem_CName;
             var DataTimeStamp = item.data.MEPTOrderItem_ItemAllItem_DataTimeStamp;
             var addData = {
 	    		'MEPTOrderItem_ItemAllItem_Id':itemNo
             };
             ItemValue.push(addData);
         });
         return ItemValue;
    }
});