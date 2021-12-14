/**
 * 单选组
 * @author liangyl
 * @version 2017-11-16
 */
Ext.define('Shell.class.qms.equip.templet.operate.RadioGroup', {
	extend: 'Shell.ux.form.Panel',
    alias: 'widget.uxRadioGroup',
    title: '单选组',
	bodyPadding: '0px 0px 0px 5px;',
	bodyStyle :'overflow-x:hidden;overflow-y:hidden', 
	formtype:'add',
	layout: 'fit',
	header:false,
	border:false,
	cols:0,
	ItemList:[],
	DefaultValue:null,
	rb:'rb0',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var num=1,counttext=10;
		var columwidth=0;
		for(var i=0;i<me.ItemList.length;i++){
			var boxLabel=me.ItemList[i].boxLabel;
			var textLength=me.getNum(boxLabel);
			num+=textLength;
		}
		columwidth=num*22;
		if(columwidth<175)columwidth=180;
		me.setWidth(columwidth);
	},
	 /**
     * 字符长度计算
     *获得字符串实际长度，中文2，英文1
     *str要获得长度的字符串
     */
	getNum:function(str){
		if(!str) return;
		str = str.replace(/\s/ig,'');
	    var realLength = 0, len = str.length, charCode = -1;
	    for (var i = 0; i < len; i++) {
	      charCode = str.charCodeAt(i);
		  if (charCode >= 0 && charCode <= 128) 
		       realLength += 1;
		    else
		       realLength += 2;
	    }
	    return realLength/2;  
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			this.getForm().reset();
		if(me.DefaultValue){
			var obj={};
	        obj[me.rb] = me.DefaultValue;
			me.myRadioGroup.setValue(obj);
			}else{
				me.getForm().setValues(me.lastData);
			}
		}
	},
	
	initComponent: function() {
		var me = this;
		me.title = me.title || "";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var check=false;
		me.myRadioGroup = Ext.create('Ext.form.RadioGroup', {
			border: false,
			title: '',
			fieldLabel: '',
			columns: me.cols,
			layout: 'hbox',
            style:"text-align:center",
            bodyStyle :'overflow-x:hidden;overflow-y:hidden', 
            width:'100%',
			vertical: true,
			items:me.ItemList
		});
		return [ me.myRadioGroup];
	},
	setValue:function(value){
		var me=this;
		var obj={};
        obj[me.rb] = value;
		me.myRadioGroup.setValue(obj);
	},
	getValue:function(){
	    var me=this;
		var val = me.getForm().findField(me.rb).getGroupValue(); //此处获取到的是inputValue的值
        if(!val)val=0;
        return val;
	}
});