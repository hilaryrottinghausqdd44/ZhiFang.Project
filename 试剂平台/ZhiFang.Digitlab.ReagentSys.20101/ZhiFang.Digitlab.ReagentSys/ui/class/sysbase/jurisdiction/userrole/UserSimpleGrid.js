/**
 * 员工简单列表
 * @author Jcall
 * @version 2017-01-17
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.UserSimpleGrid', {
	extend: 'Shell.class.sysbase.user.Grid',
	title: '员工简单列表',
	width: 1200,
	height: 600,

	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**复选框*/
	multiSelect: false,
	selType: 'rowmodel',

	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			itemId: 'search',
			fields: ['hremployee.CName']
		};

		me.buttonToolbarItems = ['refresh', {
			xtype: 'checkbox',
			boxLabel: '本部门',
			itemId: 'onlyShowDept',
			checked: !me.DeptTypeModel,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, '->', {
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
			dataIndex: 'HREmployee_CName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '手机号',
			dataIndex: 'HREmployee_MobileTel',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工代码',
			dataIndex: 'HREmployee_UseCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '隶属部门',
			dataIndex: 'HREmployee_HRDept_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '使用',
			dataIndex: 'HREmployee_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			isBool:true,
			type: 'boolean'
		}, {
			text: '主键ID',
			dataIndex: 'HREmployee_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'HREmployee_DataTimeStamp',
			hidden: true,
			hideable: false
		}];
		return columns;
	}
});