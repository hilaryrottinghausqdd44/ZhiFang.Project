Ext.ns('Ext.iqc');
Ext.define('Ext.iqc.date.InfoList',{
	extend:'Ext.grid.Panel',
	alias:'widget.iqcdateinfolist',
	title:'质控列表',
	width:800,
	hieght:600,
	/**
	 * 初始化面板信息
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.store = me.createStore();//创建数据集
		me.columns = me.createColumns();//创建数据列内容
		me.dockedItems = me.createDockedItems();//创建挂靠功能
		me.cellEdit = me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});//单元格编辑
		me.callParent(arguments);
	},
});