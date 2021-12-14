/**
 * 发票列表
 * @author liangyl	
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.invoice.check.Grid', {
    extend: 'Shell.class.wfm.business.associate.invoice.basic.Grid',
    IsCheckShow:true,
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(2,0,{
			xtype: 'actioncolumn',
			text: '审核',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var checkId = record.get("PInvoice_CheckId");
					if(checkId){
						 return "";
					}else{
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.fireEvent('onSaveClick', me);
				}
			}]
		});
		columns.splice(8,0,{
			text: '审核人',
			dataIndex: 'PInvoice_CheckCName',
			width:80,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		});
		return columns;
	}
 });