/**
 * 退款申请审核流程表单
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.basic.ViewInfoForm', {
	extend: 'Shell.ux.form.Panel',
	bodyPadding: '5px 5px 5px 5px',
	labelStyle: 'fontWeight:bold;color:black', //#00F;
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 125,
		width: 315,
		labelAlign: 'right'
	},
	border: false,
	formtype: "add",
	title: '',
	width: 440,
	height: 145,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	hiddenPayDate:true,
	autoScroll: true,
	PK: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.height=me.hiddenPayDate?145:165;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createPayDate("完成时间");
		me.createViewInfo(me.title);
	},

	/**意见*/
	createViewInfo: function(fieldLabel) {
		var me = this;
		me.ViewInfo = {
			fieldLabel:'',
			labelStyle: me.labelStyle,
			xtype: 'textarea',
			border: false,
			name: 'ViewInfo',
			itemId: 'ViewInfo',
			minHeight: 20,
			height: 65,
			maxLengthText: me.title,
			emptyText:me.title,
			style: {
				marginBottom: '2px'
			}
		};
	},
	createPayDate: function(fieldLabel) {
		var me = this;
		var serverTime = JcallShell.System.Date.getDate();
		me.PayDate = {
			fieldLabel: fieldLabel,
			labelStyle: me.labelStyle,
			width: 215,
			hidden:me.hiddenPayDate,
			allowBlank:(me.hiddenPayDate?true:false),
			name: 'PayDate',
			itemId: 'PayDate',
			xtype: 'datefield',
			value: serverTime,
			format: 'Y-m-d'
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddTableLayoutItems: function() {
		var me = this,
			items = [];
		me.PayDate.colspan = 1;
		me.PayDate.width = 225;
		items.push(me.PayDate);

		me.ViewInfo.colspan = 1;
		me.ViewInfo.width = me.width - 20;
		items.push(me.ViewInfo);

		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		return items;
	},
	/**@overwrite 另存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		values.ViewInfo = values.ViewInfo.replace(/\\/g, '&#92');
		values.ViewInfo = values.ViewInfo.replace(/[\r\n]/g, '<br />');
		if(values.PayDate) {
			values.PayDate = JShell.Date.toServerDate(values.PayDate);
		}else{
			var serverTime = JcallShell.System.Date.getDate();
			values.PayDate = JShell.Date.toServerDate(serverTime);
		}
		if(values.ViewInfo==null){
			values.ViewInfo="";
		}
		me.fireEvent('save', me, values);
	}
});