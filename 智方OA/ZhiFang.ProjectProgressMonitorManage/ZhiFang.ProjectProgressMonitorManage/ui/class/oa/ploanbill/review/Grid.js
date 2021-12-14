/**
 * 借款单借款一审(一审)
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.review.Grid', {
	extend: 'Shell.class.oa.ploanbill.basic.Grid',
	title: '借款一审',
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasRefresh: true,
	hiddenIsUse: true,
	hiddenRetract: true,
	/*借款状态不等于暂存*/
	defaultWhere: 'Status!=1',
	/*ReviewManID*/
	defaultUserType: '',
	/*日期范围类型默认值**/
	defaultDateTypeValue: 'ApplyDate',
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
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempStatus, itemArr[index]);
		});
		return tempStatus;
	},
	/**人员查询选择项过滤*/
	removeSomeSearchTypeList:function() {
		var me = this;
		var tempList = me.SearchTypeList;
		var itemArr = [];

		//去除一审人
		itemArr.push(tempList[2]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return 	tempList;
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = " Status!=1 and ReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "2": //申请
				me.openEditTabPanel(record, false);
				break;
			case "6": //二审退回
				me.openEditTabPanel(record, false);
				break;
//			case "3": //一审通过
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
	/**打开借款单审核应用*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var ApplyManID="";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID=record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.78;
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var minWidth=1120;
		maxWidth=maxWidth<=minWidth?minWidth:maxWidth;
		var config = {
			showSuccessInfo: false,
			PK: id,
			hasOperation: true,
			SUB_WIN_NO:'4',
			height: height,
			width: maxWidth,
			//minWidth:minWidth,
			//zindex: 10,
			//zIndex: 10,
			resizable: true,
			title: "借款一审",
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			hiddenPass: hiddenPass,
			ApplyManID:ApplyManID,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.oa.ploanbill.review.EditTabPanel', config).show();
	}
});