/**
 * 年份和貅下拉框
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.ux.form.field.YearAndMonthComboBox', {
	extend: 'Shell.ux.form.field.SimpleComboBox',
	alias: 'widget.uxYearAndMonthComboBox',

	/**最小年份*/
	minYearValue: 1900,
	initComponent: function() {
		var me = this;
		var date = new Date();
		var curYear = date.getFullYear();
		var curMonth = date.getMonth() + 1;
		me.data = [];
		var key1 = "",
			value1 = "";
		for(var i = curYear; i >= me.minYearValue; i--) {
			for(var j = 1; j <= 12; j++) {
				var month = j;
				month = (month <= 9 ? "0" + month : "" + month);
				key1 = i + "-" + month;
				value1 = i + "年" + month + "月";
				me.data.push([key1, value1]);
				//if(key1 == ("" + me.minValue)) break;
			}
		}
		curMonth = (curMonth <= 9 ? "0" + curMonth : "" + curMonth);
		me.value = me.value || curYear + "-" + curMonth; //默认当年当前月
		me.callParent(arguments);
	}
});