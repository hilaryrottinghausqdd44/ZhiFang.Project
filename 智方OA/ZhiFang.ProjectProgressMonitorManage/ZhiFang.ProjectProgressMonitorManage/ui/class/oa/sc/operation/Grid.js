/**
 * 操作记录列表
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.sc.operation.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '操作记录列表 ',
	width: 520,
	height: 480,
	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL?isPlanish=true',

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
	SCOperationTypeEnum: {
		"1": '新增仪器信息',
		"2": '修改仪器信息',
		"3": '程序暂存',
		"4": '程序直接发布',
		"5": '程序修改暂存',
		"6": '程序修改发布',
		'7': '系统参数添加',
		'8': '系统参数修改',
		'9': '系统参数禁用',
		'12': '程序禁用',
		'13': '程序启用'
	},
	StatusList: null,
	StatusEnum: null,
	StatusFColorEnum: null,
	StatusBGColorEnum: null,
	defaultOrderBy: [{
		property: 'SCOperation_DataAddTime',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.PK != '') {
			me.loadByBobjectID(me.PK);
		}
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '文档标题',
			isLike: true,
			fields: ['scoperation.FFile.Title']
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
			dataIndex: 'SCOperation_DataAddTime',
			width: 145,
			isDate: true,
			hasTime: true
		}, {
			text: '操作类型',
			dataIndex: 'SCOperation_Type',
			width: 85,
			defaultRenderer: false,
			renderer: function(value, meta) {
				var v = value.toString(),
					style = "";
				if(me.StatusEnum == null) {
					v = me.SCOperationTypeEnum[value] || '';
				} else {
					v = me.StatusEnum[value];
					var bColor = "";
					if(me.StatusBGColorEnum != null)
						bColor = me.StatusBGColorEnum[value];
					var fColor = "";
					if(me.StatusFColorEnum != null)
						fColor = me.StatusFColorEnum[value];
					var style = 'font-weight:bold;';
					if(bColor) {
						style = style + "background-color:" + bColor + ";";
					}
					if(fColor) {
						style = style + "color:" + fColor + ";";
					}
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}

		}, {
			text: '操作人',
			dataIndex: 'SCOperation_CreatorName',
			//flex: 1,
			width: 140,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'SCOperation_Memo',
			//width: 180,
			flex: 1,
			defaultRenderer: true
		}, {
			text: '显示次序',
			dataIndex: 'SCOperation_DispOrder',
			width: 80,
			defaultRenderer: true,
			type: 'int',
			hidden: true
		}];
		return columns;
	},

	/**根据ID加载数据*/
	loadByBobjectID: function(id) {
		var me = this;
		me.defaultWhere = 'scoperation.BobjectID=' + id;
		me.onSearch();
	}
});