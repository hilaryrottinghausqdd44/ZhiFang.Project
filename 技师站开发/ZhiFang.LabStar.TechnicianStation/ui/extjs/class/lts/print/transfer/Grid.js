/**
 * 打印样本清单
 * @author liangyl
 * @version 2019-12-6
 */
Ext.define('Shell.class.lts.print.transfer.Grid', {
	extend: 'Shell.class.lts.print.basic.Grid',
	requires: ['Ext.ux.CheckColumn'],
	title: '打印样本清单',
    /**获取数据服务路径*/
	selectUrl:'',
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	//获取当前已选列表的数据
	getDataList : function(){
		var me = this;
		return me.store.data.items;
	}
});