/**
 * 收款对比审核关联客户及付款单位
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.associate.finance.check.Grid', {
	extend: 'Shell.class.wfm.business.associate.finance.basic.Grid',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '商务收款对比审核',
	hiddenCheckCheckId: false,
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(2, 0, {
			xtype: 'actioncolumn',
			text: '审核',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					var checkId = record.get("CheckId");
					if(checkId) return "";
					else
						return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					me.fireEvent('onSaveClick', grid, rowIndex, colIndex);
				}
			}]
		});
		return columns;
	}
});