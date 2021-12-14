/**
 * 出库查询
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.out.useout.ShowDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',
	
	/**用户UI配置Key*/
	userUIKey: 'out.useout.ShowDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库明细列表",
	/**查询数据*/
	selectUrl: '/ReaManageService.svc/GetPlatformOutDtlListByHQL?isPlanish=true',
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