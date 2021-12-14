/**
 * 医生奖金结算申请
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.Grid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '医生奖金结算申请',
	hasDel: false,
	hasRefresh: true,
	/**是否包含结算功能按钮*/
	hasSettlement: true,
	hasDeleteColumn: true,
	searchByMonthUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchSettlementApplyInfoByBonusFormRound',
	EditTabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.apply.EditTabPanel',
	initComponent: function() {
		var me = this;
		//初始化时间
		me.initDate();
		me.callParent(arguments);
	},
	/**初始化时间*/
	initDate: function() {
		var me = this;
		var date = JcallShell.System.Date.getDate();
		if(!date) date = JShell.Date.getNextDate(new Date());
		date = JShell.Date.getNextDate(date, -60);
		var year = date.getFullYear();
		var minMonth = date.getMonth() + 1;
		minMonth = (minMonth <= 9 ? "0" + minMonth : "" + minMonth);
		me.minYearValue = year;
		me.bonusFormRoundMinValue = year + "-" + minMonth;
		//console.log(me.bonusFormRoundMinValue);
	},
	/**创建结算功能按钮栏Items*/
	createSettlementItems: function() {
		var me = this;
		var items = [];
		items.push({
			width: 165,
			labelWidth: 65,
			fieldLabel: '结算周期',
			xtype: 'uxYearAndMonthComboBox',
			itemId: 'BonusFormRound',
			minYearValue: me.minYearValue,
			minValue: me.bonusFormRoundMinValue,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						setTimeout(function() {
							me.onSettlementApply();
						}, 500);
					}
				}
			}
		}, {
			width: 80,
			iconCls: 'button-add',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '结算制单',
			tooltip: '<b>按照自然月去制单结算</b>',
			handler: function() {
				me.onSettlementApply();
			}
		});

		return items;
	},
	/**结算申请*/
	onSettlementApply: function() {
		var me = this;
		buttonsToolbar = me.getComponent('toolbarSettlement');
		var bonusFormRound = buttonsToolbar.getComponent('BonusFormRound').getValue();
		var showInfo = "";
		var isExec = true;
		var applyInfo = null;
		if(!bonusFormRound) {
			showInfo = showInfo + "结算周期不能为空!<br />";
			isExec = false;
		}
		if(isExec) {
			var url = JShell.System.Path.ROOT + me.searchByMonthUrl;
			url += "?bonusFormRound=" + bonusFormRound;
			JShell.Server.get(url, function(data) {
				if(data.success) {
					var isSettlement = false;
					if(data.value) {
						applyInfo = data.value;
						var isSettlement = applyInfo.IsSettlement;
						if(isSettlement == true) {
							isExec = false;
							showInfo = "结算周期为" + bonusFormRound + "已经结算申请!<br/>请不要重复结算申请!";
						} else {
							if(applyInfo.OSDoctorBonusForm == null) {
								isExec = false;
								showInfo = "结算周期为" + bonusFormRound + "的结算数据为空!";
							}
							if(isExec == true && applyInfo.OSDoctorBonusList == null) {
								isExec = false;
								showInfo = "结算周期为" + bonusFormRound + "的结算数据为空!";
							}

						}
					}
				} else {
					isExec = false;
					showInfo = me.errorFormat.replace(/{msg}/, data.msg);
				}
			}, false);
		}
		if(isExec == true) {
			me.openApplyTabPanel(applyInfo);
		} else {
			JShell.Msg.alert(showInfo); //, null, 2000
		}
	},

	/**打开医生奖金结算单申请应用*/
	openApplyTabPanel: function(applyInfo) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var minWidth = (maxWidth < 720 ? 720 : maxWidth);
		var height = document.body.clientHeight - 40;
		var config = {
			showSuccessInfo: false,
			height: height,
			minWidth: minWidth,
			width: maxWidth,
			SUB_WIN_NO: '1',
			resizable: true,
			formtype: 'add',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			applyInfo: applyInfo,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.ApplyTabPanel', config).show();
	},
	checkSatusApply: function(record) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "1": //暂存
				me.openEditTabPanel(record, false);
				break;
			case "5": //一审退回
				me.openEditTabPanel(record, false);
				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		me.checkSatusApply(record);
	}
});