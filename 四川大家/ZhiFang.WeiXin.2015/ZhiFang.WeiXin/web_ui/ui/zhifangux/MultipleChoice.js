//非构建类--通用型组件或控件--多项数据选择拖动组件
/**
 * 多项数据选择拖动组件
 * 主要解决如人员选择角色的应用问题，左边是已经选择的角色，右边是还可以选择的角色。
 * 
 * @param
 * SelectType：多选方式 0,为单选 1，为复选,默认为0  --测试用例未完成，配置成功
 * DataServerUrl：后台服务地址 --测试用例未完成，配置成功
 * SaveServerUrl：后台保存服务地址 --测试用例未完成，配置成功
 * SearchField：查询项匹配数据对象的属性（支持多个） 显支持多个（全列，该属性没实现） 
 * KeyField：主键字段,是控件扩展属性  --测试用例未完成，配置成功
 * ValueField：值字段 --测试用例未完成，配置成功
 * RootLeft：左表根节点 --测试用例未完成，配置成功
 * RootRight：右表根节点 --测试用例未完成，配置成功
 
 * 
 * 对外公开事件 
 * Ondrag 当拖拽移动后触发   --测试用例完成
 * OnClick 单击列表记录时触发  --测试用例完成
 * OnMove 移动列表记录时触发 --测试用例完成
 * OnSave 保存时触发  --测试用例完成
 * OndbClick 双击列表记录时触发 --测试用例完成
 * 
 * 对外公开方法
 * SetSelectType 设置多选方式  --测试用例未完成
 * GetSelectType 获取多选方式 --测试用例完成
 * Save 保存列表对象  --测试用例完成
 * ReLoad 重载列表对象 --测试用例完成
 * SetCol 设置列表列   --测试用例未完成
 * GetCol 获取列表列   --测试用例完成
 * SetLeftList 设置左列表  ----测试用例未完成
 * GetLeftList 获取左列表  --测试用例完成
 * SetRightList 设置右列表  --测试用例未完成
 * GetRightList 获取右列表  --测试用例完成
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.MultipleChoice', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.multiplechoice',
    height: 411,
    width: 430,
    border: true,
    layout: 'border',
    bodyCls:'bg-white',
    cls:'bg-white',
    margin:4,
    frame:false,
    SimpleSelect:true,
    
    //公共属性
    SelectType:1,//多选方式
    DataServerUrl:'',//后台服务地址
    SaveServerUrl:'',//后台保存服务地址
    LeftListValue:[],//获取左列表
    RightListValue:[],//获取右列表
    SearchField:'L2',//查询项匹配数据对象的属性（支持多个）
    KeyField:'',//主键字段
    RootLeft:'ResultDataValue',
    RootRight:'ResultDataValue1',//只针对测试
    Field:[],
    ValueField:[],//数据列表值字段,可以是外面做好数据适配后传进来
    /**
     * 解析处理传列表值字段,
     * 封装store数据的Field数组
     * 
     */
     SetField: function () {
         var me = this;
         if (me.ValueField.length > 0) {
             for (var i = 0; i < me.ValueField.length; i++) {
                 var test = me.ValueField[i].dataIndex;
                 me.Field.push(test);
             }
         }
     },
     /**
      * 创建左列表对象数据源
      * 未实现数据项适配的功能
      * 
      */
     leftstore:function(){
         var me = this;
         var left=null;
         Ext.Ajax.request({
             async:false,//非异步
             url:me.DataServerUrl,
             method:'GET',
             timeout:5000,
             success:function(response,opts){
	             var result = Ext.JSON.decode(response.responseText);
	             if(result.success){
		             left=Ext.create('Ext.data.Store', {
			             fields:me.Field, //实现数据项适配的功能
			             data:result,
			             proxy: {
				             type: 'memory',
				             reader: {
					             type: 'json',
					             root:me.RootLeft// 'list'
	     	                 }
	     	             }
     	            });
                 }else{
                     Ext.Msg.alert('提示','获取列表数据信息失败！');
                 }
             },
             failure : function(response,options){ 
                     Ext.Msg.alert('提示','获取列表数据信息请求失败！');
             }
        });
        return left;
    },
        
        
    /**
     * 创建右列表对象数据源
     * 未实现数据项适配的功能
     * 
     */
    rightstore:function(){
        var me = this;
        var right=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.DataServerUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
	             var result = Ext.JSON.decode(response.responseText);
	             if(result.success){
		             right=Ext.create('Ext.data.Store', {
			             fields:me.Field, //实现数据项适配的功能
			             data:result,
			             proxy: {
				             type: 'memory',
				             reader: {
					             type: 'json',
					             root:me.RootRight// 'list'
	     	                 }
	     	             }
    	            });
                }else{
                    Ext.Msg.alert('提示','获取列表数据信息失败！');
                }
            },
            failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取列表数据信息请求失败！');
            }
       });
       return right;
   },
 
     //公共方法
     //设置多选方式
     SetSelectType: function (value) {
         var me = this;
         var y =me.getComponent('left');
         y.selType=value;
         return y.selType;
         var y1 =me.getComponent('right');
         y1.selType=value;
         return y1.selType;
         
     },

     //获取多选方式
     GetSelectType: function () {
         var me = this;
         var y =me.getComponent('left');
         if(me.SelectType==0)
 	     {
        	 var  selType='checkboxmodel';//复选框
             return me.selType;//复选框
 	     }
         else if(me.SelectType==1){
 	         me.selType='rowmodel';
 	         me.SimpleSelect=false;
 	         //alert(me.selType);
      	     return me.selType;//复选框
 	    }
     },
     //保存的对象
     Save:function(modified)
     {
    	 var t =this.getComponent('left');
   	     var modified =t.getStore().getRange(0,t.getStore().modified);
    	 var me = this;
    	 var json = []; 
         Ext.each(modified, function(item){ 
             json.push(item.data); 
         }); 
         if (json.length > 0){ 
             Ext.Ajax.request({ 
            	 async:false,//非异步
                 url: me.SaveServerUrl, 
                 params:{ data: Ext.JSON.encode(json) }, 
                 method: "POST", 
                 success: function(response){ 
                     Ext.Msg.alert("信息", "数据更新成功！", function(){
                    	 var t2 = me.getComponent('left');
                    	 t2.getStore().reload();
                    	 var t1 = me.getComponent('right');
                    	 t1.getStore().reload();
                     });
                     alert(Ext.JSON.encode(json));
                 }, 
                 failure: function(response){ 
                     Ext.Msg.alert("警告", "数据更新失败，请稍后再试！"); 
                 } 
             }); 
         } 
         else { 
             Ext.Msg.alert("警告", "没有任何需要更新的数据！"); 
         };
     },
     //重载列表对象
     ReLoad:function()
     { 
    	 var me = this;
    	 var t =this.getComponent('left').getStore();
    	 var t1 =this.getComponent('right').getStore();
    	 t.reload();
    	 t1.reload();
     },
     //设置列表列
     SetCol:function(value){
         var me = this;
         var t =this.getComponent('left');
    	 var t1 =this.getComponent('right');
         t.ValueField =value;
         t1.ValueField=value;
     },
     //获取列表列
     GetCol:function(){
    	 var me = this;
    	 var t =this.getComponent('left');
    	 t.ValueField =me.ValueField;
         return t.ValueField ;
         
    	 var t1 =this.getComponent('right');
    	 t1.ValueField =me.ValueField;
    	 return  t1.ValueField;
     },
     //设置左列表
     SetLeftList:function(value){
         var me = this;
    	 me.LeftListValue=value;
    	 return me.LeftListValue;
     },
     //获取左列表
     GetLeftList:function(){
    	 var me = this;
    	 var t =this.getComponent('left');
    	 var modified =t.getStore().getRange(0,t.getStore().modified);
    
     	 var json = []; 
          Ext.each(modified, function(item){ 
        	  json.push(item.data); 
          });
          return json;
          
     },
     //设置右列表
     SetRightList:function(value){
    	  var me = this;
     	 me.RightListValue=value;
     },
     //获取右列表
     GetRightList:function(){
    	 var me = this;
    	 var t =this.getComponent('right');
    	 var modified =t.getStore().getRange(0,t.getStore().modified);
     	 var json = []; 
         Ext.each(modified, function(item){ 
        	 json.push(item.data); 
         });
         return json;
     },
    /**
	 * 常量设置
	 * @private
	 */
	initConstants:function(){
		var me = this;
		if (me.Field.length == 0) {
	         me.SetField();
	    }
		me.LeftListValue=me.leftstore();//获得左列表
		me.RightListValue=me.rightstore();//获得右列表
		if(me.SelectType==0)
	    {
		    me.selType='checkboxmodel';//复选框
		    me.SimpleSelect=true;
		   
	    }else if(me.SelectType==1){
	        me.selType='rowmodel';
	        me.SimpleSelect=false;
	      
	    }
    },
    /**
    * 生成左边列表
    * @private
    * @return {}
    */
    createleft: function () {
        var me = this;
        var left = {
            xtype: 'gridpanel',
        	region: 'west',
            width: 180,
            title: '',
            minHeight: 380,
            maxHeight: 380,
            layout: 'fit',
            itemId: 'left',
            selType: me.selType, //复选框
            columns:me.ValueField,
            multiSelect: true, //允许多选
            simpleSelect: me.SimpleSelect,    //简单选择功能开启 
            enableKeyNav: true,     //启用键盘导航 
            store: me.LeftListValue,
            dockedItems: [
                { xtype: 'toolbar', dock: 'bottom', bodyCls:'bg-white',cls:'bg-white',
                items: [
                    { xtype: 'textfield', fieldLabel: '按,过滤',  labelAlign: 'right', height: 25,  labelWidth: 50,
                         itemId: 'com1',height:20,width: 170,
                         displayField: me.SearchField,
                         listeners:{
                        	 change:function(textfield, newValue,oldValue, eOpts ){
                                 me.filterFn(newValue);
                             },
                             keyup:function(e, eOpts ){
                                 var grid=me.getComponent('left');
                                 var store=grid.getStore();//reload()
                             }
                          }
                     }
                 ]
            }],
            multiSelect: true,
            stripeRows: true,
            viewConfig: {
                plugins: {
                    ptype: 'gridviewdragdrop',
                    dragGroup: 'firstGridDDGroup',
                    dropGroup: 'secondGridDDGroup'
                },
                listeners: {
                    drop: function (node, data, dropRec, dropPosition) {
                        var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('name') : ' on empty view';

                        me.fireEvent('Ondrag');
                    },
                    beforedrop: function (node, data, overModel, dropPosition, dropFunction, eOpts) {
                        //alert("移动列表记录时触发");
                        me.fireEvent('OnMove');
                    }
                }
            }
        };
        left.listeners = {
            itemdblclick: function () {
                var rows = this.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                var msg = [];
                var t =me.getComponent('right');
                var t1 = me.getComponent('left');
                t1.getStore().remove(rows);
                t.getStore().add(rows);
                me.fireEvent('OndbClick');
            },
            itemclick: function () {
                me.fireEvent('OnClick');

            }
        };
        return left;
    },
    /**
    * 生成右边列表
    * @private
    * @return {}
    */
    createright: function () {
        var me = this;
        var right = {
            xtype: 'gridpanel',
        	region: 'east',
            width: 180,
            title: '',
            minHeight: 380,
            maxHeight: 380,
            itemId: 'right',
            columns:me.ValueField,
            selType: me.selType, //复选框
            multiSelect: true, //允许多选
            simpleSelect: me.SimpleSelect,    //简单选择功能开启 
            enableKeyNav: true,     //启用键盘导航 
            store: me.RightListValue,
            dockedItems: [
                { xtype: 'toolbar', dock: 'bottom', bodyCls:'bg-white', cls:'bg-white',
                    items: [
                    { xtype: 'textfield', fieldLabel: '按,过滤', width: 170, labelAlign: 'right', labelWidth: 50, height: 25, itemId: 'com2',
                    height:20,displayField: me.SearchField,
                    listeners:{
                   	    change:function(textfield, newValue,oldValue, eOpts ){
                            me.filterFnright(newValue);
                        },
                        keyup:function(e, eOpts ){
                            var grid=me.getComponent('right');
                            var store=grid.getStore();//reload()
                        }
                    }
                 }
             ]
            }],
            multiSelect: true,
            stripeRows: true,
            viewConfig: {
              //  copy: true,      // 以复制方式拖拽，即拖拽后源内容不移除
                plugins: {
                    ptype: 'gridviewdragdrop',
                    dragGroup: 'secondGridDDGroup',
                    dropGroup: 'firstGridDDGroup'
                },
                listeners: {
                    drop: function (node, data, dropRec, dropPosition) {
                        var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('name') : ' on empty view';
                        me.fireEvent('Ondrag');
                    },
                    beforedrop: function (node, data, overModel, dropPosition, dropFunction, eOpts) {
                        me.fireEvent('OnMove');
                    }
                }

            }
        };
        right.listeners = {
            itemclick: function () {
                me.fireEvent('OnClick');
            },
            itemdblclick: function () {
                var rows = this.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                var t =me.getComponent('left');
                var t1 = me.getComponent('right');
                t1.getStore().remove(rows);
                t.getStore().add(rows);
                me.fireEvent('OndbClick');
            }
        };
        return right;
    },
    /**
    * 生成btton
    * @private 
    * @return {}
    */
    createbutton: function () {
        var me = this;
        var button = {
            xtype: 'panel',
            height: 380,
            width: 60,
            region: 'center',
            title: '',
            border: false,
            layout: {
    	        align: 'center',
    	        pack: 'center',
    	        type: 'vbox'
    	    },
            items: [
                { xtype: 'button', text: '<<',margins: '5',width:50,
                    handler: function () {
                        var t =me.getComponent('right');
                        var rows = t.getStore().getRange(0, t.getStore().getCount());
                            var t2 = me.getComponent('left');
                            t.getStore().remove(rows);
                            t2.getStore().add(rows);
                    }
                },
                { xtype: 'button', height: 20, text: '>',margins: '5',width:50,
                    handler: function () {
                        var t =me.getComponent('left');
                        var rows = t.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                            var t2 = me.getComponent('right');
                            t.getStore().remove(rows);
                            t2.getStore().add(rows);
                    }
                },
                { xtype: 'button', text: '<',margins: '5',width:50,
                    handler: function () {
                        var t =me.getComponent('right');
                        var rows = t.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                            var t2 = me.getComponent('left');
                            t.getStore().remove(rows);
                            t2.getStore().add(rows);
                    }
                },
                { xtype: 'button', text: '>>',margins: '5',width:50,
                    handler: function () {
                        var t =me.getComponent('left');
                        var rows = t.getStore().getRange(0, t.getStore().getCount());
                        var msg = [];
                            var t2 = me.getComponent('right');
                            t.getStore().remove(rows);
                            t2.getStore().add(rows);
                    }
                }
            ]
        };
        return button;
    },

    /**
     * 生成btton
     * @private 
     * @return {}
     */
    createbutton1:function(){
    	var me = this;
    	var button = {
    		xtype: 'panel',
    		height: 30,
    		//width: 60,
    	    region: 'south',
    		title: '',
    		//border:false,
    		layout: {
                align: 'middle',
                pack: 'end',
                type: 'hbox'
            },
    		items: [
		        {xtype: 'button',text: '保存',iconCls: 'Save', margin:5,
	            	handler: function(){
	            
		        	  me.Save();
	            	  this.fireEvent('OnSave');
	                }
	            },
	            {xtype: 'button',text: '重置',iconCls: 'refresh',
	             handler: function(){
	               // win.hide();
	            	var t =Ext.getCmp('left').getStore();
	            	t.load();
	            	var t1 =Ext.getCmp('right').getStore();
	            	t1.load();
	                }
	            }
	        
            ]   
    	};
    	return button;
    },
    /**
    * 组装组件内容
    * @private
    */
    setAppItems: function () {
        var me = this;
        var items = [];
        //左列表
        var left = me.createleft();
        if (left)
            items.push(left);
        //按钮
        var button = me.createbutton();
        if (button)
            items.push(button);
        //右列表
        var right = me.createright();
        if (right)
            items.push(right);

      //确定和重置
    	var button= me.createbutton1();
    	if(button)
    		items.push(button);
    	
        me.items = items;
    },

    /**
    * 注册事件
    * @private
    */
    addAppEvents: function () {
        var me = this;
        me.addEvents('Ondrag'); //当拖拽移动后触发
        me.addEvents('OnClick'); //单击列表记录时触发
        me.addEvents('OnMove'); //移动列表记录时触发
        me.addEvents('OnSave'); //单击列表记录时触发
        me.addEvents('OndbClick'); //双击列表记录时触发
    },

    /**
    * 初始化组件
    */
    initComponent: function () {
        var me = this;
        //常量设置
        me.initConstants();
        //注册事件
        me.addAppEvents();
        //组装组件内容
        me.setAppItems();
        this.callParent(arguments);
    },
    /**
     * 模糊查询过滤函数(左）
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         var grid = me.getComponent('left');
         var store = grid.getStore(); //reload()
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },

     /**
     * 模糊查询过滤函数(右）
     * @param {} value
     */
     filterFnright: function (value) {
         var me = this, valtemp = value;
         var grid = me.getComponent('right');
         var store = grid.getStore(); //reload()
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         //toUpperCase()
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     }

});


