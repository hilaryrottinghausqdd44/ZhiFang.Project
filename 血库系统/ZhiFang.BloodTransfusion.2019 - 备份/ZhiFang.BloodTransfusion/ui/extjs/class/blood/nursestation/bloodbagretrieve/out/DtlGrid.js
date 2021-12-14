/**
 * 护士站--血袋回收--发血血袋信息
 * @author longfc
 * @version 2020-03-13
 */
Ext.define('Shell.class.blood.nursestation.bloodbagretrieve.out.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '发血血袋信息',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	hasRefresh:true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	autoSelect: true,
	//发血主单ID
	PK:null,
	
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutItem_Bloodstyle_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'bloodbagretrieve.out.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "发血血袋信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
	
		if (me.hasRefresh) items.push('refresh');
		//查询框信息
		me.searchInfo = {
			width: 135,
			itemId:"search",
			emptyText: '血袋号/血制品',
			isLike: true,
			fields: ['bloodboutitem.BBagCode', 'bloodboutitem.Bloodstyle.CName']
		};
		items=me.createSearchButtonsToolbar(items);
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createSearchButtonsToolbar: function(items) {
		var me = this;
	
		if (!items) items = [];
		items.push({
			fieldLabel: '',
			name: 'RecoverCompletion',
			itemId: 'RecoverCompletion',
			xtype: 'uxSimpleComboBox',
			width: 105,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未回收', 'color:#FFB800;'],
				//['2', '部分回收', 'color:#00BFFF;'],
				['3', '回收完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			listeners: {
				click: function(but) {
					me.onSearch();
				}
			}
		},'-',{
			xtype: 'button',
			iconCls: 'button-add',
			text: '登记',
			hidden:true,
			tooltip: '对选中的第血袋进行新增登记',
			listeners: {
				click: function(but) {
					me.onAddTrans();
				}
			}
		});
		return items;
	},	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			xtype: 'checkcolumn',
			dataIndex: 'BloodBOutItem_CheckColumn',
			text: '<b style="color:blue;">选择</b>',
			width: 40,
			align: 'center',
			hidden:true,
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '血制品',
			dataIndex: 'BloodBOutItem_Bloodstyle_CName',
			width: 120,
			defaultRenderer: true
		},{
			text: '回收完成度',
			dataIndex: 'BloodBOutItem_RecoverCompletion',
			width: 75,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分回收";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "回收完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未回收";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '血袋号',
			dataIndex: 'BloodBOutItem_BBagCode',
			width: 110,
			defaultRenderer: true
		}, {
			text: '产品码',
			dataIndex: 'BloodBOutItem_Pcode',
			width: 90,
			defaultRenderer: true
		}, {
			text: '惟一码',
			dataIndex: 'BloodBOutItem_B3Code',
			width: 150,
			hidden:true,
			defaultRenderer: true
		},{
			text: '血型名称',
			dataIndex: 'BloodBOutItem_BloodABO_CName',
			width: 70,
			defaultRenderer: true
		}, {
			text: '血容量',
			dataIndex: 'BloodBOutItem_BOutCount',
			width: 60,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodBOutItem_BloodBUnit_BUnitName',
			width: 60,
			defaultRenderer: true
		}, {
			text: '失效时间',
			dataIndex: 'BloodBOutItem_InvalidDate',
			width: 95,
			isDate: true,
			hasTime: false,
			defaultRenderer: true
		}, {
			text: '采集时间',
			dataIndex: 'BloodBOutItem_CollectDate',
			width: 125,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '申请主单Id',
			dataIndex: 'BloodBOutItem_BloodBReqForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		},{
			text: '申请明细编号',
			dataIndex: 'BloodBOutItem_BloodBReqItem_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '发血主单编号',
			dataIndex: 'BloodBOutItem_BloodBOutForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '血制品编码',
			dataIndex: 'BloodBOutItem_Bloodstyle_Id',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '血型编码',
			dataIndex: 'BloodBOutItem_BloodABO_Id',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '单位编码',
			dataIndex: 'BloodBOutItem_BloodBUnit_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		},{
			text: '输血记录主单Id',
			dataIndex: 'BloodBOutItem_BloodTransForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '发血明细单号',
			dataIndex: 'BloodBOutItem_Id',
			isKey: true,
			width: 70,
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	getStoreFields: function(isString) {
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			fields = [];
	
		for (var i = 0; i < length; i++) {
			if (columns[i].dataIndex&&columns[i].xtype!="checkcolumn") {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
	
		return fields;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		me.internalWhere = params.join(" and ");
		return me.callParent(arguments);
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var recoverCompletion = buttonsToolbar.getComponent('RecoverCompletion');
		var params = [];
		if (me.PK)params.push("bloodboutitem.BloodBOutForm.Id='" + me.PK+"'");
		if (recoverCompletion) {
			var value = recoverCompletion.getValue();
			if (value) {
				if (value == "1") {//未回收
					params.push("(bloodboutitem.RecoverCompletion!=3 or bloodboutitem.RecoverCompletion is null)");
				} else {
					params.push("bloodboutitem.RecoverCompletion=" + value);
				}
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				params.push("(" + me.getSearchWhere(value) + ")");
			}
		}
		return params;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "请选择发血单后再操作!");
			me.getView().update(error);
			return false;
		}
				
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	onAddTrans: function() {
		var me = this;
		me.fireEvent('onAddTrans', me);
	}
});
