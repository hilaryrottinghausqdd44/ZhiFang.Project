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
	/**最大年份*/
	maxYearValue: null,
	/**最小选择值*/
	minValue: null,
	/**最大选择值*/
	maxValue: null,
	/**默认选择值*/
	defaultValue: null,
	
	initComponent: function() {
		var me = this;
		var date = new Date();
		if(!me.maxYearValue)
			me.maxYearValue = date.getFullYear();

		var curMonth = date.getMonth() + 1;
		me.data = [];
		var curValue = "",
			curText = "";
		for(var i = me.maxYearValue; i >= me.minYearValue; i--) {
			for(var j = 1; j <= 12; j++) {
				var month = j;
				month = (month <= 9 ? "0" + month : "" + month);
				curValue = i + "-" + month;
				curText = i + "年" + month + "月";
				me.data.push([curValue, curText]);
				if(me.maxValue && curValue == me.maxValue)
					break;
			}
		}
		if(me.defaultValue) me.value = me.defaultValue;
		//curMonth = (curMonth <= 9 ? "0" + curMonth : "" + curMonth);
		//me.value = me.value || curYear + "-" + curMonth; //默认当年当前月
		me.callParent(arguments);
	}
});