/**
 * 模块角色操作应用
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleRoleOperateApp',{
	extend:'Ext.panel.Panel',
	alias: 'widget.moduleroleoperateapp',
	//=====================视图渲染======================
	/**
	 * 渲染后后执行
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	/**
	 * 初始化组件
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//初始化参数
		me.initParams();
		//初始化视图
		me.initView();
		me.callParent(arguments);
	},
	/**
	 * 初始化参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		me.layout = "border";
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		//内部模块
    	me.items = me.createItems();
	},
	/**
     * 创建内部模块
     * @private
     */
    createItems:function(){
    	var me = this;
    	//模块树
    	var moduleTree = Ext.create('Ext.manage.module.ModuleTree',{
    		itemId:'moduleTree',
    		region:'west',
    		title:'模块',
    		width:250,
    		split:true,
    		collapsible:true,
    		hasOkButton:false
    	});
    	//模块角色操作列表
    	var moduleRoleOperateList = Ext.create('Ext.manage.module.ModuleRoleOperateList',{
    		itemId:'moduleRoleOperateList',
    		region:'center',
    		title:'角色操作列表'
    	});

    	var items = [moduleTree,moduleRoleOperateList];
    	return items;
    },
    /**
     * 初始化监听
     * @private
     */
    initListeners:function(){
    	var me = this;
    	var moduleTree = me.getComponent('moduleTree');
    	var moduleRoleOperateList = me.getComponent('moduleRoleOperateList');
    	moduleTree.on({
    		select:function(row,record,index,eOpts){
    			var id = record.get('tid');
    			moduleRoleOperateList.changeListByModuleId(id);
    		}
    	});
    }
});