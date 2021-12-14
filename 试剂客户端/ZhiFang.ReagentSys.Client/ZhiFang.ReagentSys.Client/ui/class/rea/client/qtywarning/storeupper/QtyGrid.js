/**
 * @description 高库存预警
 * @author longfc
 * @version 2018-03-23
 */
Ext.define('Shell.class.rea.client.qtywarning.storeupper.QtyGrid', {
	extend: 'Shell.class.rea.client.qtywarning.basic.QtyGrid',

	title: '高库存预警',
	width: 800,
	height: 500,

	/**预警类型(1:低库存：2：高库存)*/
	warningType: 2,
	/**当前库存百分比值*/
	storePercent: 100,
	/**库存合并条件：默认不合并*/
	groupType: 0,
	/**库存预警类型:高库存预警*/
	AlertTypeId: '2',
	/**比较值为动态值时的最近几个月的默认值*/
	monthValue: 3,
	/**用户UI配置Key*/
	userUIKey: 'qtywarning.storeupper.QtyGrid',
	/**用户UI配置Name*/
	userUIName: "高库存预警列表",
	AlertTypeList: [],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, function(result) {});
		JShell.REA.StatusList.getStatusList(me.QtyWarningComparisonValueTypeKey, false, false, function(result) {});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		if(me.AlertTypeList.length <= 0) {
			me.getAlertByAlertType(function(data) {
				if(data && data.value) {
					me.AlertTypeList = data.value.list;
				}
			});
		}
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var dataList = [],
			comparisonTypeList = [];
		if(JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey]) {
			dataList = JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey].List;
		}

		if(JShell.REA.StatusList.Status[me.QtyWarningComparisonValueTypeKey]) {
			comparisonTypeList = JShell.REA.StatusList.Status[me.QtyWarningComparisonValueTypeKey].List;
		}
		var items = []; //'refresh','-'
		items.push({
			fieldLabel: '高于上限的',
			labelWidth: 65,
			width: 120,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '',
			name: 'StorePercent',
			itemId: 'StorePercent',
			xtype: 'numberfield',
			minValue: 0,
			value: me.storePercent,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.storePercent=newValue;
				},
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '%的货品',
			labelSeparator: '',
			width: 65
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
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {}
			}
		}, {
			fieldLabel: '',
			emptyText: '合并类型',
			labelWidth: 0,
			width: 100,
			hasStyle: true,
			disabled: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			data: dataList,
			value: me.defaultStatisticalTypeValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.groupType = com.getValue();
					me.onSearch();
				}
			}
		}, '-', {
			fieldLabel: '',
			emptyText: '比较值选择',
			labelWidth: 0,
			//readOnly: true,
			width: 155,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmComparisonType',
			name: 'cmComparisonType',
			data: comparisonTypeList,
			value: "1",
			listeners: {
				select: function(com, records, eOpts) {
					me.comparisonType = com.getValue();
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '按最近',
			labelWidth: 50,
			width: 105,
			labelSeparator: '',
			labelAlign: 'left',
			emptyText: '',
			name: 'Month',
			itemId: 'Month',
			xtype: 'numberfield',
			minValue: 1,
			maxValue: 12,
			value: me.monthValue,
			listeners: {
				validitychange: function(field, isValid, e) {
					var value = field.getValue();
					if(value) {
						if(value > 1 && value < 12) {
							return true;
						} else {
							return false;
						}
					} else {
						return false;
					}
				},
				change: function(field, newValue, oldValue, e) {
					if(newValue > 1 && newValue < 12) {
						me.monthValue = newValue;
					} else if(newValue < 1) {
						field.setValue(1);
					} else if(newValue > 12) {
						field.setValue(12);
					}
				},
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		}, {
			xtype: 'displayfield',
			fieldLabel: '个月使用量预警',
			labelSeparator: '',
			width: 95
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
				value: '说明:<b style="color:blue;">符合数据条件的计算公式:库存数量>库存上限值×查询输入项值/100;</b>'
			}]
		};
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
		var storePercent=me.storePercent;
		var alertTypeList = me.AlertTypeList||[];
		
		if(data.list && data.list.length > 0) {
			for(var i = 0; i < data.list.length; i++) {
				//库存低限
				var goodsQty = data.list[i].ReaBmsQtyDtl_GoodsQty;
				var storeUpper = data.list[i].ReaBmsQtyDtl_StoreUpper;
				var comparisonValue = data.list[i].ReaBmsQtyDtl_ComparisonValue;
				if(!goodsQty) goodsQty = 0;
				if(!comparisonValue) comparisonValue = 0;
				comparisonValue = Number(comparisonValue);
				goodsQty = Number(goodsQty);
				var color = '';
				
				var calcComparisonValue =0;
				/* if(comparisonValue!=0)calcComparisonValue=(goodsQty-comparisonValue)/comparisonValue*100;
				if(!calcComparisonValue)calcComparisonValue =0;
				calcComparisonValue = Number(calcComparisonValue); */
				data.list[i].ReaBmsQtyDtl_CalcComparisonValue=comparisonValue;
				
				for(var j = 0; j < alertTypeList.length; j++) {
					var lower = alertTypeList[j].ReaAlertInfoSettings_StoreLower;
					var upper = alertTypeList[j].ReaAlertInfoSettings_StoreUpper;
					var alertColor = alertTypeList[j].ReaAlertInfoSettings_AlertColor;
					if(!lower) lower = 0;
					if(!upper) upper = 0;
					lower = Number(lower);
					upper = Number(upper);
					if(comparisonValue >= lower && comparisonValue <= upper) {
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