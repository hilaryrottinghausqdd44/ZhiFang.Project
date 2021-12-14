/**
 * 经销商列表树
 * @author liangyl	
 * @version 2017-07-20
 */
Ext.define('Shell.class.pki.dunitdealer.DealerGrid', {
	extend: 'Shell.class.pki.dealer.GridTree',
	title: '经销商列表',
	//=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '经销商名称',
			xtype:'treecolumn',
			dataIndex: 'text',
			flex:1,
			sortable: false
		}, {
			text: '用户代码',
			dataIndex: 'UseCode',
			width: 60,
			sortable: false,
			menuDisabled:false,defaultRenderer: true
		},
		{
			dataIndex: 'BDealer_BBillingUnit_Name',
			text: '默认开票方',
			hidden:false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_BBillingUnit_Id',
			text: '开票方ID',
			hidden: true,
			hideable: false
		}];
		return columns;
	}
});