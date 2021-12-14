/**
 * 退费
 * @author liangyl	
 * @version 2017-02-23
 */
Ext.define('Shell.class.weixin.ordersys.seach.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '填写退费信息',
	width: 250,
	height: 190,
	bodyPadding: 10,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchOSUserOrderFormById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSUserOrderFormRefundE',
	/**修改服务地址*/
    editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderFormByField',
    /**管理订单退费申请*/
	RefundEUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSUserOrderFormRefundE',
    /**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
 	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**银行种类*/
	Bank: 'Bank',
	/**显示成功信息*/
	showSuccessInfo: false,
	PK:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
			fieldLabel: '退费原因',
			xtype: 'textarea',
			height:80,
			emptyText:'必填项',allowBlank:false,
			name: 'OSUserOrderForm_RefundReason',
			itemId: 'OSUserOrderForm_RefundReason'
		}, {
			fieldLabel: '退费价格',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			name: 'OSUserOrderForm_RefundPrice',
			itemId: 'OSUserOrderForm_RefundPrice',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '退款银行账号',
			name: 'OSUserOrderForm_BankAccount',
			hidden:true,
			itemId: 'OSUserOrderForm_BankAccount'
		},{
			fieldLabel: '备注',
			xtype: 'textarea',
			height:80,
			hidden:true,
			name: 'OSUserOrderForm_Memo',
			itemId: 'OSUserOrderForm_Memo'
		}, {
			fieldLabel: '主键ID',
			name: 'OSUserOrderForm_Id',
			value:me.PK	,
			hidden: true
		}];

		return items;
	},

   /**获取当前用户所负责的客户Ids数组*/
	onSaveClick: function() {
		var me = this;
	   if(!me.getForm().isValid()) return;
		var where = '',values = me.getForm().getValues();
		var url  = JShell.System.Path.getRootUrl(me.RefundEUrl);
		if(values.OSUserOrderForm_Id){
			where+='?OrderFormID='+values.OSUserOrderForm_Id;
		}
		if(values.OSUserOrderForm_RefundReason){
			where+='&RefundReason='+values.OSUserOrderForm_RefundReason.replace(/\\/g, '&#92');
		}
		if(values.OSUserOrderForm_RefundPrice){
			where+='&RefundPrice='+values.OSUserOrderForm_RefundPrice;
		}
		if(values.OSUserOrderForm_Memo){
			where+='&MessageStr='+values.OSUserOrderForm_Memo.replace(/\\/g, '&#92');
		}
        url=url+where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.fireEvent('save',me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.OSUserOrderForm_Id=me.PK;
		return data;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
		
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});