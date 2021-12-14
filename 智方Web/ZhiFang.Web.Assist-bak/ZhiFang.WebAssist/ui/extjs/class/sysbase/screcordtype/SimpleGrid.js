/**
 * 记录项类型字典列表
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtype.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '记录项类型字典列表',
	width: 225,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeByHQL?isPlanish=true',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
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
	hasRownumberer: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'SCRecordType_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '编码/名称',
			isLike: true,
			fields: ['screcordtype.TypeCode', 'screcordtype.CName']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'SCRecordType_ContentTypeID',
			text: '所属类别',
			width: 90,
			renderer: function(value, meta) {
				var v = "";
				if(value == "10000") {
					v = "院感登记";
					meta.style = "color:green;";
				} else if(value == "20000") {
					v = "输血过程登记";
					meta.style = "color:orange;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '编码',
			dataIndex: 'SCRecordType_TypeCode',
			width: 100,
			sortable: false,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'SCRecordType_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'SCRecordType_Id',
			isKey: true,
			hidden: true,
			menuDisabled: true,
			hideable: false
		}];

		return columns;
	}
});