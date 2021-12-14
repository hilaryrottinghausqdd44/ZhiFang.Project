/**
 * 收费类型列表
 * @author longfc
 * @version 2020-07-10
 */
Ext.define('Shell.class.sysbase.chargeitemlink.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '收费类型列表',
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
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += "bdict.IsUse=1 and bdict.BDictType.DictTypeCode='BloodChargeType'";
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
			flex:1,
			width: 100,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BDict_DispOrder',
			width: 70,
			align: 'center',
			type: 'int',
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BDict_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	}
});
