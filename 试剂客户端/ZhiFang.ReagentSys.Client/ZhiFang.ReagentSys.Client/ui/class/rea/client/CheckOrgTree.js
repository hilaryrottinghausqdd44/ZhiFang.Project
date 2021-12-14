/**
 * 选择部门树
 * @author longfc
 * @version 2018-01-31
 */
Ext.define('Shell.class.rea.client.CheckOrgTree', {
	extend: 'Shell.class.sysbase.org.Tree',

	title: '选择部门',

	selectOrgUrl: '/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_UseCode',
	width: 405,
	height: 460,
	/**默认加载数据*/
	defaultLoad: true,

	/**是否带清除按钮*/
	hasClearButton: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**对外公开:显示所有部门树:false;只显示用户自己的树:true*/
	ISOWN: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record, item, index, e, eOpts) {
				me.fireEvent('accept', me, record);
			}
		});
		me.store.on({
			load: function(store, node, records) {
				if(node || node.childNodes.length > 0) {
					var select = node;
					if(!me.rootVisible && node.get("tid") == "0")
						select = node.childNodes[0];
					me.getSelectionModel().select(select);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		//根据登录者的部门id过滤
		var depID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		if(me.ISOWN == true&&depID) {
			me.root.id = depID;
		}
		me.addEvents('accept');
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
		me.callParent(arguments);
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