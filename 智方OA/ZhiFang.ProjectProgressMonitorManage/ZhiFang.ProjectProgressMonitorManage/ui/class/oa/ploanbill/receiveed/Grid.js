/**
 * 借款单借款检查并打款
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.receiveed.Grid', {
	extend: 'Shell.class.oa.ploanbill.basic.Grid',
	title: '领款确认',
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasRefresh: true,
	hiddenIsUse: true,
	hiddenRetract: true,
	/*借款状态不等于暂存*/
	defaultWhere: 'Status!=1',
	/*ApplyManID*/
	defaultUserType: '',
	/*日期范围类型默认值PayDate**/
	defaultDateTypeValue: '',
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
	initComponent: function() {
		var me = this;
		//领款确认 默认条件,申请人id为登录者自己
		me.defaultWhere = " Status!=1 and ApplyManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		//打款
		var status = record.get('Status').toString();
		switch(status) {
			case "11":
				me.openEditTabPanel(record, false);
				break;
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
	/**领款人确认*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var id = "";
		var ApplyManID="";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID=record.get('ApplyManID');
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
			hasOperation: true,
			SUB_WIN_NO:'9',
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			resizable: false,
			title: "领款确认",
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
		JShell.Win.open('Shell.class.oa.ploanbill.receiveed.EditTabPanel', config).show();
	}
});