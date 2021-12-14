/**
 * 审核人确认提示
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.out.retreatsuppliers.AcceptForm', {
	extend: 'Shell.class.rea.client.out.basic.AcceptForm',
	title: '审核人确认提示',
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding:'10px 20px 0px 10px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchTestEquipLabById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddTestEquipLab',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateTestEquipLabByField',
     /**获取用户密码数据服务路径*/
	selectLoginUrl: '/RBACService.svc/RBAC_BA_Login',
	 /**获取账户服务路径*/
	selectUserUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
	formtype:'add',
    /**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 50,
		labelAlign: 'right'
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人',
			itemId: 'UserName',
			name: 'UserName'
		},{
			fieldLabel: '审核人ID',hidden:true,itemId:'UserID',name: 'UserID'
		}, {
			fieldLabel: '密码',inputType: 'password',
			emptyText: '必填项',allowBlank: false,itemId:'Password',name: 'Password'
		},
		{
			hidden:true,fieldLabel: '账号',itemId:'AccountName',name: 'AccountName'
		});
		return items;
	},

	onAcceptClick:function(){
		var me = this;
		if(!me.getForm().isValid()) return;
		var	values = me.getForm().getValues();
		me.showMask(me.loadingText);//显示遮罩层
		var Account=me.getComponent('AccountName');
		if(!Account.getValue()){
			JShell.Msg.error('用户账号不能为空!');
			return;
		}
		me.Login(Account.getValue(),values.Password,function(data){		
			var success = Ext.JSON.decode(data);
			me.hideMask();
			var str=data+'';
			if(str=='true' ){
				me.fireEvent('save',me);
			}else{
				JShell.Msg.error('审核人密码不正确,请重新输入!');
			}
		});
	}
});