/**
 * 输血申请综合查询:出库信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.out.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '出库信息',
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL?isPlanish=true',
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
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.out.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库信息",
	
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutItem_Bloodstyle_Id',
		direction: 'ASC'
	}],
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
			text: '发血单号',
			dataIndex: 'BloodBOutItem_BloodBOutForm_Id',
			width: 135,
			defaultRenderer: true
		}, {
			text: '献血编号',
			dataIndex: 'BloodBOutItem_BBagCode',
			width: 110,
			defaultRenderer: true
		}, {
			text: '产品码',
			dataIndex: 'BloodBOutItem_Pcode',
			width: 90,
			hidden:true,
			defaultRenderer: true
		},{
			text: '惟一码',
			dataIndex: 'BloodBOutItem_B3Code',
			width: 150,
			hidden:true,
			defaultRenderer: true
		}, {
			text: '血制品',
			dataIndex: 'BloodBOutItem_Bloodstyle_CName',
			width: 135,
			defaultRenderer: true
		},   {
			text: '血型名称',
			dataIndex: 'BloodBOutItem_BloodABO_CName',
			width: 70,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '数量',
			dataIndex: 'BloodBOutItem_BOutCount',
			width: 95,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodBOutItem_BloodBUnit_BUnitName',
			width: 60,
			defaultRenderer: true
		}, {
			text: 'ABO',
			dataIndex: 'BloodBOutItem_BloodABO_ABOType',
			width: 95,
			defaultRenderer: true
		},{
			text: 'RH',
			dataIndex: 'BloodBOutItem_BloodABO_RHType',
			width: 95,
			defaultRenderer: true
		},{
			text: '发血人Id',
			dataIndex: 'BloodBOutItem_BloodBOutForm_OperatorID',
			width: 80,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '发血人',
			dataIndex: 'BloodBOutItem_BloodBOutForm_Operator',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '发血时间',
			dataIndex: 'BloodBOutItem_BloodBOutForm_OperTime',
			width: 95,
			isDate: true,
			hasTime: false,
			flex:1,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '失效时间',
			dataIndex: 'BloodBOutItem_InvalidDate',
			width: 95,
			hidden:true,
			isDate: true,
			hasTime: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '采集时间',
			dataIndex: 'BloodBOutItem_CollectDate',
			width: 125,
			hidden:true,
			isDate: true,
			hasTime: true,			
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '申请明细编号',
			dataIndex: 'BloodBOutItem_BloodBReqItem_Id',
			width: 70,
			hidden:true,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '血制品编码',
			dataIndex: 'BloodBOutItem_Bloodstyle_Id',
			width: 100,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '主键ID',
			dataIndex: 'BloodBOutItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
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
		if (me.PK) url = url + "&reqFormId=" + me.PK
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		if(me.PK){
			params.push("bloodbreqform.Id='"+me.PK+"'");
		}
		return params;
	}
});
