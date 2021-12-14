/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.use.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',

	/**1:直接出库;3:支持直接出库及按出库申请进行出库确认*/
	TYPE: '1',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**是否按出库人权限出库 2,false否,1,TRUE是*/
	IsEmpOut: false,
	/**用户UI配置Key*/
	userUIKey: 'out.use.DocGrid',
	/**用户UI配置Name*/
	userUIName: "使用出库列表",
	/**出库单状态默认选择值*/
	defaultStatus: '7',
	/**出库类型*/
	defaluteOutType: '1',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.setDisabledAdd();
		me.onSetDateArea(-1);
	},
	initComponent: function() {
		var me = this;
		//直接出库时,单据状态默认为"出库完成"
		if(me.TYPE=="1")me.defaultStatus="6";
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
			text: '新增出库',
			tooltip: '新增出库',
			iconCls: 'button-add',
			itemId: 'bntAdd',
			handler: function() {
				me.onAddClick();
			}
		}, {
			text: '确认出库',
			tooltip: '确认出库',
			iconCls: 'button-accept',
			itemId: 'btnAccept',
			hidden: true,
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.onAcceptClick(records[0]);
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
	/**显示表单*/
	openAddPanel: function(takerObj) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			TakerObj: takerObj,
			/**库存新增仪器是否允许为空,1是,0否*/
			IsEquip: me.IsEquip,
			/**按领用人权限出库,true 是,false否*/
			IsEmpOut: me.IsEmpOut,
			defaluteOutType: me.defaluteOutType,
			/**出库扫码模式(严格模式:1,混合模式：2)*/
			OutScanCodeModel: me.OutScanCodeModel,
			listeners: {
				save: function(p, records) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
					p.close();
					me.onSearch();
				}
			}
		};
		//完成出库时是否需要出库确认
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocUseIsCheck", false, function(data) {
			var paraValue = "0";
			if(data.success) {
				var obj = data.value;
				if(obj.ParaValue) paraValue = obj.ParaValue;
			}
			config.IsCheck = paraValue;
			JShell.Win.open('Shell.class.rea.client.out.use.AddPanel', config).show();
		});
	},
	setDisabledAdd: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			bntAdd = buttonsToolbar.getComponent('bntAdd');
		if(!bntAdd) return;

		if(me.TYPE == "1") { //直接出库
			if(bntAdd) bntAdd.setDisabled(true);
			return;
		}
		//是否需要支持直接出库;1:是(新增出库按钮显示);2:否(新增出库按钮隐藏);
		JcallShell.REA.RunParams.getRunParamsValue("ISNeedSupportDirectOut", false, function(data) {
			var paraValue = "1";
			if(data.success) {
				var obj = data.value;
				if(obj.ParaValue) paraValue = "" + obj.ParaValue;
				var disabled = true;
				if(paraValue != "1") disabled = false;
				if(!bntAdd) bntAdd.setDisabled(disabled);
			}
		});
	},
	/**新增出库(直接出库完成)*/
	onAddClick: function() {
		var me = this;
		var takerObj = {};
		if(me.OutboundIsLogin) {
			if(me.OutboundIsLogin == "2") {
				takerObj = me.getAddDelfaultValue();
			}
			me.openAddPanel(takerObj);
		} else {
			me.getOutTakerParaVal(function(val) {
				if(me.OutboundIsLogin == "2") {
					takerObj = me.getAddDelfaultValue();
				}
				me.openAddPanel(takerObj);
			});
		}
	},
	getAddDelfaultValue: function() {
		var me = this;
		var takerObj = {};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || "";
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		takerObj.TakerId = userId;
		takerObj.TakerName = userName;
		takerObj.DeptId = deptId;
		takerObj.DeptName = deptName;
		return takerObj;
	},
	onBtnChange: function(rec) {
		var me = this;
		//直接出库
		if(me.TYPE == "1") return;

		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			btnAccept = buttonsToolbar.getComponent('btnAccept');
		btnAccept.setVisible(false);
		if(!rec) return;
		var status = rec.get('ReaBmsOutDoc_Status');
		//审批通过
		if(status == '7') {
			btnAccept.setVisible(true);
		}
	},
	/**对出库申请进行确认出库*/
	onAcceptClick: function(record) {
		var me = this;
		var visible = "" + record.get("ReaBmsOutDoc_Visible");
		if(visible == "false" || visible == "0") {
			JShell.Msg.error("当前出库申请单已作废!");
			return;
		}
		var status = record.get("ReaBmsOutDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum;
		var statusName = "";
		if(StatusEnum)
			statusName = StatusEnum[status];
		if(status != "7") {
			JShell.Msg.error("当前出库申请单状态为【" + statusName + "】,不能进行确认出库!");
			return;
		}
		var id = record.get('ReaBmsOutDoc_Id');
		me.openAcceptPanel(id);
	},
	openAcceptPanel: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			defaluteOutType: me.defaluteOutType,
			/**库存新增仪器是否允许为空,1是,0否*/
			IsEquip: me.IsEquip,
			IsEmpOut: me.IsEmpOut,
			/**出库扫码模式(严格模式:1,混合模式：2)*/
			OutScanCodeModel: me.OutScanCodeModel,
			PK: id,
			listeners: {
				save: function(p, records) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
					p.close();
					me.onSearch();
				}
			}
		};
		//完成出库时是否需要出库确认
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocUseIsCheck", false, function(data) {
			var paraValue = "0";
			if(data.success) {
				var obj = data.value;
				if(obj.ParaValue) paraValue = obj.ParaValue;
			}
			config.IsCheck = paraValue;
			JShell.Win.open('Shell.class.rea.client.out.accept.AddPanel', config).show();
		});
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		switch(me.TYPE) {
			case '1': //直接出库
				me.typeByHQL = '2';
				break;
			case '2': //出库管理（申请）
				me.typeByHQL = '3';
				break;
			case '3': //出库管理（全部）
				me.typeByHQL = '4';
				break;
			default:
				me.typeByHQL = '4';
				break;
		}
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		//直接出库
		if(me.TYPE == "1") {
			if(tempList[7]) removeArr.push(tempList[7]);
			if(tempList[6]) removeArr.push(tempList[6]);
		}
		//只保留全部和出库完成
		if(tempList[5]) removeArr.push(tempList[5]);
		if(tempList[4]) removeArr.push(tempList[4]);
		if(tempList[3]) removeArr.push(tempList[3]);
		if(tempList[2]) removeArr.push(tempList[2]);
		if(tempList[1]) removeArr.push(tempList[1]);
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	}
});