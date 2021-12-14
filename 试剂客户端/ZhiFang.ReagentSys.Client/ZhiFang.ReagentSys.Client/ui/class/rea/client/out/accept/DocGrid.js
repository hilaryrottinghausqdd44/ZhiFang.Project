/**
 * 出库总单
 * @author liangyl
 * @version 2018-10-31
 */
Ext.define('Shell.class.rea.client.out.accept.DocGrid', {
	extend: 'Shell.class.rea.client.out.use.DocGrid',

	title: '出库总单列表',
	defaultWhere: '',
	/**暂存状态值*/
	searchStatusValue: '7',
	/**用户UI配置Key*/
	userUIKey: 'out.accept.DocGrid',
	/**用户UI配置Name*/
	userUIName: "使用出库列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-', {
			text: '确认出库',
			tooltip: '确认出库',
			iconCls: 'button-accept',
			itemId: 'btnAccept',
			hidden:true,
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
	/**对出库申请进行确认出库*/
	onAcceptClick: function(record) {
		var me = this;
		var visible = "" + records.get("ReaBmsOutDoc_Visible");
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
		me.openAddPanel(id);
	},
	/**显示表单*/
	openAddPanel: function(id) {
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
			/**按领用人权限出库,true 是,false否*/
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
	onBtnChange: function(rec) {
		var me = this;
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
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		if(tempList[1]) removeArr.push(tempList[1]);
		if(tempList[2]) removeArr.push(tempList[2]);

		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	}
});