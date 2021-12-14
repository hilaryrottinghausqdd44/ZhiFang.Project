/**
 * @description 注册证将过期报警
 * @author liangyl	
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.registerwarning.willexpire.QtyGrid', {
	extend: 'Shell.class.rea.client.registerwarning.basic.QtyGrid',

	title: '注册证将过期报警',
	width: 800,
	height: 500,

	/**默认加载数据*/
	defaultLoad: false,
	/**效期预警类型:2:效期已过期报警;3:效期将过期报警*/
	qtyType: 3,
	/**库存合并条件：默认不合并*/
	groupType: 0,
	/**预警类型:注册证将过期预警*/
	AlertTypeId: '6',
	/**注册证预警默认将过期预警天数*/
	willexpireDays: 10,
	/**用户UI配置Key*/
	userUIKey: 'registerwarning.willexpire.QtyGrid',
	/**用户UI配置Name*/
	userUIName: "注册证将过期报警",

	initComponent: function() {
		var me = this;
		me.initDefaultWillexpireDays();
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
			fieldLabel: '再过',
			labelWidth: 40,
			width: 135,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '天数选择',
			name: 'ValidityDay',
			itemId: 'ValidityDay',
			xtype: 'numberfield',
			minValue: 0,
			maxValue: 10000,
			value: me.willexpireDays,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '天过期的注册证',
			labelSeparator: '',
			width: 95
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
			value: '说明:<b style="color:blue;">符合数据条件:注册证启用 并且有效期大于当前服务器时间及有效期小于(当前服务器时间加上输入的天数值)的时间;</b>'
		});
		return items;
	},
	/**注册证预警默认将过期预警天数*/
	initDefaultWillexpireDays: function() {
		var me = this;
		if(!me.willexpireDays) me.willexpireDays = 10;
		var willexpireDays2 = JcallShell.REA.RunParams.Lists["RegistWillexpireWarning"].Value;
		if(!willexpireDays2) {
			JShell.REA.RunParams.getRunParamsValue("RegistWillexpireWarning", false, function(data) {
				willexpireDays2 = JcallShell.REA.RunParams.Lists.RegistWillexpireWarning.Value;
				if(willexpireDays2 && parseInt(me.willexpireDays) != parseInt(willexpireDays2)) {
					me.willexpireDays = willexpireDays2;
				}
			});
		}
		if(willexpireDays2 && parseInt(me.willexpireDays) != parseInt(willexpireDays2)) {
			me.willexpireDays = willexpireDays2;
		}
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var days = buttonsToolbar.getComponent('ValidityDay').getValue();
		if(days && days >= 0) {
			var dateValue = me.calcDateArea(days);
			//开始预警日期
			if(dateValue.end) {
				where.push("reagoodsregister.RegisterInvalidDate>='" + JShell.Date.toString(dateValue.end, true) + " 00:00:00'");
			}
			//有效期至
			if(dateValue.start) {
				where.push("reagoodsregister.RegisterInvalidDate<'" + JShell.Date.toString(dateValue.start, true) + " 23:59:59'");
			}
		}
		return where.join(" and ");
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var AlertTypeList = me.AlertTypeList||[];
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
					var upperdate = JShell.Date.toString(JShell.Date.getNextDate(edate, Upper), true);
					var lowerdate = JShell.Date.toString(JShell.Date.getNextDate(edate, Lower), true);
					var curDate= JShell.Date.toString(edate, true);
					if((nvalidDate > curDate) && (InvalidDate >= lowerdate) && (InvalidDate <= upperdate)) {
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