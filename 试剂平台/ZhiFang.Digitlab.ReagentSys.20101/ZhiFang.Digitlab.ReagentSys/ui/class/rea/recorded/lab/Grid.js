/**
 * 入账管理列表-机构
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.lab.Grid', {
	extend: 'Shell.class.rea.recorded.basic.Grid',

	selType: 'checkboxmodel',
	multiSelect: true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**隐藏删除列*/
	hideDelColumn: false,
	/**机构Id*/
	CENORG_ID: null,
	/**机构名称*/
	CENORG_NAME: null,

	initComponent: function() {
		var me = this;
		var cenOrgId = JShell.REA.System.CENORG_ID;
		var CENORG_NAME = JShell.REA.System.CENORG_NAME;
		//获取机构Id
		if(cenOrgId) {
			me.CENORG_ID = cenOrgId;
			me.CENORG_NAME = CENORG_NAME;
			me.defaultWhere = me.defaultWhere || '';
			if(me.defaultWhere) {
				me.defaultWhere = '(' + me.defaultWhere + ') and ';
			}
			me.defaultWhere += 'bmsaccountinput.Lab.Id=' + me.CENORG_ID;
		}
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.splice(2, 0, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'add',
			text: '入账',
			tooltip: '入账',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-del',
			itemId: 'delete',
			text: '取消入帐',
			tooltip: '取消入帐',
			handler: function() {
				me.onDelClick();
			}
		});
		return items;
	},
	onAddClick: function() {
		var me = this;
		var maxWidth = document.body.clientWidth - 60;
		var height = document.body.clientHeight - 40;
		JShell.Win.open('Shell.class.rea.recorded.basic.AddPanel', {
			SUB_WIN_NO: '2', //内部窗口编号
			resizable: false,
			width: maxWidth,
			height: height,
			/**机构Id*/
			CENORG_ID: me.CENORG_ID,
			/**机构名称*/
			CENORG_NAME: me.CENORG_NAME,
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		}).show();
	}
});