/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.CheckForm', {
	extend: 'Shell.ux.form.Panel',
	
	title: '实验室验收确认',
	width: 460,
	height: 210,
	formtype: 'add',
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',
	
	/** 每个组件的默认属性*/
	defaults: {
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**光标定位延时*/
	focusTimes: 200,

	/**是否异常*/
	isError: false,
	/**验收双确认方式:secAccepterType：本实验室(默认):1;本实验室双人确认:2*/
	secAccepterType: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//验收双确认方式  本实验室(默认):1;本实验室双人确认:2
		JcallShell.REA.RunParams.getRunParamsValue("SecAccepterAccount", false, function(data) {
			if(data.success) {
				var paraValue = "1";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
				}
				if(paraValue) {
					me.secAccepterType =paraValue;
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');

		me.items = [{
			x: 10,
			y: 10,
			width: 200,
			fieldLabel: '主验收人',
			xtype: 'displayfield',
			IsnotField: true,
			value: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}, {
			x: 240,
			y: 10,
			width: 70,
			hidden: true,
			IsnotField: true,
			xtype: 'label',
			itemId: 'Status'
		}, {
			x: 10,
			y: 35,
			width: 300,
			xtype: 'textarea',
			height: 60,
			fieldLabel: '验收备注',
			emptyText: '请输入备注',
			itemId: 'ReaBmsCenSaleDocConfirm_AcceptMemo',
			name: 'ReaBmsCenSaleDocConfirm_AcceptMemo'
		}, {
			x: 10,
			y: 100,
			width: 175,
			fieldLabel: '验收确认',
			emptyText: '请输入账号',
			IsnotField: true,
			//hidden: (me.secAccepterType == 3 ? false : true),
			itemId: 'Account',
			name: 'Account'
		}, {
			x: 190,
			y: 100,
			width: 120,
			emptyText: '请输入密码',
			//hidden: (me.secAccepterType == 3 ? false : true),
			itemId: 'Pwd',
			name: 'Pwd',
			IsnotField: true,
			inputType: 'password'
		}, {
			x: 320,
			y: 10,
			width: 120,
			xtype: 'label',
			IsnotField: true,
			html: '<div>验收时，如需要两人确认，其中主验收人为当前登录用户，另一人为本机构的"账户+密码"用户。</div>' +
				'<div style="padding-top:5px;"><span style="color:red;">注意</span><span>：主验收人和验收确认不能是同一人，必须是本机构的不同用户。</span></div>'
		}];
		me.buttonToolbarItems = ['->', 'accept'];

		me.callParent(arguments);
	},
	onAcceptClick: function() {
		var me = this,
			values = me.getValues();
		if(me.secAccepterType == 2) {
			if(!values.Account || !values.Pwd) {
				JShell.Msg.error('必须填写验收确认账号密码才能验收，请填写后再操作！');
				return;
			}

			if(values.Account == JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME)) {
				JShell.Msg.error('验收时，验收确认不能是登录者本人，请重新填写验收确认账号！');
				return;
			}
		}
		me.fireEvent('accept', me, {
			AcceptMemo: values.ReaBmsCenSaleDocConfirm_AcceptMemo,
			Account: values.Account,
			Pwd: values.Pwd
		});
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			fields = ["ReaBmsCenSaleDocConfirm_AcceptMemo"];
		return fields;
	}
});