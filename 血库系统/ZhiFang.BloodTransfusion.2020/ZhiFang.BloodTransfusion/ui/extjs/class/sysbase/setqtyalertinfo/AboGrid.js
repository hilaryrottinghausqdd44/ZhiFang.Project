/**
 * 库存预警设置-血型ABO列表
 * @author xiehz
 * @version 2020-08-10
 */
Ext.define('Shell.class.sysbase.setqtyalertinfo.AboGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '血型ABO',
	width: 270,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABOByHQL?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,
	/**复选框*/
	//multiSelect: true,
	//selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用检索框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 200,
		emptyText: 'ABO名称',
		isLike: true,
		fields: ['bloodabo.CName']
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent: function() {
		var me = this;
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '血型ABO编号',
			dataIndex: 'BloodABO_Id',
			isKey: true,
			hideable: false
		},{
			text: '血型ABO名称',
			dataIndex: 'BloodABO_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}];
		return columns;
	}
})