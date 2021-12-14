/**
 * 输血申请综合查询:血袋接收&回收
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.recover.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '血袋接收&回收',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationVOOfByBReqFormID?isPlanish=true',
	/**默认查询条件:1:领用;2:接收;3:回收;*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	//申请单号
	PK: null,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	hasPagingtoolbar: false,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBagOperation_Bloodstyle_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.recover.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "血袋接收&回收",
	
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
			text: '申请单号',
			dataIndex: 'BloodBagOperationVO_BloodBReqForm_Id',
			width: 60,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BloodBReqForm_Id";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		},{
			text: '血袋号',
			dataIndex: 'BloodBagOperationVO_BBagCode',
			width: 135,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BBagCode";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '产品码',
			dataIndex: 'BloodBagOperationVO_PCode',
			width: 135,
			hidden:true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_PCode";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		},  {
			text: '惟一码',
			dataIndex: 'BloodBagOperationVO_BloodBOutItem_B3Code',
			width: 125,
			hidden:true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BloodBOutItem_B3Code";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		},{
			text: '血制品编码',
			dataIndex: 'BloodBagOperationVO_Bloodstyle_Id',
			width: 135,
			hidden: true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperationVO_Bloodstyle_Id";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '血制品',
			dataIndex: 'BloodBagOperationVO_Bloodstyle_CName',
			width: 135,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_Bloodstyle_CName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '接收人',
			dataIndex: 'BloodBagOperationVO_BloodBagHandover_BagOper',
			width: 95,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BagOper";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '接收时间',
			dataIndex: 'BloodBagOperationVO_BloodBagHandover_BagOperTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BagOperTime";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '接收运送人',
			dataIndex: 'BloodBagOperationVO_BloodBagHandover_Carrier',
			width: 95,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_Carrier";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '回收科室',
			dataIndex: 'BloodBagOperationVO_BloodBagRecover_DeptCName',
			width: 115,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_DeptCName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '回收人',
			dataIndex: 'BloodBagOperationVO_BloodBagRecover_BagOper',
			width: 95,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BagOper";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '回收时间',
			dataIndex: 'BloodBagOperationVO_BloodBagRecover_BagOperTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_BagOperTime";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '回收运送人',
			dataIndex: 'BloodBagOperationVO_BloodBagRecover_Carrier',
			width: 95,
			flex: 1,
			defaultRenderer: true,
			doSort: function(state) {
				var field = "BloodBagOperation_Carrier";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}];
		return columns;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if(!me.PK) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		if(params && params.length > 0) me.internalWhere = params.join(" and ");
		var url = me.callParent(arguments);
		//申请单号参数
		if(me.PK) url = url + "&reqFormId=" + me.PK;

		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];

		return params;
	}
});