/**
 * 个人开票信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.PersonForm', {
	extend: 'Shell.ux.form.Panel',

	title: '个人开票信息',
	width: 220,
	height: 180,
	
	/**默认个人姓名*/
	IsDefaultName:true,

	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用取消按钮*/
	hasCancel:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			x:40,
			y:10,
			width: 70,
			itemId: 'radio1',
			boxLabel: '个人姓名',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.IsDefaultName
		},{
			x:120,
			y:10,
			width: 70,
			itemId: 'radio2',
			boxLabel: '统一指定',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.IsDefaultName
		},{
			x:15,
			y:40,
			width:190,
			height:60,
			xtype:'textarea',
			name:'Name',
			itemId:'Name',
			emptyText:'请填写统一信息',
			disabled:me.IsDefaultName
		});

		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			Name = me.getComponent('Name'),
			radio1 = me.getComponent('radio1'),
			radio2 = me.getComponent('radio2');

		radio1.on({
			change: function(field, newValue) {
				if (newValue) {
					Name.disable();
				}
			}
		});
		radio2.on({
			change: function(field, newValue) {
				if (newValue) {
					Name.enable();
				}
			}
		});
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this,
			Name = me.getComponent('Name').getValue(),
			radio2 = me.getComponent('radio2').getValue();
		
		if(radio2){
			if(!Name){
				JShell.Msg.error('统一指定方式下，必须填写统一信息');
				return;
			}
		}

		me.fireEvent('save', me, radio2,Name);
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick:function(){
		this.close();
	}
});