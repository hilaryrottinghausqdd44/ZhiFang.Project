/**
 * 新增出库(直接出库完成)
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.use.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '新增出库',
	width: 700,
	height: 480,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**条码类型*/
	barcodeOperType: '7',
	/**出库类型默认值*/
	defaluteOutType: '1',
	TakerObj: {},
	/**表单选中的库房*/
	StorageObj: {},
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc',
	/**获取上次出库的货品批号和货运批号，传值为货品id*/
	selectReaLastOutGoodUrl: '/ReaManageService.svc/RS_UDTO_GetLastLotNoAndTransportNo?',
	//按钮是否可点击
	BUTTON_CAN_CLICK: true,
	/*浮动框设置*/
	OTYPE: 'DtlGrid',
	/**后台定制的服务，传入货品id和货品批号可以判断该货品性能验证是否能够出库*/
	selectReaPerformanceVerification: '/ReaManageService.svc/RS_UDTO_LotNoPerformanceVerification?',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.rea.client.out.use.Form', {
			region: 'north',
			height: 135,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			TakerObj: me.TakerObj,
			IsCheck: me.IsCheck,
			IsEquip: me.IsEquip,
			formtype: 'add',
			IsEmpOut: me.IsEmpOut,
			defaluteOutType: me.defaluteOutType
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.out.stock.App', {
			region: 'north',
			height: 200,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			IsEmpOut: me.IsEmpOut,
			StorageObj: me.StorageObj,
			barcodeOperType: me.barcodeOperType
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.use.DtlGrid', {
			header: false,
			region: 'center',
			layout: 'fit',
			itemId: 'DtlGrid',
			defaultLoad: false,
			IsEquip: me.IsEquip,
			OutScanCodeModel: me.OutScanCodeModel,
			defaluteOutType: me.defaluteOutType
		});
		return [me.Form, me.StockPanel, me.DtlGrid];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				text: '确认出库',
				iconCls: 'button-save',
				itemId: "btnSave",
				tooltip: '保存并完成出库',
				handler: function() {
					JShell.Action.delay(function() {
						me.onSaveClick('6', '出库完成', null);
						// 监听点击确认出库按钮时，将出库的弹框也一并关闭
						if (Ext.WindowManager.get(me.OTYPE)) {
							Ext.WindowManager.get(me.OTYPE).close();
						}
					}, null, 500);
				}
			}]
		};
		return dockedItems;
	},
	loadDatas: function(id, name) {
		var me = this;
		me.StorageObj.StorageID = id;
		me.StorageObj.StorageName = name;
		//清除库存表与出库明细表
		me.clearData();
		me.StockPanel.loadData(me.StorageObj);
	},
	onDelOne: function(tab) {
		var me = this;
		me.StockPanel.onDelOne(tab);
		//me.changeSumTotal();
	},
	changeSumTotal: function() {
		var me = this;
		var Total = me.DtlGrid.getSumTotal();
		var Price = me.Form.getComponent('ReaBmsOutDoc_TotalPrice');
		Price.setValue(Total);
	},
	datachanged: function(store) {
		var me = this;
		var bo = false;
		if (store.data.items.length > 0) {
			bo = true;
		}
		me.Form.setStorageReadOnly(bo);
	},
	onListeners: function() {
		var me = this;
		// 监听关闭面板时，将出库的弹框也一并关闭
		me.on({
			close: function() {
				if (Ext.WindowManager.get(me.OTYPE)) {
					Ext.WindowManager.get(me.OTYPE).close();
				}
			}
		});
		me.Form.on({
			setDefaultStorage: function(id, name) {
				JShell.Action.delay(function() {
					me.loadDatas(id, name);
				}, null, 500);
			}
		});
		me.DtlGrid.on({
			changeSumTotal: function() {
				me.changeSumTotal();
			},
			delclick: function(tab) {
				me.onDelOne(tab);
			}
		});
		me.DtlGrid.store.on({
			datachanged: function(store, eOpts) {
				me.datachanged(store);
			},
			// 需求调整：显示弹框事件
			onshowpanel: function(grid, bool, info,IsShowScan) {
				if (grid.isOpenDtlPanel) {
					me.showDtlPanel(grid, bool, info,IsShowScan);
				}
			}

		});
		me.StockPanel.on({
			itemdblclick: function(record, unitArr, barcode) {

				if (record.get('isChanageLotNo') == '') { // 减少请求服务器的次数
					// 条件列赋值（这个函数里面的ajax请求是同步的，目的在先将record结构改变，再在明细表中添加数据）
					me.onDataValid(record);
				}
				// 需求调整：在试剂加入出库明细表时，做性能验证的判断，性能通过的可以加入到明细表，不通过的不能加弹框提示！
				// 调用后台定制的服务
				me.onPerformanceVerification(record,function(bool,msg) {
					if(bool) {
						me.DtlGrid.addRecordOne(record, barcode);
					} else {
						// JShell.Msg.error('性能验证没有通过，不能出库！');
						JShell.Msg.error(msg);
						// 下面的代码用来解决：当双击一次性能未通过的试剂后，再次双击就没有弹框的信息提示了
						record.set('ReaBmsQtyDtl_SelectTag', '0');
					}
				});
				me.StockPanel.QtyDtlGrid.setScanCodeFocus();
				// 原来的操作
				// me.DtlGrid.addRecordOne(record, barcode);


			},
			itemdbselectlclick: function(record, barcode) {
				var itemsTab = me.DtlGrid.store.data.items;
				var unitArr = [];
				for (var i = 0; i < itemsTab.length; i++) {
					var tab = itemsTab[i].data.ReaBmsOutDtl_Tab + "";
					var tab1 = record.get('ReaBmsQtyDtl_Tab');
					/* 这里有个问题就是，当明细表中加入一个ReaBmsQtyDtl_Tab这个字段为空的数据后，在加入其它ReaBmsQtyDtl_Tab
					 这个字段为空的就会当前行数据已选择，要想用就要维护好数据。发现在Shell.class.rea.client.out.stock.Grid中有个changeResult方法
					 里面只对选择的试剂日期和更早日期的数据做了ReaBmsQtyDtl_Tab的处理，不知道为什么不处理所有的数据呢？
					 将方法中的if判断放开后，近效期grid中所有数据的ReaBmsQtyDtl_Tab都做了处理，这样在下面的判断中两个为空数据比对时，就不会报“当前数据已选择”
					 但是不知道对其他模块有没有影响，暂时测试的时候没有发现
					*/
					if (tab1 === tab) {
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
		if (me.Form.formtype == 'add') me.DtlGrid.store.removeAll();
	},
	removeData: function() {
		var me = this;
		me.StockPanel.clearData();
		me.DtlGrid.store.removeAll();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**确认人*/
	showUserForm: function(status, statusName, useID, userName) {
		var me = this;
		var config = {
			resizable: false,
			height: 150,
			width: 250,
			SUB_WIN_NO: '1',
			UserID: useID,
			UserName: userName,
			listeners: {
				save: function(p) {
					JShell.Action.delay(function() {
						me.onSave(status, statusName, p);
					}, null, 500);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.out.basic.AcceptForm', config).show();
	},
	/**出库保存服务*/
	onSaveClick: function(status, statusName, p) {
		var me = this;
		if (!me.Form.getForm().isValid()) return;
		var isAllowZero = false;
		if (me.PK) isAllowZero = true;
		//出库明细验证
		var check = me.DtlGrid.onSaveCheck(isAllowZero);
		if (check == false) return;

		var values = me.Form.getForm().getValues();
		if (me.IsCheck == '1') {
			me.showUserForm(status, statusName, values.ReaBmsOutDoc_ConfirmId, values.ReaBmsOutDoc_ConfirmName);
		} else {
			me.onSave(status, statusName, p);
		}
	},
	onSave: function(status, statusName, p) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		//获取总单信息
		var outDoc = me.getOutDocInfo();
		//获取明细
		var DtlInfo = me.DtlGrid.getOutDtlInfo();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			reaBmsOutDoc: outDoc,
			listReaBmsOutDtl: DtlInfo,
			isEmpOut: me.IsEmpOut
		});
		var btnSave = me.getComponent("buttonsToolbar").getComponent("btnSave");
		//设置保存按钮为隐藏或只读,防止用户多次点保存按钮重复提交setDisabled
		if (btnSave) btnSave.setDisabled(true);
		me.showMask("出库保存中...");
		me.BUTTON_CAN_CLICK = false; //

		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if (btnSave) btnSave.setDisabled(false);
			if (data.success) {
				if (p) {
					p.close();
				}

				//清空移库列表信息，防止重复提交
				me.DtlGrid.store.removeAll();
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取出库主单信息*/
	getOutDocInfo: function() {
		var me = this;
		var outDoc = me.Form.getAddParams();
		outDoc = me.changeOutDoc(outDoc);
		//确认时间
		if (outDoc.ConfirmId) {
			var sysdate = JcallShell.System.Date.getDate();
			sysdate = JcallShell.Date.toString(sysdate);
			outDoc.ConfirmTime = JShell.Date.toServerDate(sysdate);
		}
		return outDoc;
	},
	/**直接出库完成*/
	changeOutDoc: function(outDoc) {
		var me = this;
		var sysdate = JcallShell.System.Date.getDate();
		sysdate = JcallShell.Date.toString(sysdate);
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		outDoc.IsHasCheck = 1;
		outDoc.CheckID = usernId;
		outDoc.CheckName = username;
		outDoc.CheckTime = JShell.Date.toServerDate(sysdate);
		outDoc.IsHasApproval = 1;
		outDoc.ApprovalId = usernId;
		outDoc.ApprovalCName = username;
		outDoc.ApprovalTime = JShell.Date.toServerDate(sysdate);
		return outDoc;
	},
	/**改变record，给它增加两个用于判断批号和货运单号是否渲染的条件列赋值
	 * 如果上次出现了该批号，就将该条数据的isChanageLotNo赋值为noChange,
	 * 如果上次没有出现该批号，就将该数据的isChanageLotNo赋值为Change.
	 * 货运单号同上
	 * 给库存表的ReaBmsQtyDtl_LastLotNo上次批号赋值，ReaBmsQtyDtl_LastTransportNo上次货运单号赋值
	 * */
	onDataValid: function(record) {
		var me = this;
		// 要传的货品id
		var id = record.get('ReaBmsQtyDtl_GoodsID');
		// 本次的批号
		var lotNo = record.get('ReaBmsQtyDtl_LotNo');
		// 本次的货运单号
		var transportNo = record.get('ReaBmsQtyDtl_TransportNo');
		var url = (me.selectReaLastOutGoodUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectReaLastOutGoodUrl;
		url += 'goodsId=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				// 将库存表的上次批号赋值
				record.set('ReaBmsQtyDtl_LastLotNo', data.value.LastLotNo);
				// 将库存表的上次货运单号赋值
				record.set('ReaBmsQtyDtl_LastTransportNo', data.value.LastTransportNo);
				/**对批号的处理*/
				// 用来存放返回的上一次的批号组
				var lastLotNos = [];
				var lastLotNoStr = data.value.LastLotNo;
				if (lastLotNoStr == '') { // 上次没有这个批号出库，变色
					record.set('isChanageLotNo', 'Change');
					record.commit();
				} else {
					lastLotNos = lastLotNoStr.split("^");
					// 如果lastLotNos中有lotNo就返回它对应的索引值，没有返回-1
					var index = Ext.Array.indexOf(lastLotNos, lotNo);
					if (index == -1) { // 数组中没有,变色
						record.set('isChanageLotNo', 'Change');
						record.commit();
					} else { // 数组中有，不变色
						record.set('isChanageLotNo', 'noChange');
						record.commit();
					}

				}
				/**对货运单号的处理*/
				var lastTransportNo = [];
				var lastTransportNoStr = data.value.LastTransportNo;
				if (lastTransportNoStr == '') {
					record.set('isChanageTransportNo', 'Change');
					record.commit();
				} else {
					lastTransportNo = lastTransportNoStr.slice("^");
					var indexTra = Ext.Array.indexOf(lastTransportNo, transportNo);
					if (indexTra == -1) {
						record.set('isChanageTransportNo', 'Change');
						record.commit();
					} else {
						record.set('isChanageTransportNo', 'noChange');
						record.commit();
					}
				}
			}
		}, false);

	},
	/**
	 * 数据在加入出库明细表时，做性能验证的判断
	 * 如果试剂通过性能验证就可以成功加入到出库明细表，
	 * 否则就不能加如到出库明细表中，并提示用户“性能验证没有通过，不能出库！”
	 * 这个后台定制了一个服务
	 * */

	onPerformanceVerification: function(record, callback) {
		var me = this;
		// 货品id
		var goodsId = record.get('ReaBmsQtyDtl_GoodsID');
		// 货品批号
		var GoodsLot = record.get('ReaBmsQtyDtl_LotNo');
		var params = null;
		var msg = '';
		var url = (me.selectReaPerformanceVerification.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectReaPerformanceVerification;
		url += '&goodsId=' + goodsId + '&goodsLotNo=' + GoodsLot;
		JShell.Server.get(url, function(data) {
			params = data.success;
			msg = data.msg;
			callback(params,msg);
		});
	},
	
	/**出库时展示出库试剂相关信息的弹框
	 * */
	showDtlPanel: function(grid, bool, info,IsShowScan) {
		var me = this;
		var win = Ext.WindowManager.get(me.OTYPE);
		if (!win) {
			var config = {
				title: "货品信息(修改出库数或删除明细表中的数据弹框会隐藏)",
				resizable: false, // 尺寸是否可变，由于做没有自适应，内容无法动态改变，还是不让用户修改弹框尺寸了
				// resizable: true,
				maximizable: false,
				modal: false,
				closable: true, //关闭功能
				draggable: true, //移动功能
				floating: true, //浮动模式
				width: 330,
				height: 160,
				alwaysOnTop: true,
				itemId: me.OTYPE,
				id: me.OTYPE
			};
			win = JShell.Win.open('Shell.class.rea.client.out.basic.DtlInfo', config);
			Ext.WindowManager.register(win);
		}
		if (win) {
			//WIN宽高、位置
			var winHeight = me.getHeight();
			var winWidth = me.getWidth();
			var zIndex = me.zIndexManager.zseed + 100;
			var position = me.getPosition();
			var winPosition = [position[0] + winWidth - win.width - 200, winHeight - win.height - 425];
			win.initData(info);
			if (bool) {
				win.showAt(winPosition);
			} else {
				win.hide();
			}
			if(!IsShowScan){
				me.StockPanel.QtyDtlGrid.setScanCodeFocus();
			}
			// if(grid.hideTimes && grid.hideTimes > 0) {
			// 	JcallShell.Action.delay(function() {
			// 		win.hide();
			// 	}, null, grid.hideTimes);
			// }
		}
		
	}


});
