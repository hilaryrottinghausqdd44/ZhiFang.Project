/**
 * 输血过程记录:发血血袋信息(出库明细列表)
 * @description 分步批量登记(分为输血结束前登记及输血结束登记)
 * @author longfc
 * @version 2020-08-06
 */
Ext.define('Shell.class.blood.nursestation.transrecord.batchregister.OutDtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '发血血袋信息',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	hasPagingtoolbar: true,
	hasRownumberer: false,
	hasRefresh: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	autoSelect: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutItem_Bloodstyle_Id',
		direction: 'ASC'
	}, {
		property: 'BloodBOutItem_CourseCompletion',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'batchregister.OutDtlGrid',
	/**用户UI配置Name*/
	userUIName: "发血血袋信息",
	//发血主单ID
	PK: null,
	//发血主单ID
	outDocId: null,
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr: null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '血制品',
			dataIndex: 'BloodBOutItem_Bloodstyle_CName',
			width: 115,
			defaultRenderer: true
		}, {
			text: '血袋号',
			dataIndex: 'BloodBOutItem_BBagCode',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '产品码',
			dataIndex: 'BloodBOutItem_Pcode',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '惟一码',
			dataIndex: 'BloodBOutItem_B3Code',
			width: 110,
			defaultRenderer: true
		}, {
			text: '血容量',
			dataIndex: 'BloodBOutItem_BOutCount',
			width: 55,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodBOutItem_BloodBUnit_BUnitName',
			width: 50,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '失效时间',
			dataIndex: 'BloodBOutItem_InvalidDate',
			width: 85,
			hidden: true,
			isDate: true,
			hasTime: false,
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
			if (columns[i].dataIndex && columns[i].xtype != "checkcolumn") {
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
		var params = [];
		if (me.outDocId) {
			params.push("bloodboutitem.BloodBOutForm.Id='" + me.outDocId + "'");
		} else if (me.outDtlIdStr && me.outDtlIdStr.length > 0) { //当前选择的发血明细Id
			var outDtlIdStr = "'" + me.outDtlIdStr.split(",").join("','") + "'";
			if (outDtlIdStr) params.push("bloodboutitem.Id in (" + outDtlIdStr + ")");
		}

		return params;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	}
});
