/**
 * 是否下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.enum.CooplevComboBox',{
    extend:'Shell.ux.form.field.SimpleComboBox',
    
	/**是否含有'全部'选项*/
	hasAll:false,
	/**默认否*/
	value:null,
	
	/**列表内容*/
	List:[
		[null,JShell.All.ALL, 'font-weight:bold;color:black;'],
		['1','送检单位', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2','单位科室', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';']
	],
	/**带样式*/
	hasStyle:true,
	
	initComponent:function(){
		var me = this,
			data = [];
		
		if(me.hasAll) data.push(me.List[0]);
		
		data.push(me.List[1],me.List[2]);
		
		me.data = data;
		
		me.callParent(arguments);
	}
});