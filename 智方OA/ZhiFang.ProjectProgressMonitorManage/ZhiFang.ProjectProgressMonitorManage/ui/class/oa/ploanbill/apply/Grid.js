/**
 * 借款单申请
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.apply.Grid', {
	extend: 'Shell.class.oa.ploanbill.basic.Grid',
	title: '借款申请',
	hasAdd: true,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasRefresh: true,
	hiddenIsUse: false,
	hiddenRetract: false,
	/*借款状态选择不显示暂存*/
	removeApply: false,
	/*借款状态不等于暂存*/
	defaultWhere: 'Status!=1',
	/*ApplyManID*/
	defaultUserType: '',
	/*文档日期范围类型默认值**/
	defaultDateTypeValue: 'ApplyDate',
	/*是否显示启/禁用列**/
	isShowIsUseColumn: true,
	isRemoveApplyManID: true,
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
	initComponent: function() {
		var me = this;

		me.defaultWhere = "ApplyManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		if(me.removeApply)
			itemArr.push(tempList[1]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	/**人员查询选择项过滤*/
	removeSomeSearchTypeList: function() {
		var me = this;
		var tempList = me.SearchTypeList;
		var itemArr = [];
		//去除申请人
		if(me.isRemoveApplyManID) {
			itemArr.push(tempList[1]);
		}
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	onAddClick: function() {
		var me = this;
		me.openApplyAppForm(null);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get('Status').toString();
		switch(status) {
			case "1": //
				me.openApplyAppForm(records[0]);
				break;
//			case "2": //申请状态,当部门审核人没有进行审核时可以进行撤回操作
//				var reviewDate = records[0].get('ReviewDate');
//				if(reviewDate == null || reviewDate == "") {
//					me.openApplyAppForm(records[0], false);
//				} else {
//					me.openShowTabPanel(records[0]);
//				}
//				break;
			case "4": //一审退回
				me.openApplyAppForm(records[0], false);
				break;
			default:
				me.openShowTabPanel(records[0]);
				break;
		}
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
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "1": //
				me.openApplyAppForm(record, false);
				break;
//			case "2": //申请状态,当部门审核人没有进行审核时可以进行撤回操作
//				var reviewDate = record.get('ReviewDate');
//				if(reviewDate == null || reviewDate == "") {
//					me.openApplyAppForm(record, false);
//				} else {
//					me.openShowTabPanel(record);
//				}
//				break;
			case "4": //一审退回
				me.openApplyAppForm(record, false);
				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},

	/**打开新增或编辑借款单申请应用*/
	openApplyAppForm: function(record, hiddenPass) {
		var me = this;
		var ApplyManID = "";
		var id = "",
			Status = 1;
		var hiddenButtonRetract = true;
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
			Status = record.get('Status').toString();
			var ReviewDate = record.get('ReviewDate').toString();
			//如果是申请状态,并且一审时间为空,可以显示撤回按钮
			if(Status == "2" && (ReviewDate == "" || ReviewDate == null)) {
				hiddenButtonRetract = false;
			}
		}
		var maxWidth = document.body.clientWidth * 0.80;
		var height = document.body.clientHeight * 0.74;
		height = (height > 375 ? 375 : height);
		maxWidth = (maxWidth > 1030 ? 1030 : maxWidth);
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var config = {
			showSuccessInfo: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			hiddenButtonRetract: hiddenButtonRetract,
			Status: Status,
			resizable: true,
			title: "借款申请",
			formtype: 'add',
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
		if(id && id != null && id != "") {
			config.formtype = 'edit';
			config.PK = id;
			config.hasOperation = true;
			title: "编辑借款单";
		}
		JShell.Win.open('Shell.class.oa.ploanbill.apply.AddTabPanel', config).show();
	}
});