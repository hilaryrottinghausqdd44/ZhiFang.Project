/**
 * 部门树选择
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.basic.CheckTree', {
	extend: 'Shell.class.sysbase.org.CheckTree',

	title: '选择部门',
	width: 405,
	height: 460,
	/**机构类型*/
	OrgType: null,
	/**默认加载数据*/
	defaultLoad: true,
	/**根节点*/
	root: {
		text: '所有部门',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: true
	},
	/**是否带清除按钮*/
	hasClearButton: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**是否显示根节点*/
	rootVisible: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//		me.load();
		me.onPlusClick();
		me.on({
			itemdblclick: function(view, record, item, index, e, eOpts) {
				me.fireEvent('accept', me, record);
			}
		});

	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');

		me.callParent(arguments);
	},
	createTopToolbar: function() {
		var me = this;
		me.topToolbar = me.topToolbar || [];

		me.topToolbar.push('-', {
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: 175,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			onTriggerClick: function() {
				this.setValue('');
				me.clearFilter();
			},
			listeners: {
				keyup: {
					fn: function(field, e) {
						var bo = Ext.EventObject.ESC == e.getKey();
						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
					}
				}
			}
		}, '->', {
			xtype: 'button',
			iconCls: 'button-accept',
			text: '确定',
			tooltip: '确定',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		if(me.hasClearButton) {
			dockedItems[0].items.splice(0, 0, {
				text: '清除',
				iconCls: 'button-cancel',
				tooltip: '<b>清除原先的选择</b>',
				handler: function() {
					me.fireEvent('accept', me, null);
				}
			});
		}
		return dockedItems;
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();

		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('accept', me, records[0]);
	}
});