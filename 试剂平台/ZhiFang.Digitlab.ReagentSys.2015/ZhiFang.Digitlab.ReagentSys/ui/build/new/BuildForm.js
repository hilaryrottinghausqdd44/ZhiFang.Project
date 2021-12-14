/**
 * 【功能说明】
 * 		表单构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BuildForm',{
    extend:'Ext.build.BasicPanel',
    alias: 'widget.buildform',
    panelName:'表单构建工具',
    //==========================可配参数=========================
   	
    //==========================基础方法=========================
    /**
     * 初始化面板
     * @private
     */
    initComponent:function(){
    	var me = this;
    	me.callParent(arguments);
    }
    //==========================================================
});