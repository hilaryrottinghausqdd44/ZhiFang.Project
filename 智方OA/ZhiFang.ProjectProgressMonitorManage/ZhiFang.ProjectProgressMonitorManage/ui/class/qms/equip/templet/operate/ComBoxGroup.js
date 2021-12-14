/**
 * 状态复选框单选组件
 * 只有两个选项，1,2
 * @author liangyl
 * @version 2017-11-16
 */
Ext.define('Shell.class.qms.equip.templet.operate.ComBoxGroup', {
	extend: 'Shell.ux.form.Panel',
    alias: 'widget.uxcheckbox',
    title: '状态复选框单选组件',
	layout: {
		type: 'hbox',
	    pack: 'start',
	    align: 'stretch'
	},
	width: 150,
	height: 25,
	bodyPadding: '0px 0px 0px 10px;',
	formtype:'add',
	/*默认选中 1 正常 2是异常*/
	defalutVal1:false,
	/*默认选中 1 正常 2是异常*/
	defalutVal2:false,
	/*(正常)第一个值显示内容*/
    NameText1:'',
    /*(异常)第二个值显示内容*/
	NameText2:'',
	header:false,
	border:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "仪器维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var check=false;

		me.OneCheckbox = Ext.create('Ext.form.field.Checkbox', {
			border: false,
			title: '',
			name:'Checkbox',
			ItemId:'OneCheckbox',
			boxLabel: me.NameText1, 
			inputValue: 'true',uncheckedValue: 'false',
			width: 70,
			checked:me.defalutVal1,
			type: 'checkboxfield',fieldLabel: '',
			listeners:{
				change:function(com,  newValue,  oldValue,  eOpts){
					if(newValue==true){
						me.TwoCheckbox.setValue(false);
					}
				}
			}
		});
		me.TwoCheckbox = Ext.create('Ext.form.field.Checkbox', {
			border: false,
			title: '',
			name:'Checkbox',
			boxLabel:  me.NameText2,
			ItemId:'Checkbox2',
			inputValue: 'true',uncheckedValue: 'false',
			width: 70,
			checked:me.defalutVal2,
			type: 'checkboxfield',fieldLabel: '',
			listeners:{
				change:function(com,  newValue,  oldValue,  eOpts){
					if(newValue==true){
						me.OneCheckbox.setValue(false);
					}
				}
			}
		});
		return [ me.OneCheckbox,me.TwoCheckbox];
	},
	setValue:function(value){
		var me=this;
		if(!value || value=='0')return;
		if(value && value=='1'){
			me.OneCheckbox.setValue(true);
		}
		if(value && value=='2'){
			me.TwoCheckbox.setValue(true);
		}
	},
	getValue:function(){
		var me=this;
		var val=0;
		var values=me.getForm().getValues();
		
		if(values.Checkbox[0]=='true' || values.Checkbox[0]==true){
			val=1;
		}
		if(values.Checkbox[1]=='true' || values.Checkbox[1]==true){
			val=2;
		}
        return val;
	}
});