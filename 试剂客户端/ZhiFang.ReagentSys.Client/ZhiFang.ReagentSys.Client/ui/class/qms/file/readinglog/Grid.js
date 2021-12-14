/**
 * 阅读记录列表
 * @author
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.readinglog.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '文档阅读记录列表 ',
	width: 560,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileReadingLogByHQL?isPlanish=true',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: true,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	PK: '',
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	defaultOrderBy: [{
		property: 'FFileReadingLog_DataAddTime',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if (me.PK != '') {
			me.loadByFileId(me.PK);
		}
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '文档标题',
			isLike: true,
			fields: ['ffilereadinglog.FFile.Title']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '阅读时间',
			dataIndex: 'FFileReadingLog_DataAddTime',
			width: 150,
			isDate: true,
			hasTime: true
		}, {
			text: '阅读时长',
			hidden: true,
			dataIndex: 'FFileReadingLog_ReadTimes',
			width: 100,
			defaultRenderer: true
		}, {
			text: '阅读人',
			dataIndex: 'FFileReadingLog_ReaderName',
			width: 160,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'FFileReadingLog_Memo',
			flex:1,
			defaultRenderer: true,
			hidden: false
		}, {
			text: '显示次序',
			dataIndex: 'FFileReadingLog_DispOrder',
			width: 80,
			defaultRenderer: true,
			type: 'int',
			hidden: true
		}];
		return columns;
	},
	/**根据ID加载数据*/
	loadByFileId: function(id) {
		var me = this;
		me.defaultWhere = 'ffilereadinglog.FFile.Id=' + id;
		me.onSearch();
	}
});