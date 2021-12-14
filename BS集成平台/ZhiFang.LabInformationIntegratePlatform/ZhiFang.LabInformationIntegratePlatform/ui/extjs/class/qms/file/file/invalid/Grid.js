/**
 * 文档作废
 * @author longfc
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.invalid.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档作废',
	width: 1200,
	height: 800,
	/**默认加载数据*/
	defaultLoad: true,
	/**文档状态默认为发布*/
	defaultStatusValue: "5",

	/**是否隐藏文档状态选择项*/
	hiddenFFileStatus: false,
	interactionType: "show",
	hasDel: false,
	hideDelColumn: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.PublisherDateTime',
	isSearchUSERID: false,
	
	hasInteraction: true,
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	/**是否显示操作记录页签*/
	hasOperation: true,
	/**是否显示阅读记录页签*/
	hasReadingLog: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.openShowTabPanel(record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "') or(ffile.EndTime>='" + dt + "'))";

		me.callParent(arguments);
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '文档ID',
			dataIndex: 'FFile_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '文档标题',
			dataIndex: 'FFile_Title',
			width: 130,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '文档状态',
			dataIndex: 'FFile_Status',
			width: 70,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.FFileStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value];
				return v;
			}
		}, {
			text: '类型',
			dataIndex: 'FFile_BDictTree_CName',
			hidden: false,
			width: 110,
			hideable: false
		});
		columns.push({
			text: '发布人',
			dataIndex: 'FFile_PublisherName',
			width: 100,
			sortable: false,
			menuDisabled: true
		}, {
			text: '发布时间',
			dataIndex: 'FFile_PublisherDateTime',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			text: '是否允许评论',
			dataIndex: 'FFile_IsDiscuss',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '作废',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('FFile_Status') == '5') {
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('FFile_Id');
					var msg = "请确认是否作废?";
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								if(id != '') {
									me.UpdateFFileStatus(7, id, 7, rec);
								}
							}
						}
					});
				}
			}]
		});
		//修订时间
		columns.push({
			text: '修订时间',dataIndex: 'FFile_ReviseTime',
			hidden: true,isDate: true,hasTime: true,
			sortable: true,width: 100,hideable: false
		});
		//是否有交流列
		columns.push(me.createInteraction());
		//是否有操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		me.createOtherColumn(columns);
		return columns;
	},
	/**作废*/
	UpdateFFileStatus: function(fFileStatus, id, fFileOperationType, rec) {
		var me = this;
		var url = '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField';
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			Status: fFileStatus,
			Id: id
		};
		var fields = 'Id,Status';
		var params = {
			entity: entity,
			//文档操作记录备注信息
			ffileOperationMemo: '作废',
			fFileOperationType: fFileOperationType,
			ffileCopyUserType: -1,
			fFileCopyUserList: [],
			ffileReadingUserType: -1,
			fFileReadingUserList: [],
			fields: fields
		};
		//抄送人信息
		var ffileCopyUser = null;
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.store.remove(rec);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**文档状态下拉选择框数据处理*/
	removeDataList: function(dataList) {
		var me = this;
		var me = this;
		var returndata=[];
		if(!dataList) return returndata;
		var addIdStr = ["5","7"]; //发布,作废
		for(var i = 0; i < dataList.length; i++) {
			var model = dataList[i];
			if(model && Ext.Array.indexOf(addIdStr,"" + model[0])>-1) {
				returndata.push(model);
			}
		}
		return returndata;
	}
});