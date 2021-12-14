/**
 * 入账管理列表-平台管理
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.manage.Grid', {
	extend: 'Shell.class.rea.recorded.basic.Grid',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	/**默认加载*/
	defaultLoad: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.splice(2, 0, {
			width: 280,
			labelWidth: 35,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			itemId: 'CenOrgName',
			fieldLabel: '机构',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			emptyText: '机构选择',
			classConfig: {
				title: '机构选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'CenOrgID',
			fieldLabel: '机构主键ID',
			hidden: true
		});
		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CenOrg_CName = buttonsToolbar.getComponent('CenOrgName'),
			CenOrg_Id = buttonsToolbar.getComponent('CenOrgID');
		CenOrg_CName.on({
			check: function(p, record) {
				me.CENORG_ID = null;
				CenOrg_CName.setValue(record ? record.get('CenOrg_CName') : '');
				CenOrg_Id.setValue(record ? record.get('CenOrg_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CenOrgID = buttonsToolbar.getComponent('CenOrgID'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if(CenOrgID && CenOrgID.getValue()) {
			params.push('bmsaccountinput.Lab.Id=' + CenOrgID.getValue());
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}
		return me.callParent(arguments);
	}
});