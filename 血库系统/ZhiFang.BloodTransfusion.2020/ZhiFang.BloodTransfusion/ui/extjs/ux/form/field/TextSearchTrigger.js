/**
 * 文本查询框
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.ux.form.field.TextSearchTrigger', {
	extend: 'Ext.form.field.Trigger',
	alias: 'widget.textSearchTrigger',

	fieldLabel: '',
	trigger1Cls: Ext.baseCSSPrefix + 'form-clear-trigger',
	trigger2Cls: Ext.baseCSSPrefix + 'form-search-trigger',

	triggerCls: 'x-form-search-trigger',
	enableKeyEvents: true,
	editable: true,

	onTriggerClick: function() {
		var me = this;
		me.fireEvent('onSearchClick', me, me.getValue());
	},
	onTrigger1Click: function() {
		var me = this;
		if(me.hasSearch) {
			me.setValue('');
			me.hasSearch = false;
			me.triggerCell.item(0).setDisplayed(false);
			//me.updateLayout();
		}
		me.fireEvent('onClearClick', me);
	},
	onTrigger2Click: function() {
		var me = this,
			value = me.getValue();
		if(value.length > 0) {
			me.hasSearch = true;
			me.triggerCell.item(0).setDisplayed(true);
			//me.updateLayout();
		}
		me.fireEvent('onSearchClick', me, value);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('onSearchClick', 'onClearClick');
		me.callParent(arguments);
		me.on('specialkey', function(f, e) {
			if(e.getKey() == e.ENTER) {
				me.onTrigger2Click();
			}
		});
	},
	afterRender: function() {
		this.callParent();
		this.triggerCell.item(0).setDisplayed(false);
	},
	keyup: function(field, e) {
		if(e.getKey() == Ext.EventObject.ESC) {
			field.setValue('');
			me.fireEvent('onSearchClick', me, field.getValue());
		} else if(e.getKey() == Ext.EventObject.ENTER) {
			me.fireEvent('onSearchClick', me, field.getValue());
		}
	}
});