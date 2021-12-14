/**
 * 出库审批
 * @author longfc
 * @version 2019-03-18
 */
Ext.define('Shell.class.rea.client.out.approval.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',

	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsOutDocByHQL?isPlanish=true',
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField',
	/**出库新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	/**是否按出库人权限出库 2,false否,1,TRUE是*/
	IsEmpOut: false,
	/**出库单状态默认选择值*/
	defaultStatus: '4',
	ReaBmsOutDocStatus: 'ReaBmsOutDocStatus',
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**用户UI配置Key*/
	userUIKey: 'out.approval.DocGrid',
	/**用户UI配置Name*/
	userUIName: "出库审批列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSetDateArea(-10);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = "reabmsoutdoc.Visible=1";
		//初始化参数
		me.initOutParams();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
		//初始化参数
	initOutParams: function() {
		var me = this;
		me.initRunParams();
		me.changeType();
		var isUseEmpOut = me.IsEmpOut ? 1 : 2;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.selectUrl += '&empId=' + userId + '&type=' + me.typeByHQL + '&isUseEmpOut=' + isUseEmpOut;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		if(!items) items = [];
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-', {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '审批通过',
			tooltip: '只对当前选择的出库申请单进行审批通过',
			handler: function() {
				me.onCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '审批退回',
			tooltip: '只对当前选择的出库申请单进行审批退回',
			handler: function() {
				me.onUnCheckClick();
			}
		});
		return buttonToolbarItems;
	},
	initRunParams: function() {
		var me = this;
		me.callParent(arguments);
		me.IsEquip = '0';
		//是否必填(2:不启用;1:启用)
		me.getIsEquipParaVal(function(val) {
			val = val + '';
			if(val == '1') {
				me.IsEquip = '1';
			}
		});
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		//审核退回,已申请,申请作废,暂存
		if(tempList[5]) removeArr.push(tempList[5]);
		if(tempList[3]) removeArr.push(tempList[3]);
		if(tempList[2]) removeArr.push(tempList[2]);
		if(tempList[1]) removeArr.push(tempList[1]);
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '4';
	},
	/**@description 审批按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var visible = "" + records[0].get("ReaBmsOutDoc_Visible");
		if(visible == "false" || visible == "0") {
			JShell.Msg.error("当前出库申请单已作废!");
			return;
		}
		var status = records[0].get("ReaBmsOutDoc_Status");
		var statusEnum = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum;
		var statusName = "";
		if(statusEnum)
			statusName = statusEnum[status];
		//审核通过
		if(status != "4") {
			JShell.Msg.error("当前出库申请单状态为【" + statusName + "】!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审批通过操作</div>',
			msg: '审批意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;

			var checkMemo = text;
			if(checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.onSaveOfCheck(records[0], "7", checkMemo);
		});
	},
	/**@description 审批退回按钮点击处理方法*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var visible = "" + records[0].get("ReaBmsOutDoc_Visible");
		if(visible == "false" || visible == "0") {
			JShell.Msg.error("当前出库申请单已作废!");
			return;
		}
		var status = records[0].get("ReaBmsOutDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum;
		var statusName = "";
		if(StatusEnum)
			statusName = StatusEnum[status];
		//审核通过
		if(status != "4") {
			JShell.Msg.error("当前出库申请单状态为【" + statusName + "】!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审批退回操作</div>',
			msg: '审批意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;

			var checkMemo = text;
			if(checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.onSaveOfCheck(records[0], "8", checkMemo);
		});
	},
	onSaveOfCheck: function(record, status, checkMemo) {
		var me = this;
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var sysdate = JcallShell.System.Date.getDate();
		sysdate = JcallShell.Date.toString(sysdate);

		var outDoc = {
			"Id": record.get("ReaBmsOutDoc_Id"),
			"Status": status,
			"IsHasApproval": 1,
			"ApprovalId": usernId,
			"CheckName": username,
			"ApprovalCName": JShell.Date.toServerDate(sysdate),
			"ApprovalMemo": checkMemo
		};
		var fields = ["Id", "Status"]; //, "IsHasApproval", "ApprovalId", "ApprovalTime", "ApprovalMemo"
		var params = {
			"entity": outDoc,
			"fields": fields.join(",")
		};
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocByCheck';
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				JShell.Msg.alert('出库申请审批操作保存成功', null, 2000);
				me.onSearch();
			} else {
				JShell.Msg.error('出库申请审批操作保存失败！' + data.msg);
			}
		});
	}
});