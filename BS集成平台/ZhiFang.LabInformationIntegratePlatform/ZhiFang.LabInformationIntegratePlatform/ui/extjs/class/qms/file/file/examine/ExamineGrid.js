/**
 * 文档审核应用
 * @author liangyl
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.examine.ExamineGrid', {
	extend: 'Shell.class.qms.file.file.examine.Grid',
	title: '文档审核',
	
	defaultLoad: true,
	//文档状态默认为发布
	defaultStatusValue: "2",
	/**1:审核人2:审批人 3:发布*/
	CheckerType: '1',
	DisagreeOfGridText: '撤销审核',
	HiddenDisagreeOfGrid: false,
	FTYPE: '',
	IDS: '',
	AgreeButtonText: '通过',
	/**文档审核通过状态*/
	AgreeOperationType: 3,
	DisagreeButtonText: '不通过',
	/**文档审核不通过状态*/
	DisagreeStatus: 9,
	/**文档操作记录备注(同意)*/
	ffileOperationMemo: '自动审核',
	/**文档操作记录备注(不同意)*/
	disffileMemo: '审核不通过',
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, false, false, false],
	/**功能按钮是的boxLabel显示*/
	boxLabelRadiogroupChoose: ['只起草', '仅审核', '仅批准', '发布'],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, true, false, false],
	MemoPrefix: '审核',
	/**审批人是登陆者*/
	defaultWherefile: 'ffile.CheckerId=',
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.DrafterDateTime',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				var IsDiscuss = record.get("FFile_IsDiscuss");
				var form = 'Shell.class.qms.file.file.examine.Form';
				var Status = "" + record.get("FFile_Status");
				if(Status == '2' || Status == '10') { //已提交,撤销审批
					me.openShowTabPanel(record, me.title, form);
				} else {
					me.openShowTabPanel(record, me.title, null);
				}
			},
			onShowClick: function(grid) {
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get("FFile_Id");
				var IsDiscuss = records[0].get("FFile_IsDiscuss");

				me.openFFileForm(id, 'Shell.class.qms.ffile.search.showForm', '文档查看', true);
			},
			//撤销审核
			onDisagreeSaveClick: function(grid) {
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var isExec = true;
				var tempArr = [];
				for(var i in records) {
					var Status = records[i].get("FFile_Status");
					if(Status == '3') { //已审核,撤销审批 || Status == '10'
						tempArr.push(records[i]);
					} else {
						isExec = false;
						break;
					}
				}
				if(isExec && tempArr.length > 0) {
					var msg = "请确认是否" + me.DisagreeOfGridText + "?";
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								for(var i = 0; i < tempArr.length; i++) {
									var id = tempArr[i].get("FFile_Id");
									//撤销审核
									me.UpdateFFileStatus(2, id, 9);
									//me.UpdateFFileStatus(2, id, 9);
								}
								me.onSearch();
							}
						}
					});
				} else {
					var msg = '只能撤销文档状态为已审核的数据!';
					JShell.Msg.error(msg);
				}
			},
			save: function(win) {
				me.onSearch();
			}
		});
	},
	/**文档状态下拉选择框数据处理*/
	removeDataList: function(dataList) {
		var me = this;
		var returndata = [];
		if(!dataList) return returndata;
		var removeIdStr = ["1", "7", "8", "15"]; //暂存,作废,撤消提交,打回起草人		
		for(var i = 0; i < dataList.length; i++) {
			var model = dataList[i];
			if(model && Ext.Array.indexOf(removeIdStr,"" + model[0]) == -1) {
				returndata.push(model);
			}
		}
		return returndata;
	}
});