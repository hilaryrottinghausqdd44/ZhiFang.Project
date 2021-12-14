/**
 * 字典列表
 * @author longfc
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.dict.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '字典列表',
	width: 205,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',

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
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BDict_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '快捷码/名称',
			isLike: true,
			fields: ['bdict.ShortCode', 'bdict.CName']
		};

		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '名称',
			dataIndex: 'BDict_CName',
			width: 100,
			flex:1,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BDict_DispOrder',
			width: 80,
			align: 'center',
			type: 'int',
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BDict_Id',
			isKey: true,
			hidden: true,
			hideable: false
		},{
			text: '标准代码',
			dataIndex: 'BDict_StandCode',
			width: 80,
			hidden:true,
			defaultRenderer: true
		},{
			text: '业务类型编码',
			dataIndex: 'BDict_UseCode',
			width: 80,
			hidden:true,
			defaultRenderer: true
		},{
			text: '开发商代码',
			dataIndex: 'BDict_DeveCode',
			width: 120,
			hidden:true,
			defaultRenderer: true
		}];

		return columns;
	}
});
