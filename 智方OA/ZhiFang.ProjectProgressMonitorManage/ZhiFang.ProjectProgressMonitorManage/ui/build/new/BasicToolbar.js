/**
 * 【功能说明】
 * 		构建工具-工具栏基类;
 * 		具有浏览、保存、另存三个按钮，构建工具的名称在工具栏的最前端;
 * 【可配属性】
 * 		panelName 构建工具名称
 * 【公开的事件】
 * 		browse 浏览
 * 		save 保存
 * 		saveAs 另存
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicToolbar',{
    extend:'Ext.toolbar.Toolbar',
    alias: 'widget.basictoolbar',
    //==========================可配参数=========================
    /**
     * 构建工具名称
     * @type String
     */
    panelName:'构建工具工具栏',
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
    	//注册事件
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
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
    	var me = this;
    	//内部数据项
    	var items = ['<b>'+me.panelName+'</b>','->'];
    	
		me.items && (items = items.concat(me.items));
		
		var basic = [{
			xtype:'button',text:'浏览',itemId:'browse',iconCls:'build-button-see',
			tooltip:'浏览',
			handler:function(){me.browse();}
		},'-',{
			xtype:'button',text:'保存',itemId:'save',iconCls:'build-button-save',
			tooltip:'保存',
			handler:function(){me.save();}
		},'-',{
			xtype:'button',text:'另存',itemId:'saveAs',iconCls:'build-button-save',
			tooltip:'另存',
			handler:function(){me.saveAs();}
		}];
		items = items.concat(basic);
		
		me.items = items;
    },
    //==========================================================
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