Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.checkboxgrid',{
	extend:'Ext.grid.Panel',
	alias:['widget.checkboxgrid','widget.checkgrid'],
	//========================可配属性=============================
	/**
	 * 勾选匹配字段
	 * @type String
	 */
	keyColumn:'Id',
	//========================视图渲染=============================
	/**
	 * 初始化组件
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//创建列属性
		me.columns = me.createColumns();
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**
	 * 创建列属性
	 * @private
	 */
	createColumns:function(){
		var me = this;
		//复选框列
		var columns = [{
			
		}];
		//追加上外部赋的columns
		columns.concat(me.columns);
		return columns;
	},
	/**
	 * 创建爱你数据集
	 * @private
	 */
	createStore:function(){
		var me = this;
		var store = me.store;
		return store;
	}
});