/**
 * 借款单借款检查并打款
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.pay.Grid', {
	extend: 'Shell.class.oa.ploanbill.basic.Grid',
	title: '借款检查并打款',
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasPrint: true,
	hasRefresh: true,
	hiddenIsUse: true,
	hiddenRetract: true,
	/*借款状态不等于暂存*/
	defaultWhere: 'Status!=1',
	/*PayManID*/
	defaultUserType: '',
	/*日期范围类型默认值FourReviewDate**/
	defaultDateTypeValue: '',
	/**默认员工赋值*/
	hasDefaultUser: false,
	SearchTypeList: [
		["", "不过滤"],
		["ApplyManID", "申请人"],
		["ReviewManID", "一审人"],
		["TwoReviewManID", "核对人"],
		["ThreeReviewManID", "特殊审批人"],
		["FourReviewManID", "借款复核人"],
		["PayManID", "出纳检查打款人"],
		["ReceiveManID", "领款人"]
	],
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		var itemArr = [];
		if(me.removeApply)
			itemArr.push(tempStatus[1]);
		//申请也需要去除
		itemArr.push(tempStatus[2]);
		//一审通过
		itemArr.push(tempStatus[3]);
		//一审退回
		itemArr.push(tempStatus[4]);
		//二审通过
		itemArr.push(tempStatus[5]);
		//二审退回
		itemArr.push(tempStatus[6]);
		//三审通过
		itemArr.push(tempStatus[7]);
		//三审退回
		itemArr.push(tempStatus[8]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempStatus, itemArr[index]);
		});
		return tempStatus;
	},
	/**人员查询选择项过滤*/
	removeSomeSearchTypeList:function() {
		var me = this;
		var tempList = me.SearchTypeList;
		return 	tempList;
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = "(PayManID is null and Status=9 ) or (PayManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + ")";
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "9": //四审通过
				me.openEditTabPanel(record, false);
				break;
				//			case "11": //打款
				//				me.openEditTabPanel(record, true);
				//				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},
	onEditClick: function() {
		var me = this;
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
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("Status").toString();
		if(status == "11") {
			me.openPreviewForm(records[0]);
		} else {
			JShell.Msg.alert("借款单状态必须为【打款】方可操作!", null, 1000);
		}
	},
	/**打开预览窗口*/
	openPreviewForm: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth - 320;
		var height = document.body.clientHeight - 30;
		var id = record.get("Id");
		var config = {
			width: maxWidth,
			height: height,
			PK: id,
			title: '预览借款单PDF文件',
			hasColse: true,
			closeAction: 'hide',
			resizable: true, //可变大小功能
			hasBtntoolbar: true,
			listeners: {
				onCloseClick: function(win) {
					//me.onSearch();
					win.hide();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.oa.ploanbill.basic.PreviewApp', config).show();
		win.showPdf();
	},
	/**打开借款检查并打款应用*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var ApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.84;
		var minWidth=(maxWidth<1147?1147:maxWidth);
		var height = document.body.clientHeight * 0.88;
		height=(height<560?560:height);
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var config = {
			showSuccessInfo: false,
			PK: id,
			SUB_WIN_NO:'8',
			hasOperation: true,
			height: height,
			width: maxWidth,
//			minWidth: minWidth,
//			zindex: 10,
//			zIndex: 10,
			resizable: true,
			title: "借款检查并打款",
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
		JShell.Win.open('Shell.class.oa.ploanbill.pay.EditTabPanel', config).show();
	}
});