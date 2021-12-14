/**
 * 文档新增
 * @author longfc
 * @version 2017-05-15
 */
Ext.define('Shell.class.qms.file.file.basic.AddTabPanel', {
	extend: 'Shell.class.qms.file.basic.AddTabPanel',
	
	basicFormApp: 'Shell.class.qms.file.file.create.Form',
	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || "";
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.basicFormApp = me.basicFormApp || 'Shell.class.qms.file.file.create.Form';
		
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**确定提交按钮点击处理方法*/
	onAgreeSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		var form = me.getComponent('basicForm');
		switch(me.AgreeOperationType) { //
			case 1: //文档起草确认提交操作类型
				//确定提交修改文档状态为2
				me.fFileStatus = 2;
				me.fFileOperationType = 2;
				//审核人
				itemId = "FFile_CheckerName";
				me.ffileOperationMemo = "起草并确认提交";
				if(me.hasNextExecutor == true)
					msg = "下一执行者【审核人】信息不能为空!";
				break;
			case 3: //审核通过
				me.fFileStatus = 3;
				me.fFileOperationType = 3;
				me.ffileOperationMemo = "起草并直接自动审核";
				if(me.hasNextExecutor == true)
					msg = "下一执行者【审批人】信息不能为空!";
				break;
			case 4: //批准通过
				me.fFileStatus = 4;
				me.fFileOperationType = 4;
				me.ffileOperationMemo = "起草并直接自动审批";
				if(me.hasNextExecutor == true)
					msg = "下一执行者【发布人】信息不能为空!";
				break;
			case 5: //发布文档
				me.fFileStatus = 5;
				me.fFileOperationType = 5;
				isExec = false;
				me.ffileOperationMemo = "起草并直接自动发布";
				me.openFFileForm(me.FFileId);
				//发布人
				if(me.hasNextExecutor == true)
					msg = "发布人信息不能为空!";
				break;
			case 7: //作废
				me.fFileStatus = 7;
				me.fFileOperationType = 7;
				itemId = "";
				msg = "作废!";
				me.ffileOperationMemo = "作废";
				break;
			default:
				me.fFileOperationType = 2;
				me.fFileStatus = 2;
				me.ffileOperationMemo = "提交";
				break;
		}
		//文档状态如果不是发布
		if(isExec && me.fFileStatus != 5) {
			if(me.hasNextExecutor == true) {
				//下一执行者处理
				var NextExecutorId = "";
				var comId = me.getComponent("buttonsToolbar").getComponent('NextExecutorId');
				if(comId) {
					NextExecutorId = comId.getValue();
				}
				if(NextExecutorId == "") {
					if(msg === "") {
						msg = "下一执行者信息不能为空!";
					}
					JShell.Msg.error(msg);
					isExec = false;
				}
			}
		}
		if(isExec) {
			msg = "请确认是否" + me.ffileOperationMemo + "?";
			Ext.MessageBox.show({
				title: '操作确认消息',
				msg: msg,
				width: 300,
				zIndex: 999990,
				style: {
					zIndex: 999990
				},
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						me.saveffile(null);
					}
				},
				icon: Ext.MessageBox.QUESTION
			});
		}
	},
	/**不通过按钮点击处理方法*/
	onDisagreeSaveClick: function() {
		var me = this;
	},
	/**文档的审核通过/不通过;同意/不同意*/
	UpdateFFileStatus: function() {
		var me = this;
	},
	/*文档新增及编辑保存*/
	saveffile: function(paramsRelease) {
		var me = this;
	}
});