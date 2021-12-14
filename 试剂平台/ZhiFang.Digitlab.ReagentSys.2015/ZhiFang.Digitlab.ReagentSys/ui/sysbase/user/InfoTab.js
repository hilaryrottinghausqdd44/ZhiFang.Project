/**
 * 用户信息
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.user.InfoTab',{
	extend:'Ext.panel.Panel',
	
	layout:'fit',
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	title:'用户信息',
	width:440,
	height:600,
	
	/**根部门ID*/
	rootOrgId:0,
	/**跟部门名称*/
	rootOrgName:'所有部门',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var UserForm = me.getComponent('UserForm');
		UserForm.on({
			save:function(){me.fireEvent('save',me,UserForm);}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var userForm = Ext.create('Shell.sysbase.user.UserForm',{
			title:'用户信息',
			itemId:'UserForm',
			rootOrgId:me.rootOrgId,
			rootOrgName:me.rootOrgName
		});
		var AccountForm = Ext.create('Shell.sysbase.user.AccountForm',{
			title:'用户账号',
			itemId:'AccountForm'
		});
		return [userForm,AccountForm];
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
				handler:function(but){me.onApplyAccount();}
			},{
				xtype:'button',
				text:'账号停用',
				handler:function(but){me.onStopAccount();}
			},{
				xtype:'button',
				text:'密码重置',
				handler:function(but){me.onResetPassword();}
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
	},
	isEdit:function(id,info){
		var me = this,
			UserForm = me.getComponent('UserForm');
			
		UserForm.isEdit(id,info);
	},
	onApplyAccount:function(){
		var me = this,
			UserForm = me.getComponent('UserForm'),
			AccountForm = me.getComponent('AccountForm');
		
		me.layout = 'fit';
		me.doLayout();
	},
	onStopAccount:function(){
		var me = this,
			UserForm = me.getComponent('UserForm'),
			AccountForm = me.getComponent('AccountForm');
		
		UserForm.show();
		AccountForm.hide();
	},
	onResetPassword:function(){
		var me = this,
			UserForm = me.getComponent('UserForm'),
			AccountForm = me.getComponent('AccountForm');
		
		UserForm.hide();
		AccountForm.show();
	}
});