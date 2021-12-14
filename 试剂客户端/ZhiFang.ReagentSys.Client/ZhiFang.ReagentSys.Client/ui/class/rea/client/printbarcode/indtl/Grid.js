/**
 * 某一入库明细货品条码信息
 * @author longfc
 * @version 2018-04-25
 */
Ext.define('Shell.class.rea.client.printbarcode.indtl.Grid', {
	extend: 'Shell.class.rea.client.printbarcode.basic.DtlGrid',
	title: '货品条码信息',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId?isPlanish=true',
	/**获取条码信息的业务明细ID*/
	PK: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: "ReaBmsInDtl",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbarPrinter').getComponent('PrinterList');
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		if(me.PK) url = url + "&inDtlId=" + me.PK;
		return url;
	}
});