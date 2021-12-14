/**
 * 重新复选组
 * @author liangyl
 * @version 2017-11-16
 */
Ext.define('Shell.class.qms.equip.templet.operate.CheckBoxGroup', {
    extend: 'Ext.form.CheckboxGroup',  
    alias: 'widget.uxcheckboxgroup',  
    style:"text-align:center",
    setValue: function (val) {
	    var valAry = val.split(",");
	    this.items.each(function (item) {
	        if(Ext.Array.contains(valAry,item.inputValue)){
	        	item.setValue(true);
	        }
//	        if (valAry.indexOf(item.inputValue) != -1) item.setValue(true);
	        else item.setValue(false);
	    });
	},
	getValue: function () {
        var val = "";
        this.items.each(function (item) {
            if (item.getValue()) {
                val += item.inputValue + ",";
            }
        });
        return val.slice(0, val.length - 1);
    }
   
});  
