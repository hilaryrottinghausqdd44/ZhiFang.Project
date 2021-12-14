/**
 * 验货入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.confirm.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '验货入库',
	border: false,
	/**验货单ID*/
	BmsCenSaleDocConfirmID: null,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 0,
	/**是否严格模式，严格1,混合模式’0*/
	CodeScanningMode: '0',
	winPanel: '',
	/**入库扫码模式(严格模式:1,混合模式：2)*/
	scanCodeModelParaNo: 'C-RBID-GISC-0005',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			onStoreInClick: function(panel) {
				JShell.Action.delay(function() {
					me.onSaveClick();
				}, null, 500);
			},
			nodata: function() {
				if(me.winPanel) {
					me.winPanel.close();
				}
			},
			changeSumTotal: function(record, rectotal) {
				var TotalPrice = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_TotalPrice');
				var SumTotal = me.DtlGrid.getAllSumTotal();
				TotalPrice.setValue(SumTotal);
			},
			onScanCodeShowDtl: function(grid, info) {
				me.showDtlPanel(grid, info);
			}
		});
		me.DocForm.on({
			load: function(form, data) {
				if(data && data.value) {
					me.DtlGrid.ReaCompID = data.value.ReaBmsCenSaleDocConfirm_ReaCompID;
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.getScanCodeModel(function(data) {
			me.CodeScanningMode = data;
		});
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.stock.confirm.DocForm', {
			title: '验收单入库',
			itemId: 'DocGrid',
			region: 'north',
			formtype: 'edit',
			header: false,
			/**带功能按钮栏*/
			hasButtontoolbar: false,
			PK: me.BmsCenSaleDocConfirmID,
			height: 100,
			border: false,
			split: false,
			collapsible: false,
			collapsed: false
		});

		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.confirm.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			CodeScanningMode: me.CodeScanningMode,
			BmsCenSaleDocConfirmID: me.BmsCenSaleDocConfirmID
		});
		var appInfos = [me.DocForm, me.DtlGrid];
		return appInfos;
	},
	clearData: function() {
		var me = this;
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
	},
	/**入库保存服务*/
	onSaveClick: function() {
		var me = this;
		//验证验货单入库明细
		var isExect = me.DtlGrid.isVerification();
		if(!isExect) return;

		//获取本次入库合计总额
		var count = me.DtlGrid.getSumTotal();
		//获取总单信息
		var bmsindoc = me.DocForm.getAddParams();
		bmsindoc.TotalPrice = count;
		//验货单明细id
		var dtlDocConfirmIDStr = me.DtlGrid.getDtlConfirmID();
		//入库明细
		var ReaBmsInDtlVO = me.DtlGrid.getReaBmsInDtl(bmsindoc);
		var url = JShell.System.Path.getUrl(me.DtlGrid.addUrl);
		//	/**扫码模式(严格模式:strict,混合模式：mixing)*/
		var CodeScanningMode = "mixing";
		if(me.CodeScanningMode == '1') {
			CodeScanningMode = 'strict';
		}
		//是否需要保存后打印
		var IsPrint = me.DtlGrid.getIsPrint();
		var params = Ext.JSON.encode({
			entity: bmsindoc,
			docConfirmID: me.BmsCenSaleDocConfirmID,
			dtlDocConfirmIDStr: dtlDocConfirmIDStr,
			dtAddList: ReaBmsInDtlVO,
			codeScanningMode: CodeScanningMode
		});
		if (!me.BUTTON_CAN_CLICK) return;
		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("入库保存中...");
		
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//先清空列表信息,防止后续处理出现异常,用户又进行第二次保存
				me.DtlGrid.store.removeAll();
				
				var id = null;
				if(data && data.value) {
					id = data.value.id;
				}
				me.fireEvent('save', me, id, IsPrint);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	showDtlPanel: function(grid, info) {
		var me = this;
		var itemId = "reastore";
		var win = Ext.WindowManager.get(itemId);
		if(!win) {
			var config = {
				title: "货品信息(5秒后会自动隐藏)",
				resizable: false,
				maximizable: false,
				modal: false,
				closable: true, //关闭功能
				draggable: true, //移动功能
				floating: true, //浮动模式
				width: 280,
				height: 380,
				alwaysOnTop: true,
				itemId: itemId,
				id: itemId
			};
			win = JShell.Win.open('Shell.class.rea.client.stock.confirm.DtlInfo', config);
			Ext.WindowManager.register(win);
		}
		if(win) {
			//WIN宽高、位置
			var winHeight = me.getHeight();
			var winWidth = me.getWidth();
			var zIndex = me.zIndexManager.zseed + 100;
			var position = me.getPosition();
			var winPosition = [position[0] + winWidth - win.width - 20, winHeight - win.height - 25];
			win.initData(info);
			if(grid.getIShowDtlInfoValue() == true) {
				win.showAt(winPosition);
			} else {
				win.hide();
			}
			if(grid.hideTimes && grid.hideTimes > 0) {
				JcallShell.Action.delay(function() {
					win.hide();
				}, null, grid.hideTimes);
			}
		}
	},
	/**获取扫码模式参数信息*/
	getScanCodeModel: function(callback) {
		var me = this;
		var CodeScanningMode = Ext.util.Cookies.get("CodeScanningMode"); //JcallShell.System.Cookie.get
		if(!CodeScanningMode) {
			var url = JShell.System.Path.getRootUrl("/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=" + me.scanCodeModelParaNo);
			JShell.Server.get(url, function(data) {
				if(data.success) {
					var obj = data.value;
					if(obj) {
						var paraValue = parseInt(obj.ParaValue);
						if(paraValue)
							me.CodeScanningMode = parseInt(paraValue);
						else
							paraValue = me.CodeScanningMode;
						var days = 30;
						var exp = new Date();
						var expires = exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
						Ext.util.Cookies.set("CodeScanningMode", paraValue);
					}
					if(callback) callback(paraValue);
				} else {
					JShell.Msg.error('获取系统参数(出库扫码信息)出错！' + data.msg);
				}
			}, false);
		} else {
			me.CodeScanningMode = parseInt(CodeScanningMode);
			if(callback) callback(me.CodeScanningMode);
		}
	}
});