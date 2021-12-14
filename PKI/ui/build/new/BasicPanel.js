/**
 * 【功能说明】
 * 		构建工具-面板基类;
 * 		所有的构建工具继承自本面板;
 * 
 * 【可以重写的方法】
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicpanel',
    requires:[
    	'Ext.build.BasicToolbar',
    	'Ext.build.BasicCenterPanel',
    	'Ext.build.BasicEastPanel',
    	'Ext.build.BasicSouthPanel'
    ],
    mixins:['Ext.build.InteractiveService'],
    //==========================可配参数=========================
    /**
     * 构建工具名称
     * @type String
     */
    panelName:'构建工具基本面板',
    /**
     * 追加的功能栏按钮组
     * @type 
     */
    toolbarItems:[],
    //==========================基础方法=========================
    /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	//初始化面板监听
    	me.initListenres();
    },
    /**
     * 初始化面板
     * @private
     */
    initComponent:function(){
    	var me = this;
    	//初始化公开事件
    	me.initEvent();
    	//初始化面板属性
    	me.initParams();
    	//初始化视图
    	me.initView();
    	me.callParent(arguments);
    },
    /**
     * 初始化面板监听
     * @private
     */
    initListenres:function(){
    	var me = this;
    },
    /**
     * 初始化公开事件
     * @private
     */
    initEvent:function(){
    	var me = this;
    	me.addEvents('browse');//浏览按钮
		me.addEvents('save');//保存按钮
		me.addEvents('saveAs');//另存按钮
    },
    /**
     * 初始化面板属性
     * @private
     */
    initParams:function(){
    	var me = this;
    	
		//边距
		me.bodyPadding = 2;
		//布局方式
		me.layout = {
			type:'border',
			regionWeights:{south:1,east:2,north:3}
		};
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
    	var me = this;
    	
		//效果展示区
		var center = me.createCenterPanel();
		//属性面板
		var east = me.createEastPanel();
		//列属性列表
		var south = me.createSouthPanel();
		
		//功能模块ItemId
		center.itemId = "center";
		east.itemId = "east";
		south.itemId = "south";
		
		//功能块位置
		center.region = "center";
		east.region = "east";
		south.region = "south";
		
		//功能块大小
		south.height = 200;
		east.width = 250;
		
		//功能块收缩属性
		east.split = true;
		east.collapsible = true;
		
		south.split = true;
		south.collapsible = true;
		south.header = false;
		
		//列属性列表是否默认收缩
		//south.collapsed = true;
		
		me.items = [center,east,south];
		
		//功能按钮栏
		var toolbar = me.createToolbar();
		toolbar.itemId = "toolbar";
		toolbar.dock = "top";
		me.dockedItems = me.dockedItems || [];
		me.dockedItems.push(toolbar);
    },
    //==========================================================
    /**
     * 创建功能按钮栏
     * @private
     */
    createToolbar:function(){
    	var me = this;
    	var com = {
    		xtype:'basictoolbar',
    		panelName:me.panelName,
    		items:me.toolbarItems,
    		listeners:{
    			browse:function(){me.browse();},
    			save:function(){me.save();},
    			saveAs:function(){me.saveAs();}
    		}
    	};
    	return com;
    },
    /**
     * 创建功能展示区域
     * @private
     */
    createCenterPanel:function(){
    	var me = this;
    	var com = {xtype:'basiccenterpanel'};
    	return com;
    },
    /**
     * 创建面板属性面板
     * @private
     */
    createEastPanel:function(){
    	var me = this;
    	var com = {xtype:'basiceastpanel'};
    	return com;
    },
    /**
     * 创建数据项属性面板
     * @private
     */
    createSouthPanel:function(){
    	var me = this;
    	var com = {xtype:'basicsouthpanel'};
    	return com;
    },
    /**
     * 浏览
     * @private
     */
    browse:function(){
    	var me = this;
    	me.fireEvent('browse');
    },
    /**
     * 保存
     * @private
     */
    save:function(){
    	var me = this;
    	me.fireEvent('save');
    },
    /**
     * 另存
     * @private
     */
    saveAs:function(){
    	var me = this;
    	me.fireEvent('saveAs');
    }
});