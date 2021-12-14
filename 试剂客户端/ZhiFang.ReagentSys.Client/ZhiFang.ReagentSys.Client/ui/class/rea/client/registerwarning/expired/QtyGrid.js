/**
 * @description 注册证已过期
 * @author liangyl
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.registerwarning.expired.QtyGrid', {
	extend: 'Shell.class.rea.client.registerwarning.basic.QtyGrid',

	title: '效期已过期',
	width: 800,
	height: 500,

	/**效期预警类型:2:效期已过期报警;3:效期将过期报警*/
	qtyType: 2,
	/**库存合并条件：默认不合并*/
	groupType: 0,
	/**预警类型:注册证已过期预警*/
	AlertTypeId: '5',
	/**效期已过期默认已过期天数*/
	expiredDays: 1,
	/**用户UI配置Key*/
	userUIKey: 'registerwarning.expired.QtyGrid',
	/**用户UI配置Name*/
	userUIName: "效期已过期",

	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		//me.columns = me.createGridColumns();
		if(me.AlertTypeList.length <= 0) {
			me.getAlertByAlertType(function(data) {
				if(data && data.value) {
					me.AlertTypeList = data.value.list;
				}
			});
		}
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var dataList = [];
		if(JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey]) {
			dataList = JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey].List;
		}
		var items = ['refresh'];

		items.push('-', {
			fieldLabel: '已过期',
			labelWidth: 45,
			width: 135,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '天数选择',
			name: 'ValidityDay',
			itemId: 'ValidityDay',
			xtype: 'numberfield',
			minValue: 0,
			maxValue: 10000,
			value: me.expiredDays,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '天的注册证',
			labelSeparator: '',
			width: 65
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			style: {
				marginLeft: "5px"
			},
			handler: function() {
				me.onSearch();
			}
		});
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			hidden: true,
			width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		items.push('-', {
			xtype: 'displayfield',
			itemId: "btnInfo",
			disabled: false,
			value: '说明:<b style="color:blue;">符合数据条件:注册证启用并且有效期小于当前服务器时间及有效期大于等于(当前服务器时间减去已过期的天数)的时间;</b>'
		});
		return items;
	},
	/**效期已过期默认已过期天数*/
	initDefaultExpiredDays: function() {
		var me = this;
		if(!me.expiredDays) me.expiredDays = 1;
		var expiredDays2 = JcallShell.REA.RunParams.Lists["RegistWarnExpiredDefaultDays"].Value;
		if(!expiredDays2) {
			JShell.REA.RunParams.getRunParamsValue("RegistWarnExpiredDefaultDays", false, function(data) {
				expiredDays2 = JcallShell.REA.RunParams.Lists.RegistWarnExpiredDefaultDays.Value;
				if(expiredDays2 && parseInt(me.expiredDays) != parseInt(expiredDays2)) {
					me.expiredDays = expiredDays2;
				}
			});
		}
		if(expiredDays2 && parseInt(me.expiredDays) != parseInt(expiredDays2)) {
			me.expiredDays = expiredDays2;
		}
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var days = buttonsToolbar.getComponent('ValidityDay').getValue();
		if(days && days >= 0) {
			var dateValue = me.calcDateArea(-days);
			//InvalidWarningDate
			if(dateValue.start)
				where.push("reagoodsregister.RegisterInvalidDate>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
			if(dateValue.end)
				where.push("reagoodsregister.RegisterInvalidDate<'" + JShell.Date.toString(dateValue.end, true) + " 23:59:59'");
		}
		return where.join(" and ");
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var AlertTypeList = me.AlertTypeList || [];
		if(data.list && data.list.length > 0) {
			for(var i = 0; i < data.list.length; i++) {
				//注册证有效期
				var InvalidDate = data.list[i].ReaGoodsRegister_RegisterInvalidDate;
				InvalidDate = JShell.Date.toString(InvalidDate, true);
				var color = '';
				for(var j = 0; j < AlertTypeList.length; j++) {
					var Lower = AlertTypeList[j].ReaAlertInfoSettings_StoreLower;
					var Upper = AlertTypeList[j].ReaAlertInfoSettings_StoreUpper;
					var AlertColor = AlertTypeList[j].ReaAlertInfoSettings_AlertColor;
					if(!Lower) Lower = 0;
					if(!Upper) Upper = 0;
					Lower = Number(Lower);
					Upper = Number(Upper);
					var edate = JcallShell.System.Date.getDate();
					var upperdate = JShell.Date.toString(JShell.Date.getNextDate(edate, -Upper), true);
					var lowerdate = JShell.Date.toString(JShell.Date.getNextDate(edate, -Lower), true);
					var curDate= JShell.Date.toString(edate, true);
					if((nvalidDate < curDate) && (InvalidDate <= lowerdate) && (InvalidDate >= upperdate)) {
						color = AlertColor;
						break;
					}
				}
				data.list[i]["ReaGoodsRegister_Color"] = color;
			}
		}
		return data;
	}
});