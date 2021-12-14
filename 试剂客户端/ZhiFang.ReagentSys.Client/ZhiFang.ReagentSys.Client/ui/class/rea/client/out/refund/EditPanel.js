/**
 * 出库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.refund.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	/**当前选择的主单Id*/
	PK: null,
	/**退库入库保存服务*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddInputReaGoodsByReturn',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var TotalPrice = me.DocForm.getComponent('ReaBmsInDoc_TotalPrice');
		me.DtlGrid.on({
			changeSumTotal: function() {
				var count = me.DtlGrid.getSumTotal();
				TotalPrice.setValue(count);
			},
			onSaveClick: function(p) {
				JShell.Action.delay(function() {
					var isExec = me.DtlGrid.onSaveCheck();
					if(isExec) me.onSave(p);
				}, null, 500);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.refund.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center'
		});
		me.DocForm = Ext.create('Shell.class.rea.client.out.refund.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 145,
			split: true,
			collapsible: true,
			formtype: 'add',
			collapseMode: 'mini'
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	onSearch: function(id) {
		var me = this;
		me.DocForm.PK = id;
		me.DocForm.isShow(id);
		me.DtlGrid.defaultWhere = 'reabmsindtl.InDocID=' + id;
		me.hiddenColumns(true);
		var buttonsToolbar = me.DtlGrid.getComponent('buttonsToolbar');
		var saveBtn = buttonsToolbar.getComponent('saveBtn');
		saveBtn.setVisible(false);
		me.DtlGrid.isLoad = true;
		me.DtlGrid.onSearch();
	},
	hiddenColumns: function(isHidden) {
		var me = this;
		var counts = 0;
		Ext.Array.each(me.DtlGrid.columns, function(column, index, countriesItSelf) {
			if(counts == 2) return false;
			if(column.text == "删除" || column.dataIndex == "ReaBmsInDtl_DefaulteGoodsQty") {
				if(isHidden == true) me.DtlGrid.columns[index].hide();
				else me.DtlGrid.columns[index].show();
				counts = counts + 1;
			}
		});
	},
	clearData: function(id) {
		var me = this;
		me.DocForm.PK = id;
		me.DocForm.isAdd();
		var buttonsToolbar = me.DtlGrid.getComponent('buttonsToolbar');
		var rbBtn = buttonsToolbar.getComponent('rbBtn');
		var v = rbBtn.getValue().rb;
		me.DtlGrid.isLoad = false;
		var buttonsToolbar = me.DtlGrid.getComponent('buttonsToolbar');
		var saveBtn = buttonsToolbar.getComponent('saveBtn');
		saveBtn.setVisible(true);
		me.DtlGrid.clearData();
		me.DocForm.clearData();
	},
	addDtlData: function(res, docId) {
		var me = this;
		me.DtlGrid.onUpdateData(res);
		var GoodsQty = 0;
		for(var i = 0; i < res.length; i++) {
			var count = res[i].get('ReaBmsInDtl_SumTotal');
			if(!count) count = 0;
			GoodsQty += Number(count);
		}
		me.DocForm.isAdd(docId);
		var count = me.DtlGrid.getSumTotal();
		me.DocForm.setTotalPrice(count);
		me.DtlGrid.enableControl();
	},
	/**出库保存服务*/
	onSave: function(p) {
		var me = this;
		me.showMask();
		//判断是整单入库还是部分入库
		var inputType = me.DtlGrid.getInputType();
		//获取总单信息
		var bmsindoc = me.DocForm.getAddParams();
		//获取明细
		var DtlInfo = me.DtlGrid.getDtlInfo();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			inDoc: bmsindoc,
			listReaBmsInDtl: DtlInfo,
			inputType: inputType
		});

		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			if(data.success) {
				//				p.close();
				//me.fireEvent('save', p);
				me.fireEvent('save');
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});