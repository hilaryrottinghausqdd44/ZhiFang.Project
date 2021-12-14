/**
 * 某一入库单货品条码信息
 * @author longfc
 * @version 2018-04-25
 */
Ext.define('Shell.class.rea.client.printbarcode.indoc.Grid', {
	extend: 'Shell.class.rea.client.printbarcode.basic.DocGrid',
	title: '货品条码信息',

	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDocId?isPlanish=true',

	/**默认加载*/
	defaultLoad: true,
	/**获取条码信息的业务ID(入库单ID)*/
	PK: null,
	/**获取条码信息的业务明细IDStr(入库明细ID)*/
	IDStr: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: "ReaBmsInDtl",
	hiddenSearchComboBox: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);

		var cboSearch = me.getComponent('buttonsToolbar').getComponent('cboSearch');
		var boxHql = "";
		if(cboSearch && cboSearch.hidden == false) {
			switch(cboSearch.getValue()) {
				case "1": //仅显示大包装盒条码
					boxHql = "reagoodsbarcodeoperation.Id=reagoodsbarcodeoperation.PUsePackSerial";
					break;
				case "2": //仅显示小包装盒条码
					boxHql = "reagoodsbarcodeoperation.Id!=reagoodsbarcodeoperation.PUsePackSerial";
					break;
					break;
				default:
					break;
			}
		}
		if(me.PK) url = url + "&inDocId=" + me.PK;
		if(me.IDStr) url = url + "&dtlIdStr=" + me.IDStr;
		if(boxHql) url = url + "&boxHql=" + boxHql;
		return url;
	},
	/**获取批条码单打组件*/
	getPrintOne: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('printOne');
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('PrinterList');
	}
});