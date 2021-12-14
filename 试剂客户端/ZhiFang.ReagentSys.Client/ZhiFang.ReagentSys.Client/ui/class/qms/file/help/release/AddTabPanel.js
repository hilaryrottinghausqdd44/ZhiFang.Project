/**
 * 帮助系统发布应用
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.release.AddTabPanel', {
	extend: 'Shell.class.qms.file.file.create.AddTabPanel',
	requires: [
		'Shell.class.qms.file.basic.toolbarButtons'
	],
	header: true,
	activeTab: 0,
	title: '帮助信息发布',
	border: false,
	closable: true,
	hasNextExecutor: false,
	/**显示附件页签	,false不显示*/
	hasUploadForm:false,
	basicFormApp: 'Shell.class.qms.file.help.release.Form',
	/**帮助系统生成html及Json文件服务地址*/
	saveUrl: '/CommonService.svc/QMS_UDTO_SaveHelpHtmlAndJson',
	FTYPE: '5',
	initComponent: function() {
		var me = this;
		me.basicFormApp = me.basicFormApp || 'Shell.class.qms.file.help.release.Form';

		me.FTYPE = "5";
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			save: function() {
				var url = JShell.System.Path.getRootUrl(me.saveUrl) + "?id=" + me.PK;
				JShell.Server.get(url, function(data) {
					var success = data.success;
					var callbackData = {
						success: data.success,
						msg: data.msg
					};
					if(data.success) {
						//JShell.Msg.alert("导出帮助文档成功!", null, 1000);
					} else {
						JShell.Msg.alert("导出帮助文档失败!", null, 1000);
					}
				}, false);
			}
		});
	},
	/**确定提交按钮点击处理方法*/
	onAgreeSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		var form = me.getComponent('basicForm');
		me.fFileStatus = 5;
		me.fFileOperationType = 5;
		me.ffileOperationMemo = "帮助信息发布";
		me.saveffile(null);

	}
});