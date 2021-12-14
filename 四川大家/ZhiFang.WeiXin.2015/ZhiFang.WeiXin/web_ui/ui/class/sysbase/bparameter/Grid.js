/**
 * 系统参数列表
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.class.sysbase.bparameter.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '系统参数列表',
	width: 480,
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
	hasRownumberer: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	hasEdit: true,
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
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
//	defaultOrderBy: [{
//		property: 'BParameter_DispOrder',
//		direction: 'ASC'
//	}],
	/**是否单选*/
	checkOne: false,

	initComponent: function() {
		var me = this;
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onShowClick', me);
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '系统参数名称',
			isLike: true,
			fields: ['bparameter.Name']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '系统参数',
			dataIndex: 'BParameter_Name',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '说明',
			dataIndex: 'BParameter_Memo',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BParameter_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', this);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onShowClick', me, records[0]);
	}
});