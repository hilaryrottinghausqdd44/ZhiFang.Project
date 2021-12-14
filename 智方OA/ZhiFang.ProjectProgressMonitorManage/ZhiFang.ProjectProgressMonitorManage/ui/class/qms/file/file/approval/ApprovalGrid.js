/**
 * 文档批准应用
 * @author
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.approval.ApprovalGrid', {
	extend: 'Shell.class.qms.file.file.examine.Grid',
	title: '文档批准',
	/**默认加载数据*/
	defaultLoad: true,
	/**文档状态默认为发布*/
	defaultStatusValue: "3",
	FTYPE: '',
	/**审批人是登陆者*/
	defaultWherefile: 'ffile.ApprovalId=',
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	IDS: '',
	DisagreeOfGridText: '撤销审批',
	/**文档状态*/
	AgreeOperationType: 4,
	/**文档操作记录备注(同意)*/
	ffileOperationMemo: '自动审批',
	/**文档操作记录备注(不同意)*/
	disffileMemo: '不同意审批',
	/**不同意/通过按钮隐藏*/
	HiddenDisagreeOfGrid: false,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, true, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, true, false],
	MemoPrefix: '审批',
	AgreeButtonText: '同意',
	DisagreeButtonText: '不同意',
	/**文档审批不同意状态*/
	DisagreeStatus: 10,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.CheckerDateTime',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				var IsDiscuss = record.get("FFile_IsDiscuss");
				var form = 'Shell.class.qms.file.file.examine.Form';
				var Status = "" + record.get("FFile_Status");
				if(Status == '3' || Status == '11') { //已审核,撤消发布
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
				me.openShowTabPanel(records[0], '文档查看', null);
			},
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
					if(Status == '4') { //已批准,撤消发布|| Status == '11'
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
									//撤销审批
									//me.UpdateFFileStatus(10, id, 10);
									me.UpdateFFileStatus(3, id, 10);
								}
								me.onSearch();
							}
						}
					});
				} else {
					var msg = '只能撤销文档状态为已审批的数据!';
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
		var me = this;
		var returndata = [];
		if(!dataList) return returndata;
		var removeIdStr = ["1", "2", "7", "8", "9", "15"]; //暂存,已提交,作废,撤消提交,审核退回,打回起草人
		for(var i = 0; i < dataList.length; i++) {
			var model = dataList[i];
			if(model && Ext.Array.indexOf(removeIdStr,"" + model[0]) == -1) {
				returndata.push(model);
			}
		}
		return returndata;
	}
});