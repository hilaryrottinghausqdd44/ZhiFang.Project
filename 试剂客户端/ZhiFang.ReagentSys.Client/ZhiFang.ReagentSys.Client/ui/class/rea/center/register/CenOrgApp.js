/**
 * 机构管理
 * @author liangyl	
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.center.register.CenOrgApp', {
	extend: 'Ext.panel.Panel',
	title: '机构管理',

	layout:'border',
	border:false,
     margin: '0 0 1 0',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		me.AdminForm=Ext.create('Shell.class.rea.center.register.AdminForm', {
			region:'center',
			itemId:'AdminForm',
			header:false
		});
		me.Form=Ext.create('Shell.class.rea.center.register.Form', {
			region:'north',height:350,
			itemId:'Form',
			header:false,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		return [me.AdminForm,me.Form];
	},
	//获取机构信息
	getCenOrgInfo:function(){
		var me =this;
		var info=me.Form.getAddParams();
		return info;
	},
	//获取人员信息
	getEmpInfo:function(){
		var me =this;
		var info=me.AdminForm.getAddParams();
		return info;
	},
    //获取账号信息
	getAccountInfo:function(){
		var me =this;
		var info=me.AdminForm.getAccounInfo();
		return info;
	},
	isValid:function(){
		var me=this;
		var isExect=true;
		if(!me.AdminForm.getForm().isValid()) isExect=false;
		if(!me.Form.getForm().isValid()) isExect=false;
		return isExect;
	}
});