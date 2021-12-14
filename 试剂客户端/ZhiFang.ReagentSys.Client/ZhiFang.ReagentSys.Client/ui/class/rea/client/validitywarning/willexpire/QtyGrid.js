/**
 * @description 效期将过期报警
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.validitywarning.willexpire.QtyGrid', {
	extend: 'Shell.class.rea.client.validitywarning.basic.QtyGrid',

	title: '效期将过期报警',
	width: 800,
	height: 500,

	/**默认加载数据*/
	defaultLoad: false,
	/**效期预警类型:2:效期已过期报警;3:效期将过期报警*/
	qtyType: 3,
	/**库存合并条件：默认不合并*/
	groupType: 0,
	/**预警类型:库存效期将过期预警*/
	AlertTypeId: '4',
	/**效期预警默认将过期预警天数*/
	willexpireDays: 10,
	/**用户UI配置Key*/
	userUIKey: 'validitywarning.willexpire.QtyGrid',
	/**用户UI配置Name*/
	userUIName: "效期将过期报警",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
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
			fieldLabel: '天过期的货品',
			labelSeparator: '',
			width: 85
		});
		items.push('-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 115,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 135,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		items.push('-', {
			boxLabel: '合并',
			name: 'cbMerge',
			itemId: 'cbMerge',
			xtype: 'checkboxfield',
			inputValue: 'true',
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {}
			}
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 165,
			hasStyle: true,
			disabled: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			data: dataList,
			listeners: {
				select: function(com, records, eOpts) {
					me.groupType = com.getValue();
					me.onSearch();
				}
			}
		});
		items.push('-', {
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
			width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		return items;
	},
	/**获取效期预警默认已过期天数*/
	initDefaultWillexpireDays: function() {
		var me = this;
		if(!me.willexpireDays) me.willexpireDays = 10;
		var willexpireDays2 = JcallShell.REA.RunParams.Lists["ExpirationAlarmWillexpireDefaultDays"].Value;
		if(!willexpireDays2) {
			JShell.REA.RunParams.getRunParamsValue("ExpirationAlarmWillexpireDefaultDays", false, function(data) {
				willexpireDays2 = JcallShell.REA.RunParams.Lists.ExpirationAlarmWillexpireDefaultDays.Value;
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
				where.push("reabmsqtydtl.InvalidDate>='" + JShell.Date.toString(dateValue.end, true) + " 00:00:00'");
			}
			//有效期至
			if(dateValue.start) {
				where.push("reabmsqtydtl.InvalidDate<'" + JShell.Date.toString(dateValue.start, true) + " 23:59:59'");
			}
		}
		return where.join(" and ");
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: [{
				xtype: 'displayfield',
				itemId: "btnInfo",
				disabled: false,
				value: '说明:<b style="color:blue;">符合数据条件:库存数量大于0 并且有效期大于当前服务器时间及有效期小于(当前服务器时间加上输入的天数值)的时间;</b>'
			}]
		};

		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		cmReaBmsStatisticalType.disable();
		cbMerge.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue) {
					cmReaBmsStatisticalType.enable();
				} else {
					cmReaBmsStatisticalType.setValue('');
					me.groupType = cmReaBmsStatisticalType.getValue();
					cmReaBmsStatisticalType.disable();
					me.onSearch();
				}

			}
		});
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		if(cbMerge.getValue()) return;
		cmReaBmsStatisticalType.disable();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var AlertTypeList = me.AlertTypeList || [];
		if(data.list && data.list.length > 0) {
			for(var i = 0; i < data.list.length; i++) {
				//有效期
				var invalidDate = data.list[i].ReaBmsQtyDtl_InvalidDate;
				invalidDate = JShell.Date.toString(invalidDate, true);
				var color = '';
				for(var j = 0; j < AlertTypeList.length; j++) {
					var lower = AlertTypeList[j].ReaAlertInfoSettings_StoreLower;
					var upper = AlertTypeList[j].ReaAlertInfoSettings_StoreUpper;
					if(!lower) lower = 0;
					if(!upper) upper = 0;
					lower = Number(lower);
					upper = Number(upper);
					var edate = JcallShell.System.Date.getDate();
					var upperdate = JShell.Date.toString(JShell.Date.getNextDate(edate, upper), true);
					var lowerdate = JShell.Date.toString(JShell.Date.getNextDate(edate, lower), true);
					var alertColor = AlertTypeList[j].ReaAlertInfoSettings_AlertColor;
					var curDate= JShell.Date.toString(edate, true);
					if((invalidDate > curDate) && (invalidDate >= lowerdate) && (invalidDate < upperdate)) {
						color = alertColor;
						break;
					}
				}
				data.list[i]["ReaBmsQtyDtl_Color"] = color;
			}
		}
		return data;
	}
});