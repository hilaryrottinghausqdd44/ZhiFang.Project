/**
 * 输血申请综合查询:样本信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.recei.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '样本信息',

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiListByBReqVO?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	hasPagingtoolbar: false,
	//申请单号
	PK: null,
	//申请信息VO
	bReqVO:null,	
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodRecei_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.recei.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "样本信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '住院号',
			dataIndex: 'BloodRecei_PatNo',
			width: 135,
			defaultRenderer: true
		}, {
			text: '条码号',
			dataIndex: 'BloodRecei_Id',
			width: 115,
			defaultRenderer: true
		}, {
			text: '姓名',
			dataIndex: 'BloodRecei_CName',
			width: 95,
			defaultRenderer: true
		}, {
			text: '标本状态',
			dataIndex: 'BloodRecei_StatusCName',
			width: 120,
			flex:1,
			defaultRenderer: true
		},{
			text: '拒收原因Id',
			dataIndex: 'BloodRecei_Bloodrefuse_Id',
			width: 120,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BloodRecei_Id',
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
		if (me.PK) url = url + "&reqFormId=" + me.PK;
		if(me.bReqVO){
			url = url + "&bReqVO=" +JShell.JSON.encode(me.bReqVO);
		}
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		
		return params;
	}
});
