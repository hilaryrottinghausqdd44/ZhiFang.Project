/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.apply.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',

	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsOutDocByHQL?isPlanish=true',
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField',
	/**出库新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	/**是否按出库人权限出库 2,false否,1,TRUE是*/
	IsEmpOut: false,
	/**出库单状态默认选择值*/
	defaultStatus: '1',
	ReaBmsOutDocStatus: 'ReaBmsOutDocStatus',
	/**用户UI配置Key*/
	userUIKey: 'out.apply.DocGrid',
	/**用户UI配置Name*/
	userUIName: "出库申请列表",
	/**出库申请类型:1:出库申请;2:出库申请+;*/
	TYPE: "1",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSetDateArea(-10);
	},
	initComponent: function() {
		var me = this;
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
			text: '出库申请',
			tooltip: '出库申请',
			iconCls: 'button-add',
			itemId: 'btnAdd',
			handler: function() {
				me.onAddClick();
			}
		}, {
			text: '继续申请',
			tooltip: '继续申请',
			iconCls: 'button-edit',
			itemId: 'Edit',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.onEditClick(records[0]);
			}
		}, {
			text: '作废',
			tooltip: '作废',
			iconCls: 'button-del',
			itemId: 'Invalid',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.onInvalidClick(records[0]);
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
	/**新增申请*/
	onAddClick: function() {
		var me = this;
		me.openAddPanel();
	},
	/**申请编辑*/
	onEditClick: function(record) {
		var me = this;
		var visible = "" + record.get("ReaBmsOutDoc_Visible");
		if(visible == "false" || visible == "0") {
			JShell.Msg.error("当前出库申请单已作废!");
			return;
		}
		var status = record.get("ReaBmsOutDoc_Status");
		var statusEnum = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum;
		var statusName = "";
		if(statusEnum)
			statusName = statusEnum[status];
		//暂存,审核退回
		if(status != "1" && status != "5") {
			JShell.Msg.error("当前出库申请单状态为【" + statusName + "】,不能编辑!");
			return;
		}
		var id = record.get('ReaBmsOutDoc_Id');
		me.openAddPanel(id);
	},
	/**打开新增或编辑App*/
	openAddPanel: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			TYPE: me.TYPE,
			IsEmpOut: me.IsEmpOut,
			listeners: {
				save: function(p, records) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
					p.close();
					me.onSearch();
				}
			}
		};
		if(id) {
			config.PK = id;
		}
		JShell.Win.open('Shell.class.rea.client.out.apply.AddPanel', config).show();
	},
	onBtnChange: function(rec) {
		var me = this;
	},
	/**作废*/
	onInvalidClick: function(record) {
		var me = this;
		var visible = "" + record.get("ReaBmsOutDoc_Visible");
		if(visible == "false" || visible == "0") {
			JShell.Msg.error("当前出库申请单已作废!");
			return;
		}
		var status = record.get("ReaBmsOutDoc_Status");
		var statusEnum = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum;
		var statusName = "";
		if(statusEnum)
			statusName = statusEnum[status];
		//暂存,审核退回
		if(status != "1" && status != "5") {
			JShell.Msg.error("当前出库申请状态为【" + statusName + "】,不能作废!");
			return;
		}
		var msg = '您确定要作废出库单吗';
		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;
			me.onInvalidOfCheck(record, "2");
		});
	},
	onInvalidOfCheck: function(record, status) {
		var me = this;
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var sysdate = JcallShell.System.Date.getDate();
		sysdate = JcallShell.Date.toString(sysdate);

		var outDoc = {
			"Id": record.get("ReaBmsOutDoc_Id"),
			"Visible":"0",
			"Status": status
		};
		var fields = ["Id", "Status","Visible"];
		var params = {
			"entity": outDoc,
			"fields": fields.join(",")
		};
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocByCheck';
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				JShell.Msg.alert('作废出库单成功', null, 2000);
				me.onSearch();
			} else {
				JShell.Msg.error('作废出库单失败！' + data.msg);
			}
		});
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		me.searchStatusValue = tempList;
		return tempList;
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '1';
	}
});