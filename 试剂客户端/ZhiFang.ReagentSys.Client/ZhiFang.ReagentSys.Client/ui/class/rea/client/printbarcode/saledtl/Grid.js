/**
 * 某一供货明细货品条码信息
 * @author longfc
 * @version 2018-04-28
 */
Ext.define('Shell.class.rea.client.printbarcode.saledtl.Grid', {
	extend: 'Shell.class.rea.client.printbarcode.basic.DtlGrid',
	title: '货品条码信息',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaleDtlId?isPlanish=true',

	/**获取条码信息的业务明细ID*/
	PK: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: "ReaBmsCenSaleDtl",

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
		if(me.PK) url = url + "&saleDtlId=" + me.PK;
		return url;
	}
});