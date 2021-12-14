/**
 * 检查并打款
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.pay.EditBonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**默认加载数据*/
	defaultLoad: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	hasButtontoolbar: true,
	//heckOne: true,
	/**导出excel*/
	hasExportExcel: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			selectionchange: function(model, selected, eOpts) {
				me.setLabelInfo(selected);
			},
			nodata: function(p) {
				me.setLabelInfo(null);
			}
		});
	},
	setLabelInfo: function(selected) {
		var me = this;
		var text = '';
		var totalAmount = 0,
			curCount = 0;
		if(selected && selected.length > 0) {
			for(var i = 0; i < selected.length; i++) {
				var record = selected[i];
				var status = "" + record.get("Status");
				//检查并打款,打款完成
				if(status != "9" && status != "11") {
					curCount += 1;
					var orderFormAmount = record.get("OrderFormAmount");
					var amount = record.get("Amount");
					var percent = record.get("Percent");

					if(orderFormAmount) orderFormAmount = parseFloat(orderFormAmount);
					if(amount) amount = parseFloat(amount);
					if(percent) percent = parseFloat(percent);

					if(orderFormAmount > 0 && amount > 0 && percent <= 0) {
						percent = (parseFloat(amount / orderFormAmount) * 100).toFixed(2);
						record.set('Percent', percent);
					}
					if(orderFormAmount > 0 && percent > 0 && amount <= 0) {
						amount = parseFloat(orderFormAmount * percent * 0.01).toFixed(2);
						record.set('Amount', amount);
					}
					if(orderFormAmount > 0 && percent > 0 && amount > 0) totalAmount += amount;
				}
			}
		}
		if(totalAmount > 0) {
			totalAmount = parseFloat(totalAmount).toFixed(2);
			text += '<b style="color:red">发放总额:</b><b style="color:blue">' + totalAmount + '</b><b style="color:red">元</b>';
			text += '<b style="color:red">,发放数量:</b><b style="color:blue">' + curCount + "。</b>";
		}
		text += '<b style="color:red">请确保帐号资金充足。</b>'
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var labelInfo = buttonsToolbar.getComponent("labelInfo");
		if(labelInfo) {
			labelInfo.setValue(text);
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onDoctorBonusPayOne');
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.push('-', {
			fieldLabel: '设置发放方式',
			labelWidth: 85,
			width: 165,
			hasStyle: true,
			tooltip: '设置奖金明细列表里勾选中行的发放方式',
			hidden: me.hiddenPaymentMethod,
			xtype: 'uxSimpleComboBox',
			itemId: 'cboPaymentMethod',
			value: "1",
			data: [
				["", "请选择"],
				["1", "微信支付"],
				["2", "银行转账"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					if(com.getValue()) me.onSetPaymentMethod(com.getValue());
				}
			}
		});
		items.push('-', {
			xtype: 'displayfield',
			name: 'labelInfo',
			itemId: 'labelInfo',
			style: {
				marginLeft: "5px"
			},
			value: '<b style="color:red">请确保帐号资金充足。</b>'
		});
		return items;
	},
	onSetPaymentMethod: function(value) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		for(var i in records) {
			var record = records[i];
			var status = "" + record.get("Status");
			//检查并打款,打款完成
			if(status != "9" && status != "11") {
				record.set("PaymentMethod", value);
				record.commit();
			}
		}
		me.getView().refresh();
	},
	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(15, 0, {
			xtype: 'actioncolumn',
			text: '发放',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-check hand',
				tooltip: '发放',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onDoctorBonusPayOne', me, rec);
				}
			}]
		});
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			hidden: me.hideDelColumn,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}
				return v;
			}
		});
		return columns
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);
		//申请
		if(tempList[2]) itemArr.push(tempList[2]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});