/**
 * 输血申请综合查询:血袋入库信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.in.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '血袋入库信息',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID?isPlanish=true',
	/**只能获取到可配置的系统参数*/
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
		property: 'BReqComplexOfInInfoVO_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.in.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "血袋入库信息",
	
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
			text: '交叉配血明细Id',
			dataIndex: 'BReqComplexOfInInfoVO_BPreItemId',
			width: 135,
			hidden:true,
			defaultRenderer: true
		}, {
			text: '献血编号',
			dataIndex: 'BReqComplexOfInInfoVO_BBagCode',
			width: 125,
			defaultRenderer: true
		},{
			text: '产品码',
			dataIndex: 'BReqComplexOfInInfoVO_PCode',
			width: 125,
			hidden:true,
			defaultRenderer: true
		},{
			text: '惟一码',
			dataIndex: 'BReqComplexOfInInfoVO_B3Code',
			width: 125,
			hidden:true,
			defaultRenderer: true
		},{
			text: '血制品',
			dataIndex: 'BReqComplexOfInInfoVO_Bloodstyle_CName',
			width: 135,
			defaultRenderer: true
		},{
			text: '入库时间',
			dataIndex: 'BReqComplexOfInInfoVO_BInDate',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		},  {
			text: '入库人员',
			dataIndex: 'BReqComplexOfInInfoVO_InOperator',
			width: 75,
			defaultRenderer: true
		}, {
			text: '血袋复检时间',
			dataIndex: 'BReqComplexOfInInfoVO_ReCheckTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '血袋复检人',
			dataIndex: 'BReqComplexOfInInfoVO_ReCheck',
			width: 75,
			defaultRenderer: true
		}, {
			text: '加工方式',
			dataIndex: 'BReqComplexOfInInfoVO_PTCName',
			width: 90,
			defaultRenderer: true
		}, {
			text: '交叉配血时间',
			dataIndex: 'BReqComplexOfInInfoVO_BPreDate',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '交叉配血人',
			dataIndex: 'BReqComplexOfInInfoVO_BPOperator',
			width: 70,
			flex:1,
			hidden: true,
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
		//申请单号参数
		if (me.PK) url = url + "&reqFormId=" + me.PK;
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		
		return params;
	}
});
