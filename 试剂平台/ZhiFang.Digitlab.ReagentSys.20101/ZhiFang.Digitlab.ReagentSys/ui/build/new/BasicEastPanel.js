/**
 * 【功能说明】
 * 		构建工具-面板属性基类
 * 【提供的方法】
 * 		addPanel(panel) 添加内部面板
 * 		setActivePanel(itemId) 显示选中项
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicEastPanel',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basiceastpanel',
    //==========================可配参数=========================
    /**
     * 选中项的内部编号
     * @type String
     */
    activedItemId:'',
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
    },
    //==========================================================
    /**
     * 添加内部面板
     * @public
     * @param {} panel 面板对象
     */
   	addPanel:function(panel){
   		var me = this;
   		var itemId = panel.itemId;
   		me.remove(itemId);
   		me.add(panel);
   		me.setActivePanel(itemId);
   	},
   	/**
   	 * 显示选中项
   	 * @public
   	 * @param {} itemId 内部编号
   	 */
   	setActivePanel:function(itemId){
   		var me = this;
   		if(me.activedItemId && me.activedItemId != ''){
   			var panel = me.getComponent(me.activedItemId);
   			panel && panel.hide();
   		}
   		var activedPanel = me.getComponent(itemId);
   		activedPanel.show();
   		me.setTitle(activedPanel.title);
   		me.activedItemId = itemId;
   	}
});