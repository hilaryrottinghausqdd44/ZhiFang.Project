/**
 * 输血过程记录:用户确认表单
 * @author xiehz
 * @version 2021-01-26
 */
Ext.define('Shell.class.blood.nursestation.transrecord.checkuser.CheckUserForm', {
	extend:'Shell.ux.form.Panel',
	title:'用户校验',
	bodyPadding:20,
	width: 260,
	height: 180,
	formtype:'add',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**是否启用取消按钮*/
	hasCancel:true,	
	
	UserNo: '',
	UserName: '',
	ShortCode:'',
	
	/**布局方式*/
	layout:'anchor',
    /**默认组件*/
    defaultType:'textfield',
    /** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:50,
        labelAlign:'right'
    },
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '工号',
			name: 'ShortCode',
			itemId:'ShortCode',
			xtype: 'textfield',
			allowBlank: false,
			value:me.ShortCode,
			locked:true,
			readOnly:true
		}, {
			fieldLabel: '名称',
			name: 'UserName',
			itemId:'UserName',
			xtype: 'textfield',
			allowBlank: true,
			value:me.UserName,
			locked:true,
			readOnly:true
		}, {
			fieldLabel: '密码',
			name: 'Password',
			itemId:'Password',
			xtype: 'textfield',
			inputType: 'password',
			allowBlank: true
		});
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
			
		items.push('->');
		items.push('accept');
		items.push('cancel');
		me.buttonToolbarItems = items;
		return me.callParent(arguments);
	},
	/*校验用户*/
	CheckUser: function(ShortCode, PassWord, callback){
		var me = this;
		var url = JShell.System.Path.ROOT +"/BloodTransfusionManageService.svc/BT_SYS_LoginOfPUser";
		url = url + '?strUserAccount=' + ShortCode + '&strPassWord=' + PassWord;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback && typeof callback === 'function' && callback();
			} else {
				JShell.Msg.error('账号或密码错误！');
			}
		});		
	},
	/*确认事件*/
	onAcceptClick: function(p) {
		var me = this;
		var ShortCode = me.ShortCode;
		var PassWord = me.getForm().findField("Password").getValue();
		//特殊字符转义
		PassWord = PassWord.replace(/#/gi, '%23'); 
		PassWord = PassWord.replace(/&/gi, '%26'); 
		PassWord = PassWord.replace(/\+/gi, '%2B'); 
		//校验用户密码
		me.CheckUser(ShortCode, PassWord, function(){
	    	me.fireEvent('accept', p);		
		})
	},
	/*取消关闭*/
	onCancelClick: function(p){
		p.close();
	},
	/**更改标题*/
	changeTitle:function(){
		//
	}
	
})