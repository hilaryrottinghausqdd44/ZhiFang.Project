/**
 * 出库查询
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.out.search.ShowDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',
	
	/**用户UI配置Key*/
	userUIKey: 'out.search.ShowDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库明细列表",
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(10,0,{
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',type:'int',sortable: false,
		    width: 80,defaultRenderer: true
		});
		return columns;
	}
});