/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.CheckForm', {
	extend: 'Shell.ux.form.Panel',
	title: '实验室验收确认',

	width: 460,
	height: 270,
	formtype: 'add',

	/** 每个组件的默认属性*/
	defaults: {
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**光标定位延时*/
	focusTimes: 200,

	/**是否异常*/
	isError: false,
	/**验收双确认方式:secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)*/
	secAccepterType: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Status = me.getComponent('Status'),
			StatusText = '';
		if(me.isError) {
			StatusText = '<span style="padding:2px 10px;color:red;border:1px solid red;">异常验收</span>';
		} else {
			StatusText = '<span style="padding:2px 10px;color:green;border:1px solid green;">正常验收</span>';
		}

		Status.setText(StatusText, false);
		setTimeout(function() {
			me.getComponent('InvoiceNo').focus();
		}, me.focusTimes);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');

		me.getSecAccepterType(function() {
			me.items = [{
				x: 10,
				y: 10,
				width: 200,
				fieldLabel: '主验收人',
				xtype: 'displayfield',
				value: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
			}, {
				x: 240,
				y: 10,
				width: 70,
				xtype: 'label',
				itemId: 'Status'
			}, {
				x: 10,
				y: 35,
				width: 300,
				xtype: 'textarea',
				height: 60,
				fieldLabel: '发票信息',
				emptyText: '请输入发票号信息，用逗号隔开',
				itemId: 'InvoiceNo',
				name: 'InvoiceNo'
			}, {
				x: 10,
				y: 100,
				width: 300,
				xtype: 'textarea',
				height: 60,
				fieldLabel: '验收备注',
				emptyText: '请输入备注',
				itemId: 'AcceptMemo',
				name: 'AcceptMemo'
			}, {
				x: 10,
				y: 165,
				width: 175,
				fieldLabel: '供货确认',
				emptyText: '请输入账号',
				hidden: (me.secAccepterType == 3 ? false : true),
				itemId: 'Account',
				name: 'Account'
			}, {
				x: 190,
				y: 165,
				width: 120,
				emptyText: '请输入密码',
				hidden: (me.secAccepterType == 3 ? false : true),
				itemId: 'Pwd',
				name: 'Pwd',
				inputType: 'password'
			}, {
				x: 320,
				y: 10,
				width: 120,
				xtype: 'label',
				html: '<div>验收时，需要两人确认，其中主验收人为当前登录用户，供货确认为供应商或本机构的"账户+密码"用户。</div>' +
					'<div style="padding-top:5px;"><span style="color:red;">注意</span><span>：，主验收人和供货确认不能是同一人，必须是供应商或本机构的不同用户。</span></div>'
			}];
		});

		me.buttonToolbarItems = ['->', 'accept'];

		me.callParent(arguments);
	},
	/**更改标题*/
	changeTitle: function() {

	},
	/**获取验收确认的系统参数信息*/
	getSecAccepterType: function(callback) {
		var me = this;
		var secAccepterType = Ext.util.Cookies.get("SecAccepterAccount"); //JcallShell.System.Cookie.get
		if(!secAccepterType) {
			var url = JShell.System.Path.getRootUrl("/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=SecAccepterAccount");
			JcallShell.Server.get(url, function(data) {
				if(data.success) {
					var obj = data.value;
					if(obj) {
						var paraValue = parseInt(obj.ParaValue);
						if(paraValue)
							me.secAccepterType = parseInt(paraValue);
						else
							paraValue = me.secAccepterType;
						var days = 30;
						var exp = new Date();
						var expires = exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
						Ext.util.Cookies.set("SecAccepterAccount", paraValue);
					}
					if(callback) callback();
				}
			}, false);
		} else {
			me.secAccepterType = parseInt(secAccepterType);
			if(callback) callback();
		}
	},
	onAcceptClick: function() {
		var me = this,
			values = me.getValues();
		if(me.secAccepterType == 3) {
			if(!values.Account || !values.Pwd) {
				JShell.Msg.error('必须填写供货确认账号密码才能验收，请填写后操作！');
				return;
			}

			if(values.Account == JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME)) {
				JShell.Msg.error('验收时，供货确认不能是登录者本人，请重新填写供货确认账号！');
				return;
			}
		}
		me.fireEvent('accept', me, {
			InvoiceNo: values.InvoiceNo,
			AcceptMemo: values.AcceptMemo,
			Account: values.Account,
			Pwd: values.Pwd
		});
		//		JShell.Msg.confirm({
		//			msg: '确认验收后将不能更改供货单明细内容，确认验收吗？'
		//		}, function(but) {
		//			if(but != "ok") return;
		//
		//			me.fireEvent('accept', me, {
		//				InvoiceNo: values.InvoiceNo,
		//				AcceptMemo: values.AcceptMemo,
		//				Account: values.Account,
		//				Pwd: values.Pwd
		//			});
		//		});
	}
});