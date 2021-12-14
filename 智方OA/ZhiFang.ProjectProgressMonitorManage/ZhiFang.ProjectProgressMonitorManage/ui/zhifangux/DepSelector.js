/**
 * 部门选择器
 * 选择一个或多个部门
  如果为多选，则前面有选择框（按确定返回），如果是单选则没有选择框（双击或确定返回）

 * @param
 * SelectType：多选方式（0,1）  可配置，但现实复选框有问题
 * DataServerUrl：后台服务地址  可配置
 * width：宽  可配置
 * height：高  可配置
 * RootVisible：是否显示根节点  可配置
 * DefaultRootProperty：子节点的属性名   可配置
 * okText：文本（保存）  可配置
 * DefaultRootProperty：文本（取消）   可配置
 * 
 * 对外公开事件
 * OnChanged  当改变后触发
 * OnClick  单击树节点时触发
 * OndbClick	双击树节点时触发	
 * OnSave 保存时触发
 * onCancelCilck	取消时触发	

 * 对外公开方法
 * SetSelectType 设置选择方式 
 * GetSelectType 获取选择方式
 * Save	保存对象	
 * getAllValue 获取整体树
 * Reload 重载	
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DepSelector', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.depselector',
    border: false,
    
    lastValue: [],
    SelectType: 0, //多选方式，0为单选，1为多选
    DataServerUrl: '', //后台服务地址
    Lines: false, //树线
    RootVisible: true, //是否显示根节点
    Expanded: true, //是否默认打开
    DefaultRootProperty: 'ResultDataValue', //子节点的属性名
    okText: '确定',
    cancelText: '取消',
    MultiSelect: false,
    width:300,
    height:330,
    /**
    * 获取数据列表选中的数据行
    * 返回json数据
    * @return {}
    */
    getAllValue: function () {
        var me = this;
        
        var tree =me.getComponent('tree');
        var records = tree.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
        if (records.length > 0) {
            //当有选择的数据的时候   
          	me.lastValue=[];
            for (var i = 0; i < records.length; i++) {
                //循环迭代所有的选择的row            
                me.lastValue.push(records[i].raw);
            }   
        }else{
            Ext.Msg.alert("请先选择人员");
            return;
       }
        
        return me.lastValue;
    },
    //设置选择方式
    SetSelectType: function (value) {
        var me = this;
        me.SelectType = value;
    },
    //获取选择方式
    GetSelectType: function () {
        var me = this;
        return me.SelectType;
    },
    //获取整体树
    GetValue: function () {
        var me = this;
        return me.treestore();
    },
    //重载
    Reload: function () {
        var me = this;
        var tree = me.treestore();
        tree.reload();
    },
 
    treestore: function () {
        var me = this;
        var store = new Ext.create('Ext.data.TreeStore', {
            async: false, //非异步
            method: 'GET',
            timeout: 5000,
            proxy: {//数据代理
                type: 'ajax', //请求方式
                url: me.DataServerUrl//数据访问路径  'server/BZtree.json'
            },
            defaultRootProperty: me.DefaultRootProperty, //子节点的属性名
            root: {
                text: '树菜单',
                leaf: false,      //是否是页子节点
                expanded: me.Expanded//是否默认打开
            },
            folderSort: true,
            sorters: [{
                property: 'text',
                direction: 'ASC'
            }]
        });
        return store;
    },
    nodep:function (node){ 
		var bnode=true;
		Ext.Array.each(node.childNodes,function(v){ 
			if(!v.data.checked){
				bnode=false;
				return;
			}
		});
		return bnode;
	},
	parentnode:function (node){
		if(node.parentNode != null){
			if(this.nodep(node.parentNode)){
				node.parentNode.set('checked',true);
			}else{ 
				node.parentNode.set('checked',false);
			}
			this.parentnode(node.parentNode);
		}
	},
	/**
     * 遍历子结点 选中 与取消选中操作
     * */
	chd:function (node,check){
		node.set('checked',check);
		if(node.isNode){
			node.eachChild(function(child){
				chd(child,check);
			}); 
		}
	},
    /**
    * 常量设置
    * @private
    */
    initConstants: function () {
        var me = this;
        if (me.SelectType == 1) {
            me.rootV = true;
            me.SelectType = 'checkboxmodel'; //复选框 	
            me.MultiSelect = true;
        }
        else if (me.SelectType == 0) {
            me.SelectType = 'rowmodel';
            me.MultiSelect = false;
        }
    },
    /**
    * 生成树
    * @private
    * @return {}
    */
    createtree: function () {
        var me = this;
        var tree = {
            itemId: 'tree',
            xtype: 'treepanel',
            lines: me.Lines,
            SelectType: me.SelectType,
        	checkModel: 'cascade',
        	checkModifiers:false,
            width:me.width,
            useArrows:true,//隐藏前导线使用箭头表示节点的展开
            store: this.treestore(),
            rootVisible: me.RootVisible,
            layout:'fit',
            height:me.height-30,
            containerScroll: true, //是否支持滚动条
            autoScroll: false, //内容溢出的时候是否产生滚动条
            multiSelect: me.MultiSelect//允许多选
        };
        tree.listeners = {
        	checkchange:function(node,checked){
        		if(checked){
        			node.eachChild(function (child){
        				me.chd(child,true);
        			});
        		}else{
        			node.eachChild(function (child){
        				me.chd(child,false);
        			});
        		}
        		me.parentnode(node);//进行父级选中操作 
        	},
        	beforerender: function () {
                if (me.SelectType == 'checkboxmodel') {
                	var t =me.getComponent('tree');
                    var tree = t.store;
                    var rootnodes = tree.getRootNode().childNodes; //获取主节点
                    function iterNodes(rootnodes) {
                        for (var i = 0; i < rootnodes.length; i++) {
                            rootnodes[i].data.checked = false;           //设置checkebox      
                            if (rootnodes[i].childNodes.length > 0) {
                                iterNodes(rootnodes[i].childNodes); //如果有子节点就递归。     
                            }
                        }
                    }
                    iterNodes(rootnodes);
                   
                } else {
                    return;
                }
               
            },
            itemdblclick: function () {
                if (me.SelectType == 'checkboxmodel') {
                    return;
                }
                else if (me.SelectType == 'rowmodel') {
                    var tree =me.getComponent('tree');
                    var records = tree.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                    if (records.length > 0) {
                        //当有选择的数据的时候   
                      	me.lastValue=[];
                        for (var i = 0; i < records.length; i++) {
                            //循环迭代所有的选择的row            
                            me.lastValue.push(records[i].raw);
                        }   
                    }else{
                        Ext.Msg.alert("请先选择人员");
                        return;
                   }
                }
                me.fireEvent('OndbClick');
            },
            itemclick: function (view,record,item,index,e) {
                me.fireEvent('OnClick');
            }
        };
        return tree;
    },
    createbutton: function () {
        var me = this;
        var button = {
            xtype: 'panel',
            height: 30,
            width:me.width,
            dock: 'bottom',
            title: '',
            border: false,
            layout: {
                align: 'middle',
                pack: 'end',
                type: 'hbox'
            },
            items: [
                { xtype: 'button', text: me.okText, margin:5,
                    handler: function () {
                        if (me.SelectType == 'checkboxmodel') {
                            var records = me.getComponent('tree').getView().getChecked();
                            me.lastValue=[];
                                if (records.length > 0) {
                                   //当有选择的数据的时候   
                              	    me.lastValue=[];
                                    for (var i = 0; i < records.length; i++) {
                                      //循环迭代所有的选择的row                                              	
                                    	me.lastValue.push(records[i].raw);
                                    
                                    }
                             	   Ext.MessageBox.show({
                                       title: 'Selected Nodes',
                                       msg: Ext.encode(me.lastValue),
                                       icon: Ext.MessageBox.INFO
                                   });
                                }
                                else{
                                    Ext.Msg.alert("请先选择人员");
                                    return;
                                }
                            
                            me.fireEvent('OnSave');
                        }
                        else if (me.SelectType == 'rowmodel') {
                            var grid=me.getComponent('tree');
                            var store=grid.getStore();//reload()
                            //得到列表数据所有选中的行数据
                            var records = grid.getSelectionModel().getSelection();
                              //当有选择的数据的时候   
                              	me.lastValue=[];
                                for (var i = 0; i < records.length; i++) {
                                	me.lastValue.push(records[i].raw);
                                   }   
                                }else{
                                    Ext.Msg.alert("请先选择人员");
                                    return;
                                }

                            me.fireEvent('OnSave');
                    }
                },
                { xtype: 'button', text: me.cancelText,
                    handler: function () {
                        me.fireEvent('onCancelCilck');
                    }
                }
            ]
        };
        return button;
    },
    /**
    * 注册事件
    * @private
    */
    addAppEvents: function () {
        var me = this;
        me.addEvents('OndbClick'); //双击
        me.addEvents('OnClick'); //单击
        me.addEvents('CheckChange'); //选择改变
        me.addEvents('OnSave'); //保存
        me.addEvents('onCancelCilck'); //取消
    },
    /**
    * 组装组件内容
    * @private
    */
    setAppItems: function () {
        var me = this;
        var items = [];
        var tree = me.createtree();
        if (tree)
            items.push(tree);
        var button = me.createbutton();
        if (button)
            items.push(button);
        me.items = items;
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
    }
});