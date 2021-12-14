/**
 * 自定义项目配置
 * @author liangyl
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.report.finance.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '自定义项目配置',
	width: 640,
	height: 360,
	bodyPadding: 10,

	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContract',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,

	/**布局方式*/
	layout: 'anchor',
	formtype: 'edit',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 5,
		labelAlign: 'right'
	},
	afterRender: function () {
        var me = this;
        me.callParent(arguments);
        var myCheckboxGroup=me.getComponent('myCheckboxGroup');
        var Arr=[];
	    if(me.LocalStorage.get('FinanceReport')) {
	    	var tempArr = Ext.JSON.decode(me.LocalStorage.get('FinanceReport'));
	    	if(tempArr)Arr=Arr.concat(tempArr);
	    	for (var i = 0; i < myCheckboxGroup.items.length; i++)    
            {     
            	myCheckboxGroup.items.items[i].setValue(false);
            	for(var j = 0; j < Arr.length; j++) {
            		if(Arr[j] == myCheckboxGroup.items.items[i].inputValue) {
            		    myCheckboxGroup.items.items[i].setValue(true);
            		    break;
            		}
            	}
            }    
		}
    },
	initComponent: function() {
		var me = this;
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0) {
			me.dockedItems = dockedItems;
		}
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		items.push(me.createtoptoolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createtoptoolbar: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'label',
			text: '自定义项目配置',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 6 10'
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height: 30,
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
			xtype: 'checkboxgroup',
			fieldLabel: '',
			columns: 2,
			vertical: true,
			itemId: 'myCheckboxGroup',
			items: [
				{ boxLabel: '订单编号', name: 'rb', checked: true, inputValue: 'UOFCode'},
				{ boxLabel: '客户姓名', name: 'rb', checked: true, inputValue:'UserName' },
				{ boxLabel: '性别', name: 'rb', inputValue: 'SexName'},
				{ boxLabel: '市场价格', name: 'rb', checked: true, inputValue: 'MarketPrice' },
				{ boxLabel: '大家价格', name: 'rb', checked: true, inputValue: 'GreatMasterPrice' },
				{ boxLabel: '实际价格', name: 'rb', checked: true, inputValue:'Price' },
				{
					boxLabel: '开单日期',
					name: 'rb',
					checked: true,
					inputValue: 'BillingDate'
				},
				{
					boxLabel: '采样日期',
					name: 'rb',
					inputValue: 'SamplingDate'
				},
				{ boxLabel: '咨询费率', name: 'rb', checked: true, inputValue: 'AdvicePriceRate'},
				{ boxLabel: '咨询费', name: 'rb', checked: true, inputValue: 'AdvicePrice'},
				{ boxLabel: '开单医生', name: 'rb', checked: true, inputValue:'DoctorName'},
				{ boxLabel: '退款单号', name: 'rb', checked: true, inputValue: 'MRefundFormCode'},
				{ boxLabel: '退款金额', name: 'rb', checked: true, inputValue: 'RefundPrice' }
			]
		}];
		return items;
	},

	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values=[];
	    values = me.getForm().getValues();
		me.saveStore(values);
		me.fireEvent('save', me, values);
	},
	/**更改标题*/
	changeTitle: function() {},

	/**本地数据存储*/
	LocalStorage: {
		set: function(name, value) {
			localStorage.setItem(name, value);
		},
		get: function(name) {
			return localStorage.getItem(name);
		},
		remove: function(name) {
			localStorage.removeItem(name);
		}
	},

	/**本地保存localstorage*/
	saveStore: function(values) {
		var me = this;
		if(me.LocalStorage.get('FinanceReport')) {
			me.LocalStorage.remove('FinanceReport');
		}
		me.LocalStorage.set('FinanceReport', JSON.stringify(values.rb));
	}
});