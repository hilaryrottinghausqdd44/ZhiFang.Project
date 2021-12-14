/**
 * 报销单列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.expenseaccount.apply.Grid', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.Grid',
	title: '报销单列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePExpenseAccountByField',
	/**默认员工类型*/
	defaultUserType: '',
	/**默认显示状态*/
	defaultStatusValue: '',
	TemporaryStatus: true,
	defaultWhere: '',

	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ReviewManID', '一审人'],
		['TwoReviewManID', '二审人'],
		['ThreeReviewManID', '三审人'],
		['FourReviewManID', '四审人'],
		['PayManID', '打款负责人'],
		['ReceiveManID', '领款人']
	],
	hiddenIsUse: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('PExpenseAccount_Status');
				if(Status == 1 || Status == 4) {
					me.openEditForm(id);
				} else {
					me.openShowForm(id);
				}
			}
		});
		me.onAddButtons();
	},

    /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PExpenseAccountStatus',function(){
			if(!JShell.System.ClassDict.PExpenseAccountStatus){
    			JShell.Msg.error('未获取到报销状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PExpenseAccountStatus;
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			var UserId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '';
			me.defaultWhere = ' pexpenseaccount.ApplyManID=' + UserId;
	    	me.load(null, true, autoSelect);
    	});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		return columns;
	},

	onAddButtons: function(list) {
		var me = this,
			arr = list || [],
			len = arr.length,
			items = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.insert(1, [{
			xtype: 'button',
			iconCls: 'button-add',
			text: '报销申请',
			tooltip: '报销申请',
			handler: function(but) {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-edit',
			text: '编辑',
			hidden:true,
			tooltip: '编辑',
			handler: function(but) {
				var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.PKField);
				var Status = records[0].get('PExpenseAccount_Status');
				if(Status == 1 || Status == 4) {
					me.openEditForm(id);
				}
			}
		}]);
	},
	/**新增任务*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.apply.AddPanel', {
			title: '报销申请',
			FormConfig: {},
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**修改*/
	openEditForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.apply.EditPanel', {
			PK: id,
			formtype: "edit",
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(6, 0, {
			xtype: 'actioncolumn',
			text: '撤回',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var Status = record.get('PExpenseAccount_Status');
					var ReviewDate = record.get('PExpenseAccount_ReviewDate');
					//申请					
					if(Status == 2 && ReviewDate == '') {
						meta.tdAttr = 'data-qtip="<b>撤回申请</b>"';
						return 'button-save hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onBackApply(id);
				}
			}]
		});

		columns.splice(8, 0, {
			text: '启用',
			dataIndex: 'PExpenseAccount_IsUse',
			width: 40,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			xtype: 'actioncolumn',
			text: '启/禁',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var reviewDate = record.get('PExpenseAccount_ReviewDate');
					var status = record.get('PExpenseAccount_Status').toString();
					var isShow = false;
					switch(status) {
						case "1": //
							isShow = true;
							break;
						default:
							isShow = false;
							break;
					}
					if(isShow == true) {
						if(record.get('PExpenseAccount_IsUse') == "true") {
							meta.tdAttr = 'data-qtip="<b>禁用</b>"';
							//meta.style = 'background-color:green;';
							return 'button-edit hand';
						} else {
							meta.tdAttr = 'data-qtip="<b>启用</b>"';
							//meta.style = 'background-color:red;';
							return 'button-edit hand';
						}
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isUse = rec.get('PExpenseAccount_IsUse');
					var msg = isUse.toString() == "true" ? "是否禁用该报销单?" : "是否启用该报销单?";
					var newIsUse = isUse.toString() == "true" ? 0 : 1;
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.UpdateIsUseByStrIds(rec, newIsUse);
							}
						}
					});
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openShowForm(id);
				}
			}]
		});
		return columns;
	},
	/**撤回*/
	onBackApply: function(id) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);

		JShell.Msg.confirm({
			msg: '确定要撤回申请吗？'
		}, function(but) {
			if(but != "ok") return;
			var params = {
				entity: {
					Id: id,
					Status: 1
				},
				fields: 'Id,Status'
			};
			me.showMask('报销申请撤回中'); //显示遮罩层
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					me.onSearch();
				} else {
					var msg = data.msg ? data.msg : '报销状态已经更改，请刷新列表后再操作。</br>如果想撤回报销申请，请联系一审人员，让其主动退回。';
					JShell.Msg.error(msg);
				}
			});
		});
	},
	/*报销启用或禁用操作**/
	UpdateIsUseByStrIds: function(rec, newIsUse) {
		var me = this;
		var id = rec.get(me.PKField);
		var isUse = rec.get('PExpenseAccount_IsUse');
		url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var msgInfo = "";
		if(isUse.toString() == "true" || isUse.toString() == "1") {
			msgInfo = "报销单禁用";
		} else {
			msgInfo = "报销单启用";
		}
		var entity = {
			Id: id,
			Status: rec.get('PExpenseAccount_Status'),
			IsUse: newIsUse,
			OperationMemo: msgInfo
		};
		var params = {
			entity: entity,
			fields: "Id,Status,IsUse"
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.set("PExpenseAccount_Status", newIsUse);
				//me.getView().refresh();
				rec.commit();
				JShell.Msg.alert(msgInfo + "成功", null, 1000);
				me.onSearch();
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo + "<br />" + data.msg);
			}
		});
	},
	/**功能按钮栏1监听*/
	doButtonsToolbarListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		var StatusID = buttonsToolbar.getComponent('StatusID');
		var checkIsUse = buttonsToolbar.getComponent('checkIsUse');
		//报销状态
		if(StatusID) {
			StatusID.on({
				change: function() {
					me.onGridSearch();
				}
			});
		}
		if(checkIsUse) {
			checkIsUse.on({
				change: function() {
					me.onGridSearch();
				}
			});
		}
	}
});