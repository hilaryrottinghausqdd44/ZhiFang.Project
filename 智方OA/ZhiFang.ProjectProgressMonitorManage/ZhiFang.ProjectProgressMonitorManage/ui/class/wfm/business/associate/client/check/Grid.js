/**
 * 客户与授权系统对照审核
 * @author liangyl
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.associate.client.check.Grid', {
	extend: 'Shell.class.wfm.business.associate.client.basic.CUserGrid',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	OAtype:2,
	title: '客户与授权系统对照审核',
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
			items: [{
				getClass: function(v, meta, record) {
					var checkId = record.get("CheckId");
				    var IsMapping =  record.get("IsMapping");
				    var ContrastId = record.get("ContrastId");
					if(!checkId && (IsMapping=='0' || IsMapping==true || IsMapping=='true')) {
						return 'button-edit hand';
					}
					else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					me.fireEvent('onSaveClick', grid, rowIndex, colIndex);
				}
			}],
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
				var checkId = record.get("CheckId");
				
				if(IsMapping=='1' || IsMapping==true || IsMapping=='true') {
					if(!checkId){
						meta.style = 'background-color:#f4c600' ;
					}
				}
	            return value;
			}
		});
		return columns;
	}
});