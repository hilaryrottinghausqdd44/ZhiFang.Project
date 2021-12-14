/**
 * 机构与货品管理维护
 * @author longfc
 * @version 2018-01-30
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.Tree', {
	extend: 'Shell.class.rea.client.reacenorg.Tree',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '机构信息',
	width: 300,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgListTreeByOrgID',
	/**默认加载数据*/
	defaultLoad: true,
	/**机构类型*/
	OrgType: "0",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.topToolbar = me.topToolbar || ['-', {
			fieldLabel: '类型',
			labelWidth: 40,
			width: 110,
			xtype: 'uxSimpleComboBox',
			itemId: 'cboorgType',
			value: "0",
			data: [
				["0", "供货方"],
				["1", "订货方"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.OrgType = com.getValue();
					me.onRefreshClick();
				}
			}
		}, {
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: 125,
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
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		}];
		me.callParent(arguments);
	},
	getSearchFields: function() {
		var me = this;
		return "ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo,ReaCenOrg_OrgType";
	}
});