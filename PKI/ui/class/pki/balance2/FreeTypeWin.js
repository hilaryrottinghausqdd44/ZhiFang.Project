/**
 * 免单类型
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.FreeTypeWin',{
    extend:'Shell.ux.form.Panel',
    requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
    
    title:'免单类型',
    formtype:'add',
			
    width:200,
    height:140,
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用取消按钮*/
	hasCancel:true,
	
	bodyPadding:10,
	/**布局方式*/
	layout:'anchor',
	
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		items.push({
			fieldLabel: '免单类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'FreeType',
			name:'FreeType',
			value: '业务免单',
			data: [
				['业务免单','业务免单'],
				['差错免单','差错免单'],
				['验证样本','验证样本'],
				['PGS扩增失败','PGS扩增失败'],
				['实验失败','实验失败'],
				['市场推广','市场推广']
			]
		},{
			fieldLabel: '免单价格',
			xtype: 'numberfield',
			itemId: 'ItemFreePrice',
			name:'ItemFreePrice',
			value: 0,
			minValue:0
		});
		
		return items;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var values = me.getForm().getValues();
		
		me.fireEvent('save',me,values);
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick:function(){
		this.close();
	}
});