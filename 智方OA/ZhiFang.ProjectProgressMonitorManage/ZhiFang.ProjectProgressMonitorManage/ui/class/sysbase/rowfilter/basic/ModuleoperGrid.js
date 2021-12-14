/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.rowfilter.basic.ModuleoperGrid', {
	extend: 'Shell.class.sysbase.moduleoper.basic.Grid',
	title: '模块服务列表',

	/**是否启用序号列*/
	hasRownumberer: false,
	hasAdd: false,
	hasEdit: false,
	hasCheckIsUse: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hiddenCName: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = [],
			search = null;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}

		return me.callParent(arguments);
	}
});