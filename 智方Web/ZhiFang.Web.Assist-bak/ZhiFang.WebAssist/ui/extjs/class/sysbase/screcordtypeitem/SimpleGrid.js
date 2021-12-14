/**
 * 记录项字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtypeitem.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.grid.MergeCells'
	],
	title: '记录项字典',
	width: 225,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemByHQL?isPlanish=true',

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
		property: 'SCRecordTypeItem_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		me.getView().on("refresh", function() {
//			me.uxMergeCells.mergeCells(me, [0,1,2])
//		});
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '快捷码/名称',
			isLike: true,
			fields: ['screcordtypeitem.ShortCode', 'screcordtypeitem.CName']
		};
		me.uxMergeCells = Ext.create("Shell.ux.grid.MergeCells", {
			itemId: 'uxMergeCells'
		});
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编码',
			dataIndex: 'SCRecordTypeItem_Id',
			width: 100,
			//hidden: true,
			isKey: true,
			//menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'SCRecordTypeItem_CName',
			width: 160,
			//flex:1,
			defaultRenderer: true
		}, {
			text: '对照编码',
			dataIndex: 'SCRecordTypeItem_ItemCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '简称',
			dataIndex: 'SCRecordTypeItem_SName',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'SCRecordTypeItem_ShortCode',
			width: 80,
			hidden: true,
			//menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '拼音字头',
			dataIndex: 'SCRecordTypeItem_PinYinZiTou',
			width: 80,
			hidden: true,
			defaultRenderer: true
		},{
			text: '次序',
			dataIndex: 'SCRecordTypeItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	}
});