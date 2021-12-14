/**
 * 费用项目类型描述
 * @author longfc
 * @version 2020-07-07
 */
Ext.define('Shell.class.sysbase.chargeitemtype.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '费用项目类型描述列表',
	width: 205,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemTypeByHQL?isPlanish=true',

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
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodChargeItemType_DispOrder',
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
			width: 185,
			emptyText: '名称/简称',
			isLike: true,
			fields: ['bloodchargeitemtype.CName', 'bloodchargeitemtype.SName']
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
			dataIndex: 'BloodChargeItemType_Id',
			isKey: true,
			width: 55
		},{
			text: '名称',
			dataIndex: 'BloodChargeItemType_CName',
			width: 90,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BloodChargeItemType_DispOrder',
			width: 45,
			defaultRenderer: true,
			type: 'int'
		}];

		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick', me, records[0]);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
		//添加全部行
		obj = {
			BloodChargeItemType_CName: '全部(包含未分类)',
			BloodChargeItemType_DispOrder: 0,
			BloodChargeItemType_Id: ''
		};
		if (data.value) {
			list = data.value.list;
			list.splice(0, 0, obj);
		} else {
			list = [];
			list.push(obj);
		}
		result.list = data.value.list;
		return result;
	}
});
