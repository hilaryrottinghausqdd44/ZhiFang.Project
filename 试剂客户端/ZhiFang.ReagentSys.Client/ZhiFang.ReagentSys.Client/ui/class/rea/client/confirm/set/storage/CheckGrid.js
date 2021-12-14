/**
 * 库房选择列表
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.set.storage.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '库房选择列表',
	width: 320,
	height: 420,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**传入的货品Id*/
	ReaGoodId: null,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.set.storage.CheckGrid',
	/**用户UI配置Name*/
	userUIName: "库房选择列表",
	
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reastorage.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 235,
			emptyText: '库房名称/代码',
			isLike: true,
			itemId: 'Search',
			fields: ['reastorage.CName', 'reastorage.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaStorage_CName',
			text: '库房名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorage_ShortCode',
			text: '代码',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaStorage_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	}
});