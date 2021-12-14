/**
 * 所有机构
 * @author longfc
 * @version 2018-01-30
 */
Ext.define('Shell.class.rea.client.reacenorg.CheckTree', {
	extend: 'Shell.class.rea.client.reacenorg.Tree',
	requires: [
		'Shell.ux.form.field.TextSearchTrigger'
	],
	
	title: '选择机构',
	width: 405,
	height: 460,
	
	/**机构类型*/
	OrgType: null,
	/**默认加载数据*/
	defaultLoad: true,
	/**根节点*/
	root: {
		text: '所有机构',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgListTreeByOrgID',
	
	/**是否带清除按钮*/
	hasClearButton: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
			xtype: 'textSearchTrigger',
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
				//				keyup: {
				//					fn: function(field, e) {
				//						var bo = Ext.EventObject.ESC == e.getKey();
				//						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
				//					}
				//				},
				onSearchClick: {
					fn: function(field, newValue, e) {
						JShell.Action.delay(function() {
							me.filterByText(newValue);
						}, null, 300);
					}
				},
				onClearClick: {
					fn: function(field, newValue, e) {
						this.setValue('');
						me.clearFilter();
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
	},
	onBeforeLoad: function() {
		var me = this;
		if(me.OrgType == null || me.OrgType == undefined || me.OrgType == "") return false;
		me.store.proxy.url = me.getLoadUrl();
	},
	getSearchFields: function() {
		var me = this;
		return "ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + ('fields=' + me.getSearchFields() + "&orgType=" + me.OrgType);

		return url;
	}
});