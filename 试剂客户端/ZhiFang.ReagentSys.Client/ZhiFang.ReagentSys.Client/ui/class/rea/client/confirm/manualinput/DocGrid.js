/**
 * 客户端手工验收
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.DocGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DocGrid',

	title: '验货单信息列表',

	/**是否多选行*/
	checkOne: true,
	OTYPE: "manualinput",
	ACCEPTMODEL:"1",
	/**默认单据状态*/
	defaultStatusValue: "",
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.manualinput.DocGrid',
	/**用户UI配置Name*/
	userUIName: "手工验收列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(0);
	},
	initComponent: function() {
		var me = this;
		//手工验收
		me.defaultWhere = "reabmscensaledocconfirm.SourceType=1";
		if(!me.checkOne) me.setCheckboxModel();
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增验收',
			tooltip: '新增验收',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "继续验收",
			tooltip: "对待继续验收继续验货",
			handler: function() {
				me.onContinueToAcceptClick();
			}
		});
		if(me.ACCEPTMODEL!="0"){
			items.push({
				xtype: 'button',
				iconCls: 'button-check',
				itemId: "btnCheck",
				text: '确认验收',
				tooltip: '确认验收',
				handler: function() {
					me.onConfirmClick();
				}
			});
			items.push("-", {
				xtype: 'button',
				iconCls: 'button-accept',
				itemId: "btnStorage",
				text: '入库',
				tooltip: '入库',
				handler: function() {
					me.onStorageClick();
				}
			});
		}
		items.push('->');
		return items;
	},
	/**确认验收*/
	onConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsCenSaleDocConfirm_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
		var statusName = "";
		if(StatusEnum) {
			statusName = StatusEnum[status];
		}
		if(status != "0") {
			JShell.Msg.error("当前状态为【" + statusName + "】,不能确认验收!");
			return;
		}
		//验收确认弹出录入
		me.onOpenCheckForm(records[0]);
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	}
});