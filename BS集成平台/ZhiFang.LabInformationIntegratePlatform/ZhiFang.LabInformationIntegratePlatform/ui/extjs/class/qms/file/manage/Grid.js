/**
 * 文档管理列表
 * @author 
 * @version 2016-07-12
 */
Ext.define('Shell.class.qms.file.manage.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档管理',
	width: 1200,
	height: 800,
	/**文档状态默认为发布*/
	defaultStatusValue: "",
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	/**文档的交流类型:对查询应用(show)的交流应用获取交流记录做默认时间(交流的addtime大于等于发布时间)过滤，起草等(edit)不需要 未完成*/
	interactionType: "show",
	checkOne: true,
	hideDelColumn: true,
	remoteSort: false,
	hasCheckBDictTree: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.DrafterDateTime',
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	/**是否显示操作记录页签*/
	hasOperation: true,
	/**是否显示阅读记录页签*/
	hasReadingLog: true,

	/**是否禁用按钮是否显示*/
	HiddenButtonLock: true,
	/**是否置顶按钮是否显示*/
	HiddenButtonDoTop: true,
	/**撤销置顶按钮是否显示*/
	HiddenButtonDoNoTop: true,
	/**修改置顶/撤消置顶服务地址*/
	editIsTopUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileIsTopByIds',
	/*当前列表是否应用在新闻管理或文档管理里**/
	isManageApp: false,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_IsTop',
		direction: 'DESC'
	}, {
		property: 'FFile_PublisherDateTime',
		direction: 'DESC'
	}, {
		property: 'FFile_BDictTree_Id',
		direction: 'ASC'
	}, {
		property: 'FFile_Status',
		direction: 'DESC'
	}, {
		property: 'FFile_Title',
		direction: 'ASC'
	}],

	initComponent: function() {
		var me = this;
		me.checkOne = me.checkOne || true;
		me.FTYPE = me.FTYPE || '';
		me.addEvents('ondoTopClick', me);
		me.addEvents('ondoNoTopClick', me);
		me.addEvents('onButtonLockClick', me);

		me.createdefaultWhere();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**overwrite*/
	createdefaultWhere: function() {
		var me = this;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";
	},
	/**
	 * overwrite
	 * 创建禁用内容按钮
	 * */
	createButtonLockButton: function(items) {
		var me = this;
		items.push({
			xtype: 'button',
			text: '禁用',
			hidden: me.HiddenButtonLock,
			iconCls: 'button-lock',
			tooltip: '<b>将选中的记录进行批量禁用</b>',
			handler: function() {
				me.fireEvent('onButtonLockClick', me);
			}
		});
		return items;
	},
	/**
	 * overwrite
	 * 创建置顶按钮
	 * */
	createdoTopButton: function(items) {
		var me = this;
		items.push({
			xtype: 'button',
			text: "<b style='color:red;'>置顶</b>",
			hidden: me.HiddenButtonDoTop,
			iconCls: 'button-edit',
			tooltip: '<b>将选中的记录进行批量置顶</b>',
			handler: function() {
				me.fireEvent('ondoTopClick', me);
			}
		});
		return items;
	},
	/**
	 * overwrite
	 * 创建撤销置顶按钮
	 * */
	createdoNoTopButton: function(items) {
		var me = this;
		items.push({
			xtype: 'button',
			text: "<b style='color:balck;'>撤销置顶</b>",
			hidden: me.HiddenButtonDoNoTop,
			iconCls: 'button-back',
			tooltip: '<b>将选中的记录进行批量撤销置顶</b>',
			handler: function() {
				me.fireEvent('ondoNoTopClick', me);
			}
		});
		return items;
	},
	/*更新树节点信息**/
	onUpdateBDictTreeAccept: function(record, select, win) {
		var me = this;
		var selectId = select.get('tid');
		if(selectId == "" || selectId == null) {
			JShell.Msg.alert("获取的类型Id值为空!", null, 1000);
		} else {
			var id = record.get('FFile_Id');
			var status = record.get('FFile_Status');

			var url = '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField';
			url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
			var entity = {
				Status: status,
				Id: id
			};
			if(selectId != "" || selectId != null) {
				entity.BDictTree = {
					Id: selectId
				};
			}
			var fields = 'Id,BDictTree_Id';
			var params = {
				entity: entity,
				ffileOperationMemo: "更新文档类型",
				fFileOperationType: "18",
				ffileCopyUserType: -1,
				fFileCopyUserList: [],
				ffileReadingUserType: -1,
				fFileReadingUserList: [],
				fields: fields
			};
			if(!params) return;
			params = Ext.JSON.encode(params);
			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.post(url, params, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					win.close();
					me.onSearch();
				} else {
					JShell.Msg.error("更新类型保存失败!");
				}
			});
		}
	},
	/*创建修改树类型节点操作列**/
	createEditBDictTreeColumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '类型',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					var selectId = record.get('FFile_BDictTree_Id');
					var id = record.get('FFile_Id');
					//var status = record.get('FFile_Status');
					if(id != '' && selectId != "") {
						JShell.Win.open('Shell.class.sysbase.dicttree.CheckTree', {
							selectId: selectId,
							rootVisible: false,
							IDS: me.IDS,
							LEVEL: me.LEVEL,
							listeners: {
								accept: function(win, select) {
									if(select == null) {
										JShell.Msg.alert("没有选择到类型", null, 1000);
										return;
									} else {
										me.onUpdateBDictTreeAccept(record, select, win);
									}
								}
							}
						}).show();
					}
				}
			}]
		};
	},
	/*创建修改信息操作列**/
	createEditColumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '编辑',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('FFile_Id');
					var status = rec.get('FFile_Status');
					if(id != '') {
						me.formtype = "edit";
						me.setAppOperationType();
						me.openFFileForm(rec, status, "edit");
					}
				}
			}]
		};
	},
	/*创建修改发布信息操作列**/
	createPublisher: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '发布',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					//发布功能对管理全部开放2016-11-02 longfc
					if(me.isManageApp == true) {
						return 'button-edit hand';
					} else {
						//发布应用时只有状态不为发布时才显示
						if(record.get('FFile_Status') != '5') {
							return 'button-edit hand';
						}
					}

				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('FFile_Id');
					if(id != '') {
						me.openReleaseForm(id);
					}
				}
			}]
		};
	},
	/**打开编辑表单*/
	openFFileForm: function(record, fFileStatus, formtype) {
		var me = this;

	},

	/**打开发布信息编辑表单*/
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
						//阅读人信息获取处理
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
	}

});