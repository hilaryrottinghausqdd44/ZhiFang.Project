/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.import.Form', {
	extend: 'Shell.ux.form.Panel',

	title: '条码规则信息',
	width: 360,
	height: 220,

	/**新增服务地址*/
	uploadUrl: '/ReaSysManageService.svc/ST_UDTO_UploadReaCenBarCodeFormatOfAttachment',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0 px 0 px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 170,
		labelAlign: 'right'
	},
	/**当前的供货方平台机构编码*/
	PlatformOrgNo: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = me.width / me.layout.columns - 10;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: "附件",
			name: 'file',
			itemId: 'file',
			xtype: 'filefield',
			buttonText: '选择',
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false
		});

		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '提示',
			name: 'ReaCenBarCodeFormat_Memo',
			itemId: 'ReaCenBarCodeFormat_Memo',
			colspan: 1,
			width: me.defaults.width * 1,
			height: 80,
			readOnly: true,
			locked: true
		});

		return items;

	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.uploadUrl
		me.getForm().submit({
			url: url,
			//waitMsg: "程序信息保存处理中,请稍候...",
			//async: false,
			success: function(form, action) {
				var data = action.result;
				me.hideMask();
				if(data.success) {
					JShell.Msg.alert('导入条码规则信息成功！',null,2000);
					me.fireEvent('save', me);
				} else {
					JShell.Msg.error(msg);
				}
			},
			failure: function(form, action) {
				var data = action.result;
				me.hideMask();
				JShell.Msg.error('服务错误！' + data.ErrorInfo);
			}
		});
	}
});