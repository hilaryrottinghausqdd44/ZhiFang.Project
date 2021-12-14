Ext.ns('Shell.class.rea.client.initialization');
Ext.define('Shell.class.rea.client.initialization.license.UploadPanel', {
	extend: 'Ext.form.Panel',
	bodyPadding: 10,
	layout: 'hbox',
	title: '机构授权初始化',
	hasLoadMask: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onUploadClick', 'addClick');
		me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.items = [{
			xtype: 'filefield',
			width: 265,
			labelWidth: 65,
			name: 'file',
			itemId: 'file',
			fieldLabel: '',
			labelAlign: 'right',
			allowBlank: false,
			emptyText: "机构授权文件上传",
			buttonConfig: {
				iconCls: 'button-search',
				text: '选择'
			}
		}, {
			xtype: 'button',
			style: {
				margin: "0px 0px 0px 5px"
			},
			height: 22,
			width: 80,
			iconCls: 'button-accept',
			text: '确认上传',
			handler: function() {
				me.fireEvent('addClick', me);
				//				me.onUploadAuthorization();
			}
		}];
	},
	onUploadAuthorization: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/ST_UDTO_UploadAuthorizationFileOfClient";
		me.getForm().submit({
			url: url,
			//			waitMsg: "正在进行机构注册授权初始化处理...请稍等！",
			success: function(form, action) {
				var result = action.result;
				var resultDataValue = result.ResultDataValue;
				me.fireEvent('onUploadClick', resultDataValue);
			},
			failure: function(form, action) {
				var result = action.result;
				var resultDataValue = "";
				if(result == 'undefined' || result == undefined) {
					resultDataValue = "机构初始化失败";
					JShell.Action.delay(function() {
						JShell.Msg.error(resultDataValue);
					}, null, 500);
				} else {
					resultDataValue = result.ResultDataValue;
				}
				me.fireEvent('onUploadClick', resultDataValue);
			}
		});
	}
});