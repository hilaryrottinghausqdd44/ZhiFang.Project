/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '货品信息',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlInfo = Ext.create('Shell.class.rea.client.reasale.basic.DtlInfo', {
			title: '货品信息',
			header: true,
			border: false,
			itemId: 'DtlInfo'
		});
		me.BarCodeGrid = Ext.create('Shell.class.rea.client.printbarcode.saledtl.Grid', {
			title: '条码信息',
			header: true,
			border: false,
			itemId: 'BarCodeGrid'
		});

		var appInfos = [me.DtlInfo, me.BarCodeGrid];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		me.activeTab = 0;
		me.loadDtlInfo(record);
		me.loadBarCode(record);
	},
	loadDtlInfo: function(rec) {
		var me = this;
		var id = rec.get("ReaBmsCenSaleDtl_Id");
		var info = {
			"CName": rec ? rec.get("ReaBmsCenSaleDtl_GoodsCName") : "",
			"EName": rec ? rec.get("ReaBmsCenSaleDtl_EName") : "",
			"GoodsNo": rec ? rec.get("ReaBmsCenSaleDtl_ReaGoodsNo") : "",
			"ReaGoodsNo": rec ? rec.get("ReaBmsCenSaleDtl_ReaGoodsNo") : "",
			"CenOrgGoodsNo": rec ? rec.get("ReaBmsCenSaleDtl_CenOrgGoodsNo") : "",
			"ProdGoodsNo": rec ? rec.get("ReaBmsCenSaleDtl_ProdGoodsNo") : "",
			"SName": rec ? rec.get("ReaBmsCenSaleDtl_SName") : "",
			"Unit": rec ? rec.get("ReaBmsCenSaleDtl_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("ReaBmsCenSaleDtl_UnitMemo") : "",
			"LotNo": rec ? rec.get("ReaBmsCenSaleDtl_LotNo") : "",
			"InvalidDate": rec ? rec.get("ReaBmsCenSaleDtl_InvalidDate") : "",
			"Price": rec ? rec.get("ReaBmsCenSaleDtl_Price") : "",
			"GoodsQty": rec ? rec.get("ReaBmsCenSaleDtl_GoodsQty") : "",
			"SumTotal": rec ? rec.get("ReaBmsCenSaleDtl_SumTotal") : "",
			"ProdDate": rec ? rec.get("ReaBmsCenSaleDtl_ProdDate") : "",
			"ApproveDocNo": rec ? rec.get("ReaBmsCenSaleDtl_ApproveDocNo") : "",
			"CompanyName": rec ? rec.get("ReaBmsCenSaleDtl_CompanyName") : ""
		};
		me.DtlInfo.initData(info);
	},
	loadBarCode: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDtl_Id");
		if(id) {
			me.BarCodeGrid.PK = id;
			me.BarCodeGrid.onSearch();
		} else {
			me.BarCodeGrid.PK = null;
			me.BarCodeGrid.clearData();
		}
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.DtlInfo.PK = null;
		me.DtlInfo.clearData();
		
		me.BarCodeGrid.PK = null;
		me.BarCodeGrid.clearData();
	}
});