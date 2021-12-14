/**
 * 知识库发布应用
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.knowledgebase.release.App', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '知识库发布',

	
	/**文件的操作记录类型*/
	fFileOperationType: 5,

	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "发布",
	/**基本应用的文档确认(直接发布)操作按钮的功能类型*/
	AgreeOperationType:5,

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
	hiddenRadiogroupChoose: [false, true, true, true, true],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, false, true],
	hasNextExecutor:false,
	basicGrid: 'Shell.class.qms.file.knowledgebase.release.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.FTYPE='3';
		me.callParent(arguments);
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