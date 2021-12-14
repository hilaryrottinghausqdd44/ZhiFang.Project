/**
 *预览退款申请三审
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.three.Grid', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.Grid',
	title: '退款发放',

	hasPrint: true,
	hiddenRetract: false,
	hiddenRefund: false,
	/**默认员工赋值*/
	hasDefaultUser: false,
	isSave: false,
	initComponent: function() {
		var me = this;
		//三审默认条件,((三审人=登录者) or (三审人=null and 状态=二审通过))
		me.defaultWhere = "((RefundThreeReviewManID=null and Status=6) or  (RefundThreeReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + "))";
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "6": //二审通过
				me.openEditTabPanel(record, false);
				break;
			case "8": //财务退回
				me.openEditTabPanel(record, false);
				break;
//			case "11": //退款异常
//				me.openEditTabPanel(record, false);
//				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		if(!tempStatus) return [];
		var itemArr = [];
		itemArr.push(tempStatus[0]);
		if(me.removeApply)
			itemArr.push(tempStatus[1]);
		//一审中
		itemArr.push(tempStatus[2]);
		//一审通过
		itemArr.push(tempStatus[3]);
		//一审退回
		itemArr.push(tempStatus[4]);
		//二审中
		itemArr.push(tempStatus[5]);
		//二审退回
		itemArr.push(tempStatus[7]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempStatus, itemArr[index]);
		});
		return tempStatus;
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.openShowTabPanel(records[0]);
	},
	/**打开退款发放应用*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var ApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.62;
		var height = document.body.clientHeight * 0.86;
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var minWidth = 620;
		maxWidth = maxWidth <= minWidth ? minWidth : maxWidth;
		var config = {
			showSuccessInfo: false,
			PK: id,
			SUB_WIN_NO: '6',
			hasOperation: true,
			RefundFormCode: record.get('MRefundFormCode'),
			height: height,
			width: maxWidth,
			resizable: true,
			title: "退款发放",
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			hiddenPass: hiddenPass,
			ApplyManID: ApplyManID,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.refund.three.EditTabPanel', config).show();
	},
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("Status").toString();
		if(status == "9") {
			me.openPreviewForm(records[0]);
		} else {
			JShell.Msg.alert("退款申请单状态必须为【财务打款通过】方可操作!", null, 2500);
		}
	},
	/**打开预览窗口*/
	openPreviewForm: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.72;
		var height = document.body.clientHeight - 30;
		var id = record.get("Id");
		var config = {
			width: maxWidth,
			height: height,
			PK: id,
			title: '预览退款单PDF文件',
			hasColse: true,
			closeAction: 'hide',
			resizable: true,
			hasBtntoolbar: true,
			listeners: {
				onCloseClick: function(win) {
					//me.onSearch();
					win.hide();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.weixin.ordersys.refund.show.PreviewPDF', config).show();
		win.showPdf();
	},

	getSaveParams: function(rec, Result, operationMemo) {
		var me = this;
		var entity = {
			Id: rec.get("Id"),
			Result: Result,
			RefundFormCode: rec.get("MRefundFormCode"),
			Reason: me.entityValues.ViewInfo
		};
		if(rec.get("RefundType")){
			entity.RefundType=rec.get("RefundType");
		}
		if(rec.get("BankID")){
			entity.BankID=rec.get("BankID");
		}
		if(rec.get("RefundPrice")){
			entity.BankAccount=rec.get("RefundPrice");
		}
		if(rec.get("BankTransFormCode")){
			entity.BankTransFormCode=rec.get("BankTransFormCode");
		}
		var params = {
			entity: entity
		};
		return params;
	},
	updateStatus: function(params) {
		var me = this;
		var memo = "财务退回";
		var params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		var url = JShell.System.Path.ROOT + '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormThreeReview';
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.hideMask(); //隐藏遮罩层
				JShell.Msg.alert(memo + "操作成功", null, 1000);
				me.fireEvent('save', me);
				me.ViewInfoWin.close();
			} else {
				me.hideMask();
				JShell.Msg.error(memo + "操作失败!<br />" + data.msg);
			}
		});
	},
	/**@overwrite 财务打款按钮点击处理方法*/
	onRefundClick: function(rec) {
		var me = this;
		me.openPayTabPanel(rec);
	},
	/**@overwrite 打回按钮点击处理方法*/
	onRetractClick: function(rec) {
		var me = this;
		var id = rec.get("Id");
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams(rec, 0, "财务退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("财务退回意见", callback);
	},
	openViewInfoForm: function(title, callback) {
		var me = this;
		me.isSave = false;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '3',
			resizable: false,
			title: title,
			formtype: 'add',
			zindex: 10,
			listeners: {
				save: function(win, values) {
					me.entityValues = values;
					me.isSave = true;
					me.ViewInfoWin = win;
					if(callback != null) {
						callback();
					}
					//win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.refund.basic.ViewInfoForm', config).show();
	},
	/**打开退款单申请应用*/
	openPayTabPanel: function(record) {
		var me = this;
		var UOFID = "",
			id = '',
			Status = 1;
		var hiddenButtonRetract = true;
		if(record != null) {
			id = record.get('Id');
			UOFID = record.get('UOFID');
			Status = record.get('Status').toString();
		}
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '1',
			/**用户订单ID*/
			UOFID: UOFID,
			Status: Status,
			resizable: true,
			title: "退款单",
			formtype: 'edit',
			RefundFormCode: record.get('MRefundFormCode'),
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id && id != null && id != "") {
			config.formtype = 'edit';
			config.PK = id;
			config.hasOperation = true;
			title: "退款单";
		}
		JShell.Win.open('Shell.class.weixin.ordersys.refund.three.PayTabPanel', config).show();
	}
});