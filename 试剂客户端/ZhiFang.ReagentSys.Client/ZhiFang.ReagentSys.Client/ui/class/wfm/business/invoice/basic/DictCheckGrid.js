/**
 * 字典
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.basic.DictCheckGrid', {
	extend: 'Shell.class.wfm.dict.CheckGrid',
	width: 270,
	height: 300,
		/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text: '字典编码',
			dataIndex: 'BDict_Shortcode',
			width: 80,
			hidden: true,
			sortable: true,
			defaultRenderer: true
		});	
		return columns;
	}
});