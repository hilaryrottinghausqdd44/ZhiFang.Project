/**
 * 点击同步出库单弹出的form
 * @author zq
 * @version 2021-05-25
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.SynchForm', {
	extend: 'Shell.ux.form.Panel',
	title: '同步出库单',
	width: 280,
	height: 160,
	/**同步出库单服务*/
	synchOutUrl: '/ReaCustomInterface.svc/RS_GetOutOrderInfoByInterface?ncBillNo=',
	/**显示成功信息*/
	showSuccessInfo:false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用取消按钮*/
	hasCancel: true,
	formtype:'add',

	/**内容周围距离*/
	bodyPadding: 10,
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 70,
		labelAlign: 'right'
	},
	/**form中需要的赋值*/
//	orderId: null, // 供货单的主键
//	saleDocNo: '', // 供货单号
	ncBillNo: '', // 单号

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [
//		{
//			fieldLabel: '供单主键',
//			emptyText: '供单主键',
//			allowBlank: false,
//			hidden: true,
//			itemId: 'ReaBmsCenSaleDoc_Id',
//			name: 'ReaBmsCenSaleDoc_Id',
//			value: me.id
//
//		},
		{
			fieldLabel: '单号',
			emptyText: '单号',
			xtype: 'textfield',
			enableKeyEvents: true,
			readOnly:false,
			itemId: 'ncBillNo',
			name: 'ncBillNo',
			listeners: {
				specialkey: function(field, e) {
					me.onScanCodeClick(field, e);
				}
			}

		}];

		return items;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**扫码事件：获取扫码的单号DocNo**/ 
	onScanCodeClick: function(){
		var me = this;
		if(e.getKey() == Ext.EventObject.ENTER) {
			//防止扫码时,自动出现触发多个回车事件
			JShell.Action.delay(function() {
				//"\s"匹配任何不可见字符，包括空格、制表符、换页符等等
				me.ncBillNo = field.getValue().trim().replace(/\s+/g, '');
				if(!me.ncBillNo) {
					JShell.Msg.alert("请输入单号!", null, 2000);
				}
			}, null, 30);
		}
	},
	/**@overwrite 重写保存按钮*/
	onSaveClick: function(){
		var me = this;
		var values = me.getForm().getValues();
		var ncBillNo = me.ncBillNo || values.ncBillNo; // 获取由手工输入的单号或者是扫码产生的单号
		if(!ncBillNo) {
			JShell.Msg.alert("请输入单号!", null, 2000);
			return;
		}
		var url = JShell.System.Path.ROOT + me.synchOutUrl + ncBillNo;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				JShell.Msg.alert('NC出库单获取成功！', null, 1500);
			} else {
				JShell.Msg.error('NC出库单获取失败！' + data.msg);
			}
		});

	}
	
});