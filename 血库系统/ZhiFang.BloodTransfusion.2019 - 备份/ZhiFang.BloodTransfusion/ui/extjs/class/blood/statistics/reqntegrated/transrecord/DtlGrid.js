/**
 * 输血申请综合查询:输血过程记录信息
 * @author longfc
 * @version 2020-03-19
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.transrecord.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '输血过程记录信息',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormByHQL?isPlanish=true',
	/**默认查询条件:*/
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
		property: 'BloodTransForm_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'reqntegrated.transrecord.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "输血过程记录信息",
	
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
			dataIndex: 'BloodTransForm_BloodBReqForm_Id',
			width: 60,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '血袋号',
			dataIndex: 'BloodTransForm_BBagCode',
			width: 135,
			defaultRenderer: true
		},{
			text: '产品码',
			dataIndex: 'BloodTransForm_PCode',
			width: 135,
			defaultRenderer: true
		}, {
			text: '惟一码',
			dataIndex: 'BloodTransForm_BloodBOutItem_B3Code',
			width: 125,
			hidden:true,
			defaultRenderer: true
		},{
			text: '血制品',
			dataIndex: 'BloodTransForm_Bloodstyle_CName',
			width: 135,
			defaultRenderer: true
		},{
			text: '开始时间',
			dataIndex: 'BloodTransForm_TransBeginTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		},{
			text: '结束时间',
			dataIndex: 'BloodTransForm_TransEndTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		},{
			text: '输血前核对人',
			dataIndex: 'BloodTransForm_BeforeCheck1',
			width: 95,
			defaultRenderer: true
		}, {
			text: '输血前核对人2',
			dataIndex: 'BloodTransForm_BeforeCheck2',
			width: 95,
			defaultRenderer: true
		},{
			text: '输血时核对人',
			dataIndex: 'BloodTransForm_TransCheck1',
			width: 95,
			defaultRenderer: true
		}, {
			text: '输血时核对人2',
			dataIndex: 'BloodTransForm_TransCheck2',
			width: 95,
			defaultRenderer: true
		},{
			text: '是否出现不良反应',
			dataIndex: 'BloodTransForm_HasAdverseReactions',
			width: 135,
			defaultRenderer: true,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			}
		},{
			text: '出现不良反应时间',
			dataIndex: 'BloodTransForm_AdverseReactionsTime',
			width: 135,
			isDate: true,
			hasTime: true,
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
		
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		if(me.PK){
			params.push("bloodtransform.BloodBReqForm.Id='"+me.PK+"'");
		}
		return params;
	}
});
