/**
 * 表单数据项快速排布
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.FormQuickLayout',{
    extend:'Ext.form.Panel',
    alias: 'widget.formquicklayout',
    initComponent:function(){
    	var me = this;
    	me.initView();//初始化视图
    	me.callParent(arguments);
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
    	var me = this;
    	me.items = me.createItems();
    },
    /**
     * 创建items
     * @private
     * @return {}
     */
    createItems:function(){
    	var me = this;
    	var items = [{
    		xtype:'numberfield',
    		itemId:''
    	}];
    	return items;
    }
});