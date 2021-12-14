/**
 * 字典列表
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.class.sysbase.bparameter.BDicGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '字典列表',
	width: 260,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 500,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	//	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否启用查看按钮*/
	hasShow: false,
	checkOne: true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBDicByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BDic_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: false,

	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bdic.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '参数类型',
			isLike: true,
			fields: ['bdic.Name']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '参数类型',
			dataIndex: 'BDic_Name',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, v) {
				var Memo = record.get("BDic_Comment");
				if(Memo) meta.tdAttr = 'data-qtip="<b>' + Memo + '</b>"'; 
				return value;
			}
		}, {
			text: '备注',
			dataIndex: 'BDic_Comment',
			width: 120,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '编号',
			dataIndex: 'BDic_Shortcode',
			width: 120,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '主键ID',
			dataIndex: 'BDic_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});