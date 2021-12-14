/**
 * 输血申请综合查询:申请血液制品信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.req.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '申请血液制品信息',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	hasPagingtoolbar: false,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemEntityListByJoinHql?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认每页数量*/
	defaultPageSize: 100,
	//申请单号
	PK: null,	
	/**排序字段*/
	defaultOrderBy: [ {
		property: 'BloodBReqItem_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.req.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "申请血液制品信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSearch();
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
			text: '申请明细单号',
			dataIndex: 'BloodBReqItem_Id',
			hidden:true,
			width: 160,
			isKey: true,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '血制品',
			dataIndex: 'BloodBReqItem_BloodCName',
			width: 135,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '申请数量',
			dataIndex: 'BloodBReqItem_BReqCount',
			width: 115,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodBReqItem_BloodUnitCName',
			width: 95,
			defaultRenderer: true
		}, {
			text: 'ABO',
			dataIndex: 'BloodBReqItem_BloodBReqForm_HisABOCode',
			width: 95,
			defaultRenderer: true
		}, {
			text: 'RH',
			dataIndex: 'BloodBReqItem_BloodBReqForm_HisrhCode',
			width: 120,
			flex:1,
			defaultRenderer: true
		}];
		return columns;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.PK) return false;
				
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		if(params&&params.length>0)me.internalWhere = params.join(" and ");
		
		var url = me.callParent(arguments);
		if(me.PK){
			//申请明细联查申请主单
			url=url+"&docHql=1=1&bloodstyleHql=1=1";
		}
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		if(me.PK){
			params.push("bloodbreqitem.BReqFormID='"+me.PK+"'");
		}
		return params;
	}
});
