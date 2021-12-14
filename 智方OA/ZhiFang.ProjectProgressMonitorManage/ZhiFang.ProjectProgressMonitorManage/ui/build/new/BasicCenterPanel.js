/**
 * 【功能说明】
 * 		构建工具-展示区域基类
 * 【提供的方法】
 * 		changePanel(panel) 更改内部面板
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicCenterPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basiccenterpanel',
    //==========================可配参数=========================
	
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
    },
    /**
     * 初始化面板属性
     * @private
     */
    initParams:function(){
    	var me = this;
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
    	var me = this;
    	me.items = [{xtype:'panel',itemId:'center',hidden:true}];
    },
    //==========================================================
    /**
     * 更改内部面板
     * @public
     * @param {} panel 面板对象
     */
   	changePanel:function(panel){
   		var me = this;
   		me.remove('center');
   		me.add(panel);
   	}
});