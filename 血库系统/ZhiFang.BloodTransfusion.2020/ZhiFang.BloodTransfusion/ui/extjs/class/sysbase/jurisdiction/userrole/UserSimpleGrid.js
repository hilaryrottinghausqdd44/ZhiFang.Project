/**
 * 员工简单列表
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.UserSimpleGrid', {
	extend: 'Shell.ux.grid.Panel',
	
	title: '员工简单列表',
	width: 1200,
	height: 600,

	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**复选框*/
	multiSelect: false,
	selType: 'rowmodel',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL?isPlanish=true',
	
	defaultOrderBy: [{
		property: 'PUser_DispOrder',
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
			width: 145,
			emptyText: '名称',
			isLike: true,
			itemId: 'search',
			fields: ['puser.CName']
		};

		me.buttonToolbarItems = ['refresh', {
			type: 'search',
			info: me.searchInfo
		}];

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '员工姓名',
			dataIndex: 'PUser_CName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工代码',
			dataIndex: 'PUser_ShortCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '使用',
			dataIndex: 'PUser_Visible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			isBool:true,
			type: 'boolean'
		}, {
			text: '主键ID',
			dataIndex: 'PUser_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'PUser_DataTimeStamp',
			hidden: true,
			hideable: false
		}];
		return columns;
	}
});