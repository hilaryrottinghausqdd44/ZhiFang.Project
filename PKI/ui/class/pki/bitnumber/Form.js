/**
 * 设置样本项目位点数
 * @author liangyl	
 * @version 2017-12-01
 */
Ext.define('Shell.class.pki.bitnumber.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '设置样本项目位点数',
	width: 230,
	height: 100,
	/**内容周围距离*/
	bodyPadding:'10px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '',
	/**新增服务地址*/
	addUrl: '',
	/**修改服务地址*/
	editUrl: '',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	/**创建功能按钮栏
	 
	 * */
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		var msg = '您确定要对选中行设置样本项目位点数吗？';
		items.push('->',{
			text:'确定',tooltip:'确定',iconCls:'button-accept',
			handler:function(btn){
				var values = me.getForm().getValues();
			    if(!me.getForm().isValid()) {
					me.fireEvent('isValid', me);
					return;
				}
				me.fireEvent('accept', values.ItemPoint,me);
			}
		},{
			text:'取消',tooltip:'取消',iconCls:'button-cancel',
			handler:function(btn){
			    me.close();
			}
		});
		if(items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:false
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push( {
			fieldLabel: '位点数',
			name: 'ItemPoint',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		});
		return items;
	}
});