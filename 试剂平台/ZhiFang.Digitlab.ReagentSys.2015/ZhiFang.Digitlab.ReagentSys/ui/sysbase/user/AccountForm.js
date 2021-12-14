/**
 * 用户账号表单
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.user.AccountForm',{
	extend:'Ext.form.Panel',
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	title:'用户账号',
	width:440,
	height:600,
	bodyPadding:20,
	autoScroll:true,
	/**绝对定位布局*/
	layout:'absolute',
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		return [{
			xtype:'textfield',
			itemId:'HREmployee_NameL',
			name:'HREmployee_NameL',
			x:5,y:10,
			fieldLabel:'姓',
			readOnly:true
		}];
	},
	createDockedItems:function(){
		var me = this;
		
		return [{
			xtype:'toolbar',
			dock:'top',
			itemId:'toptoolbar',
			items:[{
				xtype:'button',
				text:'账号申请',
				handler:function(but){}
			},{
				xtype:'button',
				text:'账号停用',
				handler:function(but){}
			},{
				xtype:'button',
				text:'账号更新',
				handler:function(but){}
			},{
				xtype:'button',
				text:'密码重置',
				handler:function(but){}
			}]
		},{
			xtype:'toolbar',
			dock:'bottom',
			itemId:'bottomtoolbar',
			items:[{
				xtype:'label',
				itemId:'type'
			},'->',{
				xtype:'button',
				text:'保存',
				iconCls:'build-button-save',
				handler:function(but){
					me.submit();
				}
			},{
				xtype:'button',
				text:'重置',
				iconCls:'build-button-refresh',
				handler:function(but){
					me.getForm().reset();
				}
			}]
		}];
	}
});