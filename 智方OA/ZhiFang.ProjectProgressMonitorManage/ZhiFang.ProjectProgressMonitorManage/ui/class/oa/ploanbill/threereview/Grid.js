/**
 * 借款单借款核对(三审,特殊审核)
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.threereview.Grid', {
	extend: 'Shell.class.oa.ploanbill.basic.Grid',
	title: '特殊审批',
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasRefresh: true,
	hiddenIsUse: true,
	hiddenRetract: true,
	/*借款状态不等于暂存*/
	defaultWhere: 'Status!=1',
	/*ThreeReviewManID*/
	defaultUserType: '',
	/*日期范围类型默认值TwoReviewDate**/
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
	initComponent: function() {
		var me = this;
		//三审默认条件,IsSpecially=1 and ((三审人=登录者) or (三审人=null and 状态=二审通过))
		me.defaultWhere = "IsSpecially=1 and ((ThreeReviewManID=null and Status=5) or  (ThreeReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + "))";
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();

		switch(status) {
			case "5": //二审通过
				me.openEditTabPanel(record, false);
				break;
			case "10": //四审退回
				var isSpecially = record.get('IsSpecially').toString();
				if(isSpecially.toLowerCase() == "true" || isSpecially == "1") {
					me.openEditTabPanel(record, false);
				} else {
					me.openShowTabPanel(record);
				}
				break;
				//			case "7": //三审通过
				//				me.openEditTabPanel(record, true);
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
		var itemArr = [];
		if(me.removeApply)
			itemArr.push(tempStatus[1]);
		//申请也需要去除
		itemArr.push(tempStatus[2]);
		//一审通过
		itemArr.push(tempStatus[3]);
		//一审退回
		itemArr.push(tempStatus[3]);
		itemArr.push(tempStatus[4]);
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
	/**打开借款核对应用*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var ApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;
		var minWidth = 1120;
		maxWidth = maxWidth <= 1120 ? 1120 : maxWidth;
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var config = {
			showSuccessInfo: false,
			PK: id,
			SUB_WIN_NO:'6',
			hasOperation: true,
			height: height,
			width: maxWidth,
//			minWidth: minWidth,
//			zindex: 10,
//			zIndex: 10,
			resizable: true,
			title: "特殊审批",
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
		JShell.Win.open('Shell.class.oa.ploanbill.threereview.EditTabPanel', config).show();
	}
});