/**
 * 出库登记
 * @author liangyl
 * @version 2018-02-12
 */
Ext.define('Shell.class.rea.client.out.enrollment.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '出库登记',

	/**出库类型默认值(使用出库)*/
	defaluteOutType: '1',
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	width: 800,
	height: 500,
	/**表单选中的库房*/
	StorageObj: {},
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.StockPanel.on({
			setDefaultStorage: function(id, name) {
				JShell.Action.delay(function() {
					me.loadDatas(id, name);
				}, null, 500);
			}
		});
		me.DtlGrid.on({
			delclick: function(tab) {
				me.onDelOne(tab);
			},
			save: function() {
				JShell.Msg.alert('保存成功', null, 2000);
				me.loadDatas(me.StockPanel.getStorageObj().StorageID, me.StockPanel.getStorageObj().StorageName);
			}
		});
		//选择行监听
		me.onSelect();
		//预加载是否开启近效期
		JShell.REA.RunParams.getRunParamsValue("IsOpenNearEffectPeriod", false, function(data1) {});
		//预加载是否强制近效期出库
		JShell.REA.RunParams.getRunParamsValue("IsOutOfStockInNeartermPeriod", false, function(data1) {});
	},
	initComponent: function() {
		var me = this;
		// 出库扫码模式
		me.getOutScanCodeModel(function(val) {});
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var height = document.body.clientHeight;
		me.StockPanel = Ext.create('Shell.class.rea.client.out.enrollment.stock.App', {
			region: 'north',
			height: height / 2,
			minHeight: 220,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			StorageObj: me.StorageObj
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.enrollment.DtlGrid', {
			header: false,
			region: 'center',
			itemId: 'DtlGrid',
			//扫码模式
			OutScanCodeModel: me.OutScanCodeModel,
			defaluteOutType: me.defaluteOutType
		});
		return [me.StockPanel, me.DtlGrid];
	},
	loadDatas: function(id, name) {
		var me = this;
		me.StorageObj.StorageID = id;
		me.StorageObj.StorageName = name;
		//清除库存表与出库明细表
		me.clearData();
		me.DtlGrid.StorageObj = me.StorageObj;
		me.StockPanel.loadData(me.StorageObj);
	},
	onDelOne: function(tab) {
		var me = this;
		me.StockPanel.onDelOne(tab);
	},
	onSelect: function() {
		var me = this;
		me.StockPanel.on({
			itemdblclick: function(record, unitArr, barcode) {
				me.DtlGrid.addRecordOne(record,barcode);
			},
			itemdbselectlclick: function(record, barcode) {
				var itemsTab = me.DtlGrid.store.data.items;
				var unitArr = [];
				//判断选择行是否已存在出库明细中
				var istabselect = true;
				for(var i = 0; i < itemsTab.length; i++) {
					var tab = itemsTab[i].data.ReaBmsOutDtl_Tab + "";
					var tab1 = record.get('ReaBmsQtyDtl_Tab');
					if(tab1 === tab) {
						JShell.Msg.alert('当前行数据已选择');
						return;
					}
				}
				me.StockPanel.ondbSelect2(record, unitArr, itemsTab);
			},
			scanCodeClick: function(barcode, qtyGrid) {
				var bo = me.DtlGrid.getLotNoIsScanCode(barcode, qtyGrid);
				me.StockPanel.onScanCode(barcode, bo);
			}
		});
	},
	clearData: function() {
		var me = this;
		me.StockPanel.clearData();
		me.DtlGrid.store.removeAll();
	},
	removeData: function() {
		var me = this;
		me.StockPanel.clearData();
		me.DtlGrid.store.removeAll();
	},
	/**获取出库扫码模式参数信息*/
	getOutScanCodeModel: function(callback) {
		var me = this;
		//出库货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("OutScanCode", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.OutScanCodeModel =paraValue;// parseInt(paraValue);
					if(callback) callback(me.OutScanCodeModel);
				}
			}
		});
	}
});