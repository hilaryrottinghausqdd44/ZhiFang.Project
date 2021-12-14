/**
 * 加工类型
 * @author panjie
 * @version 2020-08-06
 */
Ext.define('Shell.class.sysbase.bagprocesstype.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '字典类型列表',
	width: 220,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeByHQL?isPlanish=true',

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
		property: 'BloodBagProcessType_DispOrder',
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
			width: 165,
			emptyText: '类型编码/名称',
			isLike: true,
			fields: ['BloodBagProcessType.BloodClass', 'BloodBagProcessType.CName']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
				text: '编码',
				dataIndex: 'BloodBagProcessType_Id',
				isKey: true,
				hideable: false
			}, 
			// {
			// 	text: '类型编码',
			// 	dataIndex: 'BloodBagProcessType_BloodClass',
			// 	width: 100,
			// 	hidden: true,
			// 	menuDisabled: true,
			// 	defaultRenderer: true
			// }, 
			{
				text: '名称',
				dataIndex: 'BloodBagProcessType_CName',
				width: 100,
				flex:1,
				menuDisabled: true,
				defaultRenderer: true
			}
			//,{
			//			text:'次序',dataIndex:'BDictType_DispOrder',width:100,
			//			defaultRenderer:true,align:'center',type:'int'
		];

		return columns;
	}
});
