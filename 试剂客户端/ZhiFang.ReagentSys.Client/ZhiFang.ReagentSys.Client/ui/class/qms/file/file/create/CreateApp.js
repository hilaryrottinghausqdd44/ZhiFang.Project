/**
 * 文档起草基本应用
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.qms.file.file.create.CreateApp', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '文档起草',

	/**文件的操作记录类型*/
	fFileOperationType: 1,
	
	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "确认提交",
	/**基本应用的文档确认(通过/同意)操作按钮的功能类型*/
	AgreeOperationType: 1,

	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: false,
	/**基本应用的文档确认(不通过/不同意)操作按钮显示名称*/
	DisagreeButtonText: "暂存",
	/**基本应用的文档确认(不通过/不同意)操作按钮的功能类型*/
	DisagreeOperationType: 1,
	/**提交并发布的操作按钮是否显示*/
	HiddenPublishButton: false,
	/**隐藏阅读人信息*/
	HiddenFFileReadingLog: true,

	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, false, false, false, false],
	
	basicGrid: 'Shell.class.qms.file.file.create.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '1';
		me.callParent(arguments);
	},
	/**
	 * 文档撤消文档状态操作
	 * @param {Object} statusValue 判断文档状态是否符合更新条件值
	 * @param {Object} updateValue 文档状态更新值
	 * @param {Object} fFileOperationType 文档操作类型值
	 */
	CancelFFileStatus: function(statusValue, updateValue, fFileOperationType) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var records = Grid.getSelectionModel().getSelection();
		if(records && records.length < 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var strId = "",
			updateText = "撤消提交操作",
			status = "";
		var tempArr = [];
		for(var i = 0; i < records.length; i++) {
			status = ""+records[i].get("FFile_Status");
			if(status && status== statusValue && status!= updateValue.toString()) {
				tempArr.push(records[i]);
			}
		}
		if(tempArr && tempArr.length < 1) {
			JShell.Msg.alert("请选择符合【" + Grid.DisagreeOfGridText + "】条件的文档操作!");
			return;
		}
		Ext.MessageBox.show({
			title: '撤消提交操作确认消息',
			msg: "请确认是否撤消提交操作",
			width: 300,
			buttons: Ext.MessageBox.OKCANCEL,
			fn: function(btn) {
				if(btn == 'ok') {
					Grid.updateErrorCount = 0;
					Grid.updateCount = 0;
					Grid.updateLength = tempArr.length;
					Grid.delText = updateText;
					for(var i = 0; i < tempArr.length; i++) {
						me.updateStatus(tempArr[i], tempArr[i].get("FFile_Id"), updateValue, fFileOperationType, i);
					}
				}
			}
		});

	},
	updateStatus: function(record, id, updateValue, fFileOperationType, index) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var url = (Grid.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + Grid.editUrl;
		var entity = {
			"Id": id,
			"Status": updateValue
		};
		var params = {
			fields: "Id,Status",
			entity: entity,
			fFileOperationType: fFileOperationType,
			ffileCopyUserType: -1,
			ffileReadingUserType: -1,
			ffileOperationMemo: '',
			fFileCopyUserList: [],
			fFileReadingUserList: []
		};
		params = Ext.JSON.encode(params);
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				Grid.hideMask(); //隐藏遮罩层
				if(data.success) {
					if(record) {
						record.set("FFile_Status", updateValue);
						record.set(Grid.DelField, true);
						record.set('ErrorInfo', "撤消提交成功");
						record.commit();
					}
					Grid.updateCount++;
				} else {
					Grid.updateErrorCount++;
					record.set(Grid.DelField, false);
					record.set('ErrorInfo', data.msg);
					record.commit();
				}
				if(Grid.updateCount + Grid.updateErrorCount == Grid.updateLength) {
					Grid.hideMask(); //隐藏遮罩层
					if(Grid.updateErrorCount == 0) {
						JShell.Msg.alert('撤消提交成功!');
					} else {
						JShell.Msg.error('存在操作失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	}
});