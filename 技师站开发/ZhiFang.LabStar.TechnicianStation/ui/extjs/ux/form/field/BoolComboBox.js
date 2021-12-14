/**
 * 是否下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.BoolComboBox',{
    extend:'Shell.ux.form.field.SimpleComboBox',
    alias:'widget.uxBoolComboBox',
    
	/**是否含有'全部'选项*/
	hasAll:false,
	//是的值
	trueValue:true,
	//否的值
	falseValue:false,
	
	/**是否选择列表*/
	List:[
		[null,JShell.All.ALL, 'font-weight:bold;color:black;'],
		[true,JShell.All.TRUE, 'font-weight:bold;color:green;'],
		[false,JShell.All.FALSE, 'font-weight:bold;color:red;']
	],
	/**带样式*/
	hasStyle:true,
	initComponent:function(){
		var me = this,
			data = [];
			
		me.List[1][0] = me.trueValue;
		me.List[2][0] = me.falseValue;
		
		if(me.value === undefined){
			me.value = me.falseValue;
		}
		
		me.defaultValue = me.value;
		
		if(me.hasAll) data.push(me.List[0]);
		
		data.push(me.List[1],me.List[2]);
		
		me.data = data;
		
		me.callParent(arguments);
	},
	setValue:function(){
		var me = this;
		return me.callParent(arguments);
	},
	reset:function(){
        this.setValue(this.defaultValue);
    }
});