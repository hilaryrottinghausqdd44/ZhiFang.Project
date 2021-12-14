/**
 * 员工财务账户信息
 * @author longfc
 * @version 2016-11-18
 */
Ext.define('Shell.class.sysbase.user.pempfinanceaccount.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '员工财务账户信息',

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountById?isPlanish=false',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPEmpFinanceAccount',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePEmpFinanceAccountByField',

	bodyPadding: 5,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 85,
		labelAlign: 'right'
	},
	/**启用表单状态初始化*/
	openFormType: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	/*是否管理员应用**/
	isManage: false,
	/*该员工是否已经存在员工财务账户**/
	IsExist: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//拼音字头监听
		me.initPinYinZiTouListeners();
	},
	initComponent: function() {
		var me = this;

		me.buttonToolbarItems = ['->', 'save', 'reset'];

		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//姓名
		items.push({
			fieldLabel: '员工姓名',
			name: 'Name',
			readOnly: true,
			locked: true,
			itemId: 'Name'
		});

		//拼音字头、快捷码
		items.push({
			fieldLabel: '拼音字头',
			name: 'PinYinZiTou',
			itemId: 'PinYinZiTou'
		}, {
			fieldLabel: '快捷码',
			name: 'Shortcode',
			itemId: 'Shortcode'
		},{
			fieldLabel: '简称',
			name: 'SName',
			itemId: 'SName'
		});

		items.push({
			fieldLabel: '单笔借款上限',
			xtype: 'numberfield',
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			allowBlank: true,
			emptyText: "单笔借款上限",
			name: 'OneceLoanUpperAmount',
			itemId: 'OneceLoanUpperAmount'
		}, {
			fieldLabel: '借款上限',
			xtype: 'numberfield',
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			allowBlank: true,
			emptyText: "借款上限",
			name: 'LoanUpperAmount',
			itemId: 'LoanUpperAmount'
		}, {
			xtype: 'numberfield',
			fieldLabel: '借款总额',
			readOnly: (me.isManage == false ? true : false),
			locked: (me.isManage == false ? true : false),
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			allowBlank: true,
			emptyText: "借款总额",
			name: 'LoanAmount',
			itemId: 'LoanAmount'
		}, {
			fieldLabel: '待还额度',
			readOnly: (me.isManage == false ? true : false),
			locked: (me.isManage == false ? true : false),
			xtype: 'numberfield',
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			allowBlank: true,
			emptyText: "待还额度",
			name: 'UnRepaymentAmount',
			itemId: 'UnRepaymentAmount'
		}, {
			xtype: 'numberfield',
			fieldLabel: '还款总额',
			readOnly: (me.isManage == false ? true : false),
			locked: (me.isManage == false ? true : false),
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			allowBlank: true,
			emptyText: "还款总额",
			name: 'RepaymentAmount',
			itemId: 'RepaymentAmount'
		});

		items.push({
			boxLabel: '是否使用',
			name: 'IsUse',
			xtype: 'checkbox',
			style: {
				marginLeft: '85px'
			},
			checked: true
		}, {
			fieldLabel: '主键ID',
			name: 'Id',
			hidden: true
		}, {
			fieldLabel: '员工ID',
			name: 'EmpID',
			hidden: true
		}, {
			boxLabel: '是否存在员工财务账户',
			name: 'IsExist',
			hidden: true,
			xtype: 'checkbox',
			style: {
				marginLeft: '85px'
			},
			checked: true
		});

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Name: values.Name,
			EmpID: values.EmpID,
			PinYinZiTou: values.PinYinZiTou,
			Shortcode: values.Shortcode,
			SName: values.SName,
			IsUse: values.IsUse ? true : false
		};
		if(values.OneceLoanUpperAmount) {
			entity.OneceLoanUpperAmount = values.OneceLoanUpperAmount;
		}
		if(values.LoanUpperAmount) {
			entity.LoanUpperAmount = values.LoanUpperAmount;
		}
		if(values.LoanAmount) {
			entity.LoanAmount = values.LoanAmount;
		}
		if(me.isManage == true) {
			if(values.UnRepaymentAmount) {
				entity.UnRepaymentAmount = values.UnRepaymentAmount;
			}
			if(values.RepaymentAmount) {
				entity.RepaymentAmount = values.RepaymentAmount;
			}
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();

		var fields = [
			'OneceLoanUpperAmount', 'LoanUpperAmount', 'LoanAmount', 'EmpID', 'Name', 'PinYinZiTou', 'Shortcode','SName', 'IsUse', 'Id'
		];
		if(me.isManage == true) {
			fields.push("UnRepaymentAmount", "RepaymentAmount");
		}
		entity.fields = fields.join(',');

		entity.entity.Id = values.Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {

		return data;
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners: function() {
		var me = this,
			Name = me.getComponent('Name');
		Name.on({
			change: function(field, newValue, oldValue, eOpts) {
				setTimeout(function() {
					me.changePinYinZiTou();
				}, 100);
			}
		});
	},
	changeCName: function() {
		var me = this;
	},
	changePinYinZiTou: function(data) {
		var me = this,
			Name = me.getComponent('Name'),
			PinYinZiTou = me.getComponent('PinYinZiTou'),
			Shortcode = me.getComponent('Shortcode');

		var name = Name.getValue();

		if(name != "") {
			JShell.Action.delay(function() {
				JShell.System.getPinYinZiTou(name, function(value) {
					me.getForm().setValues({
						PinYinZiTou: value,
						Shortcode: value
					});
				});
			}, null, 200);
		} else {
			me.getForm().setValues({
				PinYinZiTou: "",
				Shortcode: ""
			});
		}
	},
	isAdd: function(obj) {
		var me = this;
		me.callParent(arguments);

		me.getForm().setValues(obj);
	}
});