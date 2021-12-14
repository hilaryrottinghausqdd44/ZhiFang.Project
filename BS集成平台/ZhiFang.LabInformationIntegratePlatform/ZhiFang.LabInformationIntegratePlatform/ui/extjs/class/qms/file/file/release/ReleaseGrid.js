/**
 * 文档发布应用
 * @author
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.release.ReleaseGrid', {
	extend: 'Shell.class.qms.file.file.examine.Grid',
	title: '文档发布',
	/**默认加载数据*/
	defaultLoad: true,
	/**文档状态默认为待发布*/
	defaultStatusValue: "4",
	FTYPE: '',
	
	DisagreeOfGridText: '撤销发布',
	AgreeButtonText: '发布',
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	/**审批人是登陆者*/
	defaultWherefile: 'ffile.PublisherId=',
    HiddenDisagreeButton:false,
    HiddenAgreeButton: true,
	IDS: '',
	hasDisagreeOfGridText: true,
	DisagreeButtonText:'打回起草人',
	/**文档状态*/
	AgreeOperationType: 5,
	/**文档操作记录备注(同意)*/
	ffileOperationMemo: '自动审核',
	/**文档操作记录备注(不同意)*/
	disffileMemo: '打回起草人',
	/**打回起草人状态*/
	DisagreeStatus: 15,
    agreefFileOperationType:15,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [true, true, true, true, true],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, false, false],
	MemoPrefix: '发布',
	hasNextExecutor: false,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.ApprovalDateTime',
	/**打回起草人是否需要变更状态,true变更*/
	IsChangeStatus: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				var IsDiscuss = record.get("FFile_IsDiscuss");
				var form = 'Shell.class.qms.file.file.examine.Form';
				var Status = ""+record.get("FFile_Status");
				if(Status == '4'||Status == '11') {//已批准,撤消发布
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
				me.openShowTabPanel(records[0], me.title, null);
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
					if(Status == '5') {
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
									//撤销发布
									//me.UpdateFFileStatus(11, id, 11);
									me.UpdateFFileStatus(4, id, 11);
								}
								me.onSearch();
							}
						}
					});
				} else {
					var msg = '只能撤销文档状态为已发布的数据!';
					JShell.Msg.error(msg);
				}
			},
			onPublisherSaveClick: function(grid, rowIndex) {
				var me = this;
				var rec = grid.getStore().getAt(rowIndex);
				var id = rec.get('FFile_Id');
				if(id != '') {
					me.openReleaseForm(id);
				}
			},
			save: function(win) {
				me.onSearch();
			}
		});
	},
	/**打开新增或编辑文档表单*/
	openReleaseForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.file.release.ReleaseForm', {
			formtype: 'edit',
			PK: id,
			listeners: {
				save: function(win, params) {
					var msg = "请确认是否更改发布信息" + "?";
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.saveffile(params, id);
								win.close();
								me.onSearch();
							}
						},
						icon: Ext.MessageBox.QUESTION
					});
				},
				load: function(from, data) {
					JShell.Action.delay(function() {
						//抄送人信息获取处理
						var copyFrom = from.getComponent('FFileReadingUser');
						from.formtype = 'edit';
						if(id != "") {
							copyFrom.loadDataById(id);
						}
					}, null, 100);
				}
			}
		}).show();
	},
	/*文档新增及编辑保存*/
	saveffile: function(paramsRelease, id) {
		var me = this;
		var url = me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = me.getEditParams(id);
		if(!params) return;

		//抄送对象类型, 默认没有选择
		params.ffileCopyUserType = -1;
		params.fFileCopyUserList = [];
		params.ffileReadingUserType = -1;
		params.fFileReadingUserList = [];
		params.ffileOperationMemo = '发布';
		//发布信息处理
		if(paramsRelease != null) {
			params.ffileReadingUserType = paramsRelease.ffileReadingUserType;
			params.fFileReadingUserList = paramsRelease.fFileReadingUserList;
			if(paramsRelease.entity != null) {
				params.entity.IsTop = paramsRelease.entity.IsTop;
				params.entity.IsDiscuss = paramsRelease.entity.IsDiscuss;
				if(paramsRelease.entity.BeginTime) {
					params.entity.BeginTime = paramsRelease.entity.BeginTime;
				}
				if(paramsRelease.entity.EndTime) {
					params.entity.EndTime = paramsRelease.entity.EndTime;
				}
			}
		}
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var entity = {};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//发布人
		entity.PublisherId = userId;
		entity.PublisherName = userName;
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function(id) {
		var me = this,
			entity = me.getAddParams();
		var fields = ['Id', 'Status'];
		//发布信息处理
		fields.push('BeginTime', 'EndTime', 'IsTop', 'IsDiscuss');
		fields.push('PublisherDateTime');
		if(entity) {
			entity.fields = fields.join(',');
			entity.entity.Id = id;
			entity.fFileOperationType = 5;
			entity.entity.Status = 5;
		}
		return entity;
	},
	/**文档状态下拉选择框数据处理*/
	removeDataList: function(dataList) {
		var me = this;
		var me = this;
		var returndata=[];
		if(!dataList) return returndata;
		var removeIdStr = ["4","5","11","15"]; //已批准,发布,撤消发布,打回起草人
		for(var i = 0; i < dataList.length; i++) {
			var model = dataList[i];
			if(model && Ext.Array.indexOf(removeIdStr,"" + model[0])>-1) {
				returndata.push(model);
			}
		}
		return returndata;
	}
});