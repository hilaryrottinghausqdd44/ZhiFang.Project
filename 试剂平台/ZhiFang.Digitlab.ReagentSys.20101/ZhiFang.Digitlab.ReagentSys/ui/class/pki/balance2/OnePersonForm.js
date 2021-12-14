/**
 * 个人开票信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.OnePersonForm', {
	extend: 'Shell.ux.form.Panel',

	title: '个人开票信息',
	width: 240,
	height: 170,
	
	/**默认个人姓名*/
	defaultName:'',

	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用取消按钮*/
	hasCancel:true,
	
	/**内容周围距离*/
	bodyPadding:10,
	/**布局方式*/
	layout:'anchor',
    /** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:30,
        labelAlign:'right'
    },
    
    /**初始价格*/
	Price:0,
	
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			height:60,
			xtype:'textarea',
			name:'Name',
			itemId:'Name',
			emptyText:'请填写个人信息',
			fieldLabel:'信息',
			value:me.defaultName
		},{
			xtype:'numberfield',
			fieldLabel:'价格',
			name:'Price',
			itemId:'Price',
			value:me.Price,
			emptyText:'必填项',
			allowBlank:false
		});

		return items;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this,
			Name = me.getComponent('Name').getValue(),
			Price = me.getComponent('Price').getValue();
		
		if(Price == null) return;
		
		me.fireEvent('save', me, {
			Name:Name,
			Price:Price
		});
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick:function(){
		this.close();
	}
});