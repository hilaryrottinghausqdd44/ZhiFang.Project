/**
 * 服务器授权申请程序信息
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '服务器授权申请程序',
	width: 240,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	autoSelect:true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	LicenceProgramTypeList: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		this.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {},
	/**@public 根据条件加载数据*/
	load: function() {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		if(me.LicenceProgramTypeList != null) {
			me.store.loadData(me.LicenceProgramTypeList);
			me.getSelectionModel().select(0);
		} else {
			me.clearData();
		}
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '申请程序',
			dataIndex: 'CName',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'ID',
			dataIndex: 'Id',
			width: 100,
			isKey: true,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '代码',
			dataIndex: 'Code',
			width: 60,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'BGColor',
			dataIndex: 'BGColor',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}];

		return columns;
	}
});