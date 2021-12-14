/**
 * 操作记录列表
 * @author
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.operation.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '操作记录列表 ',
	width: 520,
	height: 480,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileOperationByHQL?isPlanish=true',

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
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	PK: '',
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'FFileOperation_DataAddTime',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.PK != '') {
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
			fields: ['ffileoperation.FFile.Title']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '操作时间',
			dataIndex: 'FFileOperation_DataAddTime',
			width: 145,
			isDate: true,
			hasTime: true
		}, {
			text: '操作类型',
			dataIndex: 'FFileOperation_Type',
			width: 95,
			defaultRenderer: false,
			renderer: function(value, meta) {
				var v = JShell.QMS.Enum.FFileOperationType[value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"'; //font-color:'#FFFFFF',
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value] || 'black';
				return v;
			}
		}, {
			text: '操作人',
			dataIndex: 'FFileOperation_CreatorName',
			flex:1,
			//width: 160,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'FFileOperation_Memo',
			width: 180,
			defaultRenderer: true,
			hidden: true
		}, {
			text: '显示次序',
			dataIndex: 'FFileOperation_DispOrder',
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
		me.defaultWhere = 'ffileoperation.FFile.Id=' + id;
		me.onSearch();
	}
});